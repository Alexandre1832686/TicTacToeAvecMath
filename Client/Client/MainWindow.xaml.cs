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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Image[] imgs = new Image[9];
        public static Image[] xonull = new Image[3];

        public MainWindow()
        {
            InitializeComponent();
            Connecteur.Client();
            
            imgs[0] = img1;
            imgs[1] = img2;
            imgs[2] = img3;
            imgs[3] = img4;
            imgs[4] = img5;
            imgs[5] = img6;
            imgs[6] = img7;
            imgs[7] = img8;
            imgs[8] = img9;

            xonull[0] = img8;
            xonull[1] = img7;
            xonull[2] = img9;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Connecteur.EnvoieReponse(msg.Text);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
           // Test.Content = Connecteur.reponse;
        }


        public static void RefreshBoard(int[,] tab)
        {
            int compteur = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tab[i, j] == 0)
                    {
                        imgs[compteur] = xonull[2];
                        compteur++;
                    }
                    else if (tab[i, j] == 1)
                    {
                        imgs[compteur] = xonull[0];
                        compteur++;
                    }
                    else if(tab[i, j] == 2)
                    {
                        imgs[compteur] = xonull[1];
                        compteur++;
                    }
                }
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            Controller.Tableau[0, 1] = 1;
            Controller.Tableau[1, 0] = 1;
            Controller.Tableau[1, 2] = 1;

            RefreshBoard(Controller.Tableau);
        }
    }
}
