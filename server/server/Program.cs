using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = IPAddress.Any;

            IPEndPoint ipep = new IPEndPoint(ipaddr, 34000);
            try
            {
                listenerSocket.Bind(ipep);

                listenerSocket.Listen(5);
                
                
                Console.WriteLine("Sunucu bağlantı için hazır");

                Socket client = listenerSocket.Accept();

                Console.WriteLine("İstemci bağlandı" + " - Endpoint IP'si" + client.RemoteEndPoint.ToString());
                byte[] buff = new byte[128];

                int numberOfReceiveBytes = 0;

                while (true)
                {
                    numberOfReceiveBytes = client.Receive(buff);


                    string receivedText = Encoding.ASCII.GetString(buff, 0, numberOfReceiveBytes);

                    string date = DateTime.Now.ToShortTimeString();

                    byte[] buffSend = Encoding.ASCII.GetBytes(date);


                    byte[] bufflast = buffSend.Concat(buff).ToArray();


                    Console.WriteLine("İstemci tarafından gönderilen data: " + receivedText);
                 
                    client.Send(bufflast);


                    Array.Clear(buff, 0, buff.Length);
                    numberOfReceiveBytes = 0;
                }

            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
            }


           
        }
    }
}
