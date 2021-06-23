using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.tree
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    namespace p1
    {
        public class Solution
        {
            public bool IsValidBST(TreeNode root)
            {
                if (root == null) return true;
                if (root.left != null)
                {
                    if (root.left.val >= root.val || !IsValidBST(root.left))
                    {
                        return false;
                    }
                }
                if (root.right != null)
                {
                    if (root.right.val <= root.val || !IsValidBST(root.right))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }

    namespace p1.v2
    {
        public class Solution
        {
            public bool IsValidBST(TreeNode root)
            {
                return TraverseMid(root, long.MinValue, long.MaxValue);
            }

            public bool TraverseMid(TreeNode root, long pre, long post)
            {
                if (root == null) return true;

                if (root.val <= pre || root.val >= post)
                {
                    return false;
                }

                return TraverseMid(root.left, pre, root.val) && TraverseMid(root.right, root.val, post);
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
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
        public class Solution
        {
            public int MaxPathSum(TreeNode root)
            {
                if (root == null) return 0;
                if (root.left == null && root.right == null) return root.val;
                return MaxSubSum(root, out var single);
            }

            // singleleft and singleright will contain the root node anyway
            public int MaxSubSum(TreeNode root, out int single)
            {
                if (root.left != null && root.right != null)
                {
                    var leftMax = MaxSubSum(root.left, out var leftSingle);
                    var rightMax = MaxSubSum(root.right, out var rightSingle);

                    var lrMax = Math.Max(leftMax, rightMax);

                    single = Math.Max(Math.Max(leftSingle, rightSingle), 0) + root.val;

                    var rootIncluded = Math.Max(single, leftSingle + root.val + rightSingle);

                    return Math.Max(lrMax, rootIncluded);
                }
                else if (root.left != null || root.right != null)
                {
                    var oneArm = root.left != null ? root.left : root.right;

                    var oneArmMax = MaxSubSum(oneArm, out var oneArmSingle);

                    single = oneArmSingle > 0 ? oneArmSingle + root.val : root.val;

                    return Math.Max(oneArmMax, single);
                }
                else
                {
                    single = root.val;
                    return root.val;
                }
            }
        }
    }
}
