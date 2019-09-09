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
            public static void Test()
            {
                //[['5','3','.','.','7','.','.','.','.'],['6','.','.','1','9','5','.','.','.'],[],[],[],[],[],[],[]
                var board = new char[][]
                {
                    new[] {'5', '3', '.', '.', '7', '.', '.', '.', '.'},
                    new[] {'6', '.', '.', '1', '9', '5', '.', '.', '.'},
                    new[] {'.', '9', '8', '.', '.', '.', '.', '6', '.'},
                    new[] {'8', '.', '.', '.', '6', '.', '.', '.', '3'},
                    new[] {'4', '.', '.', '8', '.', '3', '.', '.', '1'},
                    new[] {'7', '.', '.', '.', '2', '.', '.', '.', '6'},
                    new[] {'.', '6', '.', '.', '.', '.', '2', '8', '.'},
                    new[] {'.', '.', '.', '4', '1', '9', '.', '.', '5'},
                    new[] {'.', '.', '.', '.', '8', '.', '.', '7', '9'}
                };
                new Solution().SolveSudoku(board);
            }

            public void SolveSudoku(char[][] board)
            {
                SubSolve(board, MissingBlock(board), 0);
            }

            private List<Tuple<int, int>> MissingBlock(char[][] board)
            {
                var rs = new List<Tuple<int, int>>();
                for (var i = 0; i < 9; i++)
                {
                    for (var j = 0; j < 9; j++)
                    {
                        if (!IsNum(board[i][j])) rs.Add(new Tuple<int, int>(i, j));
                    }
                }

                return rs;
            }

            private bool IsNum(char ch)
            {
                return '1' <= ch && ch <= '9';
            }

            private bool SubSolve(char[][] board, List<Tuple<int, int>> list, int idx)
            {
                if (idx == list.Count) return true;
                var coordinate = list[idx];
                var row = coordinate.Item1;
                var col = coordinate.Item2;
                var rowUsed = new bool[9];
                var colUsed = new bool[9];
                var blockUsed = new bool[9]; //'.'
                for (var i = 0; i < 9; i++)
                {
                    var ch = board[i][col];
                    if (!IsNum(ch)) continue;
                    colUsed[ch - '1'] = true;
                }

//                for (var i = 0; i < 9; i++)
//                {
//                    var ch = board[row][i];
//                    if (ch == '.') continue;
//                    rowUsed[ch - '1'] = true;
//                }
                foreach (var ch in board[row])
                {
                    if (!IsNum(ch)) continue;
                    rowUsed[ch - '1'] = true;
                }



                var blockRow = row / 3;
                var blockCol = col / 3;
                var blockRowStart = blockRow * 3;
                var blockColStart = blockCol * 3;
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        var ch = board[blockRowStart + i][blockColStart + j];
                        if (IsNum(ch))
                        {
                            blockUsed[ch - '1'] = true;
                        }
                    }
                }

                var available = new List<int>();
                for (var i = 0; i < 9; i++)
                {
                    if (!rowUsed[i] && !colUsed[i] && !blockUsed[i])
                    {
                        available.Add(i);
                    }
                }

                if (available.Count == 0) return false;
                foreach (var num in available)
                {
                    board[row][col] = (char) ('1' + num);
                    if (SubSolve(board, list, idx + 1)) return true;
                }

                board[row][col] = '.';
                return false;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public static void Test()
            {
                var n = 4;
                var k = 2;
                var rs = new Solution().Combine(n, k);
                foreach (var combination in rs)
                {
                    foreach (var num in combination)
                    {
                        Console.Write(num + "\t");
                    }

                    Console.WriteLine();
                }
            }

            public IList<IList<int>> Combine(int n, int k)
            {
                var rs = new List<IList<int>>();
                SubSearch(n, k, 1, new List<int>(), rs);
                return rs;
            }

            private void SubSearch(int n, int k, int start, List<int> list, List<IList<int>> rs)
            {
                if (list.Count == k)
                {
                    rs.Add(new List<int>(list));
                }
                else if (start > n) return;
                else
                {
                    for (var i = start; i <= n; i++)
                    {
                        list.Add(i);
                        SubSearch(n, k, i + 1, list, rs);
                        list.RemoveAt(list.Count - 1);
                    }
                }
            }
        }
    }

    namespace p6
    {

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int x)
            {
                val = x;
            }
        }

        public class Solution
        {
            public bool IsSameTree(TreeNode p, TreeNode q)
            {
                if (p == null && q == null) return true;
                else if (p == null || q == null) return false;
                if (p.val != q.val) return false;
                return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
            }
        }
    }

    namespace p7
    {

        public class Node
        {
            public int val;
            public Node left;
            public Node right;

            public Node()
            {
            }

            public Node(int _val, Node _left, Node _right)
            {
                val = _val;
                left = _left;
                right = _right;
            }

            public class Solution
            {
                public Node TreeToDoublyList(Node root)
                {
                    if (root == null) return null;
                    Extend(root, out var leftMost, out var rightMost);
                    leftMost.left = rightMost;
                    rightMost.right = leftMost;
                    return leftMost;
                }

                private void Extend(Node root, out Node leftMost, out Node rightMost)
                {
                    if (root.left != null)
                    {
                        Extend(root.left, out var ll, out var lr);
                        leftMost = ll;
                        lr.right = root;
                        root.left = lr;
                    }
                    else leftMost = root;

                    if (root.right != null)
                    {
                        Extend(root.right, out var rl, out var rr);
                        rightMost = rr;
                        root.right = rl;
                        rl.left = root;
                    }
                    else rightMost = root;
                }
            }
        }
    }
}