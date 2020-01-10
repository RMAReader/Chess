using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{

    public class Pawn : PieceBase
    {
        private static int forwardOne;
        private static int forwardTwo;
        private static int captureLeft;
        private static int captureRight;
        private static int enPassantCheckLeft;
        private static int enPassantCheckRight;
        private static int enPassantRankMin;
        private static int enPassantRankMax;

        public Pawn(Board board) : base(board) { }

        public override List<int> CalculateNextMoves()
        {
            var moves = new List<int>();

            bool checkForEnPaasant = enPassantRankMin <= _position && _position <= enPassantRankMax;

            int move = _position + forwardOne;
            if (_board.Squares[move] == null)
            {
                moves.Add(move);

                if (MovesCount == 0)
                {
                    move = _position + forwardTwo;
                    if (_board.Squares[move] == null)
                        moves.Add(move);
                }
            }

            move = _position + captureLeft;
            if (_board.Squares[move] != null)
            {
                if (_board.Squares[move].Player != this.Player)
                {
                    moves.Add(move);
                }
                else if (checkForEnPaasant)
                {
                    int onLeft = _position + enPassantCheckLeft;
                    if (_board.Squares[onLeft] != null
                        && _board.Squares[onLeft].Player != Player
                        && _board.Squares[onLeft].MovesCount == 1
                        && _board.Squares[onLeft].Piece == EnumPiece.Pawn)
                    {
                        moves.Add(move);
                    }
                }
            }

            move = _position + captureRight;
            if (_board.Squares[move] != null)
            {
                if (_board.Squares[move].Player != this.Player)
                {
                    moves.Add(move);
                }
                else if (checkForEnPaasant)
                {
                    int onRight = _position + enPassantCheckRight;
                    if (_board.Squares[onRight] != null
                        && _board.Squares[onRight].Player != Player
                        && _board.Squares[onRight].MovesCount == 1
                        && _board.Squares[onRight].Piece == EnumPiece.Pawn)
                    {
                        moves.Add(move);
                    }
                }
            }

            return moves;
        }

    }
}
