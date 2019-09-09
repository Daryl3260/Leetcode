using System.Collections.Generic;

namespace Leetcode.leetcode.mock.microsoft.p190817
{
    namespace p1
    {
        public class Solution
        {
            public bool IsAnagram(string s, string t)
            {
                if (s == t) return true;
                else if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t)) return false;
                var dict1 = ConstructDict(s);
                var dict2 = ConstructDict(t);
                for (var i = 0; i < dict1.Length; i++)
                {
                    if (dict1[i] != dict2[i]) return false;
                }

                return true;
            }

            private int[] ConstructDict(string s)
            {
                var rs = new int[26];
                foreach (var ch in s)
                {
                    rs[ch - 'a']++;
                }
                return rs;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int TitleToNumber(string s)
            {
                if (string.IsNullOrEmpty(s)) return 0;
                var bs = 1;
                var sum = 0;
                for (var i = s.Length - 1; i > -1; i--)
                {
                    sum += bs * (s[i] - 'A' + 1);
                    bs *= 26;
                }

                return sum;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public bool Exist(char[][] board, string word)
            {
                if (board == null || board.Length == 0 || board[0].Length == 0 || string.IsNullOrEmpty(word))
                    return false;
                var rows = board.Length;
                var cols = board[0].Length;
                var visited = new bool[rows][];
                for (var i = 0; i < rows; i++)
                {
                    visited[i]=new bool[cols];
                }
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (SubSearch(board, i, j, word, 0, visited)) return true;
                    }
                }

                return false;
            }

            private bool SubSearch(char[][] board, int i, int j, string word, int idx, bool[][] visited)
            {
                if (idx == word.Length) return true;
                var rows = board.Length;
                var cols = board[0].Length;
                if (i < 0 || j < 0 || i == rows || j == cols || visited[i][j] || board[i][j]!=word[idx]) return false;
                visited[i][j] = true;
                var rs = SubSearch(board, i + 1, j, word, idx + 1, visited) ||
                         SubSearch(board, i - 1, j, word, idx + 1, visited) ||
                         SubSearch(board, i, j + 1, word, idx + 1, visited) ||
                         SubSearch(board, i, j - 1, word, idx + 1, visited);
                visited[i][j] = false;
                return rs;
            }
        }
    }
}