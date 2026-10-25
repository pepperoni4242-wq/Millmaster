using System;
using System.IO;
using System.Windows.Forms;

namespace Millmaster
{
    public partial class DataTransferForm : Form
    {
        // The main form checks this after the window closes
        public bool SettingsChanged { get; private set; }

        public DataTransferForm()
        {
            InitializeComponent();

            rbMerge.Checked = true;
            ShowCounts();
        }

        // Shows how much of each is currently saved
        private void ShowCounts()
        {
            chkMaterials.Text = "Materials (" + MaterialStore.Load().Count + ")";
            chkTools.Text = "Tool library (" + ToolLibraryStore.Load().Count + ")";
            chkSettings.Text = "Calculation settings";
            chkLog.Text = "Saved log (" + LogStore.Load().Count + " entries)";
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            if (!chkMaterials.Checked && !chkTools.Checked && !chkSettings.Checked && !chkLog.Checked)
            {
                MessageBox.Show("Tick at least one item to export.");
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Export Millmaster data";
                dlg.Filter = DataTransfer.FileFilter;
                dlg.FileName = "Millmaster-" + DateTime.Now.ToString("yyyy-MM-dd") + ".millmaster";

                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    DataPackage pkg = DataTransfer.Collect(
                        chkMaterials.Checked, chkTools.Checked, chkSettings.Checked, chkLog.Checked);
                    DataTransfer.Write(dlg.FileName, pkg);

                    ActivityLog.Add("Data exported to " + Path.GetFileName(dlg.FileName), LogLevel.Success);
                    MessageBox.Show("Exported to:" + Environment.NewLine + dlg.FileName, "Export finished");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message);
                }
            }
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Import Millmaster data";
                dlg.Filter = DataTransfer.FileFilter;

                if (dlg.ShowDialog() != DialogResult.OK) return;

                DataPackage pkg;
                try
                {
                    pkg = DataTransfer.Read(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                // Only what is ticked AND really inside the file
                bool materials = chkMaterials.Checked && pkg.HasMaterials;
                bool tools = chkTools.Checked && pkg.HasTools;
                bool settings = chkSettings.Checked && pkg.HasSettings;
                bool log = chkLog.Checked && pkg.HasLog;

                if (!materials && !tools && !settings && !log)
                {
                    MessageBox.Show("There is nothing to import. The file does not contain the items you ticked.");
                    return;
                }

                bool replace = rbReplace.Checked;
                string nl = Environment.NewLine;

                string question = "File made on " + pkg.ExportedAt + " will import:" + nl;
                if (materials) question += "  - Materials: " + pkg.Materials.Count + nl;
                if (tools) question += "  - Tool library: " + pkg.Tools.Count + nl;
                if (settings) question += "  - Calculation settings" + nl;
                if (log) question += "  - Saved log: " + pkg.Log.Count + " entries" + nl;

                if (replace)
                {
                    question += nl + "Mode: REPLACE. Your current data for these items will be replaced.";
                }
                else
                {
                    question += nl + "Mode: MERGE. New items are added, yours are kept.";
                }
                question += nl + "A backup of your current data is saved first." + nl + nl + "Continue?";

                if (MessageBox.Show(question, "Import", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                try
                {
                    string result = DataTransfer.Apply(pkg, materials, tools, settings, log, replace);

                    if (settings) SettingsChanged = true;
                    ActivityLog.Add("Data imported from " + Path.GetFileName(dlg.FileName), LogLevel.Success);

                    ShowCounts();
                    MessageBox.Show(result, "Import finished");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import failed: " + ex.Message);
                }
            }
        }
    }
}