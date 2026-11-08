using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace cs2_things
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button_cs2_root_autodetect.Click += button_cs2_root_autodetect_Click;
            button_cs2_root.Click += button_cs2_root_Click;
            button_cs2fixescfg.Click += delegate { SelectFile(textbox_cs2fixescfg, "Config files (*.cfg)|*.cfg|All files (*.*)|*.*", "Select cs2fixes.cfg"); };
            button_metamod.Click += delegate { SelectZip(textbox_metamod, "Select the Metamod zip"); };
            button_metamod_launcher.Click += delegate { SelectZip(textbox_metamod_launcher, "Select the Metamod Launcher zip"); };
            button_cs2fixes.Click += delegate { SelectZip(textbox_cs2fixes, "Select the CS2Fixes zip"); };
            button_strippercs2.Click += delegate { SelectZip(textbox_strippercs2, "Select the StripperCS2 zip"); };
            button_gfl_ze_configs.Click += delegate { SelectZip(textbox_gfl_ze_configs, "Select the GFL CS2 ZE Configs zip"); };
            button_install.Click += button_install_Click;
            button_launch_cs2.Click += button_launch_cs2_Click;
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
            var request = new Cs2InstallationRequest
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

            button_install.Enabled = false;
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
                button_install.Enabled = true;
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
            else if (message.StartsWith("Install complete", StringComparison.OrdinalIgnoreCase))
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
    }
}
