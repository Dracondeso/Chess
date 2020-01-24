using ChessOnline.Models.Board;
using ChessOnline.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models
{
    public class DataClientModel
    {
        public ServerOperationType serverOperation { get; set; }
        public List<User> Users { get ; set;}
        public Room Room { get; set; }

    }
}
