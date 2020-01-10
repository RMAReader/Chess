using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Queen : PieceBase
    {
        private static int north;
        private static int east;
        private static int northWest;
        private static int northEast;

        public Queen(Board board) : base(board) { }

        public override List<int> CalculateNextMoves()
        {
            _moves.Clear();

            AddStraightLine(north);

            AddStraightLine(-north);

            AddStraightLine(east);

            AddStraightLine(-east);

            AddStraightLine(northWest);

            AddStraightLine(-northWest);

            AddStraightLine(northEast);

            AddStraightLine(-northEast);

            return _moves;
        }
    }
}
