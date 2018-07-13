using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipAdd = "10.0.1.13";
            IPEndPoint IpEnd = new IPEndPoint(IPAddress.Parse(ipAdd), 1100);
            Socket socket = new Socket(IpEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(IpEnd);
                socket.Listen(10);
                while (true)
                {
                    Console.WriteLine("Connecting to socket : {0}",IpEnd);
                    Socket listener = socket.Accept();
                    byte[] bytes = new byte[1024];
                    string data = null;
                    int bytecount = listener.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytecount);
                    Console.WriteLine("Received Text : " + data + "\n");
                    string reply = "Thanks for sending message symbols = " + data.Length.ToString();
                    byte[] senbytes = Encoding.UTF8.GetBytes(reply);

                    listener.Send(senbytes);

                    if(data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Server Closing");
                        break;
                    }
                    listener.Shutdown(SocketShutdown.Both);
                    listener.Close();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
