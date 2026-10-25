using System;
using System.Collections.Generic;
using System.Text;

namespace Millmaster
{
    public class Material
    {
        public string Name { get; set; }
        public double Hardness { get; set; }
        public double VcCarbide { get; set; }
        public double VcHss { get; set; }
        public double FeedFactor { get; set; }

        // Makes the ListBox show the name instead of "Millmaster.Material"
        public override string ToString()
        {
            return Name;
        }
    }
}
