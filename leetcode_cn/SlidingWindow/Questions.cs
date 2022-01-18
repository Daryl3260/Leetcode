using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.SlidingWindow
{
    namespace p1
    {
        public class Solution
        {
            public int RemoveDuplicates(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                if (nums.Length == 1) return 1;
                var i = 0;
                var j = 0;

                while (true)
                {
                    while (j < nums.Length && nums[i] == nums[j])
                    {
                        j++;
                    }

                    if (j == nums.Length)
                    {
                        break;
                    }
                    else
                    {
                        nums[++i] = nums[j++];
                    }
                }

                return i + 1;
            }
        }
    }
}
