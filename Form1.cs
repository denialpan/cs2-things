using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace cs2_things
{
    public partial class Form1 : Form
    {
        private const int AutojoinerQueryTimeoutMilliseconds = 3000;
        private const int AutojoinerConnectAttemptMilliseconds = 10000;

        private CancellationTokenSource autojoinerCancellation;
        private Task autojoinerTask;

        public Form1()
        {
            InitializeComponent();
            LoadProgramState();

            button_cs2_root_autodetect.Click += button_cs2_root_autodetect_Click;
            button_cs2_root.Click += button_cs2_root_Click;
            button_cs2fixescfg.Click += delegate { SelectFile(textbox_cs2fixescfg, "Config files (*.cfg)|*.cfg|All files (*.*)|*.*", "Select cs2fixes.cfg"); };
            button_metamod.Click += delegate { SelectZip(textbox_metamod, "Select the Metamod zip"); };
            button_metamod_launcher.Click += delegate { SelectZip(textbox_metamod_launcher, "Select the Metamod Launcher zip"); };
            button_cs2fixes.Click += delegate { SelectZip(textbox_cs2fixes, "Select the CS2Fixes zip"); };
            button_strippercs2.Click += delegate { SelectZip(textbox_strippercs2, "Select the StripperCS2 zip"); };
            button_gfl_ze_configs.Click += delegate { SelectZip(textbox_gfl_ze_configs, "Select the GFL CS2 ZE Configs zip"); };
            button_install.Click += button_install_Click;
            button_uninstall.Click += button_uninstall_Click;
            button_launch_cs2.Click += button_launch_cs2_Click;
            button_autojoiner_start.Click += button_autojoiner_start_Click;
            tab_main.SelectedIndexChanged += tab_main_SelectedIndexChanged;
            UpdateMinimumHeightForSelectedTab();
        }

        private void tab_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMinimumHeightForSelectedTab();
        }

        private void UpdateMinimumHeightForSelectedTab()
        {
            bool autojoinerSelected = tab_main.SelectedTab == tabpage_autojoiner;
            MinimumSize = new System.Drawing.Size(
                MinimumSize.Width,
                autojoinerSelected ? 434 : 515);

            if (autojoinerSelected)
            {
                Height = 434;
            }
        }

        private async void button_uninstall_Click(object sender, EventArgs e)
        {
            var uninstallTimer = Stopwatch.StartNew();

            if (MessageBox.Show(
                    this,
                    "Remove CS2Fixes and related files?",
                    "Confirm uninstall",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            string cs2Root = textbox_cs2_root.Text.Trim();
            IProgress<string> progress = new Progress<string>(AppendConsoleLine);
            rich_console.Clear();
            button_uninstall.Enabled = false;
            button_install.Enabled = false;
            SetInstallInputsEnabled(false);
            UseWaitCursor = true;

            try
            {
                int deletedFiles = await Task.Run(() =>
                {
                    int removed = LaunchCS2.RestoreNormalLaunchState(cs2Root, progress.Report);
                    removed += InstallRollback.Uninstall(cs2Root, progress.Report);
                    return removed;
                });
                uninstallTimer.Stop();
                AppendConsoleLine(
                    "Uninstall complete: " + deletedFiles + " files deleted in " +
                    uninstallTimer.Elapsed.TotalSeconds.ToString("F2") + " seconds.");
                MessageBox.Show(
                    this,
                    deletedFiles + " files were deleted in " +
                    uninstallTimer.Elapsed.TotalSeconds.ToString("F2") + " seconds.",
                    "Uninstall complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception error)
            {
                AppendConsoleLine("ERROR: " + error.Message);
                MessageBox.Show(
                    this,
                    error.Message,
                    "Uninstall failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                SetInstallInputsEnabled(true);
                button_install.Enabled = true;
                button_uninstall.Enabled = true;
            }
        }

        private void LoadProgramState()
        {
            try
            {
                ProgramStateData state = ProgramStateConfig.Load();
                textbox_cs2_root.Text = state.Cs2Root;
                textbox_steamid64.Text = state.SteamId64;
                textbox_cs2fixescfg.Text = state.Cs2FixesConfig;
                textbox_metamod.Text = state.MetamodZip;
                textbox_metamod_launcher.Text = state.MetamodLauncherZip;
                textbox_cs2fixes.Text = state.Cs2FixesZip;
                textbox_strippercs2.Text = state.StripperCs2Zip;
                textbox_gfl_ze_configs.Text = state.GflZeConfigsZip;
                textbox_autojoiner_ip.Text = state.AutojoinerIp;
                textbox_autojoiner_port.Text = state.AutojoinerPort;
                textbox_autojoiner_username.Text = state.AutojoinerUsername;
                SetNumericValue(num_autojoiner_threshold, state.AutojoinerThreshold);
                SetNumericValue(num_autojoiner_max, state.AutojoinerServerMax);
                SetNumericValue(num_autojoiner_interval, state.AutojoinerIntervalMilliseconds);
                checkbox_autojoiner_sound.Checked = state.AutojoinerSound;
                checkbox_cs2fixes.Checked = state.LaunchWithCs2Fixes;
                checkbox_tools.Checked = state.LaunchWorkshopTools;

                if (state.SelectedTab >= 0 && state.SelectedTab < tab_main.TabCount)
                {
                    tab_main.SelectedIndex = state.SelectedTab;
                }

                if (state.HasWindowBounds)
                {
                    var savedBounds = new System.Drawing.Rectangle(
                        state.WindowX,
                        state.WindowY,
                        Math.Max(state.WindowWidth, MinimumSize.Width),
                        Math.Max(state.WindowHeight, MinimumSize.Height));

                    bool visibleOnScreen = false;
                    foreach (Screen screen in Screen.AllScreens)
                    {
                        if (screen.WorkingArea.IntersectsWith(savedBounds))
                        {
                            visibleOnScreen = true;
                            break;
                        }
                    }

                    if (visibleOnScreen)
                    {
                        StartPosition = FormStartPosition.Manual;
                        Bounds = savedBounds;
                    }
                }

                if (state.WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception error)
            {
                AppendConsoleLine("ERROR: Could not load config.ini: " + error.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (autojoinerCancellation != null)
            {
                autojoinerCancellation.Cancel();
            }

            try
            {
                System.Drawing.Rectangle savedBounds = WindowState == FormWindowState.Normal
                    ? Bounds
                    : RestoreBounds;

                ProgramStateConfig.Save(new ProgramStateData
                {
                    Cs2Root = textbox_cs2_root.Text.Trim(),
                    SteamId64 = textbox_steamid64.Text.Trim(),
                    Cs2FixesConfig = textbox_cs2fixescfg.Text.Trim(),
                    MetamodZip = textbox_metamod.Text.Trim(),
                    MetamodLauncherZip = textbox_metamod_launcher.Text.Trim(),
                    Cs2FixesZip = textbox_cs2fixes.Text.Trim(),
                    StripperCs2Zip = textbox_strippercs2.Text.Trim(),
                    GflZeConfigsZip = textbox_gfl_ze_configs.Text.Trim(),
                    AutojoinerIp = textbox_autojoiner_ip.Text.Trim(),
                    AutojoinerPort = textbox_autojoiner_port.Text.Trim(),
                    AutojoinerUsername = textbox_autojoiner_username.Text.Trim(),
                    AutojoinerThreshold = Decimal.ToInt32(num_autojoiner_threshold.Value),
                    AutojoinerServerMax = Decimal.ToInt32(num_autojoiner_max.Value),
                    AutojoinerIntervalMilliseconds = Decimal.ToInt32(num_autojoiner_interval.Value),
                    AutojoinerSound = checkbox_autojoiner_sound.Checked,
                    LaunchWithCs2Fixes = checkbox_cs2fixes.Checked,
                    LaunchWorkshopTools = checkbox_tools.Checked,
                    SelectedTab = tab_main.SelectedIndex,
                    HasWindowBounds = true,
                    WindowX = savedBounds.X,
                    WindowY = savedBounds.Y,
                    WindowWidth = savedBounds.Width,
                    WindowHeight = savedBounds.Height,
                    WindowState = WindowState == FormWindowState.Maximized
                        ? FormWindowState.Maximized
                        : FormWindowState.Normal
                });
            }
            catch (Exception error)
            {
                MessageBox.Show(
                    this,
                    "Could not save config.ini.\r\n\r\n" + error.Message,
                    "Save settings failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            base.OnFormClosing(e);
        }

        private async void button_launch_cs2_Click(object sender, EventArgs e)
        {
            string cs2Root = textbox_cs2_root.Text.Trim();
            bool launchWithCs2Fixes = checkbox_cs2fixes.Checked;
            bool launchWorkshopTools = checkbox_tools.Checked;
            IProgress<string> progress = new Progress<string>(AppendConsoleLine);

            rich_console.Clear();
            button_launch_cs2.Enabled = false;
            UseWaitCursor = true;

            try
            {
                await Task.Run(() => LaunchCS2.Launch(
                    cs2Root,
                    launchWithCs2Fixes,
                    launchWorkshopTools,
                    progress.Report));
            }
            catch (FileNotFoundException error)
            {
                string missingFile = string.IsNullOrWhiteSpace(error.FileName)
                    ? error.Message
                    : error.FileName;
                string message = "Could not find required file: " + missingFile;
                AppendConsoleLine("ERROR: " + message);
                MessageBox.Show(
                    this,
                    message,
                    "Launch failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                AppendConsoleLine("ERROR: " + error.Message);
                MessageBox.Show(
                    this,
                    error.Message,
                    "Launch failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                button_launch_cs2.Enabled = true;
            }
        }

        private async void button_autojoiner_start_Click(object sender, EventArgs e)
        {
            if (autojoinerCancellation != null)
            {
                AppendConsoleLine("Stopping autojoiner.");
                button_autojoiner_start.Enabled = false;
                autojoinerCancellation.Cancel();
                return;
            }

            string serverHost = textbox_autojoiner_ip.Text.Trim();
            string playerName = textbox_autojoiner_username.Text.Trim();
            int serverPort;
            if (serverHost.Length == 0)
            {
                ShowAutojoinerError("Enter an autojoiner server IP or hostname first.");
                return;
            }
            if (!int.TryParse(textbox_autojoiner_port.Text.Trim(), out serverPort) ||
                serverPort < 1 || serverPort > 65535)
            {
                ShowAutojoinerError("Enter a numeric autojoiner server port from 1 through 65535.");
                return;
            }
            if (playerName.Length == 0)
            {
                ShowAutojoinerError("Enter a player name to check for first.");
                return;
            }
            if (num_autojoiner_interval.Value < 1 ||
                num_autojoiner_interval.Value > int.MaxValue)
            {
                ShowAutojoinerError("Enter an autojoiner interval from 1 through 2147483647 milliseconds.");
                return;
            }
            if (checkbox_cs2fixes.Checked || checkbox_tools.Checked)
            {
                ShowAutojoinerError(
                    "Disable CS2Fixes and workshop tools before starting the autojoiner.");
                return;
            }

            string cs2Root = textbox_cs2_root.Text.Trim();
            if (!Directory.Exists(cs2Root))
            {
                ShowAutojoinerError("Select a valid CS2 root directory first.");
                return;
            }

            int playerThreshold = Decimal.ToInt32(num_autojoiner_threshold.Value);
            int intervalMilliseconds = Decimal.ToInt32(num_autojoiner_interval.Value);
            bool playSound = checkbox_autojoiner_sound.Checked;
            IProgress<string> progress = new Progress<string>(AppendConsoleLine);

            rich_console.Clear();
            button_autojoiner_start.Enabled = false;
            UseWaitCursor = true;
            try
            {
                AppendConsoleLine("Preparing normal CS2 launch for autojoiner.");
                await Task.Run(() => LaunchCS2.Launch(cs2Root, false, false, progress.Report));
            }
            catch (Exception error)
            {
                ShowAutojoinerError(error.Message);
                return;
            }
            finally
            {
                UseWaitCursor = false;
                button_autojoiner_start.Enabled = true;
            }

            var cancellation = new CancellationTokenSource();
            autojoinerCancellation = cancellation;
            SetAutojoinerRunning(true);
            AppendConsoleLine("Starting autojoiner for " + serverHost + ":" + serverPort + ".");

            autojoinerTask = RunAutojoinerAsync(
                serverHost,
                serverPort,
                playerName,
                playerThreshold,
                intervalMilliseconds,
                playSound,
                cancellation.Token);

            try
            {
                await autojoinerTask;
            }
            catch (OperationCanceledException)
            {
                AppendConsoleLine("Autojoiner stopped.");
            }
            finally
            {
                if (ReferenceEquals(autojoinerCancellation, cancellation))
                {
                    autojoinerCancellation = null;
                    autojoinerTask = null;
                }
                cancellation.Dispose();
                SetAutojoinerRunning(false);
            }
        }

        private async Task RunAutojoinerAsync(
            string serverHost,
            int serverPort,
            string playerName,
            int playerThreshold,
            int intervalMilliseconds,
            bool playSound,
            CancellationToken cancellationToken)
        {
            bool connectAttempted = false;
            DateTime playerCheckUntil = DateTime.MinValue;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    SourceServerInfo info = await Task.Run(
                        () => SourceServerQuery.QueryInfo(
                            serverHost,
                            serverPort,
                            AutojoinerQueryTimeoutMilliseconds),
                        cancellationToken);

                    SetNumericValue(num_autojoiner_max, info.MaxPlayers);
                    AppendConsoleLine(
                        "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + info.Map +
                        " - player count: " + info.PlayerCount + "/" + info.MaxPlayers);

                    if (connectAttempted)
                    {
                        AppendConsoleLine("Checking for '" + playerName + "'.");
                        try
                        {
                            IList<string> players = await Task.Run(
                                () => SourceServerQuery.QueryPlayers(
                                    serverHost,
                                    serverPort,
                                    AutojoinerQueryTimeoutMilliseconds),
                                cancellationToken);
                            if (players.Any(name => string.Equals(
                                name,
                                playerName,
                                StringComparison.OrdinalIgnoreCase)))
                            {
                                AppendConsoleLine(
                                    "Detected player '" + playerName +
                                    "' in server; stopping autojoiner.");
                                if (playSound)
                                {
                                    await Task.Run(() => PlayBeeps(
                                        new[] { 600, 800, 1000 },
                                        new[] { 200, 200, 300 }));
                                }
                                return;
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception error)
                        {
                            AppendConsoleLine("Player query failed: " + error.Message);
                        }
                    }

                    if (connectAttempted && DateTime.UtcNow >= playerCheckUntil)
                    {
                        connectAttempted = false;
                    }

                    if (info.PlayerCount < playerThreshold && !connectAttempted)
                    {
                        AppendConsoleLine(
                            "Player count is below " + playerThreshold +
                            "; attempting to connect.");
                        LaunchCS2.ConnectToServer(serverHost, serverPort);
                        if (playSound)
                        {
                            await Task.Run(() => PlayBeeps(
                                new[] { 600, 600 },
                                new[] { 200, 200 }));
                        }
                        connectAttempted = true;
                        playerCheckUntil = DateTime.UtcNow.AddMilliseconds(
                            AutojoinerConnectAttemptMilliseconds);
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception error)
                {
                    AppendConsoleLine("Server query failed: " + error.Message);
                }

                await Task.Delay(intervalMilliseconds, cancellationToken);
            }
        }

        private void SetAutojoinerRunning(bool running)
        {
            textbox_autojoiner_ip.Enabled = !running;
            textbox_autojoiner_port.Enabled = !running;
            textbox_autojoiner_username.Enabled = !running;
            num_autojoiner_threshold.Enabled = !running;
            num_autojoiner_max.Enabled = !running;
            num_autojoiner_interval.Enabled = !running;
            checkbox_autojoiner_sound.Enabled = !running;
            checkbox_cs2fixes.Enabled = !running;
            checkbox_tools.Enabled = !running;
            button_launch_cs2.Enabled = !running;
            button_autojoiner_start.Enabled = true;
            button_autojoiner_start.Text = running ? "stop" : "start";
        }

        private void ShowAutojoinerError(string message)
        {
            AppendConsoleLine("ERROR: " + message);
            MessageBox.Show(
                this,
                message,
                "Autojoiner failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void SetNumericValue(NumericUpDown control, int value)
        {
            decimal bounded = Math.Max(control.Minimum, Math.Min(control.Maximum, value));
            control.Value = bounded;
        }

        private static void PlayBeeps(int[] frequencies, int[] durations)
        {
            try
            {
                for (int index = 0; index < frequencies.Length && index < durations.Length; index++)
                {
                    Console.Beep(frequencies[index], durations[index]);
                }
            }
            catch (Exception)
            {
            }
        }

        private void SelectZip(TextBox target, string title)
        {
            SelectFile(target, "Zip files (*.zip)|*.zip|All files (*.*)|*.*", title);
        }

        private void SelectFile(TextBox target, string filter, string title)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = title;
                dialog.Filter = filter;
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;

                if (File.Exists(target.Text))
                {
                    dialog.FileName = target.Text;
                }

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    target.Text = dialog.FileName;
                }
            }
        }

        private async void button_install_Click(object sender, EventArgs e)
        {
            Cs2InstallationRequest request = CaptureInstallationRequest();

            button_install.Enabled = false;
            SetInstallInputsEnabled(false);
            UseWaitCursor = true;
            rich_console.Clear();
            IProgress<string> progress = new Progress<string>(AppendConsoleLine);

            try
            {
                IList<string> results = await Task.Run(
                    () => InstallCS2Fixes.Install(request, progress.Report));
                MessageBox.Show(
                    this,
                    "Installation completed successfully.\r\n\r\n" +
                    string.Join("\r\n", results),
                    "Install complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Cs2InstallationException error)
            {
                AppendConsoleLine("ERROR: " + error.Message);
                MessageBox.Show(
                    this,
                    error.Message,
                    error.Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                AppendConsoleLine("ERROR: " + error.Message);
                MessageBox.Show(
                    this,
                    "Could not complete the installation.\r\n\r\n" + error.Message,
                    "Install failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                SetInstallInputsEnabled(true);
                button_install.Enabled = true;
            }
        }

        private Cs2InstallationRequest CaptureInstallationRequest()
        {
            return new Cs2InstallationRequest
            {
                Cs2Root = textbox_cs2_root.Text.Trim(),
                SteamId64 = textbox_steamid64.Text.Trim(),
                Cs2FixesConfig = textbox_cs2fixescfg.Text.Trim(),
                MetamodZip = textbox_metamod.Text.Trim(),
                MetamodLauncherZip = textbox_metamod_launcher.Text.Trim(),
                Cs2FixesZip = textbox_cs2fixes.Text.Trim(),
                StripperCs2Zip = textbox_strippercs2.Text.Trim(),
                GflZeConfigsZip = textbox_gfl_ze_configs.Text.Trim()
            };
        }

        private void SetInstallInputsEnabled(bool enabled)
        {
            foreach (Control control in new Control[]
            {
                textbox_cs2_root,
                textbox_steamid64,
                textbox_cs2fixescfg,
                textbox_metamod,
                textbox_metamod_launcher,
                textbox_cs2fixes,
                textbox_strippercs2,
                textbox_gfl_ze_configs,
                button_cs2_root,
                button_cs2_root_autodetect,
                button_cs2fixescfg,
                button_metamod,
                button_metamod_launcher,
                button_cs2fixes,
                button_strippercs2,
                button_gfl_ze_configs
            })
            {
                control.Enabled = enabled;
            }
        }

        private void AppendConsoleLine(string message)
        {
            if (rich_console.IsDisposed)
            {
                return;
            }

            System.Drawing.Color messageColor = rich_console.ForeColor;
            if (message.StartsWith("ERROR:", StringComparison.OrdinalIgnoreCase))
            {
                messageColor = System.Drawing.Color.Firebrick;
            }
            else if (message.StartsWith("Install complete", StringComparison.OrdinalIgnoreCase) ||
                     message.StartsWith("Uninstall complete", StringComparison.OrdinalIgnoreCase))
            {
                messageColor = System.Drawing.Color.ForestGreen;
            }

            rich_console.SelectionStart = rich_console.TextLength;
            rich_console.SelectionLength = 0;
            rich_console.SelectionColor = messageColor;
            rich_console.AppendText(message + Environment.NewLine);
            rich_console.SelectionColor = rich_console.ForeColor;
            rich_console.ScrollToCaret();
        }

        private void button_cs2_root_autodetect_Click(object sender, EventArgs e)
        {
            AutoDetectCs2Root();
        }

        private void button_cs2_root_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the Counter-Strike 2 root directory";
                dialog.ShowNewFolderButton = false;

                if (Directory.Exists(textbox_cs2_root.Text))
                {
                    dialog.SelectedPath = textbox_cs2_root.Text;
                }

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    textbox_cs2_root.Text = dialog.SelectedPath;
                }
            }
        }

        private void AutoDetectCs2Root()
        {
            string cs2Root = FindCs2Root();

            if (cs2Root != null)
            {
                textbox_cs2_root.Text = cs2Root;
                return;
            }

            MessageBox.Show(
                this,
                "Counter-Strike 2 was not found in any configured Steam library.",
                "CS2 not found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static string FindCs2Root()
        {
            var steamRoots = GetSteamInstallPaths();
            var libraries = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (string steamRoot in steamRoots)
            {
                AddSteamLibrary(libraries, steamRoot);

                string libraryFile = Path.Combine(steamRoot, "steamapps", "libraryfolders.vdf");
                if (!File.Exists(libraryFile))
                {
                    continue;
                }

                try
                {
                    string contents = File.ReadAllText(libraryFile);
                    MatchCollection matches = Regex.Matches(
                        contents,
                        "\"path\"\\s*\"([^\"]+)\"",
                        RegexOptions.IgnoreCase);

                    foreach (Match match in matches)
                    {
                        AddSteamLibrary(libraries, UnescapeVdfValue(match.Groups[1].Value));
                    }
                }
                catch (IOException)
                {
                    // Steam may be rewriting this file; the default library is still checked.
                }
                catch (UnauthorizedAccessException)
                {
                    // Continue checking any other readable Steam installations.
                }
            }

            foreach (string library in libraries)
            {
                string steamApps = Path.Combine(library, "steamapps");
                string manifest = Path.Combine(steamApps, "appmanifest_730.acf");
                if (!File.Exists(manifest))
                {
                    continue;
                }

                try
                {
                    string contents = File.ReadAllText(manifest);
                    Match installDirectory = Regex.Match(
                        contents,
                        "\"installdir\"\\s*\"([^\"]+)\"",
                        RegexOptions.IgnoreCase);

                    if (!installDirectory.Success)
                    {
                        continue;
                    }

                    string candidate = Path.Combine(
                        steamApps,
                        "common",
                        UnescapeVdfValue(installDirectory.Groups[1].Value));

                    if (Directory.Exists(candidate))
                    {
                        return Path.GetFullPath(candidate);
                    }
                }
                catch (IOException)
                {
                    // Try the next configured library.
                }
                catch (UnauthorizedAccessException)
                {
                    // Try the next configured library.
                }
            }

            return null;
        }

        private static HashSet<string> GetSteamInstallPaths()
        {
            var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            AddRegistrySteamPath(paths, RegistryHive.CurrentUser, RegistryView.Default,
                @"Software\Valve\Steam", "SteamPath");
            AddRegistrySteamPath(paths, RegistryHive.LocalMachine, RegistryView.Registry64,
                @"Software\Valve\Steam", "InstallPath");
            AddRegistrySteamPath(paths, RegistryHive.LocalMachine, RegistryView.Registry32,
                @"Software\Valve\Steam", "InstallPath");

            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            if (!string.IsNullOrWhiteSpace(programFilesX86))
            {
                AddExistingPath(paths, Path.Combine(programFilesX86, "Steam"));
            }

            return paths;
        }

        private static void AddRegistrySteamPath(
            HashSet<string> paths,
            RegistryHive hive,
            RegistryView view,
            string subKey,
            string valueName)
        {
            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, view))
                using (RegistryKey key = baseKey.OpenSubKey(subKey))
                {
                    AddExistingPath(paths, key == null ? null : key.GetValue(valueName) as string);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Fall back to the other registry locations and the conventional Steam path.
            }
            catch (System.Security.SecurityException)
            {
                // Fall back to the other registry locations and the conventional Steam path.
            }
        }

        private static void AddSteamLibrary(HashSet<string> libraries, string path)
        {
            AddExistingPath(libraries, path);
        }

        private static void AddExistingPath(HashSet<string> paths, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            try
            {
                string fullPath = Path.GetFullPath(path.Replace('/', Path.DirectorySeparatorChar));
                if (Directory.Exists(fullPath))
                {
                    paths.Add(fullPath);
                }
            }
            catch (ArgumentException)
            {
            }
            catch (NotSupportedException)
            {
            }
            catch (PathTooLongException)
            {
            }
        }

        private static string UnescapeVdfValue(string value)
        {
            return value.Replace("\\\\", "\\");
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);
            SuspendLayout();
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout(true);
            base.OnResizeEnd(e);
        }

        private void SayHelloButton_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void downloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var linkLabel = sender as LinkLabel;
            string url = linkLabel == null ? null : linkLabel.Tag as string;
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
                linkLabel.LinkVisited = true;
            }
            catch (Exception error)
            {
                MessageBox.Show(
                    this,
                    "Could not open the link.\r\n\r\n" + error.Message,
                    "Open link failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkbox_autojoiner_sound_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
