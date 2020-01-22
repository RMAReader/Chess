using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxDepth = 10;
            var rnd = new Random();

            var gameTree = new GameTree();
            var board = Board.Factory.GetDefault();

            for (int d = 4; d <= maxDepth; d++)
            {
                var sw = Stopwatch.StartNew();
                var moves = gameTree.AlphaBetaRecursive(board, d, true);
                var t1 = sw.ElapsedMilliseconds;

                double dummy = 1;
                sw = Stopwatch.StartNew();
                for (int i = 0; i < gameTree._numberOfLeafEvaluations; i++)
                {
                    dummy *= rnd.NextDouble();
                }
                var t2 = sw.ElapsedMilliseconds;

                Console.WriteLine($"{board}: " +
                    $"depth={d}, " +
                    $"time={t1}ms, " +
                    $"numerOfEvaluations={gameTree._numberOfLeafEvaluations}, " +
                    $"numberOfBranchesPruned={gameTree._numberOfBranchesPruned}, " +
                    $"moves[0]={moves[0].ToString()}, minTime={t2}, {(double)t1 / t2}");
            }

            //var gameTree2 = new Chess.V2.GameTree();
            //var board2 = Chess.V2.Board.Factory.GetDefault();
            
            //for (int d = 4; d <= maxDepth; d++)
            //{
            //    var sw = Stopwatch.StartNew();
            //    var moves = gameTree2.AlphaBetaRecursive(board2, d, true);
            //    var t1 = sw.ElapsedMilliseconds;

            //    var trueValue = moves.First().CalculateScoresThroughSequence(board2);

            //    double dummy = 1;
            //    sw = Stopwatch.StartNew();
            //    for (int i=0; i < gameTree2._numberOfLeafEvaluations; i++)
            //    {
            //        dummy *= rnd.NextDouble();
            //    }
            //    var t2 = sw.ElapsedMilliseconds;

            //    Console.WriteLine($"{board2}: " +
            //        $"depth={d}, " +
            //        $"time={t1}ms, " +
            //        $"numerOfEvaluations={gameTree2._numberOfLeafEvaluations}, " +
            //        $"numberOfBranchesPruned={gameTree2._numberOfBranchesPruned}, " +
            //        $"moves[0]={moves[0].Move.ToString()}, minTime={t2}, {(double)t1/t2}");
            //}


            var gameTree3 = new Chess.V3.GameTree();
            var board3 = Chess.V3.Board.Factory.GetDefault();

            for (int d = 4; d <= maxDepth; d++)
            {
                var sw = Stopwatch.StartNew();
                V3.MoveSequence alpha = new V3.MoveSequence { Value = float.NegativeInfinity };
                V3.MoveSequence beta = new V3.MoveSequence { Value = float.PositiveInfinity };
                var moves = gameTree3.GetMaximizingSequence(null, new V3.Move(), board3, d, alpha, beta);
                var t1 = sw.ElapsedMilliseconds;

                var trueValue = moves.CalculateScoresThroughSequence(board3);

                double dummy = 1;
                sw = Stopwatch.StartNew();
                for (int i = 0; i < gameTree3._numberOfLeafEvaluations; i++)
                {
                    dummy *= rnd.NextDouble();
                }
                var t2 = sw.ElapsedMilliseconds;

                Console.WriteLine($"{board3}: " +
                    $"depth={d}, " +
                    $"time={t1}ms, " +
                    $"numerOfEvaluations={gameTree3._numberOfLeafEvaluations}, " +
                    $"numberOfBranchesPruned={gameTree3._numberOfBranchesPruned}, " +
                    $"moves[0]={moves.Move.ToString()}, minTime={t2}, {(double)t1 / t2}");
            }

         

            Console.ReadKey();


        }
    }
}
