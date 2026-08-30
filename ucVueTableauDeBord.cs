using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UcMembre;
using UcMission;

namespace SAE24_Stargate
{
    public partial class ucVueTableauDeBord : UserControl
    {
        public ucVueTableauDeBord()
        {
            InitializeComponent();
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ucVueTableauDeBord_Load(object sender, EventArgs e)
        {
            // On ajoute un texte d'aide sur le bouton du journal de bord
            tltJournalDeBord.SetToolTip(btnJournalDeBord, "Accès au journal de bord");

            // On ajoute un texte d'aide sur le bouton pour ajouter un contact
            tltNouveauContact.SetToolTip(btnNouveauContact, "Ajouter un nouveau contact");

            // On ajoute un texte d'aide sur le bouton pour ajouter une dépense
            tltNouvelleDepense.SetToolTip(btnNouvelleDepense, "Saisir une nouvelle dépense");

            // On ajoute un texte d'aide sur le bouton pour ajouter un événement
            tltNouvelEvenement.SetToolTip(btnNouvelEvenement, "Ajouter un nouvelle évènement");

            // On ajoute un texte d'aide sur le bouton pour ajouter une capture
            tltAjoutCapture.SetToolTip(btnAjouterCapture, "Ajouter une nouvelle capture");

            // On attend que l'affichage soit prêt avant de lancer la suite
            this.BeginInvoke(new Action(() =>
            {
                // On charge la liste des missions
                ChargerMissions();

                // On centre les blocs sur l'écran
                CentrerPanels();

                // On arrondit les coins du bloc des boutons de mission
                ArrondiControle(pnlBoutonMission, 22);

                // On arrondit les coins du bloc de la mission
                ArrondiControle(pnlMission, 22);

                // On arrondit le bouton pour aller tout à droite
                ArrondiControle(btnFullDroite, 46);

                // On arrondit le bouton pour aller à droite
                ArrondiControle(btnDroite, 46);

                // On arrondit le bouton pour aller à gauche
                ArrondiControle(btnGauche, 46);

                // On arrondit le bouton pour aller tout à gauche
                ArrondiControle(btnFullGauche, 46);

                // On arrondit les coins du bloc des boutons d'événement
                ArrondiControle(pnlBoutonEvenement, 36);

                // On arrondit les coins du bloc des commentaires et dates
                ArrondiControle(pnlCommentaireEtDateEvenement, 36);

            }));

            // On force l'affichage du texte d'aide pour le bouton contact
            tltNouveauContact.ShowAlways = true;

            // On force l'affichage du texte d'aide pour le bouton dépense
            tltNouvelleDepense.ShowAlways = true;

            // On force l'affichage du texte d'aide pour le bouton événement
            tltNouvelEvenement.ShowAlways = true;

            // On force l'affichage du texte d'aide pour le bouton capture
            tltAjoutCapture.ShowAlways = true;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ucVueTableauDeBord_Resize(object sender, EventArgs e)
        {
            // On centre le GroupBox principal sur l'écran
            CentrerGroupBox();

            // On centre les différents Panels sur l'écran
            CentrerPanels();

            // On arrondit les coins du Panel des boutons de mission
            ArrondiControle(pnlBoutonMission, 22);

            // On arrondit les coins du Panel de la mission
            ArrondiControle(pnlMission, 22);

            // On arrondit les coins du Bouton pour aller tout à droite
            ArrondiControle(btnFullDroite, 40);
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ArrondiControle(Control ctrl, int rayon)
        {
            // On crée un outil pour dessiner une forme personnalisée
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            // On dessine l'arrondi du coin supérieur gauche
            path.AddArc(0, 0, rayon, rayon, 180, 90);

            // On dessine l'arrondi du coin supérieur droit
            path.AddArc(ctrl.Width - rayon, 0, rayon, rayon, 270, 90);

            // On dessine l'arrondi du coin inférieur droit
            path.AddArc(ctrl.Width - rayon, ctrl.Height - rayon, rayon, rayon, 0, 90);

            // On dessine l'arrondi du coin inférieur gauche
            path.AddArc(0, ctrl.Height - rayon, rayon, rayon, 90, 90);

            // On ferme la forme géométrique dessinée
            path.CloseAllFigures();

            // On applique cette forme arrondie comme nouvelle bordure du Control
            ctrl.Region = new Region(path);
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void CentrerPanels()
        {
            // On définit l'espace entre les deux Panels
            int marge = 20;

            // On calcule la largeur totale des deux Panels avec la marge
            int largeurTotale = pnlBoutonMission.Width + pnlMission.Width + marge;

            // On calcule la position de départ à gauche pour centrer l'ensemble
            int depart = (this.Width - largeurTotale) / 2;

            // On calcule la position hauteur pour centrer les deux Panels verticalement
            int topCommun = (this.Height - Math.Max(pnlBoutonMission.Height, pnlMission.Height)) / 2;

            // On positionne le Panel des boutons à gauche et au centre vertical
            pnlBoutonMission.Left = depart;
            pnlBoutonMission.Top = topCommun;

            // On positionne le Panel de la mission à droite du premier avec la marge
            pnlMission.Left = depart + pnlBoutonMission.Width + marge;
            pnlMission.Top = topCommun;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void CentrerGroupBox()
        {
            // On calcule la position pour centrer le GroupBox de la fiche mission sur l'écran
            grpFicheMission.Left = (this.Width - grpFicheMission.Width) / 2;
            grpFicheMission.Top = (this.Height - grpFicheMission.Height) / 2;

            // On calcule la position pour centrer le GroupBox du journal de bord sur l'écran
            grpJournalDeBord.Left = (this.Width - grpJournalDeBord.Width) / 2;
            grpJournalDeBord.Top = (this.Height - grpJournalDeBord.Height) / 2;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private string FormatDate(string date)
        {
            // On essaie d'abord le format base de données yyyy-MM-dd 
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
            {
                // Si ça marche on retourne au format français dd/MM/yyyy
                return dt.ToString("dd/MM/yyyy");
            }
            // Sinon on essaie le format français dd/MM/yyyy
            else if (DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt2))
            {
                // Si ça marche on retourne tel quel puisque c'est déjà le bon format
                return dt2.ToString("dd/MM/yyyy");
            }
            // Si aucun des deux formats ne correspond
            else
            {
                // On retourne la date telle quelle pour ne pas perdre l'info
                return date;
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private uscMission CreerUcMission(DataRow ligne)
        {
            // On crée une nouvelle instance du User Control de la mission
            uscMission uc = new uscMission();

            // On charge l'image de la planète de la mission depuis le dossier des images
            uc.ImageMission = Image.FromFile($@"img\Planetes\{ligne[0].ToString()}.png");

            // On compose le nom unique de la mission avec la planète et le numéro
            uc.NomMission = ligne["nomPlanete"].ToString() + ligne["numero"].ToString();

            // On gère l'événement du clic sur le bouton de recherche pour afficher la fiche de la mission
            uc.RechercheCliquee += (s, nomMission) =>
            {
                // On charge les données de la fiche de la mission sélectionnée
                ChargerFicheMission(nomMission);
            };

            // On récupère l'objectif de Databaz de la mission
            uc.Databaz = ligne["objectifDatabaz"].ToString();

            // On récupère le nombre de membres requis pour la mission
            uc.Membre = ligne["nbMembreRequis"].ToString();

            // On récupère le matricule du chef de mission
            string matriculeChef = ligne["matriculeChef"].ToString();

            // On prépare une variable pour stocker le nom et le prénom du chef
            string nomPrenom = matriculeChef;

            // On parcourt la table des membres pour trouver l'identité du chef
            foreach (DataRow ligneMembre in MesDatas.DsGlobal.Tables["Membre"].Rows)
            {
                // Si le matricule correspond à celui du chef de mission
                if (ligneMembre["matricule"].ToString() == matriculeChef)
                {
                    // On assemble le nom et le prénom du chef
                    nomPrenom = ligneMembre["nom"].ToString() + " " + ligneMembre["prenom"].ToString();

                    // On arrête la recherche puisqu'on a trouvé le chef
                    break;
                }
            }

            // On affecte le nom complet du chef au User Control
            uc.ChefMission = nomPrenom;

            // On récupère le budget de la mission
            uc.Budget = ligne["budget"].ToString();

            // On met en forme la date de départ pour l'affichage
            uc.DateDepart = FormatDate(ligne["dateDepart"].ToString());

            // On met en forme la date de retour pour l'affichage
            uc.DateRetour = FormatDate(ligne["dateRetour"].ToString());

            // On récupère la date du jour au format année-mois-jour
            string dateActuelle = DateTime.Now.ToString("yyyy-MM-dd");

            // On garde la date de départ brute pour faire la comparaison
            string dateDepart = ligne["dateDepart"].ToString();

            // On garde la date de retour brute pour faire la comparaison
            string dateRetour = ligne["dateRetour"].ToString();

            // Si la date de retour est passée par rapport à aujourd'hui
            if (string.Compare(dateRetour, dateActuelle) < 0)
            {
                // On met la bordure du User Control en vert (mission terminée)
                uc.CouleurBordure = Color.LimeGreen;
            }
            // Sinon si la date de départ est dans le futur par rapport à aujourd'hui
            else if (string.Compare(dateDepart, dateActuelle) > 0)
            {
                // On met la bordure du User Control en rouge (mission pas commencée)
                uc.CouleurBordure = Color.Red;
            }
            // Sinon la mission est actuellement en cours
            else
            {
                // On met la bordure du User Control en orange (mission en cours)
                uc.CouleurBordure = Color.Orange;
            }

            // On renvoie le User Control de la mission complètement configuré
            return uc;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void AfficherMissions(DataTable table)
        {
            // On vide le Panel des missions pour supprimer les anciens affichages
            pnlMission.Controls.Clear();

            // On définit la position de départ en X pour les éléments
            int posX = 100;

            // On définit la position de départ en Y pour les éléments
            int posY = 25;

            // Si le tableau de données ne contient aucune mission
            if (table.Rows.Count == 0)
            {
                // On crée un nouveau Label pour afficher un message vide
                Label lblAucune = new Label();

                // On active le redimensionnement automatique du Label
                lblAucune.AutoSize = true;

                // On change la couleur du texte en gris clair
                lblAucune.ForeColor = Color.LightGray;

                // On applique une police d'écriture en italique
                lblAucune.Font = new Font("Segoe UI", 10, FontStyle.Italic);

                // On positionne le Label dans le Panel
                lblAucune.Location = new Point(posX, posY);

                // Si la CheckBox des missions terminées est cochée
                if (chkMissionT.Checked)
                {
                    // On écrit que le catalogue de missions terminées est vide
                    lblAucune.Text = "Aucune mission terminée pour le moment";
                }
                // Sinon si la CheckBox des missions à venir est cochée
                else if (chkMissionAV.Checked)
                {
                    // On écrit que le catalogue de missions à venir est vide
                    lblAucune.Text = "Aucune mission à venir pour le moment";
                }
                // Sinon si la CheckBox des missions en cours est cochée
                else if (chkMissionEC.Checked)
                {
                    // On écrit que le catalogue de missions en cours est vide
                    lblAucune.Text = "Aucune mission en cours pour le moment";
                }
                // Si aucune de ces CheckBox n'est cochée
                else
                {
                    // On écrit un message d'absence général
                    lblAucune.Text = "Aucune mission disponible";
                }

                // On ajoute le Label de message vide dans le Panel des missions
                pnlMission.Controls.Add(lblAucune);

                // On sort de la méthode car il n'y a rien d'autre à afficher
                return;
            }

            // On parcourt chaque ligne du tableau de données des missions
            foreach (DataRow ligne in table.Rows)
            {
                // On appelle la méthode pour fabriquer le User Control de la mission
                uscMission uc = CreerUcMission(ligne);

                // On règle la position horizontale du User Control
                uc.Left = posX;

                // On règle la position verticale du User Control
                uc.Top = posY;

                // On injecte le User Control configuré dans le Panel des missions
                pnlMission.Controls.Add(uc);

                // On décale la position verticale vers le bas pour le prochain élément
                posY += uc.Height + 25;
            }

            // On active les barres de défilement sur le Panel des missions
            pnlMission.AutoScroll = true;

            // On définit la zone de défilement minimale pour ne pas couper le bas de la liste
            pnlMission.AutoScrollMinSize = new Size(0, posY + 8);
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ChargerMissions()
        {
            AfficherMissions(MesDatas.DsGlobal.Tables["Mission"]);
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void DecocherSauf(CheckBox saufCelle)
        {
            // On rassemble toutes les CheckBox de filtrage dans un tableau
            CheckBox[] toutesLesCheckboxes = new CheckBox[] { chkMissionT, chkMissionAV, chkMissionEC, chkBudget, chkObjectifDatabaz, chkNombreDeMembre };

            // On parcourt une par une les CheckBox de ce tableau
            foreach (CheckBox chk in toutesLesCheckboxes)
            {
                // Si la CheckBox en cours n'est pas celle qu'on veut garder active
                if (chk != saufCelle)
                {
                    // On force la CheckBox à se décocher
                    chk.Checked = false;
                }
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void chkMissionT_CheckedChanged(object sender, EventArgs e)
        {
            // Si la CheckBox des missions terminées vient d'être cochée
            if (chkMissionT.Checked)
            {
                // On décoche toutes les autres CheckBox sauf celle-ci
                DecocherSauf(chkMissionT);

                // On récupère la date d'aujourd'hui au format année-mois-jour
                string dateActuelle = DateTime.Now.ToString("yyyy-MM-dd");

                // On crée un tableau vide qui a la même structure que la table Mission
                DataTable tableFiltree = MesDatas.DsGlobal.Tables["Mission"].Clone();

                // On parcourt toutes les missions de la base de données
                foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
                {
                    // Si la date de retour est plus petite que la date du jour (mission passée)
                    if (string.Compare(ligne["dateRetour"].ToString(), dateActuelle) < 0)
                    {
                        // On copie cette ligne de mission dans notre tableau filtré
                        tableFiltree.ImportRow(ligne);
                    }
                }

                // On affiche uniquement les missions terminées qui ont été trouvées
                AfficherMissions(tableFiltree);
            }
            // Si la CheckBox vient d'être décochée
            else
            {
                // On recharge et on affiche toutes les missions sans filtre
                ChargerMissions();
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void chkMissionAV_CheckedChanged(object sender, EventArgs e)
        {
            // Si la CheckBox des missions à venir vient d'être cochée
            if (chkMissionAV.Checked)
            {
                // On décoche toutes les autres CheckBox sauf celle-ci
                DecocherSauf(chkMissionAV);

                // On récupère la date d'aujourd'hui au format année-mois-jour
                string dateActuelle = DateTime.Now.ToString("yyyy-MM-dd");

                // On crée un tableau vide qui a la même structure que la table Mission
                DataTable tableFiltree = MesDatas.DsGlobal.Tables["Mission"].Clone();

                // On parcourt toutes les missions de la base de données
                foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
                {
                    // Si la date de départ est plus grande que la date du jour (mission dans le futur)
                    if (string.Compare(ligne["dateDepart"].ToString(), dateActuelle) > 0)
                    {
                        // On copie cette ligne de mission dans notre tableau filtré
                        tableFiltree.ImportRow(ligne);
                    }
                }

                // On affiche uniquement les missions à venir qui ont été trouvées
                AfficherMissions(tableFiltree);
            }
            // Si la CheckBox vient d'être décochée
            else
            {
                // On recharge et on affiche toutes les missions sans filtre
                ChargerMissions();
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void chkMissionEC_CheckedChanged(object sender, EventArgs e)
        {
            // Si la CheckBox des missions en cours vient d'être cochée
            if (chkMissionEC.Checked)
            {
                // On décoche toutes les autres CheckBox sauf celle-ci
                DecocherSauf(chkMissionEC);

                // On récupère la date d'aujourd'hui au format année-mois-jour
                string dateActuelle = DateTime.Now.ToString("yyyy-MM-dd");

                // On crée un tableau vide qui a la même structure que la table Mission
                DataTable tableFiltree = MesDatas.DsGlobal.Tables["Mission"].Clone();

                // On parcourt toutes les missions de la base de données
                foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
                {
                    // Si la date de départ est passée ou égale à aujourd'hui ET que la date de retour est future ou égale à aujourd'hui
                    if (string.Compare(ligne["dateDepart"].ToString(), dateActuelle) <= 0 && string.Compare(ligne["dateRetour"].ToString(), dateActuelle) >= 0)
                    {
                        // On copie cette ligne de mission dans notre tableau filtré
                        tableFiltree.ImportRow(ligne);
                    }
                }

                // On affiche uniquement les missions en cours qui ont été trouvées
                AfficherMissions(tableFiltree);
            }
            // Si la CheckBox vient d'être décochée
            else
            {
                // On recharge et on affiche toutes les missions sans filtre
                ChargerMissions();
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void chkNombreDeMembre_CheckedChanged(object sender, EventArgs e)
        {
            // Si la CheckBox du nombre de membres vient d'être cochée
            if (chkNombreDeMembre.Checked)
            {
                // On décoche toutes les autres CheckBox sauf celle-ci
                DecocherSauf(chkNombreDeMembre);

                // On applique un tri par le nombre de membres requis sur la vue par défaut du tableau
                MesDatas.DsGlobal.Tables["Mission"].DefaultView.Sort = "nbMembreRequis";

                // On affiche les missions triées en convertissant la vue en un nouveau tableau
                AfficherMissions(MesDatas.DsGlobal.Tables["Mission"].DefaultView.ToTable());
            }
            // Si la CheckBox vient d'être décochée
            else
            {
                // On réinitialise le tri de la vue par défaut pour l'annuler
                MesDatas.DsGlobal.Tables["Mission"].DefaultView.Sort = "";

                // On recharge et on affiche toutes les missions sans tri
                ChargerMissions();
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void chkObjectifDatabaz_CheckedChanged(object sender, EventArgs e)
        {
            // Si la CheckBox de l'objectif Databaz vient d'être cochée
            if (chkObjectifDatabaz.Checked)
            {
                // On décoche toutes les autres CheckBox sauf celle-ci
                DecocherSauf(chkObjectifDatabaz);

                // On applique un tri par l'objectif Databaz sur la vue par défaut du tableau
                MesDatas.DsGlobal.Tables["Mission"].DefaultView.Sort = "objectifDatabaz";

                // On affiche les missions triées en convertissant la vue en un nouveau tableau
                AfficherMissions(MesDatas.DsGlobal.Tables["Mission"].DefaultView.ToTable());
            }
            // Si la CheckBox vient d'être décochée
            else
            {
                // On réinitialise le tri de la vue par défaut pour l'annuler
                MesDatas.DsGlobal.Tables["Mission"].DefaultView.Sort = "";

                // On recharge et on affiche toutes les missions sans tri
                ChargerMissions();
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void chkBudget_CheckedChanged(object sender, EventArgs e)
        {
            // Si la CheckBox du budget vient d'être cochée
            if (chkBudget.Checked)
            {
                // On décoche toutes les autres CheckBox sauf celle-ci
                DecocherSauf(chkBudget);

                // On applique un tri par le budget sur la vue par défaut du tableau
                MesDatas.DsGlobal.Tables["Mission"].DefaultView.Sort = "budget";

                // On affiche les missions triées en convertissant la vue en un nouveau tableau
                AfficherMissions(MesDatas.DsGlobal.Tables["Mission"].DefaultView.ToTable());
            }
            // Si la CheckBox vient d'être décochée
            else
            {
                // On réinitialise le tri de la vue par défaut pour l'annuler
                MesDatas.DsGlobal.Tables["Mission"].DefaultView.Sort = "";

                // On recharge et on affiche toutes les missions sans tri
                ChargerMissions();
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ChargerFicheMission(string nomMission)
        {
            // On masque le Panel des boutons de filtrage
            pnlBoutonMission.Visible = false;

            // On masque le Panel contenant la liste des missions
            pnlMission.Visible = false;

            // On rend le GroupBox de la fiche de mission visible
            grpFicheMission.Visible = true;

            // On passe le GroupBox au premier plan de l'affichage
            grpFicheMission.BringToFront();

            // On prépare une variable pour stocker la ligne de la mission trouvée
            DataRow ligneMission = null;

            // On parcourt toutes les lignes de la table Mission
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
            {
                // Si le nom reconstruit de la mission correspond à celui recherché
                if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == nomMission)
                {
                    // On sauvegarde la ligne de cette mission
                    ligneMission = ligne;

                    // On arrête la recherche dans la boucle
                    break;
                }
            }

            // Si la mission a bien été trouvée dans la base
            if (ligneMission != null)
            {
                // On affiche le nom de la mission dans le Label correspondant
                lblNomMission.Text = nomMission;

                // On charge l'image de la planète dans le PictureBox depuis le dossier
                pcbImageFicheMission.Image = Image.FromFile($@"img\Planetes\{ligneMission["nomPlanete"].ToString()}.png");

                // On règle le mode d'affichage de l'image pour qu'elle s'adapte sans déformer
                pcbImageFicheMission.SizeMode = PictureBoxSizeMode.Zoom;

                // On affiche le budget initial dans son Label avec le symbole euro
                lblBudget.Text = "Budget : " + ligneMission["budget"] + " €";

                // On affiche la date de départ formatée dans son Label
                lblDateDepart.Text = "Date de départ : " + FormatDate(ligneMission["dateDepart"].ToString());

                // On affiche la date de retour formatée dans son Label
                lblDateRetour.Text = "Date de retour : " + FormatDate(ligneMission["dateRetour"].ToString());

                // On remplit la zone de texte RichTextBox avec la feuille de route
                rtbFeuilleDeRoute.Text = ligneMission["feuilleDeRoute"].ToString();

                // On vide le FlowLayoutPanel des captures pour nettoyer les anciens affichages
                flpObjectifsCapture.Controls.Clear();

                // On configure le FlowLayoutPanel pour aligner les éléments du haut vers le bas
                flpObjectifsCapture.FlowDirection = FlowDirection.TopDown;

                // On désactive le retour à la ligne automatique dans le FlowLayoutPanel
                flpObjectifsCapture.WrapContents = false;

                // On parcourt toutes les lignes de la table des captures
                foreach (DataRow ligneCapture in MesDatas.DsGlobal.Tables["Capturer"].Rows)
                {
                    // Si la capture correspond à la planète ET au numéro de la mission actuelle
                    if (ligneCapture["nomPlanete"].ToString() == ligneMission["nomPlanete"].ToString() &&
                        Convert.ToInt32(ligneCapture["numeroMission"]) == Convert.ToInt32(ligneMission["numero"]))
                    {
                        // On récupère l'identifiant de l'espèce ennemie
                        string idEspece = ligneCapture["idEspeceEnnemi"].ToString();

                        // On prépare une variable pour le nom de l'alien
                        string nomAlien = "";

                        // On parcourt la table des espèces pour retrouver le nom de l'alien
                        foreach (DataRow ligneAlien in MesDatas.DsGlobal.Tables["Espece"].Rows)
                        {
                            // Si l'identifiant correspond à celui recherché
                            if (ligneAlien["id"].ToString() == idEspece)
                            {
                                // On récupère le nom textuel de l'alien
                                nomAlien = ligneAlien["nom"].ToString();

                                // On arrête la recherche de l'espèce
                                break;
                            }
                        }

                        // On crée un nouveau Label pour afficher cette capture
                        Label lblCapture = new Label();

                        // On compose le texte du Label avec le nom de l'alien et le nombre
                        lblCapture.Text = nomAlien + " --> " + ligneCapture["nombre"].ToString() + " prise(s)";

                        // On active la taille automatique du Label
                        lblCapture.AutoSize = true;

                        // On définit les marges autour du Label
                        lblCapture.Margin = new Padding(3, 3, 3, 3);

                        // On applique un style de police en gras sur ce Label
                        lblCapture.Font = new Font(lblCapture.Font, FontStyle.Bold);

                        // On ajoute le Label de capture dans le FlowLayoutPanel
                        flpObjectifsCapture.Controls.Add(lblCapture);
                    }
                }

                // Si le FlowLayoutPanel ne contient aucun élément après la boucle
                if (flpObjectifsCapture.Controls.Count == 0)
                {
                    // On crée un nouveau Label pour indiquer l'absence de capture
                    Label lblAucuneCapture = new Label();

                    // On écrit le message d'absence de capture dans le Label
                    lblAucuneCapture.Text = "Aucune capture effectuée pour l'instant";

                    // On active la taille automatique du Label
                    lblAucuneCapture.AutoSize = true;

                    // On change la couleur du texte en gris clair
                    lblAucuneCapture.ForeColor = Color.LightGray;

                    // On applique une police d'écriture en italique
                    lblAucuneCapture.Font = new Font(lblAucuneCapture.Font, FontStyle.Italic);

                    // On ajoute ce Label d'information dans le FlowLayoutPanel
                    flpObjectifsCapture.Controls.Add(lblAucuneCapture);
                }
            }

            // On initialise le compteur de la somme des dépenses à zéro
            int sommeDepenses = 0;

            // On parcourt toutes les lignes de la table des dépenses
            foreach (DataRow ligneDepense in MesDatas.DsGlobal.Tables["Depense"].Rows)
            {
                // Si la dépense concerne la même planète et le même numéro de mission
                if (ligneDepense["nomPlanete"].ToString() == ligneMission["nomPlanete"].ToString() &&
                    Convert.ToInt32(ligneDepense["numeroMission"]) == Convert.ToInt32(ligneMission["numero"]))
                {
                    // On ajoute le montant de la dépense au compteur total
                    sommeDepenses += Convert.ToInt32(ligneDepense["montant"]);
                }
            }

            // On initialise le compteur de la somme des frais de contacts à zéro
            int sommeContacts = 0;

            // On parcourt toutes les lignes de la table des contacts
            foreach (DataRow ligneContact in MesDatas.DsGlobal.Tables["Contact"].Rows)
            {
                // Si le contact concerne la même planète et le même numéro de mission
                if (ligneContact["nomPlanete"].ToString() == ligneMission["nomPlanete"].ToString() &&
                    Convert.ToInt32(ligneContact["numeroMission"]) == Convert.ToInt32(ligneMission["numero"]))
                {
                    // On ajoute la somme versée au contact au compteur total
                    sommeContacts += Convert.ToInt32(ligneContact["sommeVersee"]);
                }
            }

            // On convertit le budget de la mission en nombre entier
            int budget = Convert.ToInt32(ligneMission["budget"]);

            // On calcule l'argent restant en soustrayant les dépenses et les contacts du budget
            int solde = budget - sommeDepenses - sommeContacts;

            // On écrit le libellé fixe dans le Label textuel du solde
            lblSoldeTexte.Text = "Solde après dépenses : ";

            // On affiche la valeur numérique calculée dans le Label du solde avec l'euro
            lblSoldeValeur.Text = solde + " €";

            // Si le solde est positif mais qu'il reste moins de 500 euros
            if (solde >= 0 && solde < 500)
            {
                // On colorie la valeur du solde en rouge pour alerter
                lblSoldeValeur.ForeColor = Color.Red;
            }
            // Sinon si le solde est compris entre 500 et 2500 euros inclus
            else if (solde <= 2500 && solde >= 500)
            {
                // On colorie la valeur du solde en orange pour attention
                lblSoldeValeur.ForeColor = Color.Orange;
            }
            // Si le solde est supérieur à 2500 euros
            else
            {
                // On colorie la valeur du solde en vert (tout va bien)
                lblSoldeValeur.ForeColor = Color.LimeGreen;
            }


            // On vide le FlowLayoutPanel des membres de l'équipage
            flpMembreEquipage.Controls.Clear();

            // On configure le FlowLayoutPanel pour aligner les éléments de gauche à droite
            flpMembreEquipage.FlowDirection = FlowDirection.LeftToRight;

            // On active le retour à la ligne automatique dans le FlowLayoutPanel
            flpMembreEquipage.WrapContents = true;

            // On récupère le matricule du chef de mission
            string matriculeChef = ligneMission["matriculeChef"].ToString();

            // On parcourt la table Composer pour trouver qui participe à la mission
            foreach (DataRow membreMission in MesDatas.DsGlobal.Tables["Composer"].Rows)
            {
                // Si l'association correspond à la planète ET au numéro de la mission actuelle
                if (membreMission["nomPlanete"].ToString() == ligneMission["nomPlanete"].ToString() &&
                    Convert.ToInt32(membreMission["numeroMission"]) == Convert.ToInt32(ligneMission["numero"]))
                {
                    // On récupère le matricule du membre d'équipage
                    string matriculeMembre = membreMission["matriculeMembre"].ToString();

                    // On prépare une variable pour son nom complet
                    string nomMembre = "";

                    // On cherche l'identité du membre dans la table des membres
                    foreach (DataRow ligneMembre in MesDatas.DsGlobal.Tables["Membre"].Rows)
                    {
                        // Si le matricule correspond
                        if (ligneMembre["matricule"].ToString() == matriculeMembre)
                        {
                            // On assemble son nom et son prénom avec un retour à la ligne entre les deux
                            nomMembre = ligneMembre["nom"] + "\n" + ligneMembre["prenom"];

                            // On arrête la recherche de ce membre
                            break;
                        }
                    }

                    // On crée un Panel pour regrouper le visuel de cette personne
                    Panel pnlMembre = new Panel();

                    // On définit la largeur et la hauteur du Panel
                    pnlMembre.Size = new Size(110, 100);

                    // On applique la même couleur de fond que le conteneur principal
                    pnlMembre.BackColor = flpMembreEquipage.BackColor;

                    // On applique une marge de sécurité tout autour du Panel
                    pnlMembre.Margin = new Padding(10);

                    // On crée un PictureBox pour afficher la photo ou l'icône du membre
                    PictureBox pcb = new PictureBox();

                    // On définit la taille carrée du PictureBox
                    pcb.Size = new Size(45, 45);

                    // On positionne le PictureBox en haut au centre du Panel
                    pcb.Location = new Point(21, 0);

                    // On règle l'affichage pour adapter l'icône sans la déformer
                    pcb.SizeMode = PictureBoxSizeMode.Zoom;

                    // Si le membre actuel est le chef de la mission
                    if (matriculeMembre == matriculeChef)
                    {
                        // On lui charge l'icône spécifique du capitaine
                        pcb.Image = Image.FromFile(@"img\Icone\imageCapitaineFondBlanc.png");
                    }
                    // Si c'est un membre d'équipage classique
                    else
                    {
                        // On lui charge l'icône classique de l'utilisateur bleu
                        pcb.Image = Image.FromFile(@"img\Icone\imageUtilisateurBleuMarine.png");
                    }

                    // On crée un outil géométrique pour découper l'image
                    System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

                    // On dessine un cercle parfait de la taille du PictureBox
                    path.AddEllipse(0, 0, pcb.Width, pcb.Height);

                    // On applique ce cercle pour donner une forme arrondie au PictureBox
                    pcb.Region = new Region(path);

                    // On crée un Label pour inscrire le nom et prénom sous la photo
                    Label lbl = new Label();

                    // On règle la taille du Label pour qu'il tienne dans le Panel
                    lbl.Size = new Size(90, 30);

                    // On place le Label juste en dessous du PictureBox
                    lbl.Location = new Point(0, 45);

                    // On affecte le texte du nom et prénom construit précédemment
                    lbl.Text = nomMembre;

                    // On aligne le texte parfaitement au centre du Label
                    lbl.TextAlign = ContentAlignment.MiddleCenter;

                    // On applique la police d'écriture en gras et de taille réduite
                    lbl.Font = new Font("Segoe UI", 7, FontStyle.Bold);

                    // On écrit le texte du nom en blanc
                    lbl.ForeColor = Color.White;

                    // On ajoute le PictureBox de l'icône dans le Panel du membre
                    pnlMembre.Controls.Add(pcb);

                    // On ajoute le Label du nom dans le Panel du membre
                    pnlMembre.Controls.Add(lbl);

                    // Si ce membre est le chef de la mission
                    if (matriculeMembre == matriculeChef)
                    {
                        // On l'ajoute dans le FlowLayoutPanel global
                        flpMembreEquipage.Controls.Add(pnlMembre);

                        // On force sa position à l'index 0 pour qu'il apparaisse tout à gauche de la liste
                        flpMembreEquipage.Controls.SetChildIndex(pnlMembre, 0);
                    }
                    // Si c'est un membre classique
                    else
                    {
                        // On l'ajoute simplement à la suite dans le FlowLayoutPanel global
                        flpMembreEquipage.Controls.Add(pnlMembre);
                    }
                }
            }

            // On appelle la méthode pour activer ou bloquer les boutons d'action de la fiche
            GererBoutonsFicheMission(ligneMission);
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private async void AfficherMessage(string message, Color couleur)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = couleur;
            lblMessage.Visible = true;

            await Task.Delay(3000);

            lblMessage.Visible = false;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnNouveauContact_Click(object sender, EventArgs e)
        {
            // On change le titre du GroupBox d'ajout
            grpAjoutNouvelMission.Text = "Nouveau contact";

            // On affiche le Panel pour créer un contact
            pnlNouveauContact.Visible = true;

            // On masque le Panel de création de dépense
            pnlNouvelleDepense.Visible = false;

            // On masque le Panel de création d'événement
            pnlNouvelleEvenement.Visible = false;

            // On masque le Panel de création de capture
            pnlNouvelleCapture.Visible = false;

            // On efface les messages d'alerte de l'ErrorProvider de la date
            erpDate.Clear();

            // On efface les messages d'alerte de l'ErrorProvider de la somme
            erpSomme.Clear();

            // On efface les messages d'alerte de l'ErrorProvider de l'appréciation
            erpAppreciation.Clear();

            // On efface les messages d'alerte de l'ErrorProvider de l'informateur
            erpInformateur.Clear();

            // On ajoute un texte d'aide sur le bouton de validation du contact
            tltValiderContact.SetToolTip(btnValiderContact, "Valider le contact");

            // On ajoute un texte d'aide sur le bouton d'annulation du contact
            tltAnnulerContact.SetToolTip(btnAnnulerMission, "Annuler le contact");

            // On remet la date du jour dans le DateTimePicker
            dtpDate.Value = DateTime.Today;

            // On vide le TextBox de la somme
            txtSomme.Clear();

            // On vide le RichTextBox de l'appréciation
            rtbAppreciation.Clear();

            // On décoche l'élément sélectionné dans la ComboBox de l'informateur
            cboInformateur.SelectedIndex = -1;

            // On vide tous les choix de la ComboBox de l'informateur
            cboInformateur.Items.Clear();

            // On parcourt toutes les lignes de la table Informateur
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Informateur"].Rows)
            {
                // On ajoute le nom de l'informateur dans la ComboBox
                cboInformateur.Items.Add(ligne["nom"]);
            }
        }

        // ##############################################################################################################################################################################################################################################

        private void txtSomme_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            // On désactive les raccourcis clavier
            txtSomme.ShortcutsEnabled = false;

            // On vérifie si la touche est un chiffre ou s'il s'agit de la touche Backspace
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                // On vérifie si l'utilisateur tape un 0 alors que le champ est encore complètement vide
                if (e.KeyChar == '0' && txtSomme.Text.Length == 0)
                {
                    // On maintient le blocage pour empêcher le budget de commencer par un zéro
                    e.Handled = true;
                }
                // On vérifie si le texte atteint déjà 7 caractères et que la touche pressée n'est pas la touche Backspace
                else if (txtSomme.Text.Length >= 5 && e.KeyChar != (char)Keys.Back)
                {
                    // On bloque la saisie pour limiter le budget à une longueur maximale de 7 chiffres
                    e.Handled = true;
                }
                // Si la touche est valide
                else
                {
                    // On autorise la saisie en passant Handled à faux
                    e.Handled = false;
                }
            }
        }

        // ##############################################################################################################################################################################################################################################

        private void btnValiderContact_Click(object sender, EventArgs e)
        {
            // On initialise un indicateur pour vérifier si toutes les données saisies sont valides
            Boolean contactValide = true;

            // On efface les anciens messages d'alerte sur tous les ErrorProvider
            erpDate.Clear();
            erpSomme.Clear();
            erpAppreciation.Clear();
            erpInformateur.Clear();

            // On prépare les variables pour stocker les informations de la mission actuelle
            string nomPlanete = "";
            int numeroMission = 0;
            DateTime dateFinMission = DateTime.MaxValue;

            // On parcourt la table Mission pour extraire les détails de la mission sélectionnée
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
            {
                // Si la mission correspond au nom affiché sur le Label du formulaire
                if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == lblNomMission.Text)
                {
                    nomPlanete = ligne["nomPlanete"].ToString();
                    numeroMission = Convert.ToInt32(ligne["numero"]);

                    // Si une date de retour existe, on la récupère pour la validation
                    if (ligne["dateRetour"] != DBNull.Value)
                    {
                        dateFinMission = Convert.ToDateTime(ligne["dateRetour"]);
                    }
                    break;
                }
            }

            // On vérifie que la date choisie dans le DateTimePicker n'est pas dans le passé
            if (dtpDate.Value.Date < DateTime.Today)
            {
                erpDate.SetIconPadding(dtpDate, 10);
                erpDate.SetError(dtpDate, "La date du contact ne peut pas être antérieure à aujourd'hui");
                contactValide = false;
            }
            // On vérifie que la date choisie ne dépasse pas la date de fin de la mission
            else if (dtpDate.Value.Date > dateFinMission.Date)
            {
                erpDate.SetIconPadding(dtpDate, 10);
                erpDate.SetError(dtpDate, "La date du contact ne peut pas dépasser la date de fin de la mission");
                contactValide = false;
            }

            // On vérifie que le TextBox de la somme n'est pas vide
            if (txtSomme.Text == string.Empty)
            {
                erpSomme.SetIconPadding(txtSomme, 10);
                erpSomme.SetError(txtSomme, "Veuillez saisir la somme versée à l'informateur");
                contactValide = false;
            }

            // On vérifie que le RichTextBox de l'appréciation n'est pas vide
            if (rtbAppreciation.Text == string.Empty)
            {
                erpAppreciation.SetIconPadding(rtbAppreciation, 10);
                erpAppreciation.SetError(rtbAppreciation, "Veuillez saisir votre appréciation sur ce contact");
                contactValide = false;
            }

            // On vérifie qu'un élément est bien sélectionné dans la ComboBox de l'informateur
            if (cboInformateur.SelectedIndex == -1)
            {
                erpInformateur.SetIconPadding(cboInformateur, 10);
                erpInformateur.SetError(cboInformateur, "Veuillez sélectionner un informateur");
                contactValide = false;
            }

            // Si une somme a été saisie, on effectue le contrôle du budget disponible
            if (txtSomme.Text != string.Empty)
            {
                int sommeVerif = int.Parse(txtSomme.Text);
                int soldeCourant = 0;

                // On parcourt à nouveau les missions pour calculer l'argent restant disponible
                foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
                {
                    if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == lblNomMission.Text)
                    {
                        int budgetMission = Convert.ToInt32(ligne["budget"]);
                        int totalDepenses = 0;
                        int totalContacts = 0;

                        // On calcule le cumul des dépenses déjà enregistrées pour cette mission
                        foreach (DataRow ld in MesDatas.DsGlobal.Tables["Depense"].Rows)
                        {
                            if (ld["nomPlanete"].ToString() == nomPlanete && Convert.ToInt32(ld["numeroMission"]) == numeroMission)
                                totalDepenses += Convert.ToInt32(ld["montant"]);
                        }

                        // On calcule le cumul des sommes déjà versées aux contacts pour cette mission
                        foreach (DataRow lc in MesDatas.DsGlobal.Tables["Contact"].Rows)
                        {
                            if (lc["nomPlanete"].ToString() == nomPlanete && Convert.ToInt32(lc["numeroMission"]) == numeroMission)
                                totalContacts += Convert.ToInt32(lc["sommeVersee"]);
                        }

                        // On détermine le solde courant de la mission
                        soldeCourant = budgetMission - totalDepenses - totalContacts;
                        break;
                    }
                }

                // Si la nouvelle somme dépasse l'argent disponible, on lève une alerte
                if (soldeCourant - sommeVerif < 0)
                {
                    erpSomme.SetIconPadding(txtSomme, 10);
                    erpSomme.SetError(txtSomme, "Solde insuffisant, cette somme dépasserait le budget disponible");
                    contactValide = false;
                }
            }

            // Si toutes les vérifications de saisie sont correctes
            if (contactValide == true)
            {
                try
                {
                    // On convertit et stocke les données prêtes à être insérées
                    string dateContact = dtpDate.Value.ToString("yyyy-MM-dd");
                    int somme = int.Parse(txtSomme.Text);
                    string appreciation = rtbAppreciation.Text;

                    string nomCodeInformateur = "";

                    // On détermine le code trigramme de l'informateur selon la sélection de la ComboBox
                    switch (cboInformateur.SelectedItem.ToString())
                    {
                        case "neugleh'L évreH": nomCodeInformateur = "HL"; break;
                        case "drahciR euqinoréV": nomCodeInformateur = "VR"; break;
                        case "assorgerroT elleiruM": nomCodeInformateur = "MT"; break;
                        case "nnamremmiZ ueihtaM": nomCodeInformateur = "MZ"; break;
                        case "idayA ilA": nomCodeInformateur = "AA"; break;
                        case "nirreP niamoR": nomCodeInformateur = "RP"; break;
                        case "telarB eniotnA": nomCodeInformateur = "AB"; break;
                        case "hatfeM lenaM": nomCodeInformateur = "MM"; break;
                        case "relssew cirE": nomCodeInformateur = "EW"; break;
                        default: nomCodeInformateur = "XX"; break;
                    }

                    // On prépare une requête SQL pour vérifier s'il existe déjà un contact à cette date
                    string requeteVerif = @"SELECT COUNT(*) FROM Contact                                     WHERE nomPlanete = @nomPlanete                                     AND numeroMission = @numeroMission                                     AND dateC = @dateC";

                    SQLiteCommand cmdVerif = new SQLiteCommand(requeteVerif, Connexion.Connec);
                    cmdVerif.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmdVerif.Parameters.AddWithValue("@numeroMission", numeroMission);
                    cmdVerif.Parameters.AddWithValue("@dateC", dateContact);

                    // On exécute la requête de vérification en récupérant le résultat numérique
                    int nbContacts = Convert.ToInt32(cmdVerif.ExecuteScalar());

                    // Si un contact existe déjà à cette date précise, on stoppe l'enregistrement
                    if (nbContacts > 0)
                    {
                        erpDate.SetIconPadding(dtpDate, 10);
                        erpDate.SetError(dtpDate, "Un contact existe déjà à cette date pour cette mission");
                        contactValide = false;
                        return;
                    }

                    // On prépare la requête SQL d'insertion pour créer le nouveau contact en base
                    string requeteContact = @"INSERT INTO Contact (nomPlanete, numeroMission, dateC, sommeVersee, appreciation, nomCodeInformateur)                                       VALUES (@nomPlanete, @numeroMission, @dateC, @sommeVersee, @appreciation, @nomCodeInformateur)";

                    SQLiteCommand cmdContact = new SQLiteCommand(requeteContact, Connexion.Connec);
                    cmdContact.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmdContact.Parameters.AddWithValue("@numeroMission", numeroMission);
                    cmdContact.Parameters.AddWithValue("@dateC", dateContact);
                    cmdContact.Parameters.AddWithValue("@sommeVersee", somme);
                    cmdContact.Parameters.AddWithValue("@appreciation", appreciation);
                    cmdContact.Parameters.AddWithValue("@nomCodeInformateur", nomCodeInformateur);

                    // On exécute la commande SQL d'insertion
                    cmdContact.ExecuteNonQuery();

                    // On met à jour l'application avec les nouvelles données de la base
                    UpdateDuDataset();

                    // On recharge la fiche de mission actuelle pour rafraîchir les affichages
                    ChargerFicheMission(lblNomMission.Text);

                    // On réinitialise tous les composants du formulaire de saisie
                    dtpDate.Value = DateTime.Today;
                    txtSomme.Clear();
                    rtbAppreciation.Clear();
                    cboInformateur.SelectedIndex = -1;

                    // On masque tous les panels de saisie
                    pnlNouveauContact.Visible = false;
                    pnlNouvelleDepense.Visible = false;
                    pnlNouvelleEvenement.Visible = false;
                    pnlNouvelleCapture.Visible = false;

                    // On remet le titre du GroupBox à son état par défaut
                    grpAjoutNouvelMission.Text = "Veuillez choisir une action";

                    // On affiche le message de succès
                    AfficherMessage("✔ Contact ajouté avec succès !", Color.Green);
                }
                catch (Exception monErreur)
                {
                    // On remonte un message en cas d'échec ou d'anomalie durant l'accès à la base
                    AfficherMessage("✖ Erreur : " + monErreur.Message, Color.Red);
                }
            }
        }

        // ##############################################################################################################################################################################################################################################

        private void btnAnnulerMission_Click(object sender, EventArgs e)
        {
            // On efface les messages d'alerte de tous les ErrorProvider
            erpDate.Clear();
            erpSomme.Clear();
            erpAppreciation.Clear();
            erpInformateur.Clear();

            // On réinitialise tous les composants du formulaire de saisie à leur état d'origine
            dtpDate.Value = DateTime.Today;
            txtSomme.Clear();
            rtbAppreciation.Clear();
            cboInformateur.SelectedIndex = -1;

            // On masque le Panel de création du contact pour fermer le formulaire
            pnlNouveauContact.Visible = false;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnNouvelleDepense_Click(object sender, EventArgs e)
        {
            // On change le titre du GroupBox d'ajout
            grpAjoutNouvelMission.Text = "Nouvelle dépense";

            // On affiche le Panel pour créer une dépense
            pnlNouvelleDepense.Visible = true;

            // On masque le Panel de création de contact
            pnlNouveauContact.Visible = false;

            // On masque le Panel de création d'événement
            pnlNouvelleEvenement.Visible = false;

            // On masque le Panel de création de capture
            pnlNouvelleCapture.Visible = false;

            // On efface les messages d'alerte de l'ErrorProvider de la date de dépense
            erpDateDepense.Clear();

            // On efface les messages d'alerte de l'ErrorProvider de la somme de dépense
            erpSommeDepense.Clear();

            // On efface les messages d'alerte de l'ErrorProvider du motif de dépense
            erpMotifDepense.Clear();

            // On efface les messages d'alerte de l'ErrorProvider du type de dépense
            erpTypeDepense.Clear();

            // On ajoute un texte d'aide sur le bouton de validation de la dépense
            tltAjouterDepense.SetToolTip(btnValiderDepense, "Valider la dépense");

            // On ajoute un texte d'aide sur le bouton d'annulation de la dépense
            tltAnnulerDepense.SetToolTip(btnAnnulerDepense, "Annuler la dépense");

            // On remet la date du jour dans le DateTimePicker de la dépense
            dtpDateDepense.Value = DateTime.Today;

            // On vide le TextBox de la somme de la dépense
            txtSommeDepense.Clear();

            // On vide le RichTextBox du motif de la dépense
            rtbMotifDepense.Clear();

            // On décoche l'élément sélectionné dans la ComboBox du type de dépense
            cboTypeDepense.SelectedIndex = -1;

            // On vide tous les choix de la ComboBox du type de dépense
            cboTypeDepense.Items.Clear();

            // On parcourt toutes les lignes de la table TypeDepense
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["TypeDepense"].Rows)
            {
                // On ajoute le libellé du type de dépense dans la ComboBox
                cboTypeDepense.Items.Add(ligne["libelle"]);
            }
        }

        // ##############################################################################################################################################################################################################################################

        private void txtSommeDepense_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            // On désactive les raccourcis clavier
            txtSommeDepense.ShortcutsEnabled = false;

            // On vérifie si la touche est un chiffre ou s'il s'agit de la touche Backspace
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                // On vérifie si l'utilisateur tape un 0 alors que le champ est encore complètement vide
                if (e.KeyChar == '0' && txtSommeDepense.Text.Length == 0)
                {
                    // On maintient le blocage pour empêcher le budget de commencer par un zéro
                    e.Handled = true;
                }
                // On vérifie si le texte atteint déjà 7 caractères et que la touche pressée n'est pas la touche Backspace
                else if (txtSommeDepense.Text.Length >= 5 && e.KeyChar != (char)Keys.Back)
                {
                    // On bloque la saisie pour limiter le budget à une longueur maximale de 7 chiffres
                    e.Handled = true;
                }
                // Si la touche est valide
                else
                {
                    // On autorise la saisie en passant Handled à faux
                    e.Handled = false;
                }
            }
        }

        // ##############################################################################################################################################################################################################################################

        private void btnValiderDepense_Click(object sender, EventArgs e)
        {
            // On initialise un indicateur pour vérifier si toutes les données de la dépense sont valides
            Boolean depenseValide = true;

            // On efface les anciens messages d'alerte sur tous les ErrorProvider de la dépense
            erpDateDepense.Clear();
            erpSommeDepense.Clear();
            erpMotifDepense.Clear();
            erpTypeDepense.Clear();

            // On prépare les variables pour stocker les informations de la mission actuelle
            string nomPlanete = "";
            int numeroMission = 0;
            DateTime dateFinMission = DateTime.MaxValue;

            // On parcourt la table Mission pour extraire les détails de la mission sélectionnée
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
            {
                // Si la mission correspond au nom affiché sur le Label du formulaire
                if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == lblNomMission.Text)
                {
                    nomPlanete = ligne["nomPlanete"].ToString();
                    numeroMission = Convert.ToInt32(ligne["numero"]);

                    // Si une date de retour existe, on la récupère pour la validation
                    if (ligne["dateRetour"] != DBNull.Value)
                    {
                        dateFinMission = Convert.ToDateTime(ligne["dateRetour"]);
                    }
                    break;
                }
            }

            // On vérifie que la date choisie dans le DateTimePicker n'est pas dans le passé
            if (dtpDateDepense.Value.Date < DateTime.Today)
            {
                erpDateDepense.SetIconPadding(dtpDateDepense, 10);
                erpDateDepense.SetError(dtpDateDepense, "La date de la dépense ne peut pas être antérieure à aujourd'hui");
                depenseValide = false;
            }
            // On vérifie que la date de la dépense ne dépasse pas la date de fin de la mission
            else if (dtpDateDepense.Value.Date > dateFinMission.Date)
            {
                erpDateDepense.SetIconPadding(dtpDateDepense, 10);
                erpDateDepense.SetError(dtpDateDepense, "La date de la dépense ne peut pas dépasser la date de fin de la mission");
                depenseValide = false;
            }

            // On vérifie que le TextBox de la somme n'est pas vide
            if (txtSommeDepense.Text == string.Empty)
            {
                erpSommeDepense.SetIconPadding(txtSommeDepense, 10);
                erpSommeDepense.SetError(txtSommeDepense, "Vuvillez saisir la somme de la dépense");
                depenseValide = false;
            }

            // On vérifie que le RichTextBox du motif n'est pas vide
            if (rtbMotifDepense.Text == string.Empty)
            {
                erpMotifDepense.SetIconPadding(rtbMotifDepense, 10);
                erpMotifDepense.SetError(rtbMotifDepense, "Veuillez saisir le motif de votre dépense");
                depenseValide = false;
            }

            // On vérifie qu'un élément est bien sélectionné dans la ComboBox du type de dépense
            if (cboTypeDepense.SelectedIndex == -1)
            {
                erpTypeDepense.SetIconPadding(cboTypeDepense, 10);
                erpTypeDepense.SetError(cboTypeDepense, "Veuillez sélectionner un type de dépense");
                depenseValide = false;
            }

            // Si une somme a été saisie, on effectue le contrôle du budget disponible
            if (txtSommeDepense.Text != string.Empty)
            {
                int montantVerif = int.Parse(txtSommeDepense.Text);
                int soldeCourant = 0;

                // On parcourt à nouveau les missions pour calculer l'argent restant disponible
                foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
                {
                    if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == lblNomMission.Text)
                    {
                        int budgetMission = Convert.ToInt32(ligne["budget"]);
                        int totalDepenses = 0;
                        int totalContacts = 0;

                        // On calcule le cumul des dépenses déjà enregistrées pour cette mission
                        foreach (DataRow ld in MesDatas.DsGlobal.Tables["Depense"].Rows)
                        {
                            if (ld["nomPlanete"].ToString() == nomPlanete && Convert.ToInt32(ld["numeroMission"]) == numeroMission)
                            {
                                totalDepenses += Convert.ToInt32(ld["montant"]);
                            }
                        }

                        // On calcule le cumul des sommes déjà versées aux contacts pour cette mission
                        foreach (DataRow lc in MesDatas.DsGlobal.Tables["Contact"].Rows)
                        {
                            if (lc["nomPlanete"].ToString() == nomPlanete && Convert.ToInt32(lc["numeroMission"]) == numeroMission)
                            {
                                totalContacts += Convert.ToInt32(lc["sommeVersee"]);
                            }
                        }

                        // On détermine le solde courant de la mission
                        soldeCourant = budgetMission - totalDepenses - totalContacts;
                        break;
                    }
                }

                // Si la nouvelle dépense dépasse l'argent disponible, on lève une alerte
                if (soldeCourant - montantVerif < 0)
                {
                    erpSommeDepense.SetIconPadding(txtSommeDepense, 10);
                    erpSommeDepense.SetError(txtSommeDepense, "Solde insuffisant, cette dépense dépasserait le budget disponible");
                    depenseValide = false;
                }
            }

            // Si toutes les vérifications de saisie sont correctes
            if (depenseValide == true)
            {
                try
                {
                    // On convertit et stocke les données prêtes à être insérées
                    string dateDepense = dtpDateDepense.Value.ToString("yyyy-MM-dd");
                    int montant = int.Parse(txtSommeDepense.Text);
                    string motif = rtbMotifDepense.Text;

                    int identifiantDepense;
                    // On détermine l'ID numérique du type de dépense selon la sélection de la ComboBox
                    switch (cboTypeDepense.SelectedItem.ToString())
                    {
                        case "DataBaz": identifiantDepense = 1; break;
                        case "Informateur": identifiantDepense = 2; break;
                        case "Réparation": identifiantDepense = 3; break;
                        case "Droit de passage": identifiantDepense = 4; break;
                        default: identifiantDepense = 0; break;
                    }

                    // On prépare une requête SQL pour compter les dépenses de la planète afin de générer le nouvel ID
                    string requeteCompter = @"SELECT COUNT(*) FROM Depense WHERE nomPlanete = @nomPlanete";

                    SQLiteCommand cmdCompter = new SQLiteCommand(requeteCompter, Connexion.Connec);
                    cmdCompter.Parameters.AddWithValue("@nomPlanete", nomPlanete);

                    // On calcule l'identifiant de la nouvelle dépense (total actuel + 1)
                    int nouvelId = Convert.ToInt32(cmdCompter.ExecuteScalar()) + 1;

                    // On prépare la requête SQL d'insertion pour créer la nouvelle dépense en base
                    string requeteNouvelleDepense = @"INSERT INTO Depense (nomPlanete, numeroMission, id, dateD, montant, motif, idTypeDepense)                                               VALUES (@nomPlanete, @numeroMission, @id, @dateD, @montant, @motif, @idTypeDepense)";

                    SQLiteCommand cmdDepense = new SQLiteCommand(requeteNouvelleDepense, Connexion.Connec);
                    cmdDepense.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmdDepense.Parameters.AddWithValue("@numeroMission", numeroMission);
                    cmdDepense.Parameters.AddWithValue("@id", nouvelId);
                    cmdDepense.Parameters.AddWithValue("@dateD", dateDepense);
                    cmdDepense.Parameters.AddWithValue("@montant", montant);
                    cmdDepense.Parameters.AddWithValue("@motif", motif);
                    cmdDepense.Parameters.AddWithValue("@idTypeDepense", identifiantDepense);

                    // On exécute la commande SQL d'insertion
                    cmdDepense.ExecuteNonQuery();

                    // On met à jour l'application avec les nouvelles données de la base
                    UpdateDuDataset();

                    // On recharge la fiche de mission actuelle pour rafraîchir les affichages
                    ChargerFicheMission(lblNomMission.Text);

                    // On réinitialise tous les composants du formulaire de saisie de dépense
                    dtpDateDepense.Value = DateTime.Today;
                    txtSommeDepense.Clear();
                    rtbMotifDepense.Clear();
                    cboTypeDepense.SelectedIndex = -1;

                    // On masque tous les panels de saisie
                    pnlNouveauContact.Visible = false;
                    pnlNouvelleDepense.Visible = false;
                    pnlNouvelleEvenement.Visible = false;
                    pnlNouvelleCapture.Visible = false;

                    // On remet le titre du GroupBox à son état par défaut
                    grpAjoutNouvelMission.Text = "Veuillez choisir une action";

                    // On affiche le message de succès
                    AfficherMessage("✔ Dépense ajoutée avec succès !", Color.Green);
                }
                catch (Exception monErreur)
                {
                    // On remonte un message en cas d'échec ou d'anomalie durant l'accès à la base
                    AfficherMessage("✖ Erreur : " + monErreur.Message, Color.Red);
                }
            }
        }

        // ##############################################################################################################################################################################################################################################

        private void btnAnnulerDepense_Click(object sender, EventArgs e)
        {
            // On efface les messages d'alerte de tous les ErrorProvider de la dépense
            erpDateDepense.Clear();
            erpSommeDepense.Clear();
            erpMotifDepense.Clear();
            erpTypeDepense.Clear();

            // On réinitialise tous les composants du formulaire de saisie de dépense à leur état d'origine
            dtpDateDepense.Value = DateTime.Today;
            txtSommeDepense.Clear();
            rtbMotifDepense.Clear();
            cboTypeDepense.SelectedIndex = -1;

            // On masque le Panel de création de dépense pour fermer le formulaire
            pnlNouvelleDepense.Visible = false;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnNouvelEvenement_Click(object sender, EventArgs e)
        {
            // On change le titre du GroupBox d'ajout pour l'adapter aux événements
            grpAjoutNouvelMission.Text = "Nouvel évènement";

            // On affiche le Panel pour créer un événement
            pnlNouvelleEvenement.Visible = true;

            // On masque tous les autres Panel de saisie pour éviter les superpositions
            pnlNouvelleDepense.Visible = false;
            pnlNouveauContact.Visible = false;
            pnlNouvelleCapture.Visible = false;

            // On efface les messages d'alerte des ErrorProvider liés aux événements
            erpDateEvenement.Clear();
            erpCommentaire.Clear();

            // On ajoute un texte d'aide sur les boutons de validation et d'annulation de l'événement
            tltValiderEvenement.SetToolTip(btnValiderEvenement, "Valider l'évènement");
            tltAnnulerEvenement.SetToolTip(btnAnnulerEvenement, "Annuler l'évènement");

            // On remet le DateTimePicker à la date du jour et on vide le RichTextBox du commentaire
            dtpEvenement.Value = DateTime.Today;
            rtbEvenement.Clear();
        }

        // ##############################################################################################################################################################################################################################################

        private void btnValiderEvenement_Click(object sender, EventArgs e)
        {
            // On initialise un indicateur pour vérifier si toutes les données de l'événement sont valides
            Boolean eventValide = true;

            // On efface les anciens messages d'alerte sur les ErrorProvider de l'événement
            erpDateEvenement.Clear();
            erpCommentaire.Clear();

            // On prépare les variables pour stocker les informations de la mission actuelle
            string nomPlanete = "";
            int numeroMission = 0;
            DateTime dateFinMission = DateTime.MaxValue;

            // On parcourt la table Mission pour extraire les détails de la mission sélectionnée
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
            {
                // Si la mission correspond au nom affiché sur le Label du formulaire
                if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == lblNomMission.Text)
                {
                    nomPlanete = ligne["nomPlanete"].ToString();
                    numeroMission = Convert.ToInt32(ligne["numero"]);

                    // Si une date de retour existe, on la récupère pour la validation
                    if (ligne["dateRetour"] != DBNull.Value)
                    {
                        dateFinMission = Convert.ToDateTime(ligne["dateRetour"]);
                    }
                    break;
                }
            }

            // On vérifie que la date choisie dans le DateTimePicker n'est pas dans le passé
            if (dtpEvenement.Value.Date < DateTime.Today)
            {
                erpDateEvenement.SetIconPadding(dtpEvenement, 10);
                erpDateEvenement.SetError(dtpEvenement, "La date de l'événement ne peut pas être antérieure à aujourd'hui");
                eventValide = false;
            }
            // On vérifie que la date de l'événement ne dépasse pas la date de fin de la mission
            else if (dtpEvenement.Value.Date > dateFinMission.Date)
            {
                erpDateEvenement.SetIconPadding(dtpEvenement, 10);
                erpDateEvenement.SetError(dtpEvenement, "La date de l'événement ne peut pas dépasser la date de fin de la mission");
                eventValide = false;
            }

            // On vérifie que le RichTextBox du commentaire n'est pas vide
            if (rtbEvenement.Text == string.Empty)
            {
                erpCommentaire.SetIconPadding(rtbEvenement, 10);
                erpCommentaire.SetError(rtbEvenement, "Veuillez saisir le commentaire de l'événement");
                eventValide = false;
            }

            // Si toutes les vérifications de saisie sont correctes
            if (eventValide == true)
            {
                try
                {
                    // On convertit et stocke les données prêtes à être insérées
                    string dateEvenement = dtpEvenement.Value.ToString("yyyy-MM-dd");
                    string commentaireEvenement = rtbEvenement.Text;

                    // On prépare la requête SQL d'insertion pour enregistrer l'événement dans le journal de bord
                    string requeteEvenement = @"INSERT INTO JournalDeBord (nomPlanete, numero, dateJ, commentaires)                        VALUES (@nomPlanete, @numero, @dateJ, @commentaires)";

                    SQLiteCommand cmdEvenement = new SQLiteCommand(requeteEvenement, Connexion.Connec);
                    cmdEvenement.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmdEvenement.Parameters.AddWithValue("@numero", numeroMission);
                    cmdEvenement.Parameters.AddWithValue("@dateJ", dateEvenement);
                    cmdEvenement.Parameters.AddWithValue("@commentaires", commentaireEvenement);

                    // On exécute la commande SQL d'insertion
                    cmdEvenement.ExecuteNonQuery();

                    // On met à jour l'application avec les nouvelles données de la base
                    UpdateDuDataset();

                    // On recharge le journal de bord pour afficher le nouvel événement à l'écran
                    ChargerJournalDeBord(lblNomMission.Text);

                    // On réinitialise les composants du formulaire de saisie d'événement
                    dtpEvenement.Value = DateTime.Today;
                    rtbEvenement.Clear();

                    // On masque tous les panels de saisie
                    pnlNouveauContact.Visible = false;
                    pnlNouvelleDepense.Visible = false;
                    pnlNouvelleEvenement.Visible = false;
                    pnlNouvelleCapture.Visible = false;

                    // On remet le titre du GroupBox à son état par défaut
                    grpAjoutNouvelMission.Text = "Veuillez choisir une action";

                    // On affiche le message de succès
                    AfficherMessage("✔ Événement ajouté avec succès !", Color.Green);
                }
                catch (Exception monErreur)
                {
                    // On remonte un message en cas d'échec durant l'accès à la base
                    AfficherMessage("✖ Erreur : " + monErreur.Message, Color.Red);
                }
            }
        }

        // ##############################################################################################################################################################################################################################################

        private void btnAnnulerEvenement_Click(object sender, EventArgs e)
        {
            // On efface les messages d'alerte des ErrorProvider liés aux événements
            erpDateEvenement.Clear();
            erpCommentaire.Clear();

            // On réinitialise le DateTimePicker à la date du jour et on vide le RichTextBox du commentaire
            dtpEvenement.Value = DateTime.Today;
            rtbEvenement.Clear();

            // On masque le Panel de création d'événement pour fermer le formulaire
            pnlNouvelleEvenement.Visible = false;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnAjouterCapture_Click(object sender, EventArgs e)
        {
            // On change le titre du GroupBox d'ajout pour l'adapter aux captures
            grpAjoutNouvelMission.Text = "Nouvelle capture";

            // On efface les messages d'alerte des ErrorProvider liés aux captures
            erpEspece.Clear();
            erpNombreIndividu.Clear();

            // On vide le TextBox de saisie de la quantité
            txtNombreIndividu.Clear();

            // On ajoute un texte d'aide sur les boutons de validation et d'annulation de la capture
            tltValiderCapture.SetToolTip(btnValiderCapture, "Valider la capture");
            tltAnnulerCapture.SetToolTip(btnAnnulerCapture, "Annuler la capture");

            // On vide tous les choix de la ComboBox de l'espèce
            cboEspece.Items.Clear();

            // On utilise ton DataSet global pour remplir la combo
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Espece"].Rows)
            {
                // On construit le texte en associant le nom de l'alien et sa couleur
                string nomAlien = ligne["nom"].ToString() + " - " + ligne["couleur"].ToString();

                // On ajoute cet alien dans la ComboBox
                cboEspece.Items.Add(nomAlien);
            }

            // Si la ComboBox possède au moins un élément, on sélectionne le premier par défaut
            if (cboEspece.Items.Count > 0)
            {
                cboEspece.SelectedIndex = 0;
            }

            // On rend le Panel de nouvelle capture visible
            pnlNouvelleCapture.Visible = true;

            // On force le Panel à s'afficher au premier plan
            pnlNouvelleCapture.BringToFront();
        }

        // ##############################################################################################################################################################################################################################################

        private void btnValiderCapture_Click(object sender, EventArgs e)
        {
            // On efface les anciens messages d'alerte sur les ErrorProvider de capture
            erpEspece.Clear();
            erpNombreIndividu.Clear();

            // On vérifie qu'un élément est bien sélectionné dans la ComboBox de l'espèce
            if (cboEspece.SelectedIndex == -1)
            {
                erpEspece.SetIconPadding(cboEspece, 10);
                erpEspece.SetError(cboEspece, "Veuillez sélectionner une espèce.");
                return;
            }

            // On vérifie que le TextBox de la quantité n'est pas vide
            if (txtNombreIndividu.Text == string.Empty)
            {
                erpNombreIndividu.SetIconPadding(txtNombreIndividu, 10);
                erpNombreIndividu.SetError(txtNombreIndividu, "Veuillez saisir un nombre.");
                return;
            }

            // On prépare les variables pour stocker les informations de la mission actuelle
            string nomPlanete = "";
            int numeroMission = 0;
            DateTime dateFinMission = DateTime.MaxValue;

            // On parcourt la table Mission pour extraire les détails de la mission sélectionnée
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
            {
                // Si la mission correspond au nom affiché sur le Label du formulaire
                if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == lblNomMission.Text)
                {
                    nomPlanete = ligne["nomPlanete"].ToString();
                    numeroMission = Convert.ToInt32(ligne["numero"]);

                    // Si une date de retour existe, on la récupère pour la validation
                    if (ligne["dateRetour"] != DBNull.Value)
                    {
                        dateFinMission = Convert.ToDateTime(ligne["dateRetour"]);
                    }
                    break;
                }
            }

            // On convertit en nombre entier la quantité saisie dans le TextBox
            int quantiteSaisie = Convert.ToInt32(txtNombreIndividu.Text);

            // On extrait uniquement le nom de l'alien en découpant la chaîne sélectionnée au niveau du séparateur
            string nomAlien = cboEspece.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0].Trim();

            try
            {
                // 1. On prépare et exécute la requête SQL pour obtenir l'identifiant de l'espèce à partir de son nom
                string reqId = "SELECT id FROM Espece WHERE nom = @nomAlien";
                SQLiteCommand cmdId = new SQLiteCommand(reqId, Connexion.Connec);
                cmdId.Parameters.AddWithValue("@nomAlien", nomAlien);
                int idEspece = Convert.ToInt32(cmdId.ExecuteScalar());

                // 2. On vérifie si cet alien existe déjà pour cette mission
                string reqCheck = @"SELECT nombre FROM Capturer                     WHERE nomPlanete = @nomPlanete                     AND numeroMission = @numeroMission                     AND idEspeceEnnemi = @idEspece";

                SQLiteCommand cmdCheck = new SQLiteCommand(reqCheck, Connexion.Connec);
                cmdCheck.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                cmdCheck.Parameters.AddWithValue("@numeroMission", numeroMission);
                cmdCheck.Parameters.AddWithValue("@idEspece", idEspece);

                // On exécute la vérification
                object exist = cmdCheck.ExecuteScalar();

                // Si l'alien a déjà été capturé auparavant lors de cette mission
                if (exist != null)
                {
                    // On additionne la quantité existante et la nouvelle quantité saisie
                    int nouvelleQuantite = Convert.ToInt32(exist) + quantiteSaisie;

                    // On prépare la requête SQL de mise à jour pour modifier la quantité globale
                    string reqUpdate = @"UPDATE Capturer                          SET nombre = @nouvelleQuantite                          WHERE nomPlanete = @nomPlanete                          AND numeroMission = @numeroMission                          AND idEspeceEnnemi = @idEspece";

                    SQLiteCommand cmdUpdate = new SQLiteCommand(reqUpdate, Connexion.Connec);
                    cmdUpdate.Parameters.AddWithValue("@nouvelleQuantite", nouvelleQuantite);
                    cmdUpdate.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmdUpdate.Parameters.AddWithValue("@numeroMission", numeroMission);
                    cmdUpdate.Parameters.AddWithValue("@idEspece", idEspece);

                    // On exécute la commande de mise à jour
                    cmdUpdate.ExecuteNonQuery();
                }
                // Si c'est la toute première capture de cette espèce pour cette mission
                else
                {
                    // On prépare la requête SQL d'insertion pour créer un nouvel enregistrement
                    string reqInsert = @"INSERT INTO Capturer (nomPlanete, numeroMission, idEspeceEnnemi, nombre)                          VALUES (@nomPlanete, @numeroMission, @idEspece, @quantite)";

                    SQLiteCommand cmdInsert = new SQLiteCommand(reqInsert, Connexion.Connec);
                    cmdInsert.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmdInsert.Parameters.AddWithValue("@numeroMission", numeroMission);
                    cmdInsert.Parameters.AddWithValue("@idEspece", idEspece);
                    cmdInsert.Parameters.AddWithValue("@quantite", quantiteSaisie);

                    // On exécute la commande d'insertion
                    cmdInsert.ExecuteNonQuery();
                }

                // On met à jour l'application avec les nouvelles données de la base
                UpdateDuDataset();

                // On recharge la fiche de mission actuelle pour actualiser la liste des captures
                ChargerFicheMission(lblNomMission.Text);

                // On réinitialise les composants du formulaire de saisie de capture
                txtNombreIndividu.Clear();
                cboEspece.SelectedIndex = -1;

                // On masque tous les panels de saisie
                pnlNouveauContact.Visible = false;
                pnlNouvelleDepense.Visible = false;
                pnlNouvelleEvenement.Visible = false;
                pnlNouvelleCapture.Visible = false;

                // On remet le titre du GroupBox à son état par défaut
                grpAjoutNouvelMission.Text = "Veuillez choisir une action";

                // On affiche le message de succès
                AfficherMessage("✔ Capture enregistrée avec succès !", Color.Green);
            }
            catch (Exception ex)
            {
                // On affiche un message d'erreur en cas de problème avec la base de données
                AfficherMessage("✖ Erreur : " + ex.Message, Color.Red);
            }
        }

        // ##############################################################################################################################################################################################################################################

        private void btnAnnulerCapture_Click(object sender, EventArgs e)
        {
            // On efface les messages d'alerte des ErrorProvider de l'espèce et de la quantité
            erpEspece.Clear();
            erpNombreIndividu.Clear();

            // On vide le TextBox de saisie du nombre d'individus
            txtNombreIndividu.Clear();

            // On masque le Panel de création de capture pour fermer le formulaire
            pnlNouvelleCapture.Visible = false;
        }

        // ##############################################################################################################################################################################################################################################

        private void txtNombreIndividu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            // On désactive les raccourcis clavier
            txtNombreIndividu.ShortcutsEnabled = false;

            // On vérifie si la touche est un chiffre ou s'il s'agit de la touche Backspace
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                // On bloque le 0 uniquement si le champ est encore vide (évite de commencer par 0)
                if (e.KeyChar == '0' && txtNombreIndividu.Text.Length == 0)
                {
                    e.Handled = true;
                }
                // On limite la saisie à 5 chiffres maximum
                else if (txtNombreIndividu.Text.Length >= 5 && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
                // Sinon on autorise la touche
                else
                {
                    e.Handled = false;
                }
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void UpdateDuDataset()
        {
            // On définit un tableau contenant les noms de toutes les tables de la base de données
            string[] tables = new string[]
            {"Admin", "Allie", "Capturer", "Civil", "Composer", "Contact", "Depense", "Ennemi", "Espece", "Habiter", "Informateur", "JournalDeBord", "Membre", "Militaire", "Mission", "Negocier", "ObjectifCapture", "Planete", "TypeDepense"};

            // On parcourt chaque nom de table défini dans le tableau
            foreach (string nomTable in tables)
            {
                // Si la table existe déjà dans le DataSet global, on la vide pour éviter les doublons
                if (MesDatas.DsGlobal.Tables.Contains(nomTable))
                {
                    MesDatas.DsGlobal.Tables[nomTable].Clear();
                }

                // On instancie un SQLiteDataAdapter pour charger toutes les lignes de la table actuelle
                SQLiteDataAdapter da = new SQLiteDataAdapter($"SELECT * FROM {nomTable}", Connexion.Connec);

                // On remplit la DataTable correspondante dans le DataSet avec les nouvelles données fraîches de la base
                da.Fill(MesDatas.DsGlobal, nomTable);
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void GererBoutonsFicheMission(DataRow ligneMission)
        {
            // On récupère la date du jour au format texte ISO (AAAA-MM-JJ) pour les comparaisons
            string dateActuelle = DateTime.Now.ToString("yyyy-MM-dd");

            // On extrait les dates de départ et de retour de la ligne de données (DataRow) de la mission
            string dateDepart = ligneMission["dateDepart"].ToString();
            string dateRetour = ligneMission["dateRetour"].ToString();

            // On détermine l'état temporel de la mission en comparant les chaînes de caractères des dates
            bool missionTerminee = string.Compare(dateRetour, dateActuelle) < 0;
            bool missionAVenir = string.Compare(dateDepart, dateActuelle) > 0;
            bool missionEnCours = !missionTerminee && !missionAVenir;

            // On définit deux variables de couleur (Color) personnalisées à partir de leurs composants ARGB
            Color vert = Color.FromArgb(128, 255, 128);
            Color rouge = Color.FromArgb(255, 128, 128);

            // Dépense toujours disponible sauf si mission terminée
            btnNouvelleDepense.Enabled = !missionTerminee;
            btnNouvelleDepense.Image = null;

            // On charge l'icône de fond pour le bouton depuis le dossier de ressources de l'application
            btnNouvelleDepense.BackgroundImage = Image.FromFile(@"img\Icone\imageArgentBlanc.png");
            btnNouvelleDepense.BackgroundImageLayout = ImageLayout.Zoom;

            // On applique une condition ternaire pour changer la couleur de fond du bouton selon le statut
            btnNouvelleDepense.BackColor = missionTerminee ? rouge : vert;

            if (missionEnCours)
            {
                // Mission en cours → tout en vert
                btnNouveauContact.Enabled = true;
                btnNouveauContact.BackColor = vert;

                btnNouvelEvenement.Enabled = true;
                btnNouvelEvenement.BackColor = vert;

                btnAjouterCapture.Enabled = true;
                btnAjouterCapture.BackColor = vert;
            }
            else if (missionAVenir)
            {
                // Mission à venir → seulement dépense active, reste rouge
                btnNouveauContact.Enabled = false;
                btnNouveauContact.BackColor = rouge;

                btnNouvelEvenement.Enabled = false;
                btnNouvelEvenement.BackColor = rouge;

                btnAjouterCapture.Enabled = false;
                btnAjouterCapture.BackColor = rouge;
            }
            else
            {
                // Mission terminée → tout rouge tout désactivé
                btnNouvelleDepense.Enabled = false;
                btnNouvelleDepense.BackColor = rouge;

                btnNouveauContact.Enabled = false;
                btnNouveauContact.BackColor = rouge;

                btnNouvelEvenement.Enabled = false;
                btnNouvelEvenement.BackColor = rouge;

                btnAjouterCapture.Enabled = false;
                btnAjouterCapture.BackColor = rouge;
            }

            // On remet Image à null pour tous (évite les résidus visuels)
            btnNouveauContact.Image = null;
            btnNouvelEvenement.Image = null;
            btnAjouterCapture.Image = null;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnJournalDeBord_Click(object sender, EventArgs e)
        {
            // On définit des textes d'aide (ToolTip) pour les boutons du journal de bord et d'édition PDF
            tltJournalDeBord.SetToolTip(btnJournalDeBord, "Accès au journal de bord");
            tltRetourJournalDeBord.SetToolTip(btnRetourJournalDeBord, "Retourner au journal de bord");
            tltEditerPDF.SetToolTip(btnEditerPDF, "Editer un PDF");

            // On masque les contrôles et conteneurs liés à la fiche de mission principale
            pnlBoutonMission.Visible = false;
            pnlMission.Visible = false;
            grpFicheMission.Visible = false;

            // On affiche le GroupBox du journal de bord
            grpJournalDeBord.Visible = true;

            // On force le GroupBox du journal de bord à passer au premier plan de l'interface
            grpJournalDeBord.BringToFront();

            // On appelle la méthode personnalisée pour repositionner le GroupBox au centre de l'écran
            CentrerGroupBox();

            // On charge les données textuelles et les événements liés à la mission actuelle dans le journal
            ChargerJournalDeBord(lblNomMission.Text);
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ChargerJournalDeBord(string nomMission)
        {
            // On définit les textes d'aide pour les boutons de navigation
            tltJournalDeBord.SetToolTip(btnJournalDeBord, "Accès au journal de bord");
            tltFullGauche.SetToolTip(btnFullGauche, "Revenir au premier évènement");
            tltGauche.SetToolTip(btnGauche, "Reculer d'un évènement");
            tltDroite.SetToolTip(btnDroite, "Avancer d'un évènement");
            tltFullDroite.SetToolTip(btnFullDroite, "Aller au dernier évènement");

            // On cherche la ligne de la mission dans le DataSet
            DataRow ligneMission = null;
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
            {
                if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == nomMission)
                {
                    ligneMission = ligne;
                    break;
                }
            }

            // Si la mission n'existe pas dans le DataSet on sort immédiatement
            if (ligneMission == null)
            {
                return;
            }

            // On met à jour le titre du groupbox avec le nom de la mission
            grpJournalDeBord.Text = "Journal de bord de la mission " + nomMission;

            // On crée une vue filtrée sur la table JournalDeBord pour ne garder que les événements de la mission courante
            DataView viewJournal = new DataView(MesDatas.DsGlobal.Tables["JournalDeBord"]);
            viewJournal.RowFilter = $"nomPlanete = '{ligneMission["nomPlanete"]}' AND numero = {ligneMission["numero"]}";

            // On convertit la vue filtrée en DataTable pour pouvoir manipuler les lignes et les trier manuellement
            DataTable tableSource = viewJournal.ToTable();

            // On charge toutes les lignes dans une List<DataRow> pour pouvoir les trier avec un comparateur personnalisé
            List<DataRow> lignesTriees = new List<DataRow>();
            foreach (DataRow row in tableSource.Rows)
            {
                lignesTriees.Add(row);
            }

            // On trie la liste par date croissante du plus ancien au plus récent
            // On utilise TryParseExact avec les deux formats possibles car la BDD contient des dates dans ces deux formats différents
            lignesTriees.Sort((a, b) =>
            {
                DateTime dateA, dateB;

                // On parse la date de l'événement A
                DateTime.TryParseExact(a["dateJ"].ToString(), new string[] { "yyyy-MM-dd", "dd/MM/yyyy" }, null, System.Globalization.DateTimeStyles.None, out dateA);

                // On parse la date de l'événement B
                DateTime.TryParseExact(b["dateJ"].ToString(), new string[] { "yyyy-MM-dd", "dd/MM/yyyy" }, null, System.Globalization.DateTimeStyles.None, out dateB);

                // CompareTo retourne un négatif si dateA < dateB
                return dateA.CompareTo(dateB);
            });

            // On recrée un DataTable vide avec la même structure que tableSource puis on y insère les lignes dans l'ordre trié
            DataTable tableFinal = tableSource.Clone();
            foreach (DataRow row in lignesTriees)
            {
                tableFinal.ImportRow(row);
            }

            // On branche le DataTable trié sur le BindingSource
            // C'est lui qui gère la navigation entre les événements
            bsJournal.DataSource = tableFinal;

            if (bsJournal.Count > 0)
            {
                // On se positionne sur le premier événement
                bsJournal.Position = 0;

                // On rend visibles les contrôles d'affichage
                lblNombre.Visible = true;
                lblDateJournal.Visible = true;
                rtbCommentaire.Visible = true;

                // On affiche le premier événement
                AfficherEvenementCourant();
            }
            else
            {
                // S'il n'y a aucun événement,on cache la pagination et la date
                lblNombre.Visible = false;
                lblDateJournal.Visible = false;

                // On affiche un message d'absence de données dans la RichTextBox
                rtbCommentaire.Visible = true;
                rtbCommentaire.Text = "Aucun événement pour cette mission";
                rtbCommentaire.ForeColor = Color.LightGray;
                rtbCommentaire.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            }

            // On recharge les trois onglets du journal de bord
            ChargerOngletContacts(ligneMission);
            ChargerOngletDepenses(ligneMission);
            ChargerOngletBilanCaptures(ligneMission);
        }

        // ##############################################################################################################################################################################################################################################

        private void AfficherEvenementCourant()
        {
            // Sécurité si le BindingSource est vide
            if (bsJournal.Count == 0)
            {
                return;
            }

            // On récupère l'élément sélectionné sous forme de DataRowView via le BindingSource courant
            DataRowView ligne = (DataRowView)bsJournal.Current;

            // On affiche la date formatée et le commentaire de l'événement dans leurs contrôles respectifs
            lblDateJournal.Text = FormatDate(ligne["dateJ"].ToString());
            rtbCommentaire.Text = ligne["commentaires"].ToString();

            // On applique le style de texte standard pour l'affichage des données (couleur blanche et style régulier)
            rtbCommentaire.ForeColor = Color.White;
            rtbCommentaire.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            // On met à jour le texte du Label d'indexation pour afficher la pagination (ex: "3 / 11")
            lblNombre.Text = $"{bsJournal.Position + 1} / {bsJournal.Count}";
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ChargerOngletContacts(DataRow ligneMission)
        {
            // On vide les lignes existantes
            dgvContacts.Rows.Clear();

            // On vide les colonnes existantes
            dgvContacts.Columns.Clear();

            // On étire le DGV pour remplir tout l'onglet et supprimer le fond blanc
            dgvContacts.Dock = DockStyle.Fill;

            // On définit la couleur de fond du DGV
            dgvContacts.BackgroundColor = Color.FromArgb(20, 40, 80);

            // On définit la couleur des lignes de la grille
            dgvContacts.GridColor = Color.FromArgb(50, 80, 120);

            // On supprime la bordure autour du DGV
            dgvContacts.BorderStyle = BorderStyle.None;

            // On supprime la colonne grise à gauche avec la flèche
            dgvContacts.RowHeadersVisible = false;

            // On supprime la ligne vide en bas pour ajouter des données
            dgvContacts.AllowUserToAddRows = false;

            // On empêche l'utilisateur de redimensionner les lignes
            dgvContacts.AllowUserToResizeRows = false;

            // On empêche l'utilisateur de modifier les cellules
            dgvContacts.ReadOnly = true;

            // On sélectionne toute la ligne au clic
            dgvContacts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // On étire les colonnes pour remplir toute la largeur
            dgvContacts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // On désactive le style Windows par défaut pour pouvoir appliquer nos propres couleurs
            dgvContacts.EnableHeadersVisualStyles = false;

            // On définit la couleur de fond des en-têtes
            dgvContacts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(14, 28, 54);

            // On définit la couleur du texte des en-têtes
            dgvContacts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // On définit la police des en-têtes
            dgvContacts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // On définit la hauteur des en-têtes
            dgvContacts.ColumnHeadersHeight = 35;

            // On définit la couleur de fond des cellules
            dgvContacts.DefaultCellStyle.BackColor = Color.FromArgb(20, 40, 80);

            // On définit la couleur du texte des cellules
            dgvContacts.DefaultCellStyle.ForeColor = Color.White;

            // On définit la police des cellules
            dgvContacts.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // On définit la couleur de fond des cellules sélectionnées
            dgvContacts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 138, 221);

            // On définit la couleur du texte des cellules sélectionnées
            dgvContacts.DefaultCellStyle.SelectionForeColor = Color.White;

            // On ajoute un petit padding à gauche pour aérer le texte
            dgvContacts.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

            // On définit la hauteur des lignes
            dgvContacts.RowTemplate.Height = 30;

            // On définit la couleur de fond des lignes alternées pour faciliter la lecture
            dgvContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 55, 100);

            // On définit la couleur du texte des lignes alternées
            dgvContacts.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            // On ajoute la colonne Date
            dgvContacts.Columns.Add("date", "Date");

            // On ajoute la colonne Somme
            dgvContacts.Columns.Add("somme", "Somme");

            // On ajoute la colonne Appréciation
            dgvContacts.Columns.Add("appreciation", "Appréciation");

            // On ajoute la colonne Informateur
            dgvContacts.Columns.Add("informateur", "Informateur");

            // On définit la largeur relative de la colonne Date
            dgvContacts.Columns["date"].FillWeight = 15;

            // On définit la largeur relative de la colonne Somme
            dgvContacts.Columns["somme"].FillWeight = 10;

            // On définit la largeur relative de la colonne Appréciation
            dgvContacts.Columns["appreciation"].FillWeight = 55;

            // On définit la largeur relative de la colonne Informateur
            dgvContacts.Columns["informateur"].FillWeight = 20;

            // On centre le texte de la colonne Date
            dgvContacts.Columns["date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre le titre de la colonne Date
            dgvContacts.Columns["date"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre le texte de la colonne Somme
            dgvContacts.Columns["somme"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre le titre de la colonne Somme
            dgvContacts.Columns["somme"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre le texte de la colonne Informateur
            dgvContacts.Columns["informateur"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre le titre de la colonne Informateur
            dgvContacts.Columns["informateur"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On initialise le cumul financier des sommes versées
            int totalSommes = 0;

            // On parcourt l'ensemble des lignes de la table Contact du DataSet global
            foreach (DataRow ligneContact in MesDatas.DsGlobal.Tables["Contact"].Rows)
            {

                // On filtre les contacts de la mission et de la planète courantes
                if (ligneContact["nomPlanete"].ToString() == ligneMission["nomPlanete"].ToString() &&
                    Convert.ToInt32(ligneContact["numeroMission"]) == Convert.ToInt32(ligneMission["numero"]))
                {

                    // On initialise le nom de l'informateur avec son code par défaut
                    string nomInformateur = ligneContact["nomCodeInformateur"].ToString();

                    // On parcourt la table Informateur pour récupérer son vrai nom
                    foreach (DataRow ligneInformateur in MesDatas.DsGlobal.Tables["Informateur"].Rows)
                    {
                        // Si le code correspond à celui du contact
                        if (ligneInformateur["nomCode"].ToString() == ligneContact["nomCodeInformateur"].ToString())
                        {
                            // On stocke le vrai nom de l'informateur
                            nomInformateur = ligneInformateur["nom"].ToString();

                            // On sort de la boucle de recherche
                            break;
                        }
                    }

                    // On ajoute la ligne de données formatée dans le DataGridView
                    dgvContacts.Rows.Add(
                        FormatDate(ligneContact["dateC"].ToString()),
                        ligneContact["sommeVersee"].ToString() + " €",
                        ligneContact["appreciation"].ToString(),
                        nomInformateur
                    );

                    // On cumule le montant versé dans le total
                    totalSommes += Convert.ToInt32(ligneContact["sommeVersee"]);
                }
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ChargerOngletDepenses(DataRow ligneMission)
        {
            // On vide les lignes existantes du DataGridView
            dgvDepenses.Rows.Clear();

            // On vide les colonnes existantes du DataGridView
            dgvDepenses.Columns.Clear();

            // On étire le contrôle pour remplir tout l'espace de l'onglet
            dgvDepenses.Dock = DockStyle.Fill;

            // On définit la couleur de fond principale du conteneur
            dgvDepenses.BackgroundColor = Color.FromArgb(20, 40, 80);

            // On définit la couleur des lignes séparatrices de la grille
            dgvDepenses.GridColor = Color.FromArgb(50, 80, 120);

            // On supprime la bordure extérieure du contrôle
            dgvDepenses.BorderStyle = BorderStyle.None;

            // On masque la colonne d'en-tête située à l'extrême gauche des lignes
            dgvDepenses.RowHeadersVisible = false;

            // On empêche l'apparition de la ligne vide interactive en bas de grille
            dgvDepenses.AllowUserToAddRows = false;

            // On interdit le redimensionnement manuel de la hauteur des lignes
            dgvDepenses.AllowUserToResizeRows = false;

            // On bascule les cellules en mode lecture seule
            dgvDepenses.ReadOnly = true;

            // On configure le mode de sélection pour cibler la ligne complète au clic
            dgvDepenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // On active le redimensionnement automatique proportionnel des colonnes
            dgvDepenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // On désactive les thèmes visuels du système pour appliquer nos styles personnalisés
            dgvDepenses.EnableHeadersVisualStyles = false;

            // On applique la couleur de fond des cellules d'en-tête de colonne
            dgvDepenses.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(14, 28, 54);

            // On applique la couleur du texte des en-têtes de colonne
            dgvDepenses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // On configure la police de caractères des en-têtes en gras
            dgvDepenses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // On fixe la hauteur de la ligne d'en-tête
            dgvDepenses.ColumnHeadersHeight = 35;

            // On définit la couleur de fond par défaut des cellules de données
            dgvDepenses.DefaultCellStyle.BackColor = Color.FromArgb(20, 40, 80);

            // On définit la couleur du texte par défaut des cellules de données
            dgvDepenses.DefaultCellStyle.ForeColor = Color.White;

            // On configure la police de caractères standard des cellules de données
            dgvDepenses.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // On définit la couleur de fond de la ligne sélectionnée
            dgvDepenses.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 138, 221);

            // On définit la couleur du texte de la ligne sélectionnée
            dgvDepenses.DefaultCellStyle.SelectionForeColor = Color.White;

            // On réinitialise les marges intérieures (Padding) des cellules
            dgvDepenses.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);

            // On fixe la hauteur par défaut des lignes du modèle
            dgvDepenses.RowTemplate.Height = 30;

            // On configure la couleur de fond des lignes paires ou alternées
            dgvDepenses.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 55, 100);

            // On configure la couleur du texte des lignes paires ou alternées
            dgvDepenses.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            // On crée la colonne Numéro
            dgvDepenses.Columns.Add("numero", "Numéro");

            // On crée la colonne Date
            dgvDepenses.Columns.Add("date", "Date");

            // On crée la colonne Motif
            dgvDepenses.Columns.Add("motif", "Motif");

            // On crée la colonne Montant
            dgvDepenses.Columns.Add("montant", "Montant");

            // On crée la colonne Type de dépense
            dgvDepenses.Columns.Add("type", "Type de dépense");

            // On attribue le poids proportionnel de largeur à la colonne Numéro
            dgvDepenses.Columns["numero"].FillWeight = 8;

            // On attribue le poids proportionnel de largeur à la colonne Date
            dgvDepenses.Columns["date"].FillWeight = 12;

            // On attribue le poids proportionnel de largeur à la colonne Motif
            dgvDepenses.Columns["motif"].FillWeight = 45;

            // On attribue le poids proportionnel de largeur à la colonne Montant
            dgvDepenses.Columns["montant"].FillWeight = 10;

            // On attribue le poids proportionnel de largeur à la colonne Type de dépense
            dgvDepenses.Columns["type"].FillWeight = 25;

            // On centre l'alignement du texte de données pour la colonne Numéro
            dgvDepenses.Columns["numero"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre l'alignement de l'en-tête textuel pour la colonne Numéro
            dgvDepenses.Columns["numero"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre l'alignement du texte de données pour la colonne Date
            dgvDepenses.Columns["date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre l'alignement de l'en-tête textuel pour la colonne Date
            dgvDepenses.Columns["date"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre l'alignement du texte de données pour la colonne Montant
            dgvDepenses.Columns["montant"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre l'alignement de l'en-tête textuel pour la colonne Montant
            dgvDepenses.Columns["montant"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre l'alignement du texte de données pour la colonne Type de dépense
            dgvDepenses.Columns["type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre l'alignement de l'en-tête textuel pour la colonne Type de dépense
            dgvDepenses.Columns["type"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On initialise la variable de cumul financier global des dépenses
            int totalDepenses = 0;

            // On parcourt l'ensemble des lignes de données de la table Depense du DataSet global
            foreach (DataRow ligneDepense in MesDatas.DsGlobal.Tables["Depense"].Rows)
            {

                // On filtre pour extraire uniquement les lignes liées à la planète et au numéro de mission courants
                if (ligneDepense["nomPlanete"].ToString() == ligneMission["nomPlanete"].ToString() &&
                    Convert.ToInt32(ligneDepense["numeroMission"]) == Convert.ToInt32(ligneMission["numero"]))
                {

                    // On extrait l'identifiant numérique correspondant à la catégorie de la dépense
                    int idDepense = Convert.ToInt32(ligneDepense["idTypeDepense"]);

                    // On déclare la variable textuelle destinée à stocker l'intitulé de la catégorie
                    string typeDepense;

                    // On analyse l'identifiant pour lui associer son libellé explicite
                    switch (idDepense)
                    {
                        case 1: typeDepense = "DataBaz"; break;
                        case 2: typeDepense = "Informateur"; break;
                        case 3: typeDepense = "Réparation"; break;
                        case 4: typeDepense = "Droit de passage"; break;
                        default: typeDepense = "Inconnu"; break;
                    }

                    // On insère une nouvelle ligne formatée avec les données de la dépense
                    dgvDepenses.Rows.Add(
                        ligneDepense["id"].ToString(),
                        FormatDate(ligneDepense["dateD"].ToString()),
                        ligneDepense["motif"].ToString(),
                        ligneDepense["montant"].ToString() + " €",
                        typeDepense
                    );

                    // On additionne la valeur financière au montant total cumulé
                    totalDepenses += Convert.ToInt32(ligneDepense["montant"]);

                }
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnRetourJournalDeBord_Click(object sender, EventArgs e)
        {
            // On masque le GroupBox du journal de bord
            grpJournalDeBord.Visible = false;

            // On affiche le GroupBox de la fiche de mission principale
            grpFicheMission.Visible = true;

            // On force la fiche de mission à passer au premier plan de l'interface
            grpFicheMission.BringToFront();

            // On appelle la méthode pour repositionner le GroupBox au centre de l'écran
            CentrerGroupBox();
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnEditerPDF_Click(object sender, EventArgs e)
        {
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ INITIALISATION DES TABLES ═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On récupère la table des membres depuis la base de données
            DataTable dtMembrePDF = MesDatas.DsGlobal.Tables["Membre"];

            // On récupère la table des dépenses depuis la base de données
            DataTable dtDepensePDF = MesDatas.DsGlobal.Tables["Depense"];

            // On récupère la table qui associe les membres aux missions
            DataTable dtComposerPDF = MesDatas.DsGlobal.Tables["Composer"];

            // On récupère la table qui contient les civils
            DataTable dtCivilPDF = MesDatas.DsGlobal.Tables["Civil"];

            // On récupère la table qui contient les militaires
            DataTable dtMilitairePDF = MesDatas.DsGlobal.Tables["Militaire"];

            // On récupère la table des objectifs de capture
            DataTable dtObjectifPDF = MesDatas.DsGlobal.Tables["ObjectifCapture"];

            // On récupère la table des captures d'aliens réalisées
            DataTable dtCapturePDF = MesDatas.DsGlobal.Tables["Capturer"];

            // On récupère la table qui liste les espèces d'aliens
            DataTable dtEspecePDF = MesDatas.DsGlobal.Tables["Espece"];

            // On récupère la table du journal de bord
            DataTable dtJournalDeBordPDF = MesDatas.DsGlobal.Tables["JournalDeBord"];

            // On récupère la table qui contient les types de dépenses
            DataTable dtTypeDepensePDF = MesDatas.DsGlobal.Tables["TypeDepense"];

            // On récupère la table qui contient les contacts
            DataTable dtContactPDF = MesDatas.DsGlobal.Tables["Contact"];

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ RÉCUPÉRATION DE LA MISSION ══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On prépare une variable vide pour stocker la ligne de la mission
            DataRow ligneMission = null;

            // On parcourt toutes les lignes de la table Mission
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables["Mission"].Rows)
            {
                // On vérifie si le nom de la planète et le numéro correspondent au label de l'écran
                if (ligne["nomPlanete"].ToString() + ligne["numero"].ToString() == lblNomMission.Text)
                {
                    // On enregistre la ligne de la mission trouvée
                    ligneMission = ligne;

                    // On arrête de chercher puisque la mission est trouvée
                    break;
                }
            }

            // On arrête tout si aucune mission n'a été trouvée
            if (ligneMission == null)
            {
                return;
            }

            // On récupère le nom de la planète sous forme de texte
            string nomPlanete = ligneMission["nomPlanete"].ToString();

            // On récupère le numéro de la mission sous forme de nombre entier
            int numeroMission = Convert.ToInt32(ligneMission["numero"]);

            // On récupère le budget initial de la mission sous forme de nombre entier
            int budgetInitial = Convert.ToInt32(ligneMission["budget"]);

            // On récupère le nombre de membres requis pour la mission sous forme de nombre entier
            int nbMembre = Convert.ToInt32(ligneMission["nbMembreRequis"]);

            // On récupère l'objectif databaz sous forme de nombre entier
            int objectifDatabaz = Convert.ToInt32(ligneMission["objectifDatabaz"]);

            // On crée une variable pour calculer le total des dépenses et on la met à zéro
            int totalDepenses = 0;

            // On parcourt toutes les lignes de la table des dépenses
            foreach (DataRow ligneDepense in MesDatas.DsGlobal.Tables["Depense"].Rows)
            {
                // On vérifie si la dépense concerne la même planète et la même mission
                if (ligneDepense["nomPlanete"].ToString() == nomPlanete && Convert.ToInt32(ligneDepense["numeroMission"]) == numeroMission)
                {
                    // On ajoute le montant de la dépense au total des dépenses
                    totalDepenses += Convert.ToInt32(ligneDepense["montant"]);
                }
            }

            // On calcule le budget restant en soustrayant les dépenses du budget initial
            int budgetRestant = budgetInitial - totalDepenses;

            // On récupère le matricule du chef de mission sous forme de texte
            string matriculeChef = ligneMission["matriculeChef"].ToString();

            // On donne au nom du chef la valeur du matricule par défaut
            string nomChef = matriculeChef;

            // On parcourt toutes les lignes de la table des membres
            foreach (DataRow ligneMembre in MesDatas.DsGlobal.Tables["Membre"].Rows)
            {
                // On vérifie si le matricule du membre est celui du chef de mission
                if (ligneMembre["matricule"].ToString() == matriculeChef)
                {
                    // On assemble le nom et le prénom pour faire le nom complet du chef
                    nomChef = ligneMembre["nom"].ToString() + " " + ligneMembre["prenom"].ToString();

                    // On arrête de chercher puisque le chef est identifié
                    break;
                }
            }

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ CRÉATION DU DOCUMENT ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On crée un nouveau document PDF vide
            PdfDocument document = new PdfDocument();

            // On donne un titre au document avec le nom de la planète et le numéro de mission
            document.Info.Title = $"Mission {nomPlanete}-{numeroMission}";

            // On indique l'auteur du document PDF
            document.Info.Author = "Stargate";

            // On indique le sujet du document PDF
            document.Info.Subject = $"Rapport de la mission {nomPlanete}{numeroMission}";

            // On ajoute une nouvelle page dans le document PDF
            PdfPage page = document.AddPage();

            // On règle la taille de la page au format A4
            page.Size = PageSize.A4;

            // On règle l'orientation de la page en mode portrait
            page.Orientation = PageOrientation.Portrait;

            // On crée l'outil graphique qui permet de dessiner sur la page
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // On crée l'outil qui permet d'écrire du texte avec des retours à la ligne automatiques
            XTextFormatter tf = new XTextFormatter(gfx);

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ PALETTE DE COULEURS ═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On crée la couleur bleu foncé
            XColor bleuFonce = XColor.FromArgb(28, 78, 128);

            // On crée la couleur bleu moyen
            XColor bleuMoyen = XColor.FromArgb(52, 120, 185);

            // On crée la couleur bleu pâle
            XColor bleuPale = XColor.FromArgb(210, 228, 245);

            // On crée la couleur bleu très pâle
            XColor bleuTresPale = XColor.FromArgb(235, 243, 251);

            // On crée la couleur gris foncé
            XColor grisFonce = XColor.FromArgb(55, 55, 55);

            // On crée la couleur gris moyen
            XColor grisMoyen = XColor.FromArgb(140, 140, 140);

            // On crée la couleur gris clair
            XColor grisClair = XColor.FromArgb(220, 220, 220);

            // On crée la couleur gris fond
            XColor grisFond = XColor.FromArgb(248, 248, 248);

            // On crée la couleur blanche
            XColor blanc = XColors.White;

            // On crée la couleur rouge
            XColor rouge = XColor.FromArgb(192, 57, 43);

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ POLICES ═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On crée la police pour le grand titre
            XFont fontTitre = new XFont("Segoe UI", 15, XFontStyle.Bold);

            // On crée la police pour le sous-titre
            XFont fontSousTitre = new XFont("Segoe UI", 9, XFontStyle.Regular);

            // On crée la police pour les titres de section
            XFont fontSection = new XFont("Segoe UI", 10, XFontStyle.Bold);

            // On crée la police pour le texte en gras
            XFont fontGras = new XFont("Segoe UI", 9, XFontStyle.Bold);

            // On crée la police pour le texte normal
            XFont fontNormal = new XFont("Segoe UI", 9, XFontStyle.Regular);

            // On crée la police pour le petit texte
            XFont fontPetit = new XFont("Segoe UI", 7, XFontStyle.Regular);

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ MARGES ══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On fixe la taille de la marge gauche
            double margeG = 36;

            // On calcule la largeur utile de la page
            double largeurPage = page.Width - margeG * 2;

            // On initialise la position verticale à zéro
            double y = 0;

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ MÉTHODES LOCALES ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            void controlerEspacePage()
            {
                // On vérifie si la position verticale dépasse la limite du bas de la page
                if (y > page.Height - 70)
                {
                    // On écrit le numéro de la page actuelle tout en bas
                    gfx.DrawString($"— {document.PageCount} —", fontPetit, new XSolidBrush(grisMoyen), new XRect(margeG, page.Height - 25, largeurPage, 12), XStringFormats.Center);

                    // On crée une nouvelle page dans le document PDF
                    page = document.AddPage();

                    // On règle la taille de la nouvelle page au format A4
                    page.Size = PageSize.A4;

                    // On règle l'orientation de la nouvelle page en mode portrait
                    page.Orientation = PageOrientation.Portrait;

                    // On crée l'outil graphique pour dessiner sur la nouvelle page
                    gfx = XGraphics.FromPdfPage(page);

                    // On crée l'outil pour écrire du texte sur la nouvelle page
                    tf = new XTextFormatter(gfx);

                    // On remet la position verticale en haut de la nouvelle page
                    y = 30;
                }
            }

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On initialise le compteur des sections à zéro
            int numeroSection = 0;

            void dessinerTitreSection(string titre)
            {
                // On contrôle l'espace disponible sur la page
                controlerEspacePage();

                // On augmente le numéro de la section de un
                numeroSection++;

                // On descend le curseur vertical
                y += 12;

                // On dessine le petit rectangle vertical en bleu moyen
                gfx.DrawRectangle(new XSolidBrush(bleuMoyen), margeG, y, 4, 18);

                // On écrit le numéro de la section formaté sur deux chiffres
                gfx.DrawString($"{numeroSection:D2}", fontPetit, new XSolidBrush(bleuMoyen), new XRect(margeG + 9, y, 16, 18), XStringFormats.CenterLeft);

                // On écrit le titre en lettres majuscules et en gris foncé
                gfx.DrawString(titre.ToUpper(), fontSection, new XSolidBrush(grisFonce), new XRect(margeG + 26, y, largeurPage - 26, 18), XStringFormats.CenterLeft);

                // On descend le curseur vertical sous le texte
                y += 20;

                // On trace une ligne horizontale fine en gris clair
                gfx.DrawLine(new XPen(grisClair, 0.5), margeG + 9, y, margeG + largeurPage, y);

                // On ajoute un petit espace vertical sous la ligne
                y += 7;
            }

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            void afficherBlocInfos(string[,] champs)
            {
                // On récupère le nombre total de lignes dans le tableau
                int total = champs.GetLength(0);

                // On calcule la largeur de chaque colonne
                double colW = largeurPage / 2 - 2;

                // On fixe la hauteur pour le texte du titre
                double labelH = 13;

                // On fixe la hauteur pour le texte de la valeur
                double valH = 16;

                // On calcule la hauteur totale de la case
                double cellH = labelH + valH + 2;

                // On parcourt les éléments deux par deux
                for (int i = 0; i < total; i += 2)
                {
                    // On contrôle l'espace disponible sur la page
                    controlerEspacePage();

                    // On gère les deux colonnes d'affichage
                    for (int col = 0; col < 2; col++)
                    {
                        // On calcule l'indice de l'élément à afficher
                        int idx = i + col;

                        // On arrête si on a dépassé le nombre total d'éléments
                        if (idx >= total)
                        {
                            break;
                        }

                        // On calcule la position horizontale de la case
                        double x = margeG + col * (colW + 4);

                        // On dessine le rectangle de fond bleu pâle pour le titre
                        gfx.DrawRectangle(new XSolidBrush(bleuPale), x, y, colW, labelH);

                        // On écrit le texte du titre en bleu foncé
                        gfx.DrawString(champs[idx, 0], fontGras, new XSolidBrush(bleuFonce), new XRect(x + 5, y + 1, colW - 10, labelH - 1), XStringFormats.CenterLeft);

                        // On dessine le rectangle de fond blanc pour la valeur
                        gfx.DrawRectangle(new XSolidBrush(blanc), x, y + labelH, colW, valH);

                        // On écrit le texte de la valeur en gris foncé
                        gfx.DrawString(champs[idx, 1], fontNormal, new XSolidBrush(grisFonce), new XRect(x + 5, y + labelH + 1, colW - 10, valH - 1), XStringFormats.CenterLeft);

                        // On dessine la bordure fine en gris clair tout autour de la case
                        gfx.DrawRectangle(new XPen(grisClair, 0.4), x, y, colW, cellH);
                    }

                    // On descend le curseur vertical sous la ligne de cases
                    y += cellH + 3;
                }

                // On ajoute un petit espace de sécurité en bas du bloc
                y += 4;
            }

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            void afficherTexteLibre(string texte)
            {
                // On contrôle l'espace disponible sur la page
                controlerEspacePage();

                // On vérifie si le texte est vide avec un if else
                if (string.IsNullOrWhiteSpace(texte))
                {
                    // On donne un texte par défaut si la variable est vide
                    texte = "Aucun texte saisi.";
                }
                else
                {
                    // On garde le texte d'origine s'il contient des caractères
                    texte = texte;
                }

                // On calcule le nombre de lignes nécessaires pour le texte
                int nbLignes = Math.Max(2, (int)Math.Ceiling(texte.Length / 80.0));

                // On calcule la hauteur totale de la case de texte
                double hauteur = nbLignes * 13 + 12;

                // On dessine le rectangle de fond en bleu très pâle
                gfx.DrawRectangle(new XSolidBrush(bleuTresPale), margeG, y, largeurPage, hauteur);

                // On dessine la bordure fine autour de la case en bleu pâle
                gfx.DrawRectangle(new XPen(bleuPale, 0.6), margeG, y, largeurPage, hauteur);

                // On écrit le texte libre à l'intérieur de la case avec l'outil de texte
                tf.DrawString(texte, fontNormal, new XSolidBrush(grisFonce), new XRect(margeG + 7, y + 5, largeurPage - 14, hauteur - 8));

                // On descend le curseur vertical sous la case de texte
                y += hauteur + 8;
            }

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            void construireTableau(DataTable tableDonnees, string messageVide, int[] proportions = null)
            {
                // On vérifie si la table ne contient aucune ligne de données
                if (tableDonnees.Rows.Count == 0)
                {
                    controlerEspacePage();
                    gfx.DrawString(messageVide, fontPetit, new XSolidBrush(grisMoyen), new XRect(margeG, y, largeurPage, 14), XStringFormats.CenterLeft);
                    y += 18;
                    return;
                }

                controlerEspacePage();

                int nbCols = tableDonnees.Columns.Count;
                double[] colsW = new double[nbCols];

                if (proportions != null && proportions.Length == nbCols)
                {
                    for (int i = 0; i < nbCols; i++)
                    {
                        colsW[i] = largeurPage * proportions[i] / 100.0;
                    }
                }
                else
                {
                    for (int i = 0; i < nbCols; i++)
                    {
                        colsW[i] = largeurPage / nbCols;
                    }
                }

                // On dessine la ligne des titres
                gfx.DrawRectangle(new XSolidBrush(bleuFonce), margeG, y, largeurPage, 17);
                double px = margeG;

                for (int i = 0; i < nbCols; i++)
                {
                    gfx.DrawString(tableDonnees.Columns[i].ColumnName, fontGras, new XSolidBrush(blanc), new XRect(px + 4, y + 2, colsW[i] - 4, 13), XStringFormats.CenterLeft);
                    px += colsW[i];
                }

                y += 18;
                bool pair = false;

                // On parcourt toutes les lignes de données du tableau
                foreach (DataRow row in tableDonnees.Rows)
                {
                    controlerEspacePage();

                    // CORRECTION INTERNE : On définit une hauteur fixe un peu plus grande pour la ligne (ex: 28) 
                    // pour laisser la place aux longs textes de faire 2 lignes si besoin.
                    double hauteurLigne = 28;

                    if (pair)
                    {
                        gfx.DrawRectangle(new XSolidBrush(bleuTresPale), margeG, y - 1, largeurPage, hauteurLigne);
                    }
                    else
                    {
                        gfx.DrawRectangle(new XSolidBrush(grisFond), margeG, y - 1, largeurPage, hauteurLigne);
                    }

                    // On trace le petit trait horizontal sous la ligne de données
                    gfx.DrawLine(new XPen(grisClair, 0.3), margeG, y + hauteurLigne - 1, margeG + largeurPage, y + hauteurLigne - 1);

                    px = margeG;

                    // On parcourt toutes les cellules de la ligne
                    for (int i = 0; i < nbCols; i++)
                    {
                        string texteCellule = row[i] != null ? row[i].ToString() : "";

                        // CORRECTION ICI : Au lieu de gfx.DrawString, on utilise tf.DrawString !
                        // On lui donne le rectangle exact de la cellule. Si le texte tape dans le bord de la colonne, 
                        // il revient à la ligne du dessous automatiquement sans déborder sur sa voisine !
                        tf.DrawString(texteCellule, fontNormal, new XSolidBrush(grisFonce), new XRect(px + 4, y + 2, colsW[i] - 6, hauteurLigne - 4));

                        px += colsW[i];
                    }

                    // On descend le curseur de la hauteur de notre ligne
                    y += hauteurLigne;
                    pair = !pair;
                }

                gfx.DrawLine(new XPen(bleuMoyen, 0.5), margeG, y, margeG + largeurPage, y);
                y += 8;
            }

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ BANDEAU TITRE ═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On dessine le rectangle de fond bleu foncé tout en haut de la page
            gfx.DrawRectangle(new XSolidBrush(bleuFonce), 0, 0, page.Width, 55);

            // On écrit le petit sous-titre en bleu pâle au-dessus du titre principal
            gfx.DrawString("RAPPORT DE MISSION", fontSousTitre, new XSolidBrush(bleuPale), new XRect(margeG, 10, largeurPage, 13), XStringFormats.CenterLeft);

            // On écrit le grand titre principal en blanc avec le nom de la planète et le numéro
            gfx.DrawString($"{nomPlanete}  —  Mission n°{numeroMission}", fontTitre, new XSolidBrush(blanc), new XRect(margeG, 24, largeurPage, 22), XStringFormats.CenterLeft);

            // On trace une ligne horizontale en bleu moyen juste sous le rectangle foncé
            gfx.DrawLine(new XPen(bleuMoyen, 2), 0, 55, page.Width, 55);

            // On positionne le curseur vertical sous le bandeau pour la suite du texte
            y = 68;

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ SECTION 1 : INFOS GÉNÉRALES ═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On dessine le titre de la première section pour les informations générales
            dessinerTitreSection("Informations générales");

            // On affiche le bloc de cases avec toutes les données principales de la mission
            afficherBlocInfos(new string[,] 
            {
                { "Planète",           nomPlanete                                          },
                { "Numéro de mission", numeroMission.ToString()                            },
                { "Chef de mission",   nomChef                                             },
                { "Date de départ",    FormatDate(ligneMission["dateDepart"].ToString())   },
                { "Date de retour",    FormatDate(ligneMission["dateRetour"].ToString())   },
                { "Nb membres requis", nbMembre.ToString()                                 },
                { "Budget initial",    budgetInitial + " €"                                },
                { "Total dépenses",    totalDepenses + " €"                                },
                { "Budget restant",    budgetRestant + " €"                                },
                { "Objectif Databaz",  objectifDatabaz + " tonnes"                         }
            });

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ SECTION 2 : FEUILLE DE ROUTE ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On dessine le titre de la deuxième section pour la feuille de route
            dessinerTitreSection("Feuille de route");

            // On prépare une variable vide pour stocker le texte de la feuille de route
            string texteFeuilleRoute = "";

            // On vérifie si la feuille de route existe dans la ligne avec un if else
            if (ligneMission["feuilleDeRoute"] != null)
            {
                // On transforme le contenu de la feuille de route en texte
                texteFeuilleRoute = ligneMission["feuilleDeRoute"].ToString();
            }
            else
            {
                // On laisse un texte vide si la feuille de route n'existe pas
                texteFeuilleRoute = "";
            }

            // On affiche le bloc de texte avec le contenu de la feuille de route
            afficherTexteLibre(texteFeuilleRoute);

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ SECTION 3 : ÉQUIPE ══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On dessine le titre de la troisième section pour les membres de la mission
            dessinerTitreSection("Membres de la mission");

            // On contrôle l'espace disponible sur la page
            controlerEspacePage();

            // On récupère toutes les lignes des membres inscrits pour cette planète et cette mission
            DataRow[] effectifAttribue = dtComposerPDF.Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numeroMission}");

            // On compte le nombre total de membres trouvés dans la liste
            int totalInscrits = effectifAttribue.Length;

            // On écrit le texte qui indique le nombre de membres affectés à la mission
            gfx.DrawString($"Membres affectés : {totalInscrits}", fontNormal, new XSolidBrush(grisMoyen), new XRect(margeG, y, largeurPage, 13), XStringFormats.CenterLeft);

            // On descend le curseur vertical sous le texte du nombre de membres
            y += 18;

            // On crée une table de données vide pour préparer notre tableau d'affichage
            DataTable dtPersonnel = new DataTable();

            // On ajoute la colonne pour le matricule du membre
            dtPersonnel.Columns.Add("Matricule");

            // On ajoute la colonne pour le nom complet du membre
            dtPersonnel.Columns.Add("Nom");

            // On ajoute la colonne pour le grade ou le métier du membre
            dtPersonnel.Columns.Add("Métier / Grade");

            // On parcourt chaque ligne de notre liste de membres inscrits
            foreach (DataRow ligneLiaison in effectifAttribue)
            {
                // On récupère le numéro de matricule du membre actuel
                string idMembre = ligneLiaison["matriculeMembre"].ToString();

                // On cherche les informations de ce membre dans la table principale des membres
                DataRow[] rechercheFiche = dtMembrePDF.Select($"matricule = '{idMembre}'");

                // On passe directement au membre suivant avec un if si la fiche n'existe pas
                if (rechercheFiche.Length == 0)
                {
                    continue;
                }

                // On assemble le prénom et le nom pour fabriquer l'identité complète
                string identite = $"{rechercheFiche[0]["prenom"]} {rechercheFiche[0]["nom"]}";

                // On crée une variable vide pour stocker le métier ou le grade du membre
                string fonctionOccupee = "";

                // On vérifie si le matricule commence par la lettre M avec un if else
                if (idMembre.StartsWith("M"))
                {
                    // On cherche les informations du membre dans la table des militaires
                    DataRow[] classeMilitaire = dtMilitairePDF.Select($"matriculeMembre = '{idMembre}'");

                    // On vérifie avec un if si la ligne militaire a été trouvée
                    if (classeMilitaire.Length > 0)
                    {
                        // On récupère le grade du militaire
                        fonctionOccupee = classeMilitaire[0]["grade"].ToString();
                    }
                }
                else
                {
                    // On cherche les informations du membre dans la table des civils
                    DataRow[] classeCivil = dtCivilPDF.Select($"matriculeMembre = '{idMembre}'");

                    // On vérifie avec un if si la ligne civile a été trouvée
                    if (classeCivil.Length > 0)
                    {
                        // On récupère la spécialité du civil
                        fonctionOccupee = classeCivil[0]["Specialite"].ToString();
                    }
                }

                // On ajoute toutes les informations trouvées dans une nouvelle ligne de notre table
                dtPersonnel.Rows.Add(idMembre, identite, fonctionOccupee);
            }

            // On dessine le tableau sur la page avec les colonnes et les dimensions choisies
            construireTableau(dtPersonnel, "Aucun membre enregistré pour cette mission.", new int[] { 20, 45, 35 });

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ SECTION 4 : DÉPENSES ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On dessine le titre de la quatrième section pour le suivi des dépenses
            dessinerTitreSection("Suivi des dépenses");

            // On crée une table de données temporaire pour ranger les informations des dépenses
            DataTable dtDepenseTemp = new DataTable();

            // On ajoute la colonne pour la date de la dépense
            dtDepenseTemp.Columns.Add("Date");

            // On ajoute la colonne pour le type de la dépense
            dtDepenseTemp.Columns.Add("Type");

            // On ajoute la colonne pour le montant de la dépense
            dtDepenseTemp.Columns.Add("Montant");

            // On ajoute la colonne pour le motif de la dépense
            dtDepenseTemp.Columns.Add("Motif");

            // On parcourt toutes les lignes des dépenses de cette planète et de cette mission
            foreach (DataRow drDepense in dtDepensePDF.Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numeroMission}"))
            {
                // On initialise le texte du type de dépense avec la valeur inconnu par défaut
                string typeLibele = "Inconnu";

                // On vérifie avec un if si la table des types de dépenses existe bien
                if (dtTypeDepensePDF != null)
                {
                    // On cherche la ligne correspondante à l'identifiant du type de la dépense
                    DataRow[] ligneType = dtTypeDepensePDF.Select($"id = {drDepense["idTypeDepense"]}");

                    // On vérifie avec un if si on a trouvé le type dans la table
                    if (ligneType.Length > 0)
                    {
                        // On récupère le texte du libellé du type de dépense
                        typeLibele = ligneType[0]["libelle"].ToString();
                    }
                }

                // On prépare une variable vide pour stocker la date textuelle
                string dateTexte = "";

                // On vérifie si la date existe avec un if else
                if (drDepense["dateD"] != null)
                {
                    // On transforme la date de la dépense en texte
                    dateTexte = drDepense["dateD"].ToString();
                }
                else
                {
                    // On laisse un texte vide si la date n'existe pas
                    dateTexte = "";
                }

                // On prépare une variable vide pour stocker le motif textuel
                string motifTexte = "";

                // On vérifie si le motif existe avec un if else
                if (drDepense["motif"] != null)
                {
                    // On transforme le motif de la dépense en texte
                    motifTexte = drDepense["motif"].ToString();
                }
                else
                {
                    // On laisse un texte vide si le motif n'existe pas
                    motifTexte = "";
                }

                // On ajoute toutes les informations de la dépense dans une nouvelle ligne de notre table
                dtDepenseTemp.Rows.Add(FormatDate(dateTexte), typeLibele, drDepense["montant"] + " €", motifTexte);
            }

            // On dessine le tableau des dépenses sur la page avec les dimensions choisies
            construireTableau(dtDepenseTemp, "Aucune dépense enregistrée.", new int[] { 20, 20, 20, 40 });

            // On contrôle l'espace disponible sur la page
            controlerEspacePage();

            // On prépare le pinceau pour choisir la couleur du texte du bilan
            XSolidBrush pinceauBilan;

            // On utilise un if else à la place du code avec le point d'interrogation pour la couleur du texte
            if (budgetRestant < 0)
            {
                // On choisit le pinceau rouge si l'argent restant est inférieur à zéro
                pinceauBilan = new XSolidBrush(rouge);
            }
            else
            {
                // On choisit le pinceau bleu moyen si l'argent restant est positif ou égal à zéro
                pinceauBilan = new XSolidBrush(bleuMoyen);
            }

            // On écrit le texte du total des dépenses et du budget restant à droite de la page
            gfx.DrawString($"Total : {totalDepenses} €   |   Restant : {budgetRestant} €", fontGras, pinceauBilan, new XRect(margeG, y, largeurPage, 14), XStringFormats.CenterRight);

            // On descend le curseur vertical sous le texte du bilan financier
            y += 18;

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ SECTION 5 : CAPTURES ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On dessine le titre de la cinquième section pour le bilan des captures
            dessinerTitreSection("Bilan des captures");

            // On crée une liste pour stocker les codes uniques de toutes les espèces concernées
            HashSet<int> listeCodesEspeces = new HashSet<int>();

            // On parcourt la table des objectifs pour récupérer les codes des espèces ciblées
            foreach (DataRow ligneCible in dtObjectifPDF.Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numeroMission}"))
            {
                // On ajoute le code de l'espèce dans notre liste unique
                listeCodesEspeces.Add(Convert.ToInt32(ligneCible["idEspeceEnnemi"]));
            }

            // On parcourt la table des captures pour récupérer les codes des espèces attrapées
            foreach (DataRow ligneSaisie in dtCapturePDF.Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numeroMission}"))
            {
                // On ajoute le code de l'espèce dans notre liste unique
                listeCodesEspeces.Add(Convert.ToInt32(ligneSaisie["idEspeceEnnemi"]));
            }

            // On crée une table de données pour préparer notre tableau d'affichage
            DataTable dtSyntheseCaptures = new DataTable();

            // On ajoute la colonne pour le nom de l'espèce
            dtSyntheseCaptures.Columns.Add("Espèce");

            // On ajoute la colonne pour la quantité demandée en objectif
            dtSyntheseCaptures.Columns.Add("Objectif");

            // On ajoute la colonne pour la quantité réellement capturée
            dtSyntheseCaptures.Columns.Add("Capturés");

            // On ajoute la colonne pour le pourcentage de réussite
            dtSyntheseCaptures.Columns.Add("Taux");

            // On parcourt chaque code d'espèce trouvé pour calculer ses résultats
            foreach (int codeEspece in listeCodesEspeces)
            {
                // On cherche les informations de cette espèce dans la table principale des espèces
                DataRow[] ficheEspece = dtEspecePDF.Select($"id = {codeEspece}");

                // On prépare une variable pour le nom de l'espèce
                string libelleEspece = "";

                // On utilise un if else à la place du code avec le point d'interrogation pour le nom
                if (ficheEspece.Length > 0)
                {
                    // On récupère le vrai nom de l'espèce si la ligne existe
                    libelleEspece = ficheEspece[0]["nom"].ToString();
                }
                else
                {
                    // On écrit que l'espèce est inconnue si la ligne n'existe pas
                    libelleEspece = "Inconnue";
                }

                // On initialise la quantité de l'objectif à zéro
                int quotaFixe = 0;

                // On cherche si cette espèce possède un objectif chiffré dans la table correspondante
                DataRow[] rechercheObjectif = dtObjectifPDF.Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numeroMission} AND idEspeceEnnemi = {codeEspece}");

                // On vérifie avec un if si un objectif a bien été défini
                if (rechercheObjectif.Length > 0)
                {
                    // On récupère le nombre fixé pour cet objectif
                    quotaFixe = Convert.ToInt32(rechercheObjectif[0]["objectif"]);
                }

                // On initialise la quantité attrapée à zéro
                int quantiteAttrapee = 0;

                // On cherche si des captures ont été saisies pour cette espèce dans la table correspondante
                DataRow[] rechercheCapture = dtCapturePDF.Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numeroMission} AND idEspeceEnnemi = {codeEspece}");

                // On vérifie avec un if si des captures ont bien été trouvées
                if (rechercheCapture.Length > 0)
                {
                    // On récupère le nombre d'animaux attrapés
                    quantiteAttrapee = Convert.ToInt32(rechercheCapture[0]["nombre"]);
                }

                // On initialise la variable pour stocker le pourcentage final
                double taux = 0.0;

                // On utilise une structure de if else pour calculer le taux sans risquer de division par zéro
                if (quotaFixe > 0)
                {
                    // On calcule le pourcentage et on l'arrondit à un chiffre après la virgule
                    taux = Math.Round((double)quantiteAttrapee / quotaFixe * 100, 1);
                }
                else
                {
                    // On vérifie avec un deuxième if imbriqué si on a fait des captures sans objectif
                    if (quantiteAttrapee > 0)
                    {
                        // On fixe le taux à cent pour cent si des captures existent sans quota demandé
                        taux = 100.0;
                    }
                    else
                    {
                        // On laisse le taux à zéro pour cent s'il n'y a ni objectif ni capture
                        taux = 0.0;
                    }
                }

                // On ajoute toutes les données calculées dans une nouvelle ligne de notre table
                dtSyntheseCaptures.Rows.Add(libelleEspece, quotaFixe.ToString(), quantiteAttrapee.ToString(), $"{taux} %");
            }

            // On dessine le tableau de synthèse des captures sur la page avec les dimensions choisies
            construireTableau(dtSyntheseCaptures, "Aucune capture enregistrée.", new int[] { 35, 20, 20, 25 });

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ GRAPHIQUE CAMEMBERT ═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On initialise le compteur total des captures à zéro
            int grandTotalCaptures = 0;

            // On parcourt toutes les lignes de données de notre tableau de synthèse
            foreach (DataRow ligneSynthese in dtSyntheseCaptures.Rows)
            {
                // On ajoute la quantité capturée de cette espèce au total absolu
                grandTotalCaptures += Convert.ToInt32(ligneSynthese["Capturés"]);
            }

            // On utilise un if pour dessiner le graphique uniquement s'il y a des captures
            if (grandTotalCaptures > 0)
            {
                // On descend le curseur vertical pour laisser un espace avant le graphique
                y += 20;

                // On vérifie avec un if si l'espace restant sur la page est insuffisant pour le graphique
                if (y + 160 > page.Height - 60)
                {
                    // On écrit le numéro de la page actuelle tout en bas avant de changer de page
                    gfx.DrawString($"— {document.PageCount} —", fontPetit, XBrushes.Gray, new XRect(margeG, page.Height - 30, largeurPage, 14), XStringFormats.Center);

                    // On crée une nouvelle page dans le document PDF
                    page = document.AddPage();

                    // On crée l'outil graphique pour dessiner sur la nouvelle page
                    gfx = XGraphics.FromPdfPage(page);

                    // On crée l'outil pour écrire du texte sur la nouvelle page
                    tf = new XTextFormatter(gfx);

                    // On remet la position verticale en haut de la nouvelle page
                    y = 40;
                }

                // On crée un tableau avec des couleurs harmonieuses pour les parts du graphique
                XColor[] paletteCouleurs = new XColor[] {
                    XColor.FromArgb(79, 129, 189),
                    XColor.FromArgb(192, 80, 77),
                    XColor.FromArgb(155, 187, 89),
                    XColor.FromArgb(128, 100, 162),
                    XColor.FromArgb(75, 172, 198),
                    XColor.FromArgb(239, 154, 154)
                };

                // On fixe le diamètre du graphique en camembert
                double tailleDisque = 120;

                // On fixe la position horizontale du graphique sur la marge gauche
                double xCamembert = margeG;

                // On crée la zone rectangulaire qui va contenir le disque du graphique
                XRect zoneCamembert = new XRect(xCamembert, y, tailleDisque, tailleDisque);

                // On fixe l'angle de départ tout en haut du cercle
                double angleCourant = -90;

                // On initialise l'index pour parcourir la liste des couleurs
                int indexCouleur = 0;

                // On calcule la position horizontale pour commencer à écrire la légende à droite
                double xLegende = margeG + tailleDisque + 30;

                // On calcule la position verticale pour le début de la légende
                double yLegende = y + 10;

                // On parcourt chaque ligne du tableau de synthèse pour créer les parts du graphique
                foreach (DataRow ligneSynthese in dtSyntheseCaptures.Rows)
                {
                    // On récupère la quantité d'animaux attrapés pour l'espèce courante
                    int nbCapturesEspece = Convert.ToInt32(ligneSynthese["Capturés"]);

                    // On passe directement à l'espèce suivante avec un if si aucune capture n'a été faite
                    if (nbCapturesEspece == 0)
                    {
                        continue;
                    }

                    // On calcule l'angle de la portion du cercle de manière proportionnelle
                    double anglePortion = ((double)nbCapturesEspece / grandTotalCaptures) * 360.0;

                    // On sélectionne la couleur de la part dans notre tableau de couleurs
                    XColor couleurPortion = paletteCouleurs[indexCouleur % paletteCouleurs.Length];

                    // On dessine la part du camembert pleine avec la couleur choisie
                    gfx.DrawPie(new XSolidBrush(couleurPortion), zoneCamembert, angleCourant, anglePortion);

                    // On dessine un contour blanc d'un pixel pour séparer proprement les parts
                    gfx.DrawPie(new XPen(XColors.White, 1), zoneCamembert, angleCourant, anglePortion);

                    // On dessine le petit carré de couleur pour la légende à droite
                    gfx.DrawRectangle(new XSolidBrush(couleurPortion), xLegende, yLegende + 2, 10, 10);

                    // On récupère le nom de l'espèce textuelle
                    string nomEspece = ligneSynthese["Espèce"].ToString();

                    // On récupère le pourcentage textuel de réussite
                    string pourcent = ligneSynthese["Taux"].ToString();

                    // On assemble les informations pour fabriquer le texte complet de la ligne de légende
                    string texteLegende = $"{nomEspece} : {nbCapturesEspece} ({pourcent})";

                    // On écrit le texte de la légende à côté du petit carré de couleur
                    gfx.DrawString(texteLegende, fontNormal, XBrushes.Black, new XRect(xLegende + 18, yLegende, largeurPage - xLegende, 14), XStringFormats.CenterLeft);

                    // On descend le curseur vertical de la légende pour la ligne suivante
                    yLegende += 18;

                    // On ajoute l'angle de la part actuelle à l'angle total pour la part suivante
                    angleCourant += anglePortion;

                    // On augmente l'index pour utiliser la couleur suivante du tableau au prochain tour
                    indexCouleur++;
                }

                // On calcule l'élément le plus bas entre le camembert et la légende pour descendre le curseur global
                y += Math.Max(tailleDisque, (yLegende - y)) + 15;
            }

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ SECTION 6 : CONTACTS ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On dessine le titre de la sixième section pour les contacts avec les informateurs
            dessinerTitreSection("Contacts avec les informateurs");

            // On crée une table de données temporaire pour ranger les informations des contacts
            DataTable dtContactsTemp = new DataTable();

            // On ajoute la colonne pour le nom de l'informateur
            dtContactsTemp.Columns.Add("Informateur");

            // On ajoute la colonne pour la date du contact
            dtContactsTemp.Columns.Add("Date contact");

            // On ajoute la colonne pour l'argent donné à l'informateur
            dtContactsTemp.Columns.Add("Somme versée");

            // On ajoute la colonne pour l'avis ou l'appréciation du contact
            dtContactsTemp.Columns.Add("Appréciation");

            // On parcourt toutes les lignes de la table des contacts pour cette planète et cette mission
            foreach (DataRow ligneContact in MesDatas.DsGlobal.Tables["Contact"].Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numeroMission}"))
            {
                // On initialise une variable vide pour la date du contact
                string dateContact = "";

                // On vérifie avec un if else si la date existe dans la base de données
                if (ligneContact["dateC"] != DBNull.Value)
                {
                    // On transforme et on formate la date du contact en texte
                    dateContact = FormatDate(ligneContact["dateC"].ToString());
                }
                else
                {
                    // On laisse un texte vide si la date n'existe pas
                    dateContact = "";
                }

                // On initialise une variable pour l'argent versé
                string somme = "";

                // On vérifie avec un if else si le montant existe dans la base de données
                if (ligneContact["sommeVersee"] != DBNull.Value)
                {
                    // On convertit le montant en texte avec deux chiffres après la virgule et le symbole euro
                    somme = $"{Convert.ToDecimal(ligneContact["sommeVersee"]):N2} €";
                }
                else
                {
                    // On écrit un montant à zéro par défaut si la valeur n'existe pas
                    somme = "0,00 €";
                }

                // On initialise une variable vide pour l'appréciation
                string appreciation = "";

                // On vérifie avec un if else si l'appréciation existe dans la base de données
                if (ligneContact["appreciation"] != DBNull.Value)
                {
                    // On transforme l'appréciation en texte
                    appreciation = ligneContact["appreciation"].ToString();
                }
                else
                {
                    // On laisse un texte vide si l'appréciation n'existe pas
                    appreciation = "";
                }

                // On initialise une variable vide pour le nom de l'informateur
                string nomInformateur = "";

                // On vérifie avec un if else si le nom de code existe dans la base de données
                if (ligneContact["nomCodeInformateur"] != DBNull.Value)
                {
                    // On transforme le nom de code en texte
                    nomInformateur = ligneContact["nomCodeInformateur"].ToString();
                }
                else
                {
                    // On laisse un texte vide si le nom n'existe pas
                    nomInformateur = "";
                }

                // On ajoute toutes les données du contact dans une nouvelle ligne de notre table temporaire
                dtContactsTemp.Rows.Add(nomInformateur, dateContact, somme, appreciation);
            }

            // On dessine le tableau des contacts sur la page avec les dimensions choisies
            construireTableau(dtContactsTemp, "Aucun contact répertorié.", new int[] { 15, 15, 20, 50 });

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ SECTION 7 : JOURNAL DE BORD ═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On dessine le titre de la septième section pour le journal de bord
            dessinerTitreSection("Journal de bord");

            // On récupère toutes les lignes du journal de bord correspondantes à cette planète et cette mission
            DataRow[] entrees = dtJournalDeBordPDF.Select($"nomPlanete = '{nomPlanete}' AND numero = {numeroMission}");

            // On vérifie avec un if else si le journal de bord ne contient aucune entrée
            if (entrees.Length == 0)
            {
                // On contrôle l'espace disponible sur la page
                controlerEspacePage();

                // On écrit le texte qui indique qu'aucune entrée n'existe dans le journal
                gfx.DrawString("Aucune entrée dans le journal de bord.", fontPetit, new XSolidBrush(grisMoyen), new XRect(margeG, y, largeurPage, 14), XStringFormats.CenterLeft);

                // On descend le curseur vertical sous le texte
                y += 18;
            }
            else
            {
                // On crée une liste pour stocker et trier les entrées du journal de bord
                List<DataRow> entreesTriees = new List<DataRow>(entrees);

                // On trie la liste des entrées par ordre chronologique de date
                entreesTriees.Sort((a, b) =>
                {
                    // On prépare les deux variables pour recevoir les vraies dates converties
                    DateTime da, db;

                    // On tente de convertir le texte de la première date en vraie date informatique
                    DateTime.TryParseExact(a["dateJ"].ToString(), new[] { "yyyy-MM-dd", "dd/MM/yyyy" }, null, System.Globalization.DateTimeStyles.None, out da);

                    // On tente de convertir le texte de la deuxième date en vraie date informatique
                    DateTime.TryParseExact(b["dateJ"].ToString(), new[] { "yyyy-MM-dd", "dd/MM/yyyy" }, null, System.Globalization.DateTimeStyles.None, out db);

                    // On compare les deux dates pour le rangement de la liste
                    return da.CompareTo(db);
                });

                // On parcourt chaque entrée triée du journal de bord pour l'afficher sur la page
                foreach (DataRow drJournal in entreesTriees)
                {
                    // On contrôle l'espace disponible sur la page
                    controlerEspacePage();

                    // On initialise une variable vide pour la date de la ligne actuelle
                    string dateJournal = "";

                    // On vérifie avec un if else si la date existe bien dans la base de données
                    if (drJournal["dateJ"] != DBNull.Value)
                    {
                        // On transforme et on formate la date du journal en texte
                        dateJournal = FormatDate(drJournal["dateJ"].ToString());
                    }
                    else
                    {
                        // On laisse un texte vide si la date n'existe pas
                        dateJournal = "";
                    }

                    // On initialise une variable vide pour le commentaire de la ligne actuelle
                    string commentaire = "";

                    // On vérifie avec un if else si le commentaire existe bien dans la base de données
                    if (drJournal["commentaires"] != DBNull.Value)
                    {
                        // On transforme le contenu du commentaire en texte
                        commentaire = drJournal["commentaires"].ToString();
                    }
                    else
                    {
                        // On laisse un texte vide si le commentaire n'existe pas
                        commentaire = "";
                    }

                    // On vérifie avec un if si le texte du commentaire est totalement vide ou blanc
                    if (string.IsNullOrWhiteSpace(commentaire))
                    {
                        // On donne un texte générique pour indiquer que la ligne est vide
                        commentaire = "(entrée vide)";
                    }

                    // On dessine le rectangle de fond en bleu pâle pour faire la pastille de la date
                    gfx.DrawRectangle(new XSolidBrush(bleuPale), margeG, y, largeurPage, 14);

                    // On écrit le texte de la date de l'entrée en bleu foncé et en gras par-dessus la pastille
                    gfx.DrawString($"  {dateJournal}", fontGras, new XSolidBrush(bleuFonce), new XRect(margeG, y + 1, largeurPage, 12), XStringFormats.CenterLeft);

                    // On descend le curseur vertical sous la barre de la date
                    y += 16;

                    // On contrôle à nouveau l'espace disponible sur la page avant d'écrire le bloc de texte
                    controlerEspacePage();

                    // On affiche le bloc de texte avec le contenu du commentaire du journal
                    afficherTexteLibre(commentaire);
                }
            }

            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ══════════════ NUMÉRO DE PAGE FINALE ET SAUVEGARDE ═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //
            // ════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════ //

            // On écrit le numéro de la page actuelle tout en bas
            gfx.DrawString($"— {document.PageCount} —", fontPetit, new XSolidBrush(grisMoyen), new XRect(margeG, page.Height - 25, largeurPage, 12), XStringFormats.Center);

            // On prépare le nom du fichier PDF avec le nom de la planète et le numéro de la mission
            string cheminPdf = $"Mission_{nomPlanete}_{numeroMission}.pdf";

            // On sauvegarde le document PDF créé
            document.Save(cheminPdf);

            // On ferme le document pour libérer la mémoire
            document.Close();

            // On lance automatiquement l'ouverture du fichier PDF avec le lecteur par défaut du système
            System.Diagnostics.Process.Start(cheminPdf);

            // On affiche une boîte de message pour informer l'utilisateur que l'exportation a réussi
            MessageBox.Show("PDF exporté avec succès !", "Export réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ConstruireBilanCaptures(DataRow ligneMission)
        {
            // On construit le nom de la table locale dynamiquement selon la mission
            string nomTable = "BilanCapture" + ligneMission["nomPlanete"].ToString() + "-" + ligneMission["numero"].ToString();
            string nomPlanete = ligneMission["nomPlanete"].ToString();
            int numeroMission = Convert.ToInt32(ligneMission["numero"]);

            // Si la table existe déjà dans le DataSet global, on la vide pour la reconstruire proprement
            if (MesDatas.DsGlobal.Tables.Contains(nomTable))
            {
                MesDatas.DsGlobal.Tables[nomTable].Clear();
            }
            // Sinon on instancie une nouvelle DataTable avec ses colonnes typées avant de l'ajouter
            else
            {
                DataTable tblBilan = new DataTable(nomTable);
                tblBilan.Columns.Add("nomEspece", typeof(string));
                tblBilan.Columns.Add("objectif", typeof(int));
                tblBilan.Columns.Add("captures", typeof(int));
                tblBilan.Columns.Add("taux", typeof(double));
                MesDatas.DsGlobal.Tables.Add(tblBilan);
            }

            // On garde une référence directe vers la table locale
            DataTable bilan = MesDatas.DsGlobal.Tables[nomTable];

            // Premier parcours : ajout de toutes les espèces planifiées dans ObjectifCapture
            foreach (DataRow ligneObjectif in MesDatas.DsGlobal.Tables["ObjectifCapture"].Rows)
            {
                // Filtrage des objectifs correspondants à la planète et à la mission courante
                if (ligneObjectif["nomPlanete"].ToString() != nomPlanete || Convert.ToInt32(ligneObjectif["numeroMission"]) != numeroMission)
                {
                    continue;
                }

                string idEspece = ligneObjectif["idEspeceEnnemi"].ToString();
                string nomEspece = idEspece;

                // Récupération du libellé textuel de l'espèce alien dans la DataTable Espece
                foreach (DataRow ligneEspece in MesDatas.DsGlobal.Tables["Espece"].Rows)
                {
                    if (ligneEspece["id"].ToString() == idEspece)
                    {
                        nomEspece = ligneEspece["nom"].ToString();
                        break;
                    }
                }

                int objectif = Convert.ToInt32(ligneObjectif["objectif"]);
                int captures = 0;

                // Recherche du volume d'individus réellement capturés pour cette espèce
                foreach (DataRow ligneCapture in MesDatas.DsGlobal.Tables["Capturer"].Rows)
                {
                    if (ligneCapture["nomPlanete"].ToString() == nomPlanete &&
                        Convert.ToInt32(ligneCapture["numeroMission"]) == numeroMission &&
                        ligneCapture["idEspeceEnnemi"].ToString() == idEspece)
                    {
                        captures = Convert.ToInt32(ligneCapture["nombre"]);
                        break;
                    }
                }

                // Calcul du taux de réussite (gestion de la division par zéro si l'objectif est nul)
                double taux;
                if (objectif > 0)
                {
                    taux = Math.Round((double)captures / objectif * 100, 1);
                }
                else
                {
                    taux = 0;
                }

                // Enregistrement de la ligne de bilan pour cette espèce planifiée
                bilan.Rows.Add(nomEspece, objectif, captures, taux);
            }

            // Second parcours : ajout des espèces capturées de manière imprévue (hors objectifs)
            foreach (DataRow ligneCapture in MesDatas.DsGlobal.Tables["Capturer"].Rows)
            {
                if (ligneCapture["nomPlanete"].ToString() != nomPlanete || Convert.ToInt32(ligneCapture["numeroMission"]) != numeroMission)
                {
                    continue;
                }

                string idEspece = ligneCapture["idEspeceEnnemi"].ToString();
                bool aUnObjectif = false;

                // Vérification de l'existence d'un objectif initial pour éviter les doublons
                foreach (DataRow ligneObjectif in MesDatas.DsGlobal.Tables["ObjectifCapture"].Rows)
                {
                    if (ligneObjectif["nomPlanete"].ToString() == nomPlanete &&
                        Convert.ToInt32(ligneObjectif["numeroMission"]) == numeroMission &&
                        ligneObjectif["idEspeceEnnemi"].ToString() == idEspece)
                    {
                        aUnObjectif = true;
                        break;
                    }
                }

                if (aUnObjectif)
                {
                    continue;
                }

                string nomEspece = idEspece;

                // Récupération du nom lisible pour l'espèce hors objectif
                foreach (DataRow ligneEspece in MesDatas.DsGlobal.Tables["Espece"].Rows)
                {
                    if (ligneEspece["id"].ToString() == idEspece)
                    {
                        nomEspece = ligneEspece["nom"].ToString();
                        break;
                    }
                }

                // Ajout au bilan avec un taux de -1 (convention locale pour affichage "N/A")
                bilan.Rows.Add(nomEspece, 0, Convert.ToInt32(ligneCapture["nombre"]), -1.0);
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ChargerOngletBilanCaptures(DataRow ligneMission)
        {

            // On remplit d'abord la table locale dans le DataSet
            ConstruireBilanCaptures(ligneMission);

            // On construit le nom de la table dynamique
            string nomTable = "BilanCapture" + ligneMission["nomPlanete"].ToString() + "-" + ligneMission["numero"].ToString();

            // On vide les lignes existantes du DataGridView
            dgvBilanCapture.Rows.Clear();

            // On vide les colonnes existantes du DataGridView
            dgvBilanCapture.Columns.Clear();

            // On étire le contrôle pour remplir tout l'espace de l'onglet
            dgvBilanCapture.Dock = DockStyle.Fill;

            // On définit la couleur de fond principale du conteneur
            dgvBilanCapture.BackgroundColor = Color.FromArgb(20, 40, 80);

            // On définit la couleur des lignes séparatrices de la grille
            dgvBilanCapture.GridColor = Color.FromArgb(50, 80, 120);

            // On supprime la bordure extérieure du contrôle
            dgvBilanCapture.BorderStyle = BorderStyle.None;

            // On masque la colonne d'en-tête située à l'extrême gauche des lignes
            dgvBilanCapture.RowHeadersVisible = false;

            // On empêche l'apparition de la ligne vide interactive en bas de grille
            dgvBilanCapture.AllowUserToAddRows = false;

            // On interdit le redimensionnement manuel de la hauteur des lignes
            dgvBilanCapture.AllowUserToResizeRows = false;

            // On bascule les cellules en mode lecture seule
            dgvBilanCapture.ReadOnly = true;

            // On configure le mode de sélection pour cibler la ligne complète au clic
            dgvBilanCapture.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // On active le redimensionnement automatique proportionnel des colonnes
            dgvBilanCapture.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // On désactive les thèmes visuels du système pour appliquer nos styles personnalisés
            dgvBilanCapture.EnableHeadersVisualStyles = false;

            // On applique la couleur de fond des cellules d'en-tête de colonne
            dgvBilanCapture.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(14, 28, 54);

            // On applique la couleur du texte des en-têtes de colonne
            dgvBilanCapture.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // On configure la police de caractères des en-têtes en gras
            dgvBilanCapture.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // On fixe la hauteur de la ligne d'en-tête
            dgvBilanCapture.ColumnHeadersHeight = 35;

            // On définit la couleur de fond par défaut des cellules de données
            dgvBilanCapture.DefaultCellStyle.BackColor = Color.FromArgb(20, 40, 80);

            // On définit la couleur du texte par défaut des cellules de données
            dgvBilanCapture.DefaultCellStyle.ForeColor = Color.White;

            // On configure la police de caractères standard des cellules de données
            dgvBilanCapture.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // On définit la couleur de fond de la ligne sélectionnée
            dgvBilanCapture.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 138, 221);

            // On définit la couleur du texte de la ligne sélectionnée
            dgvBilanCapture.DefaultCellStyle.SelectionForeColor = Color.White;

            // On réinitialise les marges intérieures (Padding) des cellules
            dgvBilanCapture.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);

            // On fixe la hauteur par défaut des lignes du modèle
            dgvBilanCapture.RowTemplate.Height = 30;

            // On configure la couleur de fond des lignes paires ou alternées
            dgvBilanCapture.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 55, 100);

            // On configure la couleur du texte des lignes paires ou alternées
            dgvBilanCapture.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            // On crée la colonne Nom de l'espèce
            dgvBilanCapture.Columns.Add("espece", "Nom de l'espèce");

            // On crée la colonne Objectif initial
            dgvBilanCapture.Columns.Add("objectif", "Objectif initial");

            // On crée la colonne Captures réalisées
            dgvBilanCapture.Columns.Add("captures", "Captures réalisées");

            // On crée la colonne Taux de réussite
            dgvBilanCapture.Columns.Add("taux", "Taux de réussite (%)");

            // On attribue le poids proportionnel de largeur à la colonne Espèce
            dgvBilanCapture.Columns["espece"].FillWeight = 30;

            // On attribue le poids proportionnel de largeur à la colonne Objectif
            dgvBilanCapture.Columns["objectif"].FillWeight = 20;

            // On attribue le poids proportionnel de largeur à la colonne Captures
            dgvBilanCapture.Columns["captures"].FillWeight = 25;

            // On attribue le poids proportionnel de largeur à la colonne Taux
            dgvBilanCapture.Columns["taux"].FillWeight = 25;

            // On parcourt la liste des colonnes numériques pour les centrer
            foreach (string col in new[] { "objectif", "captures", "taux" })
            {

                // On centre l'alignement du texte de données pour la colonne actuelle
                dgvBilanCapture.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // On centre l'alignement de l'en-tête textuel pour la colonne actuelle
                dgvBilanCapture.Columns[col].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

            // On parcourt l'ensemble des lignes de données de la table locale du DataSet
            foreach (DataRow ligne in MesDatas.DsGlobal.Tables[nomTable].Rows)
            {

                // On extrait la valeur numérique du taux de réussite
                double taux = Convert.ToDouble(ligne["taux"]);

                // On déclare la variable textuelle destinée à l'affichage du taux
                string tauxAffiche;

                // Si le taux est négatif on affiche N/A
                if (taux < 0)
                {

                    // On affecte la valeur textuelle par défaut
                    tauxAffiche = "N/A";

                }
                // Sinon on construit la chaîne avec le symbole pourcentage
                else
                {

                    // On ajoute le symbole après la valeur numérique
                    tauxAffiche = taux + " %";

                }

                // On insère la nouvelle ligne formatée avec les données du bilan
                int rowIndex = dgvBilanCapture.Rows.Add(
                    ligne["nomEspece"].ToString(),
                    ligne["objectif"].ToString(),
                    ligne["captures"].ToString(),
                    tauxAffiche
                );
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private BindingSource bsJournal = new BindingSource();

        private void btnFullGauche_Click(object sender, EventArgs e)
        {

            // On positionne le BindingSource sur le tout premier élément de la collection
            bsJournal.MoveFirst();

            // On met à jour l'affichage de l'interface avec le nouvel événement courant
            AfficherEvenementCourant();
        }

        private void btnGauche_Click(object sender, EventArgs e)
        {

            // On recule d'un élément dans la collection du BindingSource
            bsJournal.MovePrevious();

            // On met à jour l'affichage de l'interface avec le nouvel événement courant
            AfficherEvenementCourant();
        }

        private void btnDroite_Click(object sender, EventArgs e)
        {

            // On avance d'un élément dans la collection du BindingSource
            bsJournal.MoveNext();

            // On met à jour l'affichage de l'interface avec le nouvel événement courant
            AfficherEvenementCourant();
        }

        private void btnFullDroite_Click(object sender, EventArgs e)
        {

            // On positionne le BindingSource sur le tout dernier élément de la collection
            bsJournal.MoveLast();

            // On met à jour l'affichage de l'interface avec le nouvel événement courant
            AfficherEvenementCourant();
        }

        private void brnRetourTableauDeBord_Click(object sender, EventArgs e)
        {
            grpFicheMission.Visible = false;
            pnlMission.Visible = true;
            pnlBoutonMission.Visible = true;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

    }
} 