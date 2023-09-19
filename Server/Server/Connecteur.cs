using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using System.Configuration;
using System.Reflection;

namespace Server
{
    internal class Connecteur
    {
        public static string Message { get; set; }
        static Socket server;
        static Socket listener;
        static int compteur;
        static IPEndPoint localEndPoint;
        public static void Server()
        {
            compteur = 0;
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, 11000);
            listener = new Socket(SocketType.Stream, ProtocolType.Tcp);

            Connection();
        }

        static void Connection()
        {
            try
            {
                listener.Bind(localEndPoint);

                listener.Listen(10);

                server = listener.Accept();
                Recevoir("CAT");

            }
            catch (Exception e)
            {
            }
        }

        public static void EnvoieReponse(string m)
        {
            string jsonString = JsonSerializer.Serialize(m);

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(jsonString + "<EOF>");

            // Send the data through the socket.
            int bytesSent = server.Send(msg);

            if (m == "CAT")
            {
                Recevoir("coord");
            }
            else if (m == "OK")
            {
                Recevoir("CAT");
            }
            else
            {
                Recevoir("OK");
            }
        }

        public static void Recevoir(string message)
        {
            byte[] bytes;
            string data = null;

            while (true)
            {
                bytes = new byte[1024];
                int bytesRec = server.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }

            int pos = data.IndexOf("<EOF>");
            if (pos >= 0)
            {
                // String after founder
                data = data.Remove(pos);
            }

            Message = JsonSerializer.Deserialize<string>(data);

            if (message == "OK")
            {
                if(Message=="Erreur")
                {
                    Controller.Tableau[Controller.dernierCoup[0], Controller.dernierCoup[1]] = 0;
                    MainWindow.RefreshBoard(Controller.Tableau);
                }
                else if (Message=="1" || Message == "2")
                {
                    winner w = new winner(Convert.ToInt32(Message));
                    w.Show();
                }
                else
                {
                    MainWindow.RefreshBoard(Controller.Tableau);
                    EnvoieReponse("CAT");
                }
            }
            else if (message == "coord")
            {
                if (Message == "1" || Message == "2")
                {
                    winner w = new winner(Convert.ToInt32(Message));
                    w.Show();
                }
                else if (Controller.ValiderCoup(Convert.ToInt32(Message.Substring(0, 1)), Convert.ToInt32(Message.Substring(2, 1)), 2))
                {
                    Controller.Tableau[Convert.ToInt32(Message.Substring(0, 1)), Convert.ToInt32(Message.Substring(2, 1))] = 2;
                    MainWindow.RefreshBoard(Controller.Tableau);
                    EnvoieReponse("OK");
                }
                else
                {
                    EnvoieReponse("Erreur");
                }
            }
            else if (message == "CAT")
            {
                //Laisse l'utilisateur jouer son coup en cliquant sur le boutton

            }
        }
    }
}

