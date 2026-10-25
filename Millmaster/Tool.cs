using System;

namespace Millmaster
{
    // The fixed list of tool types
    public enum ToolType
    {
        Endmill,
        BallMill,
        Drill,
        FaceMill,
        ThreadMill,
        ThreadTap
    }

    // Helper methods that go with the list
    public static class ToolTypeInfo
    {
        // The readable name shown to the user
        public static string DisplayName(this ToolType type)
        {
            switch (type)
            {
                case ToolType.Endmill: return "Endmill";
                case ToolType.BallMill: return "Ball mill";
                case ToolType.Drill: return "Drill";
                case ToolType.FaceMill: return "Face mill";
                case ToolType.ThreadMill: return "Thread mill";
                case ToolType.ThreadTap: return "Thread tap";
                default: return type.ToString();
            }
        }

        // The name of the picture in Resources (no spaces)
        public static string ResourceName(this ToolType type)
        {
            return type.DisplayName().Replace(" ", "");
        }

        // Tools that use the Operation choice (roughing / finishing / slotting)
        public static bool UsesOperation(this ToolType type)
        {
            return type == ToolType.Endmill || type == ToolType.BallMill || type == ToolType.FaceMill;
        }

        // Tools that have a number of flutes (teeth) to enter
        public static bool UsesFlutes(this ToolType type)
        {
            return type == ToolType.Endmill || type == ToolType.BallMill
                || type == ToolType.FaceMill || type == ToolType.ThreadMill;
        }

        // Turns a saved readable name back into the type
        public static ToolType FromName(string name)
        {
            foreach (ToolType t in Enum.GetValues(typeof(ToolType)))
            {
                if (string.Equals(t.DisplayName(), name, StringComparison.OrdinalIgnoreCase))
                {
                    return t;
                }
            }
            return ToolType.Endmill;
        }
    }

    public class Tool
    {
        public ToolType Type { get; set; }
        public string Name { get { return Type.DisplayName(); } }   // readable name, read-only

        public double Diameter { get; set; }        // tool diameter (thread diameter for a tap), mm
        public int Flutes { get; set; }
        public string ToolMaterial { get; set; }    // "Carbide" or "HSS"
        public double Pitch { get; set; }           // thread pitch, mm (thread tools only)
        public double ThreadDiameter { get; set; }  // major diameter of the thread, mm
        public bool Internal { get; set; }          // thread mill: internal or external thread
    }
}