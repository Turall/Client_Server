using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter Server IP :");
                string ip = Console.ReadLine();
                Console.WriteLine("Enter port number: ");
                int port = int.Parse(Console.ReadLine());
                SendMessage(ip, port);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }
        static void SendMessage(string serverIp,int port)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), port);
            Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            byte[] bytes = new byte[1024];
            socket.Connect(iPEndPoint);
            Console.WriteLine("Enter message : ");
            string message = Console.ReadLine();
            Console.WriteLine("Socket Connecting {0}", socket.RemoteEndPoint.ToString());
            byte[] sendingmsg = Encoding.UTF8.GetBytes(message);
            int byteSent = socket.Send(sendingmsg);
            int bytRec = socket.Receive(bytes);
            Console.WriteLine("\n Server answer : {0} " , Encoding.UTF8.GetString(bytes,0,bytRec));

            if (message.IndexOf("<TheEnd>") == -1)
                SendMessage(serverIp,port);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
