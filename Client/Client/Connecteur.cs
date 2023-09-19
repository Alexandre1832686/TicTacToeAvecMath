using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;

namespace Client
{
    public static class Connecteur
    {
        public static string reponse { get; set; }
        static Socket sender;
        static IPEndPoint remoteEP;
        public static void Client()
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.
                sender = new Socket(SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Connection()
        {
            try
            {
                sender.Connect(remoteEP);
                EnvoieReponse("CAT");
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        public static void EnvoieCoup(string m)
        {
            string jsonString = JsonSerializer.Serialize(m);

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(jsonString + "<EOF>");

            // Send the data through the socket.
            int bytesSent = sender.Send(msg);

            Recevoir("OK");
        }
        public static void EnvoieReponse(string m)
        {
            string jsonString = JsonSerializer.Serialize(m);

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(jsonString + "<EOF>");

            // Send the data through the socket.
            int bytesSent = sender.Send(msg);
            if(m=="CAT")
            {
                Recevoir("coord");
            }
            else if (m=="OK")
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
                int bytesRec = sender.Receive(bytes);
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

            reponse = JsonSerializer.Deserialize<string>(data);

            if (message == "OK")
            {
                EnvoieReponse("CAT");
            }
            else if (message == "coord")
            {
                if (Controller.ValiderCoup(Convert.ToInt32(reponse.Substring(0, 1)), Convert.ToInt32(reponse.Substring(2, 1)), 1))
                {
                    Controller.Tableau[Convert.ToInt32(reponse.Substring(0, 1)), Convert.ToInt32(reponse.Substring(2, 1))] = 1;
                    MainWindow.RefreshBoard(Controller.Tableau);
                    EnvoieReponse("OK");
                }
            }
            else if (message == "CAT")
            {

            }
        }
    }
}
