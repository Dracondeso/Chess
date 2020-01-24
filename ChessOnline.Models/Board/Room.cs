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
        public Board Board;
        public List<User> Users;
        public static Dictionary<string, Room> RoomsMultitone = new Dictionary<string, Room>();
        public string Token;
        private Room(string token)
        {
            Board = new Board();
            Users = new List<User>();
            Token = token;
        }
        public static Room Instance(string token)
        {
            if (RoomsMultitone.ContainsKey(token))
            {
                return RoomsMultitone[token];
            }
            else
            {
                Room room = new Room(token);
                RoomsMultitone.Add(token, room);
                return room;
            }
        }
    }
    


}
