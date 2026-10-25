using System;
using System.Windows.Forms;

namespace Millmaster
{
    public partial class ToolSelectForm : Form
    {
        // The main form reads this after the window closes
        public ToolType SelectedTool { get; private set; }

        public ToolSelectForm()
        {
            InitializeComponent();

            // One list entry for every tool type
            foreach (ToolType t in Enum.GetValues(typeof(ToolType)))
            {
                lstTools.Items.Add(t.DisplayName());
            }
        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            if (lstTools.SelectedItem == null)
            {
                MessageBox.Show("Please select a cutting tool.");
                return;
            }

            SelectedTool = ToolTypeInfo.FromName(lstTools.SelectedItem.ToString());
            this.DialogResult = DialogResult.OK;
        }
    }
}