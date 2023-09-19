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

                string valid = JsonSerializer.Deserialize<string>(data);

                if (valid == "CAT")
                {
                    
                }
                else
                {
                    EnvoieReponse("end");
                }
            }
            catch (Exception e)
            {
            }
        }

        static void CommencerPartie()
        {
            bool gameOver = false;
            do { 
                //traitement de la partie

            } while (gameOver == false);
        }


        public static void EnvoieCoup(string m)
        {
            string jsonString = JsonSerializer.Serialize(m);

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(jsonString + "<EOF>");

            // Send the data through the socket.
            int bytesSent = server.Send(msg);

            Recevoir("OK");
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
                EnvoieReponse("CAT");
            }
            else if (message == "coord")
            {
                if (Controller.ValiderCoup(Convert.ToInt32(Message.Substring(0, 1)), Convert.ToInt32(Message.Substring(2, 1)), 2))
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
                
            }
            else if(message == "Erreur")
            {

            }
        }
    }
}

