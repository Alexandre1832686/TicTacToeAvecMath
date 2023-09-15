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
        public static void Client()
        {

            try
            {
               
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.
                sender = new Socket(SocketType.Stream, ProtocolType.Tcp);

                

                try
                {
                    sender.Connect(remoteEP);

                    EnvoieReponse("start");

                    if (reponse == "\"end\"")
                    {
                        // Release the socket.
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }

                    if (reponse == "\"OK\"") ;
                    {
                        CommencerPartie();
                    }
                    

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
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void CommencerPartie()
        {


            //Envoie Commence...
            //Recoit coord...
            //traite coord...
            //renvoie bool...
            //recoit ak...
            //envoie coord...

            if (reponse != null)
            {
                bool valid = int.TryParse(reponse[1].ToString(), out int x);
                bool valid2 = int.TryParse(reponse[3].ToString(), out int y);
                if (valid && valid2)
                {
                    bool valid3 = Controller.JouerCoup(x, y, 1);
                    reponse = valid3.ToString();
                }
            }
        }

        public static void EnvoieReponse(string m)
        {
            string jsonString = JsonSerializer.Serialize(m);

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(jsonString + "<EOF>");

            // Send the data through the socket.
            int bytesSent = sender.Send(msg);

            Recevoir();
        }

        public static void Recevoir()
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
        }
    }
}
