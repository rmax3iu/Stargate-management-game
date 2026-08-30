using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace SAE24_Stargate
{
    public partial class ucVueNvlMission : UserControl
    {
        public ucVueNvlMission()
        {
            InitializeComponent();
        }

        // On déclare un compteur pour le numéro de la mission
        int compteur = 1;

        // On déclare une liste pour stocker les matricules des membres affectés à la mission
        List<string> listeMatricules = new List<string>();

        // On déclare un dictionnaire pour stocker les captures
        Dictionary<string, int> dicoCaptures = new Dictionary<string, int>();

        // On indique si une insertion est en cours
        public bool InsertionEnCours = false;

        // On stocke les infos de la mission partiellement créée
        private string nomPlaneteMissionEnCours = "";
        private int numeroMissionEnCours = 0;

        // On stocke à quelle insertion nous sommes
        private int etapeInsertion = 0;

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnValiderMission_Click(object sender, EventArgs e)
        {
            Boolean missionValide = true;

            // On efface toutes les erreurs
            erpChoixPlanete.Clear();
            erpChoixChef.Clear();
            erpDateDepart.Clear();
            erpDateRetour.Clear();
            erpFeuilleDeRoute.Clear();
            erpNombreMembreMission.Clear();
            erpObjectifDatabaz.Clear();
            erpBudget.Clear();

            // On vérifie que la planète est sélectionnée
            if (cboChoixPlaneteMission.SelectedIndex == -1)
            {
                erpChoixPlanete.SetIconPadding(cboChoixPlaneteMission, 35);
                erpChoixPlanete.SetError(cboChoixPlaneteMission, "Veuillez choisir une planète");
                missionValide = false;
            }

            // On vérifie que le chef de mission est sélectionné
            if (cboChoixChef.SelectedIndex == -1)
            {
                erpChoixChef.SetIconPadding(cboChoixChef, 35);
                erpChoixChef.SetError(cboChoixChef, "Veuillez choisir un chef de mission");
                missionValide = false;
            }

            // On vérifie que la date de départ n'est pas dans le passé
            if (dtpDateDepartMission.Value.Date < DateTime.Today)
            {
                erpDateDepart.SetIconPadding(dtpDateDepartMission, 35);
                erpDateDepart.SetError(dtpDateDepartMission, "La date de départ ne peut pas être dans le passé");
                missionValide = false;
            }

            // On vérifie que la date de retour est après la date de départ
            if (dtpDateRetourMission.Value.Date <= dtpDateDepartMission.Value.Date)
            {
                erpDateRetour.SetIconPadding(dtpDateRetourMission, 35);
                erpDateRetour.SetError(dtpDateRetourMission, "La date de retour doit être après la date de départ");
                missionValide = false;
            }

            // On vérifie que la feuille de route est remplie
            if (rtbFeuilleDeRoute.Text == string.Empty)
            {
                erpFeuilleDeRoute.SetIconPadding(rtbFeuilleDeRoute, 10);
                erpFeuilleDeRoute.SetError(rtbFeuilleDeRoute, "La feuille de route est obligatoire");
                missionValide = false;
            }

            // On vérifie que le nombre de membres est renseigné
            if (txtNombreMembreMission.Text == string.Empty)
            {
                erpNombreMembreMission.SetIconPadding(txtNombreMembreMission, 100);
                erpNombreMembreMission.SetError(txtNombreMembreMission, "Le nombre de membres est obligatoire");
                missionValide = false;
            }
            // On vérifie qu'une mission contient au minimum 2 membres
            else if (int.Parse(txtNombreMembreMission.Text) < 2)
            {
                erpNombreMembreMission.SetIconPadding(txtNombreMembreMission, 100);
                erpNombreMembreMission.SetError(txtNombreMembreMission, "Une mission doit contenir au minimum 2 membres");
                missionValide = false;
            }
            // On vérifie que le nombre ne dépasse pas le nombre de membres disponibles
            else if (int.Parse(txtNombreMembreMission.Text) > cboChoixChef.Items.Count + 1)
            {
                erpNombreMembreMission.SetIconPadding(txtNombreMembreMission, 100);
                erpNombreMembreMission.SetError(txtNombreMembreMission, "Le nombre de membres dépasse le nombre de membres disponibles (" + (cboChoixChef.Items.Count + 1) + ")");
                missionValide = false;
            }

            // On vérifie que l'objectif DataBaz est renseigné
            if (txtTonnes.Text == string.Empty)
            {
                erpObjectifDatabaz.SetIconPadding(txtTonnes, 100);
                erpObjectifDatabaz.SetError(txtTonnes, "L'objectif DataBaz est obligatoire");
                missionValide = false;
            }

            // On vérifie que le budget est renseigné
            if (txtBudget.Text == string.Empty)
            {
                erpBudget.SetIconPadding(txtBudget, 70);
                erpBudget.SetError(txtBudget, "Le budget est obligatoire");
                missionValide = false;
            }

            // Si tous les champs sont valides, on insère la mission dans la base de données
            if (missionValide == true)
            {
                try
                {
                    // ##################################################################################################################################################################################################################################
                    // ### - Étape n°1 : Insertion de la mission dans la base de données
                    // ##################################################################################################################################################################################################################################

                    // On récupère les valeurs des champs
                    string nomPlanete = cboChoixPlaneteMission.SelectedItem.ToString();
                    int numero = compteur;
                    int nbMembreRequis = int.Parse(txtNombreMembreMission.Text);
                    string dateDepart = dtpDateDepartMission.Value.ToString("yyyy-MM-dd");
                    string dateRetour = dtpDateRetourMission.Value.ToString("yyyy-MM-dd");
                    string feuilleDeRoute = rtbFeuilleDeRoute.Text;
                    int objectifDatabaz = int.Parse(txtTonnes.Text);
                    int budget = int.Parse(txtBudget.Text);

                    // On récupère le matricule du chef à partir du texte sélectionné dans la cbo
                    string nomChef = cboChoixChef.SelectedItem.ToString();
                    string requete1 = @"SELECT matricule FROM Membre 
                                       JOIN Militaire ON matricule = matriculeMembre 
                                       WHERE nom || ' ' || prenom || ' - ' || grade = @nomChef";

                    SQLiteCommand cmd1 = new SQLiteCommand(requete1, Connexion.Connec);
                    cmd1.Parameters.AddWithValue("@nomChef", nomChef);
                    string matriculeChef = cmd1.ExecuteScalar().ToString();

                    // On ajoute le chef dans la liste des matricules
                    listeMatricules.Add(matriculeChef);

                    // On construit et on exécute la requête d'insertion avec paramètres
                    string requete2 = @"INSERT INTO Mission (nomPlanete, numero, nbMembreRequis, dateDepart, dateRetour, matriculeChef, feuilleDeRoute, objectifDatabaz, budget)
                                        VALUES (@nomPlanete, @numero, @nbMembreRequis, @dateDepart, @dateRetour, @matriculeChef, @feuilleDeRoute, @objectifDatabaz, @budget)";

                    SQLiteCommand cmd2 = new SQLiteCommand(requete2, Connexion.Connec);
                    cmd2.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmd2.Parameters.AddWithValue("@numero", numero);
                    cmd2.Parameters.AddWithValue("@nbMembreRequis", nbMembreRequis);
                    cmd2.Parameters.AddWithValue("@dateDepart", dateDepart);
                    cmd2.Parameters.AddWithValue("@dateRetour", dateRetour);
                    cmd2.Parameters.AddWithValue("@matriculeChef", matriculeChef);
                    cmd2.Parameters.AddWithValue("@feuilleDeRoute", feuilleDeRoute);
                    cmd2.Parameters.AddWithValue("@objectifDatabaz", objectifDatabaz);
                    cmd2.Parameters.AddWithValue("@budget", budget);
                    cmd2.ExecuteNonQuery();

                    // On stocke les valeurs
                    nomPlaneteMissionEnCours = nomPlanete;
                    numeroMissionEnCours = numero;
                    InsertionEnCours = true;

                    // On met à jour le DataSet
                    UpdateDuDataset();

                    // ##################################################################################################################################################################################################################################
                    // ### - Étape n°2 : On cache la 1° GroupBox et on affiche la 2° GroupBox
                    // ##################################################################################################################################################################################################################################

                    // On cache la première GroupBox
                    grpNouvelleMission.Visible = false;

                    // On affiche la deuxième GroupBox
                    grpAffection.Visible = true;

                    // ##################################################################################################################################################################################################################################
                    // ### - Étape n°3 : On charge cboAffectationMembre
                    // ##################################################################################################################################################################################################################################

                    // On vide complètement la liste déroulante des membres à affecter pour la réinitialiser
                    cboAffectationMembre.Items.Clear();

                    // On prépare la requête d'union SQL pour extraire les identités et matricules des civils et militaires
                    string requete3 = @"SELECT Membre.matricule, nom || ' ' || prenom || ' - ' || Specialite AS nomPrenom
                                        FROM Membre
                                        JOIN Civil ON Membre.matricule = Civil.matriculeMembre
                                        UNION
                                        SELECT Membre.matricule, nom || ' ' || prenom || ' - ' || grade AS nomPrenom
                                        FROM Membre
                                        JOIN Militaire ON Membre.matricule = Militaire.matriculeMembre
                                        ORDER BY nomPrenom";

                    // On instancie la commande SQLite associée à la requête de récupération globale du personnel
                    SQLiteCommand cmd3 = new SQLiteCommand(requete3, Connexion.Connec);

                    // On exécute la commande et on récupère les données via le lecteur SQLiteDataReader
                    SQLiteDataReader rdr3 = cmd3.ExecuteReader();

                    // On instancie une liste de tableaux de chaînes pour stocker temporairement les données du personnel en mémoire
                    List<string[]> listeMembres3 = new List<string[]>();

                    // On parcourt les lignes retournées par le lecteur de base de données
                    while (rdr3.Read())
                    {
                        // On ajoute le matricule et l'identité textuelle du membre courant dans notre collection temporaire
                        listeMembres3.Add(new string[]
                        {
                            rdr3["matricule"].ToString(),
                            rdr3["nomPrenom"].ToString()
                        });
                    }

                    // On ferme le lecteur de données pour libérer la connexion SQLite avant d'exécuter d'autres requêtes
                    rdr3.Close();

                    // On examine individuellement chaque enregistrement mémorisé dans notre liste temporaire
                    foreach (string[] membre in listeMembres3)
                    {
                        // On filtre pour éviter de traiter et d'ajouter le membre désigné comme chef de mission
                        if (membre[1] != cboChoixChef.Text.Trim())
                        {
                            // On prépare la requête de vérification des chevauchements de dates sur d'autres missions actives
                            string requeteVerif = @"SELECT COUNT(*) FROM Composer c
                                            JOIN Mission m ON c.nomPlanete = m.nomPlanete 
                                            AND c.numeroMission = m.numero
                                            WHERE c.matriculeMembre = @matricule
                                            AND m.dateDepart <= @dateRetour
                                            AND m.dateRetour >= @dateDepart";

                            // On instancie la commande SQLite pour effectuer le contrôle de disponibilité du membre
                            SQLiteCommand cmdVerif = new SQLiteCommand(requeteVerif, Connexion.Connec);

                            // On injecte le matricule unique du membre à tester dans les paramètres de validation
                            cmdVerif.Parameters.AddWithValue("@matricule", membre[0]);

                            // On injecte la date de départ prévue au format texte standardisé pour la comparaison SQL
                            cmdVerif.Parameters.AddWithValue("@dateDepart", dtpDateDepartMission.Value.ToString("yyyy-MM-dd"));

                            // On injecte la date de retour prévue au format texte standardisé pour la comparaison SQL
                            cmdVerif.Parameters.AddWithValue("@dateRetour", dtpDateRetourMission.Value.ToString("yyyy-MM-dd"));

                            // On évalue si le résultat du décompte de missions simultanées est supérieur à zéro
                            bool dejaAffecte = Convert.ToInt32(cmdVerif.ExecuteScalar()) > 0;

                            // On injecte l'identité textuelle du membre dans la liste uniquement s'il est libre aux dates indiquées
                            if (!dejaAffecte)
                            {
                                // On insère le nom, prénom et distinction du membre éligible dans la liste déroulante
                                cboAffectationMembre.Items.Add(membre[1]);
                            }
                        }
                    }

                    // On force la sélection sur le tout premier choix de la liste déroulante d'affectation
                    cboAffectationMembre.SelectedIndex = 0;

                    // On ajoute l'identité complète du chef de mission suivie d'un saut de ligne dans la zone de texte riche
                    rtbAffectationMembre.AppendText(cboChoixChef.Text + "\n");

                    // On calcule et on affiche le nombre restant de membres requis à affecter en soustrayant le chef
                    txtNombreAffectation.Text = (int.Parse(txtNombreMembreMission.Text) - 1).ToString();

                }
                catch (Exception monErreur)
                {
                    MessageBox.Show("Erreur : " + monErreur.Message);
                }
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnEffacerMission_Click(object sender, EventArgs e)
        {
            // On efface toutes les erreurs
            erpChoixPlanete.Clear();
            erpChoixChef.Clear();
            erpDateDepart.Clear();
            erpDateRetour.Clear();
            erpFeuilleDeRoute.Clear();
            erpNombreMembreMission.Clear();
            erpObjectifDatabaz.Clear();
            erpBudget.Clear();

            // On remet tous les champs à leur valeur par défaut
            cboChoixPlaneteMission.Enabled = true;
            dtpDateDepartMission.Enabled = true;
            dtpDateRetourMission.Enabled = true;
            btnValiderDate.Enabled = true;
            btnValiderPlaneteMission.Enabled = true;
            cboChoixPlaneteMission.SelectedIndex = -1;
            cboChoixChef.Items.Clear();

            dtpDateDepartMission.Value = DateTime.Today;
            dtpDateRetourMission.Value = DateTime.Today;

            rtbFeuilleDeRoute.Text = "";
            txtNombreMembreMission.Text = "";
            txtTonnes.Text = "";
            txtBudget.Text = "";

            // On remet les labels à vide
            lblNomPlaneteDeLaMission.Text = "";
            lblNumeroMission.Text = "";

            // On remet le compteur à 1
            compteur = 1;

            // On cache tous les composants de la suite
            lblParametreMission.Visible = false;
            cboChoixChef.Visible = false;
            lblDetailMission.Visible = false;
            lblDateDepartMission.Visible = false;
            dtpDateDepartMission.Visible = false;
            lblDateRetourMission.Visible = false;
            dtpDateRetourMission.Visible = false;
            lblFeuilleDeRouteMission.Visible = false;
            rtbFeuilleDeRoute.Visible = false;
            lblChoixChef.Visible = false;
            lblDate.Visible = false;
            btnValiderDate.Visible = false;
            lblNombreMembreMission.Visible = false;
            txtNombreMembreMission.Visible = false;
            lblPersonnes.Visible = false;
            lblObjectifDatabaz.Visible = false;
            txtTonnes.Visible = false;
            lblTonnes.Visible = false;
            lblBudgetMission.Visible = false;
            txtBudget.Visible = false;
            lblEuros.Visible = false;
            btnEffacerMission.Visible = false;
            btnValiderMission.Visible = false;

            listeMatricules.Clear();
            dicoCaptures.Clear();
            compteur = 1;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        // On empêche toute saisie invalide dans le champ nombre de membres
        private void txtNombreMembreMission_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            // On désactive les raccourcis clavier
            txtNombreMembreMission.ShortcutsEnabled = false;

            // On vérifie si la touche est un chiffre ou s'il s'agit de la touche Backspace
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                // On vérifie si l'utilisateur tape un 0 alors que le champ est encore complètement vide
                if (e.KeyChar == '0' && txtNombreMembreMission.Text.Length == 0)
                {
                    // On maintient le blocage pour empêcher le nombre de membres de commencer par un zéro
                    e.Handled = true;
                }
                // On vérifie si le texte atteint déjà 3 caractères et que la touche pressée n'est pas la touche Backspace
                else if (txtNombreMembreMission.Text.Length >= 3 && e.KeyChar != (char)Keys.Back)
                {
                    // On bloque la saisie pour limiter à une longueur maximale de 3 chiffres
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
        // ##############################################################################################################################################################################################################################################

        // On empêche toute saisie invalide dans le champ objectif de Databaz
        private void txtTonnes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            // On désactive les raccourcis clavier
            txtTonnes.ShortcutsEnabled = false;

            // On vérifie si la touche est un chiffre ou s'il s'agit de la touche Backspace
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                // Si l'utilisateur veut taper un deuxième caractère alors que le premier est déjà un 0 et qu'il n'est pas en train d'effacer avec Backspace)
                if (txtTonnes.Text == "0" && e.KeyChar != (char)Keys.Back)
                {
                    // On bloque
                    e.Handled = true;
                }
                // Sinon on vérifie la limite classique des 4 caractères maximum
                else if (txtTonnes.Text.Length >= 4 && e.KeyChar != (char)Keys.Back)
                {
                    // On bloque la saisie si la longueur max est atteinte
                    e.Handled = true;
                }
                // Si aucune règle de blocage n'est activée, la touche est valide
                else
                {
                    // On autorise la saisie
                    e.Handled = false;
                }
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        // On empêche toute saisie invalide dans le champ budget

        private void txtBudget_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            // On désactive les raccourcis clavier
            txtBudget.ShortcutsEnabled = false;

            // On vérifie si la touche est un chiffre ou s'il s'agit de la touche Backspace
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                // On vérifie si l'utilisateur tape un 0 alors que le champ est encore complètement vide
                if (e.KeyChar == '0' && txtBudget.Text.Length == 0)
                {
                    // On maintient le blocage pour empêcher le budget de commencer par un zéro
                    e.Handled = true;
                }
                // On vérifie si le texte atteint déjà 7 caractères et que la touche pressée n'est pas la touche Backspace
                else if (txtBudget.Text.Length >= 7 && e.KeyChar != (char)Keys.Back)
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
        // ##############################################################################################################################################################################################################################################

        private void ucVueNvlMission_Load(object sender, EventArgs e)
        {
            CentrerGroupBox();

            // On initialise les tooltips des boutons
            tltValiderMission.SetToolTip(btnValiderMission, "Valider la mission");
            tltEffacerMission.SetToolTip(btnEffacerMission, "Effacer tous les champs");
            tltValiderChoixPlaneteMission.SetToolTip(btnValiderPlaneteMission, "Valider la planète");
            tltValiderDate.SetToolTip(btnValiderDate, "Valider les dates de la mission");
            tltAjouterMembre.SetToolTip(btnAjouterMembre, "Ajouter le membre à la mission");
            tltValiderMembre.SetToolTip(btnValiderMembre, "Valider l'affectation des membres");
            tltEffacerMembre.SetToolTip(btnEffacerMembre, "Effacer l'affectation des membres");
            tltObjectifCapture.SetToolTip(btnObjectifCapture, "Ajouter les objectifs de capture");
            tltEffacerObjectif.SetToolTip(btnEffacerObjectif, "Effacer les objectifs de capture");
            tltValiderObjectif.SetToolTip(btnValiderObjectif, "Valider les objectifs de capture");

            try
            {
                // On charge les planètes directement depuis la base de données
                string requete4 = @"SELECT nom 
                                   FROM Planete 
                                   ORDER BY nom";

                SQLiteCommand cmd4 = new SQLiteCommand(requete4, Connexion.Connec);
                SQLiteDataReader rdr4 = cmd4.ExecuteReader();

                cboChoixPlaneteMission.Items.Clear();
                while (rdr4.Read())
                {
                    cboChoixPlaneteMission.Items.Add(rdr4["nom"].ToString());
                }
                rdr4.Close();
            }
            catch (Exception monErreur)
            {
                MessageBox.Show("Erreur : " + monErreur.Message);
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void ucVueNvlMission_Resize(object sender, EventArgs e)
        {
            CentrerGroupBox();
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        // Méthode pour centrer les GroupBox
        private void CentrerGroupBox()
        {
            // On centre juste, sans redimensionner
            grpNouvelleMission.Left = (this.Width - grpNouvelleMission.Width) / 2;
            grpNouvelleMission.Top = (this.Height - grpNouvelleMission.Height) / 2;

            grpAffection.Left = (this.Width - grpAffection.Width) / 2;
            grpAffection.Top = (this.Height - grpAffection.Height) / 2;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnValiderPlaneteMission_Click(object sender, EventArgs e)
        {
            erpChoixPlanete.Clear();

            if (cboChoixPlaneteMission.SelectedIndex == -1)
            {
                erpChoixPlanete.SetIconPadding(cboChoixPlaneteMission, 35);
                erpChoixPlanete.SetError(cboChoixPlaneteMission, "Veuillez choisir une planète");
                return;
            }

            UpdateDuDataset();

            try
            {
                // On affiche le nom de la planète sélectionnée
                lblNomPlaneteDeLaMission.Text = cboChoixPlaneteMission.SelectedItem.ToString();

                // On compte le nombre de missions déjà effectuées sur cette planète
                string requete6 = @"SELECT COUNT(*) 
                            FROM Mission 
                            WHERE nomPlanete = @nomPlanete";

                SQLiteCommand cmd6 = new SQLiteCommand(requete6, Connexion.Connec);
                cmd6.Parameters.AddWithValue("@nomPlanete", cboChoixPlaneteMission.SelectedItem.ToString());
                compteur = Convert.ToInt32(cmd6.ExecuteScalar()) + 1;
                lblNumeroMission.Text = " - " + compteur.ToString();
            }
            catch (Exception monErreur)
            {
                MessageBox.Show("Erreur : " + monErreur.Message);
            }

            // On affiche uniquement la section "2 - Choix des dates"
            lblDateDepartMission.Visible = true;
            lblDate.Visible = true;
            dtpDateDepartMission.Visible = true;
            lblDateRetourMission.Visible = true;
            dtpDateRetourMission.Visible = true;
            btnValiderDate.Visible = true;

            // On fige la combobox planète
            cboChoixPlaneteMission.Enabled = false;
            btnValiderPlaneteMission.Enabled = false;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnAjouterMembre_Click(object sender, EventArgs e)
        {
            // On efface l'erreur précédente
            erpAffectationMembre.Clear();

            // On ajoute le membre dans la rtb
            rtbAffectationMembre.AppendText(cboAffectationMembre.SelectedItem.ToString() + "\n");

            // On effectue une requête pour récupérer le matricule du membre ajouter
            string nomMembre = cboAffectationMembre.SelectedItem.ToString();
            string requete10 = @"SELECT matricule FROM Membre
                                 JOIN Civil ON matricule = matriculeMembre
                                 WHERE nom || ' ' || prenom || ' - ' || Specialite = @nomMembre
                                 UNION
                                 SELECT matricule FROM Membre
                                 JOIN Militaire ON matricule = matriculeMembre
                                 WHERE nom || ' ' || prenom || ' - ' || grade = @nomMembre";

            SQLiteCommand cmd10 = new SQLiteCommand(requete10, Connexion.Connec);
            cmd10.Parameters.AddWithValue("@nomMembre", nomMembre);
            string leMatricule = cmd10.ExecuteScalar().ToString();

            listeMatricules.Add(leMatricule);

            // On supprime le membre de la cbo
            cboAffectationMembre.Items.RemoveAt(cboAffectationMembre.SelectedIndex);

            // On resélectionne automatiquement le premier élément
            if (cboAffectationMembre.Items.Count > 0)
            {
                cboAffectationMembre.SelectedIndex = 0;
            }

            // On décrémente le nombre restant à affecter
            txtNombreAffectation.Text = (int.Parse(txtNombreAffectation.Text) - 1).ToString();

            // Si tous les membres sont affectés on bloque le bouton et la cbo
            if (int.Parse(txtNombreAffectation.Text) == 0)
            {
                btnAjouterMembre.Enabled = false;
                cboAffectationMembre.Enabled = false;
                rtbAffectationMembre.Enabled = false;
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnValiderMembre_Click(object sender, EventArgs e)
        {
            // On efface les erreurs précédentes
            erpAffectationMembre.Clear();

            // On vérifie que tous les membres sont affectés
            if (int.Parse(txtNombreAffectation.Text) > 0)
            {
                erpAffectationMembre.SetIconPadding(txtNombreAffectation, 10);
                erpAffectationMembre.SetError(txtNombreAffectation, "Vous devez affecter tous les membres avant de valider");
                return;
            }

            // Si tous les membres sont affectés on affiche la suite
            lblObjectifCapture.Visible = true;
            cboAlienCouleur.Visible = true;
            txtObjectifCapture.Visible = true;
            btnObjectifCapture.Visible = true;
            rtbObjectifCapture.Visible = true;
            btnValiderObjectif.Visible = true;
            btnEffacerObjectif.Visible = true;

            // On effectue l'insertion des membres dans la table Composer
            
            try
            {
                // On effectue une requête pour ajouter les aliens à la ComboBox
                string requete8 = @"SELECT esp.id, esp.nom || ' - ' || esp.couleur AS nomAlien
                                    FROM Espece esp";

                SQLiteCommand cmd8 = new SQLiteCommand(requete8, Connexion.Connec);
                SQLiteDataReader rdr8 = cmd8.ExecuteReader();

                cboAlienCouleur.Items.Clear();
                while(rdr8.Read())
                {
                    cboAlienCouleur.Items.Add(rdr8["nomAlien"].ToString());
                }
                rdr8.Close();
                cboAlienCouleur.SelectedIndex = 0;
            }
            catch (Exception monErreur)
            {
                MessageBox.Show("Erreur : " + monErreur.Message);
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnEffacerMembre_Click(object sender, EventArgs e)
        {
            // On efface les bulles d'erreurs à l'écran
            erpAffectationMembre.Clear();

            // On vérifie s'il reste des membres à part le chef (le chef est le premier élément [0])
            if (listeMatricules.Count <= 1)
            {
                MessageBox.Show("Aucun membre à supprimer (le chef ne peut pas être retiré ici).");
                return;
            }

            // On récupère toutes les lignes de la zone de texte dans une liste
            var lignes = new List<string>(rtbAffectationMembre.Lines);

            // On nettoie les lignes vides à la fin
            while (lignes.Count > 0 && string.IsNullOrWhiteSpace(lignes[lignes.Count - 1]))
            {
                lignes.RemoveAt(lignes.Count - 1);
            }

            // S'il n'y a plus de lignes valides, on s'arrête
            if (lignes.Count == 0)
            {
                MessageBox.Show("Plus aucun membre à effacer dans l'affichage.");
                return;
            }

            // On récupère le nom du tout dernier membre
            string dernierMembre = lignes[lignes.Count - 1].Trim();

            // On vérifie par sécurité que ce n'est pas le chef qu'on va effacer
            if (dernierMembre == cboChoixChef.Text.Trim())
            {
                MessageBox.Show("Impossible de supprimer le chef de mission.");
                return;
            }

            // On supprime ce membre de notre liste de lignes
            lignes.RemoveAt(lignes.Count - 1);

            // CORRECTION ICI : On reconstruit le texte avec des sauts de ligne propres pour que le prochain ajout aille à la ligne
            if (lignes.Count > 0)
            {
                rtbAffectationMembre.Text = string.Join("\n", lignes) + "\n";
            }
            else
            {
                rtbAffectationMembre.Clear();
            }

            // On retire le dernier matricule de notre liste de suivi de l'équipe
            listeMatricules.RemoveAt(listeMatricules.Count - 1);

            // On remet le nom du membre dans la liste déroulante
            cboAffectationMembre.Items.Add(dernierMembre);

            // On active le tri automatique pour garder l'ordre alphabétique
            cboAffectationMembre.Sorted = true;

            // On sélectionne automatiquement le premier élément disponible
            if (cboAffectationMembre.Items.Count > 0)
            {
                cboAffectationMembre.SelectedIndex = 0;
            }

            // On prend la valeur actuelle du compteur et on fait STRICTEMENT + 1
            int nbRestant = int.Parse(txtNombreAffectation.Text) + 1;
            txtNombreAffectation.Text = nbRestant.ToString();

            // On réactive les boutons et la liste déroulante au cas où ils étaient bloqués
            btnAjouterMembre.Enabled = true;
            cboAffectationMembre.Enabled = true;
            rtbAffectationMembre.Enabled = true;

            // On cache toute la partie des objectifs si elle était affichée
            lblObjectifCapture.Visible = false;
            cboAlienCouleur.Visible = false;
            txtObjectifCapture.Visible = false;
            btnObjectifCapture.Visible = false;
            rtbObjectifCapture.Visible = false;
            btnValiderObjectif.Visible = false;
            btnEffacerObjectif.Visible = false;
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void txtObjectifCapture_KeyPress(object sender, KeyPressEventArgs e)
        {
            // On bloque par défaut la prise en compte du caractère saisi
            e.Handled = true;

            // On désactive les raccourcis clavier comme le copier-coller dans la zone de texte
            txtObjectifCapture.ShortcutsEnabled = false;

            // On vérifie si le caractère saisi est un chiffre ou une touche d'effacement arrière
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                // On refuse la saisie si l'utilisateur tente de taper un zéro en tout début de texte
                if (e.KeyChar == '0' && txtObjectifCapture.SelectionStart == 0)
                {
                    // On maintient le blocage de la saisie
                    e.Handled = true;
                }
                // On limite la saisie à un maximum de 4 chiffres tout en autorisant l'effacement
                else if (txtObjectifCapture.Text.Length >= 4 && e.KeyChar != (char)Keys.Back)
                {
                    // On bloque la saisie car la limite de caractères est atteinte
                    e.Handled = true;
                }
                // Dans tous les autres cas valides la saisie est acceptée
                else
                {
                    // On autorise l'écriture du caractère dans le champ de texte
                    e.Handled = false;
                }
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnObjectifCapture_Click(object sender, EventArgs e)
        {
            // On efface les alertes visuelles du ErrorProvider de la liste des aliens
            erpAlienCouleur.Clear();

            // On efface les alertes visuelles du ErrorProvider du champ de saisie numérique
            erpObjectifCapture.Clear();

            erpRtbObjectifCapture.Clear();
            txtErreurMessage.Text = "";

            // Cas où aucun alien n'est sélectionné mais qu'une quantité a été saisie
            if (cboAlienCouleur.SelectedIndex == -1 && txtObjectifCapture.Text != "")
            {
                // On définit une marge de 10 pixels pour l'icône d'erreur de la liste déroulante
                erpAlienCouleur.SetIconPadding(cboAlienCouleur, 10);

                // On affiche le message d'erreur sur la liste de choix de l'alien
                erpAlienCouleur.SetError(cboAlienCouleur, "Vous devez séléctionner un alien pour l'ajouter à la RichTextBox");
            }
            // Cas où aucun alien n'est sélectionné et qu'aucune quantité n'a été entrée
            else if (cboAlienCouleur.SelectedIndex == -1 && txtObjectifCapture.Text == "")
            {
                // On définit une marge de 10 pixels pour l'icône d'erreur de la liste déroulante
                erpAlienCouleur.SetIconPadding(cboAlienCouleur, 10);

                // On affiche le message d'erreur sur la liste de choix de l'alien
                erpAlienCouleur.SetError(cboAlienCouleur, "Vous devez séléctionner un alien pour l'ajouter à la RichTextBox");

                // On définit une marge de 10 pixels pour l'icône d'erreur du champ de texte
                erpObjectifCapture.SetIconPadding(txtObjectifCapture, 10);

                // On affiche le message d'erreur sur la zone de saisie numérique
                erpObjectifCapture.SetError(txtObjectifCapture, "Vous devez saisir un nombre d'alien pour l'ajouter à la RichTextBox");
            }
            // Cas où un alien est bien sélectionné mais que la quantité est absente
            else if (cboAlienCouleur.SelectedIndex != -1 && txtObjectifCapture.Text == "")
            {
                // On définit une marge de 10 pixels pour l'icône d'erreur du champ de texte
                erpObjectifCapture.SetIconPadding(txtObjectifCapture, 10);

                // On affiche le message d'erreur sur la zone de saisie numérique
                erpObjectifCapture.SetError(txtObjectifCapture, "Vous devez saisir un nombre d'alien pour l'ajouter à la RichTextBox");
            }
            // Cas où toutes les saisies sont correctes et prêtes à être traitées
            else
            {
                // On vide le message d'erreur
                txtErreurMessage.Text = ""; 

                // On récupère le nom textuel de l'alien sélectionné
                string alien = cboAlienCouleur.Text;

                // On convertit le texte de la quantité saisie en entier numérique
                int nbCapture = int.Parse(txtObjectifCapture.Text);

                // Si l'alien est déjà présent dans le dictionnaire de stockage
                if (dicoCaptures.ContainsKey(alien))
                {
                    // On cumule la nouvelle quantité à celle déjà enregistrée
                    dicoCaptures[alien] += nbCapture;
                }
                // Si l'alien est sélectionné pour la première fois
                else
                {
                    // On crée une nouvelle entrée dans le dictionnaire avec sa quantité
                    dicoCaptures[alien] = nbCapture;
                }

                // On vide complètement la zone de texte riche avant de recalculer l'affichage
                rtbObjectifCapture.Clear();

                // On parcourt l'intégralité des couples clé/valeur stockés dans le dictionnaire
                foreach (KeyValuePair<string, int> kvp in dicoCaptures)
                {
                    // On ajoute une ligne formatée pour chaque objectif de capture d'alien
                    rtbObjectifCapture.AppendText(kvp.Key + " ---> Objectif de capture : " + kvp.Value + "\n");
                }

                // On remet la sélection de la liste déroulante sur le premier élément par défaut
                cboAlienCouleur.SelectedIndex = 0;

                // On réinitialise à blanc le champ textuel de saisie de la quantité
                txtObjectifCapture.Text = "";
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnEffacerObjectif_Click(object sender, EventArgs e)
        {
            // On vide le message d'erreur après suppression réussie
            txtErreurMessage.Text = "";

            erpRtbObjectifCapture.Clear();
            erpObjectifCapture.Clear();

            // On vérifie que la RTB contient au moins une ligne
            if (rtbObjectifCapture.Lines.Length > 0)
            {
                // On récupère la dernière ligne non vide
                string derniereCapture = "";
                for (int i = rtbObjectifCapture.Lines.Length - 1; i >= 0; i--)
                {
                    if (rtbObjectifCapture.Lines[i].Trim() != "")
                    {
                        derniereCapture = rtbObjectifCapture.Lines[i].Trim();
                        break;
                    }
                }

                if (derniereCapture != "")
                {
                    // On extrait le nom de l'alien depuis la ligne (format : "nomAlien ---> Objectif de capture : X")
                    string nomAlien = derniereCapture.Split(new string[] { " ---> " }, StringSplitOptions.None)[0].Trim();

                    // On retire l'alien du dictionnaire
                    if (dicoCaptures.ContainsKey(nomAlien))
                    {
                        dicoCaptures.Remove(nomAlien);
                    }

                    // On supprime la dernière ligne non vide de la RTB
                    string contenuActuel = rtbObjectifCapture.Text;
                    int dernierSaut = contenuActuel.TrimEnd().LastIndexOf('\n');
                    if (dernierSaut >= 0)
                    {
                        rtbObjectifCapture.Text = contenuActuel.Substring(0, dernierSaut + 1);
                    }
                    else
                    {
                        rtbObjectifCapture.Clear();
                    }
                }
            }
            else
            {
                txtErreurMessage.Text = "✖ Aucun objectif à supprimer";
                txtErreurMessage.ForeColor = Color.Red;
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private async void btnValiderObjectif_Click(object sender, EventArgs e)
        {
            // On efface les erreurs affichées précédemment
            erpRtbObjectifCapture.Clear();

            erpObjectifCapture.Clear();
            txtErreurMessage.Text = "";

            // Si le champ objectif est vide
            if (rtbObjectifCapture.Text == "")
            {
                // On décale l'icône d'erreur de 10px
                erpRtbObjectifCapture.SetIconPadding(rtbObjectifCapture, 10);

                // On affiche le message d'erreur
                erpRtbObjectifCapture.SetError(rtbObjectifCapture, "Les objectifs de capture ne peuvent pas être vide");
            }
            // Si des objectifs sont saisis, on procède à l'enregistrement complet de la mission
            else
            {
                // On récupère la planète sélectionnée
                string nomPlanete = cboChoixPlaneteMission.SelectedItem.ToString();

                // On récupère le numéro de mission en cours
                int numeroMission = compteur;

                // On cache le groupe d'affectation
                grpAffection.Visible = false;

                // On force le redessin de l'interface pour que le hide soit visible immédiatement
                this.Refresh();

                // On crée le formulaire de chargement
                frmChargement fenetreChargement = new frmChargement();

                // On affiche sans bloquer
                fenetreChargement.Show();

                // On attend que la barre soit à 100% avant de continuer
                await fenetreChargement.DemarrerChargement();

                // On démarre une transaction SQL (tout ou rien)
                SQLiteTransaction maTransaction = Connexion.Connec.BeginTransaction();

                // Bloc sécurisé pour effectuer l'ensemble des écritures liées à la mission
                try
                {
                    // Pour chaque alien à capturer
                    foreach (KeyValuePair<string, int> kvp in dicoCaptures)
                    {
                        // On extrait le nom de l'alien depuis la clé
                        string nomAlien = kvp.Key.Split(new string[] { " - " }, StringSplitOptions.None)[0].Trim();

                        // On effectue une requête pour récupérer l'id de l'espèce
                        string requete12 = @"SELECT id FROM Espece WHERE nom = @nomAlien";

                        // On prépare la commande SQL
                        SQLiteCommand cmd12 = new SQLiteCommand(requete12, Connexion.Connec);

                        // On injecte le nom de l'alien de façon sécurisée
                        cmd12.Parameters.AddWithValue("@nomAlien", nomAlien);

                        // On rattache la commande à la transaction en cours
                        cmd12.Transaction = maTransaction;

                        // On exécute et récupère l'id retourné
                        int idEspece = Convert.ToInt32(cmd12.ExecuteScalar());

                        // Requête d'insertion de l'objectif
                        string requete11 = @"INSERT INTO ObjectifCapture VALUES (@nomPlanete, @numeroMission, @idEspeceEnnemi, @objectif)";

                        // Prépare la commande SQL
                        SQLiteCommand cmd11 = new SQLiteCommand(requete11, Connexion.Connec);

                        // Rattache à la transaction
                        cmd11.Transaction = maTransaction;

                        // Injecte la planète
                        cmd11.Parameters.AddWithValue("@nomPlanete", nomPlanete);

                        // Injecte le numéro de mission
                        cmd11.Parameters.AddWithValue("@numeroMission", numeroMission);

                        // Injecte l'id de l'espèce
                        cmd11.Parameters.AddWithValue("@idEspeceEnnemi", idEspece);

                        // Injecte le nombre à capturer
                        cmd11.Parameters.AddWithValue("@objectif", kvp.Value);

                        // Exécute l'insertion (ne retourne pas de valeur)
                        cmd11.ExecuteNonQuery();
                    }

                    // Pour chaque membre de l'équipe
                    foreach (string matricule in listeMatricules)
                    {
                        // Requête pour lier le membre à la mission
                        string requete7 = @"INSERT INTO Composer (nomPlanete, numeroMission, matriculeMembre) VALUES (@nomPlanete, @numeroMission, @matriculeMembre)";

                        // Prépare la commande SQL
                        SQLiteCommand cmd7 = new SQLiteCommand(requete7, Connexion.Connec);

                        // Rattache à la transaction
                        cmd7.Transaction = maTransaction;

                        // Injecte la planète
                        cmd7.Parameters.AddWithValue("@nomPlanete", nomPlanete);

                        // Injecte le numéro de mission
                        cmd7.Parameters.AddWithValue("@numeroMission", numeroMission);

                        // Injecte le matricule du membre
                        cmd7.Parameters.AddWithValue("@matriculeMembre", matricule);

                        // Exécute l'insertion
                        cmd7.ExecuteNonQuery();
                    }

                    // Valide toutes les insertions en base (si on arrive ici, tout a réussi)
                    maTransaction.Commit();

                    // On réinitialise l'indicateur d'état d'insertion globale
                    InsertionEnCours = false;

                    // On vide le nom de la planète actuellement traitée
                    nomPlaneteMissionEnCours = "";

                    // On remet à zéro le numéro d'identifiant de la mission active
                    numeroMissionEnCours = 0;

                    // Met à jour le dataset local pour refléter les nouvelles données
                    UpdateDuDataset();

                    // Si le panel final n'est pas encore dans le formulaire
                    if (!this.Controls.Contains(pnlFinMission))
                    {
                        // On l'ajoute
                        this.Controls.Add(pnlFinMission);
                    }

                    // Centre horizontalement
                    pnlFinMission.Left = (this.Width - pnlFinMission.Width) / 2;

                    // Centre verticalement
                    pnlFinMission.Top = (this.Height - pnlFinMission.Height) / 2;

                    // Affiche le panel de fin
                    pnlFinMission.Visible = true;

                    // Affiche l'image de fin
                    pcbFinMission.Visible = true;

                    // Affiche le label 1
                    lblFinMission1.Visible = true;

                    // Affiche le label 2
                    lblFinMission2.Visible = true;

                    // Affiche le label 3
                    lblFinMission3.Visible = true;

                    // Met le panel au premier plan pour qu'il soit visible par dessus tout
                    pnlFinMission.BringToFront();
                }
                // Si une erreur SQL survient
                catch (Exception monErreur)
                {
                    // Vérifie que la transaction est encore active
                    if (maTransaction != null && maTransaction.Connection != null)
                    {
                        // Annule toutes les insertions faites depuis BeginTransaction()
                        maTransaction.Rollback();
                    }

                    // Affiche l'erreur à l'utilisateur
                    MessageBox.Show("Erreur lors de la création de la mission : " + monErreur.Message);
                }
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void UpdateDuDataset()
        {

            // On déclare un tableau contenant la liste ordonnée de toutes les tables de la base de données
            string[] tables = new string[]
            {"Admin", "Allie", "Capturer", "Civil", "Composer", "Contact", "Depense", "Ennemi", "Espece", "Habiter", "Informateur", "JournalDeBord", "Membre", "Militaire", "Mission", "Negocier", "ObjectifCapture", "Planete", "TypeDepense"};

            // On parcourt individuellement chaque nom de table défini dans le tableau
            foreach (string nomTable in tables)
            {

                // On vérifie si la table actuelle est déjà présente dans la structure du DataSet global
                if (MesDatas.DsGlobal.Tables.Contains(nomTable))
                {
                    // On retire les colonnes calculées AVANT le Clear pour éviter le conflit
                    if (nomTable == "Membre" && MesDatas.DsGlobal.Tables["Membre"].Columns.Contains("NomComplet"))
                    {
                        MesDatas.DsGlobal.Tables["Membre"].Columns.Remove("NomComplet");
                    }
                    // On vide l'intégralité des lignes de la table locale pour éviter les doublons avant la recharge
                    MesDatas.DsGlobal.Tables[nomTable].Clear();
                }

                // On instancie le connecteur de données avec une requête de sélection totale dynamique
                SQLiteDataAdapter da = new SQLiteDataAdapter($"SELECT * FROM {nomTable}", Connexion.Connec);

                // On injecte les nouveaux enregistrements de la base SQLite dans la table correspondante du DataSet
                da.Fill(MesDatas.DsGlobal, nomTable);
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        public void AbandonnerMission()
        {
            // Bloc sécurisé pour gérer la suppression en base de données et la réinitialisation
            try
            {
                // On supprime la mission incomplète de la base de données
                string req = @"DELETE FROM Mission WHERE nomPlanete = @nomPlanete AND numero = @numero";

                // On instancie la commande SQLite pour exécuter la requête de suppression
                SQLiteCommand cmd = new SQLiteCommand(req, Connexion.Connec);

                // On injecte le nom de la planète associée à la mission en cours d'annulation
                cmd.Parameters.AddWithValue("@nomPlanete", nomPlaneteMissionEnCours);

                // On injecte le numéro de la mission actuellement ciblée par l'abandon
                cmd.Parameters.AddWithValue("@numero", numeroMissionEnCours);

                // On exécute la commande SQL de suppression sans retourner de ligne
                cmd.ExecuteNonQuery();

                // On met à jour le DataSet pour refléter la suppression
                UpdateDuDataset();

                // On remet les variables à zéro
                InsertionEnCours = false;

                // On réinitialise à blanc la variable du nom de la planète en cours
                nomPlaneteMissionEnCours = "";

                // On remet à zéro l'identifiant numérique de la mission en cours
                numeroMissionEnCours = 0;

            }
            // Interception d'une éventuelle erreur lors du processus d'abandon de la mission
            catch (Exception ex)
            {

                // On affiche un message d'alerte avec le descriptif technique de l'exception
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

        private void btnValiderDate_Click(object sender, EventArgs e)
        {
            // On efface les erreurs précédentes
            erpDateDepart.Clear();
            erpDateRetour.Clear();

            bool datesValides = true;

            // On vérifie que la date de départ n'est pas dans le passé
            if (dtpDateDepartMission.Value.Date < DateTime.Today)
            {
                erpDateDepart.SetIconPadding(dtpDateDepartMission, 35);
                erpDateDepart.SetError(dtpDateDepartMission, "La date de départ ne peut pas être dans le passé");
                datesValides = false;
            }

            // On vérifie que la date de retour est après la date de départ
            if (dtpDateRetourMission.Value.Date <= dtpDateDepartMission.Value.Date)
            {
                erpDateRetour.SetIconPadding(dtpDateRetourMission, 35);
                erpDateRetour.SetError(dtpDateRetourMission, "La date de retour doit être après la date de départ");
                datesValides = false;
            }

            if (!datesValides) return;

            try
            {
                // On fige les DateTimePicker et le bouton valider date
                dtpDateDepartMission.Enabled = false;
                dtpDateRetourMission.Enabled = false;
                btnValiderDate.Enabled = false;

                // On charge les militaires disponibles sur la période saisie
                string requete5 = @"SELECT matricule, nom || ' ' || prenom || ' - ' || grade AS nomPrenom
                            FROM Membre
                            JOIN Militaire ON matricule = matriculeMembre
                            ORDER BY nomPrenom";

                SQLiteCommand cmd5 = new SQLiteCommand(requete5, Connexion.Connec);
                SQLiteDataReader rdr5 = cmd5.ExecuteReader();

                cboChoixChef.Items.Clear();

                List<string[]> listeMilitaires = new List<string[]>();
                while (rdr5.Read())
                {
                    listeMilitaires.Add(new string[]
                    {
                rdr5["matricule"].ToString(),
                rdr5["nomPrenom"].ToString()
                    });
                }
                rdr5.Close();

                foreach (string[] militaire in listeMilitaires)
                {
                    // On vérifie si le militaire a une mission qui chevauche la période saisie
                    string requeteVerif = @"SELECT COUNT(*) FROM Composer c
                                    JOIN Mission m ON c.nomPlanete = m.nomPlanete 
                                    AND c.numeroMission = m.numero
                                    WHERE c.matriculeMembre = @matricule
                                    AND m.dateDepart <= @dateRetour
                                    AND m.dateRetour >= @dateDepart";

                    SQLiteCommand cmdVerif = new SQLiteCommand(requeteVerif, Connexion.Connec);
                    cmdVerif.Parameters.AddWithValue("@matricule", militaire[0]);
                    cmdVerif.Parameters.AddWithValue("@dateDepart", dtpDateDepartMission.Value.ToString("yyyy-MM-dd"));
                    cmdVerif.Parameters.AddWithValue("@dateRetour", dtpDateRetourMission.Value.ToString("yyyy-MM-dd"));

                    bool dejaAffecte = Convert.ToInt32(cmdVerif.ExecuteScalar()) > 0;

                    if (!dejaAffecte)
                    {
                        cboChoixChef.Items.Add(militaire[1]);
                    }
                }

                // On affiche toute la section "3 - Paramètres de la mission"
                lblParametreMission.Visible = true;
                lblChoixChef.Visible = true;
                cboChoixChef.Visible = true;
                lblDetailMission.Visible = true;
                lblFeuilleDeRouteMission.Visible = true;
                rtbFeuilleDeRoute.Visible = true;
                lblNombreMembreMission.Visible = true;
                txtNombreMembreMission.Visible = true;
                lblPersonnes.Visible = true;
                lblObjectifDatabaz.Visible = true;
                txtTonnes.Visible = true;
                lblTonnes.Visible = true;
                lblBudgetMission.Visible = true;
                txtBudget.Visible = true;
                lblEuros.Visible = true;
                btnEffacerMission.Visible = true;
                btnValiderMission.Visible = true;
            }
            catch (Exception monErreur)
            {
                MessageBox.Show("Erreur : " + monErreur.Message);
            }
        }

        private void txtBudget_TextChanged(object sender, EventArgs e)
        {
            // Si le champ commence par un ou plusieurs zéros on le vide
            if (txtBudget.Text.StartsWith("0"))
            {
                txtBudget.Text = "";
                txtBudget.SelectionStart = 0;
            }
        }

        // ##############################################################################################################################################################################################################################################
        // ##############################################################################################################################################################################################################################################

    }
}