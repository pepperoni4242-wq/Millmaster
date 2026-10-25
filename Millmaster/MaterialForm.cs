using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Millmaster
{
    public partial class MaterialForm : Form
    {
        private List<Material> materials;

        // The material currently being edited (null = creating a new one)
        private Material editingMaterial;

        // The main form reads this after the window closes
        public Material SelectedMaterial { get; private set; }

        public MaterialForm()
        {
            InitializeComponent();

            // Set up the table
            lstMaterials.View = View.Details;
            lstMaterials.FullRowSelect = true;
            lstMaterials.MultiSelect = false;
            lstMaterials.HideSelection = false;
            lstMaterials.GridLines = true;
            lstMaterials.Columns.Add("Name", 150);
            lstMaterials.Columns.Add("Hardness (HB)", 95);
            lstMaterials.Columns.Add("Vc carbide", 80);
            lstMaterials.Columns.Add("Vc HSS", 70);
            lstMaterials.Columns.Add("Feed factor", 85);

            materials = MaterialStore.Load();
            RefreshList(null);
            StartNew();
        }

        // Rebuilds the table. If "select" is given, that row is highlighted.
        private void RefreshList(Material select)
        {
            lstMaterials.Items.Clear();
            foreach (Material m in materials)
            {
                ListViewItem item = new ListViewItem(m.Name);
                item.SubItems.Add(m.Hardness.ToString("0"));
                item.SubItems.Add(m.VcCarbide.ToString("0"));
                item.SubItems.Add(m.VcHss.ToString("0"));
                item.SubItems.Add(m.FeedFactor.ToString("0.00"));
                item.Tag = m;
                lstMaterials.Items.Add(item);

                if (m == select)
                {
                    item.Selected = true;
                }
            }
        }

        // Empties the boxes so you can enter a new material
        private void StartNew()
        {
            lstMaterials.SelectedItems.Clear();
            editingMaterial = null;

            txtName.Clear();
            numHardness.Value = 100;
            numVcCarbide.Value = 100;
            numVcHss.Value = 30;
            numFeedFactor.Value = 1.0m;

            lblMode.Text = "New material";
            txtName.Focus();
        }

        // The selected material in the table (or null)
        private Material GetSelected()
        {
            if (lstMaterials.SelectedItems.Count == 0) return null;
            return lstMaterials.SelectedItems[0].Tag as Material;
        }

        // Clicking a row loads that material into the boxes for editing
        private void lstMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            Material m = GetSelected();
            if (m == null) return;

            editingMaterial = m;
            txtName.Text = m.Name;
            numHardness.Value = (decimal)m.Hardness;
            numVcCarbide.Value = (decimal)m.VcCarbide;
            numVcHss.Value = (decimal)m.VcHss;
            numFeedFactor.Value = (decimal)m.FeedFactor;

            lblMode.Text = "Editing: " + m.Name;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            StartNew();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Please enter a material name.");
                return;
            }

            // The name must not belong to a different material
            foreach (Material m in materials)
            {
                if (m != editingMaterial &&
                    string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Another material already uses this name.");
                    return;
                }
            }

            // Edit the selected material, or create a new one
            bool isNew = (editingMaterial == null);     // add BEFORE "Material target = editingMaterial;"
            Material target = editingMaterial;
            if (target == null)
            {
                target = new Material();
                materials.Add(target);
            }

            target.Name = name;
            target.Hardness = (double)numHardness.Value;
            target.VcCarbide = (double)numVcCarbide.Value;
            target.VcHss = (double)numVcHss.Value;
            target.FeedFactor = (double)numFeedFactor.Value;

            MaterialStore.Save(materials);
            // add right AFTER "MaterialStore.Save(materials);"
            if (isNew)
            {
                ActivityLog.Add("Material added: " + name, LogLevel.Success);
            }
            else
            {
                ActivityLog.Add("Material edited: " + name, LogLevel.Success);
            }
            RefreshList(target);
            MessageBox.Show("Material saved.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Material m = GetSelected();
            if (m == null)
            {
                MessageBox.Show("Select a material in the list first.");
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Delete " + m.Name + "?", "Millmaster", MessageBoxButtons.YesNo);
            if (answer != DialogResult.Yes) return;

            materials.Remove(m);
            ActivityLog.Add("Material deleted: " + m.Name, LogLevel.Warning);
            MaterialStore.Save(materials);
            RefreshList(null);
            StartNew();
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            Material m = GetSelected();
            if (m == null)
            {
                MessageBox.Show("Select a material in the list first.");
                return;
            }

            SelectedMaterial = m;
            this.DialogResult = DialogResult.OK;   // closes the window
        }
    }
}