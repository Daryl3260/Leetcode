using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode.ListAndStack
{
    namespace p1
    {
        public class MyCircularQueue
        {
            class Node
            {
                public Node prev { get; set; }
                public Node next { get; set; }
                public int Value { get; set; }
            }

            private int _capacity;
            private int _size;

            private Node _header;

            /** Initialize your data structure here. Set the size of the queue to be k. */
            public MyCircularQueue(int k)
            {
                if (k < 0) k = 0;
                _capacity = k;
                _header = new Node();
                _header.prev = _header;
                _header.next = _header;
                _header.Value = -1;
                _size = 0;
            }

            /** Insert an element into the circular queue. Return true if the operation is successful. */
            public bool EnQueue(int value)
            {
                if (IsFull()) return false;
                _size++;
                var node = new Node {Value = value, prev = _header, next = _header.next};
                node.prev.next = node;
                node.next.prev = node;
                return true;
            }

            /** Delete an element from the circular queue. Return true if the operation is successful. */
            public bool DeQueue()
            {
                if (IsEmpty()) return false;
                _size--;
                var last = _header.prev;
                last.next.prev = last.prev;
                last.prev.next = last.next;
                return true;
            }

            /** Get the front item from the queue. */
            public int Front()
            {
                if (IsEmpty()) return -1;
                else return _header.prev.Value;
            }

            /** Get the last item from the queue. */
            public int Rear()
            {
                if (IsEmpty()) return -1;
                else return _header.next.Value;
            }

            /** Checks whether the circular queue is empty or not. */
            public bool IsEmpty()
            {
                return _size == 0;
            }

            /** Checks whether the circular queue is full or not. */
            public bool IsFull()
            {
                return _size == _capacity;
            }
        }

/**
 * Your MyCircularQueue object will be instantiated and called as such:
 * MyCircularQueue obj = new MyCircularQueue(k);
 * bool param_1 = obj.EnQueue(value);
 * bool param_2 = obj.DeQueue();
 * int param_3 = obj.Front();
 * int param_4 = obj.Rear();
 * bool param_5 = obj.IsEmpty();
 * bool param_6 = obj.IsFull();
 */
    }

    namespace p2
    {
        public class MovingAverage
        {
            private int _capacity;
            private int _sum;

            private Queue<int> _queue;

            /** Initialize your data structure here. */
            public MovingAverage(int size)
            {
                if (size < 0) size = 0;
                _capacity = size;
                _sum = 0;
                _queue = new Queue<int>(size);
            }

            public double Next(int val)
            {
                if (_queue.Count < _capacity)
                {
                    _sum += val;
                    _queue.Enqueue(val);
                    return _sum / ((double) _queue.Count);
                }
                else
                {
                    _sum = _sum + val - _queue.Dequeue();
                    _queue.Enqueue(val);
                    return _sum / ((double) _capacity);
                }
            }
        }

/**
 * Your MovingAverage object will be instantiated and called as such:
 * MovingAverage obj = new MovingAverage(size);
 * double param_1 = obj.Next(val);
 */
    }

    namespace p3
    {
        public class Solution
        {
            struct Node
            {
                public int X { get; set; }
                public int Y { get; set; }
                public int Distance { get; set; }
            }

            public void WallsAndGates(int[][] rooms)
            {
                if (rooms == null || rooms.Length == 0 || rooms[0].Length == 0) return;
                int rows = rooms.Length;
                int cols = rooms[0].Length;
                Queue<Node> queue = new Queue<Node>(rows * cols);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (rooms[i][j] == 0)
                        {
                            queue.Enqueue(new Node {X = i, Y = j, Distance = 0});
                            SearchAndUpdate(rooms, queue);
                        }
                    }
                }
            }

            private void SearchAndUpdate(int[][] rooms, Queue<Node> queue)
            {
                int rows = rooms.Length;
                int cols = rooms[0].Length;
                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    int x = node.X;
                    int y = node.Y;
                    int d = node.Distance;
                    if (d != 0 && (x < 0 || x == rows || y < 0 || y == cols || rooms[x][y] == -1 || rooms[x][y] <= d))
                        continue;
                    else
                    {
                        rooms[x][y] = d;
                        queue.Enqueue(new Node {X = x + 1, Y = y, Distance = d + 1});
                        queue.Enqueue(new Node {X = x - 1, Y = y, Distance = d + 1});
                        queue.Enqueue(new Node {X = x, Y = y + 1, Distance = d + 1});
                        queue.Enqueue(new Node {X = x, Y = y - 1, Distance = d + 1});
                    }
                }
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public int OpenLock(string[] deadends, string target)
            {
                HashSet<string> dds = new HashSet<string>(deadends);
                HashSet<string> visited = new HashSet<string>();
                var queue = new Queue<Node>();
                queue.Enqueue(new Node {word = "0000", distance = 0});
                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    if (dds.Contains(node.word) || visited.Contains(node.word)) continue;
                    if (node.word.Equals(target)) return node.distance;
                    visited.Add(node.word);
                    for (int i = 0; i < 4; i++)
                    {
                        queue.Enqueue(new Node {word = Next(node.word, i), distance = node.distance + 1});
                        queue.Enqueue(new Node {word = Previous(node.word, i), distance = node.distance + 1});
                    }
                }

                return -1;
            }

            struct Node
            {
                public string word { get; set; }
                public int distance { get; set; }
            }

            private string Next(string word, int idx)
            {
                char ch = word[idx];
                char next;
                if (ch == '9')
                {
                    next = '0';
                }
                else
                {
                    next = (char) (ch + 1);
                }

                return word.Substring(0, idx) + next + word.Substring(idx + 1);
            }

            private string Previous(string word, int idx)
            {
                char ch = word[idx];
                char prev;
                prev = ch == '0' ? '9' : (char) (ch - 1);
                return word.Substring(0, idx) + prev + word.Substring(idx + 1);
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public bool IsValid(string s)
            {
                if (string.IsNullOrEmpty(s)) return true;
                var stack = new Stack<char>();
                foreach (char c in s)
                {
                    if (c.IsLeft())
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        if (stack.Count == 0) return false;
                        else
                        {
                            var top = stack.Pop();
                            if (!top.Match(c)) return false;
                        }
                    }
                }

                return stack.Count == 0;
            }




        }

        public static class CharExtension
        {
            public static bool IsLeft(this char ch)
            {
                return ch == '[' || ch == '{' || ch == '(';
            }

            public static bool Match(this char lhs, char rhs)
            {
                return (lhs == '(' && rhs == ')') || (lhs == '[' && rhs == ']') || (lhs == '{' && rhs == '}');
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public int[] DailyTemperatures(int[] T)
            {
                if (T == null || T.Length == 0)
                {
                    return T;
                }
                else if (T.Length == 1)
                {
                    return new[] {0};
                }

                LinkedList<int> list = new LinkedList<int>();
                int len = T.Length;
                Node root = new Node {Val = T[len - 1], Index = len - 1, LeastIndex = len - 1};
                list.AddFirst(0);
                for (int i = len - 2; i > -1; i--)
                {
                    int val = T[i];
                    int idx = i;
                    var nearest = root.Insert(val, idx);
                    if (nearest == null) list.AddFirst(0);
                    else list.AddFirst(nearest.Value - idx);
                }

                return list.ToArray();
            }

            class Node
            {
                public int Val { get; set; }
                public int Index { get; set; }
                public int LeastIndex { get; set; }
                public Node Lc { get; set; }
                public Node Rc { get; set; }

                public int? Insert(int val, int idx) //return the nearest
                {
                    LeastIndex = LeastIndex > idx ? idx : LeastIndex;
                    if (val < Val)
                    {
                        var nearest = Index;
                        if (Rc != null)
                        {
                            nearest = Math.Min(nearest, Rc.LeastIndex);
                        }

                        if (Lc == null)
                        {
                            Lc = new Node {Val = val, Index = idx, LeastIndex = idx};
                            return nearest;
                        }
                        else
                        {
                            var lcInsert = Lc.Insert(val, idx);
                            if (lcInsert == null)
                            {
                                return nearest;
                            }
                            else
                            {
                                return Math.Min(nearest, lcInsert.Value);
                            }
                        }
                    }
                    else if (val == Val)
                    {
                        Index = idx;
                        return Rc?.LeastIndex;
                    }
                    else
                    {
                        if (Rc == null)
                        {
                            Rc = new Node {Val = val, Index = idx, LeastIndex = idx};
                            return null;
                        }
                        else
                        {
                            return Rc.Insert(val, idx);
                        }
                    }
                }
            }
        }
    }

    namespace p6.better
    {
        public class Solution
        {
            public int[] DailyTemperatures(int[] T)
            {
                if (T == null || T.Length == 0) return T;
                else if (T.Length == 1) return new[] {0};
                LinkedList<int> rs = new LinkedList<int>();
                var stack = new Stack<Node>();
                var len = T.Length;
                rs.AddFirst(0);
                stack.Push(new Node {Val = T[len - 1], Index = len - 1});
                for (int i = len - 2; i > -1; i--)
                {
                    int val = T[i];
                    while (stack.Count > 0 && stack.Peek().Val <= val) stack.Pop();
                    if (stack.Count == 0)
                    {
                        rs.AddFirst(0);
                    }
                    else
                    {
                        var top = stack.Peek();
                        rs.AddFirst(top.Index - i);
                    }

                    stack.Push(new Node {Val = val, Index = i});
                }

                return rs.ToArray();
            }

            struct Node
            {
                public int Val { get; set; }
                public int Index { get; set; }
            }
        }
    }

    namespace p7
    {
        public class Node
        {
            public int val;
            public IList<Node> neighbors;

            public Node()
            {
            }

            public Node(int _val, IList<Node> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }


        }

        public class Solution
        {
            public Node CloneGraph(Node node)
            {
                if (node == null) return null;
                Dictionary<Node, int> dict = new Dictionary<Node, int>();
                HashSet<Node> set = new HashSet<Node>();
                List<Node> list = new List<Node>();
                ConstructSet(node, set);
                int idx = 0;
                foreach (var n in set)
                {
                    list.Add(n);
                    dict[n] = idx++;
                }

                List<Node> copyList = new List<Node>(list.Count);
                foreach (var node1 in list)
                {
                    copyList.Add(new Node {val = node1.val, neighbors = new List<Node>()});
                }

                int len = list.Count;
                for (int i = 0; i < len; i++)
                {
                    var newHost = copyList[i];
                    var oldNeighbors = list[i].neighbors;
                    foreach (var oldNeighbor in oldNeighbors)
                    {
                        var newNeighbor = copyList[dict[oldNeighbor]];
                        newHost.neighbors.Add(newNeighbor);
                    }
                }

                return copyList[dict[node]];
            }

            private void ConstructSet(Node node, HashSet<Node> set)
            {
                if (!set.Contains(node))
                {
                    set.Add(node);
                    foreach (var neighbor in node.neighbors)
                    {
                        ConstructSet(neighbor, set);
                    }
                }
            }
        }
    }

    namespace p8
    {
        public class Solution
        {
            public int FindTargetSumWays(int[] nums, int S)
            {
                if (nums == null || nums.Length == 0) return S == 0 ? 1 : 0;
                int count = 0;
                Calc(nums, 0, S, 0, ref count);
                return count;
            }

            private void Calc(int[] nums, int idx, int target, int sum, ref int count)
            {
                if (idx == nums.Length)
                {
                    if (target == sum)
                    {
                        count++;
                    }
                }
                else
                {
                    int val = nums[idx];
                    Calc(nums, idx + 1, target, sum + val, ref count);
                    Calc(nums, idx + 1, target, sum - val, ref count);
                }
            }
        }
    }

    namespace p9
    {
        /**
 * Definition for a binary tree node.
 
 */
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int x)
            {
                val = x;
            }
        }

        public class Solution
        {
            public IList<int> InorderTraversal(TreeNode root)
            {
                List<int> rs = new List<int>();
                Stack<TreeNode> stack = new Stack<TreeNode>();
                TreeNode p = root;
                while (true)
                {
                    while (p != null)
                    {
                        stack.Push(p);
                        p = p.left;
                    }

                    if (stack.Count == 0) break;
                    else
                    {
                        var top = stack.Pop();
                        rs.Add(top.val);
                        if (top.right != null)
                        {
                            p = top.right;
                        }
                    }
                }

                return rs;
            }
        }
    }

    namespace p10
    {
        public class MyQueue
        {
            private Stack<int> _s1;

            private Stack<int> _s2;

            /** Initialize your data structure here. */
            public MyQueue()
            {
                _s1 = new Stack<int>();
                _s2 = new Stack<int>();
            }

            /** Push element x to the back of queue. */
            public void Push(int x)
            {
                _s1.Push(x);
            }

            /** Removes the element from in front of queue and returns that element. */
            public int Pop()
            {
                if (Empty()) return -1;
                if (_s2.Count == 0)
                {
                    while (_s1.Count > 0)
                    {
                        _s2.Push(_s1.Pop());
                    }
                }

                return _s2.Pop();
            }

            /** Get the front element. */
            public int Peek()
            {
                if (Empty()) return -1;
                if (_s2.Count == 0)
                {
                    while (_s1.Count > 0)
                    {
                        _s2.Push(_s1.Pop());
                    }
                }

                return _s2.Peek();
            }

            /** Returns whether the queue is empty. */
            public bool Empty()
            {
                return _s1.Count == 0 && _s2.Count == 0;
            }
        }

/**
 * Your MyQueue object will be instantiated and called as such:
 * MyQueue obj = new MyQueue();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Peek();
 * bool param_4 = obj.Empty();
 */
    }

    namespace p11
    {
        public class MyStack
        {
            private Queue<int> _main;

            private Queue<int> _sub;

            /** Initialize your data structure here. */
            public MyStack()
            {
                _main = new Queue<int>();
                _sub = new Queue<int>();
            }

            /** Push element x onto stack. */
            public void Push(int x)
            {
                _main.Enqueue(x);
            }

            /** Removes the element on top of the stack and returns that element. */
            public int Pop()
            {
                if (_main.Count == 0) return -1;
                while (_main.Count > 1)
                {
                    _sub.Enqueue(_main.Dequeue());
                }

                var rs = _main.Dequeue();
                var tmp = _sub;
                _sub = _main;
                _main = tmp;
                return rs;
            }

            /** Get the top element. */
            public int Top()
            {
                if (_main.Count == 0) return -1;
                while (_main.Count > 1)
                {
                    _sub.Enqueue(_main.Dequeue());
                }

                var rs = _main.Peek();
                _sub.Enqueue(_main.Dequeue());
                var tmp = _sub;
                _sub = _main;
                _main = tmp;
                return rs;
            }

            /** Returns whether the stack is empty. */
            public bool Empty()
            {
                return _main.Count == 0;
            }
        }

/**
 * Your MyStack object will be instantiated and called as such:
 * MyStack obj = new MyStack();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Top();
 * bool param_4 = obj.Empty();
 */
    }

    namespace p12
    {
        public class Solution
        {
            public string DecodeString(string s)
            {
                if (string.IsNullOrEmpty(s)) return "";
                else
                {
                    return DecodeInQuotes(s, 0, s.Length);
                }
            }

            private string DecodeInQuotes(string s, int from, int to) //[)
            {
                if (from == to) return "";
                var builder = new StringBuilder();
                for (int i = from; i < to;)
                {
                    var ch = s[i];
                    if (IsNum(ch))
                    {
                        var start = i;
                        var j = i;
                        while (j < s.Length && IsNum(s[j])) j++;
                        if (j == s.Length) throw new Exception($"j==s.length,s:{s},from:{from},to:{to}");
                        var count = ToInt(s, start, j);
                        var innerStart = j + 1; //[X
                        var innerEnd = FindRightBracket(s, innerStart);
                        i = innerEnd + 1; //]
                        var innerStr = DecodeInQuotes(s, innerStart, innerEnd);
                        for (var k = 0; k < count; k++)
                        {
                            builder.Append(innerStr);
                        }
                    }
                    else
                    {
                        builder.Append(ch);
                        i++;
                    }
                }

                return builder.ToString();
            }

            private int ToInt(string s, int from, int to)
            {
                var sum = 0;
                for (int i = from; i < to; i++)
                {
                    sum *= 10;
                    sum += s[i] - '0';
                }

                return sum;
            }

            private bool IsNum(char ch)
            {
                return '0' <= ch && ch <= '9';
            }

            private int FindRightBracket(string s, int from)
            {
                int extra = 0;
                for (int i = from; i < s.Length; i++)
                {
                    if (s[i] == '[')
                    {
                        extra++;
                    }
                    else if (s[i] == ']')
                    {
                        if (extra > 0) extra--;
                        else return i;
                    }
                }

                throw new Exception($"Not match wish s:{s},from:{from}");
            }
        }
    }

    namespace p13
    {
        public class Solution
        {
            private int _rows;
            private int _cols;
            private int _initColor;
            private bool[][] _visited;

            public int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
            {
                if (image == null || image.Length == 0 || image[0].Length == 0) return image;
                _rows = image.Length;
                _cols = image[0].Length;
                _initColor = image[sr][sc];
                _visited = new bool[_rows][];
                for (int i = 0; i < _rows; i++)
                {
                    _visited[i] = new bool[_cols];
                }

                Stack<Node> stack = new Stack<Node>();
                stack.Push(new Node {X = sr, Y = sc});
                while (stack.Count > 0)
                {
                    var node = stack.Pop();
                    int x = node.X;
                    int y = node.Y;
                    if (x < 0 || x == _rows || y < 0 || y == _cols || _visited[x][y] ||
                        image[x][y] != _initColor) continue;
                    _visited[x][y] = true;
                    image[x][y] = newColor;
                    stack.Push(new Node {X = x - 1, Y = y});
                    stack.Push(new Node {X = x + 1, Y = y});
                    stack.Push(new Node {X = x, Y = y + 1});
                    stack.Push(new Node {X = x, Y = y - 1});
                }

                return image;
            }

            struct Node
            {
                public int X { get; set; }
                public int Y { get; set; }
            }
        }
    }

    namespace p14
    {
        public class Solution
        {
            struct Node
            {
                public int X { get; set; }
                public int Y { get; set; }
                public int Distance { get; set; }
            }

            private Queue<Node> _queue;
            private int[][] _storage;
            private int _rows;
            private int _cols;
            private int[][] _matrix;
            
            public int[][] UpdateMatrix(int[][] matrix)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return matrix;
                _matrix = matrix;
                _rows = matrix.Length;
                _cols = matrix[0].Length;
                _storage = new int[_rows][];
                _queue = new Queue<Node>();
                for (int i = 0; i < _rows; i++)
                {
                    _storage[i]=new int[_cols];
                    for (int j = 0; j < _cols; j++)
                    {
                        _storage[i][j] = -1;
                    }
                }

                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _cols; j++)
                    {
                        _matrix[i][j] = Nearest(i, j);
                    }
                }

                return _matrix;
            }

            private int Nearest(int x, int y)
            {
                if (_storage[x][y] != -1) return _storage[x][y];
                if (_matrix[x][y] == 0)
                {
                    _storage[x][y] = 0;
                    return 0;
                }
                _queue.Clear();
                _queue.Enqueue(new Node{X = x,Y = y,Distance = 0});
                int rs;
                while (true)
                {
                    var node = _queue.Dequeue();
                    if (node.X < 0 || node.X == _rows || node.Y< 0 || node.Y == _cols) continue;
                    var val = _matrix[node.X][node.Y];
                    if (val == 0)
                    {
                        rs = node.Distance;
                        break;
                    }
                    else
                    {
                        _queue.Enqueue(new Node{X = node.X-1,Y = node.Y,Distance = node.Distance+1});
                        _queue.Enqueue(new Node{X = node.X+1,Y = node.Y,Distance = node.Distance+1});
                        _queue.Enqueue(new Node{X = node.X,Y = node.Y+1,Distance = node.Distance+1});
                        _queue.Enqueue(new Node{X = node.X,Y = node.Y-1,Distance = node.Distance+1});
                    }
                }

                _storage[x][y] = rs;
                return rs;
//                if (x < 0 || x == _rows || y < 0 || y == _cols) return -1;
//                if (_storage[x][y] != -1) return _storage[x][y];
//                
//                if (_matrix[x][y] == 0)
//                {
//                    _storage[x][y] = 0;
//                    return 0;
//                }
//                else
//                {
//                    int left = Nearest(x - 1, y);
//                    int right = Nearest(x + 1, y);
//                    int top = Nearest(x, y - 1);
//                    int bottom = Nearest(x, y + 1);
//                    int min = int.MaxValue;
//                    UpdateMin(left,ref min);
//                    UpdateMin(right,ref min);
//                    UpdateMin(top,ref min);
//                    UpdateMin(bottom,ref min);
//                    _storage[x][y] = min+1;
//                    return min+1;
//                }
            }

            private void UpdateMin(int val, ref int min)
            {
                if (val == -1) return;
                else min = Math.Min(val, min);
            }
        }
    }

    namespace p15
    {
        public class Solution
        {
            private bool[] _visited;
            public bool CanVisitAllRooms(IList<IList<int>> rooms)
            {
                int len = rooms.Count;
                _visited = new bool[len];
//                _accessible[0] = true;
                VisitFrom(rooms,0);
                foreach (var elem in _visited)
                {
                    if (elem == false) return false;
                }
                return true;
            }

            private void VisitFrom(IList<IList<int>> rooms,int roomIdx)
            {
                if (_visited[roomIdx]) return;
                _visited[roomIdx] = true;
                IList<int> room = rooms[roomIdx];
                foreach (var next in room)
                {
                    VisitFrom(rooms,next);
                }
            }
        }
    }
}