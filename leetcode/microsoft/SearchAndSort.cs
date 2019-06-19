using System;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode.microsoft.SearchAndSort
{
    namespace p1
    {
        public class Solution
        {
            public int RemoveDuplicates(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                if (nums.Length == 1) return 1;
                int back = 0;
                int front = 0;
                while (true)
                {
                    while (front < nums.Length && nums[back] == nums[front]) front++;
                    if (front == nums.Length) break;
                    nums[++back] = nums[front];
                }

                return back + 1;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public void Merge(int[] nums1, int m, int[] nums2, int n)
            {
                if (nums1 == null || nums2 == null) return;
                var all = m + n;
                var k = all - 1;
                var i = m - 1;
                var j = n - 1;
                while (i > -1 || j > -1)
                {
                    if (i > -1 && j > -1)
                    {
                        var val1 = nums1[i];
                        var val2 = nums2[j];
                        if (val1 < val2)
                        {
                            nums1[k--] = nums2[j--];
                        }
                        else
                        {
                            nums1[k--] = nums1[i--];
                        }
                    }
                    else if (i > -1)
                    {
                        break;
                    }
                    else
                    {
                        nums1[k--] = nums2[j--];
                    }
                }
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public void SortColors(int[] nums)
            {
                QuickSort(nums);
            }

            private void QuickSort(int[] nums)
            {
                if (nums == null || nums.Length < 2) return;
                SubSort(nums, 0, nums.Length - 1);
            }

            private void SubSort(int[] nums, int left, int right)
            {
                if (left >= right) return;
                var mid = Partition(nums, left, right);
                SubSort(nums, left, mid - 1);
                SubSort(nums, mid + 1, right);
            }

            private int Partition(int[] nums, int left, int right)
            {
                var pivot = nums[left];
                while (left < right)
                {
                    while (left < right && nums[right] >= pivot) right--;
                    nums[left] = nums[right];
                    while (left < right && nums[left] <= pivot) left++;
                    nums[right] = nums[left];
                }

                nums[left] = pivot;
                return left;
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public int FindMin(int[] nums)
            {
                return FindMinBetween(nums, 0, nums.Length - 1);
            }

            private int FindMinBetween(int[] nums, int left, int right)
            {
                if (right - left < 5)
                {
                    List<int> list = new List<int>();
                    for (int i = left; i <= right; i++)
                    {
                        list.Add(nums[i]);
                    }

                    return list.Min(elem => elem);
                }
                else if (nums[left] < nums[right])
                {
                    return nums[left];
                }
                else
                {
                    var mid = (left + right) >> 1;
                    if (nums[mid] < nums[mid - 1]) return nums[mid];
                    if (nums[mid] < nums[right])
                    {
                        return FindMinBetween(nums, left, mid - 1);
                    }
                    else
                    {
                        return FindMinBetween(nums, mid + 1, right);
                    }

                }
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public int FindMin(int[] nums)
            {
                return FindMinBetween(nums, 0, nums.Length - 1);
            }

            private int FindMinBetween(int[] nums, int left, int right)
            {
                if (right - left < 5)
                {
                    List<int> list = new List<int>();
                    for (int i = left; i <= right; i++)
                    {
                        list.Add(nums[i]);
                    }

                    return list.Min(elem => elem);
                }
                else if (nums[left] < nums[right])
                {
                    return nums[left];
                }
                else if (nums[left] > nums[right])
                {
                    var mid = (left + right) >> 1;
                    if (nums[mid] < nums[mid - 1]) return nums[mid];
                    if (nums[mid] <= nums[right])
                    {
                        return FindMinBetween(nums, left, mid - 1);
                    }
                    else
                    {
                        return FindMinBetween(nums, mid + 1, right);
                    }
                }
                else
                {
                    var mid = (left + right) >> 1;
                    return Math.Min(FindMinBetween(nums, left, mid), FindMinBetween(nums, mid + 1, right));
                }
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public bool SearchMatrix(int[][] matrix, int target)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return false;
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                var x = 0;
                var y = cols - 1;
                while (true)
                {
                    if (x < 0 || y < 0 || x == rows || y == cols) return false;
                    var val = matrix[x][y];
                    if (val == target) return true;
                    else if (val < target)
                    {
                        x++;
                    }
                    else
                    {
                        y--;
                    }
                }
            }
        }
    }

    namespace p7
    {
        public class Solution
        {
            public int Search(int[] nums, int target)
            {
                if (nums == null || nums.Length < 0) return -1;
                return Subsearch(nums, 0, nums.Length - 1, target);
            }

            private int Subsearch(int[] nums, int left, int right, int target)
            {
                if (left > right) return -1;
                if (right - left <= 4)
                {
                    for (int i = left; i <= right; i++)
                    {
                        if (nums[i] == target) return i;
                    }

                    return -1;
                }

                if (nums[left] < nums[right])
                {
                    var mid = (left + right) >> 1;
                    var midVal = nums[mid];
                    if (midVal == target) return mid;
                    if (midVal < target) return Subsearch(nums, mid + 1, right, target);
                    else return Subsearch(nums, left, mid - 1, target);
                }
                else
                {
                    var mid = (left + right) >> 1;
                    var midVal = nums[mid];
                    if (midVal == target) return mid;
                    if (midVal > nums[left])
                    {
                        if (midVal < target) return Subsearch(nums, mid + 1, right, target);
                        else
                        {
                            if (nums[left] <= target)
                            {
                                return Subsearch(nums, left, mid - 1, target);
                            }
                            else
                            {
                                return Subsearch(nums, mid + 1, right, target);
                            }
                        }
                    }
                    else
                    {
                        if (target < midVal) return Subsearch(nums, left, mid - 1, target);
                        else if (target <= nums[right]) return Subsearch(nums, mid + 1, right, target);
                        else return Subsearch(nums, left, mid - 1, target);
                    }
                }
            }
        }
    }

    namespace p8
    {
        public class Solution
        {
            public bool SearchMatrix(int[,] matrix, int target)
            {
                var rows = matrix.GetLength(0);
                var cols = matrix.GetLength(1);
                var x = 0;
                var y = cols - 1;
                while (true)
                {
                    if (x < 0 || x == rows || y < 0 || y == cols) return false;
                    if (matrix[x, y] == target) return true;
                    else if (matrix[x, y] < target)
                    {
                        x++;
                    }
                    else
                    {
                        y--;
                    }
                }
            }
        }
    }

    namespace p9
    {
        public class Solution
        {
            private const int limit = 10;
            public double FindMedianSortedArrays(int[] nums1, int[] nums2)
            {
                int[] single = null;
                if (nums1 == null || nums1.Length == 0)
                {
                    single = nums2;
                }
                else if (nums2 == null || nums2.Length == 0)
                {
                    single = nums1;
                }

                if (single != null)
                {
                    var len = single.Length;
                    if ((len & 1) == 1)
                    {
                        return single[len >> 1];
                    }
                    else
                    {
                        return (single[len >> 1] + single[(len >> 1) - 1]) / 2.0;
                    }
                }

                var totalNum = nums1.Length + nums2.Length;
                if ((totalNum & 1) == 1)
                {
                    return FindNth(nums1, 0, nums1.Length, nums2, 0, nums2.Length, totalNum / 2 + 1);
                }
                else
                {
                    var right = FindNth(nums1, 0, nums1.Length, nums2, 0, nums2.Length, totalNum / 2 + 1);
                    var left = FindNth(nums1, 0, nums1.Length, nums2, 0, nums2.Length, totalNum / 2);
                    return (left + right) / 2.0;
                }
            }

            private int FindNth(int[] nums1, int l1, int h1, int[] nums2, int l2, int h2,int nth)
            {
                var totalNum = h1 - l1 + h2 - l2;
                if (totalNum <= limit)
                {
                    var list = new List<int>(totalNum);
                    for (int i = l1; i < h1; i++) list.Add(nums1[i]);
                    for (int i = l2; i < h2; i++) list.Add(nums2[i]);
                    list.Sort(Comparer<int>.Default);
                    return list[nth - 1];
                }
                if (h1 - l1 < h2 - l2)
                {
                    var tmp = nums1;
                    nums1 = nums2;
                    nums2 = tmp;
                    var temp = l1;
                    l1 = l2;
                    l2 = temp;
                    temp = h1;
                    h1 = h2;
                    h2 = temp;
                }

                var mid1 = (l1 + h1) >> 1;
                var insert = FindInsertionLocation(nums2, l2, h2, nums1[mid1]);
                var leftNum = mid1 - l1 + insert - l2;
                var rightNum = totalNum - leftNum;
                if (leftNum >= nth) return FindNth(nums1, l1, mid1, nums2, l2, insert, nth);
                else return FindNth(nums1, mid1, h1, nums2,insert, h2,nth-leftNum);
            }

            private int FindInsertionLocation(int[] nums, int l1, int h1, int target)
            {
                if (nums[h1 - 1] < target) return h1;
                if (h1 - l1 <= limit)
                {
                    var idx = h1 - 1;
                    while (l1 <= idx && nums[idx] >= target) idx--;
                    return idx + 1;
                }

                var mid = (l1 + h1) >> 1;
                if (nums[mid] >= target && nums[mid - 1] < target) return mid;
                if (nums[mid] >= target) return FindInsertionLocation(nums, l1, mid, target);
                return FindInsertionLocation(nums, mid, h1, target);
            }
        }
    }
}