using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace cs2_things
{
    internal sealed class ProgramStateData
    {
        public string Cs2Root { get; set; }
        public string SteamId64 { get; set; }
        public string Cs2FixesConfig { get; set; }
        public string MetamodZip { get; set; }
        public string MetamodLauncherZip { get; set; }
        public string Cs2FixesZip { get; set; }
        public string StripperCs2Zip { get; set; }
        public string GflZeConfigsZip { get; set; }
        public string AutojoinerIp { get; set; }
        public string AutojoinerPort { get; set; }
        public string AutojoinerUsername { get; set; }
        public int AutojoinerThreshold { get; set; }
        public int AutojoinerServerMax { get; set; }
        public int AutojoinerIntervalMilliseconds { get; set; }
        public bool AutojoinerSound { get; set; }
        public bool LaunchWithCs2Fixes { get; set; }
        public bool LaunchWorkshopTools { get; set; }
        public int SelectedTab { get; set; }
        public bool HasWindowBounds { get; set; }
        public int WindowX { get; set; }
        public int WindowY { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public FormWindowState WindowState { get; set; }
    }

    internal static class ProgramStateConfig
    {
        public static readonly string ConfigPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "config.ini");

        public static ProgramStateData Load()
        {
            var state = new ProgramStateData
            {
                AutojoinerThreshold = 62,
                AutojoinerServerMax = 64,
                AutojoinerIntervalMilliseconds = 1500,
                WindowState = FormWindowState.Normal
            };

            if (!File.Exists(ConfigPath))
            {
                return state;
            }

            Dictionary<string, Dictionary<string, string>> values = ParseIni(ConfigPath);
            state.Cs2Root = Get(values, "fields", "cs2_root");
            state.SteamId64 = Get(values, "fields", "steamid64");
            state.Cs2FixesConfig = Get(values, "fields", "cs2fixes_cfg");
            state.MetamodZip = Get(values, "fields", "zip_metamod");
            state.MetamodLauncherZip = Get(values, "fields", "zip_metamod_launcher");
            state.Cs2FixesZip = Get(values, "fields", "zip_cs2fixes");
            state.StripperCs2Zip = Get(values, "fields", "zip_strippercs2");
            state.GflZeConfigsZip = Get(values, "fields", "zip_gfl_cs2_ze_configs");
            state.AutojoinerIp = Get(values, "fields", "autojoiner_ip");
            state.AutojoinerPort = Get(values, "fields", "autojoiner_port");
            state.AutojoinerUsername = Get(values, "fields", "autojoiner_username");
            state.AutojoinerThreshold = GetInt(values, "fields", "autojoiner_threshold", 62);
            state.AutojoinerServerMax = GetInt(values, "fields", "autojoiner_server_max", 64);
            state.AutojoinerIntervalMilliseconds = GetInt(
                values, "fields", "autojoiner_interval_ms", 1500);
            state.AutojoinerSound = GetBool(values, "checkboxes", "autojoiner_sound", false);
            state.LaunchWithCs2Fixes = GetBool(
                values, "checkboxes", "launch_with_cs2fixes", false);
            state.LaunchWorkshopTools = GetBool(
                values, "checkboxes", "launch_workshop_tools", false);
            state.SelectedTab = GetInt(values, "window", "selected_tab", 0);

            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            state.HasWindowBounds =
                TryGetInt(values, "window", "x", out x) &&
                TryGetInt(values, "window", "y", out y) &&
                TryGetInt(values, "window", "width", out width) &&
                TryGetInt(values, "window", "height", out height) &&
                width > 0 &&
                height > 0;

            if (state.HasWindowBounds)
            {
                state.WindowX = x;
                state.WindowY = y;
                state.WindowWidth = width;
                state.WindowHeight = height;
            }

            string windowState = Get(values, "window", "state");
            if (string.Equals(windowState, "Maximized", StringComparison.OrdinalIgnoreCase))
            {
                state.WindowState = FormWindowState.Maximized;
            }

            return state;
        }

        public static void Save(ProgramStateData state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("state");
            }

            var output = new StringBuilder();
            output.AppendLine("[fields]");
            Append(output, "cs2_root", state.Cs2Root);
            Append(output, "steamid64", state.SteamId64);
            Append(output, "cs2fixes_cfg", state.Cs2FixesConfig);
            Append(output, "zip_metamod", state.MetamodZip);
            Append(output, "zip_metamod_launcher", state.MetamodLauncherZip);
            Append(output, "zip_cs2fixes", state.Cs2FixesZip);
            Append(output, "zip_strippercs2", state.StripperCs2Zip);
            Append(output, "zip_gfl_cs2_ze_configs", state.GflZeConfigsZip);
            Append(output, "autojoiner_ip", state.AutojoinerIp);
            Append(output, "autojoiner_port", state.AutojoinerPort);
            Append(output, "autojoiner_username", state.AutojoinerUsername);
            Append(output, "autojoiner_threshold", state.AutojoinerThreshold.ToString());
            Append(output, "autojoiner_server_max", state.AutojoinerServerMax.ToString());
            Append(output, "autojoiner_interval_ms", state.AutojoinerIntervalMilliseconds.ToString());
            output.AppendLine();

            output.AppendLine("[checkboxes]");
            Append(output, "launch_with_cs2fixes", state.LaunchWithCs2Fixes.ToString());
            Append(output, "launch_workshop_tools", state.LaunchWorkshopTools.ToString());
            Append(output, "autojoiner_sound", state.AutojoinerSound.ToString());
            output.AppendLine();

            output.AppendLine("[window]");
            Append(output, "selected_tab", state.SelectedTab.ToString());
            Append(output, "x", state.WindowX.ToString());
            Append(output, "y", state.WindowY.ToString());
            Append(output, "width", state.WindowWidth.ToString());
            Append(output, "height", state.WindowHeight.ToString());
            Append(output, "state", state.WindowState.ToString());

            File.WriteAllText(ConfigPath, output.ToString(), new UTF8Encoding(false));
        }

        private static Dictionary<string, Dictionary<string, string>> ParseIni(string path)
        {
            var result = new Dictionary<string, Dictionary<string, string>>(
                StringComparer.OrdinalIgnoreCase);
            string section = string.Empty;

            foreach (string rawLine in File.ReadAllLines(path, Encoding.UTF8))
            {
                string line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith(";", StringComparison.Ordinal) ||
                    line.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }

                if (line.StartsWith("[", StringComparison.Ordinal) &&
                    line.EndsWith("]", StringComparison.Ordinal) && line.Length > 2)
                {
                    section = line.Substring(1, line.Length - 2).Trim();
                    if (!result.ContainsKey(section))
                    {
                        result[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    }
                    continue;
                }

                int equals = line.IndexOf('=');
                if (equals <= 0 || section.Length == 0)
                {
                    continue;
                }

                string key = line.Substring(0, equals).Trim();
                string value = line.Substring(equals + 1).Trim();
                result[section][key] = value;
            }

            return result;
        }

        private static string Get(
            IDictionary<string, Dictionary<string, string>> values,
            string section,
            string key)
        {
            Dictionary<string, string> sectionValues;
            string value;
            return values.TryGetValue(section, out sectionValues) &&
                   sectionValues.TryGetValue(key, out value)
                ? value
                : string.Empty;
        }

        private static bool GetBool(
            IDictionary<string, Dictionary<string, string>> values,
            string section,
            string key,
            bool fallback)
        {
            bool value;
            return bool.TryParse(Get(values, section, key), out value) ? value : fallback;
        }

        private static int GetInt(
            IDictionary<string, Dictionary<string, string>> values,
            string section,
            string key,
            int fallback)
        {
            int value;
            return TryGetInt(values, section, key, out value) ? value : fallback;
        }

        private static bool TryGetInt(
            IDictionary<string, Dictionary<string, string>> values,
            string section,
            string key,
            out int value)
        {
            return int.TryParse(Get(values, section, key), out value);
        }

        private static void Append(StringBuilder output, string key, string value)
        {
            string singleLineValue = (value ?? string.Empty)
                .Replace("\r", " ")
                .Replace("\n", " ");
            output.Append(key).Append('=').AppendLine(singleLineValue);
        }
    }
}
