using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode.microsoft.BackTracking
{
    namespace p1
    {
        public class Solution
        {
            private static StringBuilder _builder = new StringBuilder();

            public IList<string> LetterCombinations(string digits)
            {
                if (string.IsNullOrEmpty(digits)) return new List<string>();
                var rs = new List<string>();
                Subsearch(digits, 0, CreateDict(), new List<char>(), rs);
                return rs;
            }

            private Dictionary<char, IList<char>> CreateDict()
            {
                var dict = new Dictionary<char, IList<char>>();
                dict['2'] = new List<char> {'a', 'b', 'c'};
                dict['3'] = new List<char> {'d', 'e', 'f'};
                dict['4'] = new List<char> {'g', 'h', 'i'};
                dict['5'] = new List<char> {'j', 'k', 'l'};
                dict['6'] = new List<char> {'m', 'n', 'o'};
                dict['7'] = new List<char> {'p', 'q', 'r', 's'};
                dict['8'] = new List<char> {'t', 'u', 'v'};
                dict['9'] = new List<char> {'w', 'x', 'y', 'z'};
                return dict;
            }

            private void Subsearch(
                string digits, int idx, Dictionary<char, IList<char>> dict, List<char> list,
                List<string> rs)
            {
                if (idx == digits.Length)
                {
                    _builder.Clear();
                    foreach (var ch in list)
                    {
                        _builder.Append(ch);
                    }

                    rs.Add(_builder.ToString());
                }
                else
                {
                    var num = digits[idx];
                    var letters = dict[num];
                    foreach (var letter in letters)
                    {
                        list.Add(letter);
                        Subsearch(digits, idx + 1, dict, list, rs);
                        list.RemoveAt(list.Count - 1);
                    }
                }
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            private bool[][] visited;
            private int rows;
            private int cols;

            public IList<string> FindWords(char[][] board, string[] words)
            {
                rows = board.Length;
                cols = board[0].Length;
                visited = new bool[rows][];
                for (int i = 0; i < rows; i++)
                {
                    visited[i] = new bool[cols];
                }

                var rs = new HashSet<string>();
                foreach (var word in words)
                {
                    FindWord(board, word, rs);
                }

                return new List<string>(rs);
            }

            private void FindWord(char[][] board, string word, ISet<string> rs)
            {
//                var initSize = rs.Count;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (board[i][j] != word[0]) continue;
//                        foreach (var onerow in visited)
//                        {
//                            for (int k = 0; k < onerow.Length; k++)
//                            {
//                                onerow[k] = false;
//                            }
//                        }
                        Subsearch(board, visited, i, j, word, 0, rs);
//                        if (rs.Count > initSize) return;
                    }
                }

            }

            private void Subsearch(char[][] board, bool[][] visited, int x, int y, string word, int idx,
                ISet<string> rs)
            {
                if (idx == word.Length)
                {
                    rs.Add(word);
                }
                else
                {
                    var rows = board.Length;
                    var cols = board[0].Length;
                    if (x < 0 || x == rows || y < 0 || y == cols || visited[x][y] || board[x][y] != word[idx]) return;
                    visited[x][y] = true;
                    Subsearch(board, visited, x - 1, y, word, idx + 1, rs);
                    Subsearch(board, visited, x + 1, y, word, idx + 1, rs);
                    Subsearch(board, visited, x, y - 1, word, idx + 1, rs);
                    Subsearch(board, visited, x, y + 1, word, idx + 1, rs);
                    visited[x][y] = false;
                }
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public bool IsMatch(string s, string p)
            {
                if (s == null || p == null) return false;
                bool[][] trapped = new bool[s.Length][];
                for (int i = 0; i < s.Length; i++)
                {

                    trapped[i] = new bool[p.Length];

                }

                return SubMatch(s, 0, p, 0, trapped);
            }

            private bool SubMatch(string s, int i, string p, int j, bool[][] trapped)
            {
                if (i == s.Length && j == p.Length) return true;
                else if (j == p.Length) return false;
                else if (i == s.Length)
                {
                    if (p[j] == '*')
                    {
                        while (j < p.Length && p[j] == '*') j++;
                        if (j < p.Length) return false;
                        return true;
                    }
                    else return false;
                }
                else
                {
                    if (trapped[i][j]) return false;
                    bool rs;
                    if (p[j] == '*')
                    {
                        rs = SubMatch(s, i, p, j + 1, trapped) ||
                             SubMatch(s, i + 1, p, j, trapped);

                    }

                    else if (p[j] == '?')
                    {
                        rs = SubMatch(s, i + 1, p, j + 1, trapped);

                    }
                    else if (s[i] == p[j])
                    {
                        rs = SubMatch(s, i + 1, p, j + 1, trapped);
                    }
                    else
                    {
                        rs = false;
                    }

                    if (rs) return true;
                    else
                    {
                        trapped[i][j] = true;
                        return false;
                    }
                }
            }
        }
    }

    namespace p3.better
    {
        public class Solution
        {
            public bool IsMatch(string s, string p)
            {
                if (s == null || p == null) return false;
                var i = 0;
                var j = 0;
                var star = new int?();
                var starpair = new int?();
                while (i<s.Length)
                {
//                    if (i == s.Length && j == p.Length) return true;
//                    if (i == s.Length)
//                    {
//                        while (j < p.Length && p[j] == '*') j++;
//                        return j == p.Length;
//                    }
//
//                    if (j == p.Length) return false;
                    if (j<p.Length&&(p[j] == '?' || s[i] == p[j]))
                    {
                        i++;
                        j++;
                    }
                    else if (j<p.Length&&p[j] == '*')
                    {
                        star = j;
                        starpair = i;
                        j++;
                    }
                    else if (star != null)
                    {
                        starpair++;
                        i = starpair.Value;
                        j = star.Value + 1;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (j < p.Length)
                {
                    while (j < p.Length && p[j] == '*') j++;
                    return j == p.Length;
                }
                else return true;
            }
        }
    }
}