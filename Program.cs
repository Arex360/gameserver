using System;
using GameServer;
namespace UnityServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Start(500,8080);
        }
    }
}
