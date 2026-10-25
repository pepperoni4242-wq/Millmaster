using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace Millmaster
{
    public static class LogStore
    {
        private static readonly string Folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Millmaster");
        private static readonly string JsonPath = Path.Combine(Folder, "log.json");
        private static readonly string OldPath = Path.Combine(Folder, "log.txt");

        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions { WriteIndented = true };

        public static List<LogEntry> Load()
        {
            if (File.Exists(JsonPath))
            {
                try
                {
                    List<LogEntry> list =
                        JsonSerializer.Deserialize<List<LogEntry>>(File.ReadAllText(JsonPath));
                    if (list != null) return list;
                }
                catch
                {
                    // Damaged file: keep a copy so nothing is lost, then start empty
                    try { File.Copy(JsonPath, JsonPath + ".bak", true); } catch { }
                }
                return new List<LogEntry>();
            }

            // First run of this version: bring in the old log.txt
            List<LogEntry> imported = LoadOldFormat();
            if (imported.Count > 0)
            {
                Save(imported);
                try { File.Move(OldPath, OldPath + ".old", true); } catch { }
            }
            return imported;
        }

        public static void Save(List<LogEntry> list)
        {
            Directory.CreateDirectory(Folder);
            File.WriteAllText(JsonPath, JsonSerializer.Serialize(list, Options));
        }

        // Reads the old tab-separated format (11 columns)
        private static List<LogEntry> LoadOldFormat()
        {
            List<LogEntry> list = new List<LogEntry>();
            if (!File.Exists(OldPath)) return list;

            foreach (string line in File.ReadAllLines(OldPath))
            {
                string[] p = line.Split('\t');
                if (p.Length != 11) continue;
                try
                {
                    LogEntry e = new LogEntry();
                    e.Version = 1;
                    e.Name = p[0];
                    e.Date = p[1];
                    e.ToolName = p[2];
                    e.Diameter = double.Parse(p[3], CultureInfo.InvariantCulture);
                    e.Flutes = int.Parse(p[4]);
                    e.ToolMaterial = p[5];
                    e.MaterialName = p[6];
                    e.Vc = double.Parse(p[7], CultureInfo.InvariantCulture);
                    e.Rpm = double.Parse(p[8], CultureInfo.InvariantCulture);
                    e.FeedPerTooth = double.Parse(p[9], CultureInfo.InvariantCulture);
                    e.FeedRate = double.Parse(p[10], CultureInfo.InvariantCulture);
                    list.Add(e);
                }
                catch
                {
                    // Skip a damaged line
                }
            }
            return list;
        }
    }
}