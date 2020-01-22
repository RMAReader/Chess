using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chess.V4
{

    public class ScoreLookup
    {
        private readonly Board _board;

        public float[][] PieceSquareValue;

        public ScoreLookup(Board board)
        {
            _board = board;

            CreatePieceValues();
        }

        private void CreatePieceValues()
        {
            PieceSquareValue = new float[256][];

            PieceSquareValue[(byte)(EnumBoardSquare.Empty)] = new float[_board.Size];

            PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Pawn)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Knight)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Bishop)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Rook)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Queen)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.King)] = new float[_board.Size];

            PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Pawn)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Knight)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Bishop)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Rook)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Queen)] = new float[_board.Size];
            PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.King)] = new float[_board.Size];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);

                    PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Pawn)][pos] = WhitePieceSquareValueConstants.Pawn[i, j] + PieceValueCOnstants.Pawn;
                    PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Knight)][pos] = WhitePieceSquareValueConstants.Knight[i, j] + PieceValueCOnstants.Knight;
                    PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Bishop)][pos] = WhitePieceSquareValueConstants.Bishop[i, j] + PieceValueCOnstants.Bishop;
                    PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Rook)][pos] = WhitePieceSquareValueConstants.Rook[i, j] + PieceValueCOnstants.Rook;
                    PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.Queen)][pos] = WhitePieceSquareValueConstants.Queen[i, j] + PieceValueCOnstants.Queen;
                    PieceSquareValue[(byte)(EnumBoardSquare.White | EnumBoardSquare.King)][pos] = WhitePieceSquareValueConstants.King[i, j] + PieceValueCOnstants.King;

                    pos = _board.GetBoardIndex(_board.Rows - i - 1, j);

                    PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Pawn)][pos] = -WhitePieceSquareValueConstants.Pawn[i, j] - PieceValueCOnstants.Pawn;
                    PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Knight)][pos] = -WhitePieceSquareValueConstants.Knight[i, j] - PieceValueCOnstants.Knight;
                    PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Bishop)][pos] = -WhitePieceSquareValueConstants.Bishop[i, j] - PieceValueCOnstants.Bishop;
                    PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Rook)][pos] = -WhitePieceSquareValueConstants.Rook[i, j] - PieceValueCOnstants.Rook;
                    PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.Queen)][pos] = -WhitePieceSquareValueConstants.Queen[i, j] - PieceValueCOnstants.Queen;
                    PieceSquareValue[(byte)(EnumBoardSquare.Black | EnumBoardSquare.King)][pos] = -WhitePieceSquareValueConstants.King[i, j] - PieceValueCOnstants.King;
                }
            }

            for (int i = 0; i < PieceSquareValue.Length; i++)
            {
                if (PieceSquareValue[i] != null)
                {
                    var notMovedEnum = ((EnumBoardSquare)i) | EnumBoardSquare.NotMoved;
                    PieceSquareValue[(byte)notMovedEnum] = PieceSquareValue[i];
                }
            }
        }
    }

    public class MoveLookup
    {
        private readonly Board _board;

        public byte[][] North;
        public byte[][] NorthEast;
        public byte[][] East;
        public byte[][] SouthEast;
        public byte[][] South;
        public byte[][] SouthWest;
        public byte[][] West;
        public byte[][] NorthWest;

        public byte[][] King;
        public byte[][] Knight;
        public byte[][] WhitePawn;
        public byte[][] WhitePawnCapture;
        public byte[][] BlackPawn;
        public byte[][] BlackPawnCapture;

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
            North = new byte[size][];
            South = new byte[size][];
            East = new byte[size][];
            West = new byte[size][];
            NorthEast = new byte[size][];
            NorthWest = new byte[size][];
            SouthEast = new byte[size][];
            SouthWest = new byte[size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);

                    North[pos] = new byte[_board.Rows - i - 1];
                    for (int k = 0; k < North[pos].Length; k++)
                        North[pos][k] = _board.GetBoardIndex(i + k + 1, j);

                    South[pos] = new byte[i];
                    for (int k = 0; k < South[pos].Length; k++)
                        South[pos][k] = _board.GetBoardIndex(i - k - 1, j);

                    East[pos] = new byte[_board.Cols - j - 1];
                    for (int k = 0; k < East[pos].Length; k++)
                        East[pos][k] = _board.GetBoardIndex(i, j + k + 1);

                    West[pos] = new byte[j];
                    for (int k = 0; k < West[pos].Length; k++)
                        West[pos][k] = _board.GetBoardIndex(i, j - k - 1);

                    NorthEast[pos] = new byte[Math.Min(_board.Rows - i - 1, _board.Cols - j - 1)];
                    for (int k = 0; k < NorthEast[pos].Length; k++)
                        NorthEast[pos][k] = _board.GetBoardIndex(i + k + 1, j + k + 1);

                    NorthWest[pos] = new byte[Math.Min(_board.Rows - i - 1, j)];
                    for (int k = 0; k < NorthWest[pos].Length; k++)
                        NorthWest[pos][k] = _board.GetBoardIndex(i + k + 1, j - k - 1);

                    SouthEast[pos] = new byte[Math.Min(i, _board.Cols - j - 1)];
                    for (int k = 0; k < SouthEast[pos].Length; k++)
                        SouthEast[pos][k] = _board.GetBoardIndex(i - k - 1, j + k + 1);

                    SouthWest[pos] = new byte[Math.Min(i, j)];
                    for (int k = 0; k < SouthWest[pos].Length; k++)
                        SouthWest[pos][k] = _board.GetBoardIndex(i - k - 1, j - k - 1);
                }
            }
        }
        private void CreateKingPaths()
        {
            King = new byte[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    List<byte> moves = new List<byte>();

                    for (int m = i - 1; m <= i + 1; m++)
                    {
                        for (int n = j - 1; n <= j + 1; n++)
                        {
                            if (_board.OnBoard(m, n) && _board.GetBoardIndex(m, n) != pos)
                                moves.Add(_board.GetBoardIndex(m, n));
                        }
                    }
                    King[pos] = moves.ToArray();
                }
            }
        }
        private void CreateKnightPaths()
        {
            Knight = new byte[_board.Size][];

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
                    var moves = new List<byte>();

                    foreach (var offset in offsets)
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
            WhitePawnCapture = new byte[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<byte>();

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
            BlackPawnCapture = new byte[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<byte>();

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
            WhitePawn = new byte[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<byte>();

                    if (_board.OnBoard(i + 1, j))
                        moves.Add(_board.GetBoardIndex(i + 1, j));

                    if (i == 1 && _board.OnBoard(i + 2, j))
                        moves.Add(_board.GetBoardIndex(i + 2, j));

                    WhitePawn[pos] = moves.ToArray();
                }
            }
        }
        private void CreateBlackPawnPaths()
        {
            BlackPawn = new byte[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<byte>();

                    if (_board.OnBoard(i - 1, j))
                        moves.Add(_board.GetBoardIndex(i - 1, j));

                    if (i == _board.Rows - 2 && _board.OnBoard(i - 2, j))
                        moves.Add(_board.GetBoardIndex(i - 2, j));

                    BlackPawn[pos] = moves.ToArray();
                }
            }
        }
    }


    public class Board
    {
        private readonly byte _rows;
        private readonly byte _cols;
        private readonly MoveLookup _moveLookup;
        private readonly ScoreLookup _scoreLookup;
        /// <summary>
        /// 1-d array storing board as [a1,a2,a3,a4,a5,a6,a7,a8,0,0,0,0,0,0,0,0,b1,b2,b3,b4 ...] etc
        /// </summary>
        private EnumBoardSquare[] _squares;
        private float _score;


        public Board(byte rows, byte cols, MoveLookup moveLookup = null, ScoreLookup scoreLookup = null)
        {
            _rows = rows;
            _cols = cols;
            _moveLookup = (moveLookup == null) ? new MoveLookup(this) : moveLookup;
            _scoreLookup = (scoreLookup == null) ? new ScoreLookup(this) : scoreLookup;
            _squares = new EnumBoardSquare[Size];
        }

        public int Rows => _rows;
        public int Cols => _cols;
        public int Size => _rows * _cols;
        public MoveLookup MoveLookup => _moveLookup;
        public float Score => _score;

        public int Rank(int index) { return index / _cols; }
        public int File(int index) { return index % _rows; }

        public EnumBoardSquare GetBoardSquare(int row, int col)
        {
            return _squares[GetBoardIndex(row, col)];
        }

        public byte GetBoardIndex(int row, int col)
        {
            if (!OnBoard(row, col))
                throw new ArgumentOutOfRangeException();
            return (byte)(col + _cols * row);
        }

        public bool OnBoard(int row, int col)
        {
            return 0 <= row && row < _rows && 0 <= col && col < _cols;
        }

        public void RecalculateScore()
        {
            _score = 0;


            for (int i = 0; i < _squares.Length; i++)
            {
                var type = (int)_squares[i];
                _score += _scoreLookup.PieceSquareValue[type][i];
            }

            //var pieceScores = new Dictionary<Tuple<int, EnumBoardSquare>, float>();
            //for (int i = 0; i < _squares.Length; i++)
            //{
            //    var type = (int)_squares[i];
            //    if (_squares[i] != EnumBoardSquare.Empty)
            //    {
            //        var key = new Tuple<int, EnumBoardSquare>(i, _squares[i]);
            //        if (pieceScores.ContainsKey(key))
            //        {
            //            pieceScores[key] = pieceScores[key] + _scoreLookup.PieceSquareValue[type][i];
            //        }
            //        else
            //        {
            //            pieceScores.Add(key, _scoreLookup.PieceSquareValue[type][i]);
            //        }
            //    }
            //}
        }

        public void PlacePiece(int row, int col, EnumBoardSquare piece)
        {
            byte index = GetBoardIndex(row, col);

            _squares[index] = piece;
        }

        public Board MakeMove(Move move)
        {
            var result = new Board(_rows, _cols, _moveLookup, _scoreLookup);

            Array.Copy(_squares, result._squares, _squares.Length);

            result._squares[move.ToIndex] = move.PieceMoved & (~EnumBoardSquare.NotMoved);
            result._squares[move.FromIndex] = EnumBoardSquare.Empty;
            result._score = _score + move.ScoreChange;

            if (move.ToIndex2 != move.FromIndex2)
            {
                result._squares[move.ToIndex2] = result._squares[move.FromIndex2] & (~EnumBoardSquare.NotMoved);
                result._squares[move.FromIndex2] = EnumBoardSquare.Empty;
            }

            return result;
        }

        public Move[] GetMovesForPiece(int row, int col)
        {
            var moves = new List<Move>();

            byte index = GetBoardIndex(row, col);

            GetMovesFromSquare(index, moves);

            return moves.ToArray();
        }

        public Move[] GetMovesForPlayer(EnumBoardSquare player)
        {
            var moves = new List<Move>();

            for (byte i = 0; i < Size; i++)
            {
                if ((_squares[i] & player) == player)
                    GetMovesFromSquare(i, moves);
            }

            return moves.ToArray();
        }

        private void GetMovesFromSquare(byte i, List<Move> moves)
        {
            switch (_squares[i] & (~EnumBoardSquare.NotMoved))
            {
                case EnumBoardSquare.Empty:
                    break;

                case EnumBoardSquare.Pawn | EnumBoardSquare.White:
                    //case EnumBoardSquare.Pawn | EnumBoardSquare.White | EnumBoardSquare.NotMoved:
                    AddWhitePawnMoves(i, moves);
                    break;

                case EnumBoardSquare.Pawn | EnumBoardSquare.Black:
                    //case EnumBoardSquare.Pawn | EnumBoardSquare.Black | EnumBoardSquare.NotMoved:
                    AddBlackPawnMoves(i, moves);
                    break;

                case EnumBoardSquare.Knight | EnumBoardSquare.White:
                case EnumBoardSquare.Knight | EnumBoardSquare.Black:
                    //case EnumBoardSquare.Knight | EnumBoardSquare.White | EnumBoardSquare.NotMoved:
                    //case EnumBoardSquare.Knight | EnumBoardSquare.Black | EnumBoardSquare.NotMoved:

                    AddKnightMoves(i, moves);
                    break;

                case EnumBoardSquare.Bishop | EnumBoardSquare.White:
                case EnumBoardSquare.Bishop | EnumBoardSquare.Black:
                    // case EnumBoardSquare.Bishop | EnumBoardSquare.White | EnumBoardSquare.NotMoved:
                    // case EnumBoardSquare.Bishop | EnumBoardSquare.Black | EnumBoardSquare.NotMoved:

                    AddBishopMoves(i, moves);
                    break;

                case EnumBoardSquare.Rook | EnumBoardSquare.White:
                case EnumBoardSquare.Rook | EnumBoardSquare.Black:
                    //case EnumBoardSquare.Rook | EnumBoardSquare.White | EnumBoardSquare.NotMoved:
                    //case EnumBoardSquare.Rook | EnumBoardSquare.Black | EnumBoardSquare.NotMoved:

                    AddRookMoves(i, moves);
                    break;

                case EnumBoardSquare.Queen | EnumBoardSquare.White:
                case EnumBoardSquare.Queen | EnumBoardSquare.Black:
                    //case EnumBoardSquare.Queen | EnumBoardSquare.White | EnumBoardSquare.NotMoved:
                    //case EnumBoardSquare.Queen | EnumBoardSquare.Black | EnumBoardSquare.NotMoved:

                    AddQueenMoves(i, moves);
                    break;

                case EnumBoardSquare.King | EnumBoardSquare.White:
                case EnumBoardSquare.King | EnumBoardSquare.Black:

                    //    AddKingMoves(i, moves);
                    //    break;

                    //case EnumBoardSquare.King | EnumBoardSquare.White | EnumBoardSquare.NotMoved:
                    //case EnumBoardSquare.King | EnumBoardSquare.Black | EnumBoardSquare.NotMoved:

                    AddKingMoves(i, moves);
                    AddKingCastling(i, moves);
                    break;

                default:
                    throw new Exception();
            }
        }

        private void AddWhitePawnMoves(byte i, List<Move> moves)
        {
            var options = _moveLookup.WhitePawn[i];
            for (byte j = 0; j < options.Length; j++)
            {
                if (IsEmpty(_squares[options[j]]))
                {
                    if (Rank(options[j]) < Rows - 1)
                    {
                        moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], GetValueChange(i, options[j])));
                    }
                    else
                    {
                        moves.Add(new Move(EnumBoardSquare.Queen | EnumBoardSquare.White, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Queen | EnumBoardSquare.White)));
                        moves.Add(new Move(EnumBoardSquare.Rook | EnumBoardSquare.White, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Rook | EnumBoardSquare.White)));
                        moves.Add(new Move(EnumBoardSquare.Bishop | EnumBoardSquare.White, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Bishop | EnumBoardSquare.White)));
                        moves.Add(new Move(EnumBoardSquare.Knight | EnumBoardSquare.White, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Knight | EnumBoardSquare.White)));
                    }
                }
            }
            options = _moveLookup.WhitePawnCapture[i];
            for (byte j = 0; j < options.Length; j++)
            {
                if (IsOpponent(_squares[i], _squares[options[j]]))
                {
                    if (Rank(options[j]) < Rows - 1)
                    {
                        moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], GetValueChange(i, options[j])));
                    }
                    else
                    {
                        moves.Add(new Move(EnumBoardSquare.Queen | EnumBoardSquare.White, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Queen | EnumBoardSquare.White)));
                        moves.Add(new Move(EnumBoardSquare.Rook | EnumBoardSquare.White, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Rook | EnumBoardSquare.White)));
                        moves.Add(new Move(EnumBoardSquare.Bishop | EnumBoardSquare.White, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Bishop | EnumBoardSquare.White)));
                        moves.Add(new Move(EnumBoardSquare.Knight | EnumBoardSquare.White, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Knight | EnumBoardSquare.White)));
                    }
                }
            }
        }

        private void AddBlackPawnMoves(byte i, List<Move> moves)
        {
            var options = _moveLookup.BlackPawn[i];
            for (byte j = 0; j < options.Length; j++)
            {
                if (IsEmpty(_squares[options[j]]))
                {
                    if (Rank(options[j]) > 0)
                    {
                        moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], GetValueChange(i, options[j])));
                    }
                    else
                    {
                        moves.Add(new Move(EnumBoardSquare.Queen | EnumBoardSquare.Black, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Queen | EnumBoardSquare.Black)));
                        moves.Add(new Move(EnumBoardSquare.Rook | EnumBoardSquare.Black, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Rook | EnumBoardSquare.Black)));
                        moves.Add(new Move(EnumBoardSquare.Bishop | EnumBoardSquare.Black, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Bishop | EnumBoardSquare.Black)));
                        moves.Add(new Move(EnumBoardSquare.Knight | EnumBoardSquare.Black, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Knight | EnumBoardSquare.Black)));
                    }
                }
            }
            options = _moveLookup.BlackPawnCapture[i];
            for (byte j = 0; j < options.Length; j++)
            {
                if (IsOpponent(_squares[i], _squares[options[j]]))
                {
                    if (Rank(options[j]) > 0)
                    {
                        moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], GetValueChange(i, options[j])));
                    }
                    else
                    {
                        moves.Add(new Move(EnumBoardSquare.Queen | EnumBoardSquare.Black, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Queen | EnumBoardSquare.Black)));
                        moves.Add(new Move(EnumBoardSquare.Rook | EnumBoardSquare.Black, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Rook | EnumBoardSquare.Black)));
                        moves.Add(new Move(EnumBoardSquare.Bishop | EnumBoardSquare.Black, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Bishop | EnumBoardSquare.Black)));
                        moves.Add(new Move(EnumBoardSquare.Knight | EnumBoardSquare.Black, _squares[options[j]], i, options[j], GetValueChange(i, options[j], EnumBoardSquare.Knight | EnumBoardSquare.Black)));
                    }
                }
            }
        }

        private void AddKnightMoves(byte i, List<Move> moves)
        {
            var options = _moveLookup.Knight[i];
            for (byte j = 0; j < options.Length; j++)
            {
                if (IsOpponentOrEmpty(_squares[i], _squares[options[j]]))
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], GetValueChange(i, options[j])));

            }
        }

        private void AddKingMoves(byte i, List<Move> moves)
        {
            var options = _moveLookup.King[i];
            for (byte j = 0; j < options.Length; j++)
            {
                if (IsOpponentOrEmpty(_squares[i], _squares[options[j]]))
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], GetValueChange(i, options[j])));

            }
        }

        private void AddKingCastling(byte i, List<Move> moves)
        {
            if (HasNotMoved(_squares[i]) && IsEmpty(_squares[i - 1]) && IsEmpty(_squares[i - 2]) && IsEmpty(_squares[i - 3]) && HasNotMoved(_squares[i - 4]))
            {
                float scoreChange = GetValueChange(i, (byte)(i - 2)) + GetValueChange((byte)(i - 4), (byte)(i - 1));
                moves.Add(new Move(_squares[i], _squares[i - 2], i, (byte)(i - 2), (byte)(i - 4), (byte)(i - 1), scoreChange));
            }
            if (HasNotMoved(_squares[i]) && IsEmpty(_squares[i + 1]) && IsEmpty(_squares[i + 2]) && HasNotMoved(_squares[i + 3]))
            {
                float scoreChange = GetValueChange(i, (byte)(i + 2)) + GetValueChange((byte)(i + 3), (byte)(i + 1));
                moves.Add(new Move(_squares[i], _squares[i + 2], i, (byte)(i + 2), (byte)(i + 3), (byte)(i + 1), scoreChange));
            }
        }

        private void AddBishopMoves(byte i, List<Move> moves)
        {
            AddStraightPath(i, _moveLookup.NorthWest[i], moves);
            AddStraightPath(i, _moveLookup.NorthEast[i], moves);
            AddStraightPath(i, _moveLookup.SouthWest[i], moves);
            AddStraightPath(i, _moveLookup.SouthEast[i], moves);
        }

        private void AddRookMoves(byte i, List<Move> moves)
        {
            AddStraightPath(i, _moveLookup.North[i], moves);
            AddStraightPath(i, _moveLookup.East[i], moves);
            AddStraightPath(i, _moveLookup.West[i], moves);
            AddStraightPath(i, _moveLookup.South[i], moves);
        }

        private void AddQueenMoves(byte i, List<Move> moves)
        {
            AddStraightPath(i, _moveLookup.North[i], moves);
            AddStraightPath(i, _moveLookup.East[i], moves);
            AddStraightPath(i, _moveLookup.West[i], moves);
            AddStraightPath(i, _moveLookup.South[i], moves);
            AddStraightPath(i, _moveLookup.NorthWest[i], moves);
            AddStraightPath(i, _moveLookup.NorthEast[i], moves);
            AddStraightPath(i, _moveLookup.SouthWest[i], moves);
            AddStraightPath(i, _moveLookup.SouthEast[i], moves);
        }

        private void AddStraightPath(byte i, byte[] options, List<Move> moves)
        {
            for (byte j = 0; j < options.Length; j++)
            {
                if (IsEmpty(_squares[options[j]]))
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], GetValueChange(i, options[j])));
                }
                else if (IsOpponentOrEmpty(_squares[i], _squares[options[j]]))
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], GetValueChange(i, options[j])));
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        private bool IsOpponentOrEmpty(EnumBoardSquare me, EnumBoardSquare square2)
        {
            return (me & square2 & (EnumBoardSquare.White | EnumBoardSquare.Black)) == EnumBoardSquare.Empty;
        }
        private bool IsEmpty(EnumBoardSquare square)
        {
            return (square & (EnumBoardSquare.White | EnumBoardSquare.Black)) == EnumBoardSquare.Empty;
        }
        private bool IsOpponent(EnumBoardSquare me, EnumBoardSquare square2)
        {
            return ((me | square2) & (EnumBoardSquare.White | EnumBoardSquare.Black)) == (EnumBoardSquare.White | EnumBoardSquare.Black);
        }
        private bool HasNotMoved(EnumBoardSquare piece)
        {
            return (piece & EnumBoardSquare.NotMoved) == EnumBoardSquare.NotMoved;
        }

        private float GetValueChange(byte from, byte to)
        {
            var fromType = (int)_squares[from];
            var toType = (int)_squares[to];

            return _scoreLookup.PieceSquareValue[fromType][to]
                - _scoreLookup.PieceSquareValue[fromType][from]
                - _scoreLookup.PieceSquareValue[toType][to];
        }
        private float GetValueChange(byte from, byte to, EnumBoardSquare newPiece)
        {
            var fromType = (int)_squares[from];
            var newPieceType = (int)newPiece;
            var toType = (int)_squares[to];

            return _scoreLookup.PieceSquareValue[newPieceType][to]
                - _scoreLookup.PieceSquareValue[fromType][from]
                - _scoreLookup.PieceSquareValue[toType][to];
        }

        public static BoardFactory Factory = new BoardFactory();

        public override int GetHashCode()
        {
            //return UnsafeHash(_squares);
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                for (int i = 0; i < _squares.Length; i++)
                {
                    hash = (hash * HashingMultiplier) ^ ((byte)_squares[i]);
                }

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Board;
            if (other != null)
            {
                //return UnsafeCompare(_squares, other._squares);
                for (int i = 0; i < _squares.Length; i++)
                {
                    if (_squares[i] != other._squares[i])
                        return false;
                }
                return true;
            }
            return false;
        }

        private unsafe bool UnsafeCompare(EnumBoardSquare[] a1, EnumBoardSquare[] a2)
        {
            fixed (EnumBoardSquare* p1 = a1, p2 = a2)
            {
                byte* x1 = (byte*)p1, x2 = (byte*)p2;
                int l = a1.Length;
                for (int i = 0; i < l / 8; i++, x1 += 8, x2 += 8)
                    if (*((long*)x1) != *((long*)x2)) return false;
                if ((l & 4) != 0) { if (*((int*)x1) != *((int*)x2)) return false; x1 += 4; x2 += 4; }
                if ((l & 2) != 0) { if (*((short*)x1) != *((short*)x2)) return false; x1 += 2; x2 += 2; }
                if ((l & 1) != 0) if (*((byte*)x1) != *((byte*)x2)) return false;
                return true;
            }
        }

        private unsafe int UnsafeHash(EnumBoardSquare[] a1)
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;
                int hash = HashingBase;

                fixed (EnumBoardSquare* p1 = a1)
                {
                    byte* x1 = (byte*)p1;
                    int l = a1.Length;
                    for (int i = 0; i < l / 4; i++, x1 += 8)
                        hash = (hash * HashingMultiplier) ^ (*((int*)x1));

                    if ((l & 2) != 0)
                    {
                        hash = (hash * HashingMultiplier) ^ (*((short*)x1));
                        x1 += 2;
                    }
                    if ((l & 1) != 0)
                    {
                        hash = (hash * HashingMultiplier) ^ (*((byte*)x1));
                    }
                }
                return hash;
            }
        }
    }


    public struct Move
    {
        public EnumBoardSquare PieceMoved;
        public EnumBoardSquare PieceCaptured;
        public byte FromIndex;
        public byte ToIndex;
        public byte FromIndex2;
        public byte ToIndex2;
        public float ScoreChange;

        public Move(EnumBoardSquare pieceMoved, EnumBoardSquare pieceCaptured, byte fromIndex, byte toIndex, float scoreChange)
        {
            PieceMoved = pieceMoved;
            PieceCaptured = pieceCaptured;
            FromIndex = fromIndex;
            ToIndex = toIndex;
            FromIndex2 = 0;
            ToIndex2 = 0;
            ScoreChange = scoreChange;
        }

        public Move(EnumBoardSquare pieceMoved, EnumBoardSquare pieceCaptured, byte fromIndex, byte toIndex, byte fromIndex2, byte toIndex2, float scoreChange)
        {
            PieceMoved = pieceMoved;
            PieceCaptured = pieceCaptured;
            FromIndex = fromIndex;
            ToIndex = toIndex;
            FromIndex2 = fromIndex2;
            ToIndex2 = toIndex2;
            ScoreChange = scoreChange;
        }

        public override string ToString()
        {
            return $"PieceMoved:{PieceMoved}, PieceCaptured:{PieceCaptured}, From: {FromIndex}, To: {ToIndex}: ScoreChange: {ScoreChange}";
        }
    }


    public class BoardFactory
    {
        public Board GetDefault()
        {
            var result = new Board(8, 8);

            AddWhitePieces(result);
            AddBlackPieces(result);

            for (int i = 0; i < 8; i++)
                result.PlacePiece(1, i, EnumBoardSquare.White | EnumBoardSquare.Pawn | EnumBoardSquare.NotMoved);

            for (int i = 0; i < 8; i++)
                result.PlacePiece(6, i, EnumBoardSquare.Black | EnumBoardSquare.Pawn | EnumBoardSquare.NotMoved);

            return result;
        }

        public Board GetBoardWithPieces()
        {
            var result = new Board(8, 8);

            AddWhitePieces(result);
            AddBlackPieces(result);

            return result;
        }

        private void AddWhitePieces(Board result)
        {
            if (result.Cols != 8)
                throw new Exception();

            result.PlacePiece(0, 0, EnumBoardSquare.White | EnumBoardSquare.Rook | EnumBoardSquare.NotMoved);
            result.PlacePiece(0, 1, EnumBoardSquare.White | EnumBoardSquare.Knight | EnumBoardSquare.NotMoved);
            result.PlacePiece(0, 2, EnumBoardSquare.White | EnumBoardSquare.Bishop | EnumBoardSquare.NotMoved);
            result.PlacePiece(0, 3, EnumBoardSquare.White | EnumBoardSquare.Queen | EnumBoardSquare.NotMoved);
            result.PlacePiece(0, 4, EnumBoardSquare.White | EnumBoardSquare.King | EnumBoardSquare.NotMoved);
            result.PlacePiece(0, 5, EnumBoardSquare.White | EnumBoardSquare.Bishop | EnumBoardSquare.NotMoved);
            result.PlacePiece(0, 6, EnumBoardSquare.White | EnumBoardSquare.Knight | EnumBoardSquare.NotMoved);
            result.PlacePiece(0, 7, EnumBoardSquare.White | EnumBoardSquare.Rook | EnumBoardSquare.NotMoved);
        }
        private void AddBlackPieces(Board result)
        {
            if (result.Cols != 8)
                throw new Exception();

            result.PlacePiece(7, 0, EnumBoardSquare.Black | EnumBoardSquare.Rook | EnumBoardSquare.NotMoved);
            result.PlacePiece(7, 1, EnumBoardSquare.Black | EnumBoardSquare.Knight | EnumBoardSquare.NotMoved);
            result.PlacePiece(7, 2, EnumBoardSquare.Black | EnumBoardSquare.Bishop | EnumBoardSquare.NotMoved);
            result.PlacePiece(7, 3, EnumBoardSquare.Black | EnumBoardSquare.Queen | EnumBoardSquare.NotMoved);
            result.PlacePiece(7, 4, EnumBoardSquare.Black | EnumBoardSquare.King | EnumBoardSquare.NotMoved);
            result.PlacePiece(7, 5, EnumBoardSquare.Black | EnumBoardSquare.Bishop | EnumBoardSquare.NotMoved);
            result.PlacePiece(7, 6, EnumBoardSquare.Black | EnumBoardSquare.Knight | EnumBoardSquare.NotMoved);
            result.PlacePiece(7, 7, EnumBoardSquare.Black | EnumBoardSquare.Rook | EnumBoardSquare.NotMoved);
        }
    }




    public class MoveSequence
    {
        public float Value;
        public Move Move;
        public MoveSequence NextMove;

        public List<float> CalculateScoresThroughSequence(Board board)
        {
            board.RecalculateScore();

            var result = new List<float> { board.Score };

            board = board.MakeMove(Move);
            board.RecalculateScore();

            result.Add(board.Score);

            var nextmove = NextMove;
            while (nextmove != null)
            {
                board = board.MakeMove(nextmove.Move);
                board.RecalculateScore();
                result.Add(board.Score);

                nextmove = nextmove.NextMove;
            }

            return result;
        }

        public int Length
        {
            get
            {
                int length = 1;
                var nextmove = NextMove;
                while (nextmove != null)
                {
                    length++;

                    nextmove = nextmove.NextMove;
                }
                return length;
            }
        }
    }


    public class GameTree
    {
        private Random rnd = new Random();
        public int _numberOfLeafEvaluations;
        public long _durationMilliseconds;
        public int _numberOfBranchesPruned;



        


        public MoveSequence GetMaximizingSequence(Board previousBoard, Move move, Board board, int depth, MoveSequence alpha, MoveSequence beta)
        {
            if (depth == 0 || (move.PieceCaptured & EnumBoardSquare.King) == EnumBoardSquare.King)
            {
                _numberOfLeafEvaluations++;
                return new MoveSequence
                {
                    Value = previousBoard.Score + move.ScoreChange,
                    Move = move,
                    NextMove = null
                };
            }

            if (board == null) board = previousBoard.MakeMove(move);

            var moves = board.GetMovesForPlayer(EnumBoardSquare.White);
            Array.Sort(moves, (x, y) => y.ScoreChange.CompareTo(x.ScoreChange));

            for (int i = 0; i < moves.Length; i++)
            {
                var moveSequence = GetMinimizingSequence(board, moves[i], null, depth - 1, alpha, beta);
                if (alpha.Value < moveSequence.Value)
                {
                    if (moveSequence.Value >= beta.Value)
                    {
                        _numberOfBranchesPruned++;
                        return beta;
                    }
                    else
                    {
                        alpha = new MoveSequence { Move = moveSequence.Move, Value = moveSequence.Value, NextMove = moveSequence };
                    }
                }
            }

            return alpha;
        }


        public MoveSequence GetMinimizingSequence(Board previousBoard, Move move, Board board, int depth, MoveSequence alpha, MoveSequence beta)
        {
            if (depth == 0 || (move.PieceCaptured & EnumBoardSquare.King) == EnumBoardSquare.King)
            {
                _numberOfLeafEvaluations++;
                return new MoveSequence
                {
                    Value = previousBoard.Score + move.ScoreChange,
                    Move = move,
                    NextMove = null
                };
            }

            if (board == null) board = previousBoard.MakeMove(move);

            var moves = board.GetMovesForPlayer(EnumBoardSquare.Black);
            Array.Sort(moves, (x, y) => x.ScoreChange.CompareTo(y.ScoreChange));

            for (int i = 0; i < moves.Length; i++)
            {
                var moveSequence = GetMaximizingSequence(board, moves[i], null, depth - 1, alpha, beta);
                if (moveSequence.Value < beta.Value)
                {
                    if (alpha.Value >= moveSequence.Value)
                    {
                        _numberOfBranchesPruned++;
                        return alpha;
                    }
                    else
                    {
                        beta = new MoveSequence { Move = moveSequence.Move, Value = moveSequence.Value, NextMove = moveSequence };
                    }
                }
            }

            return beta;
        }



        // A Function to generate a 
        // random permutation of arr[] 
        private List<T> Shuffle<T>(List<T> arr)
        {
            for (int i = arr.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
                T temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            return arr;
        }
        private T[] Shuffle<T>(T[] arr)
        {
            for (int i = arr.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
                T temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            return arr;
        }

    }


    public class FixedSizeSortedCollection<TItem>
    {
        private SortedList<float, TItem> _data;
        private int _sizeLimit;

        public FixedSizeSortedCollection(int sizeLimit)
        {
            _sizeLimit = sizeLimit;
        }

        public void Add(float value, TItem item)
        {
            if (_data.Count < _sizeLimit)
            {
                _data.Add(value, item);
            }
            else if (value > _data.Keys[0])
            {
                _data.RemoveAt(0);
                _data.Add(value, item);
            }
        }

    }


}
