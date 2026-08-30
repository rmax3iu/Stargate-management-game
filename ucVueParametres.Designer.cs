namespace SAE24_Stargate
{
    partial class ucVueParametres
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpChangerCouleur = new System.Windows.Forms.GroupBox();
            this.pcbRoueNoirEtBlanc = new System.Windows.Forms.PictureBox();
            this.dgvRoue = new System.Windows.Forms.DataGridView();
            this.pcbRoue = new System.Windows.Forms.PictureBox();
            this.btnEffacerCouleur = new System.Windows.Forms.Button();
            this.grpChangerCouleur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbRoueNoirEtBlanc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbRoue)).BeginInit();
            this.SuspendLayout();
            // 
            // grpChangerCouleur
            // 
            this.grpChangerCouleur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.grpChangerCouleur.Controls.Add(this.pcbRoueNoirEtBlanc);
            this.grpChangerCouleur.Controls.Add(this.dgvRoue);
            this.grpChangerCouleur.Controls.Add(this.pcbRoue);
            this.grpChangerCouleur.Controls.Add(this.btnEffacerCouleur);
            this.grpChangerCouleur.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpChangerCouleur.ForeColor = System.Drawing.Color.White;
            this.grpChangerCouleur.Location = new System.Drawing.Point(43, 99);
            this.grpChangerCouleur.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpChangerCouleur.Name = "grpChangerCouleur";
            this.grpChangerCouleur.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpChangerCouleur.Size = new System.Drawing.Size(2168, 648);
            this.grpChangerCouleur.TabIndex = 0;
            this.grpChangerCouleur.TabStop = false;
            this.grpChangerCouleur.Text = "Changer la couleur du fond";
            // 
            // pcbRoueNoirEtBlanc
            // 
            this.pcbRoueNoirEtBlanc.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imageRoueNoirEtBlanc;
            this.pcbRoueNoirEtBlanc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbRoueNoirEtBlanc.Location = new System.Drawing.Point(1532, 84);
            this.pcbRoueNoirEtBlanc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcbRoueNoirEtBlanc.Name = "pcbRoueNoirEtBlanc";
            this.pcbRoueNoirEtBlanc.Size = new System.Drawing.Size(467, 438);
            this.pcbRoueNoirEtBlanc.TabIndex = 30;
            this.pcbRoueNoirEtBlanc.TabStop = false;
            this.pcbRoueNoirEtBlanc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pcbRoueNoirEtBlanc_MouseClick);
            // 
            // dgvRoue
            // 
            this.dgvRoue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoue.Location = new System.Drawing.Point(721, 190);
            this.dgvRoue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvRoue.Name = "dgvRoue";
            this.dgvRoue.RowHeadersWidth = 62;
            this.dgvRoue.RowTemplate.Height = 28;
            this.dgvRoue.Size = new System.Drawing.Size(744, 274);
            this.dgvRoue.TabIndex = 29;
            // 
            // pcbRoue
            // 
            this.pcbRoue.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imageRoue;
            this.pcbRoue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbRoue.Location = new System.Drawing.Point(185, 84);
            this.pcbRoue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcbRoue.Name = "pcbRoue";
            this.pcbRoue.Size = new System.Drawing.Size(467, 438);
            this.pcbRoue.TabIndex = 28;
            this.pcbRoue.TabStop = false;
            this.pcbRoue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pcbRoue_MouseClick);
            // 
            // btnEffacerCouleur
            // 
            this.btnEffacerCouleur.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imageRechargerOrange;
            this.btnEffacerCouleur.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEffacerCouleur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEffacerCouleur.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEffacerCouleur.Location = new System.Drawing.Point(1031, 510);
            this.btnEffacerCouleur.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEffacerCouleur.Name = "btnEffacerCouleur";
            this.btnEffacerCouleur.Size = new System.Drawing.Size(87, 75);
            this.btnEffacerCouleur.TabIndex = 27;
            this.btnEffacerCouleur.UseVisualStyleBackColor = true;
            this.btnEffacerCouleur.Click += new System.EventHandler(this.btnEffacerCouleur_Click);
            // 
            // ucVueParametres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(64)))), ((int)(((byte)(66)))));
            this.Controls.Add(this.grpChangerCouleur);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucVueParametres";
            this.Size = new System.Drawing.Size(2237, 1302);
            this.Load += new System.EventHandler(this.ucVueParametres_Load);
            this.Resize += new System.EventHandler(this.ucVueParametres_Resize);
            this.grpChangerCouleur.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbRoueNoirEtBlanc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbRoue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpChangerCouleur;
        private System.Windows.Forms.Button btnEffacerCouleur;
        private System.Windows.Forms.PictureBox pcbRoue;
        private System.Windows.Forms.DataGridView dgvRoue;
        private System.Windows.Forms.PictureBox pcbRoueNoirEtBlanc;
    }
}
