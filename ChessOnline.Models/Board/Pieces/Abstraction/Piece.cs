using ChessOnline.Models.Enum;
using ChessOnline.Models.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Board.Pieces.Abstraction
{
    public abstract class Piece : IMovable
    {
        public string Name => GetType().Name;
        public Vector StartPosition { get; set; }
        public Vector EndPosition { get; set; }
        public Side Side { get; set; }
        public bool FirstMove { get; set; }
        public List<Vector> Checks;
        public double[] DirectionSteps;
        public double North;
        public double South;
        public double East;
        public double West;
        public double NorthEast;
        public double NorthWest;
        public double SouthEast;
        public double SouthWest;

        public Piece(Side side, Vector startPosition)
        {
            Side = side;
            StartPosition = startPosition;
            FirstMove = true;
        }
        public virtual List<Vector> Move(User user)
        {
            if (Checks == null)
            {

                Checks = new List<Vector>();
            }
            Checks.Clear();

            North = 8 - StartPosition.Y;
            South = 0 + StartPosition.Y;
            East = 8 - StartPosition.X;
            West = 0 + StartPosition.X;
            double piecePosition;
            if (StartPosition.X <= StartPosition.Y)
            {
                piecePosition = StartPosition.X;
            }
            else
            {
                piecePosition = StartPosition.Y;
            }
            NorthEast = 8 - piecePosition;
            NorthWest = 1 - piecePosition;
            SouthEast = 8 - piecePosition;
            SouthWest = 1 - piecePosition;
            return Checks;
            //   return Core.Behavior(StartPosition, user);
        }

    }
}

