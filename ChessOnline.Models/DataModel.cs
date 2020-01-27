using ChessOnline.Models.Board;
using ChessOnline.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models
{
    public class DataModel
    {
        
        public ServerOperationType serverOperation { get; set; }
        public Room Room { get; set; }
       public User User;


    }
}
