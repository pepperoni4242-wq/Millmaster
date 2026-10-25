using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Millmaster
{
    public partial class ToolSpecForm : Form
    {
        private ToolType toolType;

        // The main form reads these after the window closes
        public Tool ResultTool { get; private set; }
        public string LibraryName { get; private set; } = "";

        // Set by the tool library window: the name is then required
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool RequireLibraryName { get; set; }

        // Names that are already used in the library (a new name must not match)
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> TakenNames { get; set; } = new List<string>();

        // "existing" and "libraryName" are only given when editing a saved tool
        public ToolSpecForm(ToolType toolType, Tool existing = null, string libraryName = "")
        {
            InitializeComponent();

            this.toolType = toolType;
            lblToolType.Text = "Tool type: " + toolType.DisplayName();

            cboToolMaterial.Items.Add("Carbide");
            cboToolMaterial.Items.Add("HSS");
            cboToolMaterial.SelectedIndex = 0;

            // Picture: the resource is named like the tool, without spaces
            picTool.Image = Properties.Resources.ResourceManager.GetObject(toolType.ResourceName()) as Image;

            // Which rows does this tool need?
            bool showFlutes = toolType.UsesFlutes();
            bool showThreadDia = (toolType == ToolType.ThreadMill);
            bool showPitch = (toolType == ToolType.ThreadMill || toolType == ToolType.ThreadTap);

            lblFlutes.Visible = showFlutes;
            numFlutes.Visible = showFlutes;
            lblThreadDia.Visible = showThreadDia;
            numThreadDia.Visible = showThreadDia;
            lblPitch.Visible = showPitch;
            numPitch.Visible = showPitch;
            chkInternal.Visible = (toolType == ToolType.ThreadMill);

            // Label text that fits each tool
            if (toolType == ToolType.ThreadTap) lblDiameter.Text = "Thread diameter (mm)";
            else if (toolType == ToolType.ThreadMill || toolType == ToolType.FaceMill) lblDiameter.Text = "Cutter diameter (mm)";
            else lblDiameter.Text = "Diameter (mm)";

            // Taps are HSS
            if (toolType == ToolType.ThreadTap)
            {
                cboToolMaterial.SelectedItem = "HSS";
                cboToolMaterial.Enabled = false;
            }

            // Library name row
            lblLibraryName.Text = "Save to library as (optional):";
            txtLibraryName.Text = libraryName;

            // Editing a saved tool: fill the boxes with its values
            if (existing != null)
            {
                FillFrom(existing);
            }

            ArrangeRows();
        }

        // Used by the tool library window: the name becomes required
        public void SetLibraryMode()
        {
            RequireLibraryName = true;
            lblLibraryName.Text = "Library name:";
        }

        private void FillFrom(Tool t)
        {
            SetValue(numDiameter, t.Diameter);
            SetValue(numFlutes, t.Flutes);
            SetValue(numPitch, t.Pitch);
            SetValue(numThreadDia, t.ThreadDiameter);
            chkInternal.Checked = t.Internal;

            if (cboToolMaterial.Items.Contains(t.ToolMaterial))
            {
                cboToolMaterial.SelectedItem = t.ToolMaterial;
            }
        }

        // Puts a number into a box, kept inside the box's minimum and maximum
        private void SetValue(NumericUpDown box, double value)
        {
            decimal v = (decimal)value;
            if (v < box.Minimum) v = box.Minimum;
            if (v > box.Maximum) v = box.Maximum;
            box.Value = v;
        }

        // Stacks the visible input rows from top to bottom, with no gaps
        private void ArrangeRows()
        {
            int y = lblToolType.Bottom + 15;

            PlaceRow(ref y, lblDiameter, numDiameter);
            PlaceRow(ref y, lblThreadDia, numThreadDia);
            PlaceRow(ref y, lblPitch, numPitch);
            PlaceRow(ref y, lblFlutes, numFlutes);
            PlaceRow(ref y, null, chkInternal);
            PlaceRow(ref y, lblToolMaterial, cboToolMaterial);
            PlaceRow(ref y, lblLibraryName, txtLibraryName);
        }

        private void PlaceRow(ref int y, Control label, Control box)
        {
            if (!box.Visible) return;

            if (label != null)
            {
                label.Left = lblDiameter.Left;
                label.Top = y + 3;
                box.Left = numDiameter.Left;
                box.Width = numDiameter.Width;
            }
            else
            {
                box.Left = lblDiameter.Left;
            }
            box.Top = y;
            y += 32;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Library name checks
            string libName = txtLibraryName.Text.Trim();

            if (RequireLibraryName && libName == "")
            {
                MessageBox.Show("Please enter a name for this tool.");
                return;
            }
            if (libName != "")
            {
                foreach (string taken in TakenNames)
                {
                    if (string.Equals(taken, libName, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("A tool named \"" + taken + "\" is already in the library. " +
                            "Choose another name (or edit that tool in the Tool library).");
                        return;
                    }
                }
            }

            Tool t = new Tool();
            t.Type = toolType;
            t.Diameter = (double)numDiameter.Value;
            t.ToolMaterial = cboToolMaterial.SelectedItem.ToString();

            if (toolType == ToolType.Drill) t.Flutes = 2;               // standard drill
            else if (toolType == ToolType.ThreadTap) t.Flutes = 0;      // not used
            else t.Flutes = (int)numFlutes.Value;

            if (toolType == ToolType.ThreadTap)
            {
                t.Pitch = (double)numPitch.Value;
                t.ThreadDiameter = t.Diameter;

                if (t.Pitch >= t.Diameter)
                {
                    MessageBox.Show("The pitch must be smaller than the thread diameter.");
                    return;
                }
            }

            if (toolType == ToolType.ThreadMill)
            {
                t.Pitch = (double)numPitch.Value;
                t.ThreadDiameter = (double)numThreadDia.Value;
                t.Internal = chkInternal.Checked;

                if (t.Pitch >= t.ThreadDiameter)
                {
                    MessageBox.Show("The pitch must be smaller than the thread diameter.");
                    return;
                }

                // An internal thread mill must fit inside the hole
                if (t.Internal)
                {
                    double minor = t.ThreadDiameter - 1.0825 * t.Pitch;
                    if (t.Diameter >= minor)
                    {
                        MessageBox.Show("The cutter is too big for this internal thread. " +
                            "It must be smaller than the minor diameter (" +
                            minor.ToString("0.00") + " mm).");
                        return;
                    }
                }
            }

            ResultTool = t;
            LibraryName = libName;
            this.DialogResult = DialogResult.OK;   // closes the window
        }
    }
}