using System;
using System.Collections.Generic;
using System.IO;
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


namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Button[] button = new Button[9];

        public MainWindow()
        {
            InitializeComponent();
            Connecteur.Server();

            button[0] = button1;
            button[1] = button2;
            button[2] = button3;
            button[3] = button4;
            button[4] = button5;
            button[5] = button6;
            button[6] = button7;
            button[7] = button8;
            button[8] = button9;
        }

        public static void RefreshBoard(int[,] tab)
        {
            int compteur = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tab[j,i] == 0)
                    {
                        button[compteur].Background = Brushes.Gray;
                        compteur++;
                    }
                    else if (tab[j, i] == 1)
                    {
                        button[compteur].Background = Brushes.Red;
                        compteur++;
                    }
                    else if (tab[j, i] == 2)
                    {
                        button[compteur].Background = Brushes.Blue;
                        compteur++;
                    }
                }
            }
        }
        private void click1(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(0, 0, 1);
        }
        private void click2(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(1, 0, 1);
        }
        private void click3(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(2, 0, 1);
        }
        private void click4(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(0, 1, 1);
        }
        private void click5(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(1, 1, 1);
        }
        private void click6(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(2, 1, 1);
        }
        private void click7(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(0, 2, 1);
        }
        private void click8(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(1, 2, 1);
        }
        private void click9(object sender, RoutedEventArgs e)
        {
            Controller.JouerCoup(2, 2, 1);
        }
    }
}
