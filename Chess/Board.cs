using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{

    public class PieceValue
    {
        public float Pawn;
        public float Knight;
        public float Bishop;
        public float Rook;
        public float Queen;
        public float King;
    }

    public class PieceSquareValue
    {
        public float[] Pawn;
        public float[] Knight;
        public float[] Bishop;
        public float[] Rook;
        public float[] Queen;
        public float[] King;
    }

    public class ScoreLookup
    {
        private readonly Board _board;

        public PieceValue WhitePieceValue = new PieceValue
        {
            Pawn = PieceValueCOnstants.Pawn,
            Knight = PieceValueCOnstants.Knight,
            Bishop = PieceValueCOnstants.Bishop,
            Rook = PieceValueCOnstants.Rook,
            Queen = PieceValueCOnstants.Queen,
            King = PieceValueCOnstants.King
        };
        public PieceValue BlackPieceValue = new PieceValue
        {
            Pawn = -PieceValueCOnstants.Pawn,
            Knight = -PieceValueCOnstants.Knight,
            Bishop = -PieceValueCOnstants.Bishop,
            Rook = -PieceValueCOnstants.Rook,
            Queen = -PieceValueCOnstants.Queen,
            King = -PieceValueCOnstants.King
        };
        public PieceSquareValue WhitePieceSquareValue;
        public PieceSquareValue BlackPieceSquareValue;


        public ScoreLookup(Board board)
        {
            _board = board;

            CreateWhitePieceValues();
            CreateBlackPieceValues();
        }

        private void CreateWhitePieceValues()
        {
            WhitePieceSquareValue = new PieceSquareValue
            {
                Pawn = new float[_board.Size],
                Knight = new float[_board.Size],
                Bishop = new float[_board.Size],
                Rook = new float[_board.Size],
                Queen = new float[_board.Size],
                King = new float[_board.Size]
            };
            for(int i=0; i < _board.Rows; i++)
            {
                for(int j=0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    WhitePieceSquareValue.Pawn[pos] = WhitePieceSquareValueConstants.Pawn[i, j];
                    WhitePieceSquareValue.Knight[pos] = WhitePieceSquareValueConstants.Knight[i, j];
                    WhitePieceSquareValue.Bishop[pos] = WhitePieceSquareValueConstants.Bishop[i, j];
                    WhitePieceSquareValue.Rook[pos] = WhitePieceSquareValueConstants.Rook[i, j];
                    WhitePieceSquareValue.Queen[pos] = WhitePieceSquareValueConstants.Queen[i, j];
                    WhitePieceSquareValue.King[pos] = WhitePieceSquareValueConstants.King[i, j];
                }
            }
        }
        private void CreateBlackPieceValues()
        {
            BlackPieceSquareValue = new PieceSquareValue
            {
                Pawn = new float[_board.Size],
                Knight = new float[_board.Size],
                Bishop = new float[_board.Size],
                Rook = new float[_board.Size],
                Queen = new float[_board.Size],
                King = new float[_board.Size]
            };
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(_board.Rows - i - 1, _board.Cols - j - 1);
                    BlackPieceSquareValue.Pawn[pos] = -WhitePieceSquareValueConstants.Pawn[i, j];
                    BlackPieceSquareValue.Knight[pos] = -WhitePieceSquareValueConstants.Knight[i, j];
                    BlackPieceSquareValue.Bishop[pos] = -WhitePieceSquareValueConstants.Bishop[i, j];
                    BlackPieceSquareValue.Rook[pos] = -WhitePieceSquareValueConstants.Rook[i, j];
                    BlackPieceSquareValue.Queen[pos] = -WhitePieceSquareValueConstants.Queen[i, j];
                    BlackPieceSquareValue.King[pos] = -WhitePieceSquareValueConstants.King[i, j];
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

                    for(int m = i - 1; m <= i + 1; m++)
                    {
                        for(int n = j - 1; n <= j + 1; n++)
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
            Knight= new byte[_board.Size][];

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
            BlackPawn = new byte[_board.Size][];

            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int pos = _board.GetBoardIndex(i, j);
                    var moves = new List<byte>();

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

        public int Rank(int index) { return index / _cols; }
        public int File(int index) { return index % _rows; }

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

        public void PlacePiece(int row, int col, EnumBoardSquare piece)
        {
            byte index = GetBoardIndex(row, col);

            _squares[index] = piece;
        }

        public Board MakeMove(Move move)
        {
            var result = new Board(_rows, _cols, _moveLookup);

            Array.Copy(_squares, result._squares, _squares.Length);

            result._squares[move.ToIndex] = move.PieceMoved;
            result._squares[move.FromIndex] = EnumBoardSquare.Empty;
            result._score = _score + move.ScoreChange;

            return result;
        }

        public Move[] GetMovesForPiece(int row, int col)
        {
            var moves = new List<Move>();

            byte index = GetBoardIndex(row, col);

            var currentSquare = _squares[index] & (EnumBoardSquare.White | EnumBoardSquare.Black);

            GetMovesFromSquare(index, currentSquare, moves);
            
            return moves.ToArray();
        }

        public Move[] GetMovesForPlayer(EnumBoardSquare player)
        {
            var moves = new List<Move>();

            for (byte i = 0; i < Size; i++)
            {
                GetMovesFromSquare(i, player, moves);
            }

            return moves.ToArray();
        }

        private void GetMovesFromSquare(byte i, EnumBoardSquare player, List<Move> moves)
        {
            switch (_squares[i])
            {
                case EnumBoardSquare.Empty:
                    break;

                case EnumBoardSquare.Pawn | EnumBoardSquare.White:
                    AddWhitePawnMoves(i, player, moves);
                    break;

                case EnumBoardSquare.Pawn | EnumBoardSquare.Black:
                    AddBlackPawnMoves(i, player, moves);
                    break;

                case EnumBoardSquare.Knight | EnumBoardSquare.White:
                case EnumBoardSquare.Knight | EnumBoardSquare.Black:

                    AddKnightMoves(i, player, moves);
                    break;

                case EnumBoardSquare.Bishop | EnumBoardSquare.White:
                case EnumBoardSquare.Bishop | EnumBoardSquare.Black:

                    AddBishopMoves(i, player, moves);
                    break;

                case EnumBoardSquare.Rook | EnumBoardSquare.White:
                case EnumBoardSquare.Rook | EnumBoardSquare.Black:

                    AddBishopMoves(i, player, moves);
                    break;

                case EnumBoardSquare.Queen | EnumBoardSquare.White:
                case EnumBoardSquare.Queen | EnumBoardSquare.Black:

                    AddQueenMoves(i, player, moves);
                    break;

                case EnumBoardSquare.King | EnumBoardSquare.White:
                case EnumBoardSquare.King | EnumBoardSquare.Black:

                    AddKingMoves(i, player, moves);
                    break;

                default:
                    throw new Exception();
            }
        }

        private void AddWhitePawnMoves(byte i, EnumBoardSquare player, List<Move> moves)
        {
            var options = _moveLookup.WhitePawn[i];
            for (byte j = 0; j < options.Length; j++)
            {
                var currentSquare = _squares[options[j]] & player;
                if (currentSquare == EnumBoardSquare.Empty)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
            }
            options = _moveLookup.WhitePawnCapture[i];
            for (byte j = 0; j < options.Length; j++)
            {
                var currentSquare = _squares[options[j]] & player;
                if (currentSquare != player)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
            }
        }

        private void AddBlackPawnMoves(byte i, EnumBoardSquare player, List<Move> moves)
        {
            var options = _moveLookup.BlackPawn[i];
            for (byte j = 0; j < options.Length; j++)
            {
                var currentSquare = _squares[options[j]] & player;
                if (currentSquare == EnumBoardSquare.Empty)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
            }
            options = _moveLookup.BlackPawnCapture[i];
            for (byte j = 0; j < options.Length; j++)
            {
                var currentSquare = _squares[options[j]] & player;
                if (currentSquare != player)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
            }
        }

        private void AddKnightMoves(byte i, EnumBoardSquare player, List<Move> moves)
        {
            var options = _moveLookup.Knight[i];
            for (byte j = 0; j < options.Length; j++)
            {
                var currentSquare = _squares[options[j]] & player;
                if (currentSquare == EnumBoardSquare.Empty)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
                else if (currentSquare != player)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
            }
        }

        private void AddKingMoves(byte i, EnumBoardSquare player, List<Move> moves)
        {
            var options = _moveLookup.King[i];
            for (byte j = 0; j < options.Length; j++)
            {
                var currentSquare = _squares[options[j]] & player;
                if (currentSquare == EnumBoardSquare.Empty)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
                else if (currentSquare != player)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
            }
        }

        private void AddBishopMoves(byte i, EnumBoardSquare player, List<Move> moves)
        {
            AddStraightPath(i, player, _moveLookup.NorthWest[i], moves);
            AddStraightPath(i, player, _moveLookup.NorthEast[i], moves);
            AddStraightPath(i, player, _moveLookup.SouthWest[i], moves);
            AddStraightPath(i, player, _moveLookup.SouthEast[i], moves);
        }

        private void AddRookMoves(byte i, EnumBoardSquare player, List<Move> moves)
        {
            AddStraightPath(i, player, _moveLookup.North[i], moves);
            AddStraightPath(i, player, _moveLookup.East[i], moves);
            AddStraightPath(i, player, _moveLookup.West[i], moves);
            AddStraightPath(i, player, _moveLookup.South[i], moves);
        }

        private void AddQueenMoves(byte i, EnumBoardSquare player, List<Move> moves)
        {
            AddStraightPath(i, player, _moveLookup.North[i], moves);
            AddStraightPath(i, player, _moveLookup.East[i], moves);
            AddStraightPath(i, player, _moveLookup.West[i], moves);
            AddStraightPath(i, player, _moveLookup.South[i], moves);
            AddStraightPath(i, player, _moveLookup.NorthWest[i], moves);
            AddStraightPath(i, player, _moveLookup.NorthEast[i], moves);
            AddStraightPath(i, player, _moveLookup.SouthWest[i], moves);
            AddStraightPath(i, player, _moveLookup.SouthEast[i], moves);
        }

        private void AddStraightPath(byte i, EnumBoardSquare player, byte[] options, List<Move> moves)
        {
            for (byte j = 0; j < options.Length; j++)
            {
                var currentSquare = _squares[options[j]] & player;
                if (currentSquare == EnumBoardSquare.Empty)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                }
                else if (currentSquare != player)
                {
                    moves.Add(new Move(_squares[i], _squares[options[j]], i, options[j], 0));
                    break;
                }
            }
        }



        public static BoardFactory Factory = new BoardFactory();
    }


    public struct Move
    {
        public EnumBoardSquare PieceMoved;
        public EnumBoardSquare PieceCaptured;
        public byte FromIndex;
        public byte ToIndex;
        public float ScoreChange;

        public Move(EnumBoardSquare pieceMoved, EnumBoardSquare pieceCaptured, byte fromIndex, byte toIndex, float scoreChange)
        {
            PieceMoved = pieceMoved;
            PieceCaptured = pieceCaptured;
            FromIndex = fromIndex;
            ToIndex = toIndex;
            ScoreChange = scoreChange;
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
                result.PlacePiece(1, i, EnumBoardSquare.White | EnumBoardSquare.Pawn);

            for (int i = 0; i < 8; i++)
                result.PlacePiece(6, i, EnumBoardSquare.Black | EnumBoardSquare.Pawn);

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

            result.PlacePiece(0, 0, EnumBoardSquare.White | EnumBoardSquare.Rook);
            result.PlacePiece(0, 1, EnumBoardSquare.White | EnumBoardSquare.Knight);
            result.PlacePiece(0, 2, EnumBoardSquare.White | EnumBoardSquare.Bishop);
            result.PlacePiece(0, 3, EnumBoardSquare.White | EnumBoardSquare.Queen);
            result.PlacePiece(0, 4, EnumBoardSquare.White | EnumBoardSquare.King);
            result.PlacePiece(0, 5, EnumBoardSquare.White | EnumBoardSquare.Bishop);
            result.PlacePiece(0, 6, EnumBoardSquare.White | EnumBoardSquare.Knight);
            result.PlacePiece(0, 7, EnumBoardSquare.White | EnumBoardSquare.Rook);
        }
        private void AddBlackPieces(Board result)
        {
            if (result.Cols != 8)
                throw new Exception();

            result.PlacePiece(7, 0, EnumBoardSquare.Black | EnumBoardSquare.Rook);
            result.PlacePiece(7, 1, EnumBoardSquare.Black | EnumBoardSquare.Knight);
            result.PlacePiece(7, 2, EnumBoardSquare.Black | EnumBoardSquare.Bishop);
            result.PlacePiece(7, 3, EnumBoardSquare.Black | EnumBoardSquare.King);
            result.PlacePiece(7, 4, EnumBoardSquare.Black | EnumBoardSquare.Queen);
            result.PlacePiece(7, 5, EnumBoardSquare.Black | EnumBoardSquare.Bishop);
            result.PlacePiece(7, 6, EnumBoardSquare.Black | EnumBoardSquare.Knight);
            result.PlacePiece(7, 7, EnumBoardSquare.Black | EnumBoardSquare.Rook);
        }
    }



}
