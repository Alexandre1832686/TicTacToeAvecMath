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
        public static void Server()
        {
            compteur = 0;
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            


            try
            {


                listener.Bind(localEndPoint);

                listener.Listen(10);


                Console.WriteLine("Connection en cours ...");


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

                if (valid == "start")
                {
                    //ok
                }
                else
                {
                    //remettre en ecoute
                }



















                //CommencerPartie();


                if (Recevoir()=="start")
                {
                    //ok
                }
                else
                {
                    Recevoir();
                }




















                //server.Shutdown(SocketShutdown.Both);
                //  server.Close();

            }
            catch (Exception e)
            {
            }

        }

        static void CommencerPartie()
        {



            bool gameOver = false;
            do
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
                compteur++;
                Message = Message + compteur;



                byte[] msg = Encoding.ASCII.GetBytes("OK");
                server.Send(msg);

                

            } while (gameOver == false);
        }


        public static void EnvoieMessage(string m)
        {
            string jsonString = JsonSerializer.Serialize(m);

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(jsonString + "<EOF>");

            // Send the data through the socket.
            int bytesSent = listener.Send(msg);

            Recevoir();
        }

        public static string Recevoir()
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
            return Message;
        }
    }
}
