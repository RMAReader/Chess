using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Old
{

    public class Node
    {
        private static int counter = 0;
        private static Random rnd = new Random();

        public State CurrentState { get; }
        public Move LastMove { get; }
        public double Value { get; set; } = double.NaN;
        public Node Parent { get; private set; }
        public int ChildrenCount { get; private set; }
        public Stack<Node> UnprocessedChildren { get; private set; }

        public Node(State currentState, Move move)
        {
            CurrentState = currentState;
            LastMove = move;
        }
        public Node(State currentState, Move move, Node parent)
        {
            Parent = parent;
            CurrentState = currentState;
            LastMove = move;
        }

        public void CreateChildren(EnumPlayer player)
        {
            if (UnprocessedChildren == null)
            {
                UnprocessedChildren = new Stack<Node>();

                foreach (var move in CurrentState.IterateMoves(player))
                {
                    var nextState = new State(CurrentState);
                    nextState.MovePieceUnchecked(move);
                    UnprocessedChildren.Push(new Node(nextState, move, this));
                }

                //var children = new List<Node>();
                //foreach (var move in CurrentState.IterateMoves(player))
                //{
                //    var nextState = new State(CurrentState);
                //    nextState.MovePieceUnchecked(move);
                //    var newNode = new Node(nextState, move, this);
                //    newNode.Value = newNode.CalculateHeuristicValue();
                //    children.Add(newNode);
                //}
                //ChildrenCount = children.Count;

                //if(player == EnumPlayer.White)
                //{
                //    foreach (var node in children.OrderBy(x => x.Value))
                //        UnprocessedChildren.Push(node);
                //}
                //else
                //{
                //    foreach (var node in children.OrderByDescending(x => x.Value))
                //        UnprocessedChildren.Push(node);
                //}
            }
        }
        public double CalculateHeuristicValue()
        {
            if(double.IsNaN(Value))
                Value = StateEvaluator.GetStateValue(CurrentState);

            return Value;
        }

        public string PathToString()
        {
            var sb = new StringBuilder();
            var steps = new List<String>();
            var node = this;

            while(node != null)
            {
                steps.Add(node.CurrentState.ToString());
                steps.Add($"heuristic = {node.CalculateHeuristicValue():0.0}");
                node = node.Parent;
            }
            steps.Reverse();

            foreach (var step in steps)
                sb.AppendLine(step);

            return sb.ToString();
        }
    }



    public class GameTreeOld
    {
       
        public Node AlphaBetaRecursive(Node node, int depth, bool isMaximizingPlayer)
        {
            double alpha = double.NegativeInfinity;
            double beta = double.PositiveInfinity;

            node.CreateChildren(isMaximizingPlayer ? EnumPlayer.White : EnumPlayer.Black);

            var options = new List<Node>();
            while(node.UnprocessedChildren.Count > 0)
            {
                var currentOption = node.UnprocessedChildren.Pop();
                currentOption.Value = AlphaBetaRecursive(currentOption, depth, alpha, beta, !isMaximizingPlayer);
                options.Add(currentOption);
            }

            if(isMaximizingPlayer)
                return options.OrderByDescending(x => x.Value).First();
            else
                return options.OrderBy(x => x.Value).First();
        }

        private double AlphaBetaRecursive(Node node, int depth, double alpha, double beta, bool isWhite)
        {
            if(depth == 0)
            {
                return node.CalculateHeuristicValue();
            }

            if(isWhite)
            {
                double value = double.NegativeInfinity;
                node.CreateChildren(EnumPlayer.White);

                while(node.UnprocessedChildren.Count > 0)
                {
                    var child = node.UnprocessedChildren.Pop();
                    value = Math.Max(value, AlphaBetaRecursive(child, depth - 1, alpha, beta, false));
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                        break;
                }

                return value;
            }
            else
            {
                double value = double.PositiveInfinity;
                node.CreateChildren(EnumPlayer.Black);

                while (node.UnprocessedChildren.Count > 0)
                {
                    var child = node.UnprocessedChildren.Pop();
                    value = Math.Min(value, AlphaBetaRecursive(child, depth - 1,  alpha,  beta, true));
                    beta = Math.Min(beta, value);
                    if (alpha >= beta)
                        break;
                }
                
                return value;
            }
        }

    }



}
