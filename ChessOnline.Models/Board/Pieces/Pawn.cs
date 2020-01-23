using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Board.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Side side, Vector position) : base(side, position)
        {
        }
        public override List<Vector> Move(User user)
        {
            base.Move(user);

            if (Side == Side.White)
            {
                Vector position1 = new Vector(StartPosition.X + 1, StartPosition.Y + 1);
                Vector position2 = new Vector(StartPosition.X - 1, StartPosition.Y + 1);
                Vector position3 = new Vector(StartPosition.X, StartPosition.Y + 1);
                Vector position4 = new Vector(StartPosition.X, StartPosition.Y + 2);
                if (user.ChessBoard.ContainsKey(position1.ToString()))
                {
                    user.ChessBoard.TryGetValue(position1.ToString(), out Piece piece);
                    if (Side != piece.Side)
                    {
                        NorthEast = 1;
                    }
                    else
                        NorthEast = 0;
                }
                if (user.ChessBoard.ContainsKey(position2.ToString()))
                {
                    user.ChessBoard.TryGetValue(position2.ToString(), out Piece piece);
                    if (Side != piece.Side)
                    {
                        NorthWest = 1;
                    }
                    else
                        NorthWest = 0;
                }
                North = 0;
                if (!user.ChessBoard.ContainsKey(position3.ToString()))
                {
                    if (!user.ChessBoard.ContainsKey(position4.ToString()) && FirstMove == true)
                    {
                        North = 2;
                    }
                    if (StartPosition.Y < 8)
                    {
                        North = 1;
                    }
                }
                SouthEast = 0;
                South = 0;
                SouthWest = 0;
            }
            else
            {
                Vector position1 = new Vector(StartPosition.X + 1, StartPosition.Y - 1);
                Vector position2 = new Vector(StartPosition.X - 1, StartPosition.Y - 1);
                Vector position3 = new Vector(StartPosition.X, StartPosition.Y - 1);
                Vector position4 = new Vector(StartPosition.X, StartPosition.Y - 2);
                if (user.ChessBoard.ContainsKey(position1.ToString()))
                {
                    user.ChessBoard.TryGetValue(position1.ToString(), out Piece piece);
                    if (Side != piece.Side)
                    {
                        SouthEast = 1;
                    }
                    else
                        SouthEast = 0;
                }
                if (user.ChessBoard.ContainsKey(position2.ToString()))
                {
                    user.ChessBoard.TryGetValue(position2.ToString(), out Piece piece);
                    if (Side != piece.Side)
                    {
                        SouthWest = 1;
                    }
                    else
                        SouthWest = 0;
                    if (!user.ChessBoard.ContainsKey(position4.ToString()) && FirstMove == true)
                        South = 2;
                    if (FirstMove == false)
                        South = 1;

                    if (!user.ChessBoard.ContainsKey(position3.ToString()))
                    {
                        if (!user.ChessBoard.ContainsKey(position3.ToString()) && !user.ChessBoard.ContainsKey(position4.ToString()) && FirstMove == true)
                        {
                            South = 2;
                        }
                        if (StartPosition.Y < 8 && !user.ChessBoard.ContainsKey(position3.ToString()))
                        {
                            South = 1;
                        }
                        else
                            SouthEast = 0;
                        NorthEast = 0;
                    }
                    North = 0;
                    NorthWest = 0;
                }
            }
            return Checks;
            //  return Core.Behavior(StartPosition, user);
        }
    }
}
