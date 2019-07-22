using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security.Cryptography;

namespace Leetcode.leetcode.facebook.SearchAndSort
{
    namespace p1
    {
        public class Solution
        {
            public int Search(int[] nums, int target)
            {
                if (nums == null || nums.Length == 0) return -1;
                return SubSearch(nums, 0, nums.Length - 1, target);
            }

            private int SubSearch(int[] nums, int left, int right, int target)
            {
                if (left > right) return -1;
                if (right - left < 4)
                {
                    for (var i = left; i <= right; i++)
                    {
                        if (nums[i] == target) return i;
                    }

                    return -1;
                }

                if (nums[left] < nums[right])
                {
                    var midIdx = (left + right) >> 1;
                    if (nums[midIdx] == target) return midIdx;
                    if (nums[midIdx] < target)
                    {
                        return SubSearch(nums, midIdx + 1, right, target);
                    }
                    else return SubSearch(nums, left, midIdx - 1, target);
                }
                else
                {
                    var midIdx = (left + right) >> 1;
                    if (nums[midIdx] == target) return midIdx;
                    if (nums[midIdx] > nums[right])
                    {
                        if (nums[left] <= target && target < nums[midIdx])
                        {
                            return SubSearch(nums, left, midIdx - 1, target);
                        }
                        else return SubSearch(nums, midIdx + 1, right, target);
                    }
                    else
                    {
                        if (nums[midIdx] < target && target <= nums[right])
                        {
                            return SubSearch(nums, midIdx + 1, right, target);
                        }
                        else return SubSearch(nums, left, midIdx - 1, target);
                    }
                }
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            private const int Len = 5;

            public int[] SearchRange(int[] nums, int target)
            {
                if (nums == null || nums.Length == 0) return new[] {-1, -1};
                var first = FindFirst(nums, 0, nums.Length - 1, target);
                if (first == -1) return new[] {-1, -1};
                var last = FindLast(nums, first, nums.Length - 1, target);
                return new[] {first, last};
            }

            private int FindFirst(int[] nums, int left, int right, int target)
            {
                if (left > right) return -1;
                if (right - left <= Len)
                {
                    for (var i = left; i <= right; i++)
                    {
                        if (nums[i] == target) return i;
                    }

                    return -1;
                }

                var mid = (left + right) >> 1;
                if (nums[mid] == target)
                {
                    if (nums[mid - 1] < target) return mid;
                    else return FindFirst(nums, left, mid - 1, target);
                }
                else if (nums[mid] > target)
                {
                    return FindFirst(nums, left, mid - 1, target);
                }
                else return FindFirst(nums, mid + 1, right, target);

            }

            private int FindLast(int[] nums, int left, int right, int target)
            {
                if (left > right) return -1;
                if (right - left <= Len)
                {
                    for (var i = right; i >= left; i--)
                    {
                        if (nums[i] == target) return i;
                    }

                    return -1;
                }

                var mid = (left + right) >> 1;
                if (nums[mid] == target)
                {
                    if (nums[mid + 1] > target) return mid;
                    else return FindLast(nums, mid + 1, right, target);
                }
                else if (nums[mid] > target)
                {
                    return FindLast(nums, left, mid - 1, target);
                }
                else
                {
                    return FindLast(nums, mid + 1, right, target);
                }
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public double MyPow(double x, int n)
            {
                if (n == 0) return 1;
                if (Math.Abs(x - 1.0) < Double.Epsilon) return 1.0;
                if (n == int.MinValue)
                {
                    return MyPow(x, n + 1) / x;
                }

                var negative = n < 0;
                n = Math.Abs(n);
                var rs = Pow(x, n);
                if (negative) return 1.0 / rs;
                else return rs;
            }

            private double Pow(double x, int n)
            {
                if (n == 0) return 1;
                if (n == 1) return x;
                var times = 1;
                var baseNum = x;
                var sumNum = 1.0;
                var sum = 0;
                while (sum < n - times)
                {
                    sumNum *= baseNum;
                    baseNum *= baseNum;
                    sum += times;
                    times <<= 1;
                }

                return sumNum * Pow(x, n - sum);
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public class MyComparer : Comparer<int[]>
            {
                public override int Compare(int[] x, int[] y)
                {
                    if (x[1] - y[1] != 0) return x[1] - y[1];
                    return x[0] - y[0];
                }
            }

            public int[][] Merge(int[][] intervals)
            {
                if (intervals == null || intervals.Length == 0) return intervals;
                var comparer = new MyComparer();
                Array.Sort(intervals, comparer);
                var rs = new List<int[]>(intervals.Length);
                var merged = intervals[0];
                for (var i = 1; i < intervals.Length; i++)
                {
                    var interval = intervals[i];
                    if (CanMergeLeft(merged, interval))
                    {
                        merged = MergeLeft(merged, interval);
                    }
                    else
                    {
                        rs.Add(merged);
                        merged = interval;
                    }
                }

                rs.Add(merged);
                var rrs = new LinkedList<int[]>();
                merged = rs[rs.Count - 1];
                for (var i = rs.Count - 2; i > -1; i--)
                {
                    var interval = rs[i];
                    if (CanMergeLeft(interval, merged))
                    {
                        merged = MergeLeft(interval, merged);
                    }
                    else
                    {
                        rrs.AddFirst(merged);
                        merged = interval;
                    }
                }

                rrs.AddFirst(merged);
                return rrs.ToArray();
            }

            private bool CanMergeLeft(int[] left, int[] right)
            {
                return left[1] >= right[0];
            }



            private int[] MergeLeft(int[] left, int[] right)
            {
                var rightBound = right[1];
                var leftBound = Math.Min(left[0], right[0]);
                return new[] {leftBound, rightBound};
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            private const int Len = 5;

            public int FindPeakElement(int[] nums)
            {
                if (nums == null || nums.Length == 0) return -1;
                if (nums.Length == 1) return 0;
                if (nums[0] > nums[1]) return 0;
                if (nums[nums.Length - 1] > nums[nums.Length - 2]) return nums.Length - 1;
                return SubSearch(nums, 1, nums.Length - 2);
            }

            private int SubSearch(int[] nums, int left, int right)
            {
                if (right - left <= Len)
                {
                    for (var i = left; i <= right; i++)
                    {
                        if (nums[i] > nums[i - 1] && nums[i] > nums[i + 1]) return i;
                    }

                    return -1;
                }

                var mid = (left + right) >> 1;
                if (nums[mid] > nums[mid - 1])
                {
                    if (nums[mid] > nums[mid + 1]) return mid;
                    else return SubSearch(nums, mid + 1, right);
                }
                else
                {
                    return SubSearch(nums, left, mid - 1);
                }
            }
        }
    }

    namespace p6
    {
        /* The isBadVersion API is defined in the parent class VersionControl.
      bool IsBadVersion(int version); */
        public class VersionControl
        {
            private const int First = 1702766719;

            protected bool IsBadVersion(int version)
            {
                return version >= First;
            }
        }

        public class Solution : VersionControl
        {
            private const int Len = 5;

            public int FirstBadVersion(int n)
            {
                if (IsBadVersion(1)) return 1;
                return SubSearch(1, n);
            }

            private int SubSearch(int left, int right) //left is good ,right is bad
            {
                if (!IsBadVersion(right - 1)) return right;
                if (right - left <= Len)
                {
                    for (var i = left; i <= right; i++)
                    {
                        if (IsBadVersion(i)) return i;
                    }

                    return -1;
                }

                var l = left;
                var r = right;
                var mid = l + ((r - l) >> 1);
                while (!IsBadVersion(mid))
                {
                    l = mid;
                    mid = l + ((r - l) >> 1);
                }

                return SubSearch(l, mid);
            }


        }
    }

    namespace p7
    {
        public class Solution
        {
            public int[] Intersection(int[] nums1, int[] nums2)
            {
                Array.Sort(nums1);
                Array.Sort(nums2);
                var i = 0;
                var j = 0;
                var rs = new List<int>();
                while (i < nums1.Length && j < nums2.Length)
                {
                    if (nums1[i] == nums2[j])
                    {
                        var val = nums1[i];
                        rs.Add(val);
                        while (i < nums1.Length && nums1[i] == val) i++;
                        while (j < nums2.Length && nums2[j] == val) j++;
                    }
                    else if (nums1[i] < nums2[j]) i++;
                    else j++;
                }

                return rs.ToArray();
            }
        }
    }

    namespace p8
    {
        public class Solution
        {
            public int[] Intersect(int[] nums1, int[] nums2)
            {
                var rs = new List<int>();
                var i = 0;
                var j = 0;
                Array.Sort(nums1);
                Array.Sort(nums2);
                while (i < nums1.Length && j < nums2.Length)
                {
                    if (nums1[i] == nums2[j])
                    {
                        var val = nums1[i];
                        while (i < nums1.Length && j < nums2.Length && nums1[i] == val && nums2[j] == val)
                        {
                            rs.Add(val);
                            i++;
                            j++;
                        }
                    }
                    else if (nums1[i] < nums2[j])
                    {
                        i++;
                    }
                    else j++;
                }

                return rs.ToArray();
            }
        }
    }
}