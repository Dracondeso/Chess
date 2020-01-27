using System;
using System.Collections.Generic;
using System.Text;
using ChessOnline.Models.Board.Pieces.Abstraction;
using ChessOnline.Models.Board.Pieces;
using ChessOnline.Models.Primitives;
using ChessOnline.Models.Enum;

namespace ChessOnline.Models.Board
{
    public class Board
    {
        public Dictionary<string, Piece> ChessBoard;
        public Board()
        {
            Rook rook1 = new Rook(Side.White, new Vector(1, 1));
            Knight knight1 = new Knight(Side.White, new Vector(2, 1));
            Bishop bishop1 = new Bishop(Side.White, new Vector(3, 1));
            Queen queen1 = new Queen(Side.White, new Vector(4, 1));
            King king1 = new King(Side.White, new Vector(5, 1));
            Bishop bishop2 = new Bishop(Side.White, new Vector(6, 1));
            Knight knight2 = new Knight(Side.White, new Vector(7, 1));
            Rook rook2 = new Rook(Side.White, new Vector(8, 1));
            Pawn pawn1 = new Pawn(Side.White, new Vector(1, 2));
            Pawn pawn2 = new Pawn(Side.White, new Vector(2, 2));
            Pawn pawn3 = new Pawn(Side.White, new Vector(3, 2));
            Pawn pawn4 = new Pawn(Side.White, new Vector(4, 2));
            Pawn pawn5 = new Pawn(Side.White, new Vector(5, 2));
            Pawn pawn6 = new Pawn(Side.White, new Vector(6, 2));
            Pawn pawn7 = new Pawn(Side.White, new Vector(7, 2));
            Pawn pawn8 = new Pawn(Side.White, new Vector(8, 2));
            Rook rook3 = new Rook(Side.White, new Vector(1, 8));
            Knight knight3 = new Knight(Side.Black, new Vector(2, 8));
            Bishop bishop3 = new Bishop(Side.Black, new Vector(3, 8));
            Queen queen2 = new Queen(Side.Black, new Vector(4, 8));
            King king2 = new King(Side.Black, new Vector(5, 8));
            Bishop bishop4 = new Bishop(Side.Black, new Vector(6, 8));
            Knight knight4 = new Knight(Side.Black, new Vector(7, 8));
            Rook rook4 = new Rook(Side.Black, new Vector(8, 8));
            Pawn pawn9 = new Pawn(Side.Black, new Vector(1, 7));
            Pawn pawn10 = new Pawn(Side.Black, new Vector(2, 7));
            Pawn pawn11 = new Pawn(Side.Black, new Vector(3, 7));
            Pawn pawn12 = new Pawn(Side.Black, new Vector(4, 7));
            Pawn pawn13 = new Pawn(Side.Black, new Vector(5, 7));
            Pawn pawn14 = new Pawn(Side.Black, new Vector(6, 7));
            Pawn pawn15 = new Pawn(Side.Black, new Vector(7, 7));
            Pawn pawn16 = new Pawn(Side.Black, new Vector(8, 7));
            ChessBoard = new Dictionary<string, Piece>
            {
                {rook1.StartPosition.PositionToString(), rook1},
                {rook2.StartPosition.ToString(),rook2},
                {rook3.StartPosition.ToString(), rook3},
                {rook4.StartPosition.ToString(), rook4},
                {bishop1.StartPosition.ToString(), bishop1},
                {bishop2.StartPosition.ToString(), bishop2},
                {bishop3.StartPosition.ToString(), bishop3},
                {bishop4.StartPosition.ToString(), bishop4},
                {knight1.StartPosition.ToString(),knight1},
                {knight2.StartPosition.ToString(),knight2},
                {knight3.StartPosition.ToString(),knight3},
                {knight4.StartPosition.ToString(),knight4},
                {queen1.StartPosition.ToString(), queen1},
                {queen2.StartPosition.ToString(), queen2},
                {king1.StartPosition.ToString(),king1},
                {king2.StartPosition.ToString(),king2},
                {pawn1.StartPosition.ToString(),pawn1},
                {pawn2.StartPosition.ToString(),pawn2},
                {pawn3.StartPosition.ToString(),pawn3},
                {pawn4.StartPosition.ToString(),pawn4},
                {pawn5.StartPosition.ToString(),pawn5},
                {pawn6.StartPosition.ToString(),pawn6},
                {pawn7.StartPosition.ToString(),pawn7},
                {pawn8.StartPosition.ToString(),pawn8},
                {pawn9.StartPosition.ToString(),pawn9},
                {pawn10.StartPosition.ToString(),pawn10},
                {pawn11.StartPosition.ToString(),pawn11},
                {pawn12.StartPosition.ToString(),pawn12},
                {pawn13.StartPosition.ToString(),pawn13},
                {pawn14.StartPosition.ToString(),pawn14},
                {pawn15.StartPosition.ToString(),pawn15},
                {pawn16.StartPosition.ToString(),pawn16}
            };

        }

    }




}

