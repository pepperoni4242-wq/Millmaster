using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Millmaster
{
    public partial class LogForm : Form
    {
        private List<LogEntry> entries;
        private bool rebinding = false;   // true while the table is being refreshed

        // The main form reads this when you choose Load
        public LogEntry SelectedEntry { get; private set; }

        public LogForm()
        {
            InitializeComponent();

            dgvLog.ReadOnly = true;
            dgvLog.AllowUserToAddRows = false;
            dgvLog.MultiSelect = false;
            dgvLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLog.RowHeadersVisible = false;

            txtPreview.ReadOnly = true;
            txtPreview.Multiline = true;

            // Connect events in code
            dgvLog.SelectionChanged += dgvLog_SelectionChanged;
            dgvLog.CellDoubleClick += dgvLog_CellDoubleClick;

            entries = LogStore.Load();
            ShowEntries();
        }

        private void ShowEntries()
        {
            rebinding = true;
            dgvLog.DataSource = null;
            dgvLog.DataSource = new List<LogEntry>(entries);
            FormatColumns();
            rebinding = false;

            ShowPreview();
        }

        // The table shows the main columns; the full details are in the preview
        private void FormatColumns()
        {
            string[] visible = { "Name", "Date", "ToolName", "ToolMaterial", "Diameter",
                                 "MaterialName", "Operation", "Rpm", "FeedRate" };

            foreach (DataGridViewColumn col in dgvLog.Columns)
            {
                col.Visible = Array.IndexOf(visible, col.Name) >= 0;
            }

            SetColumn("ToolName", "Tool", null);
            SetColumn("ToolMaterial", "Tool material", null);
            SetColumn("Diameter", "Diameter (mm)", null);
            SetColumn("MaterialName", "Material", null);
            SetColumn("Rpm", "RPM", "0");
            SetColumn("FeedRate", "Feed (mm/min)", "0");
        }

        private void SetColumn(string name, string header, string format)
        {
            DataGridViewColumn col = dgvLog.Columns[name];
            if (col == null) return;

            col.HeaderText = header;
            if (format != null)
            {
                col.DefaultCellStyle.Format = format;
            }
        }

        private LogEntry GetSelected()
        {
            if (dgvLog.CurrentRow == null) return null;

            // Use the row number instead of DataBoundItem (it is safe while the list changes)
            int index = dgvLog.CurrentRow.Index;
            if (index < 0 || index >= entries.Count) return null;

            return entries[index];
        }

        private void dgvLog_SelectionChanged(object sender, EventArgs e)
        {
            if (rebinding) return;
            ShowPreview();
        }

        private void dgvLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadSelected();
            }
        }

        // Shows everything about the selected entry
        private void ShowPreview()
        {
            LogEntry entry = GetSelected();
            if (entry == null)
            {
                txtPreview.Text = "";
                return;
            }

            string nl = Environment.NewLine;
            Tool tool = entry.ToTool();

            string text = entry.Name + "   (" + entry.Date + ")" + nl + nl +
                          Describer.DescribeTool(tool) + nl + nl;

            if (entry.Version >= 2)
            {
                text += Describer.DescribeMaterial(entry.ToMaterial());
            }
            else
            {
                text += "Material: " + entry.MaterialName + nl +
                        "(specifications were not saved in this older entry)";
            }

            text += nl + nl + Describer.DescribeResult(tool, entry.ToResult(), entry.Operation);
            txtPreview.Text = text;
        }

        private void LoadSelected()
        {
            LogEntry entry = GetSelected();
            if (entry == null)
            {
                MessageBox.Show("Select a row first.");
                return;
            }

            SelectedEntry = entry;
            this.DialogResult = DialogResult.OK;   // closes the window
        }

        private void btnLoadLog_Click(object sender, EventArgs e)
        {
            LoadSelected();
        }

        private void btnDeleteLog_Click(object sender, EventArgs e)
        {
            LogEntry entry = GetSelected();
            if (entry == null)
            {
                MessageBox.Show("Select a row first.");
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Delete " + entry.Name + "?", "Millmaster", MessageBoxButtons.YesNo);
            if (answer != DialogResult.Yes) return;

            // Detach the table before changing the list
            rebinding = true;
            dgvLog.DataSource = null;
            rebinding = false;

            entries.Remove(entry);
            LogStore.Save(entries);
            ActivityLog.Add("Log entry deleted: " + entry.Name, LogLevel.Warning);

            ShowEntries();
        }
    }
}