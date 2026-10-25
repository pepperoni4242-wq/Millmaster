using System;

namespace Millmaster
{
    // Holds the answers
    public class CalcResult
    {
        public double Vc { get; set; }                // cutting speed, m/min (at the effective diameter for a ball mill)
        public double Rpm { get; set; }               // spindle speed
        public double FeedPerTooth { get; set; }      // mm per tooth (per revolution for drill and tap)
        public double FeedRate { get; set; }          // mm/min (at the cutter edge for a thread mill)
        public double Ap { get; set; }                // axial depth of cut, mm
        public double Ae { get; set; }                // radial depth of cut / stepover, mm
        public double Mrr { get; set; }               // material removal rate, cm3/min
        public double ProgrammedFeed { get; set; }    // thread mill: feed for the CNC program (tool centre)
        public double TapDrill { get; set; }          // thread tap: drill size, mm
        public int Passes { get; set; }               // thread mill: radial passes
        public double EffectiveDiameter { get; set; } // ball mill: diameter really cutting, mm
        public double ScallopHeight { get; set; }     // ball mill: ridge height between passes, mm
        public string Note { get; set; } = "";
    }

    public static class Calculator
    {
        // Diameter that is really cutting for a ball nose at depth ap
        private static double EffectiveDiameter(double d, double ap)
        {
            double de;
            if (ap >= d / 2.0)
            {
                de = d;
            }
            else
            {
                de = 2.0 * Math.Sqrt(ap * (d - ap));
            }

            // Safety: avoid an extremely small diameter
            if (de < d * 0.05) de = d * 0.05;
            return de;
        }

        public static CalcResult Calculate(Tool tool, Material material, double maxRpm, string operation)
        {
            CalcResult result = new CalcResult();
            ToolType type = tool.Type;

            // 1. Base cutting speed (taps are always HSS)
            double vc;
            if (type == ToolType.ThreadTap || tool.ToolMaterial != "Carbide")
            {
                vc = material.VcHss;
            }
            else
            {
                vc = material.VcCarbide;
            }

            // 2. Settings that depend on the tool
            double vcFactor;
            double fzFactor = 1.0;
            double ap = 0;
            double ae = 0;

            if (type == ToolType.Drill)
            {
                vcFactor = CalcSettings.Get("Drill.VcFactor");
            }
            else if (type == ToolType.ThreadTap)
            {
                vcFactor = CalcSettings.Get("Tap.VcFactor");
            }
            else if (type == ToolType.ThreadMill)
            {
                vcFactor = CalcSettings.Get("ThreadMill.VcFactor");
                fzFactor = CalcSettings.Get("ThreadMill.FzFactor");
            }
            else if (type == ToolType.FaceMill)
            {
                string op = operation;
                if (op == "Slotting")
                {
                    op = "Roughing";
                    result.Note = "Slotting does not apply to a face mill, so Roughing values were used.";
                }

                string prefix;
                if (op == "Finishing") prefix = "FaceFinish";
                else prefix = "FaceRough";

                vcFactor = CalcSettings.Get(prefix + ".VcFactor");
                fzFactor = CalcSettings.Get(prefix + ".FzFactor");
                ap = CalcSettings.Get(prefix + ".Ap");                     // fixed depth in mm
                ae = tool.Diameter * CalcSettings.Get(prefix + ".Ae");
            }
            else if (type == ToolType.BallMill)
            {
                string op = operation;
                if (op == "Slotting")
                {
                    op = "Roughing";
                    result.Note = "Slotting does not apply to a ball mill, so Roughing values were used.";
                }

                string prefix;
                if (op == "Finishing") prefix = "BallFinish";
                else prefix = "BallRough";

                vcFactor = CalcSettings.Get(prefix + ".VcFactor");
                fzFactor = CalcSettings.Get(prefix + ".FzFactor");
                ap = tool.Diameter * CalcSettings.Get(prefix + ".Ap");
                ae = tool.Diameter * CalcSettings.Get(prefix + ".Ae");
            }
            else // Endmill
            {
                string prefix;
                if (operation == "Roughing") prefix = "Rough";
                else if (operation == "Finishing") prefix = "Finish";
                else prefix = "Slot";

                vcFactor = CalcSettings.Get(prefix + ".VcFactor");
                fzFactor = CalcSettings.Get(prefix + ".FzFactor");
                ap = tool.Diameter * CalcSettings.Get(prefix + ".Ap");
                ae = tool.Diameter * CalcSettings.Get(prefix + ".Ae");
            }

            vc = vc * vcFactor;

            // 3. The diameter used for the speed calculation
            double speedDia = tool.Diameter;
            if (type == ToolType.BallMill)
            {
                speedDia = EffectiveDiameter(tool.Diameter, ap);
                result.EffectiveDiameter = speedDia;
            }

            // RPM = (Vc x 1000) / (pi x diameter)
            double rpm = vc * 1000.0 / (Math.PI * speedDia);

            // 4. Don't exceed what your machine can do
            if (rpm > maxRpm)
            {
                rpm = maxRpm;
                result.Note = (result.Note + " Spindle speed was limited by the machine maximum.").Trim();
            }

            // Recalculate the real cutting speed after limiting the RPM
            vc = Math.PI * speedDia * rpm / 1000.0;

            // 5. Feed
            double feedPerTooth;
            double feedRate;
            double mrr = 0;

            if (type == ToolType.Drill)
            {
                // Feed per revolution
                feedPerTooth = CalcSettings.Get("Drill.FeedCoeff")
                    * Math.Pow(tool.Diameter, CalcSettings.Get("Drill.FeedExponent"))
                    * material.FeedFactor;
                feedRate = feedPerTooth * rpm;
            }
            else if (type == ToolType.ThreadTap)
            {
                // Feed = pitch x RPM (use rigid tapping)
                feedPerTooth = tool.Pitch;
                feedRate = tool.Pitch * rpm;
                result.TapDrill = tool.Diameter - CalcSettings.Get("Tap.DrillFactor") * tool.Pitch;
            }
            else if (type == ToolType.ThreadMill)
            {
                feedPerTooth = CalcSettings.Get("Mill.ChipLoadCoeff")
                    * Math.Pow(tool.Diameter, CalcSettings.Get("Mill.ChipLoadExponent"))
                    * material.FeedFactor * fzFactor;

                // Feed at the cutting edge
                feedRate = feedPerTooth * tool.Flutes * rpm;

                // Feed for the CNC program (tool centre path of the circular move)
                if (tool.Internal)
                {
                    result.ProgrammedFeed = feedRate * (tool.ThreadDiameter - tool.Diameter) / tool.ThreadDiameter;
                }
                else
                {
                    result.ProgrammedFeed = feedRate * (tool.ThreadDiameter + tool.Diameter) / tool.ThreadDiameter;
                }

                // Passes and depth
                int passes = (int)Math.Round(CalcSettings.Get("ThreadMill.Passes"));
                if (passes < 1) passes = 1;
                result.Passes = passes;

                double threadDepth = CalcSettings.Get("ThreadMill.DepthFactor") * tool.Pitch;
                ae = threadDepth / passes;   // radial depth per pass
                ap = tool.Pitch;             // axial advance per revolution
            }
            else
            {
                // Endmill, ball mill and face mill
                double coeff;
                double exponent;
                if (type == ToolType.FaceMill)
                {
                    coeff = CalcSettings.Get("FaceMill.ChipLoadCoeff");
                    exponent = CalcSettings.Get("FaceMill.ChipLoadExponent");
                }
                else if (type == ToolType.BallMill)
                {
                    coeff = CalcSettings.Get("BallMill.ChipLoadCoeff");
                    exponent = CalcSettings.Get("BallMill.ChipLoadExponent");
                }
                else
                {
                    coeff = CalcSettings.Get("Mill.ChipLoadCoeff");
                    exponent = CalcSettings.Get("Mill.ChipLoadExponent");
                }

                feedPerTooth = coeff * Math.Pow(tool.Diameter, exponent) * material.FeedFactor * fzFactor;

                // Chip thinning (end mill and ball mill): a small radial cut makes a thinner chip.
                // A ball mill is judged against its effective diameter.
                double thinDia = tool.Diameter;
                if (type == ToolType.BallMill) thinDia = result.EffectiveDiameter;

                if ((type == ToolType.Endmill || type == ToolType.BallMill) && ae < thinDia * 0.5)
                {
                    double x = 1.0 - 2.0 * ae / thinDia;
                    double thinning = 1.0 / Math.Sqrt(1.0 - x * x);
                    double maxThinning = CalcSettings.Get("Mill.MaxChipThinning");
                    if (thinning > maxThinning) thinning = maxThinning;
                    feedPerTooth = feedPerTooth * thinning;
                }

                feedRate = feedPerTooth * tool.Flutes * rpm;

                // Material removal rate in cm3/min
                mrr = ap * ae * feedRate / 1000.0;

                // Ball mill: height of the ridges left between passes on a flat surface
                if (type == ToolType.BallMill)
                {
                    double r = tool.Diameter / 2.0;
                    double half = ae / 2.0;
                    if (half < r)
                    {
                        result.ScallopHeight = r - Math.Sqrt(r * r - half * half);
                    }
                }
            }

            result.Vc = vc;
            result.Rpm = rpm;
            result.FeedPerTooth = feedPerTooth;
            result.FeedRate = feedRate;
            result.Ap = ap;
            result.Ae = ae;
            result.Mrr = mrr;
            return result;
        }
    }
}