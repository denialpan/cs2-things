using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace cs2_things
{
    internal static class Program
    {
        private const uint AttachParentProcess = 0xFFFFFFFF;
        private const int CliAutojoinerPlayerThreshold = 62;
        private const int CliAutojoinerIntervalMilliseconds = 1500;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AttachConsole(uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [STAThread]
        private static int Main(string[] args)
        {
            CommandLineOptions options;
            string error;
            if (!CommandLineOptions.TryParse(args, out options, out error))
            {
                InitializeConsole();
                Console.Error.WriteLine("Error: " + error);
                Console.Error.WriteLine("Use -h or --help to list valid flags.");
                return 2;
            }

            if (options.Help)
            {
                InitializeConsole();
                PrintHelp();
                return 0;
            }

            if (args.Length == 0 || options.Gui)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                return 0;
            }

            InitializeConsole();
            return RunCommandLine(options);
        }

        private static int RunCommandLine(CommandLineOptions options)
        {
            ProgramStateData state;
            try
            {
                state = ProgramStateConfig.Load();
            }
            catch (Exception error)
            {
                Console.Error.WriteLine("Could not load config.ini: " + error.Message);
                return 1;
            }

            string cs2Root = state.Cs2Root;
            if (!Directory.Exists(cs2Root))
            {
                cs2Root = Form1.FindCs2Root();
            }
            if (!Directory.Exists(cs2Root))
            {
                Console.Error.WriteLine(
                    "Could not find a valid CS2 root directory. Open the GUI with -gui and select it.");
                return 1;
            }

            try
            {
                LaunchCS2.Launch(
                    cs2Root,
                    options.Cs2Fixes,
                    options.Tools,
                    Console.WriteLine);
            }
            catch (FileNotFoundException error)
            {
                string missing = string.IsNullOrWhiteSpace(error.FileName)
                    ? error.Message
                    : error.FileName;
                Console.Error.WriteLine("Launch failed. Could not find required file: " + missing);
                return 1;
            }
            catch (Exception error)
            {
                Console.Error.WriteLine("Launch failed. " + error.Message);
                return 1;
            }

            if (!options.Autojoiner)
            {
                return 0;
            }

            using (var cancellation = new CancellationTokenSource())
            {
                ConsoleCancelEventHandler cancelHandler = delegate(object sender, ConsoleCancelEventArgs eventArgs)
                {
                    eventArgs.Cancel = true;
                    cancellation.Cancel();
                };
                Console.CancelKeyPress += cancelHandler;
                try
                {
                    Console.WriteLine(
                        "Starting autojoiner for " + options.Ip + ":" + options.Port + ".");
                    Cs2Autojoiner.RunAsync(
                        options.Ip,
                        options.Port.Value,
                        options.Name,
                        CliAutojoinerPlayerThreshold,
                        CliAutojoinerIntervalMilliseconds,
                        true,
                        null,
                        Console.WriteLine,
                        cancellation.Token).GetAwaiter().GetResult();
                    return 0;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Autojoiner stopped.");
                    return 130;
                }
                finally
                {
                    Console.CancelKeyPress -= cancelHandler;
                }
            }
        }

        private static void InitializeConsole()
        {
            if (!AttachConsole(AttachParentProcess))
            {
                AllocConsole();
            }

            try
            {
                var output = new StreamWriter(Console.OpenStandardOutput(), new UTF8Encoding(false));
                output.AutoFlush = true;
                Console.SetOut(output);

                var error = new StreamWriter(Console.OpenStandardError(), new UTF8Encoding(false));
                error.AutoFlush = true;
                Console.SetError(error);
            }
            catch (IOException)
            {
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("CS2 Things");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  cs2-things.exe -gui");
            Console.WriteLine("  cs2-things.exe -vanilla");
            Console.WriteLine("  cs2-things.exe [-cs2fixes] [-tools]");
            Console.WriteLine("  cs2-things.exe -autojoiner -ip <host> -port <port> -name <player>");
            Console.WriteLine();
            Console.WriteLine("Valid flags:");
            Console.WriteLine("  -gui          Launch the graphical interface.");
            Console.WriteLine("  -vanilla      Launch CS2 without CS2Fixes or Workshop Tools.");
            Console.WriteLine("  -tools        Launch CS2 Workshop Tools.");
            Console.WriteLine("  -cs2fixes     Launch CS2 with CS2Fixes.");
            Console.WriteLine("  -autojoiner   Launch normal CS2 and run the Autojoiner.");
            Console.WriteLine("  -ip <host>    Autojoiner server IP address or hostname.");
            Console.WriteLine("  -port <port>  Autojoiner server port from 1 through 65535.");
            Console.WriteLine("  -name <name>  Player name used to detect a successful connection.");
            Console.WriteLine("  -h, --help    Show this help text.");
            Console.WriteLine();
            Console.WriteLine("With no flags, the graphical interface is launched.");
            Console.WriteLine("-tools and -cs2fixes can be combined.");
        }

        private sealed class CommandLineOptions
        {
            public bool Gui { get; private set; }
            public bool Vanilla { get; private set; }
            public bool Tools { get; private set; }
            public bool Cs2Fixes { get; private set; }
            public bool Autojoiner { get; private set; }
            public bool Help { get; private set; }
            public string Ip { get; private set; }
            public int? Port { get; private set; }
            public string Name { get; private set; }

            public static bool TryParse(
                string[] args,
                out CommandLineOptions options,
                out string error)
            {
                options = new CommandLineOptions();
                error = null;

                for (int index = 0; index < args.Length; index++)
                {
                    string argument = args[index];
                    switch (argument)
                    {
                        case "-gui":
                            options.Gui = true;
                            break;
                        case "-vanilla":
                            options.Vanilla = true;
                            break;
                        case "-tools":
                            options.Tools = true;
                            break;
                        case "-cs2fixes":
                            options.Cs2Fixes = true;
                            break;
                        case "-autojoiner":
                            options.Autojoiner = true;
                            break;
                        case "-h":
                        case "--help":
                            options.Help = true;
                            break;
                        case "-ip":
                            string ip;
                            if (!TryReadValue(args, ref index, "-ip", out error, out ip))
                            {
                                return false;
                            }
                            options.Ip = ip;
                            break;
                        case "-port":
                            string portText;
                            if (!TryReadValue(args, ref index, "-port", out error, out portText))
                            {
                                return false;
                            }
                            int port;
                            if (!int.TryParse(portText, out port))
                            {
                                error = "-port requires an integer value.";
                                return false;
                            }
                            options.Port = port;
                            break;
                        case "-name":
                            string name;
                            if (!TryReadValue(args, ref index, "-name", out error, out name))
                            {
                                return false;
                            }
                            options.Name = name;
                            break;
                        default:
                            error = "Unknown flag: " + argument;
                            return false;
                    }
                }

                bool autojoinerFlagPresent =
                    options.Autojoiner ||
                    options.Ip != null ||
                    options.Port.HasValue ||
                    options.Name != null;
                if (options.Vanilla &&
                    (options.Tools || options.Cs2Fixes || autojoinerFlagPresent))
                {
                    error = "-vanilla cannot be combined with launch or Autojoiner flags.";
                    return false;
                }
                if (autojoinerFlagPresent && (options.Tools || options.Cs2Fixes))
                {
                    error = "Autojoiner flags cannot be combined with -tools or -cs2fixes.";
                    return false;
                }

                if (options.Port.HasValue &&
                    (options.Port.Value < 1 || options.Port.Value > 65535))
                {
                    error = "-port must be from 1 through 65535.";
                    return false;
                }

                if (options.Autojoiner)
                {
                    var missing = new List<string>();
                    if (string.IsNullOrWhiteSpace(options.Ip)) missing.Add("-ip");
                    if (!options.Port.HasValue) missing.Add("-port");
                    if (string.IsNullOrWhiteSpace(options.Name)) missing.Add("-name");
                    if (missing.Count > 0)
                    {
                        error = "-autojoiner requires " + string.Join(" ", missing.ToArray()) + ".";
                        return false;
                    }
                }

                return true;
            }

            private static bool TryReadValue(
                string[] args,
                ref int index,
                string flag,
                out string error,
                out string value)
            {
                error = null;
                value = null;
                if (index + 1 >= args.Length)
                {
                    error = flag + " requires a value.";
                    return false;
                }

                index++;
                value = args[index];
                return true;
            }
        }
    }
}
