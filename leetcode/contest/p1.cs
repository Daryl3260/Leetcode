using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode.contest.p190728
{
    namespace p1
    {
        public class Solution
        {
            public int Tribonacci(int n)
            {
                if (n == 0)
                {
                    return 0;
                }

                if (n == 1)
                {
                    return 1;
                }

                if (n == 2) return 1;
                var n0 = 0;
                var n1 = 1;
                var n2 = 1;
                while (n > 2)
                {
                    n--;
                    var temp = n0 + n1 + n2;
                    n0 = n1;
                    n1 = n2;
                    n2 = temp;
                }

                return n2;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public string AlphabetBoardPath(string target)
            {
                var board = new[]
                {
                    "abcde", "fghij", "klmno", "pqrst", "uvwxy", "z"
                };
                var i = 0;
                var j = 0;
                var ch = board[i][j];
                var builder = new StringBuilder();
                foreach (var letter in target)
                {
                    builder.Append(MoveTo(board, i, j, letter, out var oi, out var oj));
                    i = oi;
                    j = oj;
                }

                return builder.ToString();
            }

            private string MoveTo(string[] board, int i, int j, char target, out int oi, out int oj)
            {
                var ch = board[i][j];
                var distance = ch - target;
                if (distance == 0)
                {
                    oj = j;
                    oi = i;
                    return "!";
                }

                FindPosition(target, board, out oi, out oj);
                var builder = new StringBuilder();
                if (target == 'z')
                {
                    var horizontal = j - oj;
                    if (horizontal > 0)
                    {
                        for (int k = 0; k < horizontal; k++)
                        {
                            builder.Append('L');
                        }
                    }
                    else if (horizontal < 0)
                    {
                        for (int k = 0; k < -horizontal; k++)
                        {
                            builder.Append('R');
                        }
                    }

                    var vertical = i - oi;

                    if (vertical > 0)
                    {
                        for (int k = 0; k < vertical; k++)
                        {
                            builder.Append('U');
                        }
                    }
                    else if (vertical < 0)
                    {
                        for (int k = 0; k < -vertical; k++)
                        {
                            builder.Append('D');
                        }
                    }
                }
                else
                {
                    var vertical = i - oi;

                    if (vertical > 0)
                    {
                        for (int k = 0; k < vertical; k++)
                        {
                            builder.Append('U');
                        }
                    }
                    else if (vertical < 0)
                    {
                        for (int k = 0; k < -vertical; k++)
                        {
                            builder.Append('D');
                        }
                    }

                    var horizontal = j - oj;
                    if (horizontal > 0)
                    {
                        for (int k = 0; k < horizontal; k++)
                        {
                            builder.Append('L');
                        }
                    }
                    else if (horizontal < 0)
                    {
                        for (int k = 0; k < -horizontal; k++)
                        {
                            builder.Append('R');
                        }
                    }
                }



                builder.Append('!');
                return builder.ToString();
            }

            private void FindPosition(char ch, string[] board, out int oi, out int oj)
            {
                oi = -1;
                oj = -1;
                for (var i = 0; i < board.Length; i++)
                {
                    var str = board[i];
                    for (var j = 0; j < str.Length; j++)
                    {
                        if (str[j] == ch)
                        {
                            oi = i;
                            oj = j;
                            return;
                        }
                    }
                }
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public int Largest1BorderedSquare(int[][] grid)
            {
                if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;
                var starters = GetStarter(grid);
                if (starters.Count == 0) return 0;
                var max = 1;
                foreach (var starter in starters)
                {
                    max = Check(starter, grid, max);
                }

                return max*max;
            }

            private int Check(Tuple<int, int> starter, int[][] grid,int max)
            {
                var si = starter.Item1;
                var sj = starter.Item2;
                var rows = grid.Length;
                var cols = grid[0].Length;
                var limit = Math.Min(rows - si, cols - sj);
                var rs = max;
                for (var len = limit; len > max; len--)
                {
                    if (CheckBound(grid, si, sj, len))
                    {
                        rs = len;
                        break;
                    }
                }
                return rs;
            }

            private bool CheckBound(int[][] grid, int si, int sj,int len)
            {
                for (var i = 0; i < len; i++)
                {
                    if (!(Valid(grid, si + i, sj) && Valid(grid, si, sj + i) && Valid(grid, si + i, sj + len - 1) &&
                          Valid(grid, si + len - 1, sj + i))) return false;
                }
                return true;
            }

            private bool Valid(int[][] grid,int i,int j)
            {
                return -1< i && i < grid.Length && -1<j && j < grid[0].Length && grid[i][j] == 1;
            }

            private List<Tuple<int, int>> GetStarter(int[][] grid)
            {
                var rs = new List<Tuple<int,int>>();
                var rows = grid.Length;
                var cols = grid[0].Length;
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (grid[i][j] == 1)
                        {
                            rs.Add(new Tuple<int, int>(i,j));
                        }
                    }
                }

                return rs;
            }
        }
    }
}