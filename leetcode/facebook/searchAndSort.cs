using System;

namespace Leetcode.leetcode.facebook.recursion.searchAndSort
{
    namespace p1
    {
        public class Solution
        {
            public int Divide(int dividend, int divisor)
            {
                if (dividend == divisor) return 1;
                if (divisor == 1)
                {
                    return dividend;
                }

                if (divisor == -1)
                {
                    if (dividend == int.MinValue) return int.MaxValue;
                    else return -dividend;
                }

                var top = (long) dividend;
                var bottom = (long) divisor;
                var sign = (dividend > 0 && divisor > 0 || dividend < 0 && divisor < 0);
                if (Math.Abs(top) < Math.Abs(bottom)) return 0;
                if (Math.Abs(top) == Math.Abs(bottom))
                {
                    if (sign) return 1;
                    else return -1;
                }
                var rs = PositiveDivide(Math.Abs(top), Math.Abs(bottom));
                if (sign) return (int)rs;
                else return (int) (-rs);
            }

            private static long PositiveDivide(long top, long bottom)//both positive
            {
                if (top < bottom) return 0;
                var sum = 0L;
                var baseBottom = bottom;
                var rs = 0L;
                var baseRS = 1L;
                while (true)
                {
                    if (sum + baseBottom > top)
                    {
                        return rs + PositiveDivide(top - sum,bottom);
                    }
                    else
                    {
                        sum += baseBottom;
                        baseBottom += baseBottom;
                        rs += baseRS;
                        baseRS += baseRS;
                    }
                }
            }
        }
    }
}