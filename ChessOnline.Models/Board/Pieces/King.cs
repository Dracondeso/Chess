using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Board.Pieces
{
    public class King : Piece
    {
        public King(Side side, Vector position) : base(side, position)
        {
        }
        public override List<Vector> Move(User user)
        {
            base.Move(user);
            North = 1;
            NorthEast = 1;
            NorthWest = 1;
            South = 1;
            SouthEast = 1;
            SouthWest = 1;
            return Checks;
           // return Core.Behavior(StartPosition, user);
        }


    }
}

