using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
namespace GameServer{
     class Server{
         public static  int _maxPlayers {get; set;}
         public static int _port {get; set;}
         public static TcpListener tcpListener;
         public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        
         public static  void Start(int maxPlayers, int port){
              _maxPlayers = maxPlayers;
              _port = port;
              tcpListener = new TcpListener(IPAddress.Any,_port);
              IntializeServer();
              tcpListener.Start();
          
              tcpListener.BeginAcceptTcpClient(new AsyncCallback(tcpCallback),null);
              Console.WriteLine("Server is started");
         }
         private static void tcpCallback(IAsyncResult _result){
             Console.WriteLine("hello");
             TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
             tcpListener.BeginAcceptTcpClient(new AsyncCallback(tcpCallback),null); 
            Console.WriteLine($"Incomming connection: {_client.Client.RemoteEndPoint}");
             for(int i = 1; i <= _maxPlayers ; i++ ){
                 if(clients[i].tcp.socket == null){
                     clients[i].tcp.Connect(_client);
                     return;
                 }
             }
             Console.WriteLine($"{_client.Client.RemoteEndPoint} failed");
         }
         private static void IntializeServer(){
             for(int i = 0; i < _maxPlayers; i++){
                 clients.Add(i,new Client(i));
             }
         }
     }
}