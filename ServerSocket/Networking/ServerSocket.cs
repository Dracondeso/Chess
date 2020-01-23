﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ChessOnline.Models.Board;
using Newtonsoft.Json;

namespace Server.Networking

{// State object for reading client data asynchronously  
    public class AsynchronousSocketListener
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public AsynchronousSocketListener()
        {
        }

        public static void StartListening()
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                Core.Listener = listener;
                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();
                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);
                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();
            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();
                //if (content.IndexOf("<EOF>") > -1)
                //{
                // All the data has been read from the   
                // client. Display it on the console.  
                //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                //    content.Length, content);
                // Echo the data back to the client.  
                User user = JsonConvert.DeserializeObject<User>(content);

                content= JsonConvert.SerializeObject(Core.Elaborate(user,state));

                Send(handler, content);
                //}
                //else
                //{
                // Not all data received. Get more.  
                //handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                //new AsyncCallback(ReadCallback), state);
            }
        }


        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
//Using Math.Tools.Primitives;
//using Newtonsoft.Json;
//using Server;
//using Server.Networking;
//using Server.Pieces.Abstraction;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;
//// State object for reading client data asynchronously  


//public class AsynchronousSocketListener
//{
//    public static string CurrentUser;
//    // Thread signal.  
//    public static ManualResetEvent allDone = new ManualResetEvent(false);
//    public AsynchronousSocketListener()
//    {
//    }

//    public static void StartListening()
//    {
//        // Establish the local endpoint for the socket.  
//        // The DNS name of the computer  
//        // running the listener is "host.contoso.com".  
//        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
//        IPAddress ipAddress = ipHostInfo.AddressList[1];
//        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

//        // Create a TCP/IP socket.  
//        Socket listener = new Socket(ipAddress.AddressFamily,
//            SocketType.Stream, ProtocolType.Tcp);

//        // Bind the socket to the local endpoint and listen for incoming connections.  
//        try
//        {
//            listener.Bind(localEndPoint);
//            listener.Listen(100);

//            while (true)
//            {
//                // Set the event to nonsignaled state.  
//                allDone.Reset();

//                // Start an asynchronous socket to listen for connections.  
//                Console.WriteLine("Waiting for a connection...");
//                listener.BeginAccept(
//                    new AsyncCallback(AcceptCallback),
//                    listener);

//                // Wait until a connection is made before continuing.  
//                allDone.WaitOne();

//            }

//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//        }

//        Console.WriteLine("\nPress ENTER to continue...");
//        Console.Read();

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

//            //if (dataRead.IndexOf("<EOF>") > -1)
//            //{
//                // All the data has been read from the   
//                // client. Display it on the console.  
//                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
//                    dataRead.Length, dataRead);
//            User userRead = JsonConvert.DeserializeObject<User>(dataRead);
//                Core.Elaborate(userRead, state);
//                // Echo the data back to the client.  
//            //}
//            //else
//            //{
//            //    // Not all data received. Get more.  
//            //    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//            //    new AsyncCallback(ReadCallback), state);
//            //}
//        }
//    }

//    public static void Send(Socket handler, string data)
//    {
//        // Convert the string data to byte data using ASCII encoding.  
//        byte[] byteData = Encoding.ASCII.GetBytes(data);

//        // Begin sending the data to the remote device.  
//        handler.BeginSend(byteData, 0, byteData.Length, 0,
//            new AsyncCallback(SendCallback), handler);
//    }

//    private static void SendCallback(IAsyncResult ar)
//    {
//        try
//        {
//            // Retrieve the socket from the state object.  
//            Socket handler = (Socket)ar.AsyncState;

//            // Complete sending the data to the remote device.  
//            int bytesSent = handler.EndSend(ar);
//            Console.WriteLine("Sent {0} bytes to client.", bytesSent);
//            handler.Shutdown(SocketShutdown.Both);


//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//        }
//    }

//    private static void Disconnect(IAsyncResult ar)
//    {
//        Socket handler = (Socket)ar.AsyncState;

//        handler.Close();

//    }

//}
