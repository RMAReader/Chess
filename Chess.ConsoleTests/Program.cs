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
            int maxDepth = 8;
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

            var gameTree2 = new Chess.V2.GameTree();
            var board2 = Chess.V2.Board.Factory.GetDefault();
            
            for (int d = 4; d <= maxDepth; d++)
            {
                var sw = Stopwatch.StartNew();
                var moves = gameTree2.AlphaBetaRecursive(board2, d, true);
                var t1 = sw.ElapsedMilliseconds;

                double dummy = 1;
                sw = Stopwatch.StartNew();
                for (int i=0; i< gameTree2._numberOfLeafEvaluations; i++)
                {
                    dummy *= rnd.NextDouble();
                }
                var t2 = sw.ElapsedMilliseconds;

                Console.WriteLine($"{board2}: " +
                    $"depth={d}, " +
                    $"time={t1}ms, " +
                    $"numerOfEvaluations={gameTree2._numberOfLeafEvaluations}, " +
                    $"numberOfBranchesPruned={gameTree2._numberOfBranchesPruned}, " +
                    $"moves[0]={moves[0].ToString()}, minTime={t2}, {(double)t1/t2}");
            }

            Console.ReadKey();
        }
    }
}
