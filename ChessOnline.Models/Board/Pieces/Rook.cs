using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Board.Pieces
{
    public class Rook : Piece
    {
        public Rook(Side side, Vector position) : base(side, position)
        {
        }
        public override List<Vector> Move(User user)
        {
            base.Move(user);
            NorthEast = 0;
            NorthWest = 0;
            SouthEast = 0;
            SouthWest = 0;
            return Checks;
          //  return Core.Behavior(StartPosition, user);

        }




    }

}

