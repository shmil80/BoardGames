namespace mehoniyot
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.Save = new System.Windows.Forms.Label();
            this.domainUpdown1 = new System.Windows.Forms.DomainUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Size = new System.Drawing.Size(267, 272);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Save);
            this.panel1.Controls.Add(this.domainUpdown1);
            this.panel1.Location = new System.Drawing.Point(0, 272);
            this.panel1.Size = new System.Drawing.Size(267, 74);
            this.panel1.Controls.SetChildIndex(this.domainUpdown1, 0);
            this.panel1.Controls.SetChildIndex(this.Save, 0);
            this.panel1.Controls.SetChildIndex(this.button1, 0);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 22);
            this.button1.Size = new System.Drawing.Size(109, 24);
            this.button1.Text = "התחל מהתחלה";
            // 
            // Save
            // 
            this.Save.AutoSize = true;
            this.Save.Location = new System.Drawing.Point(238, 27);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(30, 13);
            this.Save.TabIndex = 4;
            this.Save.Text = "save";
            this.Save.Visible = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // domainUpdown1
            // 
            this.domainUpdown1.Location = new System.Drawing.Point(33, 25);
            this.domainUpdown1.Name = "domainUpdown1";
            this.domainUpdown1.Size = new System.Drawing.Size(44, 20);
            this.domainUpdown1.TabIndex = 8;
            this.domainUpdown1.SelectedItemChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 346);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Text = "מכוניות";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Save;
        private System.Windows.Forms.DomainUpDown domainUpdown1;
    }
}

