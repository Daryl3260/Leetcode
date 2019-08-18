using System;
using System.Collections.Generic;

namespace Leetcode.leetcode.recursion.recursionV2
{
    namespace p1
    {
        public class Solution
        {
            public int[] SortArray(int[] nums)
            {
                if (nums == null) return nums;
                SubSort(nums, 0, nums.Length - 1);
                return nums;
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

    namespace p2
    {
        public class Solution
        {
            public static void Test()
            {
                Console.WriteLine($"{new Solution().TotalNQueens(5)}");
            }

            public int TotalNQueens(int n)
            {
                if (n == 1) return 1;
                if (n < 4) return 0;
                var count = 0;
                SubSearch(n, new List<int>(), ref count);
                return count;
            }

            private void SubSearch(int n, List<int> precedence, ref int count)
            {
                if (precedence.Count == n)
                {
                    count++;
                }
                else
                {
                    var rowIdx = precedence.Count;
                    bool[] unavailable = new bool[n];
                    for (var i = 0; i < precedence.Count; i++)
                    {
                        var row = i;
                        var col = precedence[i];
                        unavailable[col] = true;
                        var rowDiff = Math.Abs(rowIdx - row);
                        var left = col - rowDiff;
                        var right = col + rowDiff;
                        if (left > -1) unavailable[left] = true;
                        if (right < n) unavailable[right] = true;
                    }

                    for (var i = 0; i < n; i++)
                    {
                        if (unavailable[i]) continue;
                        precedence.Add(i);
                        SubSearch(n, precedence, ref count);
                        precedence.RemoveAt(precedence.Count - 1);
                    }
                }
            }
        }
    }

    namespace p3
    {
        /**
 * // This is the robot's control interface.
 * // You should not implement it, or speculate about its implementation
 * interface Robot {
 *     // Returns true if the cell in front is open and robot moves into the cell.
 *     // Returns false if the cell in front is blocked and robot stays in the current cell.
 *     public bool Move();
 *
 *     // Robot will stay in the same cell after calling turnLeft/turnRight.
 *     // Each turn will be 90 degrees.
 *     public void TurnLeft();
 *     public void TurnRight();
 *
 *     // Clean the current cell.
 *     public void Clean();
 * }
 */
        interface Robot
        {
            bool Move();
            void TurnLeft();
            void TurnRight();
            void Clean();
        }

        /**
 * // This is the robot's control interface.
 * // You should not implement it, or speculate about its implementation
 * interface Robot {
 *     // Returns true if the cell in front is open and robot moves into the cell.
 *     // Returns false if the cell in front is blocked and robot stays in the current cell.
 *     public bool Move();
 *
 *     // Robot will stay in the same cell after calling turnLeft/turnRight.
 *     // Each turn will be 90 degrees.
 *     public void TurnLeft();
 *     public void TurnRight();
 *
 *     // Clean the current cell.
 *     public void Clean();
 * }
 */
        class Solution
        {
            private HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();

            private int[][]
                directions = new[] {new[] {-1, 0}, new[] {0, 1}, new[] {1, 0}, new[] {0, -1}}; //up, right,down,left

            public void CleanRoom(Robot robot)
            {
                SubClean(robot, 0, 0, 0);
            }

            private void SubClean(Robot robot, int row, int col, int direction) //direction 0,1,2,3
            {
                var here = new Tuple<int, int>(row, col);
                visited.Add(here);
                robot.Clean();
                for (var i = 0; i < directions.Length; i++)
                {
                    var d = (direction + i) % 4;
                    var x = directions[d][0] + row;
                    var y = directions[d][1] + col;
                    if (!visited.Contains(new Tuple<int, int>(x, y)) && robot.Move())
                    {
                        SubClean(robot, x, y, d);
                        GoBack(robot);
                    }

                    robot.TurnRight();
                }

            }

            private void GoBack(Robot robot)
            {
                robot.TurnRight();
                robot.TurnRight();
                robot.Move();
                robot.TurnRight();
                robot.TurnRight();
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public void SolveSudoku(char[][] board)
            {
                
            }
        }
    }
}