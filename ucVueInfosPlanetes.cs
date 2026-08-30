using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UcPlanete;

namespace SAE24_Stargate
{
    public partial class ucVueInfosPlanetes : UserControl
    {
        public ucVueInfosPlanetes()
        {
            InitializeComponent();
        }

        public void initInfosPlantes(Image img, string nomPlanete, string temp, string grav, string dataBaz, Color color)
        {
            this.pcbPlanete.BackgroundImage = img;
            this.lblNom.Text = nomPlanete;
            this.lblTempValeur.Text = temp;
            this.lblGraviteValeur.Text = grav;
            this.lblDatabazValeur.ForeColor = color;
            this.lblDatabazValeur.Text = dataBaz;
        }

        // Méthode qui génère les petites cartes des aliens
        public void GenererAliens(string nomDeLaPlanete)
        {
            try
            {
                // On nettoie le panel avant de le remplir
                this.flpAliens.Controls.Clear();

                DataRow[] idEspece = MesDatas.DsGlobal.Tables["Habiter"].Select($"nomPlanete = '{nomDeLaPlanete}'");

                for (int i = 0; i < idEspece.Length; i++)
                {
                    DataRow[] espece = MesDatas.DsGlobal.Tables["Espece"].Select($"id = {idEspece[i]["idEspece"].ToString()}");

                    for (int j = 0; j < espece.Length; j++)
                    {
                        ucBadgeAliens uc = new ucBadgeAliens();
                        Image img = Image.FromFile($@"img\Aliens\{espece[j]["Couleur"]}.png");
                        string nom = espece[j]["nom"].ToString();

                        uc.initBadgeAliens(img, nom);
                        this.flpAliens.Controls.Add(uc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la génération des aliens : " + ex.Message);
            }
        }

        public void GenererMissions(string nomDeLaPlanete)
        {
            try
            {
                this.flpMissions.Controls.Clear();
                DataRow[] missions = MesDatas.DsGlobal.Tables["Mission"].Select($"nomPlanete = '{nomDeLaPlanete}'");

                for (int i = 0; i < missions.Length; i++)
                {
                    ucBadgeMissions uc = new ucBadgeMissions();
                    string nom = $"MISSION {missions[i]["numero"]}";

                    // On nettoie la date pour éviter l'affichage de l'heure s'il y en a une en base
                    string dateDepart = missions[i]["dateDepart"].ToString().Split(' ')[0];
                    string dateRetour = missions[i]["dateRetour"].ToString().Split(' ')[0];
                    string chef = missions[i]["matriculeChef"].ToString();

                    string infos = $"DÉPART    : {dateDepart}\nRETOUR    : {dateRetour}\nCHEF      : {chef}";

                    uc.initBadgeMission(nom, infos);
                    this.flpMissions.Controls.Add(uc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Methode pour "cacher" et supprimer la vue de la memoire
        private void pnlBtnRetour_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
        }

        // Méthode pour arrondir les panels
        private void ArrondirPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panelActuel = sender as Panel;
            if (panelActuel == null) return;

            // 1. On active le lissage pour éviter l'effet pixelisé
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 2. TRES IMPORTANT : On repeint le fond avec la couleur du conteneur parent 
            // (pour cacher les vrais coins carrés du panel sans utiliser Region)
            if (panelActuel.Parent != null)
            {
                e.Graphics.Clear(panelActuel.Parent.BackColor);
            }

            // 3. On crée la forme arrondie (j'ai ajusté le -1 pour que la bordure ne soit pas coupée)
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int rayon = 22;
            path.AddArc(0, 0, rayon, rayon, 180, 90);
            path.AddArc(panelActuel.Width - rayon - 1, 0, rayon, rayon, 270, 90);
            path.AddArc(panelActuel.Width - rayon - 1, panelActuel.Height - rayon - 1, rayon, rayon, 0, 90);
            path.AddArc(0, panelActuel.Height - rayon - 1, rayon, rayon, 90, 90);
            path.CloseAllFigures();

            // 4. On remplit l'intérieur de notre forme avec la vraie couleur du panel
            using (SolidBrush brush = new SolidBrush(panelActuel.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // 5. On dessine la bordure avec ta couleur violette claire (#B4C3F5)
            using (Pen pen = new Pen(Color.FromArgb(180, 195, 245), 2))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }
    }
}