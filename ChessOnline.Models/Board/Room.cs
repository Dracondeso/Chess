using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace ChessOnline.Models.Board
{
    public class Room
    {
        public string Name;
        public Board Board;
        public List<User> Users;
        public Room(string name)
        {
            Board = new Board();
            Users = new List<User>();
            Name = name;
        }
    }


}
