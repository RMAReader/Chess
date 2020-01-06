using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Test
{
    [TestClass]
    public class GameTreeTests
    {
        [TestMethod]
        public void test1()
        {
          

        }

        [TestMethod]
        public void test2_timings()
        {
            int maxDepth = 10;

            var gameTree = new GameTree();

            var results = new List<string>();

            //for(int depth = 1; depth < maxDepth; depth++)
            //{
            //    var sw = System.Diagnostics.Stopwatch.StartNew();

            //    int count = gameTree.IteratePaths(new Node(0), depth).Count();
            //    long time = sw.ElapsedMilliseconds;

            //    results.Add($"depth:{depth}, count:{count}, time:{time}ms");
            //}

            //for (int depth = 1; depth < maxDepth; depth++)
            //{
            //    var sw = System.Diagnostics.Stopwatch.StartNew();

            //    int count = 0;
            //    for(int i=0; i<Math.Pow(30, depth-1); i++)
            //    {
            //        count++;
            //    }
            //    long time = sw.ElapsedMilliseconds;

            //    results.Add($"depth:{depth}, count:{count}, time:{time}ms");
            //}

            
            //for (int depth = 1; depth < maxDepth; depth++)
            //{
            //    var sw = System.Diagnostics.Stopwatch.StartNew();

            //    var r = gameTree.AlphaBeta(new Node(0), depth: depth, alpha: 1, beta: 1, isMaximizingPlayer: true);
            //    long time = sw.ElapsedMilliseconds;

            //    results.Add($"depth:{depth}, value:{r.Value}, time:{time}ms");
            //}

            //for (int depth = 1; depth < maxDepth; depth++)
            //{
            //    var sw = System.Diagnostics.Stopwatch.StartNew();

            //    var r = gameTree.AlphaBetaRecursive(new Node(0), depth: depth-1, isMaximizingPlayer: true);
            //    long time = sw.ElapsedMilliseconds;

            //    results.Add($"depth:{depth}, value:{r}, time:{time}ms");
            //}
        }

      

        [TestMethod]
        public void test3_alphabetaRecursive()
        {
            var gameTree = new GameTree();

            var initialState = StateFactory.GetStartingState();
            var initialNode = new Node(initialState, new Move());

            var result = gameTree.AlphaBetaRecursive(initialNode, depth: 7, isMaximizingPlayer: true);

        }
    }
}
