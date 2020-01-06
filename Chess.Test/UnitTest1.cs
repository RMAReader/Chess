using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    }
}
