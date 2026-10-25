using System;

namespace Millmaster
{
    public class LogEntry
    {
        public int Version { get; set; }              // 1 = old entry, 2 = complete entry
        public string Name { get; set; } = "";
        public string Date { get; set; } = "";

        // Tool
        public string ToolName { get; set; } = "";
        public string ToolMaterial { get; set; } = "";
        public double Diameter { get; set; }
        public int Flutes { get; set; }
        public double Pitch { get; set; }
        public double ThreadDiameter { get; set; }
        public bool Internal { get; set; }

        // Material (a full copy, so later edits to the material don't change this entry)
        public string MaterialName { get; set; } = "";
        public double MatHardness { get; set; }
        public double MatVcCarbide { get; set; }
        public double MatVcHss { get; set; }
        public double MatFeedFactor { get; set; }

        // Settings used
        public string Operation { get; set; } = "";
        public double MaxRpm { get; set; }

        // Results
        public double Vc { get; set; }
        public double Rpm { get; set; }
        public double FeedPerTooth { get; set; }
        public double FeedRate { get; set; }
        public double Ap { get; set; }
        public double Ae { get; set; }
        public double Mrr { get; set; }
        public double ProgrammedFeed { get; set; }
        public double TapDrill { get; set; }
        public int Passes { get; set; }
        public double EffectiveDiameter { get; set; }
        public double ScallopHeight { get; set; }
        public string Note { get; set; } = "";

        // Builds a complete entry from what is on the main form
        public static LogEntry Create(string name, Tool tool, Material mat,
                                      CalcResult r, string operation, double maxRpm)
        {
            LogEntry e = new LogEntry();
            e.Version = 2;
            e.Name = name;
            e.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            e.ToolName = tool.Name;
            e.ToolMaterial = tool.ToolMaterial;
            e.Diameter = tool.Diameter;
            e.Flutes = tool.Flutes;
            e.Pitch = tool.Pitch;
            e.ThreadDiameter = tool.ThreadDiameter;
            e.Internal = tool.Internal;

            e.MaterialName = mat.Name;
            e.MatHardness = mat.Hardness;
            e.MatVcCarbide = mat.VcCarbide;
            e.MatVcHss = mat.VcHss;
            e.MatFeedFactor = mat.FeedFactor;

            e.Operation = operation;
            e.MaxRpm = maxRpm;

            e.Vc = r.Vc;
            e.Rpm = r.Rpm;
            e.FeedPerTooth = r.FeedPerTooth;
            e.FeedRate = r.FeedRate;
            e.Ap = r.Ap;
            e.Ae = r.Ae;
            e.Mrr = r.Mrr;
            e.ProgrammedFeed = r.ProgrammedFeed;
            e.TapDrill = r.TapDrill;
            e.Passes = r.Passes;
            e.EffectiveDiameter = r.EffectiveDiameter;
            e.ScallopHeight = r.ScallopHeight;
            e.Note = r.Note;
            return e;
        }

        // Turn the entry back into the objects the main form uses
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

        public Material ToMaterial()
        {
            Material m = new Material();
            m.Name = MaterialName;
            m.Hardness = MatHardness;
            m.VcCarbide = MatVcCarbide;
            m.VcHss = MatVcHss;
            m.FeedFactor = MatFeedFactor;
            return m;
        }

        public CalcResult ToResult()
        {
            CalcResult r = new CalcResult();
            r.Vc = Vc;
            r.Rpm = Rpm;
            r.FeedPerTooth = FeedPerTooth;
            r.FeedRate = FeedRate;
            r.Ap = Ap;
            r.Ae = Ae;
            r.Mrr = Mrr;
            r.ProgrammedFeed = ProgrammedFeed;
            r.TapDrill = TapDrill;
            r.Passes = Passes;
            r.EffectiveDiameter = EffectiveDiameter;
            r.ScallopHeight = ScallopHeight;
            r.Note = Note;
            return r;
        }
    }
}