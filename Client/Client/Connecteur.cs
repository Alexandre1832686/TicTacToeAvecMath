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

namespace Client
{
    public static class Connecteur
    {
        public static async void Connect(string ip)
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipend = new IPEndPoint(ipAddress, 1100);
            string reponse;

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            await client.ConnectAsync(ipend);
            while (true)
            {
                string messageJson = JsonSerializer.Serialize("allo");
                var jsonBytes = Encoding.UTF8.GetBytes(messageJson);

                _ = await client.SendAsync(jsonBytes,SocketFlags.None);
                
                var buffer = new byte[1024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                {
                     reponse = JsonSerializer.Deserialize<string>(response);
                    break;
                }
                Messagerecu(reponse);
            }
        }

        public static string Messagerecu(string x)
        {
            return x;
        }
    }
}
