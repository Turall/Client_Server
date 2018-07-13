using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            test.ThisIsProperty = "TestServer";
            test.ServerIsRunning = true;
            test.ThisIsMethod(false);
        }
        
    }

    class Test
    {
        public string ThisIsProperty  { get; set; }
        public bool ServerIsRunning { get; set; }
        public void ThisIsMethod(bool ServerRun)
        {
            if (ServerRun)
            {
                Console.WriteLine("Server Is Run");
            }
            else
            {
                Console.WriteLine("Server Not Working");
            }
        }
    }
}
