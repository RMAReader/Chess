using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Old
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
            var result = new Dictionary<EnumPiece, double>();

            result[EnumPiece.King] = double.MaxValue;
            result[EnumPiece.Queen] = 7.9;
            result[EnumPiece.Rook] = 5;
            result[EnumPiece.Bishop] = 3.3;
            result[EnumPiece.Knight] = 3.1;
            result[EnumPiece.Pawn] = 0.7;

            return result;
        }

        public static double GetStateValue(State state)
        {
            double result = 0;

            result += GetPieceValues(state);
            //result += GetBoardCoverage(state);

            return result;
        }

        //sums piece values
        public static double GetPieceValues(State state)
        {
            double result = 0;
            for (int i = 0; i < state.Ranks; i++)
                for (int j = 0; j < state.Files; j++)
                {
                    if (state.Board[i, j].Player == EnumPlayer.White)
                    {
                        result += _pieceValues[(int)state.Board[i, j].Piece];
                    }
                    else if (state.Board[i, j].Player == EnumPlayer.Black)
                    {
                        result -= _pieceValues[(int)state.Board[i, j].Piece];
                    }
                }

            return result;
        }

        //sums number of squares controlled by player
        public static double GetBoardCoverage(State state)
        {
            return state.IterateMoves(EnumPlayer.White).Count() - state.IterateMoves(EnumPlayer.Black).Count();
        }
    }
}
