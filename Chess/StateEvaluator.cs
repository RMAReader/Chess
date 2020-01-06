using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public struct PieceValues
    {
        public double White;
        public double Black;
    }


    public class StateEvaluator
    {

        private static double[] _pieceValues = GetPieceValuesArray();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetPieceValue(EnumPiece piece)
        {
            return _pieceValues[(int)piece];
        }

        public static double[] GetPieceValuesArray()
        {
            var result = new double[(int)EnumPiece.PiecesCount];

            foreach(var item in GetPieceValuesDict())
                result[(int)item.Key] = item.Value;
           
            return result;
        }

        public static Dictionary<EnumPiece, double> GetPieceValuesDict()
        {
            var result = new Dictionary<EnumPiece,double>();

            result[EnumPiece.King] = double.MaxValue;
            result[EnumPiece.Queen] = 7.9;
            result[EnumPiece.Rook] = 5;
            result[EnumPiece.Bishop] = 3.3;
            result[EnumPiece.Knight] = 3.1;
            result[EnumPiece.Pawn] = 0.7;

            return result;
        }

        public PieceValues GetPlayerPieceValues(State state)
        {
            var result = new PieceValues { White = 0, Black = 0 };
            for (int i=0; i< state.Ranks; i++)
                for(int j=0; j < state.Files; j++)
                {
                    if(state.Board[i, j].Player == EnumPlayer.White)
                    {
                        result.White += _pieceValues[(int)state.Board[i, j].Piece];
                    }
                    else if (state.Board[i, j].Player == EnumPlayer.Black)
                    {
                        result.Black += _pieceValues[(int)state.Board[i, j].Piece];
                    }
                }

            return result;
        }
    }
}
