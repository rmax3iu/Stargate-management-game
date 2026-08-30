using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace SAE24_Stargate
{
    public partial class ucVueStatistiques : UserControl
    {
        public ucVueStatistiques()
        {
            InitializeComponent();
        }

        private void ucVueStatistiques_Load(object sender, EventArgs e)
        {
            cboNoms.DrawMode = DrawMode.OwnerDrawFixed;
            cboMissions.DrawMode = DrawMode.OwnerDrawFixed;
            cboInformateurs.DrawMode = DrawMode.OwnerDrawFixed;
            // Retire l'effet 3D pour un rendu moderne
            cboNoms.FlatStyle = FlatStyle.Flat;
            cboMissions.FlatStyle = FlatStyle.Flat;
            cboInformateurs.FlatStyle = FlatStyle.Flat;

            // Applique des couleurs qui collent à ton thème sombre
            cboNoms.BackColor = Color.FromArgb(30, 30, 45); // Un bleu/gris très sombre (adapte selon ton fond)
            cboMissions.BackColor = Color.FromArgb(30, 30, 45);
            cboInformateurs.BackColor = Color.FromArgb(30, 30, 45);
            cboNoms.ForeColor = Color.White; // Le texte en blanc
            cboMissions.ForeColor = Color.White;
            cboInformateurs.ForeColor = Color.White;

            chargerCboEquipes();
            chargerCboMissions();
            chargerGraph();
            chargerDepenses();
            chargerInfos();
        }

        private void cboNoms_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Sécurité au démarrage
            if (e.Index < 0) return;

            ComboBox combo = sender as ComboBox;

            // Est-ce que la souris passe sur cet élément ?
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Définition de tes couleurs personnalisées
            // (Ici : un bleu électrique pour la sélection, sinon la couleur de fond)
            Color bgColor = isSelected ? Color.FromArgb(70, 130, 180) : combo.BackColor;
            Color textColor = isSelected ? Color.White : combo.ForeColor;

            // 1. On peint le fond de la ligne
            using (SolidBrush bgBrush = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
            }

            // 2. On dessine le texte par-dessus
            string texte = combo.GetItemText(combo.Items[e.Index]);
            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                // Le "+2" sert juste à faire une petite marge agréable à l'œil
                e.Graphics.DrawString(texte, e.Font, textBrush, e.Bounds.X + 2, e.Bounds.Y + 2);
            }
        }

        private void cboNoms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // 1. Protection au démarrage
            if (cboNoms.SelectedValue == null) return;

            try
            {
                dgvEquipe.Rows.Clear();
                string matricule = cboNoms.SelectedValue.ToString();
                // 2. La requête finale avec l'auto-jointure et le par  amètre
                string req1 = @"SELECT 
                            m.nom, 
                            m.prenom,
                            CASE 
                                WHEN c.matriculeMembre IS NOT NULL THEN 'Civil'
                                ELSE 'Militaire'
                            END AS Type
                        FROM (
                            SELECT DISTINCT c2.matriculeMembre 
                            FROM Composer c1
                            JOIN Composer c2 ON c1.nomPlanete = c2.nomPlanete AND c1.numeroMission = c2.numeroMission
                            WHERE c1.matriculeMembre = @matricule AND c2.matriculeMembre != @matricule
                        ) AS coequipiers
                        JOIN Membre m ON coequipiers.matriculeMembre = m.matricule
                        LEFT JOIN Civil c ON m.matricule = c.matriculeMembre
                        LEFT JOIN Militaire mil ON m.matricule = mil.matriculeMembre";

                SQLiteCommand cmd = new SQLiteCommand(req1, Connexion.Connec);

                // 3. Sécurisation de la variable
                cmd.Parameters.AddWithValue("@matricule", matricule);

                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dgvEquipe.Rows.Add(dr.GetString(0), dr.GetString(1), dr.GetString(2));
                    }
                }
                // 4. Libération de la base de données
                dr.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void pnlBudget_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            int radius = 15; // Taille de l'arrondi, tu peux modifier ce chiffre !

            // 1. On crée le "chemin" de notre forme avec 4 arcs de cercle pour les coins
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90); // Haut-Gauche
            path.AddArc(pnl.Width - radius - 1, 0, radius, radius, 270, 90); // Haut-Droite
            path.AddArc(pnl.Width - radius - 1, pnl.Height - radius - 1, radius, radius, 0, 90); // Bas-Droite
            path.AddArc(0, pnl.Height - radius - 1, radius, radius, 90, 90); // Bas-Gauche
            path.CloseFigure();

            // 2. On découpe le panel selon cette forme (pour enlever les coins pointus)
            pnl.Region = new Region(path);

            // 3. OPTIONNEL : On dessine une ligne de bordure par-dessus
            // Choisis la couleur et l'épaisseur de ta bordure ici (ex: un gris un peu plus clair, épaisseur 2)
            using (Pen pen = new Pen(Color.FromArgb(80, 80, 100), 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // Pour que l'arrondi soit lisse
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void chargerCboEquipes() //Méthode pour charger la cboEquipe
        {
            try
            {
                SQLiteConnection maConnec = Connexion.Connec;
                // NomComplet calculé directement en SQL avec || (concaténation SQLite)
                string requete = "SELECT matricule, (nom || ' ' || prenom) AS NomComplet FROM Membre ORDER BY nom";
                SQLiteCommand cmd = new SQLiteCommand(requete, maConnec);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd); //Utilisation d'un DataAdapter pour charger la table qui servira de source

                DataTable dtMembre = new DataTable(); //On crée une nouvelle table
                da.Fill(dtMembre); //Puis on la remplie avec le data adapter

                cboNoms.ValueMember = "matricule"; //On stocke le matricule
                cboNoms.DataSource = dtMembre; //On donne la table qu'on a créée en source
                cboNoms.DisplayMember = "NomComplet"; //On affiche le nom complet
                cboNoms.SelectedIndex = -1; //Important pour qu'aucun nom ne soit séléctionné au départ
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void chargerCboMissions() //Méthode pour charger la cboMissions
        {
            try
            { 
                SQLiteConnection maConnec = Connexion.Connec;
                // NomComplet calculé directement en SQL avec || (concaténation SQLite)
                string requete1 = $@"select m.nomPlanete || '-' || m.numero as codeMission, m.nomPlanete, m.numero, m.budget
                                    from Mission m
                                    where (select count(*) from Composer c 
                                    where c.nomPlanete = m.nomPlanete and c.numeroMission = m.numero) > 10";
                SQLiteCommand cmd1 = new SQLiteCommand(requete1, maConnec);
                SQLiteDataAdapter da2 = new SQLiteDataAdapter(cmd1); //Utilisation d'un DataAdapter pour charger la table qui servira de source

                DataTable dtMission = new DataTable(); //On crée une nouvelle table
                da2.Fill(dtMission); //Puis on la remplie avec le data adapter
                cboMissions.ValueMember = null;  
                //On ne stocke rien du tout car on a besoin de deux colonnes donc on utilisera le DataRowView
                cboMissions.DataSource = dtMission; //On donne la table qu'on a créée en source
                cboMissions.DisplayMember = "codeMission"; //On affiche le code des missions
                cboMissions.SelectedIndex = -1; //Important pour qu'aucun nom ne soit séléctionné au départ
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cboMissions_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                dgvMissions.Rows.Clear();
                //On cast d'abord le selected item séléctionné en data row view pour l'utiliser
                DataRowView ligneSelectionnee = cboMissions.SelectedItem as DataRowView;
                //Puis on récupère le budget initial
                Decimal budgetInitial = Convert.ToDecimal(ligneSelectionnee["budget"]);
                //Et on affiche ca dans notre label
                lblSomme.Text = budgetInitial.ToString("C2");
                //On récupère tout les champs dont on a besoin pour notre requête
                string nomPlanete = ligneSelectionnee["nomPlanete"].ToString();
                int numMission = Convert.ToInt32(ligneSelectionnee["numero"]);
                int budget = Convert.ToInt32(ligneSelectionnee["budget"]);
                string requete = $@"select d.dateD, d.motif, d.montant, t.libelle 
                                    from depense d join TypeDepense t on d.idTypeDepense = t.id
                                    where d.nomPlanete = '{nomPlanete}' and d.numeroMission = {numMission}";
                SQLiteCommand cmd = new SQLiteCommand(requete, Connexion.Connec);
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dgvMissions.Rows.Add(dr.GetString(0), dr.GetString(1), dr.GetDecimal(2).ToString("C2"), dr.GetString(3));
                        //Màj du budget à chaque ligne pour calculer le budget acutel
                        budget -= Convert.ToInt32(dr.GetDecimal(2));
                    }
                }
                // 4. Libération de la base de données
                dr.Close();
                //Affichage du budget actuel dans le label
                lblSommeA.Text = budget.ToString("C2");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void chargerGraph()
        {
            try
            {
                List<string> axeX = new List<string>();
                ChartValues<int> axeY = new ChartValues<int>(); //Format de liste spécifique aux LiveCharts
                SQLiteConnection maConnec = Connexion.Connec;
                string requete1 = $@"select p.nom, 
                                    (select count(*) from Mission m where m.nomPlanete = p.nom) as nbMissions
                                    from Planete p";
                SQLiteCommand cmd1 = new SQLiteCommand(requete1, maConnec);
                SQLiteDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        axeX.Add(dr.GetString(0));
                        axeY.Add(dr.GetInt32(1));
                    }
                }

                // Nécessaire pour "extraire" le graphique de sa boite ElementHost
                LiveCharts.Wpf.CartesianChart graph = (LiveCharts.Wpf.CartesianChart)ccPlanetes.Child;
                graph.Series = new SeriesCollection
{
                new ColumnSeries
                {
                    Title = "Missions effectuées", // Texte pour la légende
                    Values = axeY,
                    DataLabels = true,
                    // On remplit la barre avec un dégradé de bleu-cyan vif
                    Fill = new System.Windows.Media.LinearGradientBrush
                    {
                        StartPoint = new System.Windows.Point(0.5, 1),
                        EndPoint = new System.Windows.Point(0.5, 0),
                        GradientStops = new System.Windows.Media.GradientStopCollection
                        {
                            new System.Windows.Media.GradientStop(System.Windows.Media.Color.FromArgb(200, 0, 191, 255), 0),
                            new System.Windows.Media.GradientStop(System.Windows.Media.Color.FromArgb(50, 0, 80, 150), 1)
                        }
                    },

                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 255)), // Cyan pur brillant
                    StrokeThickness = 4, // Épaisseur de la bordure
                    MaxColumnWidth = 60, // On force la largeur des barres
                    FontSize = 24, // Taille très grosse (24+) pour qu'on les voie !
                    Foreground = System.Windows.Media.Brushes.White, // En blanc pour un contraste maximum
                    FontWeight = System.Windows.FontWeights.Bold // En gras si nécessaire
                }
                };

                var police = new System.Windows.Media.FontFamily("Segoe UI");
                var couleurTexte = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));

                // 2. On configure les textes en bas (Axe X)
                graph.AxisX.Add(new Axis
                {
                    Labels = axeX,
                    FontSize = 13, // Agrandit le nom des planètes
                    FontFamily = police,
                    Foreground = couleurTexte,
                    FontWeight = System.Windows.FontWeights.Bold,
                    Separator = new LiveCharts.Wpf.Separator
                    {
                        Step = 1, // Force l'alignement parfait : 1 label = 1 barre
                        IsEnabled = false // Cache les lignes verticales grises pour un look plus épuré
                    }
                });

                graph.AxisY.Add(new Axis
                {
                    Title = "Missions\n\n",
                    MinValue = 0, // Force le bas du graphique à 0
                    FontSize = 22, 
                    FontFamily = police,
                    Foreground = couleurTexte,
                    FontWeight = System.Windows.FontWeights.Bold,
                    Separator = new LiveCharts.Wpf.Separator { Step = 1 } // Avance de 1 en 1
                });

                graph.LegendLocation = LiveCharts.LegendLocation.Bottom; // Affiche la légende en bas
                graph.ChartLegend = new LiveCharts.Wpf.DefaultLegend
                {
                    Foreground = System.Windows.Media.Brushes.White
                };
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void chargerDepenses()
        {
            try
            {
                SQLiteConnection maConnec = Connexion.Connec;
                // NomComplet calculé directement en SQL avec || (concaténation SQLite)
                string requete1 = $@"select d.dateD || ' - ' || d.motif || ' - ' || d.montant as depenses, 
                                   d.nomPlanete || ' - ' || d.numeroMission as nomMission,
                                   (select mbr.nom || ' ' || mbr.prenom from Membre mbr 
                                    where mbr.matricule = m.matriculeChef) as nomChef
                                    from Depense d 
                                    join Mission m on (d.nomPlanete = m.nomPlanete and d.numeroMission = m.numero)
                                    where d.montant = (select max(montant) from Depense dp
                                    where d.nomPlanete = dp.nomPlanete and d.numeroMission = dp.numeroMission)";
                SQLiteCommand cmd1 = new SQLiteCommand(requete1, maConnec);
                SQLiteDataAdapter da2 = new SQLiteDataAdapter(cmd1); //Utilisation d'un DataAdapter pour charger la table qui servira de source

                DataTable dtDepenses = new DataTable(); //On crée une nouvelle table
                da2.Fill(dtDepenses); //Puis on la remplie avec le data adapter

                //On dit au dgv de ne pas créer les colonnes lui même
                dgvDepenses.AutoGenerateColumns = false;
                dgvDepenses.DataSource = dtDepenses;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void chargerInfos()
        {
            try
            {
                SQLiteConnection maConnec = Connexion.Connec;
                // NomComplet calculé directement en SQL avec || (concaténation SQLite)
                string requete1 = $@"select m.nomPlanete || '-' || m.numero as codeMission, m.nomPlanete, m.numero, m.budget
                                    from Mission m";
                SQLiteCommand cmd1 = new SQLiteCommand(requete1, maConnec);
                SQLiteDataAdapter da2 = new SQLiteDataAdapter(cmd1); //Utilisation d'un DataAdapter pour charger la table qui servira de source

                DataTable dtMission = new DataTable(); //On crée une nouvelle table
                da2.Fill(dtMission); //Puis on la remplie avec le data adapter
                cboInformateurs.ValueMember = null;
                //On ne stocke rien du tout car on a besoin de deux colonnes donc on utilisera le DataRowView
                cboInformateurs.DataSource = dtMission; //On donne la table qu'on a créée en source
                cboInformateurs.DisplayMember = "codeMission"; //On affiche le code des missions
                cboInformateurs.SelectedIndex = -1; //Important pour qu'aucun nom ne soit séléctionné au départ
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cboInformateurs_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                dgvInformateurs.Rows.Clear();
                //On cast d'abord le selected item séléctionné en data row view pour l'utiliser
                DataRowView ligneSelectionnee = cboInformateurs.SelectedItem as DataRowView;
                //On récupère tout les champs dont on a besoin pour notre requête
                string nomPlanete = ligneSelectionnee["nomPlanete"].ToString();
                int numMission = Convert.ToInt32(ligneSelectionnee["numero"]);
                string requete = $@"select c.nomCodeInformateur, e.nom, sum(c.sommeVersee) as ""Somme versée""
                                    from Contact c 
                                    join Informateur i on c.nomCodeInformateur = i.nomCode
                                    join Espece e on i.idEspeceEnnemi = e.id
                                    where c.nomPlanete = '{nomPlanete}' and c.numeroMission = {numMission}
                                    group by c.nomPlanete, c.numeroMission, c.nomCodeInformateur
                                    having sum(c.sommeVersee) = (
                                        select min(Total) from (
                                            select sum(sommeVersee) as Total
                                            from Contact 
                                            where nomPlanete = '{nomPlanete}' and numeroMission = {numMission}
                                            group by nomCodeInformateur
                                        )
                                    )";
                SQLiteCommand cmd = new SQLiteCommand(requete, Connexion.Connec);
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dgvInformateurs.Rows.Add(dr.GetString(0), dr.GetString(1), dr.GetDecimal(2).ToString("C2"));
                    }
                }
                dr.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }   
}
