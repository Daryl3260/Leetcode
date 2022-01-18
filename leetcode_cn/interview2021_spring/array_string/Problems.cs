using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.interview2021_spring.array_string
{
    namespace p1
    {
        public class Solution
        {
            public int LengthOfLongestSubstring(string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return 0;
                }

                if (s.Length == 1)
                {
                    return 1;
                }

                var dict = new Dictionary<char, int>();

                var l = 0;
                var r = 1;
                dict[s[l]] = l;

                var maxLen = 1;

                while (r < s.Length)
                {
                    var rLetter = s[r];
                    if (dict.TryGetValue(rLetter, out var index))
                    {
                        var len = r - l;
                        maxLen = Math.Max(maxLen, len);

                        for (int i = l; i <= index; i++)
                        {
                            dict.Remove(s[i]);
                        }

                        l = index + 1;
                        dict[rLetter] = r;
                        r++;
                    }
                    else
                    {
                        dict[rLetter] = r;
                        r++;
                    }
                }

                maxLen = Math.Max(maxLen, r - l);

                return maxLen;
            }
        }
    }

    namespace p1.s2
    {
        public class Solution
        {
            public int LengthOfLongestSubstring(string s)
            {
                if (string.IsNullOrEmpty(s)) return 0;
                if (s.Length == 1) return 1;

                var set = new HashSet<char>();

                var l = 0;

                var maxLen = 1;

                set.Add(s[0]);

                for (var r = 1; r < s.Length; r++)
                {
                    var rLetter = s[r];
                    if (!set.Contains(rLetter))
                    {
                        set.Add(rLetter);
                    }
                    else
                    {
                        while (set.Contains(rLetter))
                        {
                            set.Remove(s[l++]);
                        }
                        set.Add(rLetter);
                    }

                    maxLen = Math.Max(maxLen, r - l + 1);
                }

                return maxLen;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public double FindMedianSortedArrays(int[] nums1, int[] nums2)
            {
                if (nums1.Length == 0 && nums2.Length == 0)
                {
                    return 0;
                }

                int len = nums1.Length + nums2.Length;

                if (len % 2 == 0)
                {
                    var mid1 = GetIthNum(nums1, nums2, len / 2);
                    var mid2 = GetIthNum(nums1, nums2, len / 2 - 1);
                    return (mid1 + mid2) / 2.0;
                }
                else
                {
                    var mid = GetIthNum(nums1, nums2, len / 2);
                    return mid;
                }
            }

            public int GetIthNum(int[] nums1, int[] nums2, int idx)
            {
                int i1 = 0;
                int i2 = 0;

                int rs = 0;

                while (idx > -1)
                {
                    if (i1 < nums1.Length && (i2 >= nums2.Length || nums1[i1] <= nums2[i2]))
                    {
                        rs = nums1[i1];
                        i1++;
                        idx--;
                    }
                    else
                    {
                        rs = nums2[i2];
                        i2++;
                        idx--;
                    }
                }

                return rs;
            }
        }
    }

    namespace p2.s2
    {
        public class Solution
        {
            public double FindMedianSortedArrays(int[] nums1, int[] nums2)
            {
                var len = nums1.Length + nums2.Length;
                if (len % 2 == 0)
                {
                    var mid1 = FindIthElem(nums1, 0, nums2, 0, len / 2 - 1);
                    var mid2 = FindIthElem(nums1, 0, nums2, 0, len / 2);
                    return (mid1 + mid2) / 2.0;
                }
                else
                {
                    var mid = FindIthElem(nums1, 0, nums2, 0, len / 2);
                    return mid;
                }
            }

            public int FindIthElem(int[] nums1, int start1, int[] nums2, int start2, int idx)
            {
                if (idx <= 5)
                {
                    return FindIthElemFallback(nums1, start1, nums2, start2, idx);
                }

                if (start1 >= nums1.Length)
                {
                    return nums2[start2 + idx];
                }

                if (start2 >= nums2.Length)
                {
                    return nums1[start1 + idx];
                }

                var compareIdx = (idx - 1) / 2;
                var idx1 = Math.Min(start1 + compareIdx, nums1.Length - 1);
                var idx2 = Math.Min(start2 + compareIdx, nums2.Length - 1);

                if (nums1[idx1] >= nums2[idx2])
                {
                    idx -= (idx2 - start2 + 1);
                    return FindIthElem(nums1, start1, nums2, idx2 + 1, idx);
                }
                else
                {
                    idx -= (idx1 - start1 + 1);
                    return FindIthElem(nums1, idx1 + 1, nums2, start2, idx);
                }
            }

            public int FindIthElemFallback(int[] nums1, int start1, int[] nums2, int start2, int idx)
            {
                var rs = 0;
                while (idx > -1)
                {
                    if (start1 < nums1.Length && (start2 >= nums2.Length || nums1[start1] <= nums2[start2]))
                    {
                        rs = nums1[start1++];
                        idx--;
                    }
                    else
                    {
                        rs = nums2[start2++];
                        idx--;
                    }
                }
                return rs;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public IList<IList<int>> ThreeSum(int[] nums)
            {
                Array.Sort(nums);
                var rs = new List<IList<int>>();
                for (var i = 0; i < nums.Length; i++)
                {
                    if (i > 0 && nums[i] == nums[i - 1]) continue;
                    FindSums(nums, i, rs);
                }
                return rs;
            }

            public void FindSums(int[] nums, int idxTaken, List<IList<int>> rs)
            {
                var valTaken = nums[idxTaken];
                var targetValue = -valTaken;
                var l = idxTaken + 1;
                var r = nums.Length - 1;

                while (l < r)
                {
                    if (l == idxTaken)
                    {
                        l++;
                    }
                    else if (r == idxTaken)
                    {
                        r--;
                    }
                    else
                    {
                        var sum = nums[l] + nums[r];
                        if (sum == targetValue)
                        {
                            var list = new List<int> { valTaken, nums[l], nums[r] };
                            rs.Add(list);
                            while (true)
                            {
                                l++;
                                if (l >= r || nums[l] > nums[l - 1]) break;
                            }
                        }
                        else if (sum < targetValue)
                        {
                            l++;
                        }
                        else
                        {
                            r--;
                        }
                    }
                }
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public int[] ProductExceptSelf(int[] nums)
            {
                if (nums == null || nums.Length == 0)
                {
                    return nums;
                }

                int[] leftProd = new int[nums.Length];
                int[] rightProd = new int[nums.Length];

                leftProd[0] = 1;

                for (var i = 1; i < nums.Length; i++)
                {
                    leftProd[i] = leftProd[i - 1] * nums[i - 1];
                }

                rightProd[nums.Length - 1] = 1;

                for (var i = nums.Length - 2; i > -1; i--)
                {
                    rightProd[i] = rightProd[i + 1] * nums[i + 1];
                }

                var rs = new int[nums.Length];

                for (var i = 0; i < nums.Length; i++)
                {
                    rs[i] = leftProd[i] * rightProd[i];
                }

                return rs;
            }
        }
    }

    namespace p4.s2
    {
        public class Solution
        {
            public int[] ProductExceptSelf(int[] nums)
            {
                if (nums == null || nums.Length == 0)
                {
                    return nums;
                }

                int[] leftProd = new int[nums.Length];

                leftProd[0] = 1;

                for (var i = 1; i < nums.Length; i++)
                {
                    leftProd[i] = leftProd[i - 1] * nums[i - 1];
                }

                var rProd = 1;

                for (var i = nums.Length - 2; i > -1; i--)
                {
                    rProd *= nums[i + 1];
                    leftProd[i] *= rProd;
                }

                return leftProd;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public string AddStrings(string num1, string num2)
            {
                var len = Math.Max(num1.Length, num2.Length);
                var list = new List<char>(len + 1);

                var extra = 0;

                for (var i = 0; i < len; i++)
                {
                    var val1 = i >= num1.Length ? 0 : num1[num1.Length - 1 - i] - '0';
                    var val2 = i >= num2.Length ? 0 : num2[num2.Length - 1 - i] - '0';

                    var sumByte = val1 + val2 + extra;
                    if (sumByte >= 10)
                    {
                        sumByte -= 10;
                        extra = 1;
                    }
                    else
                    {
                        extra = 0;
                    }

                    list.Add((char)(sumByte + '0'));
                }

                if (extra == 1)
                {
                    list.Add('1');
                }

                var builder = new StringBuilder();

                for (var i = list.Count - 1; i > -1; i--)
                {
                    builder.Append(list[i]);
                }

                return builder.ToString();
            }
        }
    }
}
