using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SendMessage(1100);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }
        static void SendMessage (int port)
        {
            string IpAdd = "10.0.0.50";
            byte[] bytes = new byte[1024];
           // IPHostEntry ipHost = Dns.GetHostEntry("10.0.0.50");
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(IpAdd), port);
            Socket sender = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(iPEndPoint);
            Console.WriteLine("Enter Messag : ");
            string message = Console.ReadLine();
            Console.WriteLine("Socket connecting {0}" ,sender.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);
            int bytesSent = sender.Send(msg);
            int byteRec = sender.Receive(bytes);
            Console.WriteLine("\n Server answer : {0} \n",Encoding.UTF8.GetString(bytes,0,byteRec));
            if (message.IndexOf("<TheEnd>") == -1)
                SendMessage(port);


            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
            
        }
    }
}
