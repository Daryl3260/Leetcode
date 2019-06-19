using System;
using System.Collections.Generic;

namespace Leetcode.leetcode.array
{
    namespace p1
    {
        public class Solution
        {
            public int PivotIndex(int[] nums)
            {
                if (nums == null || nums.Length == 0) return -1;
                int idx = 0;
                int leftSum = 0;
                int rightSum = 0;
                for (int i = 1; i < nums.Length; i++)
                {
                    rightSum += nums[i];
                }

                while (idx < nums.Length && leftSum != rightSum)
                {
                    leftSum += nums[idx];
                    if (idx < nums.Length - 1)
                    {
                        rightSum -= nums[idx + 1];
                    }

                    idx++;
                }

                if (idx < nums.Length) return idx;
                else return -1;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int DominantIndex(int[] nums)
            {
                Dictionary<int, int> dict = new Dictionary<int, int>(nums.Length);
                int maxIdx = 0;
                dict[nums[0]] = 0;
                for (int i = 1; i < nums.Length; i++)
                {
                    if (dict.ContainsKey(nums[i])) continue;
                    else
                    {
                        dict[nums[i]] = i;
                        if (nums[maxIdx] < nums[i]) maxIdx = i;
                    }
                }

                var maxKey = nums[maxIdx];
                foreach (var key in dict.Keys)
                {
                    if (key != maxKey)
                    {
                        if (maxKey < (key << 1)) return -1;
                    }
                }

                return maxIdx;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public int[] PlusOne(int[] digits)
            {
                var idx = FindFirstNotNine(digits);
                if (idx == -1)
                {
                    int len = digits.Length;
                    var rs = new int[len + 1];
                    rs[0] = 1;
                    return rs;
                }
                else
                {
                    digits[idx]++;
                    for (int i = idx + 1; i < digits.Length; i++)
                    {
                        digits[i] = 0;
                    }

                    return digits;
                }
            }

            private int FindFirstNotNine(int[] digits)
            {
                int idx = digits.Length - 1;
                while (idx > -1 && digits[idx] == 9) idx--;
                return idx;
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public int[] FindDiagonalOrder(int[][] matrix)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return new int[0];
                int rows = matrix.Length;
                int cols = matrix[0].Length;
                var list = new List<int>(rows * cols);
                var len = Math.Max(rows, cols) + Math.Min(rows, cols) - 1;
                for (int i = 0; i < len; i++)
                {
                    if ((i & 0x1) == 0) //even
                    {
                        if (i < rows)
                        {
                            Traverse(matrix, i, 0, true, list);
                        }
                        else
                        {
                            int d = i - (rows - 1);
                            Traverse(matrix, i - d, 0 + d, true, list);
                        }
                    }
                    else //odd
                    {
                        if (i < cols)
                        {
                            Traverse(matrix, 0, i, false, list);
                        }
                        else
                        {
                            int d = i - (cols - 1);
                            Traverse(matrix, 0 + d, i - d, false, list);
                        }
                    }
                }

                return list.ToArray();
            }

            private void Traverse(int[][] matrix, int i, int j, bool up, List<int> list)
            {
                int rows = matrix.Length;
                int cols = matrix[0].Length;
                while (-1 < i && i < rows && -1 < j && j < cols)
                {
                    list.Add(matrix[i][j]);
                    if (up)
                    {
                        i--;
                        j++;
                    }
                    else
                    {
                        i++;
                        j--;
                    }
                }
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public IList<int> SpiralOrder(int[][] matrix)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return new List<int>();
                var rs = new List<int>();
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                var upper = (Math.Min(rows, cols) - 1) / 2;
                for (var i = 0; i <= upper; i++)
                {
                    for (var j = i; j < cols - i; j++)
                    {
                        rs.Add(matrix[i][j]);
                    }

                    for (var j = i + 1; j < rows - i; j++)
                    {
                        rs.Add(matrix[j][cols - 1 - i]);
                    }

                    if (rows - 1 - i > i)
                    {
                        for (var j = cols - 2 - i; j > i - 1; j--)
                        {
                            rs.Add(matrix[rows - 1 - i][j]);
                        }
                    }

                    if (i < cols - 1 - i)
                    {
                        for (var j = rows - 2 - i; j > i; j--)
                        {
                            rs.Add(matrix[j][i]);
                        }
                    }


                }

                return rs;
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public IList<IList<int>> Generate(int numRows)
            {
                var rs = new List<IList<int>>();
                if (numRows < 1) return rs;
                var oldList = new List<int>();
                var newList = new List<int>();
                oldList.Add(1);
                rs.Add(new List<int>(oldList));
                if (numRows == 1) return rs;
                oldList.Add(1);
                rs.Add(new List<int>(oldList));
                if (numRows == 2) return rs;
                for (var i = 2; i < numRows; i++)
                {
                    newList.Add(1);
                    for (int j = 0; j < oldList.Count - 1; j++)
                    {
                        newList.Add(oldList[j]+oldList[j+1]);
                    }
                    newList.Add(1);
                    var tmp = oldList;
                    oldList = newList;
                    newList = tmp;
                    newList.Clear();
                    rs.Add(new List<int>(oldList));
                }
                return rs;
            }
        }
    }
}