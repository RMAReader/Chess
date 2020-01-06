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

        public int Data { get; }
        public double Value { get; set; } = double.NaN;
        public Node Parent { get; private set; }
        public int ChildrenCount { get; private set; }
        public Stack<Node> UnprocessedChildren { get; private set; }

        public Node(int data)
        {
            Data = data;
        }
        public Node(int data, Node parent)
        {
            Parent = parent;
            Data = data;
        }

        public void CreateChildren()
        {
            if (UnprocessedChildren == null)
            {
                UnprocessedChildren = new Stack<Node>();
                for (int i = 0; i < 30; i++)
                    UnprocessedChildren.Push(new Node(++counter, this));

                ChildrenCount = UnprocessedChildren.Count;
            }
        }
        public double CalculateHeuristicValue()
        {
            return rnd.NextDouble();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}-{1}", Value, Data);
            if(UnprocessedChildren != null)
            {
                sb.AppendFormat("|", Data);
                foreach (var c in UnprocessedChildren)
                {
                    sb.AppendFormat("{0},", c.Data);
                }
            }
            return sb.ToString();
        }

        public string PathToString()
        {
            var sb = new StringBuilder();

            var curr = this;
            while(curr != null)
            {
                if (sb.Length == 0)
                {
                    sb.AppendFormat("{0}", curr.Data);
                }
                else
                {
                    sb.AppendFormat("|{0}", curr.Data);
                }
                curr = curr.Parent;
            }
            return sb.ToString();
        }
    }



    public class GameTree
    {
        public IEnumerable<Node> IteratePaths(Node root, int depth)
        {
            var path = new Stack<Node>();
            path.Push(root);

            while (path.Count > 0)
            {
                var curr = path.Peek();

                if(curr.UnprocessedChildren == null)
                {
                    //need to look deeper...
                    if (path.Count < depth)
                    {
                        curr.CreateChildren();
                        path.Push(curr.UnprocessedChildren.Pop());
                    }
                    //return top of stack item
                    else
                    {
                        var result = path.Pop();
                        yield return result;
                    }
                }
                else
                {
                    //put next child to top path
                    if (curr.UnprocessedChildren.Count > 0)
                    {
                        path.Push(curr.UnprocessedChildren.Pop());
                    }
                    else
                    {
                        path.Pop();
                    }
                }
            }
        }

        public Node AlphaBeta(Node root, int depth, double alpha, double beta, bool isMaximizingPlayer)
        {
            Node output = null;
            var path = new Stack<Node>();
            path.Push(root);

            while (path.Count > 0)
            {
                var curr = path.Peek();

                if (double.IsNaN(curr.Value))
                {
                    //need to look deeper...
                    if (path.Count < depth)
                    {
                        curr.CreateChildren();
                        path.Push(curr.UnprocessedChildren.Pop());
                    }
                    //else calculate heuristic value now...
                    else
                    {
                        curr.Value = curr.CalculateHeuristicValue();
                    }
                }
                //put next child to top of stack...
                else if(curr.UnprocessedChildren != null && curr.UnprocessedChildren.Count > 0)
                {
                    path.Push(curr.UnprocessedChildren.Pop());
                }
                //unwind stack...
                else
                {
                    var result = path.Pop();
                    if(result.Parent == null)
                    {
                        output = result;
                    }
                    else if (double.IsNaN(result.Parent.Value))
                    {
                        result.Parent.Value = result.Value;
                    }
                    else
                    {
                        if (isMaximizingPlayer)
                        {
                            result.Parent.Value = Math.Max(result.Parent.Value, result.Value);
                        }
                        else
                        {
                            result.Parent.Value = Math.Min(result.Parent.Value, result.Value);
                        }
                    }

                }
            }

            return output;
        }

    }
    

}
