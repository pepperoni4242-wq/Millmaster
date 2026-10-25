using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Millmaster
{
    public partial class SettingsForm : Form
    {
        // Edited values waiting to be saved (key -> text)
        private Dictionary<string, string> pending = new Dictionary<string, string>();
        private string currentCategory = "";
        private Font boldFont;

        public SettingsForm()
        {
            InitializeComponent();

            boldFont = new Font(dgvSettings.Font, FontStyle.Bold);

            // Start from the current values
            LoadPendingFromSettings();

            // Category list
            foreach (string c in CalcSettings.Categories)
            {
                lstCategories.Items.Add(c);
            }

            // Table
            dgvSettings.AllowUserToAddRows = false;
            dgvSettings.AllowUserToDeleteRows = false;
            dgvSettings.RowHeadersVisible = false;
            dgvSettings.EditMode = DataGridViewEditMode.EditOnEnter;

            dgvSettings.Columns.Add("colKey", "Key");
            dgvSettings.Columns.Add("colName", "Parameter");
            dgvSettings.Columns.Add("colValue", "Value");
            dgvSettings.Columns.Add("colUnit", "Unit");
            dgvSettings.Columns.Add("colDefault", "Default");

            dgvSettings.Columns["colKey"].Visible = false;
            dgvSettings.Columns["colName"].ReadOnly = true;
            dgvSettings.Columns["colName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSettings.Columns["colValue"].Width = 100;
            dgvSettings.Columns["colUnit"].ReadOnly = true;
            dgvSettings.Columns["colUnit"].Width = 70;
            dgvSettings.Columns["colDefault"].ReadOnly = true;
            dgvSettings.Columns["colDefault"].Width = 90;
            dgvSettings.Columns["colValue"].DefaultCellStyle.BackColor = Color.LightYellow;

            // Description box
            txtDescription.ReadOnly = true;
            txtDescription.Multiline = true;

            // Connect events in code (so nothing needs double-clicking in the Designer)
            lstCategories.SelectedIndexChanged += lstCategories_SelectedIndexChanged;
            dgvSettings.SelectionChanged += dgvSettings_SelectionChanged;
            dgvSettings.CellEndEdit += dgvSettings_CellEndEdit;

            lstCategories.SelectedIndex = 0;
        }

        private void LoadPendingFromSettings()
        {
            pending.Clear();
            foreach (SettingInfo s in CalcSettings.All)
            {
                pending[s.Key] = CalcSettings.Get(s.Key).ToString(CultureInfo.InvariantCulture);
            }
        }

        private SettingInfo FindInfo(string key)
        {
            foreach (SettingInfo s in CalcSettings.All)
            {
                if (s.Key == key) return s;
            }
            return null;
        }

        // Copies what is typed in the table into the waiting list
        private void CommitGrid()
        {
            dgvSettings.EndEdit();
            foreach (DataGridViewRow row in dgvSettings.Rows)
            {
                string key = Convert.ToString(row.Cells["colKey"].Value);
                if (key != "")
                {
                    pending[key] = Convert.ToString(row.Cells["colValue"].Value);
                }
            }
        }

        private void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItem == null) return;

            CommitGrid();                       // keep edits from the category we are leaving
            currentCategory = lstCategories.SelectedItem.ToString();
            FillGrid();
        }

        private void FillGrid()
        {
            dgvSettings.Rows.Clear();

            foreach (SettingInfo s in CalcSettings.All)
            {
                if (s.Category != currentCategory) continue;

                int i = dgvSettings.Rows.Add(
                    s.Key,
                    s.Name,
                    pending[s.Key],
                    s.Unit,
                    s.Default.ToString(CultureInfo.InvariantCulture));
                MarkIfChanged(dgvSettings.Rows[i]);
            }

            if (dgvSettings.Rows.Count > 0)
            {
                dgvSettings.CurrentCell = dgvSettings.Rows[0].Cells["colValue"];
            }
            ShowDescription();
        }

        // Bold and darker when the value is not the default
        private void MarkIfChanged(DataGridViewRow row)
        {
            string key = Convert.ToString(row.Cells["colKey"].Value);
            SettingInfo info = FindInfo(key);
            if (info == null) return;

            string text = Convert.ToString(row.Cells["colValue"].Value);
            double v;
            bool changed = true;
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out v))
            {
                changed = Math.Abs(v - info.Default) > 0.0000000001;
            }

            if (changed)
            {
                row.Cells["colValue"].Style.Font = boldFont;
                row.Cells["colValue"].Style.BackColor = Color.Khaki;
            }
            else
            {
                row.Cells["colValue"].Style.Font = null;
                row.Cells["colValue"].Style.BackColor = Color.LightYellow;
            }
        }

        private void dgvSettings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvSettings.Rows[e.RowIndex];
            string key = Convert.ToString(row.Cells["colKey"].Value);
            pending[key] = Convert.ToString(row.Cells["colValue"].Value);
            MarkIfChanged(row);
        }

        private void dgvSettings_SelectionChanged(object sender, EventArgs e)
        {
            ShowDescription();
        }

        // Shows the explanation of the selected parameter
        private void ShowDescription()
        {
            if (dgvSettings.CurrentRow == null)
            {
                txtDescription.Text = "";
                return;
            }

            string key = Convert.ToString(dgvSettings.CurrentRow.Cells["colKey"].Value);
            SettingInfo s = FindInfo(key);
            if (s == null) return;

            string nl = Environment.NewLine;
            txtDescription.Text =
                s.Name + "  (" + s.Unit + ")" + nl + nl +
                s.Description.Replace("\n", nl) + nl + nl +
                "Default: " + s.Default.ToString(CultureInfo.InvariantCulture);
        }

        // Jumps to a setting (used to show where a mistake is)
        private void GoTo(SettingInfo s)
        {
            lstCategories.SelectedItem = s.Category;
            foreach (DataGridViewRow row in dgvSettings.Rows)
            {
                if (Convert.ToString(row.Cells["colKey"].Value) == s.Key)
                {
                    dgvSettings.CurrentCell = row.Cells["colValue"];
                    break;
                }
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            CommitGrid();

            // 1. Check every value (in every category) before saving anything
            foreach (SettingInfo s in CalcSettings.All)
            {
                string text = pending[s.Key];
                double value;

                bool ok = double.TryParse(text, NumberStyles.Float,
                    CultureInfo.InvariantCulture, out value);

                string where = "\"" + s.Name + "\" in " + s.Category;

                if (!ok || value <= 0)
                {
                    MessageBox.Show(where + " must be a number greater than 0.\n" +
                        "Use a dot for decimals, for example 0.85");
                    GoTo(s);
                    return;
                }
                if (s.Key.EndsWith(".Ae") && value > 1.0)
                {
                    MessageBox.Show(where + " cannot be more than 1 (the full diameter).");
                    GoTo(s);
                    return;
                }
                if (s.Key == "Mill.MaxChipThinning" && value < 1.0)
                {
                    MessageBox.Show(where + " must be 1 or more (1 = off).");
                    GoTo(s);
                    return;
                }
                if (s.Key == "ThreadMill.Passes" && value != Math.Floor(value))
                {
                    MessageBox.Show(where + " must be a whole number.");
                    GoTo(s);
                    return;
                }
            }

            // 2. All good: store and save
            foreach (SettingInfo s in CalcSettings.All)
            {
                CalcSettings.Set(s.Key, double.Parse(pending[s.Key], CultureInfo.InvariantCulture));
            }
            CalcSettings.Save();
            ActivityLog.Add("Calculation settings saved.", LogLevel.Success);

            this.DialogResult = DialogResult.OK;   // closes the window
        }

        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show(
                "Reset ALL settings (every category) to their defaults?", "Millmaster", MessageBoxButtons.YesNo);
            if (answer != DialogResult.Yes) return;

            CalcSettings.ResetAll();
            ActivityLog.Add("Calculation settings reset to defaults.", LogLevel.Warning);

            LoadPendingFromSettings();
            FillGrid();
        }
    }
}