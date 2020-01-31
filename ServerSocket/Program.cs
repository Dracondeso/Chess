using Engine.Networking;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace Engine
{
    public static class Program
    {
        private static void Main(string[] args)
        {

            Server server = new Server();
            server.Create();


        }
    }
}
