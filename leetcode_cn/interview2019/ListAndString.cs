using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.leetcode_cn.interview2019.ListAndString
{
    namespace p1
    {
        public class Solution
        {
            public string AddBinary(string a, string b)
            {
                if (a.Length > b.Length)
                {
                    var builder = new StringBuilder(a.Length);
                    for (int i = 0; i < a.Length - b.Length; i++)
                    {
                        builder.Append('0');
                    }

                    builder.Append(b);
                    b = builder.ToString();
                }
                else if (a.Length < b.Length)
                {
                    var builder = new StringBuilder(b.Length);
                    for (int i = 0; i < b.Length - a.Length; i++)
                    {
                        builder.Append('0');
                    }

                    builder.Append(a);
                    a = builder.ToString();
                }

                var len = a.Length;
                var extra = false;
                var rs = new List<char>();
                for (int i = 0; i < len; i++)
                {
                    var ch1 = a[a.Length - 1 - i];
                    var ch2 = b[b.Length - 1 - i];
                    var sum = (ch1 - '0') + (ch2 - '0') + (extra ? 1 : 0);
                    if (sum > 1)
                    {
                        extra = true;
                        rs.Add((char) (sum - 2 + '0'));
                    }
                    else
                    {
                        extra = false;
                        rs.Add((char) (sum + '0'));
                    }
                }

                if (extra)
                {
                    rs.Add('1');
                }

                var rsBuilder = new StringBuilder();
                for (int i = rs.Count - 1; i > -1; i--)
                {
                    rsBuilder.Append(rs[i]);
                }

                return rsBuilder.ToString();
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int StrStr(string haystack, string needle)
            {
                if (haystack == null || needle == null) return -1;
                else if (needle == "") return 0;
                else if (haystack.Length < needle.Length) return -1;
                for (int i = 0; i <= haystack.Length - needle.Length; i++)
                {
                    var j = 0;
                    int? next = null;
                    for (j = 0; j < needle.Length; j++)
                    {
                        if (haystack[i + j] == needle[0]) next = i + j;
                        if (haystack[i + j] != needle[j])
                        {
                            break;
                        }
                    }

                    if (j == needle.Length) return i;
                    if (next != null && next.Value > i)
                    {
                        i = next.Value - 1;
                    }
                }

                return -1;
            }
        }
        //116 ms

    }

    /*
     "mississippj"
"sippj"
     */
    namespace p3
    {
        public class Solution
        {
            public string LongestCommonPrefix(string[] strs)
            {
                if (strs == null || strs.Length == 0) return "";
                var shortest = strs.Min((elem) => elem.Length);
                var builder = new StringBuilder(shortest);
                for (var i = 0; i < shortest; i++)
                {
                    var ch = strs[0][i];
                    foreach (var str in strs)
                    {
                        if (str[i] != ch)
                        {
                            return builder.ToString();
                        }
                    }

                    builder.Append(ch);
                }

                return builder.ToString();
            }
        }

        //216 ms
    }

    namespace p3.better
    {
        public class Solution
        {
            public string LongestCommonPrefix(string[] strs)
            {
                if (strs == null || strs.Length == 0) return "";
                var shortest = strs.Min(elem => elem.Length);
                var template = strs[0].Substring(0, shortest);
                var list = new List<Task<int>>();
                foreach (var str in strs)
                {
                    list.Add(Task.Run(() => CommonLength(template, str)));
                }

                Task.WaitAll(tasks: list.ToArray());
                var commonLen = list.Min(elem => elem.Result);
                return template.Substring(0, commonLen);
//                Task<int> t = Task.Run((() => CommonLength(template,strs[0])));

            }

            private static int CommonLength(string template, string str)
            {
                int len = template.Length;
                for (int i = 0; i < len; i++)
                {
                    if (template[i] != str[i]) return i;
                }

                return len;
            }
        }

        //192 ms
    }

    namespace p4
    {
        public class Solution
        {
            public int ArrayPairSum(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                Array.Sort(nums);
                var n = nums.Length / 2;
                var sum = 0;
                for (int i = 0; i < n; i++)
                {
                    sum += nums[i << 1];
                }

                return sum;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public int[] TwoSum(int[] numbers, int target)
            {
                var rs = new int[2];
                int left = 0;
                int right = numbers.Length - 1;
                while (true)
                {
                    if (left >= right) break;
                    var sum = numbers[left] + numbers[right];
                    if (sum == target)
                    {
                        rs[0] = left + 1;
                        rs[1] = right + 1;
                        break;
                    }

                    if (sum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }

                return rs;
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public int FindMaxConsecutiveOnes(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                var max = 0;
                var back = -1;
//                var front = -1;
                var idx = 0;
                var foundOne = false;
                while (idx < nums.Length)
                {
                    var val = nums[idx];
                    if (val == 1)
                    {
                        if (!foundOne)
                        {
                            foundOne = true;
                            back = idx;
//                            front = idx;
                        }
                    }
                    else
                    {
                        if (foundOne)
                        {
                            var len = idx - back;
                            max = Math.Max(max, len);
                            foundOne = false;
                        }
                    }

                    idx++;
                }

                if (foundOne)
                {
                    max = Math.Max(idx - back, max);
                }

                return max;
            }
        }
    }

    namespace p7
    {
        public class Solution
        {
            public int MinSubArrayLen(int s, int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                var ss = nums.Sum(a => a);
                if (ss < s) return 0;
                else if (ss == s) return nums.Length;
                int front = 0;
                int back = 0;
                int sum = 0;
                int? minLen = null;
                while (true)
                {
                    if (sum >= s)
                    {
                        if (minLen.HasValue)
                        {
                            minLen = Math.Min(minLen.Value, front - back);
                        }
                        else
                        {
                            minLen = front - back;
                        }

                        sum -= nums[back++];
                    }
                    else
                    {
                        if (front == nums.Length) break;
                        sum += nums[front++];
                    }
                }

                if (!minLen.HasValue) throw new Exception("minLen has no value");
                else return minLen.Value;
            }

        }
    }

    namespace p8
    {
        public class Solution
        {
            public IList<int> GetRow(int rowIndex)
            {
                if (rowIndex == 0) return new List<int> {1};
                if (rowIndex == 1) return new List<int> {1, 1};
                var list1 = new List<int> {1, 1};
                var list2 = new List<int>();
                while (rowIndex > 1)
                {
                    list2.Clear();
                    list2.Add(1);
                    for (int i = 0; i < list1.Count - 1; i++)
                    {
                        list2.Add(list1[i] + list1[i + 1]);
                    }

                    list2.Add(1);
                    var tmp = list1;
                    list1 = list2;
                    list2 = tmp;
                    rowIndex--;
                }

                return list1;
            }
        }
    }

    namespace p9
    {
        public class Solution
        {
            public string ReverseWords(string s)
            {
//                string[] arr = s.Split(' ');
                var arr = RemoveInternalSpace(s);
                if (arr.Count == 0) return "";
                var len = arr.Count;
                var builder = new StringBuilder();
                for (int i = len - 1; i > 0; i--)
                {
                    builder.Append(arr[i]);
                    builder.Append(' ');
                }

                builder.Append(arr[0]);
                return builder.ToString();
            }

            private List<string> RemoveInternalSpace(string s)
            {
                var rs = new List<string>();
                var back = 0;
                var front = 0;
                var on = false;
                while (front < s.Length)
                {
                    var ch = s[front];
                    if (ch == ' ')
                    {
                        if (on)
                        {
                            rs.Add(s.Substring(back, front - back));
                            on = false;
                        }
                    }
                    else
                    {
                        if (!on)
                        {
                            on = true;
                            back = front;
                        }
                    }

                    front++;
                }

                if (on)
                {
                    rs.Add(s.Substring(back, front - back));
                }

                return rs;
            }

            private string Invert(string str)
            {
                var builder = new StringBuilder(str.Length);
                for (int i = 0; i < str.Length; i++)
                {
                    builder.Append(str[str.Length - 1 - i]);
                }

                return builder.ToString();
            }
        }
    }

    namespace p10
    {
        public class Solution
        {
            public string ReverseWords(string s)
            {
                if (string.IsNullOrEmpty(s)) return "";
                var back = 0;
                var front = 0;
                var rs = new StringBuilder(s.Length);
//                var on = false;
                while (front < s.Length)
                {
                    var ch = s[front];
                    if (ch == ' ')
                    {
                        
                        rs.Append(Reverse(s.Substring(back, front - back)));
                        
                        rs.Append(ch);
                        front++;
                        back = front;
                    }

                    front++;
                }

                rs.Append(Reverse(s.Substring(back, front - back)));
                return rs.ToString();
            }

            private string Reverse(string str)
            {
                var builder = new StringBuilder(str.Length);
                for (int i = str.Length - 1; i > -1; i--)
                {
                    builder.Append(str[i]);
                }

                return builder.ToString();
            }
            
        }
    }
}