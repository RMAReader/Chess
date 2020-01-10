using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Rook : PieceBase
    {

        private static int north;
        private static int east;

        public Rook(Board board) : base(board) { }

        public override List<int> CalculateNextMoves()
        {
            _moves.Clear();

            AddStraightLine(north);

            AddStraightLine(-north);

            AddStraightLine(east);

            AddStraightLine(-east);

            return _moves;
        }

    }
}
