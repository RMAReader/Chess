using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
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

                //UnprocessedChildren = new Stack<Node>();
                //for (int i = 0; i < 30; i++)
                //    UnprocessedChildren.Push(new Node(++counter, this));

                ChildrenCount = UnprocessedChildren.Count;
            }
        }
        public double CalculateHeuristicValue()
        {
            return rnd.NextDouble();
        }

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendFormat("{0}-{1}", Value, Data);
        //    if(UnprocessedChildren != null)
        //    {
        //        sb.AppendFormat("|", Data);
        //        foreach (var c in UnprocessedChildren)
        //        {
        //            sb.AppendFormat("{0},", c.Data);
        //        }
        //    }
        //    return sb.ToString();
        //}

        //public string PathToString()
        //{
        //    var sb = new StringBuilder();

        //    var curr = this;
        //    while(curr != null)
        //    {
        //        if (sb.Length == 0)
        //        {
        //            sb.AppendFormat("{0}", curr.Data);
        //        }
        //        else
        //        {
        //            sb.AppendFormat("|{0}", curr.Data);
        //        }
        //        curr = curr.Parent;
        //    }
        //    return sb.ToString();
        //}
    }



    public class GameTree
    {
        //public IEnumerable<Node> IteratePaths(Node root, int depth)
        //{
        //    var path = new Stack<Node>();
        //    path.Push(root);

        //    while (path.Count > 0)
        //    {
        //        var curr = path.Peek();

        //        if(curr.UnprocessedChildren == null)
        //        {
        //            //need to look deeper...
        //            if (path.Count < depth)
        //            {
        //                curr.CreateChildren();
        //                path.Push(curr.UnprocessedChildren.Pop());
        //            }
        //            //return top of stack item
        //            else
        //            {
        //                var result = path.Pop();
        //                yield return result;
        //            }
        //        }
        //        else
        //        {
        //            //put next child to top path
        //            if (curr.UnprocessedChildren.Count > 0)
        //            {
        //                path.Push(curr.UnprocessedChildren.Pop());
        //            }
        //            else
        //            {
        //                path.Pop();
        //            }
        //        }
        //    }
        //}

    
        public double AlphaBetaRecursive(Node node, int depth, bool isMaximizingPlayer)
        {
            double alpha = double.NegativeInfinity;
            double beta = double.PositiveInfinity;

            return AlphaBetaRecursive(node, depth, alpha, beta, isMaximizingPlayer);
        }

        private double AlphaBetaRecursive(Node node, int depth, double alpha, double beta, bool isMaximizingPlayer)
        {
            if(depth == 0)
            {
                return node.CalculateHeuristicValue();
            }

            if(isMaximizingPlayer)
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
