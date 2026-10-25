namespace Millmaster
{
    partial class DataTransferForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTransferForm));
            label1 = new Label();
            chkMaterials = new CheckBox();
            chkTools = new CheckBox();
            chkSettings = new CheckBox();
            chkLog = new CheckBox();
            groupBox1 = new GroupBox();
            rbReplace = new RadioButton();
            rbMerge = new RadioButton();
            btnExportData = new Button();
            btnImportData = new Button();
            btnCloseData = new Button();
            label2 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 7);
            label1.Name = "label1";
            label1.Size = new Size(183, 15);
            label1.TabIndex = 0;
            label1.Text = "Choose what to export or import:";
            // 
            // chkMaterials
            // 
            chkMaterials.AutoSize = true;
            chkMaterials.Checked = true;
            chkMaterials.CheckState = CheckState.Checked;
            chkMaterials.Location = new Point(12, 28);
            chkMaterials.Name = "chkMaterials";
            chkMaterials.Size = new Size(74, 19);
            chkMaterials.TabIndex = 1;
            chkMaterials.Text = "Materials";
            chkMaterials.UseVisualStyleBackColor = true;
            // 
            // chkTools
            // 
            chkTools.AutoSize = true;
            chkTools.Checked = true;
            chkTools.CheckState = CheckState.Checked;
            chkTools.Location = new Point(12, 53);
            chkTools.Name = "chkTools";
            chkTools.Size = new Size(53, 19);
            chkTools.TabIndex = 2;
            chkTools.Text = "Tools";
            chkTools.UseVisualStyleBackColor = true;
            // 
            // chkSettings
            // 
            chkSettings.AutoSize = true;
            chkSettings.Checked = true;
            chkSettings.CheckState = CheckState.Checked;
            chkSettings.Location = new Point(12, 78);
            chkSettings.Name = "chkSettings";
            chkSettings.Size = new Size(68, 19);
            chkSettings.TabIndex = 3;
            chkSettings.Text = "Settings";
            chkSettings.UseVisualStyleBackColor = true;
            // 
            // chkLog
            // 
            chkLog.AutoSize = true;
            chkLog.Checked = true;
            chkLog.CheckState = CheckState.Checked;
            chkLog.Location = new Point(12, 103);
            chkLog.Name = "chkLog";
            chkLog.Size = new Size(51, 19);
            chkLog.TabIndex = 4;
            chkLog.Text = "Logs";
            chkLog.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbReplace);
            groupBox1.Controls.Add(rbMerge);
            groupBox1.Location = new Point(196, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(387, 110);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "When importing";
            // 
            // rbReplace
            // 
            rbReplace.AutoSize = true;
            rbReplace.Location = new Point(6, 47);
            rbReplace.Name = "rbReplace";
            rbReplace.Size = new Size(280, 19);
            rbReplace.TabIndex = 1;
            rbReplace.TabStop = true;
            rbReplace.Text = "Replace: replace my data with the data in the file";
            rbReplace.UseVisualStyleBackColor = true;
            // 
            // rbMerge
            // 
            rbMerge.AutoSize = true;
            rbMerge.Location = new Point(6, 22);
            rbMerge.Name = "rbMerge";
            rbMerge.Size = new Size(339, 19);
            rbMerge.TabIndex = 0;
            rbMerge.TabStop = true;
            rbMerge.Text = "Merge: add new items, keep mine if the name already exists";
            rbMerge.UseVisualStyleBackColor = true;
            // 
            // btnExportData
            // 
            btnExportData.Location = new Point(264, 128);
            btnExportData.Name = "btnExportData";
            btnExportData.Size = new Size(107, 23);
            btnExportData.TabIndex = 6;
            btnExportData.Text = "Export to file...";
            btnExportData.UseVisualStyleBackColor = true;
            btnExportData.Click += btnExportData_Click;
            // 
            // btnImportData
            // 
            btnImportData.Location = new Point(377, 128);
            btnImportData.Name = "btnImportData";
            btnImportData.Size = new Size(125, 23);
            btnImportData.TabIndex = 7;
            btnImportData.Text = "Import from file...";
            btnImportData.UseVisualStyleBackColor = true;
            btnImportData.Click += btnImportData_Click;
            // 
            // btnCloseData
            // 
            btnCloseData.DialogResult = DialogResult.Cancel;
            btnCloseData.Location = new Point(508, 128);
            btnCloseData.Name = "btnCloseData";
            btnCloseData.Size = new Size(75, 23);
            btnCloseData.TabIndex = 8;
            btnCloseData.Text = "Close";
            btnCloseData.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 163);
            label2.Name = "label2";
            label2.Size = new Size(522, 15);
            label2.TabIndex = 9;
            label2.Text = "Before every import, your current data is saved automatically in %AppData%\\Millmaster\\backups.";
            // 
            // DataTransferForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 187);
            Controls.Add(label2);
            Controls.Add(btnCloseData);
            Controls.Add(btnImportData);
            Controls.Add(btnExportData);
            Controls.Add(groupBox1);
            Controls.Add(chkLog);
            Controls.Add(chkSettings);
            Controls.Add(chkTools);
            Controls.Add(chkMaterials);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DataTransferForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export / Import data";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckBox chkMaterials;
        private CheckBox chkTools;
        private CheckBox chkSettings;
        private CheckBox chkLog;
        private GroupBox groupBox1;
        private RadioButton rbMerge;
        private RadioButton rbReplace;
        private Button btnExportData;
        private Button btnImportData;
        private Button btnCloseData;
        private Label label2;
    }
}