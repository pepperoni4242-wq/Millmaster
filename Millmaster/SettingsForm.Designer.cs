namespace Millmaster
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            dgvSettings = new DataGridView();
            btnSaveSettings = new Button();
            btnResetSettings = new Button();
            btnCancelSettings = new Button();
            lstCategories = new ListBox();
            txtDescription = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvSettings).BeginInit();
            SuspendLayout();
            // 
            // dgvSettings
            // 
            dgvSettings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSettings.Location = new Point(322, 12);
            dgvSettings.Name = "dgvSettings";
            dgvSettings.Size = new Size(820, 464);
            dgvSettings.TabIndex = 0;
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.Location = new Point(858, 592);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.Size = new Size(75, 23);
            btnSaveSettings.TabIndex = 1;
            btnSaveSettings.Text = "Save";
            btnSaveSettings.UseVisualStyleBackColor = true;
            btnSaveSettings.Click += btnSaveSettings_Click;
            // 
            // btnResetSettings
            // 
            btnResetSettings.Location = new Point(939, 592);
            btnResetSettings.Name = "btnResetSettings";
            btnResetSettings.Size = new Size(122, 23);
            btnResetSettings.TabIndex = 2;
            btnResetSettings.Text = "Reset to defaults";
            btnResetSettings.UseVisualStyleBackColor = true;
            btnResetSettings.Click += btnResetSettings_Click;
            // 
            // btnCancelSettings
            // 
            btnCancelSettings.DialogResult = DialogResult.Cancel;
            btnCancelSettings.Location = new Point(1067, 592);
            btnCancelSettings.Name = "btnCancelSettings";
            btnCancelSettings.Size = new Size(75, 23);
            btnCancelSettings.TabIndex = 3;
            btnCancelSettings.Text = "Cancel";
            btnCancelSettings.UseVisualStyleBackColor = true;
            // 
            // lstCategories
            // 
            lstCategories.FormattingEnabled = true;
            lstCategories.IntegralHeight = false;
            lstCategories.Location = new Point(12, 30);
            lstCategories.Name = "lstCategories";
            lstCategories.Size = new Size(304, 446);
            lstCategories.TabIndex = 4;
            // 
            // txtDescription
            // 
            txtDescription.BackColor = Color.White;
            txtDescription.Location = new Point(12, 499);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(1130, 87);
            txtDescription.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 6;
            label1.Text = "Categories";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 481);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 7;
            label2.Text = "Description";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1154, 627);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtDescription);
            Controls.Add(lstCategories);
            Controls.Add(btnCancelSettings);
            Controls.Add(btnResetSettings);
            Controls.Add(btnSaveSettings);
            Controls.Add(dgvSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Calculation settings";
            ((System.ComponentModel.ISupportInitialize)dgvSettings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvSettings;
        private Button btnSaveSettings;
        private Button btnResetSettings;
        private Button btnCancelSettings;
        private ListBox lstCategories;
        private TextBox txtDescription;
        private Label label1;
        private Label label2;
    }
}