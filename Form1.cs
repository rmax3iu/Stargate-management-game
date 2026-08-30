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
using System.Xml;

namespace SAE24_Stargate
{
    public partial class frmAccueil : Form
    {
        public frmAccueil()
        {
            InitializeComponent();
        }

        // On définit la couleur globale actuelle de l'application
        public static Color CouleurGlobale = Color.FromArgb(61, 64, 66);

        // On définit la couleur par défaut qui sera utilisée pour le reset
        public static Color CouleurParDefaut = Color.FromArgb(61, 64, 66);

        // Méthode pour réinitialiser la couleur de tous les boutons
        private void ResetBoutons()
        {
            btnAccueil.BackColor = Color.White;
            btnNvlMission.BackColor = Color.White;
            btnAliens.BackColor = Color.White;
            btnPlanetes.BackColor = Color.White;
            btnStats.BackColor = Color.White;
            btnSettings.BackColor = Color.White;
        }

        private void btnAccueil_Click(object sender, EventArgs e)
        {
            ResetBoutons();
            btnAccueil.BackColor = Color.FromArgb(88, 106, 237);
            pnlContenu.Controls.Clear();
            ucVueTableauDeBord vueTdB = new ucVueTableauDeBord();
            vueTdB.Dock = DockStyle.Fill;
            vueTdB.BackColor = CouleurGlobale;
            pnlContenu.Controls.Add(vueTdB);

            lblTabDeBord.Visible = true;
            lblNvlMission.Visible = false;
            lblAliens.Visible = false;
            lblPlanetes.Visible = false;
            lblStats.Visible = false;
            lblReglages.Visible = false;
        }

        private void btnNvlMission_Click(object sender, EventArgs e)
        {
            ResetBoutons();
            btnNvlMission.BackColor = Color.FromArgb(88, 106, 237);
            pnlContenu.Controls.Clear();

            frmLogin login = new frmLogin();
            ucVueNvlMission vueNvlMission = new ucVueNvlMission();

            if (login.ShowDialog() == DialogResult.OK)
            {
                pnlContenu.Controls.Clear();
                vueNvlMission.Dock = DockStyle.Fill;
                vueNvlMission.BackColor = CouleurGlobale; 
                pnlContenu.Controls.Add(vueNvlMission);
                lblTabDeBord.Visible = false;
                lblNvlMission.Visible = true;
                lblAliens.Visible = false;
                lblPlanetes.Visible = false;
                lblStats.Visible = false;
                lblReglages.Visible = false;
            }
            else
            {
                btnAccueil.PerformClick();
            }
        }

        private void btnAliens_Click(object sender, EventArgs e)
        {
            ResetBoutons();
            btnAliens.BackColor = Color.FromArgb(88, 106, 237);
            pnlContenu.Controls.Clear();
            ucVueAliens vueAliens = new ucVueAliens();
            vueAliens.Dock = DockStyle.Fill;
            vueAliens.BackColor = CouleurGlobale;
            pnlContenu.Controls.Add(vueAliens);

            lblTabDeBord.Visible = false;
            lblNvlMission.Visible = false;
            lblAliens.Visible = true;
            lblPlanetes.Visible = false;
            lblStats.Visible = false;
            lblReglages.Visible = false;
        }

        private void btnPlanetes_Click(object sender, EventArgs e)
        {
            ResetBoutons();
            btnPlanetes.BackColor = Color.FromArgb(88, 106, 237);
            pnlContenu.Controls.Clear();
            ucVuePlanetes vuePlanetes = new ucVuePlanetes();
            vuePlanetes.Dock = DockStyle.Fill;
            vuePlanetes.BackColor = CouleurGlobale;  
            pnlContenu.Controls.Add(vuePlanetes);

            lblTabDeBord.Visible = false;
            lblNvlMission.Visible = false;
            lblAliens.Visible = false;
            lblPlanetes.Visible = true;
            lblStats.Visible = false;
            lblReglages.Visible = false;
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            ResetBoutons();
            btnStats.BackColor = Color.FromArgb(88, 106, 237);
            pnlContenu.Controls.Clear();
            ucVueStatistiques vueStats = new ucVueStatistiques();
            vueStats.Dock = DockStyle.Fill;
            vueStats.BackColor = CouleurGlobale;  
            pnlContenu.Controls.Add(vueStats);

            lblTabDeBord.Visible = false;
            lblNvlMission.Visible = false;
            lblAliens.Visible = false;
            lblPlanetes.Visible = false;
            lblStats.Visible = true;
            lblReglages.Visible = false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ResetBoutons();
            btnSettings.BackColor = Color.FromArgb(88, 106, 237);
            pnlContenu.Controls.Clear();
            ucVueParametres vuePar = new ucVueParametres();
            vuePar.Dock = DockStyle.Fill;
            vuePar.BackColor = CouleurGlobale;  
            pnlContenu.Controls.Add(vuePar);

            lblTabDeBord.Visible = false;
            lblNvlMission.Visible = false;
            lblAliens.Visible = false;
            lblPlanetes.Visible = false;
            lblStats.Visible = false;
            lblReglages.Visible = true;
        }

        private void frmAccueil_Load(object sender, EventArgs e)
        {
            btnAccueil.BackColor = Color.FromArgb(88, 106, 237);
            tltAcceuil.SetToolTip(btnAccueil, "Accueil");
            tltNouvelleMission.SetToolTip(btnNvlMission, "Nouvelle mission");
            tltAlien.SetToolTip(btnAliens, "Les races");
            tltPlanete.SetToolTip(btnPlanetes, "Les planètes");

            try
            {
                SQLiteConnection maConnec = Connexion.Connec;
                DataTable dtSchema = maConnec.GetSchema("Tables");
                for (int i = 0; i < dtSchema.Rows.Count; i++)
                {
                    string nomTable = dtSchema.Rows[i]["TABLE_NAME"].ToString();
                    string requete = "Select * from " + nomTable;
                    SQLiteCommand cmd = new SQLiteCommand(requete, maConnec);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    da.Fill(MesDatas.DsGlobal, nomTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btnAccueil.PerformClick();

            // Création d'une nouvelle table pour liaison de donnée
            DataTable tblAliens = new DataTable("tblAliens");
            tblAliens.Columns.Add("nom");
            tblAliens.Columns.Add("idEspece");
            tblAliens.Columns.Add("origine");
            tblAliens.Columns.Add("statut");
            tblAliens.Columns.Add("degreBienveillance");
            tblAliens.Columns.Add("instrument");
            tblAliens.Columns.Add("degreAgressivite");
            tblAliens.Columns.Add("arme");
            tblAliens.Columns.Add("couleur");
            tblAliens.Columns.Add("contact");

            MesDatas.DsGlobal.Tables.Add(tblAliens);
        }

        public void AppliquerCouleurGlobale(Color couleur)
        {
            try
            {
                CouleurGlobale = couleur;
                this.BackColor = couleur;
                pnlContenu.BackColor = couleur;
                pnlMenu.BackColor = couleur;

                if (tlpMenu != null)
                {
                    tlpMenu.BackColor = couleur;

                    foreach (Control ctrl in tlpMenu.Controls)
                    {
                        if (ctrl.BackColor != Color.FromArgb(88, 106, 237) && ctrl.BackColor != Color.White)
                        {
                            ctrl.BackColor = couleur;
                        }
                    }
                }

                // On change la couleur du UserControl actuellement affiché
                foreach (Control ctrl in pnlContenu.Controls)
                {
                    ctrl.BackColor = couleur;
                }
            }
            catch(Exception ex)
            {
                    MessageBox.Show("Erreur : " + ex.Message); 
            }
        }
    }
}