using System;

namespace Leetcode.hifreq2018.dp
{
    namespace p1
    {
        public class Solution
        {
            public int LengthOfLIS(int[] nums)
            {
                if (nums == null || nums.Length == 0) return 0;
                else if (nums.Length == 1) return 1;
                int len = nums.Length;
                dict = new int[len+1][];
                for (int i = 0; i < len + 1; i++)
                {
                    dict[i] = new int[len];
                    for (int j = 0; j < len; j++)
                    {
                        dict[i][j] = -1;
                    }
                }

                return MaxLen(nums, -1, len - 1);
            }

            private int[][] dict;
            private int MaxLen(int[] nums, int maxIdx, int i)
            {
                int len = nums.Length;
                if (i == -1) return 0;
                if (maxIdx == -1)
                {
                    if (dict[len][i] != -1)
                    {
                        return dict[len][i];
                    }
                }
                else
                {
                    if (dict[maxIdx][i] != -1)
                    {
                        return dict[maxIdx][i];
                    }
                }
                int val = nums[i];
                if (maxIdx == -1 || nums[maxIdx] > val)
                {
                    int rs = Math.Max(MaxLen(nums, i, i - 1)+1, MaxLen(nums, maxIdx,  i - 1));
                    if (maxIdx == -1)
                    {
                        dict[len][i] = rs;
                    }
                    else
                    {
                        dict[maxIdx][i] = rs;
                    }
                    return rs;
                }
                else
                {
                    int rs = MaxLen(nums, maxIdx, i - 1);
                    return dict[maxIdx][i] = rs;
                }
            }
        }
    }
}