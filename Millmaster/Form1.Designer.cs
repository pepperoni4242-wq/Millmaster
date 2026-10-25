namespace Millmaster
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnChooseTool = new Button();
            lblSelectedTool = new Label();
            btnMaterial = new Button();
            lblSelectedMaterial = new Label();
            label1 = new Label();
            numMaxRpm = new NumericUpDown();
            btnCalculate = new Button();
            lblResult = new Label();
            txtLogName = new TextBox();
            btnSaveLog = new Button();
            btnViewLog = new Button();
            label2 = new Label();
            label3 = new Label();
            cboOperation = new ComboBox();
            btnSettings = new Button();
            txtActivity = new RichTextBox();
            lblOperationWarning = new Label();
            btnToolLibrary = new Button();
            btnData = new Button();
            ((System.ComponentModel.ISupportInitialize)numMaxRpm).BeginInit();
            SuspendLayout();
            // 
            // btnChooseTool
            // 
            btnChooseTool.BackColor = Color.FromArgb(255, 255, 192);
            btnChooseTool.BackgroundImageLayout = ImageLayout.None;
            btnChooseTool.ForeColor = SystemColors.ActiveCaptionText;
            btnChooseTool.Location = new Point(12, 163);
            btnChooseTool.Name = "btnChooseTool";
            btnChooseTool.Size = new Size(131, 23);
            btnChooseTool.TabIndex = 0;
            btnChooseTool.Text = "Choose cutting tool";
            btnChooseTool.UseVisualStyleBackColor = false;
            btnChooseTool.Click += btnChooseTool_Click;
            // 
            // lblSelectedTool
            // 
            lblSelectedTool.AutoSize = true;
            lblSelectedTool.BackColor = SystemColors.AppWorkspace;
            lblSelectedTool.ForeColor = SystemColors.ActiveCaptionText;
            lblSelectedTool.Location = new Point(12, 16);
            lblSelectedTool.Name = "lblSelectedTool";
            lblSelectedTool.Size = new Size(93, 15);
            lblSelectedTool.TabIndex = 1;
            lblSelectedTool.Text = "No tool selected";
            lblSelectedTool.Click += lblSelectedTool_Click;
            // 
            // btnMaterial
            // 
            btnMaterial.BackColor = Color.FromArgb(255, 255, 192);
            btnMaterial.ForeColor = SystemColors.ActiveCaptionText;
            btnMaterial.Location = new Point(260, 163);
            btnMaterial.Name = "btnMaterial";
            btnMaterial.Size = new Size(131, 23);
            btnMaterial.TabIndex = 2;
            btnMaterial.Text = "Material";
            btnMaterial.UseVisualStyleBackColor = false;
            btnMaterial.Click += btnMaterial_Click;
            // 
            // lblSelectedMaterial
            // 
            lblSelectedMaterial.AutoSize = true;
            lblSelectedMaterial.BackColor = SystemColors.AppWorkspace;
            lblSelectedMaterial.ForeColor = SystemColors.ActiveCaptionText;
            lblSelectedMaterial.Location = new Point(260, 16);
            lblSelectedMaterial.Name = "lblSelectedMaterial";
            lblSelectedMaterial.Size = new Size(115, 15);
            lblSelectedMaterial.TabIndex = 3;
            lblSelectedMaterial.Text = "No material selected";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(18, 194);
            label1.Name = "label1";
            label1.Size = new Size(107, 15);
            label1.TabIndex = 4;
            label1.Text = "Machine max RPM";
            // 
            // numMaxRpm
            // 
            numMaxRpm.Location = new Point(131, 192);
            numMaxRpm.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numMaxRpm.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxRpm.Name = "numMaxRpm";
            numMaxRpm.Size = new Size(120, 23);
            numMaxRpm.TabIndex = 5;
            numMaxRpm.Value = new decimal(new int[] { 10000, 0, 0, 0 });
            // 
            // btnCalculate
            // 
            btnCalculate.ForeColor = SystemColors.ActiveCaptionText;
            btnCalculate.Location = new Point(377, 405);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(75, 23);
            btnCalculate.TabIndex = 6;
            btnCalculate.Text = "Calculate";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.ForeColor = SystemColors.ActiveCaptionText;
            lblResult.Location = new Point(18, 250);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(130, 15);
            lblResult.TabIndex = 7;
            lblResult.Text = "Results will appear here";
            // 
            // txtLogName
            // 
            txtLogName.Location = new Point(82, 434);
            txtLogName.Name = "txtLogName";
            txtLogName.Size = new Size(100, 23);
            txtLogName.TabIndex = 8;
            // 
            // btnSaveLog
            // 
            btnSaveLog.ForeColor = SystemColors.ActiveCaptionText;
            btnSaveLog.Location = new Point(188, 433);
            btnSaveLog.Name = "btnSaveLog";
            btnSaveLog.Size = new Size(75, 23);
            btnSaveLog.TabIndex = 9;
            btnSaveLog.Text = "Save to log";
            btnSaveLog.UseVisualStyleBackColor = true;
            btnSaveLog.Click += btnSaveLog_Click;
            // 
            // btnViewLog
            // 
            btnViewLog.ForeColor = SystemColors.ActiveCaptionText;
            btnViewLog.Location = new Point(377, 434);
            btnViewLog.Name = "btnViewLog";
            btnViewLog.Size = new Size(75, 23);
            btnViewLog.TabIndex = 10;
            btnViewLog.Text = "View log";
            btnViewLog.UseVisualStyleBackColor = true;
            btnViewLog.Click += btnViewLog_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(12, 438);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 11;
            label2.Text = "Tool Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(331, 298);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 12;
            label3.Text = "Operation";
            // 
            // cboOperation
            // 
            cboOperation.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOperation.FormattingEnabled = true;
            cboOperation.Location = new Point(331, 316);
            cboOperation.Name = "cboOperation";
            cboOperation.Size = new Size(121, 23);
            cboOperation.TabIndex = 13;
            cboOperation.SelectedIndexChanged += cboOperation_SelectedIndexChanged;
            // 
            // btnSettings
            // 
            btnSettings.ForeColor = SystemColors.ActiveCaptionText;
            btnSettings.Location = new Point(377, 376);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(75, 23);
            btnSettings.TabIndex = 14;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // txtActivity
            // 
            txtActivity.BackColor = Color.White;
            txtActivity.DetectUrls = false;
            txtActivity.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtActivity.Location = new Point(458, 8);
            txtActivity.Name = "txtActivity";
            txtActivity.ReadOnly = true;
            txtActivity.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtActivity.Size = new Size(300, 449);
            txtActivity.TabIndex = 15;
            txtActivity.Text = "";
            // 
            // lblOperationWarning
            // 
            lblOperationWarning.AutoSize = true;
            lblOperationWarning.ForeColor = Color.FromArgb(255, 128, 0);
            lblOperationWarning.Location = new Point(18, 224);
            lblOperationWarning.Name = "lblOperationWarning";
            lblOperationWarning.Size = new Size(69, 15);
            lblOperationWarning.TabIndex = 16;
            lblOperationWarning.Text = "No warning";
            lblOperationWarning.Visible = false;
            // 
            // btnToolLibrary
            // 
            btnToolLibrary.ForeColor = Color.Black;
            btnToolLibrary.Location = new Point(149, 163);
            btnToolLibrary.Name = "btnToolLibrary";
            btnToolLibrary.Size = new Size(75, 23);
            btnToolLibrary.TabIndex = 17;
            btnToolLibrary.Text = "Tool library";
            btnToolLibrary.UseVisualStyleBackColor = true;
            btnToolLibrary.Click += btnToolLibrary_Click;
            // 
            // btnData
            // 
            btnData.ForeColor = Color.Black;
            btnData.Location = new Point(331, 347);
            btnData.Name = "btnData";
            btnData.Size = new Size(121, 23);
            btnData.TabIndex = 18;
            btnData.Text = "Export / Import";
            btnData.UseVisualStyleBackColor = true;
            btnData.Click += btnData_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(764, 462);
            Controls.Add(btnData);
            Controls.Add(btnToolLibrary);
            Controls.Add(lblOperationWarning);
            Controls.Add(txtActivity);
            Controls.Add(btnSettings);
            Controls.Add(cboOperation);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnViewLog);
            Controls.Add(btnSaveLog);
            Controls.Add(txtLogName);
            Controls.Add(lblResult);
            Controls.Add(btnCalculate);
            Controls.Add(numMaxRpm);
            Controls.Add(label1);
            Controls.Add(lblSelectedMaterial);
            Controls.Add(btnMaterial);
            Controls.Add(lblSelectedTool);
            Controls.Add(btnChooseTool);
            ForeColor = SystemColors.Control;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Millmaster";
            ((System.ComponentModel.ISupportInitialize)numMaxRpm).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnChooseTool;
        private Label lblSelectedTool;
        private Button btnMaterial;
        private Label lblSelectedMaterial;
        private Label label1;
        private NumericUpDown numMaxRpm;
        private Button btnCalculate;
        private Label lblResult;
        private TextBox txtLogName;
        private Button btnSaveLog;
        private Button btnViewLog;
        private Label label2;
        private Label label3;
        private ComboBox cboOperation;
        private Button btnSettings;
        private RichTextBox txtActivity;
        private Label lblOperationWarning;
        private Button btnToolLibrary;
        private Button btnData;
    }
}
