using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Millmaster
{
    public partial class ToolLibraryForm : Form
    {
        private List<LibraryTool> tools;
        private bool rebinding = false;   // true while the table is being refreshed

        // The main form reads these when you choose "Use selected"
        public Tool SelectedTool { get; private set; }
        public string SelectedLabel { get; private set; } = "";

        public ToolLibraryForm()
        {
            InitializeComponent();

            dgvTools.ReadOnly = true;
            dgvTools.AllowUserToAddRows = false;
            dgvTools.MultiSelect = false;
            dgvTools.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTools.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTools.RowHeadersVisible = false;

            txtPreview.ReadOnly = true;
            txtPreview.Multiline = true;

            // Connect all events in code (nothing to double-click in the Designer)
            dgvTools.SelectionChanged += dgvTools_SelectionChanged;
            dgvTools.CellDoubleClick += dgvTools_CellDoubleClick;
            

            tools = ToolLibraryStore.Load();
            ShowTools();
        }

        // Rebuilds the table (sorted by name)
        private void ShowTools()
        {
            tools.Sort((a, b) => string.Compare(a.Label, b.Label, StringComparison.OrdinalIgnoreCase));

            rebinding = true;
            dgvTools.DataSource = null;
            dgvTools.DataSource = new List<LibraryTool>(tools);   // a fresh copy each time
            FormatColumns();
            rebinding = false;

            ShowPreview();
        }

        // Detach the table before the list is changed
        private void Detach()
        {
            rebinding = true;
            dgvTools.DataSource = null;
            rebinding = false;
        }

        private void FormatColumns()
        {
            string[] visible = { "Label", "ToolName", "ToolMaterial", "Diameter", "Flutes", "Pitch" };

            foreach (DataGridViewColumn col in dgvTools.Columns)
            {
                col.Visible = Array.IndexOf(visible, col.Name) >= 0;
            }

            SetHeader("Label", "Name");
            SetHeader("ToolName", "Type");
            SetHeader("ToolMaterial", "Tool material");
            SetHeader("Diameter", "Diameter (mm)");
            SetHeader("Pitch", "Pitch (mm)");
        }

        private void SetHeader(string name, string header)
        {
            DataGridViewColumn col = dgvTools.Columns[name];
            if (col != null) col.HeaderText = header;
        }

        private LibraryTool GetSelected()
        {
            if (dgvTools.CurrentRow == null) return null;

            int index = dgvTools.CurrentRow.Index;
            if (index < 0 || index >= tools.Count) return null;

            return tools[index];
        }

        // Highlights a tool in the table
        private void SelectEntry(LibraryTool entry)
        {
            int index = tools.IndexOf(entry);
            if (index >= 0 && index < dgvTools.Rows.Count)
            {
                dgvTools.CurrentCell = dgvTools.Rows[index].Cells["Label"];
            }
        }

        // The names of all the tools except one (so you can keep a tool's own name when editing)
        private List<string> NamesExcept(LibraryTool skip)
        {
            List<string> names = new List<string>();
            foreach (LibraryTool t in tools)
            {
                if (t != skip) names.Add(t.Label);
            }
            return names;
        }

        private void dgvTools_SelectionChanged(object sender, EventArgs e)
        {
            if (rebinding) return;
            ShowPreview();
        }

        private void dgvTools_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UseSelected();
            }
        }

        private void ShowPreview()
        {
            LibraryTool entry = GetSelected();
            if (entry == null)
            {
                txtPreview.Text = "";
                picPreview.Image = null;
                return;
            }

            Tool tool = entry.ToTool();
            txtPreview.Text = entry.Label + Environment.NewLine + Environment.NewLine +
                              Describer.DescribeTool(tool);
            picPreview.Image = Properties.Resources.ResourceManager.GetObject(tool.Type.ResourceName()) as Image;
        }

        private void UseSelected()
        {
            LibraryTool entry = GetSelected();
            if (entry == null)
            {
                MessageBox.Show("Select a tool first.");
                return;
            }

            SelectedTool = entry.ToTool();
            SelectedLabel = entry.Label;
            this.DialogResult = DialogResult.OK;   // closes the window
        }

        private void btnUseTool_Click(object sender, EventArgs e)
        {
            UseSelected();
        }

        private void btnNewTool_Click(object sender, EventArgs e)
        {
            // Step 1: which type?
            ToolSelectForm selectDlg = new ToolSelectForm();
            if (selectDlg.ShowDialog() != DialogResult.OK) return;

            // Step 2: details and a name
            ToolSpecForm specDlg = new ToolSpecForm(selectDlg.SelectedTool);
            specDlg.SetLibraryMode();
            specDlg.TakenNames = NamesExcept(null);
            if (specDlg.ShowDialog() != DialogResult.OK) return;

            Detach();
            LibraryTool entry = LibraryTool.FromTool(specDlg.LibraryName, specDlg.ResultTool);
            tools.Add(entry);
            ToolLibraryStore.Save(tools);
            ActivityLog.Add("Tool added to library: " + entry.Label, LogLevel.Success);

            ShowTools();
            SelectEntry(entry);
        }

        private void btnEditTool_Click(object sender, EventArgs e)
        {
            LibraryTool entry = GetSelected();
            if (entry == null)
            {
                MessageBox.Show("Select a tool first.");
                return;
            }

            ToolType type = ToolTypeInfo.FromName(entry.ToolName);

            ToolSpecForm specDlg = new ToolSpecForm(type, entry.ToTool(), entry.Label);
            specDlg.SetLibraryMode();
            specDlg.TakenNames = NamesExcept(entry);     // its own name is allowed
            if (specDlg.ShowDialog() != DialogResult.OK) return;

            Detach();
            entry.UpdateFrom(specDlg.LibraryName, specDlg.ResultTool);
            ToolLibraryStore.Save(tools);
            ActivityLog.Add("Tool edited: " + entry.Label, LogLevel.Success);

            ShowTools();
            SelectEntry(entry);
        }

        private void btnDeleteTool_Click(object sender, EventArgs e)
        {
            LibraryTool entry = GetSelected();
            if (entry == null)
            {
                MessageBox.Show("Select a tool first.");
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Delete " + entry.Label + "?", "Millmaster", MessageBoxButtons.YesNo);
            if (answer != DialogResult.Yes) return;

            Detach();
            tools.Remove(entry);
            ToolLibraryStore.Save(tools);
            ActivityLog.Add("Tool deleted from library: " + entry.Label, LogLevel.Warning);

            ShowTools();
        }
    }
}