namespace Millmaster
{
    partial class LogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            dgvLog = new DataGridView();
            btnDeleteLog = new Button();
            btnCloseLog = new Button();
            txtPreview = new TextBox();
            btnLoadLog = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLog).BeginInit();
            SuspendLayout();
            // 
            // dgvLog
            // 
            dgvLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLog.Location = new Point(12, 12);
            dgvLog.Name = "dgvLog";
            dgvLog.Size = new Size(962, 328);
            dgvLog.TabIndex = 0;
            // 
            // btnDeleteLog
            // 
            btnDeleteLog.Location = new Point(858, 346);
            btnDeleteLog.Name = "btnDeleteLog";
            btnDeleteLog.Size = new Size(116, 23);
            btnDeleteLog.TabIndex = 1;
            btnDeleteLog.Text = "Delete selected";
            btnDeleteLog.UseVisualStyleBackColor = true;
            btnDeleteLog.Click += btnDeleteLog_Click;
            // 
            // btnCloseLog
            // 
            btnCloseLog.DialogResult = DialogResult.Cancel;
            btnCloseLog.Location = new Point(777, 346);
            btnCloseLog.Name = "btnCloseLog";
            btnCloseLog.Size = new Size(75, 23);
            btnCloseLog.TabIndex = 2;
            btnCloseLog.Text = "Close";
            btnCloseLog.UseVisualStyleBackColor = true;
            // 
            // txtPreview
            // 
            txtPreview.BackColor = Color.White;
            txtPreview.Location = new Point(12, 375);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.ScrollBars = ScrollBars.Vertical;
            txtPreview.Size = new Size(962, 159);
            txtPreview.TabIndex = 3;
            // 
            // btnLoadLog
            // 
            btnLoadLog.Location = new Point(823, 540);
            btnLoadLog.Name = "btnLoadLog";
            btnLoadLog.Size = new Size(151, 23);
            btnLoadLog.TabIndex = 4;
            btnLoadLog.Text = "Load into main form";
            btnLoadLog.UseVisualStyleBackColor = true;
            btnLoadLog.Click += btnLoadLog_Click;
            // 
            // LogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(986, 566);
            Controls.Add(btnLoadLog);
            Controls.Add(txtPreview);
            Controls.Add(btnCloseLog);
            Controls.Add(btnDeleteLog);
            Controls.Add(dgvLog);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Saved log";
            ((System.ComponentModel.ISupportInitialize)dgvLog).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLog;
        private Button btnDeleteLog;
        private Button btnCloseLog;
        private TextBox txtPreview;
        private Button btnLoadLog;
    }
}