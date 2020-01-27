using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Board.Pieces.Abstraction
{
    public interface IMovable
    {
        List<Vector> Move(DataModel dataClient);

    }

}
