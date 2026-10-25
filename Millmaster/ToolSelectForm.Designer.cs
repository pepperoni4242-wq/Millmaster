namespace Millmaster
{
    partial class ToolSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolSelectForm));
            lstTools = new ListBox();
            btnOk_1 = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lstTools
            // 
            lstTools.FormattingEnabled = true;
            lstTools.Location = new Point(12, 12);
            lstTools.Name = "lstTools";
            lstTools.Size = new Size(373, 229);
            lstTools.TabIndex = 0;
            // 
            // btnOk_1
            // 
            btnOk_1.Location = new Point(229, 247);
            btnOk_1.Name = "btnOk_1";
            btnOk_1.Size = new Size(75, 23);
            btnOk_1.TabIndex = 1;
            btnOk_1.Text = "OK";
            btnOk_1.UseVisualStyleBackColor = true;
            btnOk_1.Click += btnOk_Click_1;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(310, 247);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // ToolSelectForm
            // 
            AcceptButton = btnOk_1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(397, 280);
            Controls.Add(btnCancel);
            Controls.Add(btnOk_1);
            Controls.Add(lstTools);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ToolSelectForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Choose cutting tool";
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstTools;
        private Button btnOk_1;
        private Button btnCancel;
    }
}