using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Board.Pieces
{
    public class Knight : Piece
    {
        public Knight(Side side, Vector position) : base(side, position)
        {

        }
        public override List<Vector> Move(User user)
        {
            Vector position1 = new Vector(StartPosition.X + 1, StartPosition.Y);
            Vector position2 = new Vector(StartPosition.X - 1, StartPosition.Y);
            Vector position3 = new Vector(StartPosition.X, StartPosition.Y + 1);
            Vector position4 = new Vector(StartPosition.X, StartPosition.Y - 1);
            Bishop bishop1 = new Bishop(Side, position1);
            Bishop bishop2 = new Bishop(Side, position2);
            Bishop bishop3 = new Bishop(Side, position3);
            Bishop bishop4 = new Bishop(Side, position4);
            Checks.Clear();
            bishop1.Move(user);
          //  Core.Behavior(position1, user);
            Checks.AddRange(bishop1.Checks);
            bishop2.Move(user);
          //  Core.Behavior(position2, user);
            Checks.AddRange(bishop2.Checks);
            bishop3.Move(user);
          //  Core.Behavior(position3, user);
            Checks.AddRange(bishop3.Checks);
            bishop4.Move(user);
          //  Core.Behavior(position4, user);
            Checks.AddRange(bishop4.Checks);
            return Checks;

        }


    }
}
