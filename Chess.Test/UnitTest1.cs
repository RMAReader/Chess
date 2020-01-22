using Chess.Old;
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
            board1[board1.Length / 16] = 1;
            board2[board1.Length / 16] = 2;
            
            int itr = 10000000;
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
                var isEqual = Compare1(board1, board2);
            }
            timings.Add(sw.ElapsedMilliseconds);
            sw.Restart();

            int count3 = 0;
            for (int i = 0; i < itr; i++)
            {
                count3 = i;
                
                var isEqual = Compare2(board1, board2);

            }
            timings.Add(sw.ElapsedMilliseconds);
            sw.Restart();

            int count4 = 0;
            for (int i = 0; i < itr; i++)
            {
                count4 = i;

                var isEqual = UnsafeCompare(board1, board2);

            }
            timings.Add(sw.ElapsedMilliseconds);
            sw.Restart();

            int count5 = 0;
            for (int i = 0; i < itr; i++)
            {
                count5 = i;

                var isEqual = UnsafeCompare2(board1, board2);

            }
            timings.Add(sw.ElapsedMilliseconds);
            sw.Restart();

            int count6 = 0;
            Byte[] boards = new byte[itr * 64];
            for (int i = 0; i < itr; i++)
            {
                count6 = i;

                Array.Copy(board1, 0, boards, i * board1.Length, board1.Length);
            }
            timings.Add(sw.ElapsedMilliseconds);
            sw.Restart();
        }

        private bool Compare1(byte[] array1, byte[] array2)
        {
            var s1 = new System.ReadOnlySpan<byte>(array1);
            var s2 = new System.ReadOnlySpan<byte>(array2);
            return s1.SequenceEqual(s2);
        }

        private bool Compare2(byte[] array1, byte[] array2)
        {
            for (int j = 0; j < array1.Length; j++)
            {
                if (array1[j] != array2[j]) return false;
            }
            return true;
        }
        private unsafe bool UnsafeCompare(byte[] a1, byte[] a2)
        {
            fixed (byte* p1 = a1, p2 = a2)
            {
                byte* x1 = p1, x2 = p2;
                int l = a1.Length;
                for (int i = 0; i < l / 8; i++, x1 += 8, x2 += 8)
                    if (*((long*)x1) != *((long*)x2)) return false;
                if ((l & 4) != 0) { if (*((int*)x1) != *((int*)x2)) return false; x1 += 4; x2 += 4; }
                if ((l & 2) != 0) { if (*((short*)x1) != *((short*)x2)) return false; x1 += 2; x2 += 2; }
                if ((l & 1) != 0) if (*((byte*)x1) != *((byte*)x2)) return false;
                return true;
            }
        }
        private unsafe bool UnsafeCompare2(byte[] a1, byte[] a2)
        {
            fixed (byte* p1 = a1, p2 = a2)
            {
                if (*((long*)p1) != *((long*)p2)) return false;
                if (*((long*)(p1 + 8)) != *((long*)(p2 + 8))) return false;
                if (*((long*)(p1 + 16)) != *((long*)(p2 + 16))) return false;
                if (*((long*)(p1 + 24)) != *((long*)(p2 + 24))) return false;
                if (*((long*)(p1 + 32)) != *((long*)(p2 + 32))) return false;
                if (*((long*)(p1 + 40)) != *((long*)(p2 + 40))) return false;
                if (*((long*)(p1 + 48)) != *((long*)(p2 + 48))) return false;
                if (*((long*)(p1 + 56)) != *((long*)(p2 + 56))) return false;

                return true;
            }
        }
    }
}
