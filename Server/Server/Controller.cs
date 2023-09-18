using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Controller
    {
        static int[,] _tableau = new int[3, 3];

        public static int[,] Tableau { get { return _tableau; } private set { _tableau = value; } }


        public static bool ValiderCoup(int x, int y, int joueur)
        {
            if (ValiderCoupDispo(x, y) && ValiderCoupLegal(x, y) && ValiderCoupNombreDeTourCorrect(joueur))
            {
                return true;
            }
            return false;
        }

        public static bool JouerCoup(int x, int y, int joueur)
        {
            if (ValiderCoup(x, y, joueur))
            {
                Tableau[x, y] = joueur;
                MainWindow.RefreshBoard(Tableau);
                Connecteur.EnvoieCoup(x + "," + y);
               
                return true;
            }
            else
                return false;
        }
        public static bool ValiderCoupDispo(int x, int y)
        {
            if (Tableau[x, y] == 0)
                return true;
            else
                return false;
        }
        public static bool ValiderCoupLegal(int x, int y)
        {
            if (x > 2 || x < 0 || y < 0 || y > 2)
            {
                return false;
            }

            return true;
        }
        public static bool ValiderCoupNombreDeTourCorrect(int joueur)
        {
            int joueur1 = 0, joueur2 = 0;
            if (joueur == 1)
            {
                joueur1++;
            }
            else
            {
                joueur2++;
            }
            //compte le nombre de coup joué de chaque joueur.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Tableau[i, j] == 1)
                    {
                        joueur1++;
                    }
                    else if (Tableau[i, j] == 2)
                    {
                        joueur2++;
                    }
                }
            }

            //verifie que les joueurs on le même nombre de coup joué ou que le joueur 1 en a un de plus
            if (joueur1 - joueur2 == 0 || joueur1 - joueur2 == 1)
            {
                return true;
            }

            return false;
        }
    }
}
