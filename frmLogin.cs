using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE24_Stargate
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnConnexion_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLogin.Text != String.Empty && txtMdp.Text != String.Empty)
                {
                    //Récupération du mot de passe en fonction du login
                    string requete = $"SELECT mdp FROM Admin WHERE login = '{txtLogin.Text}'";
                    SQLiteCommand cmd = new SQLiteCommand(requete, Connexion.Connec); object resultat = cmd.ExecuteScalar();
                    //Test si la requête a renvoyé un résultat non null
                    if (resultat != DBNull.Value && resultat != null)
                    {
                        string mdpStocke = resultat.ToString();
                        //Vérification du mot de passe
                        bool valide = BCrypt.Net.BCrypt.Verify(txtMdp.Text, mdpStocke);//Résultat de l'authentification
                        if (valide)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            lblErreur.Visible = true;
                            for (int i = 0; i < 5; i++)
                            {
                                this.Left += 10;
                                await Task.Delay(50);
                                this.Left -= 10;
                                await Task.Delay(50);
                            }
                            txtLogin.Clear();
                            txtMdp.Clear();
                            txtLogin.Focus();
                        }
                    }
                    else
                    {
                        // L'UTILISATEUR N'EXISTE PAS
                        lblErreur.Visible = true;
                        for (int i = 0; i < 5; i++)
                        {
                            this.Left += 10;
                            await Task.Delay(50);
                            this.Left -= 10;
                            await Task.Delay(50);
                        }
                        txtLogin.Clear();
                        txtMdp.Clear();
                        txtLogin.Focus();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lblErreur.Visible) { lblErreur.Visible = false; }
            if(e.KeyChar == (char)Keys.Enter)
            {
                txtMdp.Focus();
            }
        }

        private void txtMdp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lblErreur.Visible) { lblErreur.Visible = false; }
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnConnexion.PerformClick();
            }
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }
    }
}
