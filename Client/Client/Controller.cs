using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace Client
{
    public static class Controller
    {
        static int[,] _tableau = new int[3, 3];
        
        public static int[,] Tableau {  get { return _tableau; } private set { _tableau = value; } }
        

        public static bool ValiderCoup(int x, int y)
        {
            if(ValiderCoupDispo(x,y)&&ValiderCoupLegal(x, y)&&ValiderCoupNombreDeTourCorrect(x, y))
            {

            }
            return true;
        }

        public static bool JouerCoup(int x, int y)
        {
            ValiderCoup(x, y);
            return true;
        }
        public static bool ValiderCoupDispo(int x, int y)
        {
            return true;
        }
        public static bool ValiderCoupLegal(int x, int y)
        {
            return true;
        }
        public static bool ValiderCoupNombreDeTourCorrect(int x, int y)
        {
            return true;
        }
    }
}
