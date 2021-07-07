using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Timers;

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

    namespace p3
    {
        public class Solution
        {
            public IList<int> RightSideView(TreeNode root)
            {
                if (root == null) return new List<int>();

                var queue = new Queue<Tuple<TreeNode, int>>();
                var rs = new List<int>();
                var currentLevel = 0;
                var currentRight = root;
                queue.Enqueue(new Tuple<TreeNode, int>(root, currentLevel));

                while (queue.Any())
                {
                    var first = queue.Dequeue();
                    var parent = first.Item1;
                    var level = first.Item2;
                    if (currentLevel < level)
                    {
                        rs.Add(currentRight.val);
                        currentLevel = level;
                        currentRight = parent;
                    }

                    if (parent.right != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(parent.right, level + 1));
                    }

                    if (parent.left != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(parent.left, level + 1));
                    }
                }

                rs.Add(currentRight.val);

                return rs;
            }
        }
    }

    namespace p4
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
            public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
            {
                return null;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            // minus one at final return
            public int DiameterOfBinaryTree(TreeNode root)
            {
                if (root == null || (root.left == null && root.right == null)) return 0;
                SubDia(root, out var sl, out var sd);
                return sd - 1;
            }

            public void SubDia(TreeNode root, out int singleLeg, out int diameter)
            {
                if (root == null)
                {
                    singleLeg = 0;
                    diameter = 0;
                }
                else
                {
                    SubDia(root.left, out int ls, out int ld);
                    SubDia(root.right, out int rs, out int rd);
                    singleLeg = Math.Max(ls, rs) + 1;
                    var maxSubDia = Math.Max(ld, rd);
                    diameter = Math.Max(maxSubDia, ls + rs + 1);
                }
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public bool IsSubtree(TreeNode root, TreeNode subRoot)
            {
                if (IsSameTree(root, subRoot)) return true;
                if (root.left != null && IsSubtree(root.left, subRoot))
                {
                    return true;
                }
                if (root.right != null && IsSubtree(root.right, subRoot))
                {
                    return true;
                }

                return false;
            }

            // subroo
            public bool IsSameTree(TreeNode root, TreeNode subRoot)
            {
                if (root == null)
                {
                    return subRoot == null;
                }

                if (subRoot == null || root.val != subRoot.val)
                {
                    return false;
                }

                return IsSameTree(root.left, subRoot.left) && IsSameTree(root.right, subRoot.right);
            }
        }
    }
}
