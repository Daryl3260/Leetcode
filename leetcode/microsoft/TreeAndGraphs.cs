using System.Collections.Generic;
using Leetcode.leetcode.ListAndStack.p9;

namespace Leetcode.leetcode.microsoft.TreeAndGraph
{
    namespace p1
    {
        /**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
        public class Solution
        {
            public bool IsValidBST(TreeNode root)
            {
                return IsValidSub(root, null, null);
            }

            private bool IsValidSub(TreeNode root, int? leftBound, int? rightBound)
            {
                if (root == null) return true;
                if ((leftBound != null && root.val <= leftBound.Value) ||
                    (rightBound != null && root.val >= rightBound.Value)) return false;
                return IsValidSub(root.left, leftBound, root.val) && IsValidSub(root.right, root.val, rightBound);
            }
        }
    }

    namespace p2
    {
        /**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
        public class Solution
        {
            public IList<int> InorderTraversal(TreeNode root)
            {
                var rs = new List<int>();
                SubTraverse(root, rs);
                return rs;
            }

            private void SubTraverse(TreeNode root, List<int> rs)
            {
                if (root == null) return;
                SubTraverse(root.left, rs);
                rs.Add(root.val);
                SubTraverse(root.right, rs);
            }
        }
    }
    namespace p2.better
    {
        public class Solution
        {
            public IList<int> InorderTraversal(TreeNode root)
            {
                var stack = new Stack<TreeNode>();
                var node = root;
                while (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }
                var rs = new List<int>();
                while (stack.Count > 0)
                {
                    var top = stack.Pop();
                    rs.Add(top.val);
                    if (top.right != null)
                    {
                        node = top.right;
                        while (node != null)
                        {
                            stack.Push(node);
                            node = node.left;
                        }
                    }
                }

                return rs;
            }

        }
    }
}