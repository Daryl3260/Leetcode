using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode.mock.p20190913
{
    namespace p1
    {
        public class Solution
        {
            public void SetZeroes(int[][] matrix)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return;
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                var rowList = new HashSet<int>();
                var colList = new HashSet<int>();
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (matrix[i][j] == 0)
                        {
                            rowList.Add(i);
                            colList.Add(j);
                        }
                    }
                }

                foreach (var rowIdx in rowList)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        matrix[rowIdx][j] = 0;
                    }
                }

                foreach (var colIdx in colList)
                {
                    for (var i = 0; i < rows; i++)
                    {
                        matrix[i][colIdx] = 0;
                    }
                }
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public static void Test()
            {
                Console.WriteLine($"{new Solution().MyAtoi("4193 with words")}");
            }

            public int MyAtoi(string str)
            {
                if (string.IsNullOrEmpty(str)) return 0;
                str = str.TrimStart(' ');
                if (string.IsNullOrEmpty(str)) return 0;
                var sign = 1;
                if (str[0] == '+' || str[0] == '-')
                {
                    sign = str[0] == '+' ? 1 : -1;
                    str = str.Substring(1);
                }

                if (string.IsNullOrEmpty(str)) return 0;
                if (!IsDigit(str[0])) return 0;
                var i = 0;
                while (i < str.Length && IsDigit(str[i])) i++;
                str = str.Substring(0, i);
                str = str.TrimStart('0');
                if (sign == 1)
                {
                    var maxStr = int.MaxValue.ToString();
                    if (str.Length > maxStr.Length) return int.MaxValue;
                    else if (str.Length == maxStr.Length && str.CompareTo(maxStr) >= 0)
                        return int.MaxValue;
                    else return Convert(str);
                }
                else
                {
                    var maxStr = int.MinValue.ToString().Substring(1);
                    if (str.Length > maxStr.Length) return int.MinValue;
                    else if (str.Length == maxStr.Length && str.CompareTo(maxStr) >= 0) return int.MinValue;
                    else return -Convert(str);
                }
            }

            private int Convert(string str)
            {
                var sum = 0;
                for (var i = 0; i < str.Length; i++)
                {
                    sum *= 10;
                    sum += str[i] - '0';
                }

                return sum;
            }

            private bool IsDigit(char ch)
            {
                return '0' <= ch && ch <= '9';
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public int CountBattleships(char[][] board)
            {
                if (board == null || board.Length == 0 || board[0].Length == 0) return 0;
                var count = 0;
                var rows = board.Length;
                var cols = board[0].Length;
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (board[i][j] == 'X')
                        {
                            count++;
                            MarkNeighbor(board,i,j);
                        }
                    }
                }

                return count;
            }

            private void MarkNeighbor(char[][] board, int i, int j)
            {
//                for (var k = i; k > -1; k--)
//                {
//                    if (board[k][j] == 'X') board[k][j] = '.';
//                    else break;
//                }
                for (var k = i; k <board.Length; k++)
                {
                    if (board[k][j] == 'X') board[k][j] = '.';
                    else break;
                }
//                for (var k = j-1; k > -1; k--)
//                {
//                    if (board[i][k] == 'X') board[i][k] = '.';
//                    else break;
//                }
                for (var k = j+1; k<board[0].Length; k++)
                {
                    if (board[i][k] == 'X') board[i][k] = '.';
                    else break;
                }
                
                
            }

            private bool Valid(char[][] board, int i, int j)
            {
                var rows = board.Length;
                var cols = board[0].Length;
                return -1 < i && i < rows && -1 < j && j < cols;
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            private bool[][] _trapped;
            public bool IsMatch(string s, string p)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return string.IsNullOrEmpty(p) || p == "*";
                }
                else if (string.IsNullOrEmpty(p))
                {
                    return false;
                }

                var sLen = s.Length;
                var pLen = p.Length;
                _trapped = new bool[sLen][];
                for (var i = 0; i < sLen; i++)
                {
                    _trapped[i]=new bool[pLen];
                }

                return SubMatch(s, 0, p, 0);
            }

            private bool SubMatch(string s, int i, string p, int j)
            {
                if (i == s.Length)
                {
                    if (j == p.Length) return true;
                    return p[j] == '*' && SubMatch(s, i, p, j + 1);
                }
                else
                {
                    if (j == p.Length) return false;
                    if (_trapped[i][j]) return false;
                    bool rs;
                    if (s[i] == p[j] || p[j] == '?') rs = SubMatch(s, i + 1, p, j + 1);
                    else if (p[j] == '*')
                    {
                        rs = SubMatch(s, i + 1, p, j) || SubMatch(s, i, p, j + 1);
                    }
                    else rs = false;

                    if (rs) return true;
                    else
                    {
                        _trapped[i][j] = true;
                        return false;
                    }
                }
            }
        }
    }
}