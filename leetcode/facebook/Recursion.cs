using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode.facebook
{
    namespace p1
    {
        public class Solution
        {
            public IList<IList<int>> Permute(int[] nums)
            {
                var rs = new List<IList<int>>();
                if (nums == null || nums.Length == 0) return rs;
                PermuteAt(new List<int>(nums), rs, 0);
                return rs;
            }

            private void PermuteAt(List<int> p, List<IList<int>> rs, int idx)
            {
                if (idx == p.Count)
                {
                    rs.Add(new List<int>(p));
                    return;
                }

                for (int i = idx; i < p.Count; i++)
                {
                    var tmp = p[i];
                    p[i] = p[idx];
                    p[idx] = tmp;
                    PermuteAt(p, rs, idx + 1);
                    tmp = p[i];
                    p[i] = p[idx];
                    p[idx] = tmp;
                }
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public class ListWrapper : IEquatable<ListWrapper>
            {
                public IList<int> Val { get; set; }

                private static bool ListEqual(IList<int> l1, IList<int> l2)
                {
                    if (ReferenceEquals(l1, l2)) return true;
                    if (l1.Count != l2.Count) return false;
                    var len = l1.Count;
                    for (int i = 0; i < len; i++)
                    {
                        if (l1[i] != l2[i]) return false;
                    }

                    return true;
                }

                private static int ListHash(IList<int> list)
                {
                    var builder = new StringBuilder();
                    foreach (var elem in list)
                    {
                        builder.Append(elem);
                    }

                    return builder.ToString().GetHashCode();
                }

                public bool Equals(ListWrapper other)
                {
                    if (ReferenceEquals(null, other)) return false;
                    if (ReferenceEquals(this, other)) return true;
                    return ListEqual(Val, other.Val);
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    if (ReferenceEquals(this, obj)) return true;
                    if (obj.GetType() != this.GetType()) return false;
                    return Equals((ListWrapper) obj);
                }

                public override int GetHashCode()
                {
                    return (Val != null ? ListHash(Val) : 0);
                }
            }

            public IList<IList<int>> PermuteUnique(int[] nums)
            {
                var lists = Permute(nums);
                var set = new HashSet<ListWrapper>(lists.Select(elem => new ListWrapper {Val = elem}));
                return set.Select(elem => elem.Val).ToList();
            }

            public IList<IList<int>> Permute(int[] nums)
            {
                var rs = new List<IList<int>>();
                if (nums == null || nums.Length == 0) return rs;
                PermuteAt(new List<int>(nums), rs, 0);
                return rs;
            }

            private void PermuteAt(List<int> p, List<IList<int>> rs, int idx)
            {
                if (idx == p.Count)
                {
                    rs.Add(new List<int>(p));
                    return;
                }

                for (int i = idx; i < p.Count; i++)
                {
                    var tmp = p[i];
                    p[i] = p[idx];
                    p[idx] = tmp;
                    PermuteAt(p, rs, idx + 1);
                    tmp = p[i];
                    p[i] = p[idx];
                    p[idx] = tmp;
                }
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            private static Stack<char> _stack = new Stack<char>();

            private static HashSet<char> _quotes = new HashSet<char>
            {
                '(', ')', '[', ']', '{', '}'
            };

            public IList<string> RemoveInvalidParentheses(string s)
            {
                if (string.IsNullOrEmpty(s)) return new List<string> {""};
                var rs = new HashSet<string>();
                var removed = new HashSet<int>();
                var valid = IsValid(s, removed);
                var paired = new HashSet<int>();
                if (valid) return new List<string> {s};
                RemoveOne(s, removed, rs);
                var len = rs.Max(elem => elem.Length);
                return rs.Where(elem => elem.Length == len).ToList();
            }

            private void RemoveOne(string s, HashSet<int> removed, HashSet<string> rs)
            {
                if (rs.Count > 0)
                {
                    var maxLen = rs.Max(elem => elem.Length);
                    if (s.Length - removed.Count <= maxLen) return;
                }

                var valid = false;
                for (int i = 0; i < s.Length; i++)
                {
                    if (removed.Contains(i) || !_quotes.Contains(s[i])) continue;
                    removed.Add(i);
                    if (IsValid(s, removed))
                    {
                        rs.Add(Construct(s, removed));
                        valid = true;
                    }

                    removed.Remove(i); //backtracking
                }

                if (valid)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (removed.Contains(i) || !_quotes.Contains(s[i])) continue;
                        removed.Add(i);
                        RemoveOne(s, removed, rs);
                        removed.Remove(i);
                    }
                }
            }

            private string Construct(string s, HashSet<int> removed)
            {
                var builder = new StringBuilder(s.Length);
                for (var i = 0; i < s.Length; i++)
                {
                    if (removed.Contains(i)) continue;
                    builder.Append(s[i]);
                }

                return builder.ToString();
            }

            private bool Match(char left, char right)
            {
                return (left == '(' && right == ')') || (left == '[' && right == ']') || (left == '{' && right == '{');
            }

            public bool IsValid(string s, HashSet<int> removed)
            {
                if (removed.Count == s.Length) return true;
                _stack.Clear();
                for (int i = 0; i < s.Length; i++)
                {
                    if (removed.Contains(i) || !_quotes.Contains(s[i])) continue;
                    var ch = s[i];
                    if (ch == '(' || ch == '[' || ch == '{') _stack.Push(ch);
                    else if (_stack.Count == 0 || !Match(_stack.Pop(), ch)) return false;
                }

                return _stack.Count == 0;
            }

        }
    }

    namespace p3.better
    {
        public class Solution
        {
            public IList<string> RemoveInvalidParentheses(string s)
            {
                var rs = new HashSet<string>();
                Check(s, 0, rs);
                var len = rs.Max(elem => elem.Length);
                var ret = rs.Where(elem => elem.Length == len);
                return ret.ToList();
            }

            private void Check(string str, int startIdx, HashSet<string> rs)
            {
                if (rs.Count > 0 && rs.First().Length > str.Length) return;
                var len = str.Length;
                var leftCount = 0;
                var misMatched = false;
                for (int i = 0; i < len; i++)
                {
                    if (str[i] == '(')
                    {
                        leftCount++;
                    }
                    else if (str[i] == ')')
                    {
                        if (leftCount > 0)
                        {
                            leftCount--;
                        }
                        else //too many ')'
                        {
                            misMatched = true;
                            for (int j = startIdx; j <= i; j++)
                            {
                                if (str[j] == ')')
                                {
                                    Check(str.Remove(j, 1), j, rs);
                                }
                            }
                        }
                    }
                }

                if (leftCount == 0 && !misMatched)
                {
                    rs.Add(str);
                }
                else //too many '('
                {
                    for (int i = startIdx; i < str.Length; i++)
                    {
                        if (str[i] == '(')
                        {
                            Check(str.Remove(i, 1), i, rs);
                        }
                    }
                }
            }

            private String ToStr(List<char> list)
            {
                var builder = new StringBuilder(list.Count);
                foreach (var ch in list)
                {
                    builder.Append(ch);
                }

                return builder.ToString();
            }

            private bool IsValid(List<char> list)
            {
                var leftCount = 0;
                foreach (var ch in list)
                {
                    if (ch == '(' || ch == ')')
                    {
                        if (ch == '(')
                        {
                            leftCount++;
                        }
                        else
                        {
                            if (leftCount == 0) return false;
                            else leftCount--;
                        }
                    }
                }

                return leftCount == 0;
            }
        }
    }

    namespace p3.stillBetter
    {
        public class Solution
        {
            HashSet<string> result = new HashSet<string>();

            public IList<string> RemoveInvalidParentheses(string s)
            {
                Evaluate(s, 0, 0);
                return result.ToList();
            }

            private void Evaluate(string s, int completed, int ind)
            {
                var stack = new Stack<Char>();

                int internalCompleted = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '(')
                    {
                        stack.Push(s[i]);
                        continue;
                    }

                    if (s[i] == ')')
                    {
                        if (stack.Count > 0)
                        {
                            internalCompleted++;
                            stack.Pop();
                        }
                        else
                        {
                            // Excess closing
                            for (int j = ind; j <= i; j++)
                            {
                                if (s[j] == ')')
                                {
                                    Evaluate(s.Remove(j, 1),
                                        internalCompleted > completed ? internalCompleted : completed, j);
                                }
                            }

                            return;
                        }
                    }
                }

                if (stack.Count != 0)
                {
                    // Excess opening
                    for (int j = ind; j < s.Length; j++)
                    {
                        if (s[j] == '(')
                        {
                            Evaluate(s.Remove(j, 1), internalCompleted > completed ? internalCompleted : completed, j);
                        }
                    }

                    return;
                }

                if (internalCompleted >= completed) result.Add(s);
            }
        }
    }

    namespace p3.betterBetter
    {
        public class Solution
        {

            private bool IsValid(string str)
            {
                var leftCount = 0;
                foreach (var ch in str)
                {
                    if (ch == '(')
                    {
                        leftCount++;
                    }
                    else if (ch == ')')
                    {
                        if (leftCount > 0) leftCount--;
                        else return false;
                    }

                    
                }
                return leftCount == 0;
            }
            public IList<string> RemoveInvalidParentheses(string s)
            {
                var visited = new HashSet<string>();
                var rs = new HashSet<string>();
//                if(IsValid(s))return new List<string>{s};
//                visited.Add(s);
                var queue = new Queue<string>();
                queue.Enqueue(s);
                while (queue.Count > 0)
                {
                    var top = queue.Dequeue();
                    if (visited.Contains(top)) continue;
                    visited.Add(top);
                    if (IsValid(top))
                    {
                        rs.Add(top);
                    }
                    else
                    {
                        for (int i = 0; i < top.Length; i++)
                        {
                            queue.Enqueue(top.Remove(i,1));
                        }
                    }
                }

                var len = rs.Max(elem => elem.Length);
                return rs.Where(elem => elem.Length == len).ToList();
            }
        }
    }
}