using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace cs2_things
{
    internal sealed class Cs2InstallationRequest
    {
        public string Cs2Root { get; set; }
        public string SteamId64 { get; set; }
        public string Cs2FixesConfig { get; set; }
        public string MetamodZip { get; set; }
        public string MetamodLauncherZip { get; set; }
        public string Cs2FixesZip { get; set; }
        public string StripperCs2Zip { get; set; }
        public string GflZeConfigsZip { get; set; }
    }

    internal sealed class Cs2InstallationException : Exception
    {
        public Cs2InstallationException(string title, string message)
            : base(message)
        {
            Title = title;
        }

        public string Title { get; private set; }
    }

    internal static class InstallCS2Fixes
    {
        private const string AdminsTemplate = @"{
  ""Groups"":
  {
    """": // Set any Group Name you like
    {
      ""flags"": ""z"", // Root, grants all flags
      ""immunity"": 2 // Ignoring any immunity
    }
  },

  ""Admins"":
  {
    ""<id here>"": // Set your SteamID64 here
    {
      ""name"": """", // Set to anything you want
      ""groups"":
      [
        """" // Use the Group Name you set in the Groups object above.
      ],
      ""flags"": ""z"", // Root, grants all flags
      ""immunity"": 2 // Ignoring any immunity
    }
  }
}
";

        private static readonly IDictionary<string, string> RequiredZipFiles =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "metamod", "addons/metamod/bin/win64/metamod.2.cs2.dll" },
                { "cs2fixes", "addons/cs2fixes/bin/win64/cs2fixes.dll" },
                { "strippercs2", "addons/StripperCS2/bin/StripperCS2.dll" }
            };

        public static IList<string> Install(Cs2InstallationRequest request)
        {
            ValidateRequest(request);

            var results = new List<string>();
            var installs = CreateInstallDefinitions(request);

            foreach (InstallDefinition install in installs)
            {
                string targetDirectory = Path.Combine(request.Cs2Root, install.TargetPath);
                Directory.CreateDirectory(targetDirectory);

                ValidateZipContents(install.ZipPath, install.DisplayName);
                int extractedCount = ExtractZipOverwrite(
                    install.ZipPath,
                    targetDirectory,
                    install.SourcePrefix,
                    install.ExcludePrefixes);

                if (install.SourcePrefix != null && extractedCount == 0)
                {
                    throw new Cs2InstallationException(
                        "Install failed",
                        "No files were found under " + install.SourcePrefix +
                        " in the " + install.DisplayName + " zip.");
                }

                results.Add(
                    install.DisplayName + ": " + extractedCount +
                    " files to " + install.TargetPath);
            }

            string adminsPath = Path.Combine(
                request.Cs2Root,
                "game", "csgo", "addons", "cs2fixes", "configs", "admins.jsonc");
            Directory.CreateDirectory(Path.GetDirectoryName(adminsPath));
            File.WriteAllText(
                adminsPath,
                AdminsTemplate.Replace("<id here>", request.SteamId64),
                new UTF8Encoding(false));
            results.Add("admins.jsonc: steamID64 updated");

            string configPath = Path.Combine(
                request.Cs2Root,
                "game", "csgo", "cfg", "cs2fixes", "cs2fixes.cfg");
            Directory.CreateDirectory(Path.GetDirectoryName(configPath));
            File.Copy(request.Cs2FixesConfig, configPath, true);
            results.Add("cs2fixes.cfg: updated");

            return results;
        }

        private static void ValidateRequest(Cs2InstallationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (!Directory.Exists(request.Cs2Root))
            {
                throw new Cs2InstallationException(
                    "Invalid CS2 root",
                    "Select a valid CS2 root directory first.");
            }

            if (string.IsNullOrWhiteSpace(request.SteamId64))
            {
                throw new Cs2InstallationException(
                    "Missing steamID64",
                    "Enter a steamID64 value first.");
            }

            if (!File.Exists(request.Cs2FixesConfig))
            {
                throw new Cs2InstallationException(
                    "Invalid cs2fixes.cfg",
                    "Select a valid cs2fixes.cfg file first.");
            }
        }

        private static IList<InstallDefinition> CreateInstallDefinitions(Cs2InstallationRequest request)
        {
            return new List<InstallDefinition>
            {
                new InstallDefinition(request.MetamodZip, "metamod", @"game\csgo"),
                new InstallDefinition(request.MetamodLauncherZip, "metamod launcher", @"game\bin\win64"),
                new InstallDefinition(request.Cs2FixesZip, "cs2fixes", @"game\csgo"),
                new InstallDefinition(request.StripperCs2Zip, "strippercs2", @"game\csgo"),
                new InstallDefinition(
                    request.GflZeConfigsZip,
                    "gfl cs2 ze configs",
                    @"game\csgo\addons\StripperCS2\maps",
                    "CS2-ZE-Configs-main/stripper/"),
                new InstallDefinition(
                    request.GflZeConfigsZip,
                    "gfl cs2 ze configs entwatch",
                    @"game\csgo\addons\cs2fixes\configs\entwatch\maps",
                    "CS2-ZE-Configs-main/entwatch/"),
                new InstallDefinition(
                    request.GflZeConfigsZip,
                    "gfl cs2 ze configs cs2fixes",
                    @"game\csgo\addons\cs2fixes\configs",
                    "CS2-ZE-Configs-main/cs2fixes/",
                    new[] { "CS2-ZE-Configs-main/entwatch/" }),
                new InstallDefinition(
                    request.GflZeConfigsZip,
                    "gfl cs2 ze configs bosshud",
                    @"game\csgo\addons\cs2fixes\configs\bosshud",
                    "CS2-ZE-Configs-main/bosshud/",
                    new[] { "CS2-ZE-Configs-main/entwatch/" }),
                new InstallDefinition(
                    request.GflZeConfigsZip,
                    "gfl cs2 ze configs mapcfg",
                    @"game\csgo\cfg\cs2fixes\maps",
                    "CS2-ZE-Configs-main/mapcfg/")
            };
        }

        private static void ValidateZipContents(string zipPath, string displayName)
        {
            if (!File.Exists(zipPath) ||
                !string.Equals(Path.GetExtension(zipPath), ".zip", StringComparison.OrdinalIgnoreCase))
            {
                throw new Cs2InstallationException(
                    "Invalid " + displayName + " zip",
                    "Select a valid " + displayName + " .zip file first.");
            }

            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    if (string.Equals(displayName, "metamod launcher", StringComparison.OrdinalIgnoreCase))
                    {
                        bool containsLauncher = archive.Entries.Any(entry =>
                            string.Equals(
                                GetZipFileName(NormalizeZipPath(entry.FullName)),
                                "metamod-launcher.exe",
                                StringComparison.OrdinalIgnoreCase));

                        if (!containsLauncher)
                        {
                            throw new Cs2InstallationException(
                                "Install blocked",
                                "metamod launcher zip must contain metamod-launcher.exe.");
                        }

                        return;
                    }

                    string requiredFile;
                    if (!RequiredZipFiles.TryGetValue(displayName, out requiredFile))
                    {
                        return;
                    }

                    bool containsRequiredFile = archive.Entries.Any(entry =>
                        string.Equals(
                            NormalizeZipPath(entry.FullName),
                            NormalizeZipPath(requiredFile),
                            StringComparison.OrdinalIgnoreCase));

                    if (!containsRequiredFile)
                    {
                        throw new Cs2InstallationException(
                            "Install blocked",
                            displayName + " zip must contain " + requiredFile + ".");
                    }
                }
            }
            catch (InvalidDataException)
            {
                throw new Cs2InstallationException(
                    "Invalid " + displayName + " zip",
                    "The selected " + displayName + " file is not a valid zip.");
            }
        }

        private static int ExtractZipOverwrite(
            string zipPath,
            string targetDirectory,
            string sourcePrefix,
            IList<string> excludePrefixes)
        {
            string targetRoot = Path.GetFullPath(targetDirectory);
            string targetRootWithSeparator = targetRoot.TrimEnd(
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string normalizedPrefix = sourcePrefix == null ? null : NormalizeZipPath(sourcePrefix);
            string[] normalizedExcludes = (excludePrefixes ?? new string[0])
                .Select(NormalizeZipPath)
                .ToArray();
            int extractedCount = 0;

            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string memberName = NormalizeZipPath(entry.FullName);

                        if (normalizedExcludes.Any(prefix =>
                            memberName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }

                        string relativeName = memberName;
                        if (normalizedPrefix != null)
                        {
                            if (!memberName.StartsWith(normalizedPrefix, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            relativeName = memberName.Substring(normalizedPrefix.Length);
                            if (relativeName.Length == 0)
                            {
                                continue;
                            }
                        }

                        string platformRelativeName = relativeName.Replace(
                            '/', Path.DirectorySeparatorChar);
                        string destination = Path.GetFullPath(
                            Path.Combine(targetRoot, platformRelativeName));

                        if (!destination.StartsWith(
                                targetRootWithSeparator,
                                StringComparison.OrdinalIgnoreCase))
                        {
                            throw new Cs2InstallationException(
                                "Install blocked",
                                "Zip entry would extract outside the target directory: " + entry.FullName);
                        }

                        bool isDirectory = entry.FullName.EndsWith("/", StringComparison.Ordinal) ||
                                           entry.FullName.EndsWith("\\", StringComparison.Ordinal) ||
                                           string.IsNullOrEmpty(entry.Name);
                        if (isDirectory)
                        {
                            Directory.CreateDirectory(destination);
                            continue;
                        }

                        string destinationDirectory = Path.GetDirectoryName(destination);
                        if (!string.IsNullOrEmpty(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }

                        using (Stream source = entry.Open())
                        using (var output = new FileStream(
                            destination,
                            FileMode.Create,
                            FileAccess.Write,
                            FileShare.None))
                        {
                            source.CopyTo(output);
                        }

                        extractedCount++;
                    }
                }
            }
            catch (InvalidDataException)
            {
                throw new Cs2InstallationException(
                    "Invalid zip",
                    "The selected file is not a valid zip: " + zipPath);
            }

            return extractedCount;
        }

        private static string NormalizeZipPath(string path)
        {
            return (path ?? string.Empty).Replace('\\', '/').TrimStart('/');
        }

        private static string GetZipFileName(string path)
        {
            int separator = path.LastIndexOf('/');
            return separator < 0 ? path : path.Substring(separator + 1);
        }

        private sealed class InstallDefinition
        {
            public InstallDefinition(
                string zipPath,
                string displayName,
                string targetPath,
                string sourcePrefix = null,
                IList<string> excludePrefixes = null)
            {
                ZipPath = zipPath;
                DisplayName = displayName;
                TargetPath = targetPath;
                SourcePrefix = sourcePrefix;
                ExcludePrefixes = excludePrefixes;
            }

            public string ZipPath { get; private set; }
            public string DisplayName { get; private set; }
            public string TargetPath { get; private set; }
            public string SourcePrefix { get; private set; }
            public IList<string> ExcludePrefixes { get; private set; }
        }
    }
}
