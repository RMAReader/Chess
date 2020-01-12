using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Test
{
    [TestClass]
    public class BoardCodingTests
    {
        [TestMethod]
        public void Board1()
        {
            var occupied = EnumBoardSquare.White | EnumBoardSquare.Black;
            var occupiedStr = Convert.ToString(((byte)occupied), 2);

            var piece = EnumBoardSquare.Pawn 
                | EnumBoardSquare.Knight 
                | EnumBoardSquare.Bishop 
                | EnumBoardSquare.Rook 
                | EnumBoardSquare.Queen 
                | EnumBoardSquare.King;

            var pieceStr = Convert.ToString(((byte)piece), 2);
            var pieceNum = ((byte)piece).ToString();

            var whitePawn = EnumBoardSquare.White | EnumBoardSquare.Pawn;
            var whiteKnight = EnumBoardSquare.White | EnumBoardSquare.Knight;
            var whiteBishop = EnumBoardSquare.White | EnumBoardSquare.Bishop;
            var whiteRook = EnumBoardSquare.White | EnumBoardSquare.Rook;
            var whiteKing = EnumBoardSquare.White | EnumBoardSquare.King;
            var whiteQueen = EnumBoardSquare.White | EnumBoardSquare.Queen;

            var whitePawnStr = Convert.ToString(((byte)whitePawn), 2);
            var whiteKnightStr = Convert.ToString(((byte)whiteKnight), 2);
            var whiteBishopStr = Convert.ToString(((byte)whiteBishop), 2);
            var whiteRookStr = Convert.ToString(((byte)whiteRook), 2);
            var whiteKingStr = Convert.ToString(((byte)whiteKing), 2);
            var whiteQueenStr = Convert.ToString(((byte)whiteQueen), 2);

            var blackPawn = EnumBoardSquare.Black | EnumBoardSquare.Pawn;
            var blackKnight = EnumBoardSquare.Black | EnumBoardSquare.Knight;
            var blackBishop = EnumBoardSquare.Black | EnumBoardSquare.Bishop;
            var blackRook = EnumBoardSquare.Black | EnumBoardSquare.Rook;
            var blackKing = EnumBoardSquare.Black | EnumBoardSquare.King;
            var blackQueen = EnumBoardSquare.Black | EnumBoardSquare.Queen;

            var blackPawnStr = Convert.ToString(((byte)blackPawn), 2);
            var blackKnightStr = Convert.ToString(((byte)blackKnight), 2);
            var blackBishopStr = Convert.ToString(((byte)blackBishop), 2);
            var blackRookStr = Convert.ToString(((byte)blackRook), 2);
            var blackKingStr = Convert.ToString(((byte)blackKing), 2);
            var blackQueenStr = Convert.ToString(((byte)blackQueen), 2);

            bool isOccupied1 = (whitePawn & occupied) != EnumBoardSquare.Empty;
            bool isWhite1 = (whitePawn & EnumBoardSquare.White) == EnumBoardSquare.White;
            bool isBlack1 = (whitePawn & EnumBoardSquare.Black) == EnumBoardSquare.Black;
            bool isPawn1 = (whitePawn & EnumBoardSquare.Pawn) == EnumBoardSquare.Pawn;
            bool isKnight1 = (whitePawn & EnumBoardSquare.Knight) == EnumBoardSquare.Knight;
            bool isBishop1 = (whitePawn & EnumBoardSquare.Bishop) == EnumBoardSquare.Bishop;
            bool isRook1 = (whitePawn & EnumBoardSquare.Rook) == EnumBoardSquare.Rook;

            bool isOccupied2 = (blackPawn & occupied) != EnumBoardSquare.Empty;
            bool isWhite2 = (blackPawn & EnumBoardSquare.White) == EnumBoardSquare.White;
            bool isBlack2 = (blackPawn & EnumBoardSquare.Black) == EnumBoardSquare.Black;
            bool isPawn2 = (blackPawn & EnumBoardSquare.Pawn) == EnumBoardSquare.Pawn;
            bool isKnight2 = (blackPawn & EnumBoardSquare.Knight) == EnumBoardSquare.Knight;
            bool isBishop2 = (blackPawn & EnumBoardSquare.Bishop) == EnumBoardSquare.Bishop;
            bool isRook2 = (blackPawn & EnumBoardSquare.Rook) == EnumBoardSquare.Rook;

            bool areSameCOlour1 = (whitePawn & occupied) == (whiteKnight & occupied);
            bool areSameCOlour2 = (whitePawn & occupied) == (blackKnight & occupied);
            bool areSameCOlour3 = (whitePawn & occupied) == (EnumBoardSquare.Empty & occupied);

            var colour1 = whitePawn & occupied;
            var colour2 = blackKnight & occupied;
            var piece1 = whitePawn & piece;
            var piece2 = blackKnight & piece;

            switch (whitePawn)
            {
                case EnumBoardSquare.White | EnumBoardSquare.Pawn:
                    break;

                default:
                    throw new ArgumentException("Invalid Enum");

            }


        }

        [TestMethod]
        public void BoardTest()
        {
            //var board = Board.Factory.GetDefault();
            var board = Board.Factory.GetBoardWithPieces();

            var pawnMoves = board.GetMovesForPiece(1, 4);
            var knightMoves = board.GetMovesForPiece(0, 1);
            var bishopMoves = board.GetMovesForPiece(0, 2);
            var queenMoves = board.GetMovesForPiece(0, 3);
            var kingMoves = board.GetMovesForPiece(0, 4);
        }


        [TestMethod]
        public void GameTreeTest()
        {
            var gameTree = new GameTree();

            var board = Board.Factory.GetDefault();

            var moves = gameTree.AlphaBetaRecursive(board, 6, true);
            var board2 = board.MakeMove(moves.First());

            var moves2 = gameTree.AlphaBetaRecursive(board2, 6, false);
            var board3 = board.MakeMove(moves2.First());

            var moves3 = gameTree.AlphaBetaRecursive(board3, 6, false);
            var board4 = board.MakeMove(moves3.First());


        }
    }
}
