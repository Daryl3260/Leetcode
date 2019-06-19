using System;

namespace Leetcode.leetcode_cn.digit_prob
{
    namespace p1
    {
        public class Solution
        {
            public int SingleNumber(int[] nums)
            {
                int sum = 0;
                foreach (var num in nums)
                {
                    sum ^= num;
                }
                return sum;
            }
        }
    }
}