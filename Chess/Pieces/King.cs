using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class King : PieceBase
    {
        private static int[] moveOffset;
        private static int[] rookPositions;
        
        public King(Board board) : base(board) { }

        public override List<int> CalculateNextMoves()
        {
            _moves.Clear();

            for(int i=0; i < moveOffset.Length; i++)
            {
                int move = _position + moveOffset[i];
                if (IsAvailable(move))
                    _moves.Add(move);
            }
            
            return _moves;
        }

        private bool CanCastle()
        {
            if (this.MovesCount > 0)
                return false;
            for (int i = 0; i < 2; i++)
            {
                if (_board.Squares[rookPositions[i]] == null)
                    return false;
                if (_board.Squares[rookPositions[i]].MovesCount > 0)
                    return false;
            }
            for (int i = rookPositions[0] + 1; i < rookPositions[i] && i != _position; i++)
            {
                if (!IsEmptySquare(i))
                    return false;
            }
            return true;
        }
    }
}
