using System.Collections.Generic;
using Leetcode.leetcode.discovery.microsoft.tree_graph.p1;

namespace Leetcode.leetcode.mock.microsoft.p190627
{
    namespace p1
    {
        public class Solution
        {
            public void Merge(int[] nums1, int m, int[] nums2, int n)
            {
                if (nums1 == null || nums2 == null || n == 0) return;
                var i = m - 1;
                var j = n - 1;
                var k = m + n - 1;
                while (true)
                {
                    if (j < 0 || i == k) break;
                    if (i < 0 || nums1[i] < nums2[j])
                    {
                        nums1[k--] = nums2[j--];
                    }
                    else
                    {
                        nums1[k--] = nums1[i--];
                    }
                }
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
            public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
            {
                if (p.right != null)
                {
                    return FindMostLeft(p.right);
                }
                else
                {
                    var list = new List<TreeNode>();
                    FindAncestors(root,p,list);
                    if (list.Count == 0) return null;
                    TreeNode mostLeft = null;
                    foreach (var node in list)
                    {
                        if (node.val > p.val && (mostLeft == null || mostLeft.val > node.val)) mostLeft = node;
                    }

                    return mostLeft;
                }
            }


            private void FindAncestors(TreeNode root, TreeNode node,List<TreeNode> list)
            {
                if (root == node) return;
                if (root.val < node.val)
                {
                    list.Add(root);
                    FindAncestors(root.right,node,list);
                }
                else
                {
                    list.Add(root);
                    FindAncestors(root.left,node,list);
                }
            }
            
            private TreeNode FindMostLeft(TreeNode node)
            {
                while (node.left != null) node = node.left;
                return node;
            }
        }
    }

}