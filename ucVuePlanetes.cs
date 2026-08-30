using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using UcPlanete;

namespace SAE24_Stargate
{
    public partial class ucVuePlanetes : UserControl
    {
        public ucVuePlanetes()
        {
            InitializeComponent();
        }
        private void ucVuePlanetes_Load(object sender, EventArgs e)
        {
            try
            {
                flpPlanetes.SuspendLayout();

                flpPlanetes.Visible = false;
                foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Planete"].Rows)
                {
                    uscPlanete uc = new uscPlanete();
                    uc.NomPlanete = ligne[0].ToString();
                    if (ligne[1] == DBNull.Value)
                    {
                        uc.Temperature = "Inconnu";
                    }
                    else
                    {
                        uc.Temperature = ligne[1].ToString();
                    }
                    if (ligne[2] == DBNull.Value)
                    {
                        uc.Gravite = "Inconnu";
                    }
                    else
                    {
                        uc.Gravite = ligne[2].ToString();
                    }
                    if (ligne[3] == DBNull.Value)
                    {
                        uc.QtDataBaz = 2;
                    }
                    else if (Convert.ToInt32(ligne[3]) == 1)
                    {
                        uc.QtDataBaz = 1;
                    }
                    else
                    {
                        uc.QtDataBaz = 0;
                    }
                    uc.ImagePlanete = Image.FromFile($@"img\Planetes\{ligne[0].ToString()}.png");
                    uc.Margin = new Padding(8);
                    //Attribution d'un evenement click
                    uc.Click += CartePlanete_Click;
                    flpPlanetes.Controls.Add(uc);

                }
                // Force le calcul du centrage une fois que toutes les planètes sont ajoutées
                flpPlanetes_Resize(this, EventArgs.Empty);

                flpPlanetes.ResumeLayout();
                flpPlanetes.Visible = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void flpPlanetes_Resize(object sender, EventArgs e)
        {
            // Sécurité : s'il n'y a aucune planète chargée, on ne calcule rien
            if (flpPlanetes.Controls.Count == 0) return;

            // On calcule la largeur totale d'une seule carte (largeur + marges gauche/droite)
            int largeurCarte = flpPlanetes.Controls[0].Width + flpPlanetes.Controls[0].Margin.Horizontal;

            // Combien de cartes entières peuvent rentrer sur une seule ligne ?
            int nbColonnes = flpPlanetes.ClientSize.Width / largeurCarte;

            // Sécurité : si la fenêtre est trop petite pour afficher au moins 1 carte, on arrête
            if (nbColonnes == 0) return;

            // On calcule l'espace total qui reste vide à droite
            // (Largeur totale du panneau - la largeur prise par les cartes)
            int espaceRestant = flpPlanetes.ClientSize.Width - (nbColonnes * largeurCarte);

            // On divise ce vide par 2 pour en mettre la moitié à gauche (ce qui centre le reste)
            int paddingGauche = espaceRestant / 2;

            // On applique cet espace à gauche du panneau (Gauche, Haut, Droite, Bas)
            flpPlanetes.Padding = new Padding(paddingGauche, 0, 0, 0);
        }

        private void CartePlanete_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is uscPlanete carteCliquee)
                {
                    ucVueInfosPlanetes uc = new ucVueInfosPlanetes();
                    Image img = carteCliquee.imagePlanete;
                    string nomPlanete = carteCliquee.NomPlanete;
                    string temp = carteCliquee.Temperature;
                    string grav = carteCliquee.Gravite;
                    string dataBaz = carteCliquee.TexteDataBaz;
                    Color color = carteCliquee.CouleurDataBaz;
                    uc.initInfosPlantes(img, nomPlanete, temp, grav, dataBaz, color);
                    this.FindForm().Controls.Add(uc); // On l'ajoute au formulaire
                                                      // On place le composant tout en haut à gauche
                    uc.Location = new Point(0, 0);

                    // On lui force la taille exacte de l'intérieur de la fenêtre principale
                    uc.Size = this.FindForm().ClientSize;
                    uc.GenererAliens(nomPlanete);
                    uc.GenererMissions(nomPlanete);
                    // On l'ancre de tous les côtés pour qu'il suive les redimensionnements
                    uc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    uc.BringToFront();             // On le passe tout devant pour cacher le reste
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


    }
}
