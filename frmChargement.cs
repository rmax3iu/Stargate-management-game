using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE24_Stargate
{
    public partial class frmChargement : Form
    {
        public frmChargement()
        {
            InitializeComponent();                                // On initialise les composants du formulaire 
            this.FormBorderStyle = FormBorderStyle.None;          // On supprime la barre de titre et les bordures
            this.StartPosition = FormStartPosition.CenterScreen;  // On centre la fenêtre au milieu de l'écran
        }

        public async Task DemarrerChargement()
        {
            pgbTransmission.Value = 0;        // On remet la barre à 0 au départ
            pgbTransmission.Maximum = 100;    // La barre va de 0 à 100

            for (int i = 0; i < 100; i++)     // On répète 100 fois (un coup = 1%)
            {
                pgbTransmission.Value++;      // On avance la barre de 1%
                await Task.Delay(10);         // On attend 10ms entre chaque tick
            }

            await Task.Delay(200);            // On fait une petite pause à 100% avant de fermer
            this.Close();                     // On ferme le formulaire de chargement
        }
    }
}