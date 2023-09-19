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
using static System.Net.Mime.MediaTypeNames;

namespace Server
{
    /// <summary>
    /// Logique d'interaction pour winner.xaml
    /// </summary>
    public partial class winner : Window
    {
        public winner(int gagnant)
        {
            InitializeComponent();
            if (gagnant == 0)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //restart la game
            this.Close();
        }
    }
}
