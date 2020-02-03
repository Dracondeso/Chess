using System;
using System.Net.WebSockets;
namespace ChessOnline.Networking
{
    public class ClientWebSocket
    {
        ClientWebSocket ClientWebSocket;
       public static void Start()
        {

        ClientWebSocket clientWebSocket = new ClientWebSocket();
            ClientWebSocketOptions clientWebSocketOptions = new ClientWebSocketOptions();
            clientWebSocketOptions.


        }
    }
    }


//using System.Net;
//using System.Threading;
//using Alchemy;
//using Alchemy.Classes;
//using NUnit.Framework;

//{
//    [TestFixture]
//    public class ClientServer
//    {
//        private WebSocketServer _server;
//        private WebSocketClient _client;
//        private bool _forever;
//        private bool _clientDataPass = true;

//        [TestFixtureSetUp]
//        public void SetUp()
//        {
//            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
//            IPAddress ipAddress = ipHostInfo.AddressList[1];
//         //   _server = new WebSocketServer(8080, ipAddress) { OnReceive = OnServerReceive };
//         //   _server.Start();
//            _client = new WebSocketClient("ws://localhost:5001/") { Origin = "localhost", OnReceive = OnClientReceive };
//            _client.Connect();
//        }

//        [TestFixtureTearDown]
//        public void TearDown()
//        {
//            _client.Disconnect();
//            _server.Stop();
//            _client = null;
//            _server = null;
//        }

//        private static void OnServerReceive(UserContext context)
//        {
//            var data = context.DataFrame.ToString();
//            context.Send(data);
//        }

//        private void OnClientReceive(UserContext context)
//        {
//            var data = context.DataFrame.ToString();
//            if (data == "Test")
//            {
//                if (_forever && _clientDataPass)
//                {
//                    context.Send(data);
//                }
//            }
//            else
//            {
//                _clientDataPass = false;
//            }
//        }

//        [Test]
//        public void ClientConnect()
//        {
//            Assert.IsTrue(_client.Connected);
//        }

//        [Test]
//        public void ClientSendData()
//        {
//            _forever = false;
//            if (_client.Connected)
//            {
//                _client.Send("Test");
//                Thread.Sleep(1000);
//            }
//            Assert.IsTrue(_clientDataPass);
//        }

//        [Test]
//        public void ClientSendDataConcurrent()
//        {
//            _forever = true;
//            if (_client.Connected)
//            {
//                var client2 = new WebSocketClient("ws://127.0.0.1:8080/") { OnReceive = OnClientReceive };
//                client2.Connect();

//                if (client2.Connected)
//                {
//                    _client.Send("Test");
//                    client2.Send("Test");
//                }
//                else
//                {
//                    _clientDataPass = false;
//                }
//                Thread.Sleep(5000);
//            }
//            else
//            {
//                _clientDataPass = false;
//            }
//            Assert.IsTrue(_clientDataPass);
//        }
//    }
//}
////using ChessOnline.Controllers;
////using ChessOnline.Models;
////using ChessOnline.Models.Board;
////using Newtonsoft.Json;
////using System;
////using System.Net;
////using System.Net.Sockets;
////using System.Text;
////    public class SynchronousSocketClient
////    {

////        public static string StartClient(string toServer)
////        {
////            // Data buffer for incoming data.  
////            byte[] bytes = new byte[1024];

////            // Connect to a remote device.  
////            try
////            {
////                // Establish the remote endpoint for the socket.  
////                // This example uses port 11000 on the local computer.  
////                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
////                IPAddress ipAddress = ipHostInfo.AddressList[0];
////                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8080);

////                // Create a TCP/IP  socket.  
////                Socket sender = new Socket(ipAddress.AddressFamily,
////                    SocketType.Stream, ProtocolType.Tcp);

////                // Connect the socket to the remote endpoint. Catch any errors.  
////                try
////                {
////                    sender.Connect(remoteEP);

////                    Console.WriteLine("Socket connected to {0}",
////                        sender.RemoteEndPoint.ToString());

////                    // Encode the data string into a byte array.  
////                    byte[] msg = Encoding.ASCII.GetBytes(toServer);

////                    // Send the data through the socket.  
////                    int bytesSent = sender.Send(msg);
////                    // Receive the response from the remote device.  
////                    int bytesRec = sender.Receive(bytes);
////                    Console.WriteLine("Echoed test = {0}",
////                        Encoding.ASCII.GetString(bytes, 0, bytesRec));
////                    // Release the socket.  
////                    sender.Shutdown(SocketShutdown.Send);
////                    return Encoding.ASCII.GetString(bytes);


////                }
////                catch (ArgumentNullException ane)
////                {
////                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
////                }
////                catch (SocketException se)
////                {
////                    Console.WriteLine("SocketException : {0}", se.ToString());
////                }
////                catch (Exception e)
////                {
////                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
////                }

////            }
////            catch (Exception e)
////            {
////                Console.WriteLine(e.ToString());
////            }
////            return null;
////        }
////    }
////}





