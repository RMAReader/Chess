using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Interpreter
    {

        private static readonly char[] cols = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        private static readonly char[] rows = new char[] { '1', '2', '3', '4', '5', '6', '7', '8' };

        public static char RankName(int rankIndex) => rows[rankIndex];
        public static char FileName(int fileIndex) => cols[fileIndex];

        public static bool TryParseColumn(char c, out int col)
        {
            col = -1;
            switch (c)
            {
                case 'a':
                    col = 0;
                    return true;
                case 'b':
                    col = 1;
                    return true;
                case 'c':
                    col = 2;
                    return true;
                case 'd':
                    col = 3;
                    return true;
                case 'e':
                    col = 4;
                    return true;
                case 'f':
                    col = 5;
                    return true;
                case 'g':
                    col = 6;
                    return true;
                case 'h':
                    col = 7;
                    return true;
            }
            return false;
        }
        public static bool TryParseRow(char c, out int row)
        {
            row = -1;
            switch (c)
            {
                case '1':
                    row = 0;
                    return true;
                case '2':
                    row = 1;
                    return true;
                case '3':
                    row = 2;
                    return true;
                case '4':
                    row = 3;
                    return true;
                case '5':
                    row = 4;
                    return true;
                case '6':
                    row = 5;
                    return true;
                case '7':
                    row = 6;
                    return true;
                case '8':
                    row = 7;
                    return true;
            }
            return false;
        }

    }
}
