namespace Millmaster
{
    partial class MaterialForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialForm));
            txtName = new TextBox();
            numHardness = new NumericUpDown();
            numVcCarbide = new NumericUpDown();
            numVcHss = new NumericUpDown();
            numFeedFactor = new NumericUpDown();
            btnSave = new Button();
            btnDelete = new Button();
            btnUse = new Button();
            btnClose = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lstMaterials = new ListView();
            btnNew = new Button();
            lblMode = new Label();
            ((System.ComponentModel.ISupportInitialize)numHardness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVcCarbide).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVcHss).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFeedFactor).BeginInit();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(532, 27);
            txtName.Name = "txtName";
            txtName.Size = new Size(120, 23);
            txtName.TabIndex = 1;
            // 
            // numHardness
            // 
            numHardness.Location = new Point(532, 71);
            numHardness.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numHardness.Name = "numHardness";
            numHardness.Size = new Size(120, 23);
            numHardness.TabIndex = 2;
            // 
            // numVcCarbide
            // 
            numVcCarbide.Location = new Point(532, 115);
            numVcCarbide.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numVcCarbide.Name = "numVcCarbide";
            numVcCarbide.Size = new Size(120, 23);
            numVcCarbide.TabIndex = 3;
            // 
            // numVcHss
            // 
            numVcHss.Location = new Point(532, 159);
            numVcHss.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numVcHss.Name = "numVcHss";
            numVcHss.Size = new Size(120, 23);
            numVcHss.TabIndex = 4;
            // 
            // numFeedFactor
            // 
            numFeedFactor.DecimalPlaces = 2;
            numFeedFactor.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numFeedFactor.Location = new Point(532, 203);
            numFeedFactor.Maximum = new decimal(new int[] { 15, 0, 0, 65536 });
            numFeedFactor.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numFeedFactor.Name = "numFeedFactor";
            numFeedFactor.Size = new Size(120, 23);
            numFeedFactor.TabIndex = 5;
            numFeedFactor.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnSave
            // 
            btnSave.Location = new Point(370, 232);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save material";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(451, 232);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUse
            // 
            btnUse.Location = new Point(532, 232);
            btnUse.Name = "btnUse";
            btnUse.Size = new Size(93, 23);
            btnUse.TabIndex = 8;
            btnUse.Text = "Use selected";
            btnUse.UseVisualStyleBackColor = true;
            btnUse.Click += btnUse_Click;
            // 
            // btnClose
            // 
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(628, 232);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 9;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(532, 9);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 10;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(532, 53);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 11;
            label2.Text = "Hardness (HB)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(532, 97);
            label3.Name = "label3";
            label3.Size = new Size(171, 15);
            label3.TabIndex = 12;
            label3.Text = "Cutting speed carbide (m/min)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(532, 141);
            label4.Name = "label4";
            label4.Size = new Size(153, 15);
            label4.TabIndex = 13;
            label4.Text = "Cutting speed HSS (m/min)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(532, 185);
            label5.Name = "label5";
            label5.Size = new Size(66, 15);
            label5.TabIndex = 14;
            label5.Text = "Feed factor";
            // 
            // lstMaterials
            // 
            lstMaterials.Location = new Point(12, 12);
            lstMaterials.Name = "lstMaterials";
            lstMaterials.Size = new Size(514, 214);
            lstMaterials.TabIndex = 15;
            lstMaterials.UseCompatibleStateImageBehavior = false;
            lstMaterials.Click += lstMaterials_SelectedIndexChanged;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(257, 232);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(107, 23);
            btnNew.TabIndex = 16;
            btnNew.Text = "New material";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Location = new Point(12, 236);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(38, 15);
            lblMode.TabIndex = 17;
            lblMode.Text = "Mode";
            // 
            // MaterialForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(715, 263);
            Controls.Add(lblMode);
            Controls.Add(btnNew);
            Controls.Add(lstMaterials);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnClose);
            Controls.Add(btnUse);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(numFeedFactor);
            Controls.Add(numVcHss);
            Controls.Add(numVcCarbide);
            Controls.Add(numHardness);
            Controls.Add(txtName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MaterialForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Materials";
            ((System.ComponentModel.ISupportInitialize)numHardness).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVcCarbide).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVcHss).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFeedFactor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtName;
        private NumericUpDown numHardness;
        private NumericUpDown numVcCarbide;
        private NumericUpDown numVcHss;
        private NumericUpDown numFeedFactor;
        private Button btnSave;
        private Button btnDelete;
        private Button btnUse;
        private Button btnClose;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ListView lstMaterials;
        private Button btnNew;
        private Label lblMode;
    }
}