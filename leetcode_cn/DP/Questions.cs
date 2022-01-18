using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.DP
{
    namespace p1
    {
        public class Solution
        {
            public int MinPathSum(int[][] grid)
            {
                if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;
                var rows = grid.Length;
                var cols = grid[0].Length;
                if (cols > 1)
                {
                    for (var i = cols - 1; i > 0; i--)
                    {
                        grid[rows - 1][i - 1] += grid[rows - 1][i];
                    }
                }

                if (rows > 1)
                {
                    for (var i = rows - 1; i > 0; i--)
                    {
                        grid[i - 1][cols - 1] += grid[i][cols - 1];
                    }
                }

                for (var i = rows - 2; i > -1; i--)
                {
                    for (var j = cols - 2; j > -1; j--)
                    {
                        grid[i][j] = Math.Min(grid[i + 1][j], grid[i][j + 1]) + grid[i][j];
                    }
                }

                return grid[0][0];
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int ClimbStairs(int n)
            {
                if (n < 4)
                {
                    return n;
                }

                var a0 = 2;
                var a1 = 3;

                for (var i = 4; i <= n; i++)
                {
                    var a2 = a0 + a1;
                    a0 = a1;
                    a1 = a2;
                }

                return a1;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public int CoinChange(int[] coins, int amount)
            {
                if (amount == 0) return 0;
                var arr = new int[amount + 1];

                foreach (var coin in coins)
                {
                    if (coin <= amount)
                    {
                        arr[coin] = 1;
                    }
                }

                for (var i = 1; i <= amount; i++)
                {
                    if (arr[i] == 0)
                    {
                        continue;
                    }

                    foreach (var coin in coins)
                    {
                        if (coin > amount - i)
                        {
                            continue;
                        }

                        var sum = i + coin;
                        if (arr[sum] == 0 || arr[sum] > arr[i] + 1)
                        {
                            arr[sum] = arr[i] + 1;
                        }
                    }
                }

                return arr[amount] == 0 ? -1 : arr[amount];
            }
        }
    }
}
