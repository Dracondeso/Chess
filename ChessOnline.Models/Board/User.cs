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
        public Dictionary<string, Piece> ChessBoard;
        public User() { }
        public Vector StartPosition;
        public Vector EndPosition;
        public string RoomKey;
        public void SetRoomKey(Room room)
        {
            ChessBoard = room.Board.ChessBoard;
            Side = Side.NotAssigned;

            RoomKey = room.Name;
        }
        public void SetUser(User user)
        {
            UserName = user.UserName;
            Password = user.Password;
        }
        public void SetMove(User user)
        {
            StartPosition = user.StartPosition;
            EndPosition = user.EndPosition;
        }
        public override string ToString()
        {
            return "UserName= " + UserName + " Password= " + Password;
        }
    }
}