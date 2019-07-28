using System;
using System.Collections.Generic;
using System.Security.Cryptography;

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
        public class Solution {
            public int LongestValidParentheses(string s)
            {
                if (string.IsNullOrEmpty(s)) return 0;
                var leftStack = new Stack<int>();
                var pairs = new List<Tuple<int,int>>();
                for (var i = 0; i < s.Length; i++)
                {
                    var ch = s[i];
                    if (ch == '(')
                    {
                        leftStack.Push(i);
                    }
                    else if(leftStack.Count>0)
                    {
                        var leftIdx = leftStack.Pop();
                        var rightIdx = i;
                        pairs.Add(new Tuple<int, int>(leftIdx,rightIdx));
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
}