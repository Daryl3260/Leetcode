using System;
using System.Collections.Generic;

namespace Leetcode.leetcode.triva.P237
{

    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
        }
    }

    public class Solution
    {
        public void DeleteNode(ListNode node)
        {
            node.val = node.next.val;
            node.next = node.next.next;
        }
    }

    namespace p2
    {
        public class Solution
        {
            public IList<string> FindMissingRanges(int[] nums, int lower, int upper)
            {
                if (nums == null || nums.Length == 0)
                {
                    if (lower == upper) return new List<string> {$"{lower}"};
                    else return new List<string> {$"{lower}->{upper}"};
                }

                return Convert(FindMissing(nums, lower, upper));
            }

            private IList<string> Convert(IList<int[]> list)
            {
                var rs = new List<string>();
                foreach (var pair in list)
                {
                    if (pair[0] == pair[1])
                    {
                        rs.Add($"{pair[0]}");
                    }
                    else
                    {
                        rs.Add($"{pair[0]}->{pair[1]}");
                    }
                }

                return rs;
            }

            private IList<int[]> FindMissing(int[] nums, int lower, int upper)
            {
                var rs = new List<int[]>();
                var i = 0;
                if (nums[0] > lower)
                {
                    rs.Add(new[] {lower, nums[0] - 1});
                }

                while (true)
                {
                    if (i == nums.Length - 1) break;
                    if (nums[i] == nums[i + 1] || nums[i] == nums[i + 1] - 1) i++;
                    else
                    {
                        rs.Add(new[] {nums[i] + 1, nums[i + 1] - 1});
                        i++;
                    }
                }

                if (nums[nums.Length - 1] < upper)
                {
                    rs.Add(new[] {nums[nums.Length - 1] + 1, upper});
                }

                return rs;
            }
        }
    }

    namespace p3
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
            public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
            {
                if (p.right != null)
                {
                    var ptr = p.right;
                    while (ptr.left != null) ptr = ptr.left;
                    return ptr;
                }

                var list = new List<TreeNode>();
                FindAncestors(root, p, list);
                for (var i = list.Count - 1; i > -1; i--)
                {
                    if (list[i].val > p.val) return list[i];
                }

                return null;
            }

            private bool FindAncestors(TreeNode root, TreeNode target, IList<TreeNode> list)
            {
                if (root == null) return false;
                list.Add(root);
                if (root == target) return true;
                else
                {
                    if (root.val < target.val)
                    {
                        if (FindAncestors(root.right, target, list)) return true;
                        else
                        {
                            list.RemoveAt(list.Count - 1);
                            return false;
                        }
                    }
                    else
                    {
                        if (FindAncestors(root.left, target, list)) return true;
                        else
                        {
                            list.RemoveAt(list.Count - 1);
                            return false;
                        }
                    }
                }
            }

            private TreeNode FindLargest(TreeNode root)
            {
                while (root.right != null) root = root.right;
                return root;
            }
        }
    }

    namespace p4
    {

        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node next;

            public Node()
            {
            }

            public Node(int _val, Node _left, Node _right, Node _next)
            {
                val = _val;
                left = _left;
                right = _right;
                next = _next;
            }

            public class Solution
            {
                public Node Connect(Node root)
                {
                    if (root == null) return null;
                    var p = root;
                    while (true)
                    {
                        if (p.left == null) break;
                        var nextFirst = p.left;
                        while (p != null)
                        {
                            p.left.next = p.right;
                            if(p.next!=null) p.right.next = p.next.left;
                            p = p.next;
                        }
                        p = nextFirst;
                    }
                    return root;
                }
            }
        }
    }

    //next node of Binary Tree's in-order traversal
    namespace p5
    {
        public class TreeNode
        {
            public TreeNode Parent { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public int Val { get; set; }
        }

        public class Solution
        {
            public TreeNode Next(TreeNode target)
            {
                if (target.Right != null)
                {
                    var p = target.Right;
                    while (p.Left != null) p = p.Left;
                    return p;
                }
                else
                {
                    while (true)
                    {
                        if (target.Parent == null) return null;
                        else if (target.Parent.Right == target) target = target.Parent;
                        else return target.Parent;
                    }
                }
            }
        }
}

    namespace p5p
    {
        public class TreeNode
        {
            public int Val { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
        }

        public class Solution
        {
            public TreeNode Next(TreeNode root, TreeNode target)
            {
                if (target.Right != null)
                {
                    var p = target.Right;
                    while (p.Left != null) p = p.Left;
                    return p;
                }
                else
                {
                    var list = new List<TreeNode>();
                    FindAncestors(root, target, list);
                    for (var i = list.Count - 1; i > 0; i--)
                    {
                        if (list[i - 1].Left == list[i]) return list[i];
                    }
                    return null;
                }
            }

            public bool FindAncestors(TreeNode root, TreeNode target, IList<TreeNode> ancestors)
            {
                if (root == null) return false;
                ancestors.Add(root);
                if (root == target) return true;
                if (FindAncestors(root.Left, target, ancestors) || FindAncestors(root.Right, target, ancestors))
                    return true;
                else
                {
                    ancestors.RemoveAt(ancestors.Count-1);
                    return false;
                }
            }
        }
}

    namespace p6
    {
        public class Solution
        {
            public static void Test()
            {
                var matrix = new int[][]
                {
                    new[] {1, 2, 3, 4, 5, 6, 7},
                    new[] {1, 2, 3, 4, 5, 6, 7},
                    new[] {1, 2, 3, 4, 5, 6, 7},
                    new[] {1, 2, 3, 4, 5, 6, 7},
                    new[] {1, 2, 3, 4, 5, 6, 7},
                    new[] {1, 2, 3, 4, 5, 6, 7},
                    new[] {7,6,5,4,3,2,1},
                };
                new Solution().Rotate(matrix);
                foreach (var row in matrix)
                {
                    foreach (var elem in row)
                    {
                        Console.Write($"{elem}\t");
                    }
                    Console.WriteLine($"");
                }
            }
            public void Rotate(int[][] matrix)
            {
                var len = matrix.Length;
                var n = len >> 1;
                for (var i = 0; i < n; i++)
                {
                    for (var j = i; j < len - 1 - i; j++)
                    {
                        var temp = matrix[i][j];
                        matrix[i][j] = matrix[len - 1 - j][i];
                        matrix[len - 1 - j][i] = matrix[len - 1 - i][len - 1 - j];
                        matrix[len - 1 - i][len - 1 - j] = matrix[j][len - 1 - i];
                        matrix[j][len - 1 - i] = temp;
                    }
                }
            }
        }
}

    namespace p7
    {
        public class Solution {
            public string LongestPalindrome(string s)
            {
                if (string.IsNullOrEmpty(s)) return string.Empty;
                var maxLeft = 0;
                var maxRight = 0;
                var doubleList = new List<int>();
                for (var i = 0; i < s.Length - 1; i++)
                {
                    if (s[i] == s[i + 1])
                    {
                        SearchDouble(s,i,out var left,out var right);
                        if (right - left > maxRight - maxLeft)
                        {
                            maxLeft = left;
                            maxRight = right;
                        }
                    }
                    SearchSingle(s,i,out var leftSingle,out var rightSingle);
                    if (rightSingle - leftSingle > maxRight - maxLeft)
                    {
                        maxLeft = leftSingle;
                        maxRight = rightSingle;
                    }
                }

                return s.Substring(maxLeft, maxRight - maxLeft + 1);
            }

            private void SearchSingle(string s, int mid, out int left, out int right)
            {
                left = mid;
                right = mid;
                while (true)
                {
                    if (left < 0 || right == s.Length || s[left] != s[right])
                    {
                        left++;
                        right--;
                        return;
                    }
                    else
                    {
                        left--;
                        right++;
                    }
                }
            }

            private void SearchDouble(string s, int midL, out int left, out int right)
            {
                left = midL;
                right = midL + 1;
                while (true)
                {
                    if (left < 0 || right == s.Length || s[left] != s[right])
                    {
                        left++;
                        right--;
                        return;
                    }
                    else
                    {
                        left--;
                        right++;
                    }
                }
            }
            
        }
    }
    
}