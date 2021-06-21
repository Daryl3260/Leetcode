using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode_cn.interview2020
{
    namespace p1
    {
        public class Solution
        {
            public IList<int> SpiralOrder(int[][] matrix)
            {
                var m = matrix.Length;
                var n = matrix[0].Length;
                var rs = new List<int>();

                var iterations = Math.Min((m + 1) / 2, (n + 1) / 2);

                for (var i = 0; i < iterations; i++)
                {
                    for (var j = i; j < n - i; j++)
                    {
                        rs.Add(matrix[i][j]);
                    }

                    for (var j = i + 1; j < m - i; j++)
                    {
                        rs.Add(matrix[j][n - 1 - i]);
                    }

                    if (m - 1 - i > i)
                    {
                        for (var j = n - 2 - i; j >= i; j--)
                        {
                            rs.Add(matrix[m - 1 - i][j]);
                        }
                    }

                    if (i < n - 1 - i)
                    {
                        for (var j = m - 2 - i; j > i; j--)
                        {
                            rs.Add(matrix[j][i]);
                        }
                    }
                }

                return rs;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int[][] Merge(int[][] intervals)
            {
                Array.Sort(intervals, MyComparer.Instance);
                var rs = new List<int[]>();
                foreach (int[] interval in intervals)
                {
                    Merge(rs, interval);
                }

                return rs.ToArray();
            }

            public void Merge(List<int[]> rs, int[] candidate)
            {
                while (rs.Any() && CanMerge(rs[rs.Count - 1], candidate))
                {
                    var merged = MergeTwo(rs[rs.Count - 1], candidate);
                    rs.RemoveAt(rs.Count - 1);
                    candidate = merged;
                }

                rs.Add(candidate);
            }

            public int[] MergeTwo(int[] left, int[] right)
            {
                var leftSide = Math.Min(left[0], right[0]);
                return new int[] { leftSide, right[1] };
            }

            public bool CanMerge(int[] left, int[] right)
            {
                return left[1] >= right[0];
            }


            public class MyComparer : IComparer<int[]>
            {
                private MyComparer() { }

                private static MyComparer instance = new MyComparer();

                public static MyComparer Instance => instance;

                public int Compare(int[] x, int[] y)
                {
                    return x[1] - y[1];
                }
            }

        }
    }

    namespace p3
    {
        public class Solution
        {
            public int[] ProductExceptSelf(int[] nums)
            {
                var leftArray = GetLeftArray(nums);
                var rightArray = GetRightArray(nums);
                var rs = new int[nums.Length];
                for (var i = 0; i < nums.Length; i++)
                {
                    var left = GetLeft(leftArray, i);
                    var right = GetRight(rightArray, i);
                    rs[i] = left * right;
                }

                return rs;
            }

            private int GetLeft(int[] leftArr, int idx)
            {
                if (idx == 0) return 1;
                else return leftArr[idx - 1];
            }

            private int GetRight(int[] rightArr, int idx)
            {
                if (idx == rightArr.Length - 1)
                {
                    return 1;
                }
                else
                {
                    return rightArr[idx + 1];
                }
            }

            private int[] GetLeftArray(int[] nums)
            {
                var rs = new int[nums.Length];
                rs[0] = nums[0];

                for (var i = 1; i < nums.Length; i++)
                {
                    rs[i] = rs[i - 1] * nums[i];
                }

                return rs;
            }

            private int[] GetRightArray(int[] nums)
            {
                var rs = new int[nums.Length];
                rs[nums.Length - 1] = nums[nums.Length - 1];
                for (var i = nums.Length - 2; i > -1; i--)
                {
                    rs[i] = rs[i + 1] * nums[i];
                }

                return rs;
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public int LastStoneWeightII(int[] stones)
            {
                var sum = stones.Sum();

                var half = sum / 2;

                var backup = new Dictionary<int, int>[stones.Length];

                for (var i = 0; i < backup.Length; i++)
                {
                    backup[i] = new Dictionary<int, int>();
                }

                var actualHalf = GetHalfSum(stones, backup, 0, half);

                return sum - actualHalf * 2;
            }

            public int GetHalfSum(int[] stones, Dictionary<int, int>[] backup, int startIdx, int threshold)
            {
                if (startIdx == stones.Length) return 0;

                if (backup[startIdx].ContainsKey(threshold))
                {
                    return backup[startIdx][threshold];
                }

                var value = stones[startIdx];
                int rs;

                if (value == threshold)
                {
                    rs = value;
                }
                else if (value > threshold)
                {
                    rs = GetHalfSum(stones, backup, startIdx + 1, threshold);
                }
                else
                {
                    // not taking value
                    var rs1 = GetHalfSum(stones, backup, startIdx + 1, threshold);

                    // taking value
                    var rs2 = GetHalfSum(stones, backup, startIdx + 1, threshold - value) + value;

                    rs = Math.Max(rs1, rs2);
                }

                backup[startIdx][threshold] = rs;

                return rs;
            }
        }
    }

    namespace p2.s2
    {
        public class Solution
        {
            public int LastStoneWeightII(int[] stones)
            {
                var sum = stones.Sum();

                var half = sum / 2;

                var m = stones.Length;

                bool[][] records = new bool[m + 1][];
                for (var i = 0; i < records.Length; i++)
                {
                    records[i] = new bool[half + 1];
                }

                records[0][0] = true;

                for (var i = 0; i < stones.Length; i++)
                {
                    var stone = stones[i];
                    for (var j = 0; j <= half; j++)
                    {
                        if (j < stone)
                        {
                            records[i + 1][j] = records[i][j];
                        }
                        else
                        {
                            records[i + 1][j] = records[i][j] || records[i][j - stone];
                        }
                    }
                }

                for (var j = half; ; j--)
                {
                    if (records[m][j])
                    {
                        return sum - 2 * half;
                    }
                }
            }
        }
    }
}
