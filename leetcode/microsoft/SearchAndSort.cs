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
                
            }

            private void QuickSort(int[] nums)
            {
                
            }
        }
    }
}