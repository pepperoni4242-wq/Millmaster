using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Millmaster
{
    public static class MaterialStore
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Millmaster", "materials.txt");

        public static List<Material> Load()
        {
            List<Material> list = new List<Material>();
            if (!File.Exists(FilePath)) return list;

            foreach (string line in File.ReadAllLines(FilePath))
            {
                string[] p = line.Split('\t');
                if (p.Length != 5) continue;
                try
                {
                    Material m = new Material();
                    m.Name = p[0];
                    m.Hardness = double.Parse(p[1], CultureInfo.InvariantCulture);
                    m.VcCarbide = double.Parse(p[2], CultureInfo.InvariantCulture);
                    m.VcHss = double.Parse(p[3], CultureInfo.InvariantCulture);
                    m.FeedFactor = double.Parse(p[4], CultureInfo.InvariantCulture);
                    list.Add(m);
                }
                catch
                {
                    // Skip a damaged line
                }
            }
            return list;
        }

        public static void Save(List<Material> list)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            List<string> lines = new List<string>();
            foreach (Material m in list)
            {
                lines.Add(string.Join("\t",
                    m.Name.Replace("\t", " "),
                    m.Hardness.ToString(CultureInfo.InvariantCulture),
                    m.VcCarbide.ToString(CultureInfo.InvariantCulture),
                    m.VcHss.ToString(CultureInfo.InvariantCulture),
                    m.FeedFactor.ToString(CultureInfo.InvariantCulture)));
            }
            File.WriteAllLines(FilePath, lines);
        }
    }
}