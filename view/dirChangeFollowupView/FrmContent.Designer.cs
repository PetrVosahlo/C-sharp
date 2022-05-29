namespace changedDirectory {
    partial class FrmContent {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lbDir = new System.Windows.Forms.Label();
            this.tbDir = new System.Windows.Forms.TextBox();
            this.btnDir = new System.Windows.Forms.Button();
            this.btChanges = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.tbChanged = new System.Windows.Forms.TextBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.btEnd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbDir
            // 
            this.lbDir.AutoSize = true;
            this.lbDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDir.Location = new System.Drawing.Point(11, 29);
            this.lbDir.Name = "lbDir";
            this.lbDir.Size = new System.Drawing.Size(132, 20);
            this.lbDir.TabIndex = 0;
            this.lbDir.Text = "Vybraný adresář";
            // 
            // tbDir
            // 
            this.tbDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDir.Location = new System.Drawing.Point(150, 26);
            this.tbDir.Name = "tbDir";
            this.tbDir.Size = new System.Drawing.Size(457, 26);
            this.tbDir.TabIndex = 1;
            // 
            // btnDir
            // 
            this.btnDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDir.Location = new System.Drawing.Point(631, 19);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(172, 40);
            this.btnDir.TabIndex = 2;
            this.btnDir.Text = "Výběr adresáře";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btChanges
            // 
            this.btChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btChanges.Location = new System.Drawing.Point(150, 70);
            this.btChanges.Name = "btChanges";
            this.btChanges.Size = new System.Drawing.Size(172, 40);
            this.btChanges.TabIndex = 3;
            this.btChanges.Text = "Zobraz změny";
            this.btChanges.UseVisualStyleBackColor = true;
            this.btChanges.Click += new System.EventHandler(this.btChanges_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            // 
            // tbChanged
            // 
            this.tbChanged.AcceptsReturn = true;
            this.tbChanged.AcceptsTab = true;
            this.tbChanged.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbChanged.Location = new System.Drawing.Point(15, 116);
            this.tbChanged.Multiline = true;
            this.tbChanged.Name = "tbChanged";
            this.tbChanged.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbChanged.Size = new System.Drawing.Size(905, 322);
            this.tbChanged.TabIndex = 5;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // btEnd
            // 
            this.btEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEnd.Location = new System.Drawing.Point(346, 70);
            this.btEnd.Name = "btEnd";
            this.btEnd.Size = new System.Drawing.Size(172, 40);
            this.btEnd.TabIndex = 6;
            this.btEnd.Text = "Konec programu";
            this.btEnd.UseVisualStyleBackColor = true;
            this.btEnd.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 450);
            this.Controls.Add(this.btEnd);
            this.Controls.Add(this.tbChanged);
            this.Controls.Add(this.btChanges);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.tbDir);
            this.Controls.Add(this.lbDir);
            this.Name = "FrmContent";
            this.Text = "Přehled změn v adresáři";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmContent_FormClosing);
            this.Load += new System.EventHandler(this.FrmContent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDir;
        private System.Windows.Forms.TextBox tbDir;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Button btChanges;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.TextBox tbChanged;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Button btEnd;
    }
}