using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Bishop : PieceBase
    {
        private static int northWest;
        private static int northEast;

        public Bishop(Board board) : base(board) { }

        public override List<int> CalculateNextMoves()
        {
            _moves.Clear();

            AddStraightLine(northWest);

            AddStraightLine(-northWest);

            AddStraightLine(northEast);

            AddStraightLine(-northEast);

            return _moves;
        }



    }
}
