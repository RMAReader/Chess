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

            var player = EnumPlayer.Undefined;

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            do
            {
                Console.Write("Please choose player (w/b):");
                keyInfo = Console.ReadKey();
                Console.Write(Environment.NewLine);
                if (keyInfo.Key == ConsoleKey.W) player = EnumPlayer.White;
                if (keyInfo.Key == ConsoleKey.B) player = EnumPlayer.Black;
            } while (player == EnumPlayer.Undefined);

            var inProgress = true;
            var currentTurn = EnumPlayer.White;
            var state = StateFactory.GetDefaultStartingState();
            var gameTree = new GameTree();

            while (inProgress)
            {
                if(player==currentTurn)
                {
                    Console.Write("Make a move:");
                    var input = Console.ReadLine();
                    if(input.Length > 4 && input.Substring(0,5)=="query")
                    {
                        if(Position.TryParse(input.Substring(6, input.Length - 6), out Position p))
                        {
                            Console.WriteLine($"{state.Board[p.rank, p.file]}");
                        }
                    }
                    else if (Move.TryParse(input, out var move) 
                        && state.IterateMoves(player).Any(x => x.Equals(move)))
                    {
                        state.MovePieceUnchecked(move);

                        currentTurn = (currentTurn == EnumPlayer.White) ? EnumPlayer.Black : EnumPlayer.White;
                    }
                    else
                    {
                        Console.Write("Invalid move! Please try again:");
                    }
                }
                else
                {
                    Console.Write("Computer to move.  Press 'f' to force early move:");

                    var node = new Node(state, new Move());

                    var newNode = gameTree.AlphaBetaRecursive(node, 6, currentTurn == EnumPlayer.White);

                    state = newNode.CurrentState;

                    Console.Write($"{newNode.LastMove.ToString()}{Environment.NewLine}");

                    currentTurn = (currentTurn == EnumPlayer.White) ? EnumPlayer.Black : EnumPlayer.White;
                }
            }

            Console.Write("Press any key to exist.");
            Console.ReadKey();
        }


    }
}
