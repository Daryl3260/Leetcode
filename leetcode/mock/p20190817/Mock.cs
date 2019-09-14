using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode.mock.p20190817.Mock
{
    namespace p1
    {


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
            public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
            {
                if (root == null || p == null || q == null) return null;
                if (p == q) return p;
                if ((p.val <= root.val && q.val >= root.val) || (p.val >= root.val && q.val <= root.val)) return root;
                if (root.val <= p.val && root.val <= q.val) return LowestCommonAncestor(root.right, p, q);
                else return LowestCommonAncestor(root.left, p, q);
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int CompareVersion(string version1, string version2)
            {
                return Compare(Convert(version1), Convert(version2));
            }

            private int Compare(List<string> v1, List<string> v2)
            {
                var len = Math.Max(v1.Count, v2.Count);
                var v1Count = v1.Count;
                var v2Count = v2.Count;
                for (int i = 0; i < len-v1Count; i++)
                {
                    v1.Add("0");
                }

                for (int i = 0; i < len - v2Count; i++)
                {
                    v2.Add("0");
                }

                
                for (var i = 0; i < len; i++)
                {
                    var str1 = v1[i];
                    var str2 = v2[i];
                    int cp;
                    if (str1.Length != str2.Length)
                    {
                        var strlen = Math.Max(str1.Length, str2.Length);
                        var str1Len = str1.Length;
                        var str2Len = str2.Length;
                        var builder = new StringBuilder();
                        for (var j = 0; j < strlen - str1Len; j++)
                        {
                            builder.Append('0');
                        }

                        str1 = builder.ToString() + str1;
                        builder.Clear();
                        for (var j = 0; j < strlen - str2Len; j++)
                        {
                            builder.Append('0');
                        }

                        str2 = builder.ToString() + str2;
                    }
                    cp = String.Compare(str1, str2);
                    if (cp != 0) return cp;
                }

                return 0;
            }
            private List<string> Convert(string version)
            {
                var arr = version.Split('.');
                var rs = new List<string>();
                foreach (var str in arr)
                {
                    rs.Add(str.TrimStart('0'));
                }
                return rs;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public static void Test()
            {
                var solution = new Solution();
                Console.WriteLine($"{solution.Multiply("123","456")}");
            }
            public string Multiply(string num1, string num2)
            {
                if (num1 == "0" || num2 == "0") return "0";
                else if (num1 == "1") return num2;
                else if (num2 == "1") return num1;
                var sum = "0";
                if (num1.Length < num2.Length)
                {
                    var temp = num1;
                    num1 = num2;
                    num2 = temp;
                }

                for (var i = 0; i < num2.Length; i++)
                {
                    var production = SingleMultiply(num1, num2[num2.Length-1 - i]);
                    sum = Add(sum,ShiftEnd(production, i));
                }
                return sum;
            }

            private string SingleMultiply(string num, char x)
            {
                if (x == '0') return "0";
                else if (x == '1') return num;
                var arr = new List<char>();
                var extra = 0;
                var n2 = x - '0';
                for (var i = num.Length - 1; i > -1; i--)
                {
                    var n1 = num[i] - '0';
                    var production = n1 * n2+extra;
                    extra = production / 10;
                    arr.Add((char)(production%10+'0'));
                }
                if(extra>0)arr.Add((char)('0'+extra));
                var builder = new StringBuilder(arr.Count);
                for (var i = arr.Count - 1; i > -1; i--)
                {
                    builder.Append(arr[i]);
                }
                return builder.ToString().TrimStart('0');
            }

            private string Add(string num1, string num2)
            {
                var len = Math.Max(num1.Length, num2.Length);
                num1 = ShiftStart(num1, len - num1.Length);
                num2 = ShiftStart(num2, len - num2.Length);
                var arr = new List<char>();
                var extra = 0;
                for (var i = len - 1; i > -1; i--)
                {
                    var n1 = num1[i] - '0';
                    var n2 = num2[i] - '0';
                    var sum = n1 + n2 + extra;
                    if (sum > 9)
                    {
                        extra = 1;
                        sum %= 10;
                    }
                    else extra = 0;
                    arr.Add((char)(sum+'0'));
                }
                if(extra>0)arr.Add('1');
                var builder = new StringBuilder(arr.Count);
                for (var i = arr.Count - 1; i > -1; i--)
                {
                    builder.Append(arr[i]);
                }

                return builder.ToString();
            }

            private string ShiftStart(string num, int n)
            {
                var builder = new StringBuilder(n);
                for (int i = 0; i < n; i++)
                {
                    builder.Append('0');
                }
                return builder.ToString() + num;
            }
            private string ShiftEnd(string num, int n)
            {
                if (n == 0) return num;
                var builder = new StringBuilder(n);
                for (int i = 0; i < n; i++)
                {
                    builder.Append('0');
                }
                return num + builder.ToString();
            }
        }
    }

    namespace p4
    {
        public class FreqStack
        {
            /*
             ["FreqStack","push","push","push","push","push","push","pop","push","pop","push","pop","push","pop","push","pop","pop","pop","pop","pop","pop"]
[[],[4],[0],[9],[3],[4],[2],[],[6],[],[1],[],[1],[],[4],[],[],[],[],[],[]]
             */
            public static void Test()
            {
                var stack = new FreqStack();
                var arr = new []{4,0,9,3,4,2};
                foreach (var elem in arr)
                {
                    stack.Push(elem);
                }

                stack.Pop();
                stack.Push(6);
                stack.Pop();
                stack.Push(1);
                stack.Pop();
                stack.Push(1);
                stack.Pop();
                stack.Push(4);
                for (var i = 0; i < 6; i++)
                {
                    stack.Pop();
                }
            }
            private class MyComparer : Comparer<IList<int>>
            {
                public override int Compare(IList<int> x, IList<int> y)
                {
                    var rs = 0;
                    if (x.Count != y.Count) rs = x.Count - y.Count;
                    else
                    {
                        var len = x.Count;
                        rs = x[len - 1] - y[len - 1];
                    }

                    return rs;
                }
            }
            private SortedSet<IList<int>> _list;
            private Dictionary<int, IList<int>> _dict;
            private int _totalCount;
            private MyComparer _comparer;
            public FreqStack()
            {
                _totalCount = 0;
                _list = new SortedSet<IList<int>>(new MyComparer());
                _dict = new Dictionary<int, IList<int>>();
            }
            
            public void Push(int x)
            {
                if (!_dict.ContainsKey(x))
                {
                    var val = new List<int>{x,_totalCount++};
                    _list.Add(val);
                    _dict[x] = val;
                }
                else
                {
                    var val = _dict[x];
                    _list.Remove(val);
                    val.Add(_totalCount++);
                    _list.Add(val);
                }
            }

            public int Pop()
            {
                var top = _list.Max;
                var rs = top[0];
                _list.Remove(top);
                top.RemoveAt(top.Count - 1);
                if(top.Count>1)_list.Add(top);
                else
                {
                    _dict.Remove(rs);
                }
                return rs;
            }
        }
        
}
}

namespace Leetcode.leetcode.mock.p20190817.Mock2
{
    namespace p1
    {
        public class Solution {
            public static void Test()
            {
                Console.WriteLine($"{new Solution().BulbSwitch(10000000)}");
            }
            public int BulbSwitch(int n)
            {
                var bulbs = new bool[n];
                for (var i = 0; i < n; i++)
                {
                    bulbs[i] = true;
                }
                for (var i = 2; i <= n; i++)
                {
                    Toggle(bulbs,i);
                }

                return bulbs.Count(elem => elem);
            }

            private void Toggle(bool[] bulbs, int span)
            {
                var n = bulbs.Length;
                var i = 0;
                while (true)
                {
                    if (i + span > n) break;
                    i += span;
                    bulbs[i - 1] = !bulbs[i - 1];
                }
            }
        }
}

    namespace p1.better
    {
        public class Solution
        {
            public int BulbSwitch(int n)
            {
                var count = 0;
                for (var i = 1; i <= n; i++)
                {
                    if (ShouldTurnOn(i)) count++;
                }

                return count;
            }

            private bool ShouldTurnOn(int n)
            {
                if (n == 1) return true;
                var count = CountYinShu(n);
                return (count & 0x1) == 1;
            }

            private int CountYinShu(int n)
            {
                var count = 0;
                var limit = (int)Math.Sqrt(n);
                for (var i = 2; i < limit; i++)
                {
                    if (n % i == 0) count++;
                }
                return count+2;
            }
        }
    }

    namespace p1.better2
    {
        public class Solution
        {
            public int BulbSwitch(int n)
            {
                var count = 0;
                for (var i = 1; i <= n; i++)
                {
                    if (ShouldTurnOn(i)) count++;
                }

                return count;
            }

            private bool ShouldTurnOn(int n)
            {
                if (n == 1) return true;
                var lo = (int) (Math.Sqrt(n) - 1.0);
                var hi = (int) (Math.Sqrt(n) + 1.0);
                for (var i = lo; i <= hi; i++)
                {
                    if (i * i == n) return true;
                }

                return false;
            }
            
            
        }
}

    namespace p2
    {
        class NumMatrix
        {
            private int[][] _sums;
            public NumMatrix(int[][] matrix)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
                {
                    _sums = null;
                    return;
                }
                
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                _sums = new int[rows][];
                for (var i = 0; i < rows; i++)
                {
                    _sums[i]=new int[cols+1];
                    for (var j = 0; j < cols; j++)
                    {
                        _sums[i][j + 1] = _sums[i][j] + matrix[i][j];
                    }
                }
            }
    
            public int SumRegion(int row1, int col1, int row2, int col2)
            {
                if (_sums == null) return 0;
                var sum = 0;
                for (var i = row1; i <= row2; i++)
                {
                    var s = _sums[i][col2 + 1] - _sums[i][col1];
                    sum += s;
                }
                return sum;
            }
        }

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.sumRegion(row1,col1,row2,col2);
 */
}
}