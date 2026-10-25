using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Millmaster
{
    // Describes one adjustable number
    public class SettingInfo
    {
        public string Key { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Default { get; set; }
        public string Description { get; set; }


        public SettingInfo(string key, string category, string name, string unit,
                           double defaultValue, string description)
        {
            Key = key;
            Category = category;
            Name = name;
            Unit = unit;
            Default = defaultValue;
            Description = description;
        }
    }

    public static class CalcSettings
    {
        // Category names (used in the list and to group settings)
        public const string EmGeneral = "End mill - General";
        public const string EmRough = "End mill - Roughing";
        public const string EmFinish = "End mill - Finishing";
        public const string EmSlot = "End mill - Slotting";
        public const string FmGeneral = "Face mill - General";
        public const string FmRough = "Face mill - Roughing";
        public const string FmFinish = "Face mill - Finishing";
        public const string DrillCat = "Drill";
        public const string ThreadMillCat = "Thread mill";
        public const string TapCat = "Thread tap";
        public const string BallGeneral = "Ball mill - General";
        public const string BallRough = "Ball mill - Roughing";
        public const string BallFinish = "Ball mill - Finishing";

        // The order the categories appear in the list
        public static readonly string[] Categories =
{
            EmGeneral, EmRough, EmFinish, EmSlot,
            BallGeneral, BallRough, BallFinish,
            FmGeneral, FmRough, FmFinish,
            DrillCat, ThreadMillCat, TapCat
        };

        // Every adjustable number, with its default and description
        public static readonly List<SettingInfo> All = new List<SettingInfo>
        {
            // ---------- End mill - General ----------
            new SettingInfo("Mill.ChipLoadCoeff", EmGeneral, "Chip load coefficient", "mm", 0.0035,
                "The base chip load (feed per tooth) for end mills: chip load = coefficient x diameter ^ exponent x material feed factor x operation factor.\n" +
                "Raise it to feed faster with every end mill. Lower it if tools chatter, wear quickly or break.\n" +
                "Typical range: 0.002 to 0.006."),
            new SettingInfo("Mill.ChipLoadExponent", EmGeneral, "Chip load exponent", "-", 0.85,
                "Controls how fast the chip load grows with tool diameter. 1.0 means the chip load grows in direct proportion to the diameter. Smaller values give relatively lighter feeds on big tools.\n" +
                "Usually left between 0.7 and 1.0."),
            new SettingInfo("Mill.MaxChipThinning", EmGeneral, "Maximum chip-thinning boost", "x", 2.5,
                "When the radial cut (Ae) is less than half the tool diameter, the chips get thinner, so Millmaster raises the feed per tooth to compensate. This is the upper limit of that boost.\n" +
                "1 switches chip thinning off. 2.5 allows up to 2.5 times the normal feed. Lower it for flexible setups or long tools."),

            // ---------- End mill - Roughing ----------
            new SettingInfo("Rough.VcFactor", EmRough, "Cutting speed factor", "x", 0.85,
                "Multiplies the material's cutting speed for end mill roughing. Below 1 runs slower than the table speed to protect the tool in heavy cuts; above 1 runs faster.\n" +
                "Typical range: 0.7 to 1.0."),
            new SettingInfo("Rough.FzFactor", EmRough, "Chip load factor", "x", 1.0,
                "Multiplies the chip load for roughing. 1.0 uses the full base chip load.\n" +
                "Lower it for weak setups or long tools; raise it a little only on rigid machines."),
            new SettingInfo("Rough.Ap", EmRough, "Axial depth Ap", "x D", 1.0,
                "How deep the cut is along the tool axis, as a multiple of the tool diameter. 1.0 means one tool diameter deep.\n" +
                "Deeper cuts remove more material but increase tool deflection and cutting force."),
            new SettingInfo("Rough.Ae", EmRough, "Radial depth Ae", "x D", 0.4,
                "How wide the cut is, as a fraction of the tool diameter (maximum 1.0 = full width). 0.4 means 40% of the diameter.\n" +
                "Wider cuts remove more material but raise cutting force and heat."),

            // ---------- End mill - Finishing ----------
            new SettingInfo("Finish.VcFactor", EmFinish, "Cutting speed factor", "x", 1.10,
                "Multiplies the cutting speed for finishing. Finishing cuts are light, so the tool can run a little faster, which usually gives a better surface.\n" +
                "Typical range: 1.0 to 1.3."),
            new SettingInfo("Finish.FzFactor", EmFinish, "Chip load factor", "x", 0.65,
                "Multiplies the chip load for finishing. A lower chip load gives a finer surface and less tool deflection.\n" +
                "Typical range: 0.5 to 0.8."),
            new SettingInfo("Finish.Ap", EmFinish, "Axial depth Ap", "x D", 1.0,
                "Depth of the finishing cut along the tool axis, as a multiple of the tool diameter.\n" +
                "Long, deep finishing cuts on thin walls can bend the tool; reduce this if walls are not straight."),
            new SettingInfo("Finish.Ae", EmFinish, "Radial depth Ae", "x D", 0.05,
                "Width of the finishing cut, as a fraction of the tool diameter. 0.05 is a very light 5% pass.\n" +
                "With such a small value, chip thinning raises the feed (limited by the maximum boost in End mill - General)."),

            // ---------- End mill - Slotting ----------
            new SettingInfo("Slot.VcFactor", EmSlot, "Cutting speed factor", "x", 0.80,
                "Multiplies the cutting speed for slotting. In a slot the tool is buried in the material on both sides and chips are hard to clear, so the speed is reduced.\n" +
                "Typical range: 0.6 to 0.9."),
            new SettingInfo("Slot.FzFactor", EmSlot, "Chip load factor", "x", 0.70,
                "Multiplies the chip load for slotting. Lower values protect the tool when chips can't escape.\n" +
                "Typical range: 0.5 to 0.8."),
            new SettingInfo("Slot.Ap", EmSlot, "Axial depth Ap", "x D", 0.5,
                "Depth of one slotting pass, as a multiple of the tool diameter. It is shallower than roughing because a full-width cut loads the tool heavily.\n" +
                "Take several passes to reach a deep slot."),
            new SettingInfo("Slot.Ae", EmSlot, "Radial depth Ae", "x D", 1.0,
                "Width of the cut. A slot is a full-width cut, so this is normally 1.0 (the full diameter). The maximum is 1.0."),

                        // ---------- Ball mill - General ----------
            new SettingInfo("BallMill.ChipLoadCoeff", BallGeneral, "Chip load coefficient", "mm", 0.003,
                "The base chip load (feed per tooth) for ball mills: chip load = coefficient x diameter ^ exponent x material feed factor x operation factor. It is a little lower than for end mills because a ball tip cuts thinner chips.\n" +
                "Raise it to feed faster; lower it for chatter or fast wear. Typical range: 0.002 to 0.005."),
            new SettingInfo("BallMill.ChipLoadExponent", BallGeneral, "Chip load exponent", "-", 0.85,
                "Controls how fast the ball mill chip load grows with tool diameter. 1.0 means direct proportion; smaller values give lighter feeds on big tools.\n" +
                "Usually left between 0.7 and 1.0."),

            // ---------- Ball mill - Roughing ----------
            new SettingInfo("BallRough.VcFactor", BallRough, "Cutting speed factor", "x", 0.85,
                "Multiplies the material's cutting speed for ball mill roughing. The speed is applied at the effective cutting diameter (the part of the ball that is really in the cut), not the full diameter.\n" +
                "Typical range: 0.7 to 1.0."),
            new SettingInfo("BallRough.FzFactor", BallRough, "Chip load factor", "x", 1.0,
                "Multiplies the chip load for roughing. 1.0 uses the full base chip load.\n" +
                "Lower it for weak setups or long tools."),
            new SettingInfo("BallRough.Ap", BallRough, "Axial depth Ap", "x D", 0.25,
                "Depth of cut as a multiple of the ball diameter. It decides the effective cutting diameter: a shallow cut uses only the tip of the ball, so the spindle must turn faster to reach the same cutting speed.\n" +
                "Typical roughing range: 0.15 to 0.4."),
            new SettingInfo("BallRough.Ae", BallRough, "Stepover Ae", "x D", 0.3,
                "Distance between passes, as a fraction of the ball diameter (maximum 1.0). Larger stepovers remove material faster but raise cutting force."),

            // ---------- Ball mill - Finishing ----------
            new SettingInfo("BallFinish.VcFactor", BallFinish, "Cutting speed factor", "x", 1.10,
                "Multiplies the cutting speed for ball mill finishing. Light cuts allow a higher speed and a better surface.\n" +
                "Typical range: 1.0 to 1.3."),
            new SettingInfo("BallFinish.FzFactor", BallFinish, "Chip load factor", "x", 0.65,
                "Multiplies the chip load for finishing. A lower chip load gives a finer surface.\n" +
                "Typical range: 0.5 to 0.8."),
            new SettingInfo("BallFinish.Ap", BallFinish, "Axial depth Ap", "x D", 0.05,
                "Depth of the finishing cut as a multiple of the ball diameter. Very light cuts use only the tip of the ball.\n" +
                "Typical finishing range: 0.02 to 0.1."),
            new SettingInfo("BallFinish.Ae", BallFinish, "Stepover Ae", "x D", 0.05,
                "Distance between finishing passes, as a fraction of the ball diameter (maximum 1.0). A smaller stepover gives a smoother surface (less scallop height) but takes longer. The result shows the scallop height so you can judge it."),

            // ---------- Face mill - General ----------
            new SettingInfo("FaceMill.ChipLoadCoeff", FmGeneral, "Chip load coefficient", "mm", 0.004,
                "The base chip load (feed per tooth) for face mills: chip load = coefficient x diameter ^ exponent x material feed factor x operation factor. It is a little larger than for end mills because insert cutters take heavier chips.\n" +
                "Raise it for faster feeds; lower it for chatter or insert chipping. Typical range: 0.003 to 0.006."),
            new SettingInfo("FaceMill.ChipLoadExponent", FmGeneral, "Chip load exponent", "-", 0.85,
                "Controls how fast the face mill chip load grows with cutter diameter. 1.0 means direct proportion; smaller values give lighter feeds on big cutters.\n" +
                "Usually left between 0.7 and 1.0."),

            // ---------- Face mill - Roughing ----------
            new SettingInfo("FaceRough.VcFactor", FmRough, "Cutting speed factor", "x", 0.85,
                "Multiplies the material's cutting speed for face mill roughing. Below 1 runs slower to protect the inserts in heavy cuts.\n" +
                "Typical range: 0.7 to 1.0."),
            new SettingInfo("FaceRough.FzFactor", FmRough, "Chip load factor", "x", 1.0,
                "Multiplies the chip load for roughing. 1.0 uses the full base chip load.\n" +
                "Lower it if the machine lacks power or the setup is not rigid."),
            new SettingInfo("FaceRough.Ap", FmRough, "Axial depth Ap", "mm", 3.0,
                "Depth of the roughing cut in millimetres. This is a fixed depth, not related to the cutter diameter, because it is limited by the insert size and machine power.\n" +
                "Typical range: 1 to 5 mm."),
            new SettingInfo("FaceRough.Ae", FmRough, "Radial engagement Ae", "x D", 0.7,
                "How much of the cutter width is in the cut, as a fraction of the diameter (maximum 1.0). 0.6 to 0.75 gives good chip formation and tool life."),

            // ---------- Face mill - Finishing ----------
            new SettingInfo("FaceFinish.VcFactor", FmFinish, "Cutting speed factor", "x", 1.10,
                "Multiplies the cutting speed for face mill finishing. Light cuts allow a higher speed and a better surface.\n" +
                "Typical range: 1.0 to 1.3."),
            new SettingInfo("FaceFinish.FzFactor", FmFinish, "Chip load factor", "x", 0.65,
                "Multiplies the chip load for finishing. A lower chip load leaves a finer surface.\n" +
                "Typical range: 0.5 to 0.8."),
            new SettingInfo("FaceFinish.Ap", FmFinish, "Axial depth Ap", "mm", 0.5,
                "Depth of the finishing cut in millimetres (a fixed depth). Typical range: 0.2 to 1 mm."),
            new SettingInfo("FaceFinish.Ae", FmFinish, "Radial engagement Ae", "x D", 0.6,
                "How much of the cutter width is in the cut during finishing, as a fraction of the diameter (maximum 1.0)."),

            // ---------- Drill ----------
            new SettingInfo("Drill.VcFactor", DrillCat, "Cutting speed factor", "x", 0.7,
                "Multiplies the material's cutting speed for drilling. Drilling is slower than milling because the tool cuts inside a closed hole where chips and coolant have a hard time.\n" +
                "Typical range: 0.5 to 0.9."),
            new SettingInfo("Drill.FeedCoeff", DrillCat, "Feed coefficient", "mm/rev", 0.012,
                "Base feed per revolution: feed = coefficient x diameter ^ exponent x material feed factor.\n" +
                "Raise it to drill faster. Lower it for deep holes, small drills or hard materials. Typical range: 0.008 to 0.02."),
            new SettingInfo("Drill.FeedExponent", DrillCat, "Feed exponent", "-", 0.75,
                "Controls how fast the feed per revolution grows with drill diameter. Larger drills take a bigger feed, but not in direct proportion.\n" +
                "Usually left between 0.6 and 1.0."),

            // ---------- Thread mill ----------
            new SettingInfo("ThreadMill.VcFactor", ThreadMillCat, "Cutting speed factor", "x", 0.8,
                "Multiplies the material's cutting speed for thread milling. Thread mills have long contact with the material, so they run a little slower than end mills.\n" +
                "Typical range: 0.6 to 1.0."),
            new SettingInfo("ThreadMill.FzFactor", ThreadMillCat, "Chip load factor", "x", 0.5,
                "Multiplies the end mill chip load to get the thread mill chip load. Thread mills have small teeth, so a fraction of the end mill value is used.\n" +
                "Typical range: 0.3 to 0.7."),
            new SettingInfo("ThreadMill.Passes", ThreadMillCat, "Radial passes", "passes", 1,
                "How many times the cutter goes around to reach the full thread depth. 1 pass is fine for soft materials; use 2 to 3 for hard materials or deep, fine threads to reduce tool load.\n" +
                "Must be a whole number."),
            new SettingInfo("ThreadMill.DepthFactor", ThreadMillCat, "Thread depth", "x pitch", 0.6134,
                "Total radial depth of the thread as a multiple of the pitch. 0.6134 is the standard ISO depth for external metric threads (internal threads are 0.5413 in theory), which gives a safe full thread.\n" +
                "Divided by the number of passes to get the depth of each pass."),

            // ---------- Thread tap ----------
            new SettingInfo("Tap.VcFactor", TapCat, "Cutting speed factor", "x", 0.25,
                "Multiplies the material's HSS cutting speed for tapping. Tapping is much slower than milling because the tap follows its own thread and cannot clear chips freely.\n" +
                "Raise it for coated taps with good lubrication; lower it for hard materials. Typical range: 0.15 to 0.4."),
            new SettingInfo("Tap.DrillFactor", TapCat, "Tap drill factor", "x pitch", 1.0,
                "Sets the size of the hole to drill before tapping: tap drill = thread diameter - factor x pitch.\n" +
                "Use 1.0 for cutting taps (M8 x 1.25 gives 6.75 mm) and about 0.5 for forming taps."),
        };

        // Values the user has changed (anything missing uses the default)
        private static Dictionary<string, double> values = new Dictionary<string, double>();

        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Millmaster", "settings.txt");

        static CalcSettings()
        {
            Load();
        }

        public static double Get(string key)
        {
            double v;
            if (values.TryGetValue(key, out v)) return v;

            foreach (SettingInfo s in All)
            {
                if (s.Key == key) return s.Default;
            }
            throw new ArgumentException("Unknown setting: " + key);
        }

        public static void Set(string key, double value)
        {
            values[key] = value;
        }

        public static void ResetAll()
        {
            values.Clear();
            Save();
        }

        public static void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            List<string> lines = new List<string>();
            foreach (SettingInfo s in All)
            {
                lines.Add(s.Key + "=" + Get(s.Key).ToString(CultureInfo.InvariantCulture));
            }
            File.WriteAllLines(FilePath, lines);
        }

        private static void Load()
        {
            if (!File.Exists(FilePath)) return;

            foreach (string line in File.ReadAllLines(FilePath))
            {
                string[] p = line.Split('=');
                if (p.Length != 2) continue;

                double v;
                if (double.TryParse(p[1], NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                {
                    values[p[0]] = v;
                }
            }
        }
    }
}