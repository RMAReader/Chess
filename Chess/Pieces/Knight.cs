using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Knight : PieceBase
    {
        private static int[] moveOffset;

        public Knight(Board board) : base(board) { }

        public override List<int> CalculateNextMoves()
        {
            _moves.Clear();
            for (int i=0; i < moveOffset.Length; i++)
            {
                int move = _position + moveOffset[i];
                if(IsAvailable(move))
                {
                    _moves.Add(move);
                }
            }
            return _moves;
        }
    }
}
