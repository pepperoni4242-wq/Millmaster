using System.Collections.Generic;
namespace Millmaster

{
    

    public partial class Form1 : Form
    {

        private Material currentMaterial;
        private Tool currentTool;

        private CalcResult currentResult;
        private string lastOperation = "";   // operation used for the current result
        private double lastMaxRpm;           // max RPM used for the current result
        private bool loadingFromLog = false;



        public Form1()
        {
            InitializeComponent();

            // 1. Connect the activity box FIRST
            ActivityLog.MessageAdded += ShowMessage;

            cboOperation.Items.Add("Roughing");
            cboOperation.Items.Add("Finishing");
            cboOperation.Items.Add("Slotting");
            cboOperation.SelectedIndex = 0;

            AddMessage("Millmaster started.");
        }

        // Makes a tool the current tool and shows it
        private void SetCurrentTool(Tool tool)
        {
            currentTool = tool;
            currentResult = null;

            lblSelectedTool.Text = Describer.DescribeTool(currentTool);

            AddMessage("Tool selected: " + currentTool.Name + ", D" + currentTool.Diameter +
                " mm, " + currentTool.ToolMaterial);

            UpdateOperationWarning();
            if (!UsesOperation(currentTool))
            {
                AddMessage(currentTool.Name + " selected: Operation does not affect its parameters.", LogLevel.Warning);
            }
        }

        private void btnChooseTool_Click(object sender, EventArgs e)
        {
            // Window 1: pick the tool type
            ToolSelectForm selectDlg = new ToolSelectForm();
            if (selectDlg.ShowDialog() != DialogResult.OK) return;

            // Window 2: enter the details for that tool type
            ToolSpecForm specDlg = new ToolSpecForm(selectDlg.SelectedTool);
            specDlg.TakenNames = ToolLibraryStore.Names();
            if (specDlg.ShowDialog() != DialogResult.OK) return;

            // Optional: also save it into the library
            if (specDlg.LibraryName != "")
            {
                List<LibraryTool> library = ToolLibraryStore.Load();
                library.Add(LibraryTool.FromTool(specDlg.LibraryName, specDlg.ResultTool));
                ToolLibraryStore.Save(library);
                AddMessage("Tool saved to library: " + specDlg.LibraryName, LogLevel.Success);
            }

            SetCurrentTool(specDlg.ResultTool);
        }

        private void btnToolLibrary_Click(object sender, EventArgs e)
        {
            ToolLibraryForm dlg = new ToolLibraryForm();

            if (dlg.ShowDialog() == DialogResult.OK && dlg.SelectedTool != null)
            {
                SetCurrentTool(dlg.SelectedTool);
                AddMessage("Tool loaded from library: " + dlg.SelectedLabel);
            }
        }

        private void lblSelectedTool_Click(object sender, EventArgs e)
        {

        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            MaterialForm dlg = new MaterialForm();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                currentMaterial = dlg.SelectedMaterial;
                currentResult = null;

                lblSelectedMaterial.Text = Describer.DescribeMaterial(currentMaterial);
                AddMessage("Material selected: " + currentMaterial.Name);
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (currentTool == null)
            {
                AddMessage("Calculate failed: no cutting tool chosen.", LogLevel.Warning);
                MessageBox.Show("Please choose a cutting tool first.");
                return;
            }
            if (currentMaterial == null)
            {
                AddMessage("Calculate failed: no material chosen.", LogLevel.Warning);
                MessageBox.Show("Please choose a material first.");
                return;
            }

            string operation = cboOperation.SelectedItem.ToString();
            double maxRpm = (double)numMaxRpm.Value;

            CalcResult r = Calculator.Calculate(currentTool, currentMaterial, maxRpm, operation);
            currentResult = r;
            lastOperation = operation;     // remembered so the log saves exactly what was used
            lastMaxRpm = maxRpm;

            lblResult.Text = Describer.DescribeResult(currentTool, r, operation);

            string opText = "";
            if (UsesOperation(currentTool))
            {
                opText = ", " + operation;
            }
            AddMessage("Calculated " + currentTool.Name + opText + " in " + currentMaterial.Name +
                ": " + r.Rpm.ToString("0") + " RPM, " + r.FeedRate.ToString("0") + " mm/min", LogLevel.Success);
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            if (currentResult == null || currentTool == null)
            {
                MessageBox.Show("Click Calculate first, then save.");
                return;
            }
            if (currentMaterial == null)
            {
                MessageBox.Show("Please choose a material before saving.");
                return;
            }

            string name = txtLogName.Text.Trim();
            if (name == "")
            {
                name = currentTool.Name + " D" + currentTool.Diameter + " " + currentMaterial.Name;
            }

            LogEntry entry = LogEntry.Create(
                name, currentTool, currentMaterial, currentResult, lastOperation, lastMaxRpm);

            List<LogEntry> entries = LogStore.Load();
            entries.Insert(0, entry);          // newest first
            LogStore.Save(entries);

            txtLogName.Clear();
            MessageBox.Show("Saved to log.");
            AddMessage("Saved to log: " + name, LogLevel.Success);
        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            LogForm dlg = new LogForm();

            if (dlg.ShowDialog() == DialogResult.OK && dlg.SelectedEntry != null)
            {
                LoadFromLog(dlg.SelectedEntry);
            }
            else
            {
                AddMessage("Opened the saved log.");
            }
        }

        // Puts a saved entry back on the main form, without calculating again
        private void LoadFromLog(LogEntry entry)
        {
            // 1. Tool
            currentTool = entry.ToTool();

            // 2. Material: new entries carry a full copy; older ones are looked up by name
            Material mat;
            if (entry.Version >= 2)
            {
                mat = entry.ToMaterial();
            }
            else
            {
                mat = FindSavedMaterial(entry.MaterialName);
            }
            currentMaterial = mat;

            // 3. Operation and machine limit
            loadingFromLog = true;
            if (entry.Operation != "" && cboOperation.Items.Contains(entry.Operation))
            {
                cboOperation.SelectedItem = entry.Operation;
            }
            loadingFromLog = false;

            if (entry.MaxRpm >= (double)numMaxRpm.Minimum && entry.MaxRpm <= (double)numMaxRpm.Maximum)
            {
                numMaxRpm.Value = (decimal)entry.MaxRpm;
            }

            // 4. Results
            currentResult = entry.ToResult();
            lastOperation = entry.Operation;
            lastMaxRpm = entry.MaxRpm;

            // 5. Show everything
            lblSelectedTool.Text = Describer.DescribeTool(currentTool);
            if (currentMaterial != null)
            {
                lblSelectedMaterial.Text = Describer.DescribeMaterial(currentMaterial);
            }
            else
            {
                lblSelectedMaterial.Text = "Material: " + entry.MaterialName + " (choose it again)";
            }
            lblResult.Text = Describer.DescribeResult(currentTool, currentResult, entry.Operation);
            UpdateOperationWarning();

            AddMessage("Loaded from log: " + entry.Name, LogLevel.Success);
            if (currentMaterial == null)
            {
                AddMessage("This older entry has no material specifications. Choose the material again before calculating or saving.", LogLevel.Warning);
            }
        }

        private Material FindSavedMaterial(string name)
        {
            foreach (Material m in MaterialStore.Load())
            {
                if (string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return m;
                }
            }
            return null;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm dlg = new SettingsForm();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Old results were made with the old settings
                currentResult = null;
                lblResult.Text = "Settings changed. Click Calculate again.";
            }
        }

        // Other code calls this to add a line
        private void AddMessage(string text, LogLevel level = LogLevel.Info)
        {
            ActivityLog.Add(text, level);
        }

        // Shows one line in the box, with colors (runs whenever ANY window posts a message)
        private void ShowMessage(DateTime time, LogLevel level, string text)
        {
            Color color;
            if (level == LogLevel.Success) color = Color.ForestGreen;
            else if (level == LogLevel.Warning) color = Color.DarkOrange;
            else color = Color.Black;

            txtActivity.SelectionStart = txtActivity.TextLength;
            txtActivity.SelectionLength = 0;

            txtActivity.SelectionColor = Color.Gray;
            txtActivity.AppendText(time.ToString("HH:mm:ss") + "  ");

            txtActivity.SelectionColor = color;
            txtActivity.AppendText(text + Environment.NewLine);

            txtActivity.SelectionColor = txtActivity.ForeColor;
            txtActivity.ScrollToCaret();
        }

        // Shows or hides the orange warning next to the Operation box
        private void UpdateOperationWarning()
        {
            bool ignored = (currentTool != null && !UsesOperation(currentTool));

            lblOperationWarning.Visible = ignored;
            if (ignored)
            {
                lblOperationWarning.Text = "Warning: Operation does not affect " +
                    currentTool.Name.ToLower() + " parameters.";
            }
        }

        private void cboOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingFromLog) return;

            if (currentTool != null && !UsesOperation(currentTool))
            {
                AddMessage("Operation has no effect on " + currentTool.Name.ToLower() + " parameters.", LogLevel.Warning);
            }
        }

        // Only end mills and face mills use the Operation choice
        private bool UsesOperation(Tool tool)
        {
            return tool.Type.UsesOperation();
        }

        // Builds the tool specification text

        private void btnData_Click(object sender, EventArgs e)
        {
            DataTransferForm dlg = new DataTransferForm();
            dlg.ShowDialog();

            // Imported settings change the numbers, so an old result is no longer valid
            if (dlg.SettingsChanged)
            {
                currentResult = null;
                lblResult.Text = "Settings changed. Click Calculate again.";
            }
        }

    }
}
