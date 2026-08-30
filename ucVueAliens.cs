using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UcPlanete;

namespace SAE24_Stargate
{
    public partial class ucVueAliens : UserControl
    {
        // On déclare les listes pour stocker les couleurs et les noms uniques des espèces
        private List<string> listColor;
        private List<string> listName;

        // On initialise un ErrorProvider pour gérer l'affichage visuel des erreurs de filtrage
        private ErrorProvider err = new ErrorProvider();

        public ucVueAliens()
        {
            InitializeComponent();
        }

        private void ucVueAliens_Load(object sender, EventArgs e)
        {
            // On donne le focus au champ de saisie du nom dès le chargement du contrôle
            cboName.Focus();

            // On configure manuellement le défilement vertical du FlowLayoutPanel
            flpAliens.HorizontalScroll.Maximum = 0;
            flpAliens.AutoScroll = false;
            flpAliens.VerticalScroll.Visible = true;
            flpAliens.AutoScroll = true;

            // On planifie l'exécution de la mise en page graphique de manière asynchrone pour éviter les bugs d'affichage
            this.BeginInvoke(new Action(() =>
            {
                CentrerPanels();
                ArrondiControle(pnlFiltreAlien, 22);
                ArrondiControle(flpAliens, 22);
            }));

            // On instancie les listes de stockage
            listColor = new List<string>();
            listName = new List<string>();

            try
            {
                // On parcourt les données de la table "Espece" pour extraire les noms et les couleurs uniques
                foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Espece"].Rows)
                {
                    if (!listColor.Contains(ligne["couleur"].ToString()))
                    {
                        listColor.Add(ligne["couleur"].ToString());
                    }
                    listName.Add(ligne["nom"].ToString());
                }

                // On remplit la ComboBox des planètes à partir de la table "Planete"
                foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Planete"].Rows)
                {
                    cboPlanete.Items.Add(ligne["nom"].ToString());
                }
                cboPlanete.Items.Add("Origine inconnue");

                // On alimente la ComboBox des couleurs avec les éléments uniques trouvés
                foreach (string couleur in listColor)
                {
                    cboCouleurs.Items.Add(couleur);
                }

                // On alimente la ComboBox des noms avec les éléments extraits
                foreach (string name in listName)
                {
                    cboName.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                // On vide la table temporaire "tblAliens" si elle existe déjà dans le DataSet global
                if (MesDatas.DsGlobal.Tables.Contains("tblAliens"))
                {
                    MesDatas.DsGlobal.Tables["tblAliens"].Rows.Clear();
                }

                // On construit la table consolidée des Aliens en associant Espèces, Habitats, Alliés et Ennemis
                for (int i = 0; i < MesDatas.DsGlobal.Tables["Espece"].Rows.Count; i++)
                {
                    string nom = MesDatas.DsGlobal.Tables["Espece"].Rows[i][1].ToString();
                    string idEspece = MesDatas.DsGlobal.Tables["Espece"].Rows[i][0].ToString();
                    string origine = "";
                    string filtre = @"idEspece = '" + idEspece + "'";
                    string statut = "";
                    string bienveillance = "";
                    string instru = "";
                    string agressivite = "";
                    string arme = "";
                    string couleur = MesDatas.DsGlobal.Tables["Espece"].Rows[i][2].ToString();
                    string contact = "";

                    // On récupère et on formate la liste des planètes habitées par cette espèce
                    DataRow[] tabOrigine = MesDatas.DsGlobal.Tables["Habiter"].Select(filtre);
                    if (tabOrigine.Length > 0)
                    {
                        for (int j = 0; j < tabOrigine.Length; j++)
                        {
                            if (j == tabOrigine.Length - 1)
                            {
                                origine += tabOrigine[j]["nomPlanete"].ToString();
                            }
                            else
                            {
                                origine += tabOrigine[j]["nomPlanete"].ToString() + " / ";
                            }
                        }
                    }
                    else
                    {
                        origine = "Origine inconnue";
                    }

                    // On vérifie si l'espèce est répertoriée comme Alliée
                    DataRow[] tabAllie = MesDatas.DsGlobal.Tables["Allie"].Select(filtre);
                    if (tabAllie.Length > 0)
                    {
                        statut = "A";
                        bienveillance = tabAllie[0]["degreBienveillance"].ToString();
                        instru = tabAllie[0]["instrumentMusique"].ToString();
                        contact = tabAllie[0]["datePremierContact"].ToString();
                    }

                    // On vérifie si l'espèce est répertoriée comme Ennemie
                    DataRow[] tabEnnemi = MesDatas.DsGlobal.Tables["Ennemi"].Select(filtre);
                    if (tabEnnemi.Length > 0)
                    {
                        agressivite = tabEnnemi[0]["degreAgressivite"].ToString();
                        arme = tabEnnemi[0]["typeArme"].ToString();
                        statut = "E";
                    }

                    // On définit le statut par défaut à Neutre s'il n'est ni Allié ni Ennemi
                    if (statut != "A" && statut != "E")
                    {
                        statut = "N";
                    }

                    // On crée et on ajoute la nouvelle ligne d'informations dans "tblAliens"
                    DataRow ligne = MesDatas.DsGlobal.Tables["tblAliens"].NewRow();
                    ligne[0] = nom;
                    ligne[1] = idEspece;
                    ligne[2] = origine;
                    ligne[3] = statut;
                    ligne[4] = bienveillance;
                    ligne[5] = instru;
                    ligne[6] = agressivite;
                    ligne[7] = arme;
                    ligne[8] = couleur;
                    ligne[9] = contact;
                    MesDatas.DsGlobal.Tables["tblAliens"].Rows.Add(ligne);
                }

                // On affiche initialement la totalité des Aliens générés
                RemplirDonneesAliens(MesDatas.DsGlobal.Tables["tblAliens"].Rows);

                // On configure l'infobulle d'aide sur le bouton d'annulation
                tltReinitialiser.SetToolTip(btnAnnuler, "Réinitialiser les filtres");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ucVueAliens_Resize(object sender, EventArgs e)
        {
            // On recalcule le centrage et les arrondis lors du redimensionnement de la fenêtre
            CentrerPanels();
            ArrondiControle(pnlFiltreAlien, 22);
            ArrondiControle(flpAliens, 22);

            // On réapplique les filtres en cours si la table de données est disponible
            if (MesDatas.DsGlobal.Tables.Contains("tblAliens"))
            {
                verifFiltre();
            }
        }

        private void ArrondiControle(Control ctrl, int rayon)
        {
            // On utilise un GraphicsPath pour découper et arrondir les angles géométriques d'un composant
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, rayon, rayon, 180, 90);
            path.AddArc(ctrl.Width - rayon, 0, rayon, rayon, 270, 90);
            path.AddArc(ctrl.Width - rayon, ctrl.Height - rayon, rayon, rayon, 0, 90);
            path.AddArc(0, ctrl.Height - rayon, rayon, rayon, 90, 90);
            path.CloseAllFigures();
            ctrl.Region = new Region(path);
        }

        private void CentrerPanels()
        {
            // On calcule dynamiquement les positions X et Y pour centrer parfaitement les deux panneaux principaux
            int marge = 20;
            int largeurTotale = pnlFiltreAlien.Width + flpAliens.Width + marge;
            int depart = (this.Width - largeurTotale) / 2;
            int topCommun = (this.Height - Math.Max(pnlFiltreAlien.Height, flpAliens.Height)) / 2;

            pnlFiltreAlien.Left = depart;
            pnlFiltreAlien.Top = topCommun;

            flpAliens.Left = depart + pnlFiltreAlien.Width + marge;
            flpAliens.Top = topCommun;
        }

        private void AfficherAlienAuFurEtAMesure(DataRow[] lignes)
        {
            // On bloque temporairement le rafraîchissement graphique du conteneur pour optimiser les performances
            flpAliens.SuspendLayout();
            flpAliens.Controls.Clear();
            int index = 0;

            // On boucle sur chaque ligne filtrée pour instancier et injecter les fiches Alien graphiques
            foreach (DataRow ligne in lignes)
            {
                string couleur = ligne["Couleur"].ToString();
                uscAliens uc1 = new uscAliens();
                Image img = Image.FromFile($@"img\Aliens\{couleur}.png");

                // On configure l'UserControl selon la catégorie de l'Alien (Allié, Ennemi ou Neutre)
                if (ligne["statut"].ToString() == "A")
                {
                    uc1.setAllie(img, ligne["nom"].ToString(), ligne["origine"].ToString(), ligne["couleur"].ToString(), ligne["contact"].ToString(), ligne["degreBienveillance"].ToString(), ligne["instrument"].ToString());
                }
                if (ligne["statut"].ToString() == "E")
                {
                    uc1.setEnnemi(img, ligne["nom"].ToString(), ligne["origine"].ToString(), ligne["couleur"].ToString(), ligne["arme"].ToString(), ligne["degreAgressivite"].ToString());
                }
                if (ligne["statut"].ToString() == "N")
                {
                    uc1.setNeutre(img, ligne["nom"].ToString(), ligne["origine"].ToString(), ligne["couleur"].ToString());
                }

                // On crée une boîte conteneur (wrapper) blanche autour de la fiche pour styliser la bordure arrondie
                Panel wrapper = new Panel();
                wrapper.Size = new Size(uc1.Width + 4, uc1.Height + 4);
                wrapper.BackColor = Color.White;
                wrapper.Padding = new Padding(2);
                uc1.Location = new Point(2, 2);
                wrapper.Controls.Add(uc1);
                ArrondiControle(wrapper, 22);

                // On ajuste les marges extérieures pour forcer un affichage structuré sur deux colonnes centrées
                int totalWidth = wrapper.Width * 2 + 20;
                int remainingSpace = flpAliens.ClientSize.Width - totalWidth;
                int sideMargin = remainingSpace > 0 ? remainingSpace / 2 : 0;

                if (index % 2 == 0)
                {
                    wrapper.Margin = new Padding(Math.Max(0, sideMargin - 3), 20, 10, 0);
                }
                else
                {
                    wrapper.Margin = new Padding(10, 20, Math.Max(0, sideMargin + 3), 0);
                }

                flpAliens.Controls.Add(wrapper);
                index++;
            }

            // On rétablit le dessin et le rafraîchissement visuel du conteneur
            flpAliens.ResumeLayout();
        }

        private void RemplirDonneesAliens(DataRowCollection lignes)
        {
            // On convertit la collection brute de lignes de données en un tableau standard pour l'affichage
            DataRow[] rows = new DataRow[lignes.Count];
            lignes.CopyTo(rows, 0);
            AfficherAlienAuFurEtAMesure(rows);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            try
            {
                // On réinitialise l'intégralité des champs de filtrage graphiques à leur état d'origine
                cboName.SelectedIndex = -1;
                cboCouleurs.SelectedIndex = -1;
                cboPlanete.SelectedIndex = -1;
                chkAllies.Checked = false;
                chKEnnemi.Checked = false;
                chkNeutre.Checked = false;
                lblMessage.Text = string.Empty;
                lblResultat.Text = string.Empty;

                // On recharge la liste complète sans aucun filtre
                RemplirDonneesAliens(MesDatas.DsGlobal.Tables["tblAliens"].Rows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkAllies_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // On décoche obligatoirement les autres cases à cocher de statut pour garder une sélection unique
                if (chkAllies.Checked)
                {
                    chKEnnemi.Checked = false;
                    chkNeutre.Checked = false;
                }
                verifFiltre();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chKEnnemi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // On décoche obligatoirement les autres cases à cocher de statut pour garder une sélection unique
                if (chKEnnemi.Checked)
                {
                    chkAllies.Checked = false;
                    chkNeutre.Checked = false;
                }
                verifFiltre();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkNeutre_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // On décoche obligatoirement les autres cases à cocher de statut pour garder une sélection unique
                if (chkNeutre.Checked)
                {
                    chKEnnemi.Checked = false;
                    chkAllies.Checked = false;
                }
                verifFiltre();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // On recalcule les résultats dès que l'utilisateur sélectionne un nom spécifique
                verifFiltre();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void verifFiltre()
        {
            try
            {
                // On collecte les valeurs actuellement sélectionnées dans le formulaire
                string filtreNom = cboName.Text;
                string filtreCouleur = cboCouleurs.Text;
                string filtrePlanete = cboPlanete.Text;
                string filtreAEN = "";
                string filtreFinal = "";
                List<string> conditions = new List<string>();

                // On convertit l'état des cases à cocher en lettre de code statut ('A', 'E' ou 'N')
                if (chkAllies.Checked)
                {
                    filtreAEN = "A";
                }
                else if (chKEnnemi.Checked)
                {
                    filtreAEN = "E";
                }
                else if (chkNeutre.Checked)
                {
                    filtreAEN = "N";
                }

                // On construit dynamiquement la liste des clauses de filtrage SQL/ADO.NET
                if (filtreNom != string.Empty)
                {
                    conditions.Add("nom = '" + filtreNom + "'");
                }

                if (filtreCouleur != string.Empty)
                {
                    conditions.Add("couleur = '" + filtreCouleur + "'");
                }

                if (filtrePlanete != string.Empty)
                {
                    conditions.Add("origine LIKE '%" + filtrePlanete + "%'");
                }

                if (filtreAEN != string.Empty)
                {
                    conditions.Add("statut = '" + filtreAEN + "'");
                }

                // On fusionne toutes les requêtes actives avec l'opérateur logique "AND"
                filtreFinal = string.Join(" AND ", conditions);

                // On applique l'expression de filtre finale sur notre table de données
                DataRow[] tabFiltre = MesDatas.DsGlobal.Tables["tblAliens"].Select(filtreFinal);

                // On gère l'affichage selon la présence ou l'absence de correspondances trouvées
                if (tabFiltre.Length > 0)
                {
                    lblMessage.Text = string.Empty;
                    lblResultat.Text = string.Empty;
                    AfficherAlienAuFurEtAMesure(tabFiltre);
                }
                else
                {
                    // On nettoie la zone visuelle et on affiche un message d'alerte explicite en rouge
                    flpAliens.Controls.Clear();
                    lblResultat.Text = "Aucun résultat pour cette recherche !";
                    lblResultat.ForeColor = Color.Red;
                    lblResultat.Visible = true;
                    lblMessage.Text = "Veuillez réinitialiser les filtres";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}