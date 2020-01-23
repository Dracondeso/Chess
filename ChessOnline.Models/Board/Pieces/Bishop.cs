using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Board.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Side side, Vector startPosition) : base(side, startPosition)
        {
        }
        public override List<Vector> Move(User user)
        {

            base.Move(user);
            North = 0;
            South = 0;
            East = 0;
            West = 0;
            return Checks;
          //  return Core.Behavior(StartPosition, user);
        }

    }
}
