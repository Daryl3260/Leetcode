using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode.facebook.recursion
{
    namespace p1
    {
        public class Solution
        {
            private Dictionary<char, IList<char>> dict = new Dictionary<char, IList<char>>();

            public IList<string> LetterCombinations(string digits)
            {
                var rs = new List<string>();
                if (string.IsNullOrEmpty(digits)) return rs;
                dict['2'] = new List<char> {'a', 'b', 'c'};
                dict['3'] = new List<char> {'d', 'e', 'f'};
                dict['4'] = new List<char> {'g', 'h', 'i'};
                dict['5'] = new List<char> {'j', 'k', 'l'};
                dict['6'] = new List<char> {'m', 'n', 'o'};
                dict['7'] = new List<char> {'p', 'q', 'r', 's'};
                dict['8'] = new List<char> {'t', 'u', 'v'};
                dict['9'] = new List<char> {'w', 'x', 'y', 'z'};
                CheckAt(digits, 0, new List<char>(), rs);
                return rs;
            }

            private void CheckAt(string digits, int idx, List<char> list, IList<string> rs)
            {
                if (idx >= digits.Length)
                {
                    var builder = new StringBuilder();
                    foreach (var ch in list)
                    {
                        builder.Append(ch);
                    }

                    rs.Add(builder.ToString());
                }
                else
                {
                    var ch = digits[idx];
                    var dictList = dict[ch];
                    foreach (var letter in dictList)
                    {
                        list.Add(letter);
                        CheckAt(digits, idx + 1, list, rs);
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
            private bool[][] _dead;

            public bool IsMatch(string s, string p)
            {
                if (s == p) return true;
                else if (string.IsNullOrEmpty(s))
                {
                    if (string.IsNullOrEmpty(p)) return true;
                    return MatchEmpty(p, 0);
                }
                else if (string.IsNullOrEmpty(p)) return false;

                var sLen = s.Length;
                var pLen = p.Length;
                _dead = new bool[sLen][];
                for (var i = 0; i < sLen; i++)
                {
                    _dead[i] = new bool[pLen];
                }

                return CheckAt(s, 0, p, 0);
            }

            private bool MatchEmpty(string p, int idx)
            {
                var i = idx;
                while (i < p.Length)
                {
                    if (i < p.Length - 1 && p[i + 1] == '*') i += 2;
                    else return false;
                }

                return true;
            }

            private bool CheckAt(string s, int i, string p, int j)
            {
                if (i < s.Length && j < p.Length && _dead[i][j]) return false;
                bool rs;
                if (i == s.Length)
                {
                    if (j == p.Length) rs = true;
                    else rs = MatchEmpty(p, j);
                }
                else
                {
                    if (j == p.Length) rs = false;
                    else if (j == p.Length - 1 || p[j + 1] != '*')
                    {
                        if (s[i] == p[j]) rs = CheckAt(s, i + 1, p, j + 1);
                        else if (p[j] == '.') rs = CheckAt(s, i + 1, p, j + 1);
                        else rs = false;
                    }
                    else
                    {
                        if (s[i] == p[j] || p[j] == '.')
                        {
                            rs = CheckAt(s, i + 1, p, j) || CheckAt(s, i, p, j + 2);
                        }
                        else
                        {
                            rs = CheckAt(s, i, p, j + 2);
                        }
                    }
                }

                if (rs) return true;
                else
                {
                    if (i < s.Length && j < p.Length) _dead[i][j] = true;
                    return false;
                }
            }

        }
    }

    namespace p2.rework
    {
        public class Solution
        {
            private bool[][] _trapped;

            public bool IsMatch(string s, string p)
            {
                if (ReferenceEquals(s, p) || s == p) return true;
                if (string.IsNullOrEmpty(s)) return MatchNull(p, 0);
                else if (string.IsNullOrEmpty(p)) return false;
                var sLen = s.Length;
                var pLen = p.Length;
                _trapped = new bool[sLen][];
                for (var i = 0; i < sLen; i++)
                {
                    _trapped[i] = new bool[pLen];
                }

                return MatchAt(s, 0, p, 0);
            }

            private bool MatchAt(string s, int i, string p, int j)
            {
                if (i == s.Length)
                {
                    return MatchNull(p, j);
                }
                else if (j == p.Length) return false;

                if (_trapped[i][j]) return false;
                if (j < p.Length - 1 && p[j + 1] == '*')
                {
                    var schar = s[i];
                    var pp = p[j];
                    if (schar == pp || pp == '.')
                    {
                        var rs = MatchAt(s, i, p, j + 2) || MatchAt(s, i + 1, p, j);
                        if (!rs) _trapped[i][j] = true;
                        return rs;
                    }
                    else
                    {
                        var rs = MatchAt(s, i, p, j + 2);
                        if (!rs) _trapped[i][j] = true;
                        return rs;
                    }
                }
                else if (s[i] == p[j] || p[j] == '.')
                {
                    var rs = MatchAt(s, i + 1, p, j + 1);
                    if (!rs) _trapped[i][j] = true;
                    return rs;
                }
                else
                {
                    _trapped[i][j] = true;
                    return false;
                }
            }

            private bool MatchNull(string p, int startIdx)
            {
                if (string.IsNullOrEmpty(p) || startIdx == p.Length) return true;
                var len = p.Length - startIdx;
                if (len % 2 == 0)
                {
                    for (var i = 0; i < len / 2; i++)
                    {
                        if (p[startIdx + i * 2 + 1] != '*') return false;
                    }

                    return true;
                }
                else return false;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public IList<IList<int>> Subsets(int[] nums)
            {
                if (nums == null || nums.Length == 0)
                {
                    var rs = new List<IList<int>>();
                    rs.Add(new List<int>());
                    return rs;
                }

                var ret = new List<IList<int>>();
                CheckAt(nums, 0, new List<int>(), ret);
                return ret;
            }

            private void CheckAt(int[] nums, int idx, List<int> list, IList<IList<int>> rs)
            {
                if (idx == nums.Length)
                {
                    rs.Add(list);
                }
                else
                {
                    CheckAt(nums, idx + 1, list, rs);
                    var copy = new List<int>(list);
                    copy.Add(nums[idx]);
                    CheckAt(nums, idx + 1, copy, rs);
                }
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            private static List<int> _selfMirror;
            private static Dictionary<int, int> _mirror;

            static Solution()
            {
                _selfMirror = new List<int>{0,1,8};
                _mirror = new Dictionary<int, int>
                {
                    [0] = 0,
                    [1] = 1,
                    [8] = 8,
                    [6] = 9,
                    [9] = 6
                };
            }
            public IList<string> FindStrobogrammatic(int n)
            {
                if(n<1)return new List<string>();
                else if(n==1)return new List<string>{"0","1","8"};
                var rs = new List<string>();
                if ((n & 1) == 0)
                {
                    InsertAt(new int[n],0,rs);
                    return rs;
                }
                else
                {
                    var midIdx = n >> 1;
                    foreach (var i in _selfMirror)
                    {
                        var array = new int[n];
                        array[midIdx] = i;
                        InsertAt(array,0,rs);
                    }
                    return rs;
                }
            }

            private void InsertAt(int[] array, int idx, IList<string> rs)
            {
                var len = array.Length;
                if (idx >= len / 2)
                {
                    var builder = new StringBuilder();
                    foreach (var i in array)
                    {
                        builder.Append(i);
                    }
                    rs.Add(builder.ToString());
                }
                else
                {
                    if (idx == 0)
                    {
                        foreach (var key in _mirror.Keys)
                        {
                            if (key != 0)
                            {
                                var copy = new int[array.Length];
                                Array.Copy(array,copy,array.Length);
                                copy[idx] = key;
                                copy[copy.Length - 1 - idx] = _mirror[key];
                                InsertAt(copy,idx+1,rs);
                            }
                        }
                    }
                    else
                    {
                        foreach (var key in _mirror.Keys)
                        {
                            var copy = new int[array.Length];
                            Array.Copy(array, copy, array.Length);
                            copy[idx] = key;
                            copy[copy.Length - 1 - idx] = _mirror[key];
                            InsertAt(copy, idx + 1, rs);
                        }
                    }
                }
            }
        }
    }
}