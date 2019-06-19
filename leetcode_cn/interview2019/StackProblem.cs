using System;
using System.Collections.Generic;

namespace Leetcode.leetcode_cn
{
    namespace problem_stack.p1
    {
        
        public class MinStack
        {
            private Stack<int> nums;

            private Stack<int> min;
            /** initialize your data structure here. */
            public MinStack() {
                nums = new Stack<int>();
                min = new Stack<int>();
            }
    
            public void Push(int x) {
                nums.Push(x);
                if (min.Count == 0 || min.Peek() > x)
                {
                    min.Push(x);
                }
                else
                {
                    min.Push(min.Peek());
                }
            }
    
            public void Pop() {
                if (nums.Count > 0)
                {
                    nums.Pop();
                    min.Pop();
                }
            }
    
            public int Top() {
                if (nums.Count > 0)
                {
                    return nums.Peek();
                }
                else
                {
                    throw new Exception("Try peek on an empty stack");
                }
            }
    
            public int GetMin() {
                if (nums.Count > 0)
                {
                    return min.Peek();
                }
                else
                {
                    throw new Exception("Try peek on an empty stack");
                }
            }
        }

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(x);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */    
    }
    
    namespace problem_stack.p2
    {
        public class Solution {
            public int KthSmallest(int[][] matrix, int k)
            {
                int rows = matrix.Length;
                int cols = matrix[0].Length;
                SortedSet<Entity> sortedSet = new SortedSet<Entity>();
                sortedSet.Add(new Entity()
                {
                    X = 0, Y = 0, Val = matrix[0][0]
                });
                int? now=null;
                while (k > 0)
                {
                    var iter = sortedSet.GetEnumerator();
                    iter.MoveNext();
                    var entity = iter.Current;
                    now = entity.Val;
                    int x = entity.X;
                    int y = entity.Y;
                    if (x+1 < rows)
                    {
                        sortedSet.Add(new Entity()
                        {
                            X = x + 1, Y = y, Val = matrix[x + 1][y]
                        });
                    }

                    if (y+1 < cols)
                    {
                        sortedSet.Add(new Entity()
                        {
                            X = x, Y = y + 1, Val = matrix[x][y + 1]
                        });
                    }

                    sortedSet.Remove(entity);
                    k--;
                }

                if (now.HasValue)
                {
                    return now.Value;
                }
                else
                {
                    throw new Exception("now not initialized.");
                }
            }

            public struct Entity : IComparable<Entity>, IEquatable<Entity>
            {
                public int X { get; set; }
                public int Y { get; set; }
                public int Val { get; set; }

                public int CompareTo(Entity other)
                {
                    if (Equals(other)) return 0;
                    else
                    {
                        if (Val != other.Val) return Val - other.Val;
                        else
                        {
                            if (X != other.X)
                            {
                                return X - other.X;
                            }
                            else
                            {
                                return Y - other.Y;
                            }
                        }
                    }
                }

                public bool Equals(Entity other)
                {
                    return X == other.X && Y == other.Y;
                }
            }
        }    
    }

    namespace problem_stack.p5
    {
        public class Solution {
            public IList<int> TopKFrequent(int[] nums, int k) {
                Dictionary<int,int> map = new Dictionary<int, int>();
                foreach (var elem in nums)
                {
                    if (map.ContainsKey(elem))
                    {
                        map[elem]++;
                    }
                    else
                    {
                        map[elem] = 0;
                    }
                }
                List<Entry> entries = new List<Entry>();
                foreach (var key in map.Keys)
                {
                    entries.Add(new Entry()
                    {
                        Val = key,Count = map[key]
                    });
                }
                entries.Sort();
                List<int> rs = new List<int>();
                for (int i = 0; i < k; i++)
                {
                    rs.Add(entries[i].Val);
                }

                return rs;
            }
            struct Entry:IComparable<Entry>
            {
                public int Val { get; set; }
                public int Count { get; set; }
                public int CompareTo(Entry other)
                {
                    return -(this.Count - other.Count);
                }
            }
        }
    }

    namespace problem_stack.p6
    {
        public class Solution {
            public int[] MaxSlidingWindow(int[] nums, int k)
            {
                if (nums == null || nums.Length == 0) return nums;
                else if (k == 1) return nums;
                List<int> rs = new List<int>();
                int[] window = new int[k];
                for (int i = 0; i < k; i++)
                {
                    window[i] = nums[i];
                }

                for (int i = k - 1; i > 0; i--)
                {
                    if (window[i] > window[i - 1])
                    {
                        window[i - 1] = window[i];
                    }
                }
                
                LinkedList<int> queue = new LinkedList<int>(window);
                
                //0->k-1
                for (int i = k; i < nums.Length; i++)
                {
                    rs.Add(queue.First.Value);
                    queue.RemoveFirst();
                    queue.AddLast(nums[i]);
                    var node = queue.Last;
                    while (node.Previous!=null&&node.Value>node.Previous.Value)
                    {
                        node.Previous.Value = node.Value;
                        node = node.Previous;
                    }
                }
                rs.Add(queue.First.Value);
                return rs.ToArray();
            }
        }
    }

    namespace problem_stack.p7
    {
        public class Solution {
            public int Calculate(string s)
            {
                int len = s.Length;
                List<char> list = new List<char>();
                for (int i = 0; i < len; i++)
                {
                    if(s[i]!=' ') list.Add(s[i]);
                }

                if (list.Count == 0) return 0;
                Stack<int> nums = new Stack<int>();
                Stack<char> ops = new Stack<char>();
                int j = 0;
                while (j < list.Count)
                {
                    var ch = list[j];
                    if (IsNum(ch))
                    {
                        int start = j;
                        while (j < list.Count && IsNum(list[j])) j++;
                        int num = ToInt(list, start, j);
                        nums.Push(num);
                        if (ops.Count > 0 && (ops.Peek() == '*' || ops.Peek() == '/'))
                        {
                            int b = nums.Pop();
                            int a = nums.Pop();
                            char op = ops.Pop();
                            if (op == '*')
                            {
                                nums.Push(a*b);
                            }
                            else
                            {
                                nums.Push(a/b);
                            }
                        }
                    }
                    else
                    {
                        j++;
                        switch (ch)
                        {
                            case '+':
                            case '-':
                                if (ops.Count > 0)
                                {
                                    var last = ops.Peek();
                                    if (last == '+' || last == '-')
                                    {
                                        ops.Pop();
                                        int b = nums.Pop();
                                        int a = nums.Pop();
                                        if (last == '+')
                                        {
                                            nums.Push(a + b);
                                        }
                                        else
                                        {
                                            nums.Push(a - b);
                                        }
                                    }
                                }
                                ops.Push(ch);
                                break;
                            case '*':
                            case '/':
                                ops.Push(ch);
                                break;
                            default:
                                throw new Exception($"No such operator {ch}");
                        }
                    }
                }

                if (ops.Count > 0)
                {
                    int b = nums.Pop();
                    int a = nums.Pop();
                    char op = ops.Pop();
                    if (op == '+')
                    {
                        nums.Push(a+b);
                    }
                    else if (op == '-')
                    {
                        nums.Push(a-b);
                    }
                    else
                    {
                        throw new Exception($"No such operator {op}");
                    }
                }
                return nums.Pop();
            }
            
            
            
            private int ToInt(List<char> list,int start,int end)
            {
                int sum = 0;
                for (int i = start; i < end; i++)
                {
                    sum *= 10;
                    sum += list[i]-'0';
                }
                return sum;
            }
            private static bool IsNum(char ch)
            {
                return '0' <= ch && ch <= '9';
            }
        }
    }

    namespace problem_stack.p8
    {
        /**
 * // This is the interface that allows for creating nested lists.
 * // You should not implement it, or speculate about its implementation

 */
        public interface NestedInteger {
        
             // @return true if this NestedInteger holds a single integer, rather than a nested list.
             bool IsInteger();
            
             // @return the single integer that this NestedInteger holds, if it holds a single integer
             // Return null if this NestedInteger holds a nested list
            int GetInteger();
            
            // @return the nested list that this NestedInteger holds, if it holds a nested list
            // Return null if this NestedInteger holds a single integer
            IList<NestedInteger> GetList();
        }
        public class NestedIterator
        {
            private List<int> _list;
            private IEnumerator<int> iter;
            public NestedIterator(IList<NestedInteger> nestedList) {
                _list = new List<int>();
                Traverse(nestedList);
                iter = _list.GetEnumerator();
            }

            private void Traverse(IList<NestedInteger> node)
            {
                foreach (var nestedInteger in node)
                {
                    if (nestedInteger.IsInteger())
                    {
                        _list.Add(nestedInteger.GetInteger());
                    }
                    else
                    {
                        Traverse(nestedInteger.GetList());
                    }
                }
            }
            
            
            public bool HasNext()
            {
                return iter.MoveNext();
            }

            public int Next()
            {
                return iter.Current;
            }
        }

/**
 * Your NestedIterator will be called like this:
 * NestedIterator i = new NestedIterator(nestedList);
 * while (i.HasNext()) v[f()] = i.Next();
 */
}

    namespace problem_stack.p9
    {
        public class Solution {
            public int EvalRPN(string[] tokens)
            {
                if (tokens == null || tokens.Length < 1) return 0;
                Stack<int> stack = new Stack<int>();
                foreach (var token in tokens)
                {
                    if (IsOp(token))
                    {
                        int b = stack.Pop();
                        int a = stack.Pop();
                        switch (token)
                        {
                            case "+":
                                stack.Push(a+b);
                                break;
                            case "-":
                                stack.Push(a-b);
                                break;
                            case "*":
                                stack.Push(a*b);
                                break;
                            case "/":
                                stack.Push(a/b);
                                break;
                        }
                    }
                    else
                    {
                        stack.Push(int.Parse(token));
                    }
                }
                return stack.Pop();
            }

            private static bool IsOp(string token)
            {
                return token == "+" || token == "-" || token == "*" || token == "/";
            }
        }
    }
}