using Chess.Old;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Chess Console!");

            //var player = EnumPlayer.Undefined;

            //ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            //do
            //{
            //    Console.Write("Please choose player (w/b):");
            //    keyInfo = Console.ReadKey();
            //    Console.Write(Environment.NewLine);
            //    if (keyInfo.Key == ConsoleKey.W) player = EnumPlayer.White;
            //    if (keyInfo.Key == ConsoleKey.B) player = EnumPlayer.Black;
            //} while (player == EnumPlayer.Undefined);

            var inProgress = true;
            bool isWhiteTurn = true;
            var board = Board.Factory.GetDefault();
            var gameTree = new GameTree();

            var history = new List<Board>() { board };

            while (inProgress)
            {
                if(isWhiteTurn)
                {
                    Console.Write("Make a move:");
                    var input = Console.ReadLine();
                    if(input.Length > 4 && input.Substring(0,5)=="query")
                    {
                        if(Position.TryParse(input.Substring(6, input.Length - 6), out Position p))
                        {
                            Console.WriteLine($"{board.GetBoardSquare(p.rank, p.file)}");
                        }
                    }
                    else if (Chess.Old.Move.TryParse(input, out var move) 
                        && board.GetMovesForPiece(move.fromRank, move.fromFile).Any(x => x.ToIndex == board.GetBoardIndex(move.toRank, move.toFile)))
                    {
                        var newMove = board.GetMovesForPiece(move.fromRank, move.fromFile).First(x => x.ToIndex == board.GetBoardIndex(move.toRank, move.toFile));

                        board = board.MakeMove(newMove);

                        history.Add(board);

                        isWhiteTurn = false;
                    }
                    else
                    {
                        Console.Write("Invalid move! Please try again:");
                    }
                }
                else
                {
                    Console.Write("Computer to move:");

                    var moves = gameTree.AlphaBetaRecursive(board, 6, isWhiteTurn);
                    var move = moves.First();
                    board = board.MakeMove(move);

                    history.Add(board);

                    string moveStr = $"{Interpreter.FileName(board.File(move.FromIndex))}" +
                        $"{Interpreter.RankName(board.Rank(move.FromIndex))}" +
                        $"{Interpreter.FileName(board.File(move.ToIndex))}" +
                        $"{Interpreter.RankName(board.Rank(move.ToIndex))}";

                    Console.Write($"{moveStr}{Environment.NewLine}");

                    isWhiteTurn = true;
                }
            }

            Console.Write("Press any key to exist.");
            Console.ReadKey();
        }


    }
}
