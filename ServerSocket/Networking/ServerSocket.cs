using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Alchemy;
//using Alchemy.Classes;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json;
using ChessOnline.Models.Board;
using ChessOnline.Models;
using System.Net;
using Engine;
using System.Net.WebSockets;
namespace Engine.Networking
{
    public class Server
    {
        static public HttpListener httpListener = new HttpListener();
        internal static Mutex signal = new Mutex();
        private static void Main(string[] args)
        {

            httpListener.Prefixes.Add("http://localhost:8080/");
            httpListener.Start();
            Console.WriteLine("Server in ascolto");
            while (signal.WaitOne())
            {
               
                ReceiveConnection();
            }
        }
        public static async System.Threading.Tasks.Task ReceiveConnection()
        {
            HttpListenerContext context = await
            httpListener.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                WebSocket webSocket = webSocketContext.WebSocket;
                while (webSocket.State == WebSocketState.Open)
                {
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello world")),
                        WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            signal.ReleaseMutex();
        }
    }
}

//    public class Connection
//    {
//        private static Core core = new Core();

//        //public System.Threading.Timer timer;
//        public UserContext Context { get; set; }
//        public User utente;
//        public string Token; //ID partita che sta giocando
//        public bool Playing { get { return !string.IsNullOrWhiteSpace(Token); } }

//        public Connection()
//        {
//            //this.timer = new System.Threading.Timer(this.TimerCallback, null, 0, 8000);
//        }
//    }
//    public class Server
//    {
//        //Thread-safe collection of Online Connections.
//        public static ConcurrentDictionary<string, Connection> Connections = new ConcurrentDictionary<string, Connection>();
//        //public static Dictionary<string, Hero> heroes = new Dictionary<string, Hero>();
//        //public static Dictionary<string, Mappa> maps = new Dictionary<string, Mappa>();
//        //public static ConcurrentDictionary<string, Dictionary<string, bool>> threadInAction = new ConcurrentDictionary<string, Dictionary<string, bool>>();

//        public static int locked = 0;
//        WebSocketServer aServer;
//        public void Create()
//        {
//            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
//            IPAddress ipAddress = ipHostInfo.AddressList[1];
//            aServer = new WebSocketServer(8080, IPAddress.Any)
//            {
//                OnReceive = OnReceive,
//                OnSend = OnSend,
//                OnConnected = OnConnect,
//                OnDisconnect = OnDisconnect,
//                TimeOut = new TimeSpan(0, 5, 0)
//            };
//            aServer.Start();
//            Console.WriteLine("Il server é stato avviato");

//        }

//        public void Stop()
//        {
//            aServer.Stop();
//        }

//        public void OnConnect(UserContext aContext)
//        {
//            Console.WriteLine("Client Connected From : " + aContext.ClientAddress.ToString());
//            Connections.TryAdd(aContext.ClientAddress.ToString(), new Connection { Context = aContext });
//        }

//        public static void OnReceive(UserContext aContext)
//        {

//            try
//            {
//                Console.WriteLine("Data Received From [" + aContext.ClientAddress.ToString() + "] - " + aContext.DataFrame.ToString());


//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message.ToString());
//            }
//        }

//        public static void OnSend(UserContext aContext)
//        {
//            //try
//            //{
//            //    Console.WriteLine("Data Sent To : " + aContext.ClientAddress.ToString() + " - " + aContext.DataFrame.ToString());
//            //}
//            //catch { }
//        }

//        public static void OnDisconnect(UserContext aContext)
//        {
//            //Console.WriteLine("Client Disconnected : " + aContext.ClientAddress.ToString());

//            // Remove the connection Object from the thread-safe collection
//            Connection c;
//            Connections.TryRemove(aContext.ClientAddress.ToString(), out c);
//            // Dispose timer to stop sending messages to the client.
//            //u.conn.timer.Dispose();
//        }
//    }
//}


////namespace Engine.Networking
////{
////    public class Connection
////    {
////        //public System.Threading.Timer timer;
////        public UserContext Context { get; set; }
////        public User utente;
////        public string Token; //ID partita che sta giocando
////        public bool Playing { get { return !string.IsNullOrWhiteSpace(Token); } }

////        public Connection()
////        {
////            //this.timer = new System.Threading.Timer(this.TimerCallback, null, 0, 8000);
////        }
////    }
////    public class Server
////    {
////        public static Core core = new Core();
////        //Thread-safe collection of Online Connections.
////        public static ConcurrentDictionary<string, Room> OnlineGames = new ConcurrentDictionary<string, Room>();
////        public static ConcurrentDictionary<string, Room> PreGames = new ConcurrentDictionary<string, Room>();
////        public static ConcurrentDictionary<string, bool> bannedConnection = new ConcurrentDictionary<string, bool>();
////        public static ConcurrentDictionary<string, Connection> Connections = new ConcurrentDictionary<string, Connection>();
////        //public static Dictionary<string, Hero> heroes = new Dictionary<string, Hero>();
////        //public static Dictionary<string, Mappa> maps = new Dictionary<string, Mappa>();
////        //public static ConcurrentDictionary<string, Dictionary<string, bool>> threadInAction = new ConcurrentDictionary<string, Dictionary<string, bool>>();

////        public static int locked = 0;
////        WebSocketServer aServer;
////        public void Create()
////        {
////            aServer = new WebSocketServer(8080, IPAddress.Any)
////            {
////                OnReceive = OnReceive,
////                OnSend = OnSend,
////                OnConnected = OnConnect,
////                OnDisconnect = OnDisconnect,
////                TimeOut = new TimeSpan(0, 5, 0)
////            };
////            aServer.Start();
////        }

////        public void Stop()
////        {
////            aServer.Stop();
////        }

////        public void OnConnect(UserContext aContext)
////        {
////            Console.WriteLine("Client Connected From : " + aContext.ClientAddress.ToString());
////            if (!bannedConnection.ContainsKey(aContext.ClientAddress.ToString()))
////            {
////                Connections.TryAdd(aContext.ClientAddress.ToString(), new Connection { Context = aContext });
////            }
////        }

////        public static void OnReceive(UserContext aContext)

////        {
////            try
////            {
////                Console.WriteLine("Data Received From [" + aContext.ClientAddress.ToString() + "] - " + aContext.DataFrame.ToString());
////                if (Connections.ContainsKey(aContext.ClientAddress.ToString()))
////                {
////                    ThreadStart a = async delegate
////                    {
////                        try
////                        {
////                            DataModel data = JsonConvert.DeserializeObject<DataModel>(aContext.DataFrame.ToString());
////                        }
////                        catch { }
////                    };
////                    new Thread(a).Start();
////                }
////            }
////            catch (Exception ex)
////            {
////                Console.WriteLine(ex.Message.ToString());
////            }
////        }

////        public static void OnSend(UserContext aContext)
////        {
////            //try
////            //{
////            //    Console.WriteLine("Data Sent To : " + aContext.ClientAddress.ToString() + " - " + aContext.DataFrame.ToString());
////            //}
////            //catch { }
////        }

////        public static void OnDisconnect(UserContext aContext)
////        {
////            //Console.WriteLine("Client Disconnected : " + aContext.ClientAddress.ToString());

////            // Remove the connection Object from the thread-safe collection
////            Connection c;
////            Connections.TryRemove(aContext.ClientAddress.ToString(), out c);
////            // Dispose timer to stop sending messages to the client.
////            //u.conn.timer.Dispose();
////        }
////    }
////}


