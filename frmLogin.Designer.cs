namespace SAE24_Stargate
{
    partial class frmLogin
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
            this.pcbLogo = new System.Windows.Forms.PictureBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.pcbUser = new System.Windows.Forms.PictureBox();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.txtMdp = new System.Windows.Forms.TextBox();
            this.pcbLock = new System.Windows.Forms.PictureBox();
            this.pnlMdp = new System.Windows.Forms.Panel();
            this.btnConnexion = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblMdp = new System.Windows.Forms.Label();
            this.lblErreur = new System.Windows.Forms.Label();
            this.flpLogin = new System.Windows.Forms.FlowLayoutPanel();
            this.flpMdp = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUser)).BeginInit();
            this.pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLock)).BeginInit();
            this.pnlMdp.SuspendLayout();
            this.flpLogin.SuspendLayout();
            this.flpMdp.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcbLogo
            // 
            this.pcbLogo.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imgStargate;
            this.pcbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbLogo.Location = new System.Drawing.Point(239, 91);
            this.pcbLogo.Name = "pcbLogo";
            this.pcbLogo.Size = new System.Drawing.Size(172, 141);
            this.pcbLogo.TabIndex = 0;
            this.pcbLogo.TabStop = false;
            // 
            // lblLogin
            // 
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.ForeColor = System.Drawing.Color.White;
            this.lblLogin.Location = new System.Drawing.Point(160, 256);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(330, 62);
            this.lblLogin.TabIndex = 1;
            this.lblLogin.Text = "Veuillez vous authentifier pour ajouter une nouvelle mission";
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLogin
            // 
            this.txtLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(64)))), ((int)(((byte)(66)))));
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.ForeColor = System.Drawing.Color.White;
            this.txtLogin.Location = new System.Drawing.Point(38, 0);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(358, 37);
            this.txtLogin.TabIndex = 0;
            this.txtLogin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLogin_KeyPress);
            // 
            // pcbUser
            // 
            this.pcbUser.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imgUser2;
            this.pcbUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcbUser.Location = new System.Drawing.Point(0, 0);
            this.pcbUser.Name = "pcbUser";
            this.pcbUser.Size = new System.Drawing.Size(38, 31);
            this.pcbUser.TabIndex = 5;
            this.pcbUser.TabStop = false;
            // 
            // pnlLogin
            // 
            this.pnlLogin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLogin.Controls.Add(this.txtLogin);
            this.pnlLogin.Controls.Add(this.pcbUser);
            this.pnlLogin.Location = new System.Drawing.Point(3, 33);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(400, 35);
            this.pnlLogin.TabIndex = 4;
            // 
            // txtMdp
            // 
            this.txtMdp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(64)))), ((int)(((byte)(66)))));
            this.txtMdp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMdp.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMdp.ForeColor = System.Drawing.Color.White;
            this.txtMdp.Location = new System.Drawing.Point(39, 0);
            this.txtMdp.Name = "txtMdp";
            this.txtMdp.Size = new System.Drawing.Size(359, 37);
            this.txtMdp.TabIndex = 3;
            this.txtMdp.UseSystemPasswordChar = true;
            this.txtMdp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMdp_KeyPress);
            // 
            // pcbLock
            // 
            this.pcbLock.BackgroundImage = global::SAE24_Stargate.Properties.Resources.imgLock2;
            this.pcbLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbLock.Location = new System.Drawing.Point(0, -2);
            this.pcbLock.Name = "pcbLock";
            this.pcbLock.Size = new System.Drawing.Size(38, 35);
            this.pcbLock.TabIndex = 5;
            this.pcbLock.TabStop = false;
            // 
            // pnlMdp
            // 
            this.pnlMdp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMdp.Controls.Add(this.pcbLock);
            this.pnlMdp.Controls.Add(this.txtMdp);
            this.pnlMdp.Location = new System.Drawing.Point(3, 33);
            this.pnlMdp.Name = "pnlMdp";
            this.pnlMdp.Size = new System.Drawing.Size(400, 35);
            this.pnlMdp.TabIndex = 6;
            // 
            // btnConnexion
            // 
            this.btnConnexion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.btnConnexion.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnexion.ForeColor = System.Drawing.Color.White;
            this.btnConnexion.Location = new System.Drawing.Point(116, 517);
            this.btnConnexion.Name = "btnConnexion";
            this.btnConnexion.Size = new System.Drawing.Size(400, 40);
            this.btnConnexion.TabIndex = 7;
            this.btnConnexion.Text = "Connexion";
            this.btnConnexion.UseVisualStyleBackColor = false;
            this.btnConnexion.Click += new System.EventHandler(this.btnConnexion_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(3, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(173, 30);
            this.lblUser.TabIndex = 8;
            this.lblUser.Text = "Nom d\'utilisateur";
            // 
            // lblMdp
            // 
            this.lblMdp.AutoSize = true;
            this.lblMdp.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMdp.ForeColor = System.Drawing.Color.White;
            this.lblMdp.Location = new System.Drawing.Point(3, 0);
            this.lblMdp.Name = "lblMdp";
            this.lblMdp.Size = new System.Drawing.Size(138, 30);
            this.lblMdp.TabIndex = 9;
            this.lblMdp.Text = "Mot de passe";
            // 
            // lblErreur
            // 
            this.lblErreur.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErreur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblErreur.Location = new System.Drawing.Point(118, 578);
            this.lblErreur.Name = "lblErreur";
            this.lblErreur.Size = new System.Drawing.Size(398, 40);
            this.lblErreur.TabIndex = 10;
            this.lblErreur.Text = "Identifiant ou mot de passe incorrect";
            this.lblErreur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblErreur.Visible = false;
            // 
            // flpLogin
            // 
            this.flpLogin.Controls.Add(this.lblUser);
            this.flpLogin.Controls.Add(this.pnlLogin);
            this.flpLogin.Location = new System.Drawing.Point(101, 321);
            this.flpLogin.Name = "flpLogin";
            this.flpLogin.Size = new System.Drawing.Size(427, 100);
            this.flpLogin.TabIndex = 11;
            // 
            // flpMdp
            // 
            this.flpMdp.Controls.Add(this.lblMdp);
            this.flpMdp.Controls.Add(this.pnlMdp);
            this.flpMdp.Location = new System.Drawing.Point(101, 429);
            this.flpMdp.Name = "flpMdp";
            this.flpMdp.Size = new System.Drawing.Size(427, 83);
            this.flpMdp.TabIndex = 12;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(64)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(626, 681);
            this.Controls.Add(this.lblErreur);
            this.Controls.Add(this.flpMdp);
            this.Controls.Add(this.btnConnexion);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.pcbLogo);
            this.Controls.Add(this.flpLogin);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentification";
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUser)).EndInit();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLock)).EndInit();
            this.pnlMdp.ResumeLayout(false);
            this.pnlMdp.PerformLayout();
            this.flpLogin.ResumeLayout(false);
            this.flpLogin.PerformLayout();
            this.flpMdp.ResumeLayout(false);
            this.flpMdp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbLogo;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.PictureBox pcbUser;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.TextBox txtMdp;
        private System.Windows.Forms.PictureBox pcbLock;
        private System.Windows.Forms.Panel pnlMdp;
        private System.Windows.Forms.Button btnConnexion;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblMdp;
        private System.Windows.Forms.Label lblErreur;
        private System.Windows.Forms.FlowLayoutPanel flpLogin;
        private System.Windows.Forms.FlowLayoutPanel flpMdp;
    }
}