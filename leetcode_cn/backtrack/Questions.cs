using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.backtrack
{
    namespace p1
    {
        public class Solution
        {
            public IList<string> GenerateParenthesis(int n)
            {
                if (n == 0) return new List<string>();
                var rs = new List<string>();
                var builder = new StringBuilder();
                OneCheck(rs, n, 0, 0, builder);
                return rs;
            }

            public void OneCheck(List<string> rs, int n, int left, int right, StringBuilder builder)
            {
                if (left == n && right == n)
                {
                    rs.Add(builder.ToString());
                    return;
                }

                var len = builder.Length;
                var extraLeft = left - right;
                var ll = n - left;

                if (extraLeft > 0)
                {
                    builder.Append(')');
                    OneCheck(rs, n, left, right + 1, builder);
                    builder.Remove(len, 1);
                }

                if (ll > 0)
                {
                    builder.Append('(');
                    OneCheck(rs, n, left + 1, right, builder);
                    builder.Remove(len, 1);
                }
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public IList<IList<int>> Permute(int[] nums)
            {
                if (nums == null || nums.Length == 0) return new List<IList<int>>();
                var currentList = new List<int>();
                var rs = new List<IList<int>>();
                var taken = new bool[nums.Length];
                OneCheck(currentList, nums, taken, rs);
                return rs;
            }

            // from idx to the end
            public void OneCheck(List<int> currentList, int[] nums, bool[] taken, List<IList<int>> rs)
            {
                if (currentList.Count == nums.Length)
                {
                    var list = new List<int>(currentList);
                    rs.Add(list);
                    return;
                }

                for (var i = 0; i < nums.Length; i++)
                {
                    if (taken[i]) continue;
                    taken[i] = true;
                    currentList.Add(nums[i]);
                    OneCheck(currentList, nums, taken, rs);
                    currentList.RemoveAt(currentList.Count - 1);
                    taken[i] = false;
                }
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            private Dictionary<char, IList<char>> DigitToLetterMap = new Dictionary<char, IList<char>>
            {
                {'2', new List<char>{'a','b','c'} },
                {'3', new List<char>{'d','e','f'} },
                {'4', new List<char>{'g','h','i'} },
                {'5', new List<char>{'j','k','l'} },
                {'6', new List<char>{'m','n','o'} },
                {'7', new List<char>{'p','q','r','s'} },
                {'8', new List<char>{'t','u','v'} },
                {'9', new List<char>{'w','x','y','z'} },
            };

            public IList<string> LetterCombinations(string digits)
            {
                if (string.IsNullOrEmpty(digits)) return new List<string>();
                var rs = new List<string>();
                var builder = new StringBuilder();
                Traverse(digits, 0, builder, rs);
                return rs;
            }

            public void Traverse(string digits, int idx, StringBuilder builder, List<string> rs)
            {
                if (idx == digits.Length)
                {
                    rs.Add(builder.ToString());
                    return;
                }

                char digit = digits[idx];

                var letters = DigitToLetterMap[digit];

                foreach (var letter in letters)
                {
                    builder.Append(letter);
                    var builderLen = builder.Length;
                    Traverse(digits, idx + 1, builder, rs);
                    builder.Remove(builderLen - 1, 1);
                }
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public bool Exist(char[][] board, string word)
            {
                var rows = board.Length;
                var cols = board[0].Length;
                bool[][] taken = new bool[rows][];
                for (var i = 0; i < rows; i++)
                {
                    taken[i] = new bool[cols];
                }

                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (SearchFrom(board, taken, word, 0, i, j))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            public bool SearchFrom(char[][] board, bool[][] taken, string word, int idx, int rIdx, int cIdx)
            {
                if (idx == word.Length) return true;
                int rows = board.Length;
                int cols = board[0].Length;

                if (rIdx < 0 || rIdx == rows || cIdx < 0 || cIdx == cols)
                {
                    return false;
                }

                if (word[idx] != board[rIdx][cIdx])
                {
                    return false;
                }

                if (taken[rIdx][cIdx]) return false;
                taken[rIdx][cIdx] = true;

                var rs = true;
                if (SearchFrom(board, taken, word, idx + 1, rIdx + 1, cIdx) ||
                    SearchFrom(board, taken, word, idx + 1, rIdx - 1, cIdx) ||
                    SearchFrom(board, taken, word, idx + 1, rIdx, cIdx + 1) ||
                    SearchFrom(board, taken, word, idx + 1, rIdx, cIdx - 1))
                {
                    rs = true;
                }
                else
                {
                    rs = false;
                }

                taken[rIdx][cIdx] = false;
                return rs;
            }
        }
    }
}
