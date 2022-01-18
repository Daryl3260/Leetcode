using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.math
{
    namespace p1
    {
        public class Solution
        {
            private static int[] maxList = new int[] { 2, 1, 4, 7, 4, 8, 3, 6, 4, 7 };

            public int Reverse(int x)
            {
                if (x == int.MinValue)
                {
                    return 0;
                }

                if (x == 0)
                {
                    return 0;
                }
                else if (x > 0)
                {
                    return ReversePositive(x);
                }
                else
                {
                    return -ReversePositive(-x);
                }
            }

            public int ReversePositive(int x)
            {
                // 2147483647
                var arr = new List<int>();
                while (x > 0)
                {
                    var digit = x % 10;
                    arr.Add(digit);
                    x /= 10;
                }

                if (arr.Count == maxList.Length)
                {
                    for (var i = 0; i < maxList.Length; i++)
                    {
                        if (arr[i] < maxList[i]) break;
                        else if (arr[i] > maxList[i])
                        {
                            return 0;
                        }
                    }
                }

                var sum = 0;
                for (var i = 0; i < arr.Count; i++)
                {
                    sum *= 10;
                    sum += arr[i];
                }

                return sum;
            }
        }
    }
}
