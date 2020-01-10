using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public enum EnumPiece : byte
    {
        Pawn = 1,
        Knight = 2,
        Bishop = 3,
        Rook = 4,
        Queen = 5,
        King = 6,
    }




    public class Board
    {
        public int Ranks;
        public int Files;

        /// <summary>
        /// 1-d array storing board as [a1,a2,a3,a4,a5,a6,a7,a8,0,0,0,0,0,0,0,0,b1,b2,b3,b4 ...] etc
        /// </summary>
        public PieceBase[] Squares;

        public int Rank(int index) { return 0; }
    }

    public class PieceBase
    {
        protected static Board _board;
        protected int _position;
        protected List<int> _moves;

        public int MovesCount;
        public EnumPlayer Player;
        public EnumPiece Piece;

        public PieceBase(Board board)
        {
            _board = board;
            _moves = new List<int>();
        }

        public virtual List<int> CalculateNextMoves() { return new List<int>(); }

        protected void AddStraightLine(int step)
        {
            int move = _position + step;
            while (IsEmptySquare(move))
            {
                _moves.Add(move);
                move += step;
            }
            if (IsOpponentPiece(move))
            {
                _moves.Add(move);
            }
        }

        protected bool IsEmptySquare(int position)
        {
            return 0 < position
                    && position < _board.Squares.Length
                    && _board.Squares[position] == null;
        }

        protected bool IsOpponentPiece(int position)
        {
            return 0 < position
                    && position < _board.Squares.Length
                    && _board.Squares[position] != null
                    && _board.Squares[position].Player != this.Player;
        }

        protected bool IsAvailable(int position)
        {
            if (position < 0)
                return false;
            if (position >= _board.Squares.Length)
                return false;
            if (_board.Squares[position] == null)
                return true;
            if (_board.Squares[position].Player != this.Player)
                return true;

            return false;
        }



    }

 


}
