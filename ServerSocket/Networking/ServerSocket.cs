using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alchemy;
using Alchemy.Classes;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json;
using ChessOnline.Models.Board;
using ChessOnline.Models;

namespace Engine.Networking
{
    public class Connection
    {
        private static Core core = new Core();

        //public System.Threading.Timer timer;
        public UserContext Context { get; set; }
        public User utente;
        public string Token; //ID partita che sta giocando
        public bool Playing { get { return !string.IsNullOrWhiteSpace(Token); } }

        public Connection()
        {
            //this.timer = new System.Threading.Timer(this.TimerCallback, null, 0, 8000);
        }
    }
    public class Server
    {
        //Thread-safe collection of Online Connections.
        public static ConcurrentDictionary<string, Connection> Connections = new ConcurrentDictionary<string, Connection>();
        //public static Dictionary<string, Hero> heroes = new Dictionary<string, Hero>();
        //public static Dictionary<string, Mappa> maps = new Dictionary<string, Mappa>();
        //public static ConcurrentDictionary<string, Dictionary<string, bool>> threadInAction = new ConcurrentDictionary<string, Dictionary<string, bool>>();

        public static int locked = 0;
        WebSocketServer aServer;
        public void Create()
        {

            aServer = new WebSocketServer(8080, System.Net.IPAddress.Any)
            {
                OnReceive = OnReceive,
                OnSend = OnSend,
                OnConnected = OnConnect,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
            };
            aServer.Start();
        }

        public void Stop()
        {
            aServer.Stop();
        }

        public void OnConnect(UserContext aContext)
        {
            Console.WriteLine("Client Connected From : " + aContext.ClientAddress.ToString());
                Connections.TryAdd(aContext.ClientAddress.ToString(), new Connection { Context = aContext });
        }

        public static void OnReceive(UserContext aContext)
        {
            
            try
            {
                Console.WriteLine("Data Received From [" + aContext.ClientAddress.ToString() + "] - " + aContext.DataFrame.ToString());
               
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public static void OnSend(UserContext aContext)
        {
            //try
            //{
            //    Console.WriteLine("Data Sent To : " + aContext.ClientAddress.ToString() + " - " + aContext.DataFrame.ToString());
            //}
            //catch { }
        }

        public static void OnDisconnect(UserContext aContext)
        {
            //Console.WriteLine("Client Disconnected : " + aContext.ClientAddress.ToString());

            // Remove the connection Object from the thread-safe collection
            Connection c;
            Connections.TryRemove(aContext.ClientAddress.ToString(), out c);
            // Dispose timer to stop sending messages to the client.
            //u.conn.timer.Dispose();
        }
    }
}


//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using ChessOnline.Models;
//using Newtonsoft.Json;

//namespace Server.Networking

//{// State object for reading client data asynchronously  
//    public class AsynchronousSocketListener
//    {

//        // Thread signal.  
//        public static ManualResetEvent allDone = new ManualResetEvent(false);
//        private static Core CoRe = new Core();

//        public AsynchronousSocketListener()
//        {
//        }
//        public static void StartListening()
//        {
//            // Establish the local endpoint for the socket.  
//            // The DNS name of the computer  
//            // running the listener is "host.contoso.com".  
//            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
//            IPAddress ipAddress = ipHostInfo.AddressList[0];
//            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

//            // Create a TCP/IP socket.  
//            Socket listener = new Socket(ipAddress.AddressFamily,
//                SocketType.Stream, ProtocolType.Tcp);

//            // Bind the socket to the local endpoint and listen for incoming connections.  
//            try
//            {
//                listener.Bind(localEndPoint);
//                listener.Listen(100);
//                while (true)
//                {
//                    // Set the event to nonsignaled state.  
//                    allDone.Reset();
//                    // Start an asynchronous socket to listen for connections.  
//                    Console.WriteLine("Waiting for a connection...");
//                    listener.BeginAccept(
//                        new AsyncCallback(AcceptCallback),
//                        listener);
//                    // Wait until a connection is made before continuing.  
//                    allDone.WaitOne();
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }

//            Console.WriteLine("\nPress ENTER to continue...");
//            Console.Read();
//        }

//        public static void AcceptCallback(IAsyncResult ar)
//        {
//            // Signal the main thread to continue.  
//            allDone.Set();
//            // Get the socket that handles the client request.  
//            Socket listener = (Socket)ar.AsyncState;
//            Socket handler = listener.EndAccept(ar);
//            // Create the state object.  
//            StateObject state = new StateObject();
//            state.workSocket = handler;
//            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                new AsyncCallback(ReadCallback), state);
//        }

//        public static void ReadCallback(IAsyncResult ar)
//        {
//            string content = string.Empty;

//            // Retrieve the state object and the handler socket  
//            // from the asynchronous state object.  
//             StateObject state = (StateObject)ar.AsyncState;
//            Socket handler = state.workSocket;

//            // Read data from the client socket.   
//            int bytesRead = handler.EndReceive(ar);

//            if (bytesRead > 0)
//            {
//                // There  might be more data, so store the data received so far.  
//                state.sb.Append(Encoding.ASCII.GetString(
//                    state.buffer, 0, bytesRead));

//                // Check for end-of-file tag. If it is not there, read   
//                // more data.  
//                content = state.sb.ToString();
//                Console.WriteLine(JsonConvert.DeserializeObject<DataModel>(content).serverOperation.ToString());
//                string toClient = JsonConvert.SerializeObject(CoRe.Elaborate(JsonConvert.DeserializeObject<DataModel>(content)));

//                //if (content.IndexOf("<EOF>") > -1)
//                //{
//                // All the data has been read from the   
//                // client. Display it on the console.  
//                //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
//                //    content.Length, content);
//                // Echo the data back to the client.  
//                Send(handler, toClient);


//                //}
//                //else
//                //{
//                // Not all data received. Get more.  
//                //handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                //new AsyncCallback(ReadCallback), state);
//            }
//        }


//        private static void Send(Socket handler, string data)
//        {
//            // Convert the string data to byte data using ASCII encoding.  
//            byte[] byteData = Encoding.ASCII.GetBytes(data);

//            // Begin sending the data to the remote device.  
//            handler.BeginSend(byteData, 0, byteData.Length, 0,
//                new AsyncCallback(SendCallback), handler);
//        }

//        private static void SendCallback(IAsyncResult ar)
//        {
//            try
//            {
//                // Retrieve the socket from the state object.  
//                Socket handler = (Socket)ar.AsyncState;

//                // Complete sending the data to the remote device.  
//                int bytesSent = handler.EndSend(ar);
//                Console.WriteLine("Sent {0} bytes to client.", bytesSent);



//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }
//    }
//}