using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Millmaster
{
    // One saved tool in the library
    public class LibraryTool
    {
        public string Label { get; set; } = "";         // the name you gave it
        public string ToolName { get; set; } = "";      // readable tool type, e.g. "Face mill"
        public string ToolMaterial { get; set; } = "";
        public double Diameter { get; set; }
        public int Flutes { get; set; }
        public double Pitch { get; set; }
        public double ThreadDiameter { get; set; }
        public bool Internal { get; set; }

        public static LibraryTool FromTool(string label, Tool t)
        {
            LibraryTool entry = new LibraryTool();
            entry.UpdateFrom(label, t);
            return entry;
        }

        // Copies a tool's values into this entry (used for new and edited tools)
        public void UpdateFrom(string label, Tool t)
        {
            Label = label;
            ToolName = t.Name;
            ToolMaterial = t.ToolMaterial;
            Diameter = t.Diameter;
            Flutes = t.Flutes;
            Pitch = t.Pitch;
            ThreadDiameter = t.ThreadDiameter;
            Internal = t.Internal;
        }

        public Tool ToTool()
        {
            Tool t = new Tool();
            t.Type = ToolTypeInfo.FromName(ToolName);
            t.ToolMaterial = ToolMaterial;
            t.Diameter = Diameter;
            t.Flutes = Flutes;
            t.Pitch = Pitch;
            t.ThreadDiameter = ThreadDiameter;
            t.Internal = Internal;
            return t;
        }
    }

    public static class ToolLibraryStore
    {
        private static readonly string Folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Millmaster");
        private static readonly string FilePath = Path.Combine(Folder, "tools.json");

        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions { WriteIndented = true };

        public static List<LibraryTool> Load()
        {
            if (!File.Exists(FilePath)) return new List<LibraryTool>();

            try
            {
                List<LibraryTool> list =
                    JsonSerializer.Deserialize<List<LibraryTool>>(File.ReadAllText(FilePath));
                if (list != null) return list;
            }
            catch
            {
                // Damaged file: keep a copy so nothing is lost, then start empty
                try { File.Copy(FilePath, FilePath + ".bak", true); } catch { }
            }
            return new List<LibraryTool>();
        }

        public static void Save(List<LibraryTool> list)
        {
            Directory.CreateDirectory(Folder);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(list, Options));
        }

        // All the names already used
        public static List<string> Names()
        {
            List<string> names = new List<string>();
            foreach (LibraryTool t in Load())
            {
                names.Add(t.Label);
            }
            return names;
        }
    }
}