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
                        
                        imgs[compteur].Source = new BitmapImage(new Uri(@"/img/null.png", UriKind.Relative));
                        compteur++;
                    }
                    else if (tab[i, j] == 1)
                    {
                        imgs[compteur].Source = new BitmapImage(new Uri(@"/img/o.png", UriKind.Relative));
                        compteur++;
                    }
                    else if(tab[i, j] == 2)
                    {
                        imgs[compteur].Source = new BitmapImage(new Uri(@"/img/x.jpg", UriKind.Relative));
                        compteur++;
                    }
                }
            }
        }
        private void click1(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(0, 0, 2))
            {
                Controller.Tableau[0, 0] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }
        private void click2(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(1, 0, 2))
            {
                Controller.Tableau[1, 0] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }
        private void click3(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(2, 0, 2))
            {
                Controller.Tableau[2, 0] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }
        private void click4(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(0, 1, 2))
            {
                Controller.Tableau[0, 1] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }
        private void click5(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(1, 1, 2))
            {
                Controller.Tableau[1, 1] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }
        private void click6(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(2, 1, 2))
            {
                Controller.Tableau[2, 1] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }
        private void click7(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(0, 2, 2))
            {
                Controller.Tableau[0, 2] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }
        private void click8(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(1, 2, 2))
            {
                Controller.Tableau[1, 2] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }
        private void click9(object sender, RoutedEventArgs e)
        {
            if (Controller.JouerCoup(2, 2, 2))
            {
                Controller.Tableau[2, 2] = 2;
                RefreshBoard(Controller.Tableau);
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connecteur.Connection();
        }
    }
}
