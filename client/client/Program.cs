using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;//
using System.Net;//
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Socket client = null;
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = null;
            try
            {
                Console.WriteLine("*** TCP/IP Soket Uygulaması -- Ferdi ÜZEN");
                Console.WriteLine("Ip Adresini Giriniz ve Enter Tuşuna Basınız -- Varsayılan: 127.0.0.1");

                string strIPAddress = Console.ReadLine();

                Console.WriteLine(" Port Numarasını Giriniz -- Varsayılan:34000");
                string strPortInput = Console.ReadLine();


                int nPortInput = 0;

                if (!IPAddress.TryParse(strIPAddress, out ipaddr))
                {
                    Console.WriteLine("Geçersiz sunucu adresi girişi yapıldı!");
                    return;
                }
                if (!int.TryParse(strPortInput.Trim(), out nPortInput))
                {
                    Console.WriteLine("Geçersiz port adresi girişi yapıldı!");
                    return;
                }

                if (nPortInput <= 0 || nPortInput > 65535)
                {
                    Console.WriteLine("Port numarası 0 and 65535 arasında olmalıdır");
                    return;
                }

                System.Console.WriteLine(string.Format("IPAddress:{0} - Port {1}", ipaddr.ToString(), nPortInput));

                client.Connect(ipaddr, nPortInput);



                Console.WriteLine("Sunucuya bağlandı , Çıkmak için bir tuşa basınız");

                string inputCommand = string.Empty;


                //İstemci tarafından sunucuya hello! mesajı gönderilecek! 
                inputCommand = "hello!";


                byte[] buffSend = Encoding.ASCII.GetBytes(inputCommand);

                client.Send(buffSend);

                byte[] buffReceived = new byte[128];
                int nRecv = client.Receive(buffReceived);

                Console.WriteLine("Sunucu tarafından gelen mesaj: {0}", Encoding.ASCII.GetString(buffReceived, 0, nRecv));


                Console.ReadKey();



            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
            }

            finally
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }

                    client.Close();
                    client.Dispose();
                }

            }



        }
    }
}
