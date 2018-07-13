using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string IpAdd = "10.0.0.50";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(IpAdd),1100);

            Socket listener = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(iPEndPoint);
                listener.Listen(10);
                while (true)
                {
                    Console.WriteLine("Connecting port {0}", iPEndPoint);
                    Socket handler = listener.Accept();
                    string data = null;
                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Received text : " + data + "\n\n");
                    string reply = "Thanks for sending message symbols = " + data.Length.ToString();
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Server Closing");
                        break;
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
            Console.ReadKey();
        }
        
    }
}
