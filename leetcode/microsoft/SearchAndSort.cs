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
                else if(nums[left]>nums[right])
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
}