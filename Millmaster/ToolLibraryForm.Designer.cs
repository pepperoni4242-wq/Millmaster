namespace Millmaster
{
    partial class ToolLibraryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolLibraryForm));
            dgvTools = new DataGridView();
            picPreview = new PictureBox();
            txtPreview = new TextBox();
            btnUseTool = new Button();
            btnNewTool = new Button();
            btnEditTool = new Button();
            btnDeleteTool = new Button();
            btnCloseTool = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTools).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            SuspendLayout();
            // 
            // dgvTools
            // 
            dgvTools.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTools.Location = new Point(12, 12);
            dgvTools.Name = "dgvTools";
            dgvTools.Size = new Size(511, 401);
            dgvTools.TabIndex = 0;
            // 
            // picPreview
            // 
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Location = new Point(529, 12);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(259, 186);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 1;
            picPreview.TabStop = false;
            // 
            // txtPreview
            // 
            txtPreview.BackColor = Color.White;
            txtPreview.Location = new Point(529, 204);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.ScrollBars = ScrollBars.Vertical;
            txtPreview.Size = new Size(259, 209);
            txtPreview.TabIndex = 2;
            // 
            // btnUseTool
            // 
            btnUseTool.Location = new Point(351, 419);
            btnUseTool.Name = "btnUseTool";
            btnUseTool.Size = new Size(109, 23);
            btnUseTool.TabIndex = 3;
            btnUseTool.Text = "Use selected";
            btnUseTool.UseVisualStyleBackColor = true;
            btnUseTool.Click += btnUseTool_Click;
            // 
            // btnNewTool
            // 
            btnNewTool.Location = new Point(466, 419);
            btnNewTool.Name = "btnNewTool";
            btnNewTool.Size = new Size(79, 23);
            btnNewTool.TabIndex = 4;
            btnNewTool.Text = "New tool...";
            btnNewTool.UseVisualStyleBackColor = true;
            btnNewTool.Click += btnNewTool_Click;
            // 
            // btnEditTool
            // 
            btnEditTool.Location = new Point(551, 419);
            btnEditTool.Name = "btnEditTool";
            btnEditTool.Size = new Size(75, 23);
            btnEditTool.TabIndex = 5;
            btnEditTool.Text = "Edit...";
            btnEditTool.UseVisualStyleBackColor = true;
            btnEditTool.Click += btnEditTool_Click;
            // 
            // btnDeleteTool
            // 
            btnDeleteTool.Location = new Point(632, 419);
            btnDeleteTool.Name = "btnDeleteTool";
            btnDeleteTool.Size = new Size(75, 23);
            btnDeleteTool.TabIndex = 6;
            btnDeleteTool.Text = "Delete";
            btnDeleteTool.UseVisualStyleBackColor = true;
            btnDeleteTool.Click += btnDeleteTool_Click;
            // 
            // btnCloseTool
            // 
            btnCloseTool.DialogResult = DialogResult.Cancel;
            btnCloseTool.Location = new Point(713, 419);
            btnCloseTool.Name = "btnCloseTool";
            btnCloseTool.Size = new Size(75, 23);
            btnCloseTool.TabIndex = 7;
            btnCloseTool.Text = "Close";
            btnCloseTool.UseVisualStyleBackColor = true;
            // 
            // ToolLibraryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCloseTool);
            Controls.Add(btnDeleteTool);
            Controls.Add(btnEditTool);
            Controls.Add(btnNewTool);
            Controls.Add(btnUseTool);
            Controls.Add(txtPreview);
            Controls.Add(picPreview);
            Controls.Add(dgvTools);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ToolLibraryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tool library";
            ((System.ComponentModel.ISupportInitialize)dgvTools).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvTools;
        private PictureBox picPreview;
        private TextBox txtPreview;
        private Button btnUseTool;
        private Button btnNewTool;
        private Button btnEditTool;
        private Button btnDeleteTool;
        private Button btnCloseTool;
    }
}