using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Media;

namespace Client
{
    public static class Controller
    {
        static int[,] _tableau = new int[3, 3];
        
        public static int[,] Tableau {  get { return _tableau; } private set { _tableau = value; } }
        public static int[] dernierCoup = new int[2];

        public static bool ValiderCoup(int x, int y,int joueur)
        {

            if(ValiderCoupDispo(x,y)&&ValiderCoupLegal(x, y)&&ValiderCoupNombreDeTourCorrect(joueur))
            {
               
                return true;
               
            }
            return false;
        }

        public static bool JouerCoup(int x, int y,int joueur)
        {
            if (ValiderCoup(x, y,joueur))
            {
                dernierCoup[0] = x;
                dernierCoup[1] = y;
                Tableau[x, y] = joueur;
                MainWindow.RefreshBoard(Tableau);

                int valid = ValideVictoire(Tableau);
                if (valid != 0)
                {
                    Winner w = new Winner(valid);
                    Connecteur.EnvoieReponse(valid.ToString());

                }
                else
                {

                    Connecteur.EnvoieReponse(x + "," + y);
                }
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
            if (x > 2 || x < 0 || y<0 || y>2)
            {
                return false;
            }

            return true;
        }
        public static bool ValiderCoupNombreDeTourCorrect(int joueur)
        {
            int joueur1=0, joueur2=0;
            if(joueur==1)
            {
                joueur1++;
            }
            else
            {
                joueur2++;
            }
            //compte le nombre de coup joué de chaque joueur.
            for(int i =0;i<3;i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Tableau[i,j]==1)
                    {
                        joueur1++;
                    }
                    else if (Tableau[i, j]==2)
                    {
                        joueur2++;
                    }
                }
            }

            //verifie que les joueurs on le même nombre de coup joué ou que le joueur 1 en a un de plus
            if(joueur1 - joueur2 == 0 || joueur1 - joueur2 == 1)
            {
                return true;
            }

            return false;
        }
        public static int ValideVictoire(int[,] tab)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                   
                    if (tab[j, 0] == tab[j, 1] && tab[j, 1] == tab[j, 2] )
                    {
                        return tab[j, 0];
                    }
                    if (tab[0, i] == tab[1, i] && tab[1, i] == tab[2, i])
                    {
                        return tab[1, i];
                    }
                    if (tab[0, 2] == tab[1, 1] && tab[2,0] == tab[1, 1])
                    {
                        return tab[1, 1];
                    }
                    if (tab[2, 2] == tab[1, 1] && tab[0, 0] == tab[1, 1])
                    {
                        return tab[1, 1];
                    }
                }
            }

            return 0;
        }

        
    }
}
