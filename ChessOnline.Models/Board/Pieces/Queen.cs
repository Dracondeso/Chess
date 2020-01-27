using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Board.Pieces
{
    public class Queen : Piece
    {
        public Queen(Side side, Vector position) : base(side, position)
        {
        }
        public override List<Vector> Move(DataModel dataClient)
        {
            base.Move(dataClient);
            return Checks;
        //    return Core.Behavior(StartPosition, user);
        }

    }

}

