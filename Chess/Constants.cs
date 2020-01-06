using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    [Flags]
    public enum EnumBoardSquare : byte
    {
        Empty = 0,
        White = 2,
        Black = 3,
        Pawn = 4,
        Knight = 8,
        Bishop = 16,
        Rook = 32,
        Queen = 64,
        King = 128
    }
}
