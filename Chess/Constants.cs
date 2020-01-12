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
        White = 1,
        Black = 2,
        Pawn = 4,
        Knight = 8,
        Bishop = 12,
        Rook = 16,
        Queen = 20,
        King = 24,
        Piece = 28,
        MovedOnce = 32,
        MovedMoreThanOnce = 64
    }

    public static class PieceValueCOnstants
    {
        public static float Pawn = 7;
        public static float Knight = 31;
        public static float Bishop = 33;
        public static float Rook = 50;
        public static float Queen = 79;
        public static float King = float.MaxValue;
    }

    public static class WhitePieceSquareValueConstants
    {
        public static float[,] Pawn = new float[,]
            {
                {0,0,0,0,0,0,0,0 },
                {0.5f,1,1,-2,-2,1,1,0.5f },
                {0.5f,-0.5f,-1,0,0,-1,-0.5f,0.5f },
                {0,0,0,2,2,0,0,0 },
                {0.5f,0.5f,1,2.5f,2.5f,1,0.5f,0.5f },
                { 1,1,2,3,3,2,1,1},
                { 5,5,5,5,5,5,5,5},
                { 0,0,0,0,0,0,0,0}
            };

        public static float[,] Knight = new float[,]
            {
                {-5,-4,-3,-3,-3,-3,-4,-5 },
                {-4,-2,0,0.5f,0.5f,0,-2,-4 },
                {-3,0.5f,1,1.5f,1.5f,1,0.5f,-3 },
                {-3,0,1.5f,2,2,1.5f,0,-3 },
                {-3,0,1.5f,2,2,1.5f,0,-3 },
                {-3,0.5f,1,1.5f,1.5f,1,0.5f,-3 },
                {-4,-2,0,0.5f,0.5f,0,-2,-4 },
                {-5,-4,-3,-3,-3,-3,-4,-5 },
            };

        public static float[,] Bishop = new float[,]
            {
                {-2,-1,-1,-1,-1,-1,-1,-2 },
                {-1,0.5f,0,0,0,0,0.5f,-1 },
                {-1,1,1,1,1,1,1,-1 },
                {-1,0,1,1,1,1,0,-1 },
                {-1,0.5f,0.5f,1,1,0.5f,0.5f,-1 },
                {-1,0,0.5f,1,1,0.5f,0,-1 },
                {-1,0,0,0,0,0,0,-1 },
                {-2,-1,-1,-1,-1,-1,-1,-2 },
            };

        public static float[,] Rook = new float[,]
            {
                {0,0,0,0.5f,0.5f,0,0,0 },
                {-0.5f,0,0,0,0,0,0,-0.5f },
                {-0.5f,0,0,0,0,0,0,-0.5f },
                {-0.5f,0,0,0,0,0,0,-0.5f },
                {-0.5f,0,0,0,0,0,0,-0.5f },
                {-0.5f,0,0,0,0,0,0,-0.5f },
                {0.5f,1,1,1,1,1,1,0.5f },
                {0,0,0,0,0,0,0,0 }

            };

        public static float[,] Queen = new float[,]
            {
                {-2,-1,-1,-0.5f,-0.5f,-1,-1,-2 },
                { -1, 0, 0.5f,0,0,0.5f,0,-1 },
                {-1,0.5f,0.5f,0.5f,0.5f,0.5f,0,-1 },
                {0,0,0.5f,0.5f,0.5f,0.5f,0,-0.5f },
                {-0.5f,0,0.5f,0.5f,0.5f,0.5f,0,-0.5f },
                {-1,0,0.5f,0.5f,0.5f,0.5f,0,-1 },
                {-1,0,0,0,0,0,0,-1 },
                {-2,-1,-1,-0.5f,-0.5f,-1,-1,-2 }
            };

        public static float[,] King = new float[,]
            {
                {2,3,1,0,0,1,3,2 },
                {2,2,0,0,0,0,2,2 },
                {-1,-2,-2,-2,-2,-2,-2,-1 },
                {-2,-3,-3,-4,-4,-3,-3,-1 },
                {-3,-4,-4,-5,-5,-4,-4,-1},
                {-3,-4,-4,-5,-5,-4,-4,-1},
                {-3,-4,-4,-5,-5,-4,-4,-1},
                {-3,-4,-4,-5,-5,-4,-4,-1},
            };
    }
}
