using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace cs2_things
{
    internal sealed class InstallRollbackManifest
    {
        public string Cs2Root { get; set; }
        public List<InstallRollbackFile> Files { get; set; }
        public List<string> CreatedDirectories { get; set; }
    }

    internal sealed class InstallRollbackFile
    {
        public string RelativePath { get; set; }
        public string BackupName { get; set; }
    }

    internal sealed class InstallRollback
    {
        private static readonly string ManifestPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "install-rollback.json");

        private static readonly string BackupDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "install-rollback-data");

        private readonly string root;
        private readonly string rootWithSeparator;
        private readonly InstallRollbackManifest manifest;

        private InstallRollback(string cs2Root, InstallRollbackManifest loadedManifest)
        {
            root = Path.GetFullPath(cs2Root).TrimEnd(
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar);
            rootWithSeparator = root + Path.DirectorySeparatorChar;
            manifest = loadedManifest ?? new InstallRollbackManifest
            {
                Cs2Root = root,
                Files = new List<InstallRollbackFile>(),
                CreatedDirectories = new List<string>()
            };
        }

        public static InstallRollback Begin(string cs2Root)
        {
            string fullRoot = Path.GetFullPath(cs2Root).TrimEnd(
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar);
            InstallRollbackManifest existing = LoadManifest();
            if (existing != null && !string.Equals(
                    Path.GetFullPath(existing.Cs2Root).TrimEnd(
                        Path.DirectorySeparatorChar,
                        Path.AltDirectorySeparatorChar),
                    fullRoot,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "An uninstall record already exists for a different CS2 installation. " +
                    "Uninstall that installation before installing to another root.");
            }

            var rollback = new InstallRollback(fullRoot, existing);
            rollback.Save();
            return rollback;
        }

        public void EnsureDirectory(string directoryPath)
        {
            string directory = ValidateTarget(directoryPath);
            if (Directory.Exists(directory))
            {
                return;
            }

            var missing = new List<string>();
            string current = directory;
            while (!Directory.Exists(current) && !string.Equals(
                current, root, StringComparison.OrdinalIgnoreCase))
            {
                missing.Add(current);
                current = Path.GetDirectoryName(current);
                if (string.IsNullOrEmpty(current))
                {
                    break;
                }
            }

            foreach (string path in missing)
            {
                string relative = MakeRelative(path);
                if (!manifest.CreatedDirectories.Contains(relative, StringComparer.OrdinalIgnoreCase))
                {
                    manifest.CreatedDirectories.Add(relative);
                }
            }

            Save();
            Directory.CreateDirectory(directory);
        }

        public void PrepareFile(string filePath)
        {
            string target = ValidateTarget(filePath);
            string relative = MakeRelative(target);
            if (manifest.Files.Any(file => string.Equals(
                file.RelativePath, relative, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            string parent = Path.GetDirectoryName(target);
            if (!string.IsNullOrEmpty(parent))
            {
                EnsureDirectory(parent);
            }

            manifest.Files.Add(new InstallRollbackFile
            {
                RelativePath = relative,
                BackupName = null
            });
            Save();
        }

        public static int Uninstall(string requestedRoot, Action<string> progress)
        {
            InstallRollbackManifest saved = LoadManifest();
            if (saved == null)
            {
                throw new InvalidOperationException(
                    "CS2Fixes not found to be installed.");
            }

            string recordedRoot = Path.GetFullPath(saved.Cs2Root).TrimEnd(
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar);
            if (!string.IsNullOrWhiteSpace(requestedRoot))
            {
                string selectedRoot = Path.GetFullPath(requestedRoot).TrimEnd(
                    Path.DirectorySeparatorChar,
                    Path.AltDirectorySeparatorChar);
                if (!string.Equals(recordedRoot, selectedRoot, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "The selected CS2 root does not match the installation recorded for uninstall.");
                }
            }

            var rollback = new InstallRollback(recordedRoot, saved);
            Report(progress, "Starting uninstall cleanup.");
            int deletedFiles = 0;

            foreach (InstallRollbackFile file in saved.Files.AsEnumerable().Reverse())
            {
                string target = rollback.ValidateTarget(Path.Combine(recordedRoot, file.RelativePath));
                if (File.Exists(target))
                {
                    File.Delete(target);
                    deletedFiles++;
                    Report(progress, "Removed: " + target);
                }
            }

            foreach (string relative in saved.CreatedDirectories
                .OrderByDescending(path => path.Length))
            {
                string directory = rollback.ValidateTarget(Path.Combine(recordedRoot, relative));
                if (Directory.Exists(directory) && !Directory.EnumerateFileSystemEntries(directory).Any())
                {
                    Directory.Delete(directory, false);
                    Report(progress, "Removed empty directory: " + directory);
                }
            }

            if (Directory.Exists(BackupDirectory))
            {
                Directory.Delete(BackupDirectory, true);
            }
            File.Delete(ManifestPath);
            return deletedFiles;
        }

        private string ValidateTarget(string path)
        {
            string fullPath = Path.GetFullPath(path);
            if (!fullPath.StartsWith(rootWithSeparator, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Refusing to modify a path outside the recorded CS2 root: " + fullPath);
            }
            return fullPath;
        }

        private string MakeRelative(string path)
        {
            return ValidateTarget(path).Substring(rootWithSeparator.Length);
        }

        private void Save()
        {
            string json = new JavaScriptSerializer().Serialize(manifest);
            File.WriteAllText(ManifestPath, json, new UTF8Encoding(false));
        }

        private static InstallRollbackManifest LoadManifest()
        {
            if (!File.Exists(ManifestPath))
            {
                return null;
            }

            InstallRollbackManifest loaded = new JavaScriptSerializer()
                .Deserialize<InstallRollbackManifest>(File.ReadAllText(ManifestPath, Encoding.UTF8));
            if (loaded == null || string.IsNullOrWhiteSpace(loaded.Cs2Root))
            {
                throw new InvalidDataException("The uninstall record is invalid.");
            }

            loaded.Files = loaded.Files ?? new List<InstallRollbackFile>();
            loaded.CreatedDirectories = loaded.CreatedDirectories ?? new List<string>();
            return loaded;
        }

        private static void Report(Action<string> progress, string message)
        {
            if (progress != null)
            {
                progress(message);
            }
        }
    }
}
