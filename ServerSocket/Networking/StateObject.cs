﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using ChessOnline.Models.Board;
using Newtonsoft.Json;

namespace Engine.Networking
{
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
}


