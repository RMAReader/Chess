using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public enum EnumPiece : byte
    {
        Pawn = 1,
        Knight = 2,
        Bishop = 3,
        Rook = 4,
        Queen = 5,
        King = 6,
    }

    
    public class MoveLookup
    {
        private readonly Board _board;

        public int[][] North;
        public int[][] NorthEast;
        public int[][] East;
        public int[][] SouthEast;
        public int[][] South;
        public int[][] SouthWest;
        public int[][] West;
        public int[][] NorthWest;

        public int[][] King;
        public int[][] Knight;
        public int[][] WhitePawn;
        public int[][] WhitePawnCapture;
        public int[][] BlackPawn;
        public int[][] BlackPawnCapture;

        public MoveLookup(Board board)
        {
            _board = board;

            CreateStraightPaths();
            CreateKingPaths();
            CreateKnightPaths();
            CreateWhitePawnCapturePaths();
            CreateBlackPawnCapturePaths();
            CreateWhitePawnPaths();
            CreateBlackPawnPaths();
        }

        private void CreateStraightPaths()
        {
            int size = _board.Size;
            North = new int[size][];
            South = new int[size][];
            East = new int[size][];
            West = new int[size][];
            NorthEast = new int[size][];
            NorthWest = new int[size][];
            SouthEast = new int[size][];
            SouthWest = new int[size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);

                    North[pos] = new int[_board.Rows - i - 1];
                    for (int k = 0; k < North[pos].Length; k++)
                        North[pos][k] = _board.GetBoardIndex(i + k + 1, j);

                    South[pos] = new int[i];
                    for (int k = 0; k < South[pos].Length; k++)
                        South[pos][k] = _board.GetBoardIndex(i - k - 1, j);

                    East[pos] = new int[_board.Cols - j - 1];
                    for (int k = 0; k < East[pos].Length; k++)
                        East[pos][k] = _board.GetBoardIndex(i, j + k + 1);

                    West[pos] = new int[j];
                    for (int k = 0; k < West[pos].Length; k++)
                        West[pos][k] = _board.GetBoardIndex(i, j - k - 1);

                    NorthEast[pos] = new int[Math.Min(_board.Rows - i - 1, _board.Cols - j - 1)];
                    for (int k = 0; k < NorthEast[pos].Length; k++)
                        NorthEast[pos][k] = _board.GetBoardIndex(i + k + 1, j + k + 1);

                    NorthWest[pos] = new int[Math.Min(_board.Rows - i - 1, j)];
                    for (int k = 0; k < NorthWest[pos].Length; k++)
                        NorthWest[pos][k] = _board.GetBoardIndex(i + k + 1, j - k - 1);

                    SouthEast[pos] = new int[Math.Min(i, _board.Cols - j - 1)];
                    for (int k = 0; k < SouthEast[pos].Length; k++)
                        SouthEast[pos][k] = _board.GetBoardIndex(i - k - 1, j + k + 1);

                    SouthWest[pos] = new int[Math.Min(i, j)];
                    for (int k = 0; k < SouthWest[pos].Length; k++)
                        SouthWest[pos][k] = _board.GetBoardIndex(i - k - 1, j - k - 1);
                }
            }
        }
        private void CreateKingPaths()
        {
            King = new int[_board.Size][];
        
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    List<int> moves = new List<int>();

                    for(int m = i - 1; m <= i + 1 && 0 <= m && m < _board.Rows; m++)
                    {
                        for(int n = j - 1; n <= j + 1 && 0 <= n && n < _board.Cols; n++)
                        {
                            if(!(m==i && n == j))
                                moves.Add(_board.GetBoardIndex(m, n));
                        }
                    }
                    King[pos] = moves.ToArray();
                }
            }
        }
        private void CreateKnightPaths()
        {
            Knight= new int[_board.Size][];

            var offsets = new List<Tuple<int, int>>
                    {
                        new Tuple<int,int>(1, 2),
                        new Tuple<int,int>(1, -2),
                        new Tuple<int,int>(-1, 2),
                        new Tuple<int,int>(-1, -2),
                        new Tuple<int,int>(2, 1),
                        new Tuple<int,int>(2, -1),
                        new Tuple<int,int>(-2, 1),
                        new Tuple<int,int>(-2, -1),
                    };

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<int>();

                    foreach(var offset in offsets)
                    {
                        int row = i + offset.Item1;
                        int col = j + offset.Item2;

                        if (_board.OnBoard(row, col))
                            moves.Add(_board.GetBoardIndex(row, col));
                    }
                   
                    Knight[pos] = moves.ToArray();
                }
            }
        }
        private void CreateWhitePawnCapturePaths()
        {
            WhitePawnCapture = new int[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<int>();

                    int row = i + 1;
                    int col = j - 1;
                    if (_board.OnBoard(row, col))
                        moves.Add(_board.GetBoardIndex(row, col));

                    col = j + 1;
                    if (_board.OnBoard(row, col))
                        moves.Add(_board.GetBoardIndex(row, col));

                    WhitePawnCapture[pos] = moves.ToArray();
                }
            }
        }
        private void CreateBlackPawnCapturePaths()
        {
            BlackPawnCapture = new int[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<int>();

                    int row = i - 1;
                    int col = j - 1;
                    if (_board.OnBoard(row, col))
                        moves.Add(_board.GetBoardIndex(row, col));

                    col = j + 1;
                    if (_board.OnBoard(row, col))
                        moves.Add(_board.GetBoardIndex(row, col));

                    BlackPawnCapture[pos] = moves.ToArray();
                }
            }
        }
        private void CreateWhitePawnPaths()
        {
            WhitePawn = new int[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<int>();

                    int row = i + 1;
                    int col = j;
                    if (_board.OnBoard(row, col))
                        moves.Add(_board.GetBoardIndex(row, col));

                    WhitePawn[pos] = moves.ToArray();
                }
            }
        }
        private void CreateBlackPawnPaths()
        {
            BlackPawn = new int[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<int>();

                    int row = i - 1;
                    int col = j;
                    if (_board.OnBoard(row, col))
                        moves.Add(_board.GetBoardIndex(row, col));

                    BlackPawn[pos] = moves.ToArray();
                }
            }
        }
    }


    public class Board
    {
        private readonly int _rows;
        private readonly int _cols;
        private readonly MoveLookup _moveLookup;

        public Board(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _moveLookup = new MoveLookup(this);
        }

        public int Rows => _rows;
        public int Cols => _cols;
        public int Size => _rows * _cols;
        public MoveLookup MoveLookup => _moveLookup;

        /// <summary>
        /// 1-d array storing board as [a1,a2,a3,a4,a5,a6,a7,a8,0,0,0,0,0,0,0,0,b1,b2,b3,b4 ...] etc
        /// </summary>
        public PieceBase[] Squares;

        public int Rank(int index) { return index / _cols; }
        public int File(int index) { return index % _rows; }



        public int GetBoardIndex(int row, int col)
        {
            if (!OnBoard(row, col))
                throw new ArgumentOutOfRangeException();
            return col + _cols * row;
        }

        public bool OnBoard(int row, int col)
        {
            return 0 <= row && row < _rows && 0 <= col && col < _cols;
        }
    }

    public class PieceBase
    {
        protected static Board _board;
        protected int _position;
        protected List<int> _moves;

        public int MovesCount;
        public EnumPlayer Player;
        public EnumPiece Piece;

        public PieceBase(Board board)
        {
            _board = board;
            _moves = new List<int>();
        }

        public virtual List<int> CalculateNextMoves() { return new List<int>(); }

        protected void AddStraightLine(int step)
        {
            int move = _position + step;
            while (IsEmptySquare(move))
            {
                _moves.Add(move);
                move += step;
            }
            if (IsOpponentPiece(move))
            {
                _moves.Add(move);
            }
        }

        protected bool IsEmptySquare(int position)
        {
            return 0 < position
                    && position < _board.Squares.Length
                    && _board.Squares[position] == null;
        }

        protected bool IsOpponentPiece(int position)
        {
            return 0 < position
                    && position < _board.Squares.Length
                    && _board.Squares[position] != null
                    && _board.Squares[position].Player != this.Player;
        }

        protected bool IsAvailable(int position)
        {
            if (position < 0)
                return false;
            if (position >= _board.Squares.Length)
                return false;
            if (_board.Squares[position] == null)
                return true;
            if (_board.Squares[position].Player != this.Player)
                return true;

            return false;
        }



    }

 


}
