namespace SAE24_Stargate
{
    partial class frmChargement
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
            this.pgbTransmission = new System.Windows.Forms.ProgressBar();
            this.lblTransmission = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgbTransmission
            // 
            this.pgbTransmission.Location = new System.Drawing.Point(105, 172);
            this.pgbTransmission.Name = "pgbTransmission";
            this.pgbTransmission.Size = new System.Drawing.Size(381, 35);
            this.pgbTransmission.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgbTransmission.TabIndex = 1;
            // 
            // lblTransmission
            // 
            this.lblTransmission.AutoSize = true;
            this.lblTransmission.ForeColor = System.Drawing.Color.White;
            this.lblTransmission.Location = new System.Drawing.Point(165, 114);
            this.lblTransmission.Name = "lblTransmission";
            this.lblTransmission.Size = new System.Drawing.Size(246, 20);
            this.lblTransmission.TabIndex = 2;
            this.lblTransmission.Text = "Traitement des données en cours";
            // 
            // frmChargement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(603, 341);
            this.Controls.Add(this.pgbTransmission);
            this.Controls.Add(this.lblTransmission);
            this.Name = "frmChargement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmChargement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbTransmission;
        private System.Windows.Forms.Label lblTransmission;
    }
}