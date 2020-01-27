using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace ChessOnline.Models.Board
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Side Side { get; set; } = Side.NotAssigned;
        public bool YourTurn { get; set; }
        public string RoomKey;
        public Piece Piece;
    }
}