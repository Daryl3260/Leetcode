using System;
using System.Collections.Generic;

namespace Leetcode.leetcode.facebook.DP
{
    namespace p1
    {
        public class Solution
        {
            public string LongestPalindrome(string s)
            {
                if (string.IsNullOrEmpty(s)) return string.Empty;
                var maxLeft = 0;
                var maxRight = 0;
                var doubleList = new List<int>();
                for (var i = 0; i < s.Length; i++)
                {
                    if (i != s.Length - 1 && s[i] == s[i + 1])
                    {
                        doubleList.Add(i);
                    }

                    searchFromSingle(s, i, out var left, out var right);
                    if (right - left > maxRight - maxLeft)
                    {
                        maxLeft = left;
                        maxRight = right;
                    }
                }

                foreach (var midL in doubleList)
                {
                    searchFromDouble(s, midL, out var left, out var right);
                    if (right - left > maxRight - maxLeft)
                    {
                        maxLeft = left;
                        maxRight = right;
                    }
                }

                return s.Substring(maxLeft, maxRight - maxLeft + 1);
            }

            private void searchFromSingle(string s, int mid, out int left, out int right)
            {
                var i = 0;
                while (true)
                {
                    if (mid - i < 0 || mid + i == s.Length || s[mid - i] != s[mid + i]) break;
                    i++;
                }

                left = mid - i + 1;
                right = mid + i - 1;
            }

            private void searchFromDouble(string s, int midL, out int left, out int right)
            {
                var i = 0;
                while (true)
                {
                    var l = midL - i;
                    var r = midL + 1 + i;
                    if (l < 0 || r == s.Length || s[l] != s[r]) break;
                    i++;
                }

                left = midL - i + 1;
                right = midL + 1 + i - 1;
            }

        }
    }

    namespace p2
    {
        public class Solution
        {
            public int LongestValidParentheses(string s)
            {
                if (string.IsNullOrEmpty(s)) return 0;
                var leftStack = new Stack<int>();
                var pairs = new List<Tuple<int, int>>();
                for (var i = 0; i < s.Length; i++)
                {
                    var ch = s[i];
                    if (ch == '(')
                    {
                        leftStack.Push(i);
                    }
                    else if (leftStack.Count > 0)
                    {
                        var leftIdx = leftStack.Pop();
                        var rightIdx = i;
                        pairs.Add(new Tuple<int, int>(leftIdx, rightIdx));
                    }
                }

                var good = new bool[s.Length];
                foreach (var pair in pairs)
                {
                    var left = pair.Item1;
                    var right = pair.Item2;
                    for (var i = left; i <= right; i++)
                    {
                        good[i] = true;
                    }
                }

                var maxLen = 0;
                for (var i = 0; i < good.Length;)
                {
                    if (good[i])
                    {
                        var left = i;
                        while (i < good.Length && good[i]) i++;
                        var right = i;
                        var len = right - left;
                        maxLen = Math.Max(maxLen, len);
                    }
                    else i++;
                }

                return maxLen;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {

            public int NumDecodings(string s)
            {
                if (string.IsNullOrEmpty(s)) return 0;
                var dict = new int[s.Length];
                for (var i = 0; i < dict.Length; i++)
                {
                    dict[i] = -1;
                }

                return SubSearch(s, 0, dict);
            }

            private int SubSearch(string s, int i, int[] dict)
            {
                if (i == s.Length) return 1;
                if (i > s.Length) return 0;
                if (dict[i] != -1) return dict[i];
                var sum = 0;
                if (s[i] != '0')
                {
                    sum += SubSearch(s, i + 1, dict);
                }

                if (i < s.Length - 1)
                {
                    var twoDigit = int.Parse(s.Substring(i, 2));
                    if (9 < twoDigit && twoDigit < 27)
                    {
                        sum += SubSearch(s, i + 2, dict);
                    }
                }

                dict[i] = sum;
                return sum;
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public int MaxProfit(int[] prices)
            {
                if (prices == null || prices.Length < 2) return 0;
                var maxProfit = 0;
                var maxRight = new int[prices.Length];
                var len = prices.Length;
                maxRight[len - 1] = -1;
                for (var i = len - 2; i > -1; i--)
                {
                    maxRight[i] = Math.Max(maxRight[i + 1], prices[i + 1]);
                }

                for (var i = len - 2; i > -1; i--)
                {
                    if (prices[i] < maxRight[i])
                    {
                        var profit = maxRight[i] - prices[i];
                        maxProfit = Math.Max(maxProfit, profit);
                    }
                }

                return maxProfit;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public bool WordBreak(string s, IList<string> wordDict)
            {
                if (string.IsNullOrEmpty(s) || wordDict == null || wordDict.Count == 0) return false;
                var dict = new HashSet<string>(wordDict);
                var traps = new bool[s.Length];
                return SubSearch(s, 0, dict, traps);
            }

            private bool SubSearch(string s, int idx, ISet<string> wordDict, bool[] traps)
            {
                if (idx == s.Length) return true;
                if (traps[idx]) return false;
                var substr = s.Substring(idx);
                foreach (var word in wordDict)
                {
                    if (substr.StartsWith(word))
                    {
                        if (SubSearch(s, idx + word.Length, wordDict, traps)) return true;
                    }
                }

                traps[idx] = true;
                return false;
            }
        }
    }

    namespace p6
    {
        public class NumMatrix
        {

            struct Result
            {
                public Tuple<Point, Point> Rec { get; set; }
                public int Sum { get; set; }
            }

            struct Point
            {
                public int Row { get; set; }
                public int Col { get; set; }
            }

//            List<Result> Results { get; set; }
            private LinkedList<Result> Results;
            public int[][] Matrix { get; set; }

            public NumMatrix(int[][] matrix)
            {
                Matrix = matrix;
                Results = new LinkedList<Result>();
            }

            public int SumRegion(int row1, int col1, int row2, int col2)
            {
                if (Matrix == null || Matrix.Length == 0 || Matrix[0].Length == 0) return 0;
                var rec1 = new Tuple<Point, Point>(new Point {Row = row1, Col = col1},
                    new Point {Row = row2, Col = col2});
                LinkedListNode<Result> maxInner = null;
//                foreach (var result in Results)
//                {
//                    if (Contains(rec1, result.Rec))
//                    {
//                        if (maxInner == null || Contains(result.Rec, maxInner.Value.Rec))
//                        {
//                            maxInner = result;
//                        }
//                    }
//                }
                for (var iter = Results.Last; iter != null && iter.Previous != null; iter = iter.Previous)
                {
                    if (Contains(rec1, iter.Value.Rec))
                    {
                        maxInner = iter;
                        break;
                    }
                }

                if (maxInner != null)
                {
                    var rs = CalcLeftOff(maxInner, rec1);
                    return rs;
                }
                else
                {
                    var rs = CalcAll(rec1);
                    Results.AddLast(new Result {Sum = rs, Rec = rec1});
                    return rs;
                }
            }

            private int CalcAll(Tuple<Point, Point> rec)
            {
                var sum = 0;
                for (var i = rec.Item1.Row; i <= rec.Item2.Row; i++)
                {
                    for (var j = rec.Item1.Col; j <= rec.Item2.Col; j++)
                    {
                        sum += Matrix[i][j];
                    }
                }

                return sum;
            }

            private int CalcLeftOff(LinkedListNode<Result> maxInner, Tuple<Point, Point> rec)
            {
                var sum = maxInner.Value.Sum;
                for (var i = rec.Item1.Row; i <= rec.Item2.Row; i++)
                {
                    if (maxInner.Value.Rec.Item1.Row <= i && i <= maxInner.Value.Rec.Item2.Row)
                    {
                        for (var j = rec.Item1.Col; j < maxInner.Value.Rec.Item1.Col; j++)
                        {
                            sum += Matrix[i][j];
                        }

                        for (var j = maxInner.Value.Rec.Item2.Col + 1; j <= rec.Item2.Col; j++)
                        {
                            sum += Matrix[i][j];
                        }
                    }
                    else
                    {
                        for (var j = rec.Item1.Col; j <= rec.Item2.Col; j++)
                        {
                            sum += Matrix[i][j];
                        }
                    }
                }

                var rs = new Result {Sum = sum, Rec = rec};
                Results.AddAfter(maxInner, rs);
                return rs.Sum;
            }

            private bool Contains(Tuple<Point, Point> rec1, Tuple<Point, Point> rec2)
            {
                return rec1.Item1.Row <= rec2.Item1.Row && rec1.Item1.Col <= rec2.Item1.Col &&
                       rec1.Item2.Row >= rec2.Item2.Row && rec1.Item2.Col >= rec2.Item2.Col;
            }

        }

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.SumRegion(row1,col1,row2,col2);
 */
    }

    namespace p6.better
    {
        public class NumMatrix
        {
            private int[][] _matrix;

            private int RowSum(int row, int col)
            {
                if (col < 0) return 0;
                return _matrix[row][col];
            }

            public NumMatrix(int[][] matrix)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return;
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                _matrix = new int[rows][];
                for (var i = 0; i < rows; i++)
                {
                    _matrix[i] = new int[cols];
                    _matrix[i][0] = matrix[i][0];
                    for (var j = 1; j < cols; j++)
                    {
                        _matrix[i][j] = _matrix[i][j - 1] + matrix[i][j];
                    }
                }
            }

            public int SumRegion(int row1, int col1, int row2, int col2)
            {
                var sum = 0;
                for (var i = row1; i <= row2; i++)
                {
                    sum += RowSum(i, col2) - RowSum(i, col1 - 1);
                }

                return sum;
            }
        }

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.SumRegion(row1,col1,row2,col2);
 */
    }

    namespace p7
    {
        public class Solution
        {
            public bool CheckSubarraySum(int[] nums, int k)
            {
                if (nums == null || nums.Length == 0) return false;
                for (var i = 1; i < nums.Length; i++)
                {
                    nums[i] += nums[i - 1];
                    if (k == 0)
                    {
                        if (nums[i] == 0) return true;
                    }
                    else if (nums[i] % k == 0) return true;
                }

                for (var i = 0; i < nums.Length; i++)
                {
                    for (var j = i + 2; j < nums.Length; j++)
                    {
                        var sum = nums[j] - nums[i];
                        if (k == 0)
                        {
                            if (sum == 0) return true;
                        }
                        else if (sum % k == 0) return true;
                    }
                }

                return false;
            }
        }
    }
}