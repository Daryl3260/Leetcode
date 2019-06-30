using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode.microsoft.ArrayAndStrings
{
    namespace p1
    {
        public class Solution
        {
            public int[] TwoSum(int[] nums, int target)
            {
                if (nums == null || nums.Length < 2) return null;
                var dict = new Dictionary<int, List<int>>();
                for (int i = 0; i < nums.Length; i++)
                {
                    if (!dict.ContainsKey(nums[i]))
                    {
                        dict[nums[i]] = new List<int>();
                    }

                    dict[nums[i]].Add(i);
                }

                Array.Sort(nums);
                int left = 0;
                int right = nums.Length - 1;
                while (left < right)
                {
                    var sum = nums[left] + nums[right];
                    if (sum == target) break;
                    else if (sum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }

                if (left >= right) throw new Exception($"No good answers:{left},{right}");
                else
                {
                    if (nums[left] == nums[right])
                    {
                        var list = dict[nums[left]];
                        return new[] {list[0], list[1]};
                    }
                    else
                    {
                        return new[] {dict[nums[left]][0], dict[nums[right]][0]};
                    }
                }
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public bool IsPalindrome(string s)
            {
                if (string.IsNullOrEmpty(s)) return true;
                s = s.ToLower();
                var left = 0;
                var right = s.Length - 1;
                while (true)
                {
                    while (left < s.Length && !IsAlphaNumeric(s[left])) left++;
                    while (right > -1 && !IsAlphaNumeric(s[right])) right--;
                    if (left >= right) return true;
                    if (s[left] != s[right]) return false;
                    else
                    {
                        left++;
                        right--;
                    }
                }
            }

            private bool IsAlphaNumeric(char ch)
            {
                return ('0' <= ch && ch <= '9') || ('a' <= ch && ch <= 'z');
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            private string MaxPositive = int.MaxValue + "";
            private string MaxNegative = (int.MinValue + "").Substring(1);

            public int MyAtoi(string str)
            {
                bool sign;
                var idx = 0;
                while (idx < str.Length && (str[idx] == ' ')) idx++;
                if (idx == str.Length) return 0;
                if (str[idx] == '+' || IsNumeric(str[idx])) sign = true;
                else if (str[idx] == '-') sign = false;
                else return 0;
                if (str[idx] == '+' || str[idx] == '-') idx++;
                if (idx == str.Length || !IsNumeric(str[idx])) return 0;
                while (idx < str.Length && str[idx] == '0') idx++;
                if (idx == str.Length) return 0;
                var left = idx;
                while (idx < str.Length && IsNumeric(str[idx])) idx++;
                var right = idx - 1;
                var len = right - left + 1;
                if (sign)
                {
                    if (len > MaxPositive.Length || (len == MaxPositive.Length &&
                                                     string.Compare(str.Substring(left, len), MaxPositive,
                                                         StringComparison.Ordinal) >= 0)) return int.MaxValue;
                    var sum = 0;
                    var ptr = left;
                    while (ptr <= right)
                    {
                        sum *= 10;
                        sum += str[ptr++] - '0';
                    }

                    return sum;
                }
                else
                {
                    if (len > MaxNegative.Length || (len == MaxPositive.Length &&
                                                     string.Compare(str.Substring(left, len), MaxNegative,
                                                         StringComparison.Ordinal) >= 0)) return int.MinValue;
                    var sum = 0;
                    var ptr = left;
                    while (ptr <= right)
                    {
                        sum *= 10;
                        sum += str[ptr++] - '0';
                    }

                    return -sum;
                }
            }

            private bool IsNumeric(char ch)
            {
                return '0' <= ch && ch <= '9';
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public void ReverseString(char[] s)
            {
                if (s == null || s.Length < 2) return;
                var left = 0;
                var right = s.Length - 1;
                while (left < right)
                {
                    var tmp = s[left];
                    s[left] = s[right];
                    s[right] = tmp;
                    left++;
                    right--;
                }
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public string ReverseWords(string s)
            {
                if (string.IsNullOrEmpty(s)) return "";
                s = s.Trim(' ');
                var words = s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var builder = new StringBuilder(s.Length);
                for (int i = words.Length - 1; i > 0; i--)
                {
                    builder.Append(words[i]);
                    builder.Append(' ');
                }

                if (words.Length > 0) builder.Append(words[0]);
                return builder.ToString();
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public void ReverseWords(char[] s)
            {
                if (s == null || s.Length < 2) return;
                InvertBetween(s, 0, s.Length - 1);
                var left = 0;
                int right;
                while (left < s.Length)
                {
                    right = left;
                    while (right < s.Length && s[right] != ' ') right++;
                    InvertBetween(s, left, right - 1);
                    left = right + 1;
                }
            }

            private void InvertBetween(char[] s, int left, int right)
            {
                if (left > right) return;
                while (left < right)
                {
                    var tmp = s[left];
                    s[left] = s[right];
                    s[right] = tmp;
                    left++;
                    right--;
                }
            }
        }
    }

    namespace p7
    {
        public class Solution
        {
            public bool IsValid(string s)
            {
                var stack = new Stack<char>(s.Length);
                foreach (var ch in s)
                {
                    if (IsLeft(ch))
                    {
                        stack.Push(ch);
                    }
                    else
                    {
                        if (stack.Count == 0 || !Match(stack.Pop(), ch)) return false;
                    }
                }

                return stack.Count == 0;
            }

            private bool IsLeft(char ch)
            {
                return ch == '(' || ch == '[' || ch == '{';
            }

            private bool Match(char left, char right)
            {
                return (left == '(' && right == ')') || (left == '[' && right == ']') || (left == '{' && right == '}');
            }
        }
    }

    namespace p8
    {
        public class Solution
        {
            public string LongestPalindrome(string s)
            {
                if (string.IsNullOrEmpty(s) || s.Length == 1) return s;
                var dict = new Dictionary<char, List<int>>();
                var rs = s.Substring(0, 1);
                for (var i = 0; i < s.Length; i++)
                {
                    var ch = s[i];
                    if (!dict.ContainsKey(ch)) dict[ch] = new List<int>();
                    dict[ch].Add(i);
                }

                foreach (var entry in dict)
                {
                    var list = entry.Value;
                    if (list.Count < 2) continue;
                    var left = 0;
                    for (left = 0; left < list.Count - 1; left++)
                    {
                        int right;
                        for (right = list.Count - 1; right > left; right--)
                        {
                            var leftIdx = list[left];
                            var rightIdx = list[right];
                            var len = rightIdx - leftIdx + 1;
                            if (len <= rs.Length) break;
                            if (IsPalindrome(s, leftIdx, rightIdx))
                            {
                                rs = s.Substring(leftIdx, len);
                                break;
                            }
                        }
                    }
                }

                return rs;
            }

            private static bool IsPalindrome(string s, int left, int right)
            {
                while (left < right)
                {
                    if (s[left] != s[right]) return false;
                    left++;
                    right--;
                }

                return true;
            }
        }
    }

    namespace p9
    {
        public class Solution
        {
            class StringWrapper : IEquatable<StringWrapper>
            {
                public string Val { get; set; }

                private char[] _arr = null;

                private string _sortedVal = null;

                public char[] Arr
                {
                    get
                    {
                        if (_arr == null)
                        {
                            _arr = Val.ToCharArray();
                            Array.Sort(_arr);
                        }

                        return _arr;
                    }
                }

                public string SortedVal
                {
                    get
                    {
                        if (_sortedVal == null)
                        {
                            var builder = new StringBuilder(Arr.Length);
                            foreach (var ch in Arr)
                            {
                                builder.Append(ch);
                            }

                            _sortedVal = builder.ToString();
                        }

                        return _sortedVal;
                    }
                }

                public bool Equals(StringWrapper other)
                {
                    if (ReferenceEquals(this, other)) return true;
                    return SortedEqual(this, other);
                }

                private static bool SortedEqual(StringWrapper str1, StringWrapper str2)
                {
                    if (str1 == null || str2 == null || str1.Val.Length != str2.Val.Length) return false;
                    var arr1 = str1.Arr;
                    var arr2 = str2.Arr;
                    for (int i = 0; i < arr1.Length; i++)
                    {
                        if (arr1[i] != arr2[i]) return false;
                    }

                    return true;
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    if (ReferenceEquals(this, obj)) return true;
                    if (obj.GetType() != this.GetType()) return false;
                    return Equals((StringWrapper) obj);
                }

                public override int GetHashCode()
                {
                    return SortedVal.GetHashCode();
                }
            }

            public IList<IList<string>> GroupAnagrams(string[] strs)
            {

                var rs = new List<IList<string>>();
                if (strs == null || strs.Length == 0) return rs;
                var dict = new Dictionary<StringWrapper, List<StringWrapper>>();
                foreach (var str in strs)
                {
                    var wrapper = new StringWrapper
                    {
                        Val = str
                    };
                    if (!dict.ContainsKey(wrapper))
                    {
                        dict[wrapper] = new List<StringWrapper>();
                    }

                    dict[wrapper].Add(wrapper);
                }

                foreach (var entry in dict)
                {
                    rs.Add(entry.Value.ConvertAll(input => input.Val));
                }

                return rs;
            }
        }
    }

    namespace p10
    {
        public class Solution
        {
            private List<int> indexList = new List<int>();

            public int Trap(int[] height)
            {
                if (height == null || height.Length == 0) return 0;
                var sum = 0;
                var heighest = height.Max(elem => elem);
                for (int i = 1; i <= heighest; i++)
                {
                    bool maybeHigher;
                    sum += TrapOnOneLevel(height, i, out maybeHigher);
                    if (!maybeHigher) break;
                }

                return sum;
            }

            private int TrapOnOneLevel(int[] height, int ground, out bool maybeHigher) //return 0 means no higher level
            {
                var sum = 0;
                indexList.Clear();
                for (int i = 0; i < height.Length; i++)
                {
                    if (height[i] >= ground)
                    {
                        indexList.Add(i);
                    }
                }

                if (indexList.Count < 2)
                {
                    maybeHigher = false;
                    return 0;
                }

                for (int i = 0; i < indexList.Count - 1; i++)
                {
                    sum += indexList[i + 1] - indexList[i] - 1;
                }

                maybeHigher = true;
                return sum;
            }
        }
    }

    namespace p10.better
    {
        public class Solution
        {
            public int Trap(int[] height)
            {
                if (height == null || height.Length < 3) return 0;
                var idx = 0;
                while (idx < height.Length && height[idx] == 0) idx++;
                if (idx == height.Length) return 0;
                var sum = 0;
                while (true)
                {
                    var next = FindNext(height, idx);
                    if (next == -1 || height[next] == 0) break;
                    sum += TrapBetween(height, left: idx, right: next);
                    idx = next;
                }

                return sum;
            }

            private int FindNext(int[] height, int left)
            {
                var nextIdx = -1;
                for (int i = left + 1; i < height.Length; i++)
                {
                    if (height[i] >= height[left])
                    {
                        nextIdx = i;
                        break;
                    }
                    else if (nextIdx == -1 || height[nextIdx] < height[i])
                    {
                        nextIdx = i;
                    }
                }

                return nextIdx;
            }

            private int TrapBetween(int[] height, int left, int right)
            {

                var h = Math.Min(height[left], height[right]);
                var sum = h * (right - left - 1);
                for (int i = left + 1; i < right; i++)
                {
                    sum -= height[i];
                }

                return sum;
            }
        }
    }

    namespace p10.Answer
    {
        public class Solution
        {
            public int trap(int[] h)
            {

                int ans = 0;
                Stack<int> s = new Stack<int>();
                for (int i = 0; i < h.Length;)
                {
                    if (s.Count == 0 || h[s.Peek()] >= h[i])
                        s.Push(i++);
                    else
                    {
                        int curr = s.Pop();
                        // if nothing in left no water can be filled
                        if (s.Count == 0)
                            continue;

                        //s.peek() : left larger index
                        //i : larger index
                        //curr is curent height
                        ans += (Math.Min(h[s.Peek()], h[i]) - h[curr]) * (i - s.Peek() - 1);
                    }

                }

                return ans;
            }
        }

    }

    namespace p11
    {
        public class Solution
        {
            public void SetZeroes(int[][] matrix)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return;
                var rowSet = new HashSet<int>(matrix.Length);
                var colSet = new HashSet<int>(matrix[0].Length);
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (matrix[i][j] == 0)
                        {
                            rowSet.Add(i);
                            colSet.Add(j);
                        }
                    }
                }

                foreach (var idx in rowSet)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[idx][j] = 0;
                    }
                }

                foreach (var idx in colSet)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        matrix[i][idx] = 0;
                    }
                }
            }
        }
    }

    namespace p12
    {
        public class Solution
        {
            public void Rotate(int[][] matrix)
            {
                if (matrix == null || matrix.Length < 2) return;
                var n = matrix.Length;
                for (int i = 0; i < (n >> 1); i++)
                {
                    for (int j = i; j < n - 1 - i; j++)
                    {
                        RotateAt(matrix, i, j);
                    }
                }
            }

            private void RotateAt(int[][] matrix, int i, int j)
            {
                var temp = matrix[i][j];
                var shift = j;
                var n = matrix.Length;
                matrix[i][j] = matrix[n - 1 - j][i];
                matrix[n - 1 - j][i] = matrix[n - 1 - i][n - 1 - j];
                matrix[n - 1 - i][n - 1 - j] = matrix[j][n - 1 - i];
                matrix[j][n - 1 - i] = temp;
            }
        }
    }

    namespace p13
    {
        public class Solution
        {
            public IList<int> SpiralOrder(int[][] matrix)
            {
                var rs = new List<int>();
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return rs;
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                var n = (Math.Min(cols, rows) + 1) / 2;
                for (int i = 0; i < n; i++)
                {
                    Travel(matrix,i,rs);
                }
                return rs;
            }

            private void Travel(int[][] matrix, int idx, List<int> rs)
            {
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                for (int i = idx; i < cols - idx; i++)
                {
                    rs.Add(matrix[idx][i]);
                }

                for (int i = idx + 1; i < rows - idx; i++)
                {
                    rs.Add(matrix[i][cols - 1 - idx]);
                }

                if (rows - idx - 1 > idx)
                {
                    for (int i = cols - 1 - idx - 1; i >= idx; i--)
                    {
                        rs.Add(matrix[rows - idx - 1][i]);
                    }
                }

                if (idx < cols - 1 - idx)
                {
                    for (int i = rows - 1 - idx - 1; i > idx; i--)
                    {
                        rs.Add(matrix[i][idx]);
                    }
                }
            }
        }
    }
}