using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAE24_Stargate
{
    public partial class ucVueParametres : UserControl
    {
        // On stocke la couleur sélectionnée par l'utilisateur
        private Color couleurSelectionnee = frmAccueil.CouleurGlobale;

        public ucVueParametres()
        {
            // On initialise les composants du UserControl
            InitializeComponent();
        }

        private void ucVueParametres_Load(object sender, EventArgs e)
        {
            // On centre la groupbox au chargement
            CentrerGroupBox();

            // On applique la couleur globale actuelle au fond de la page
            this.BackColor = frmAccueil.CouleurGlobale;

            // On initialise le DataGridView
            InitialiserDgvRoue();
        }

        private void pcbRoue_MouseClick(object sender, MouseEventArgs e)
        {
            // On vérifie que la roue chromatique est bien chargée
            if (pcbRoue.BackgroundImage == null) return;

            // On convertit l'image en Bitmap pour pouvoir lire les pixels
            Bitmap bmp = (Bitmap)pcbRoue.BackgroundImage;

            // On convertit les coordonnées du clic en coordonnées sur l'image
            int imgX = e.X * bmp.Width / pcbRoue.Width;
            int imgY = e.Y * bmp.Height / pcbRoue.Height;

            // On vérifie que le clic est bien dans les limites de l'image
            if (imgX >= 0 && imgX < bmp.Width && imgY >= 0 && imgY < bmp.Height)
            {
                // On récupère la couleur du pixel cliqué
                Color couleur = bmp.GetPixel(imgX, imgY);

                // On vérifie que le pixel n'est pas transparent
                if (couleur.A != 0)
                {
                    // On sauvegarde la couleur choisie
                    couleurSelectionnee = couleur;

                    // On remplit le dgv avec les infos de la couleur
                    RemplirDgvRoue(couleur);

                    // On récupère la fenêtre parente
                    Form parentForm = this.FindForm();

                    // On applique la couleur à toute l'application
                    if (parentForm is frmAccueil accueil)
                    {
                        accueil.AppliquerCouleurGlobale(couleur);
                    }
                }
            }
        }

        private void btnValiderCouleur_Click(object sender, EventArgs e)
        {
            // On récupère la fenêtre parente
            Form parentForm = this.FindForm();

            // On applique la couleur sélectionnée à toute l'application
            if (parentForm is frmAccueil accueil)
            {
                accueil.AppliquerCouleurGlobale(couleurSelectionnee);
            }
        }

        private void btnEffacerCouleur_Click(object sender, EventArgs e)
        {
            // On remet la couleur par défaut comme couleur sélectionnée
            couleurSelectionnee = frmAccueil.CouleurParDefaut;

            // On remet la couleur par défaut sur le fond de cette page
            this.BackColor = frmAccueil.CouleurParDefaut;

            // On vide et remet les infos par défaut dans le dgv
            RemplirDgvRoue(frmAccueil.CouleurParDefaut);

            // On récupère la fenêtre parente
            Form parentForm = this.FindForm();

            // On remet la couleur par défaut sur toute l'application
            if (parentForm is frmAccueil accueil)
            {
                accueil.AppliquerCouleurGlobale(frmAccueil.CouleurParDefaut);
            }
        }

        private void InitialiserDgvRoue()
        {
            // On vide les lignes existantes
            dgvRoue.Rows.Clear();

            // On vide les colonnes existantes
            dgvRoue.Columns.Clear();

            // On définit la couleur de fond du DGV
            dgvRoue.BackgroundColor = Color.FromArgb(20, 40, 80);

            // On définit la couleur des lignes de la grille
            dgvRoue.GridColor = Color.FromArgb(50, 80, 120);

            // On supprime la bordure autour du DGV
            dgvRoue.BorderStyle = BorderStyle.None;

            // On supprime la colonne grise à gauche
            dgvRoue.RowHeadersVisible = false;

            // On supprime la ligne vide du bas
            dgvRoue.AllowUserToAddRows = false;

            // On empêche le redimensionnement des lignes
            dgvRoue.AllowUserToResizeRows = false;

            // On empêche la modification des cellules
            dgvRoue.ReadOnly = true;

            // On sélectionne toute la ligne
            dgvRoue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // On étire les colonnes
            dgvRoue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // On désactive le style Windows par défaut
            dgvRoue.EnableHeadersVisualStyles = false;

            // On définit la couleur des headers
            dgvRoue.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(14, 28, 54);

            // On définit la couleur du texte des headers
            dgvRoue.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // On définit la police des headers
            dgvRoue.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // On définit la hauteur des headers
            dgvRoue.ColumnHeadersHeight = 35;

            // On définit la couleur de fond des cellules
            dgvRoue.DefaultCellStyle.BackColor = Color.FromArgb(20, 40, 80);

            // On définit la couleur du texte des cellules
            dgvRoue.DefaultCellStyle.ForeColor = Color.White;

            // On définit la police des cellules
            dgvRoue.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // On définit la couleur de fond de la sélection
            dgvRoue.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 138, 221);

            // On définit la couleur du texte sélectionné
            dgvRoue.DefaultCellStyle.SelectionForeColor = Color.White;

            // On ajoute un padding
            dgvRoue.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

            // On définit la hauteur des lignes
            dgvRoue.RowTemplate.Height = 30;

            // On définit la couleur des lignes alternées
            dgvRoue.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 55, 100);

            // On définit la couleur du texte des lignes alternées
            dgvRoue.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            // On ajoute les colonnes
            dgvRoue.Columns.Add("format", "Format");
            dgvRoue.Columns.Add("valeur", "Valeur");

            // On définit les largeurs relatives
            dgvRoue.Columns["format"].FillWeight = 30;
            dgvRoue.Columns["valeur"].FillWeight = 70;

            // On centre le texte de la colonne Format
            dgvRoue.Columns["format"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // On centre le titre de la colonne Format
            dgvRoue.Columns["format"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void RemplirDgvRoue(Color couleur)
        {
            // On vide les lignes existantes
            dgvRoue.Rows.Clear();

            // On convertit la couleur en Hex
            string hex = $"#{couleur.R:X2}{couleur.G:X2}{couleur.B:X2}";

            // On convertit la couleur en RGB
            string rgb = $"RGB({couleur.R}, {couleur.G}, {couleur.B})";

            // On récupère les valeurs HSL
            float hue = couleur.GetHue();
            float saturation = couleur.GetSaturation() * 100;
            float luminosite = couleur.GetBrightness() * 100;

            // On crée la chaîne HSL
            string hsl = $"HSL({(int)hue}, {(int)saturation}%, {(int)luminosite}%)";

            // Calcul HSV
            float r = couleur.R / 255f;
            float g = couleur.G / 255f;
            float b = couleur.B / 255f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));

            float delta = max - min;

            float hsvS = max == 0 ? 0 : delta / max;
            float hsvV = max;

            // On crée la chaîne HSV
            string hsv = $"HSV({(int)hue}, {(int)(hsvS * 100)}%, {(int)(hsvV * 100)}%)";

            // On ajoute les lignes
            dgvRoue.Rows.Add("Hex", hex);
            dgvRoue.Rows.Add("RGB", rgb);
            dgvRoue.Rows.Add("HSL", hsl);
            dgvRoue.Rows.Add("HSV", hsv);
        }

        private void CentrerGroupBox()
        {
            // On calcule la position pour centrer la groupbox horizontalement
            grpChangerCouleur.Left = (this.Width - grpChangerCouleur.Width) / 2;

            // On calcule la position pour centrer la groupbox verticalement
            grpChangerCouleur.Top = (this.Height - grpChangerCouleur.Height) / 2;
        }

        private void ucVueParametres_Resize(object sender, EventArgs e)
        {
            // On recentre la groupbox à chaque redimensionnement de la fenêtre
            CentrerGroupBox();
        }

        private void pcbRoueNoirEtBlanc_MouseClick(object sender, MouseEventArgs e)
        {
            // On vérifie que la roue est bien chargée
            if (pcbRoueNoirEtBlanc.BackgroundImage == null)
            {
                return;
            }

            // On convertit l'image en Bitmap pour pouvoir lire les pixels
            Bitmap bmp = (Bitmap)pcbRoueNoirEtBlanc.BackgroundImage;

            // On convertit les coordonnées du clic en coordonnées sur l'image
            int imgX = e.X * bmp.Width / pcbRoueNoirEtBlanc.Width;
            int imgY = e.Y * bmp.Height / pcbRoueNoirEtBlanc.Height;

            // On vérifie que le clic est bien dans les limites de l'image
            if (imgX >= 0 && imgX < bmp.Width && imgY >= 0 && imgY < bmp.Height)
            {
                // On récupère la couleur du pixel cliqué
                Color couleur = bmp.GetPixel(imgX, imgY);

                // On vérifie que le pixel n'est pas transparent
                if (couleur.A != 0)
                {
                    // On sauvegarde la couleur choisie
                    couleurSelectionnee = couleur;

                    // On remplit le dgv avec les infos de la couleur
                    RemplirDgvRoue(couleur);

                    // On récupère la fenêtre parente
                    Form parentForm = this.FindForm();

                    // On applique la couleur à toute l'application
                    if (parentForm is frmAccueil accueil)
                    {
                        accueil.AppliquerCouleurGlobale(couleur);
                    }
                }
            }
        }
    }
}