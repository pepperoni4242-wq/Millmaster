using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Millmaster
{
    // Everything that goes into one export file
    public class DataPackage
    {
        public string App { get; set; } = "";
        public int FormatVersion { get; set; }
        public string ExportedAt { get; set; } = "";

        // Which parts the file really contains
        public bool HasMaterials { get; set; }
        public bool HasTools { get; set; }
        public bool HasSettings { get; set; }
        public bool HasLog { get; set; }

        public List<Material> Materials { get; set; } = new List<Material>();
        public List<LibraryTool> Tools { get; set; } = new List<LibraryTool>();
        public Dictionary<string, double> Settings { get; set; } = new Dictionary<string, double>();
        public List<LogEntry> Log { get; set; } = new List<LogEntry>();
    }

    public static class DataTransfer
    {
        public const string FileFilter = "Millmaster data (*.millmaster)|*.millmaster|All files (*.*)|*.*";

        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions { WriteIndented = true };

        public static string BackupFolder
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Millmaster", "backups");
            }
        }

        // Gathers the chosen data from the saved files
        public static DataPackage Collect(bool materials, bool tools, bool settings, bool log)
        {
            DataPackage pkg = new DataPackage();
            pkg.App = "Millmaster";
            pkg.FormatVersion = 1;
            pkg.ExportedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            if (materials)
            {
                pkg.HasMaterials = true;
                pkg.Materials = MaterialStore.Load();
            }
            if (tools)
            {
                pkg.HasTools = true;
                pkg.Tools = ToolLibraryStore.Load();
            }
            if (settings)
            {
                pkg.HasSettings = true;
                foreach (SettingInfo s in CalcSettings.All)
                {
                    pkg.Settings[s.Key] = CalcSettings.Get(s.Key);
                }
            }
            if (log)
            {
                pkg.HasLog = true;
                pkg.Log = LogStore.Load();
            }
            return pkg;
        }

        public static void Write(string path, DataPackage pkg)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(pkg, Options), new UTF8Encoding(false));
        }

        // Reads and checks a file. Throws an Exception with a readable message if it is not usable.
        public static DataPackage Read(string path)
        {
            DataPackage pkg;
            try
            {
                pkg = JsonSerializer.Deserialize<DataPackage>(File.ReadAllText(path));
            }
            catch (JsonException)
            {
                throw new Exception("This file is not a valid Millmaster data file.");
            }

            if (pkg == null || pkg.App != "Millmaster")
            {
                throw new Exception("This is not a Millmaster data file.");
            }
            if (pkg.FormatVersion > 1)
            {
                throw new Exception("This file was made by a newer version of Millmaster. Please update Millmaster first.");
            }

            if (pkg.Materials == null) pkg.Materials = new List<Material>();
            if (pkg.Tools == null) pkg.Tools = new List<LibraryTool>();
            if (pkg.Settings == null) pkg.Settings = new Dictionary<string, double>();
            if (pkg.Log == null) pkg.Log = new List<LogEntry>();
            return pkg;
        }

        private static bool SameText(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        private static bool HasMaterialNamed(List<Material> list, string name)
        {
            foreach (Material m in list)
            {
                if (SameText(m.Name, name)) return true;
            }
            return false;
        }

        private static bool HasToolNamed(List<LibraryTool> list, string label)
        {
            foreach (LibraryTool t in list)
            {
                if (SameText(t.Label, label)) return true;
            }
            return false;
        }

        private static string LogKey(LogEntry e)
        {
            return e.Date + "|" + e.Name + "|" + e.ToolName;
        }

        // Puts the chosen parts of the file into your data. Returns a summary text.
        public static string Apply(DataPackage pkg, bool materials, bool tools,
                                   bool settings, bool log, bool replace)
        {
            // 0. Safety backup of everything you have now
            Directory.CreateDirectory(BackupFolder);
            string backupFile = Path.Combine(BackupFolder,
                "auto-backup-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".millmaster");
            Write(backupFile, Collect(true, true, true, true));

            StringBuilder sb = new StringBuilder();

            // 1. Materials
            if (materials)
            {
                List<Material> list;
                if (replace) list = new List<Material>();
                else list = MaterialStore.Load();

                int added = 0;
                int skipped = 0;
                foreach (Material m in pkg.Materials)
                {
                    if (!replace && HasMaterialNamed(list, m.Name))
                    {
                        skipped++;
                        continue;
                    }
                    list.Add(m);
                    added++;
                }
                MaterialStore.Save(list);

                if (replace) sb.AppendLine("Materials: replaced, now " + list.Count);
                else sb.AppendLine("Materials: " + added + " added, " + skipped + " skipped (name already exists)");
            }

            // 2. Tool library
            if (tools)
            {
                List<LibraryTool> list;
                if (replace) list = new List<LibraryTool>();
                else list = ToolLibraryStore.Load();

                int added = 0;
                int skipped = 0;
                foreach (LibraryTool t in pkg.Tools)
                {
                    if (!replace && HasToolNamed(list, t.Label))
                    {
                        skipped++;
                        continue;
                    }
                    list.Add(t);
                    added++;
                }
                ToolLibraryStore.Save(list);

                if (replace) sb.AppendLine("Tool library: replaced, now " + list.Count);
                else sb.AppendLine("Tool library: " + added + " added, " + skipped + " skipped (name already exists)");
            }

            // 3. Calculation settings (the file's values are used for every setting it contains)
            if (settings)
            {
                int applied = 0;
                int ignored = 0;
                foreach (SettingInfo s in CalcSettings.All)
                {
                    double v;
                    if (!pkg.Settings.TryGetValue(s.Key, out v)) continue;

                    bool valid = v > 0;
                    if (s.Key.EndsWith(".Ae") && v > 1.0) valid = false;
                    if (s.Key == "Mill.MaxChipThinning" && v < 1.0) valid = false;

                    if (valid)
                    {
                        CalcSettings.Set(s.Key, v);
                        applied++;
                    }
                    else
                    {
                        ignored++;
                    }
                }
                CalcSettings.Save();
                sb.AppendLine("Settings: " + applied + " values applied, " + ignored + " ignored (not valid)");
            }

            // 4. Saved log
            if (log)
            {
                List<LogEntry> list;
                if (replace) list = new List<LogEntry>();
                else list = LogStore.Load();

                HashSet<string> known = new HashSet<string>();
                foreach (LogEntry e in list)
                {
                    known.Add(LogKey(e));
                }

                int added = 0;
                int skipped = 0;
                foreach (LogEntry e in pkg.Log)
                {
                    if (!replace && known.Contains(LogKey(e)))
                    {
                        skipped++;
                        continue;
                    }
                    list.Add(e);
                    known.Add(LogKey(e));
                    added++;
                }

                // Newest first
                list.Sort((a, b) => string.Compare(b.Date, a.Date, StringComparison.Ordinal));
                LogStore.Save(list);

                if (replace) sb.AppendLine("Saved log: replaced, now " + list.Count + " entries");
                else sb.AppendLine("Saved log: " + added + " added, " + skipped + " skipped (already there)");
            }

            sb.AppendLine();
            sb.AppendLine("A backup of your previous data was saved in:");
            sb.AppendLine(BackupFolder);
            return sb.ToString();
        }
    }
}