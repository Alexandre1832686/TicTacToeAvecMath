using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Logique d'interaction pour Winner.xaml
    /// </summary>
    public partial class Winner : Window
    {
        public Winner(int gagnant)
        {
            InitializeComponent();
            if(gagnant == 0)
            {
                text.Text = "Partie nulle";
            }
            else if (gagnant == 1)
            {
                text.Text = "Rouge gagne!";

            }
            else if (gagnant == 1)
            {
                text.Text = "Bleu gagne!";

            }
        }
    }
}
