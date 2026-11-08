using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace cs2_things
{
    internal static class LaunchCS2
    {
        private const string WorkshopBackupName = "gameinfo.gi.cs2launcher-workshop-lines.json";

        private static readonly string[] WorkshopFilterLines =
        {
            "\"substr\" \"agents/models/\"",
            "\"substr\" \"weapons/models/\""
        };

        public static void Launch(
            string cs2Root,
            bool launchWithCs2Fixes,
            bool launchWorkshopTools,
            Action<string> progress)
        {
            Report(progress, "Preparing launch.");

            if (!Directory.Exists(cs2Root))
            {
                throw new DirectoryNotFoundException("Select a valid CS2 root directory first.");
            }

            string binDirectory = Path.Combine(cs2Root, "game", "bin", "win64");
            string serverDll = Path.Combine(binDirectory, "server.dll");
            string[] workshopArguments =
            {
                "-disable_workshop_command_filtering",
                "-insecure"
            };

            UpdateGameInfoWorkshopFilters(cs2Root, launchWorkshopTools, progress);
            if (launchWorkshopTools)
            {
                UpdateWorkshopParticleToolFilters(cs2Root, progress);
            }

            if (!launchWithCs2Fixes)
            {
                Report(progress, "Deleting if present: " + serverDll);
                DeleteFileIfExists(serverDll);
            }

            if (launchWithCs2Fixes && !launchWorkshopTools)
            {
                string launcher = Path.Combine(binDirectory, "metamod-launcher.exe");
                Report(progress, "Launching: " + launcher);
                LaunchExecutable(launcher, null);
                Report(progress, "metamod-launcher.exe launched.");
                return;
            }

            if (launchWithCs2Fixes && launchWorkshopTools)
            {
                string launcher = Path.Combine(binDirectory, "csgocfg.exe");
                Report(progress, "Copying Metamod binaries to tools directory.");
                foreach (string copiedFile in CopyMetamodToTools(cs2Root))
                {
                    Report(progress, "Copied: " + copiedFile);
                }

                Report(progress, "Launching: " + launcher + " " + string.Join(" ", workshopArguments));
                LaunchExecutable(launcher, workshopArguments);
                Report(progress, "csgocfg.exe launched.");
                return;
            }

            if (!launchWithCs2Fixes && !launchWorkshopTools)
            {
                const string steamUri = "steam://rungameid/730";
                Report(progress, "Opening: " + steamUri);
                LaunchUri(steamUri);
                Report(progress, "Steam CS2 launch URI opened.");
                return;
            }

            string toolsLauncher = Path.Combine(binDirectory, "csgocfg.exe");
            Report(progress, "Launching: " + toolsLauncher + " " + string.Join(" ", workshopArguments));
            LaunchExecutable(toolsLauncher, workshopArguments);
            Report(progress, "csgocfg.exe launched.");
        }

        private static void UpdateGameInfoWorkshopFilters(
            string cs2Root,
            bool launchWorkshopTools,
            Action<string> progress)
        {
            string gameInfoPath = Path.Combine(cs2Root, "game", "csgo", "gameinfo.gi");
            string backupPath = Path.Combine(Path.GetDirectoryName(gameInfoPath), WorkshopBackupName);

            Report(progress, "Checking gameinfo.gi: " + gameInfoPath);
            if (!File.Exists(gameInfoPath))
            {
                throw new FileNotFoundException("Could not find required file.", gameInfoPath);
            }

            string content = File.ReadAllText(gameInfoPath, Encoding.UTF8);
            List<string> lines = SplitLinesKeepEndings(content);

            if (launchWorkshopTools)
            {
                Report(progress, "Removing workshop model filter lines from gameinfo.gi.");
                RemovedLines result = RemoveGameInfoWorkshopLines(lines);
                if (result.Lines.Count == 0)
                {
                    Report(progress, "Workshop model filters already removed from gameinfo.gi.");
                    return;
                }

                SaveGameInfoBackup(backupPath, result.Index, result.Lines);
                File.WriteAllText(gameInfoPath, string.Concat(result.UpdatedLines), new UTF8Encoding(false));
                Report(progress, "Removed workshop model filters from: " + gameInfoPath);
                return;
            }

            if (HasAllGameInfoWorkshopLines(lines))
            {
                Report(progress, "Workshop model filters already restored in gameinfo.gi.");
                return;
            }

            Report(progress, "Restoring workshop model filter lines to gameinfo.gi.");
            GameInfoBackup backup = LoadGameInfoBackup(backupPath);
            int restoreIndex;
            IList<string> restoreLines;

            if (backup != null)
            {
                restoreIndex = Math.Min(backup.Index, lines.Count);
                restoreLines = backup.Lines;
            }
            else
            {
                restoreIndex = FindGameInfoSubstrInsertIndex(lines);
                string newline = DetectNewline(content);
                string indent = DetectGameInfoSubstrIndent(lines);
                restoreLines = WorkshopFilterLines
                    .Select(line => indent + line + newline)
                    .ToList();
            }

            RemovedLines cleaned = RemoveGameInfoWorkshopLines(lines);
            restoreIndex = Math.Max(0, Math.Min(restoreIndex, cleaned.UpdatedLines.Count));
            cleaned.UpdatedLines.InsertRange(restoreIndex, restoreLines);
            File.WriteAllText(gameInfoPath, string.Concat(cleaned.UpdatedLines), new UTF8Encoding(false));
            Report(progress, "Restored workshop model filters in: " + gameInfoPath);
        }

        private static RemovedLines RemoveGameInfoWorkshopLines(IList<string> lines)
        {
            var updated = new List<string>();
            var removed = new List<string>();
            int index = -1;

            foreach (string line in lines)
            {
                bool isFilterLine = WorkshopFilterLines.Any(filter =>
                    string.Equals(line.Trim(), filter, StringComparison.OrdinalIgnoreCase));
                if (isFilterLine)
                {
                    if (index < 0)
                    {
                        index = updated.Count;
                    }
                    removed.Add(line);
                }
                else
                {
                    updated.Add(line);
                }
            }

            return new RemovedLines(updated, removed, index);
        }

        private static bool HasAllGameInfoWorkshopLines(IEnumerable<string> lines)
        {
            var existing = new HashSet<string>(
                lines.Select(line => line.Trim()),
                StringComparer.OrdinalIgnoreCase);
            return WorkshopFilterLines.All(existing.Contains);
        }

        private static void SaveGameInfoBackup(string path, int index, IList<string> lines)
        {
            var backup = new Dictionary<string, object>
            {
                { "index", index },
                { "lines", lines }
            };
            string json = new JavaScriptSerializer().Serialize(backup);
            File.WriteAllText(path, json, new UTF8Encoding(false));
        }

        private static GameInfoBackup LoadGameInfoBackup(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                object value = new JavaScriptSerializer().DeserializeObject(File.ReadAllText(path, Encoding.UTF8));
                var data = value as IDictionary<string, object>;
                if (data == null || !data.ContainsKey("index") || !data.ContainsKey("lines"))
                {
                    return null;
                }

                int index = Convert.ToInt32(data["index"]);
                var rawLines = data["lines"] as object[];
                if (rawLines == null)
                {
                    return null;
                }

                var lines = new List<string>();
                foreach (object line in rawLines)
                {
                    string text = line as string;
                    if (text == null)
                    {
                        return null;
                    }
                    lines.Add(text);
                }

                return new GameInfoBackup(index, lines);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string DetectNewline(string content)
        {
            if (content.Contains("\r\n")) return "\r\n";
            if (content.Contains("\r")) return "\r";
            return "\n";
        }

        private static int FindGameInfoSubstrInsertIndex(IList<string> lines)
        {
            for (int index = 0; index < lines.Count; index++)
            {
                if (lines[index].Trim().StartsWith("\"substr\" ", StringComparison.OrdinalIgnoreCase))
                {
                    return index;
                }
            }
            return lines.Count;
        }

        private static string DetectGameInfoSubstrIndent(IEnumerable<string> lines)
        {
            foreach (string line in lines)
            {
                string trimmed = line.TrimStart(' ', '\t');
                if (trimmed.StartsWith("\"substr\" ", StringComparison.OrdinalIgnoreCase))
                {
                    return line.Substring(0, line.Length - trimmed.Length);
                }
            }
            return string.Empty;
        }

        private static void UpdateWorkshopParticleToolFilters(string cs2Root, Action<string> progress)
        {
            string gameBin = Path.Combine(cs2Root, "game", "bin");
            RemoveCsgoFromToolFilter(
                Path.Combine(gameBin, "sdkenginetools.txt"),
                "m_Name = \"pet\"",
                "m_ExcludeFromMods",
                "Particle Editor",
                progress);

            string primaryAssetTypes = Path.Combine(gameBin, "assettypes_common.txt");
            string legacyAssetTypes = Path.Combine(gameBin, "assettpyes_common.txt");
            string assetTypes = File.Exists(primaryAssetTypes) ? primaryAssetTypes : legacyAssetTypes;
            RemoveCsgoFromToolFilter(
                assetTypes,
                "particle_asset",
                "m_HideForRetailMods",
                "Particle System",
                progress);
        }

        private static void RemoveCsgoFromToolFilter(
            string path,
            string sectionMarker,
            string listMarker,
            string displayName,
            Action<string> progress)
        {
            Report(progress, "Checking " + displayName + " workshop filter: " + path);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Could not find required file.", path);
            }

            List<string> lines = SplitLinesKeepEndings(File.ReadAllText(path, Encoding.UTF8));
            var updated = new List<string>();
            bool inSection = false;
            bool inList = false;
            bool sawSectionOpen = false;
            int braceDepth = 0;
            int removedCount = 0;

            foreach (string line in lines)
            {
                string trimmed = line.Trim();
                if (!inSection && ContainsIgnoreCase(line, sectionMarker))
                {
                    inSection = true;
                    if (sectionMarker.StartsWith("m_Name", StringComparison.OrdinalIgnoreCase))
                    {
                        braceDepth = 1;
                        sawSectionOpen = true;
                    }
                    else
                    {
                        braceDepth = Math.Max(0, CountCharacter(line, '{') - CountCharacter(line, '}'));
                        sawSectionOpen = line.Contains("{");
                    }
                }

                if (inSection && ContainsIgnoreCase(line, listMarker))
                {
                    inList = true;
                }

                if (inSection && inList &&
                    (string.Equals(trimmed, "\"csgo\",", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(trimmed, "\"csgo\"", StringComparison.OrdinalIgnoreCase)))
                {
                    removedCount++;
                }
                else
                {
                    updated.Add(line);
                }

                if (inSection)
                {
                    if (line.Contains("{")) sawSectionOpen = true;
                    braceDepth += CountCharacter(line, '{') - CountCharacter(line, '}');
                    if (inList && trimmed.StartsWith("]", StringComparison.Ordinal)) inList = false;
                    if (sawSectionOpen && braceDepth <= 0)
                    {
                        inSection = false;
                        inList = false;
                        sawSectionOpen = false;
                    }
                }
            }

            if (removedCount == 0)
            {
                Report(progress, displayName + " workshop filter already allows csgo.");
                return;
            }

            File.WriteAllText(path, string.Concat(updated), new UTF8Encoding(false));
            Report(progress, "Removed csgo from " + displayName + " workshop filter: " + path);
        }

        private static IList<string> CopyMetamodToTools(string cs2Root)
        {
            string sourceDirectory = Path.Combine(
                cs2Root, "game", "csgo", "addons", "metamod", "bin", "win64");
            string targetDirectory = Path.Combine(cs2Root, "game", "bin", "win64");
            Directory.CreateDirectory(targetDirectory);

            var copied = new List<string>();
            foreach (string fileName in new[] { "server.dll", "metamod.2.cs2.dll" })
            {
                string source = Path.Combine(sourceDirectory, fileName);
                if (!File.Exists(source))
                {
                    throw new FileNotFoundException("Could not find required file.", source);
                }

                string target = Path.Combine(targetDirectory, fileName);
                File.Copy(source, target, true);
                copied.Add(target);
            }
            return copied;
        }

        private static void LaunchExecutable(string path, IEnumerable<string> arguments)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Could not find required file.", path);
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                Arguments = arguments == null ? string.Empty : string.Join(" ", arguments),
                WorkingDirectory = Path.GetDirectoryName(path),
                UseShellExecute = true
            });
        }

        private static void LaunchUri(string uri)
        {
            Process.Start(new ProcessStartInfo { FileName = uri, UseShellExecute = true });
        }

        private static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

        private static List<string> SplitLinesKeepEndings(string content)
        {
            var lines = new List<string>();
            int start = 0;
            for (int index = 0; index < content.Length; index++)
            {
                if (content[index] != '\r' && content[index] != '\n') continue;
                int length = index - start + 1;
                if (content[index] == '\r' && index + 1 < content.Length && content[index + 1] == '\n')
                {
                    index++;
                    length++;
                }
                lines.Add(content.Substring(start, length));
                start = index + 1;
            }
            if (start < content.Length) lines.Add(content.Substring(start));
            return lines;
        }

        private static bool ContainsIgnoreCase(string value, string search)
        {
            return value.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static int CountCharacter(string value, char character)
        {
            return value.Count(item => item == character);
        }

        private static void Report(Action<string> progress, string message)
        {
            if (progress != null) progress(message);
        }

        private sealed class RemovedLines
        {
            public RemovedLines(List<string> updatedLines, List<string> lines, int index)
            {
                UpdatedLines = updatedLines;
                Lines = lines;
                Index = index;
            }
            public List<string> UpdatedLines { get; private set; }
            public List<string> Lines { get; private set; }
            public int Index { get; private set; }
        }

        private sealed class GameInfoBackup
        {
            public GameInfoBackup(int index, IList<string> lines)
            {
                Index = index;
                Lines = lines;
            }
            public int Index { get; private set; }
            public IList<string> Lines { get; private set; }
        }
    }
}
