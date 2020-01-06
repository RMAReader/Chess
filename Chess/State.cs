using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class Constants
    {
        public static BoardPiece EmptyCell = new BoardPiece {Player = EnumPlayer.Undefined };
    }

    public static class StateFactory
    {
        public static State GetStartingState()
        {
            var result = new State();
            result.InitialiseBoard(8, 8);

            result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Rook), 0, 0);
            result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Knight), 0,1);
            result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Bishop), 0, 2);
            result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Queen),0, 3);
            result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.King), 0, 4);
            result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Bishop), 0, 5);
            result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Knight), 0, 6);
            result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Rook), 0, 7);

            for(int i = 0; i < result.Files; i++)
                result.AddPiece(new BoardPiece(EnumPlayer.White, EnumPiece.Pawn), 1, i);

            for (int i = 0; i < result.Files; i++)
                result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.Pawn), 6, i);

            result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.Rook), 7, 0);
            result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.Knight), 7, 1);
            result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.Bishop), 7, 2);
            result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.King), 7, 3);
            result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.Queen), 7, 4);
            result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.Bishop), 7, 5);
            result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.Knight), 7, 6);
            result.AddPiece(new BoardPiece(EnumPlayer.Black, EnumPiece.Rook), 7, 7);

            return result;
        }
    }


    


    public class State
    {
        public BoardPiece[,] Board;
        public int Ranks;
        public int Files;

        public State()
        {
        }

        public State(int ranks, int files)
        {
            InitialiseBoard(ranks, files);
        }

        public State(State other)
        {
            Board = new BoardPiece[other.Ranks, other.Files];
            Ranks = other.Ranks;
            Files = other.Files;
            for (int i = 0; i < Ranks; i++)
                for (int j = 0; j < Files; j++)
                    Board[i, j] = other.Board[i, j];
        }


        public void InitialiseBoard(int ranks, int files)
        {

            Board = new BoardPiece[ranks, files];
            Ranks = ranks;
            Files = files;

            for (int i = 0; i < ranks; i++)
                for (int j = 0; j < files; j++)
                    Board[i, j] = Constants.EmptyCell;
        }

        public void AddPiece(BoardPiece piece, int rank, int file)
        {
            if (Board[rank, file].Player != EnumPlayer.Undefined)
                throw new ArgumentException($"Cannot add piece at[{rank},{file}] as cell is occupied.");

            Board[rank, file] = piece;
        }

        public void RemovePiece(int rank, int file)
        {
            if (Board[rank, file].Player == EnumPlayer.Undefined)
                throw new ArgumentException($"Cannot remove piece at[{rank},{file}] as cell is empty.");

            Board[rank, file].Player = EnumPlayer.Undefined;
        }


        public void MovePieceUnchecked(int fromRank, int fromFile, int toRank, int toFile)
        {
            if (Board[fromRank, fromFile].Player == EnumPlayer.Undefined)
                throw new ArgumentException($"There isn't a piece at [{fromRank},{toRank}]");

            Board[toRank, toFile] = Board[fromRank, fromFile];
            Board[fromRank, fromFile].Player = EnumPlayer.Undefined;
        }


        public List<Position> GetRookMoveOptions(Position current, EnumPlayer player)
        {
            var result = new List<Position>();
            for (int i = current.rank + 1; i < Board.GetLength(0); i++)
            {
                if (Board[i, current.file].Player != EnumPlayer.Undefined)
                    break;

                result.Add(new Position { rank = i, file = current.file });
            }
            for (int i = current.rank - 1; i >= 0; i--)
            {
                if (Board[i, current.file].Player != EnumPlayer.Undefined)
                    break;

                result.Add(new Position { rank = i, file = current.file });
            }
            for (int i = current.file + 1; i < Board.GetLength(1); i++)
            {
                if (Board[current.rank, i].Player != EnumPlayer.Undefined)
                    break;

                result.Add(new Position { rank = current.rank, file = i });
            }
            for (int i = current.file - 1; i >= 0; i--)
            {
                if (Board[current.rank, i].Player != EnumPlayer.Undefined)
                    break;

                result.Add(new Position { rank = current.rank, file = i });
            }
            return result;
        }

        public List<Position> GetBishopMoveOptions(Position current, EnumPlayer player)
        {
            var result = new List<Position>();

            for (int i = current.rank + 1, j = current.file + 1; 
                i < Board.GetLength(0) && j < Board.GetLength(1); i++, j++)
            {
                if (Board[i, j].Player != EnumPlayer.Undefined)
                    break;

                result.Add(new Position { rank = i, file = j });
            }
            for (int i = current.rank + 1, j = current.file - 1;
               i < Board.GetLength(0) && j >= 0; i++, j--)
            {
                if (Board[i, j].Player != EnumPlayer.Undefined)
                    break;

                result.Add(new Position { rank = i, file = j });
            }

            for (int i = current.rank - 1, j = current.file + 1;
                i >= 0 && j < Board.GetLength(1); i--, j++)
            {
                if (Board[i, j].Player != EnumPlayer.Undefined)
                    break;

                result.Add(new Position { rank = i, file = j });
            }
            for (int i = current.rank - 1, j = current.file - 1;
                i >=0 && j >= 0; i--, j--)
            {
                if (Board[i, j].Player != EnumPlayer.Undefined)
                    break;

                result.Add(new Position { rank = i, file = j });
            }
            
            return result;
        }

        public List<Position> GetQueenMoveOptions(Position current, EnumPlayer player)
        {
            var result = new List<Position>();

            result.AddRange(GetRookMoveOptions(current, player));
            result.AddRange(GetBishopMoveOptions(current, player));

            return result;
        }

        public List<Position> GetKnightMoveOptions(Position current, EnumPlayer player)
        {
            var result = new List<Position>();

            int rank, file;

            rank = current.rank + 2;
            if (rank < Ranks)
            {
                file = current.file - 1;
                if (file >= 0 && Board[rank, file].Player != player)
                    result.Add(new Position { rank = rank, file = file });

                file = current.file + 1;
                if (file < Files && Board[rank, file].Player != player)
                    result.Add(new Position { rank = rank, file = file });
            }

            rank = current.rank + 1;
            if (rank < Ranks)
            {
                file = current.file - 2;
                if (file >= 0 && Board[rank, file].Player != player)
                    result.Add(new Position { rank = rank, file = file });

                file = current.file + 2;
                if (file < Files && Board[rank, file].Player != player)
                    result.Add(new Position { rank = rank, file = file });
            }

            rank = current.rank - 1;
            if (rank >= 0)
            {
                file = current.file - 2;
                if (file >= 0 && Board[rank, file].Player != player)
                    result.Add(new Position { rank = rank, file = file });

                file = current.file + 2;
                if (file < Files && Board[rank, file].Player != player)
                    result.Add(new Position { rank = rank, file = file });
            }

            rank = current.rank - 2;
            if (rank >= 0)
            {
                file = current.file - 1;
                if (file >= 0)
                    result.Add(new Position { rank = rank, file = file });

                file = current.file + 1;
                if (file < Files && Board[rank, file].Player != player)
                    result.Add(new Position { rank = rank, file = file });
            }

            return result;
        }
        public List<Position> GetPawnMoveOptions(Position current, EnumPlayer player)
        {
            var result = new List<Position>();
            if (player == EnumPlayer.White)
            {
                var rank = current.rank + 1;
                if (rank < Ranks)
                {
                    if (Board[rank, current.file].Player == EnumPlayer.Undefined)
                    {
                        result.Add(new Position { rank = rank, file = current.file });
                    }

                    var file = current.file - 1;
                    if (file >= 0 && Board[rank, current.file].Player != player && Board[rank, current.file].Player != EnumPlayer.Undefined)
                    {
                        result.Add(new Position { rank = rank, file = file });
                    }

                    file = current.file + 1;
                    if (file < Files && Board[rank, current.file].Player != player && Board[rank, current.file].Player != EnumPlayer.Undefined)
                    {
                        result.Add(new Position { rank = rank, file = file });
                    }
                }

                rank += 1;
                if (rank < Ranks && Board[rank, current.file].Player == EnumPlayer.Undefined)
                { 
                    result.Add(new Position { rank = rank, file = current.file });
                }
            }
            else
            {
                var rank = current.rank - 1;
                if (rank >=0)
                {
                    if (Board[rank, current.file].Player == EnumPlayer.Undefined)
                    {
                        result.Add(new Position { rank = rank, file = current.file });
                    }

                    var file = current.file - 1;
                    if (file >= 0 && Board[rank, current.file].Player != player && Board[rank, current.file].Player != EnumPlayer.Undefined)
                    {
                        result.Add(new Position { rank = rank, file = file });
                    }

                    file = current.file + 1;
                    if (file < Files && Board[rank, current.file].Player != player && Board[rank, current.file].Player != EnumPlayer.Undefined)
                    {
                        result.Add(new Position { rank = rank, file = file });
                    }
                }

                rank -= 1;
                if (rank >= 0 && Board[rank, current.file].Player == EnumPlayer.Undefined)
                {
                    result.Add(new Position { rank = rank, file = current.file });
                }
            }
            return result;
        }

        public List<Position> GetMoves(Position current, EnumPlayer player, EnumPiece piece)
        {
            switch (piece)
            {
                case EnumPiece.Bishop:
                    return GetBishopMoveOptions(current, player);

                case EnumPiece.King:
                    return new List<Position>();

                case EnumPiece.Knight:
                    return GetKnightMoveOptions(current, player);

                case EnumPiece.Pawn:
                    return GetPawnMoveOptions(current, player);

                case EnumPiece.Queen:
                    return GetQueenMoveOptions(current, player);

                case EnumPiece.Rook:
                    return GetRookMoveOptions(current, player);

                default:
                    return new List<Position>();
            }
        }

        public IEnumerable<Move> IterateMoves(EnumPlayer player)
        {
            for(int i=0; i<Ranks; i++)
            {
                for(int j=0; j<Files; j++)
                {
                    if(Board[i, j].Player == player)
                    {
                        var moves = GetMoves(new Position(i, j), player, Board[i, j].Piece);
                        for(int k = 0; k<moves.Count; k++)
                        {
                            yield return new Move { fromRank = i, fromFile = j, toRank = moves[k].rank, toFile = moves[k].file };
                        }
                    }
                }
            }
        }
        
    }

    


    public enum EnumPiece
    {
        King, Queen, Rook, Knight, Bishop, Pawn, PiecesCount
    }
    public enum EnumPlayer
    {
        White, Black, Undefined
    }
    public struct BoardPiece
    {
        public EnumPiece Piece;
        public EnumPlayer Player;
        public BoardPiece(EnumPlayer player, EnumPiece type)
        {
            Player = player;
            Piece = type;
        }
    }
    public struct Position
    {
        public int rank;
        public int file;
        public Position(int rank, int file)
        {
            this.rank = rank;
            this.file = file;
        }
        public override string ToString()
        {
            return $"({rank},{file})";
        }
    }
    public struct Move
    {
        public int fromRank;
        public int fromFile;
        public int toRank;
        public int toFile;
    }
    public struct Piece
    {
        public EnumPiece Type;
        public EnumPlayer Player;
        public int rank;
        public int file;
    }
}
