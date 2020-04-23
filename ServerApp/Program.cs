using System;
using System.Net;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerModel server = new ServerModel(IPAddress.Parse("127.0.0.1"), 13000);
            server.StartListening();

            Console.ReadKey();

            server.StopListening();

            Console.ReadKey();
        }
    }
}
