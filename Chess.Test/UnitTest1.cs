using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Chess.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ArrayVsDictionarySpeed()
        {
            var asArray = StateEvaluator.GetPieceValuesArray();
            var asDict = StateEvaluator.GetPieceValuesDict();

            var sw = System.Diagnostics.Stopwatch.StartNew();

            int maxItr = 10000000;

            double sum1 = 0;
            for(int i=0; i < maxItr; i++)
            {
                EnumPiece j = (EnumPiece)(i % asArray.Length);
                //int j = i % asArray.Length;
                sum1 += asArray[(int)j];
            }

            var t1 = sw.Elapsed.TotalMilliseconds;
            sw.Restart();

            double sum2 = 0;
            for (int i = 0; i < maxItr; i++)
            {
                EnumPiece j = (EnumPiece)(i % asArray.Length);
                sum2 += asDict[j];
            }

            var t2 = sw.Elapsed.TotalMilliseconds;
            sw.Restart();

            double sum3 = 0;
            for (int i = 0; i < maxItr; i++)
            {
                EnumPiece j = (EnumPiece)(i % asArray.Length);
                sum3 += StateEvaluator.GetPieceValue(j);
            }

            var t3 = sw.Elapsed.TotalMilliseconds;
            sw.Restart();
        }

        [TestMethod]
        public void BoardComparisonSpeed()
        {
            var board1 = new byte[64];
            var board2 = new byte[64];
            board1[0] = 1;
            board2[0] = 2;
            
            int itr = 1000000;
            var timings = new List<long>();

            var sw = System.Diagnostics.Stopwatch.StartNew();

            int count1 = 0;
            for (int i=0; i < itr; i++)
            {
                count1 = i;
            }
            timings.Add(sw.ElapsedMilliseconds);
            sw.Restart();

            int count2 = 0;
            for (int i = 0; i < itr; i++)
            {
                count2 = i;
                var s1 = new System.ReadOnlySpan<byte>(board1);
                var s2 = new System.ReadOnlySpan<byte>(board2);
                var isEqual = s1.SequenceEqual(s2);
            }
            timings.Add(sw.ElapsedMilliseconds);
            sw.Restart();

            int count3 = 0;
            for (int i = 0; i < itr; i++)
            {
                count3 = i;
                
                var isEqual = true;
                for(int j=0)
            }
            timings.Add(sw.ElapsedMilliseconds);
            sw.Restart();


        }
    }
}
