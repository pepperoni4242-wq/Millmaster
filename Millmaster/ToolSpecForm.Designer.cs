namespace Millmaster
{
    partial class ToolSpecForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolSpecForm));
            lblToolType = new Label();
            lblDiameter = new Label();
            numDiameter = new NumericUpDown();
            lblFlutes = new Label();
            numFlutes = new NumericUpDown();
            lblToolMaterial = new Label();
            cboToolMaterial = new ComboBox();
            btnOk = new Button();
            btnCancel = new Button();
            picTool = new PictureBox();
            lblThreadDia = new Label();
            numThreadDia = new NumericUpDown();
            lblPitch = new Label();
            numPitch = new NumericUpDown();
            chkInternal = new CheckBox();
            lblLibraryName = new Label();
            txtLibraryName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numDiameter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFlutes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picTool).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numThreadDia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPitch).BeginInit();
            SuspendLayout();
            // 
            // lblToolType
            // 
            lblToolType.AutoSize = true;
            lblToolType.Location = new Point(18, 11);
            lblToolType.Name = "lblToolType";
            lblToolType.Size = new Size(38, 15);
            lblToolType.TabIndex = 0;
            lblToolType.Text = "label1";
            // 
            // lblDiameter
            // 
            lblDiameter.AutoSize = true;
            lblDiameter.Location = new Point(19, 45);
            lblDiameter.Name = "lblDiameter";
            lblDiameter.Size = new Size(88, 15);
            lblDiameter.TabIndex = 1;
            lblDiameter.Text = "Diameter (mm)";
            // 
            // numDiameter
            // 
            numDiameter.DecimalPlaces = 1;
            numDiameter.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numDiameter.Location = new Point(152, 43);
            numDiameter.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numDiameter.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numDiameter.Name = "numDiameter";
            numDiameter.Size = new Size(120, 23);
            numDiameter.TabIndex = 2;
            numDiameter.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblFlutes
            // 
            lblFlutes.AutoSize = true;
            lblFlutes.Location = new Point(19, 88);
            lblFlutes.Name = "lblFlutes";
            lblFlutes.Size = new Size(38, 15);
            lblFlutes.TabIndex = 3;
            lblFlutes.Text = "Flutes";
            // 
            // numFlutes
            // 
            numFlutes.Location = new Point(152, 86);
            numFlutes.Name = "numFlutes";
            numFlutes.Size = new Size(120, 23);
            numFlutes.TabIndex = 4;
            numFlutes.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // lblToolMaterial
            // 
            lblToolMaterial.AutoSize = true;
            lblToolMaterial.Location = new Point(18, 131);
            lblToolMaterial.Name = "lblToolMaterial";
            lblToolMaterial.Size = new Size(75, 15);
            lblToolMaterial.TabIndex = 5;
            lblToolMaterial.Text = "Tool material";
            // 
            // cboToolMaterial
            // 
            cboToolMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
            cboToolMaterial.FormattingEnabled = true;
            cboToolMaterial.Location = new Point(151, 131);
            cboToolMaterial.Name = "cboToolMaterial";
            cboToolMaterial.Size = new Size(121, 23);
            cboToolMaterial.TabIndex = 6;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(116, 288);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 7;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(197, 288);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // picTool
            // 
            picTool.BorderStyle = BorderStyle.FixedSingle;
            picTool.Location = new Point(286, 16);
            picTool.Name = "picTool";
            picTool.Size = new Size(211, 295);
            picTool.SizeMode = PictureBoxSizeMode.Zoom;
            picTool.TabIndex = 9;
            picTool.TabStop = false;
            // 
            // lblThreadDia
            // 
            lblThreadDia.AutoSize = true;
            lblThreadDia.Location = new Point(19, 166);
            lblThreadDia.Name = "lblThreadDia";
            lblThreadDia.Size = new Size(126, 15);
            lblThreadDia.TabIndex = 10;
            lblThreadDia.Text = "Thread diameter (mm)";
            // 
            // numThreadDia
            // 
            numThreadDia.DecimalPlaces = 1;
            numThreadDia.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numThreadDia.Location = new Point(18, 184);
            numThreadDia.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numThreadDia.Name = "numThreadDia";
            numThreadDia.Size = new Size(120, 23);
            numThreadDia.TabIndex = 11;
            numThreadDia.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblPitch
            // 
            lblPitch.AutoSize = true;
            lblPitch.Location = new Point(151, 166);
            lblPitch.Name = "lblPitch";
            lblPitch.Size = new Size(106, 15);
            lblPitch.TabIndex = 12;
            lblPitch.Text = "Thread pitch (mm)";
            // 
            // numPitch
            // 
            numPitch.DecimalPlaces = 2;
            numPitch.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numPitch.Location = new Point(151, 184);
            numPitch.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            numPitch.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numPitch.Name = "numPitch";
            numPitch.Size = new Size(120, 23);
            numPitch.TabIndex = 13;
            numPitch.Value = new decimal(new int[] { 15, 0, 0, 65536 });
            // 
            // chkInternal
            // 
            chkInternal.AutoSize = true;
            chkInternal.Checked = true;
            chkInternal.CheckState = CheckState.Checked;
            chkInternal.Location = new Point(151, 213);
            chkInternal.Name = "chkInternal";
            chkInternal.Size = new Size(103, 19);
            chkInternal.TabIndex = 14;
            chkInternal.Text = "Internal thread";
            chkInternal.UseVisualStyleBackColor = true;
            // 
            // lblLibraryName
            // 
            lblLibraryName.AutoSize = true;
            lblLibraryName.Location = new Point(19, 241);
            lblLibraryName.Name = "lblLibraryName";
            lblLibraryName.Size = new Size(88, 15);
            lblLibraryName.TabIndex = 15;
            lblLibraryName.Text = "lblLibraryName";
            // 
            // txtLibraryName
            // 
            txtLibraryName.Location = new Point(19, 259);
            txtLibraryName.Name = "txtLibraryName";
            txtLibraryName.Size = new Size(252, 23);
            txtLibraryName.TabIndex = 16;
            // 
            // ToolSpecForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 320);
            Controls.Add(txtLibraryName);
            Controls.Add(lblLibraryName);
            Controls.Add(chkInternal);
            Controls.Add(numPitch);
            Controls.Add(lblPitch);
            Controls.Add(numThreadDia);
            Controls.Add(lblThreadDia);
            Controls.Add(picTool);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(cboToolMaterial);
            Controls.Add(lblToolMaterial);
            Controls.Add(numFlutes);
            Controls.Add(lblFlutes);
            Controls.Add(numDiameter);
            Controls.Add(lblDiameter);
            Controls.Add(lblToolType);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ToolSpecForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tool details";
            ((System.ComponentModel.ISupportInitialize)numDiameter).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFlutes).EndInit();
            ((System.ComponentModel.ISupportInitialize)picTool).EndInit();
            ((System.ComponentModel.ISupportInitialize)numThreadDia).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPitch).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblToolType;
        private Label lblDiameter;
        private NumericUpDown numDiameter;
        private Label lblFlutes;
        private NumericUpDown numFlutes;
        private Label lblToolMaterial;
        private ComboBox cboToolMaterial;
        private Button btnOk;
        private Button btnCancel;
        private PictureBox picTool;
        private Label lblThreadDia;
        private NumericUpDown numThreadDia;
        private Label lblPitch;
        private NumericUpDown numPitch;
        private CheckBox chkInternal;
        private Label lblLibraryName;
        private TextBox txtLibraryName;
    }
}