namespace SAE24_Stargate
{
    partial class ucVueAliens
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
            this.components = new System.ComponentModel.Container();
            this.chkNeutre = new System.Windows.Forms.CheckBox();
            this.cboName = new System.Windows.Forms.ComboBox();
            this.cboPlanete = new System.Windows.Forms.ComboBox();
            this.lblPlanete = new System.Windows.Forms.Label();
            this.cboCouleurs = new System.Windows.Forms.ComboBox();
            this.chKEnnemi = new System.Windows.Forms.CheckBox();
            this.chkAllies = new System.Windows.Forms.CheckBox();
            this.lblCouleur = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.flpAliens = new System.Windows.Forms.FlowLayoutPanel();
            this.tltReinitialiser = new System.Windows.Forms.ToolTip(this.components);
            this.lblIdentification = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pcbImageAlien = new System.Windows.Forms.PictureBox();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.lblReinitialiserFiltre = new System.Windows.Forms.Label();
            this.pnlFiltreAlien = new System.Windows.Forms.Panel();
            this.lblResultat = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbImageAlien)).BeginInit();
            this.pnlFiltreAlien.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkNeutre
            // 
            this.chkNeutre.AutoSize = true;
            this.chkNeutre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkNeutre.ForeColor = System.Drawing.Color.White;
            this.chkNeutre.Location = new System.Drawing.Point(107, 700);
            this.chkNeutre.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chkNeutre.Name = "chkNeutre";
            this.chkNeutre.Size = new System.Drawing.Size(119, 29);
            this.chkNeutre.TabIndex = 13;
            this.chkNeutre.Text = "Neutres";
            this.chkNeutre.UseVisualStyleBackColor = true;
            this.chkNeutre.CheckedChanged += new System.EventHandler(this.chkNeutre_CheckedChanged);
            // 
            // cboName
            // 
            this.cboName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboName.ForeColor = System.Drawing.Color.Black;
            this.cboName.FormattingEnabled = true;
            this.cboName.Location = new System.Drawing.Point(107, 209);
            this.cboName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboName.Name = "cboName";
            this.cboName.Size = new System.Drawing.Size(452, 33);
            this.cboName.Sorted = true;
            this.cboName.TabIndex = 12;
            this.cboName.SelectedIndexChanged += new System.EventHandler(this.cboName_SelectedIndexChanged);
            // 
            // cboPlanete
            // 
            this.cboPlanete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboPlanete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlanete.ForeColor = System.Drawing.Color.Black;
            this.cboPlanete.FormattingEnabled = true;
            this.cboPlanete.Location = new System.Drawing.Point(107, 435);
            this.cboPlanete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboPlanete.Name = "cboPlanete";
            this.cboPlanete.Size = new System.Drawing.Size(452, 33);
            this.cboPlanete.Sorted = true;
            this.cboPlanete.TabIndex = 10;
            this.cboPlanete.SelectedIndexChanged += new System.EventHandler(this.cboName_SelectedIndexChanged);
            // 
            // lblPlanete
            // 
            this.lblPlanete.AutoSize = true;
            this.lblPlanete.ForeColor = System.Drawing.Color.White;
            this.lblPlanete.Location = new System.Drawing.Point(101, 389);
            this.lblPlanete.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPlanete.Name = "lblPlanete";
            this.lblPlanete.Size = new System.Drawing.Size(184, 25);
            this.lblPlanete.TabIndex = 9;
            this.lblPlanete.Text = "Planète d\'origine :";
            // 
            // cboCouleurs
            // 
            this.cboCouleurs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboCouleurs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCouleurs.ForeColor = System.Drawing.Color.Black;
            this.cboCouleurs.FormattingEnabled = true;
            this.cboCouleurs.Location = new System.Drawing.Point(107, 316);
            this.cboCouleurs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboCouleurs.Name = "cboCouleurs";
            this.cboCouleurs.Size = new System.Drawing.Size(452, 33);
            this.cboCouleurs.Sorted = true;
            this.cboCouleurs.TabIndex = 8;
            this.cboCouleurs.SelectedIndexChanged += new System.EventHandler(this.cboName_SelectedIndexChanged);
            // 
            // chKEnnemi
            // 
            this.chKEnnemi.AutoSize = true;
            this.chKEnnemi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chKEnnemi.ForeColor = System.Drawing.Color.White;
            this.chKEnnemi.Location = new System.Drawing.Point(107, 658);
            this.chKEnnemi.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chKEnnemi.Name = "chKEnnemi";
            this.chKEnnemi.Size = new System.Drawing.Size(127, 29);
            this.chKEnnemi.TabIndex = 5;
            this.chKEnnemi.Text = "Ennemis";
            this.chKEnnemi.UseVisualStyleBackColor = true;
            this.chKEnnemi.Click += new System.EventHandler(this.chKEnnemi_CheckedChanged);
            // 
            // chkAllies
            // 
            this.chkAllies.AutoSize = true;
            this.chkAllies.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAllies.ForeColor = System.Drawing.Color.White;
            this.chkAllies.Location = new System.Drawing.Point(107, 615);
            this.chkAllies.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chkAllies.Name = "chkAllies";
            this.chkAllies.Size = new System.Drawing.Size(96, 29);
            this.chkAllies.TabIndex = 4;
            this.chkAllies.Text = "Alliés";
            this.chkAllies.UseVisualStyleBackColor = true;
            this.chkAllies.Click += new System.EventHandler(this.chkAllies_CheckedChanged);
            // 
            // lblCouleur
            // 
            this.lblCouleur.AutoSize = true;
            this.lblCouleur.ForeColor = System.Drawing.Color.White;
            this.lblCouleur.Location = new System.Drawing.Point(101, 272);
            this.lblCouleur.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCouleur.Name = "lblCouleur";
            this.lblCouleur.Size = new System.Drawing.Size(197, 25);
            this.lblCouleur.TabIndex = 1;
            this.lblCouleur.Text = "Choisir la couleur : ";
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.ForeColor = System.Drawing.Color.White;
            this.lblNom.Location = new System.Drawing.Point(101, 161);
            this.lblNom.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(215, 25);
            this.lblNom.TabIndex = 0;
            this.lblNom.Text = "Séléctionner la race :";
            // 
            // flpAliens
            // 
            this.flpAliens.AutoScroll = true;
            this.flpAliens.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(70)))));
            this.flpAliens.Location = new System.Drawing.Point(725, 16);
            this.flpAliens.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.flpAliens.Name = "flpAliens";
            this.flpAliens.Size = new System.Drawing.Size(1577, 1081);
            this.flpAliens.TabIndex = 1;
            // 
            // lblIdentification
            // 
            this.lblIdentification.AutoSize = true;
            this.lblIdentification.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdentification.ForeColor = System.Drawing.Color.White;
            this.lblIdentification.Location = new System.Drawing.Point(167, 72);
            this.lblIdentification.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIdentification.Name = "lblIdentification";
            this.lblIdentification.Size = new System.Drawing.Size(289, 29);
            this.lblIdentification.TabIndex = 19;
            this.lblIdentification.Text = "Identification des aliens";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(153, 525);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 29);
            this.label1.TabIndex = 21;
            this.label1.Text = "Affectation";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imageUtilisateurDouble;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(60, 501);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 75);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // pcbImageAlien
            // 
            this.pcbImageAlien.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imageAlienSimple;
            this.pcbImageAlien.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbImageAlien.Location = new System.Drawing.Point(60, 50);
            this.pcbImageAlien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcbImageAlien.Name = "pcbImageAlien";
            this.pcbImageAlien.Size = new System.Drawing.Size(80, 75);
            this.pcbImageAlien.TabIndex = 18;
            this.pcbImageAlien.TabStop = false;
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imageDoubleRechargementOrange;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnnuler.Location = new System.Drawing.Point(60, 771);
            this.btnAnnuler.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(80, 72);
            this.btnAnnuler.TabIndex = 6;
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // lblReinitialiserFiltre
            // 
            this.lblReinitialiserFiltre.AutoSize = true;
            this.lblReinitialiserFiltre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblReinitialiserFiltre.ForeColor = System.Drawing.Color.White;
            this.lblReinitialiserFiltre.Location = new System.Drawing.Point(153, 795);
            this.lblReinitialiserFiltre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReinitialiserFiltre.Name = "lblReinitialiserFiltre";
            this.lblReinitialiserFiltre.Size = new System.Drawing.Size(267, 29);
            this.lblReinitialiserFiltre.TabIndex = 24;
            this.lblReinitialiserFiltre.Text = "Réinitialiser les filtres";
            // 
            // pnlFiltreAlien
            // 
            this.pnlFiltreAlien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.pnlFiltreAlien.Controls.Add(this.lblMessage);
            this.pnlFiltreAlien.Controls.Add(this.lblResultat);
            this.pnlFiltreAlien.Controls.Add(this.pcbImageAlien);
            this.pnlFiltreAlien.Controls.Add(this.cboCouleurs);
            this.pnlFiltreAlien.Controls.Add(this.pictureBox1);
            this.pnlFiltreAlien.Controls.Add(this.chkNeutre);
            this.pnlFiltreAlien.Controls.Add(this.btnAnnuler);
            this.pnlFiltreAlien.Controls.Add(this.chkAllies);
            this.pnlFiltreAlien.Controls.Add(this.lblPlanete);
            this.pnlFiltreAlien.Controls.Add(this.label1);
            this.pnlFiltreAlien.Controls.Add(this.lblReinitialiserFiltre);
            this.pnlFiltreAlien.Controls.Add(this.lblCouleur);
            this.pnlFiltreAlien.Controls.Add(this.cboPlanete);
            this.pnlFiltreAlien.Controls.Add(this.lblIdentification);
            this.pnlFiltreAlien.Controls.Add(this.lblNom);
            this.pnlFiltreAlien.Controls.Add(this.cboName);
            this.pnlFiltreAlien.Controls.Add(this.chKEnnemi);
            this.pnlFiltreAlien.Location = new System.Drawing.Point(17, 16);
            this.pnlFiltreAlien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlFiltreAlien.Name = "pnlFiltreAlien";
            this.pnlFiltreAlien.Size = new System.Drawing.Size(657, 1081);
            this.pnlFiltreAlien.TabIndex = 1;
            // 
            // lblResultat
            // 
            this.lblResultat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultat.Location = new System.Drawing.Point(18, 884);
            this.lblResultat.Name = "lblResultat";
            this.lblResultat.Size = new System.Drawing.Size(613, 54);
            this.lblResultat.TabIndex = 25;
            this.lblResultat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResultat.Visible = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(18, 938);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(613, 54);
            this.lblMessage.TabIndex = 26;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.Visible = false;
            // 
            // ucVueAliens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnlFiltreAlien);
            this.Controls.Add(this.flpAliens);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucVueAliens";
            this.Size = new System.Drawing.Size(2417, 1138);
            this.Load += new System.EventHandler(this.ucVueAliens_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbImageAlien)).EndInit();
            this.pnlFiltreAlien.ResumeLayout(false);
            this.pnlFiltreAlien.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.CheckBox chkAllies;
        private System.Windows.Forms.Label lblCouleur;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.CheckBox chKEnnemi;
        private System.Windows.Forms.FlowLayoutPanel flpAliens;
		private System.Windows.Forms.ComboBox cboCouleurs;
		private System.Windows.Forms.ComboBox cboPlanete;
		private System.Windows.Forms.Label lblPlanete;
		private System.Windows.Forms.ComboBox cboName;
		private System.Windows.Forms.CheckBox chkNeutre;
		private System.Windows.Forms.ToolTip tltReinitialiser;
        private System.Windows.Forms.PictureBox pcbImageAlien;
        private System.Windows.Forms.Label lblIdentification;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReinitialiserFiltre;
        private System.Windows.Forms.Panel pnlFiltreAlien;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblResultat;
    }
}
