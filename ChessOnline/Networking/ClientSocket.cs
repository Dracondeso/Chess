using ChessOnline.Controllers;
using ChessOnline.Models;
using ChessOnline.Models.Board;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace ChessOnline.Networking
    {
    public class SynchronousSocketClient
    {

        public static void StartClient(string toServer)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes(toServer);

                    // Send the data through the socket.  
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = sender.Receive(bytes);
                   ChessBoardController.SetUser(JsonConvert.DeserializeObject<User> (Encoding.ASCII.GetString(bytes)));

                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.  
                  

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading;
//using System.Text;
//using ChessOnline;
//using ChessOnline.Models;
//using Newtonsoft.Json;


//// State object for receiving data from remote device.  
//public class StateObject
//{
//    // Client socket.  
//    public Socket workSocket = null;
//    // Size of receive buffer.  
//    public const int BufferSize = 1024;
//    // Receive buffer.  
//    public byte[] buffer = new byte[BufferSize];
//    // Received data string.  
//    public StringBuilder sb = new StringBuilder();
//    public User User;
//}

//public class AsynchronousClient

//{
//    // The port number for the remote device.  
//    private const int port = 11000;

//    // ManualResetEvent instances signal completion.  
//    public static ManualResetEvent allDone = 
//        new ManualResetEvent(false);
//    private static ManualResetEvent connectDone =
//        new ManualResetEvent(false);
//    private static ManualResetEvent sendDone =
//        new ManualResetEvent(false);
//    private static ManualResetEvent receiveDone =
//        new ManualResetEvent(false);

//    // The response from the remote device.  
//    private static String response = String.Empty;

//    public static Socket StartClient()
//    {
//        // Connect to a remote device.  
//        try
//        {
//            // Establish the remote endpoint for the socket.  
//            // The name of the   
//            // remote device is "host.contoso.com".  
//            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
//            IPAddress ipAddress = ipHostInfo.AddressList[1];
//            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

//            // Create a TCP/IP socket.  
//            Socket client = new Socket(ipAddress.AddressFamily,
//                SocketType.Stream, ProtocolType.Tcp);

//            // Connect to the remote endpoint.  
//            client.BeginConnect(remoteEP,
//                new AsyncCallback(ConnectCallback), client);
//            connectDone.WaitOne();
//            return client;
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//            return null;
//        }
//    }

//    public static void AcceptCallback(IAsyncResult ar)
//    {
//        // Signal the main thread to continue.  
//        allDone.Set();

//        // Get the socket that handles the client request.  
//        Socket listener = (Socket)ar.AsyncState;
//        Socket handler = listener.EndAccept(ar);

//        // Create the state object.  
//        StateObject state = new StateObject();
//        state.workSocket = handler;
//        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//            new AsyncCallback(ReadCallback), state);
//    }
//    public static void ReadCallback(IAsyncResult ar)
//    {
//        string dataRead = string.Empty;

//        // Retrieve the state object and the handler socket  
//        // from the asynchronous state object.  
//        StateObject state = (StateObject)ar.AsyncState;
//        Socket handler = state.workSocket;

//        // Read data from the client socket.   
//        int bytesRead = handler.EndReceive(ar);

//        if (bytesRead > 0)
//        {
//            // There  might be more data, so store the data received so far.  
//            state.sb.Append(Encoding.ASCII.GetString(
//                state.buffer, 0, bytesRead));

//            // Check for end-of-file tag. If it is not there, read   
//            // more data.  
//            dataRead = state.sb.ToString();
//            Console.WriteLine(dataRead);
//            User user = JsonConvert.DeserializeObject<User>(dataRead);


//                // All the data has been read from the   
//                // client. Display it on the console.  
//                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
//                    dataRead.Length, dataRead);
//                // Echo the data back to the client.  
//                Send(handler, dataRead);

//        }
//    }

//    private static void ConnectCallback(IAsyncResult ar)
//    {
//        try
//        {
//            // Retrieve the socket from the state object.  
//            Socket client = (Socket)ar.AsyncState;

//            // Complete the connection.  
//            client.EndConnect(ar);

//            Console.WriteLine("Socket connected to {0}",
//                client.RemoteEndPoint.ToString());

//            // Signal that the connection has been made.  
//            connectDone.Set();
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//        }
//    }

//    public static void Receive(Socket client)
//    {
//        try
//        {
//            // Create the state object.  
//            StateObject state = new StateObject();
//            state.workSocket = client;

//            // Begin receiving the data from the remote device.  
//            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                new AsyncCallback(ReceiveCallback), state);

//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//        }
//    }

//    private static void ReceiveCallback(IAsyncResult ar)
//    {
//        try
//        {
//            // Retrieve the state object and the client socket   
//            // from the asynchronous state object.  
//            StateObject state = (StateObject)ar.AsyncState;
//            Socket client = state.workSocket;

//            // Read data from the remote device.  
//            int bytesRead = client.EndReceive(ar);

//            if (bytesRead > 0)
//            {
//                // There might be more data, so store the data received so far.  
//                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
//                string dataRead = state.sb.ToString();
//                // Get the rest of the data.  
//                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                    new AsyncCallback(ReceiveCallback), state);
//            }
//            else
//            {
//                // All the data has arrived; put it in response.  
//                if (state.sb.Length > 1)
//                {
//                    response = state.sb.ToString();
//                    Console.WriteLine(response);
//                }
//                // Signal that all bytes have been received.  
//                receiveDone.Set();
//            }
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//        }
//    }

//    public static void Send(Socket client, String data)
//    {
//        // Convert the string data to byte data using ASCII encoding.  
//        byte[] byteData = Encoding.ASCII.GetBytes(data);

//        // Begin sending the data to the remote device.  
//        client.BeginSend(byteData, 0, byteData.Length, 0,
//            new AsyncCallback(SendCallback), client);
//    }

//    private static void SendCallback(IAsyncResult ar)
//    {
//        try
//        {
//            // Retrieve the socket from the state object.  
//            Socket client = (Socket)ar.AsyncState;

//            // Complete sending the data to the remote device.  
//            int bytesSent = client.EndSend(ar);
//            Console.WriteLine("Sent {0} bytes to server.", bytesSent);

//            // Signal that all bytes have been sent.  
//            sendDone.Set();
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//        }
//    }

//}






