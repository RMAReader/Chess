using Chess.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Test
{
    [TestClass]
    public class MoveTests
    {
        [TestMethod]
        public void Knight_MiddleOfBoard()
        {
            var state = new State();
            state.InitialiseBoard(8, 8);
            state.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Knight), 4, 4);

            var moves = state.GetKnightMoveOptions(new Position(4, 4), EnumPlayer.White);
        }

        [TestMethod]
        public void Bishop_MiddleOfBoard()
        {
            var state = new State();
            state.InitialiseBoard(8, 8);
            state.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Bishop), 4, 4);

            var moves = state.GetBishopMoveOptions(new Position(4, 4), EnumPlayer.White);
        }

        [TestMethod]
        public void MoveLookup()
        {
            var board = new Board(8, 8);
            var lookup = board.MoveLookup;
        }
    }
}
