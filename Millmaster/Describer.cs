using System;

namespace Millmaster
{
    public static class Describer
    {
        public static string DescribeTool(Tool t)
        {
            string nl = Environment.NewLine;
            string text = "Tool: " + t.Name + nl;

            if (t.Type == ToolType.ThreadTap)
            {
                text += "Thread diameter: " + t.Diameter + " mm" + nl +
                        "Pitch: " + t.Pitch + " mm" + nl;
            }
            else if (t.Type == ToolType.ThreadMill)
            {
                string kind;
                if (t.Internal) kind = "Internal thread";
                else kind = "External thread";

                text += "Cutter diameter: " + t.Diameter + " mm" + nl +
                        "Thread diameter: " + t.ThreadDiameter + " mm" + nl +
                        "Pitch: " + t.Pitch + " mm" + nl +
                        kind + nl;
            }
            else
            {
                text += "Diameter: " + t.Diameter + " mm" + nl;
            }

            if (t.Type.UsesFlutes())
            {
                text += "Flutes: " + t.Flutes + nl;
            }

            text += "Tool material: " + t.ToolMaterial;
            return text;
        }

        public static string DescribeMaterial(Material m)
        {
            string nl = Environment.NewLine;
            return "Material: " + m.Name + nl +
                   "Hardness: " + m.Hardness + " HB" + nl +
                   "Cutting speed (carbide): " + m.VcCarbide + " m/min" + nl +
                   "Cutting speed (HSS): " + m.VcHss + " m/min" + nl +
                   "Feed factor: " + m.FeedFactor.ToString("0.00");
        }

        public static string DescribeResult(Tool tool, CalcResult r, string operation)
        {
            string nl = Environment.NewLine;

            string text =
                "Cutting speed (Vc): " + r.Vc.ToString("0.0") + " m/min" + nl +
                "Spindle speed: " + r.Rpm.ToString("0") + " RPM" + nl;

            if (tool.Type == ToolType.ThreadTap)
            {
                text +=
                    "Feed per revolution (= pitch): " + r.FeedPerTooth.ToString("0.00") + " mm" + nl +
                    "Feed rate: " + r.FeedRate.ToString("0") + " mm/min" + nl +
                    "Tap drill size: " + r.TapDrill.ToString("0.00") + " mm";
            }
            else if (tool.Type == ToolType.ThreadMill)
            {
                text +=
                    "Feed per tooth: " + r.FeedPerTooth.ToString("0.0000") + " mm" + nl +
                    "Feed rate at cutter edge: " + r.FeedRate.ToString("0") + " mm/min" + nl +
                    "Programmed feed (tool centre): " + r.ProgrammedFeed.ToString("0") + " mm/min" + nl +
                    "Radial passes: " + r.Passes + nl +
                    "Depth per pass: " + r.Ae.ToString("0.000") + " mm" + nl +
                    "Axial advance per revolution: " + r.Ap.ToString("0.00") + " mm (= pitch)";
            }
            else if (tool.Type == ToolType.BallMill)
            {
                string opText = operation;
                if (opText == "") opText = "(not saved)";

                text +=
                    "Effective cutting diameter: " + r.EffectiveDiameter.ToString("0.00") + " mm" + nl +
                    "Feed per tooth: " + r.FeedPerTooth.ToString("0.0000") + " mm" + nl +
                    "Feed rate: " + r.FeedRate.ToString("0") + " mm/min" + nl +
                    "Operation: " + opText + nl +
                    "Axial depth Ap: " + r.Ap.ToString("0.00") + " mm" + nl +
                    "Stepover Ae: " + r.Ae.ToString("0.00") + " mm" + nl +
                    "Scallop height (flat surface): " + r.ScallopHeight.ToString("0.0000") + " mm" + nl +
                    "Material removal rate: " + r.Mrr.ToString("0.0") + " cm3/min";
            }
            else if (tool.Type == ToolType.Drill)
            {
                text +=
                    "Feed per revolution: " + r.FeedPerTooth.ToString("0.0000") + " mm" + nl +
                    "Feed rate: " + r.FeedRate.ToString("0") + " mm/min";
            }
            else // Endmill and face mill
            {
                string opText = operation;
                if (opText == "") opText = "(not saved)";

                text +=
                    "Feed per tooth: " + r.FeedPerTooth.ToString("0.0000") + " mm" + nl +
                    "Feed rate: " + r.FeedRate.ToString("0") + " mm/min" + nl +
                    "Operation: " + opText + nl +
                    "Axial depth Ap: " + r.Ap.ToString("0.00") + " mm" + nl +
                    "Radial depth Ae: " + r.Ae.ToString("0.00") + " mm" + nl +
                    "Material removal rate: " + r.Mrr.ToString("0.0") + " cm3/min";
            }

            if (!tool.Type.UsesOperation())
            {
                text += nl + "Note: The Operation choice is not used for " + tool.Name.ToLower() + ".";
            }
            if (r.Note != "")
            {
                text += nl + "Note: " + r.Note;
            }
            return text;
        }
    }
}