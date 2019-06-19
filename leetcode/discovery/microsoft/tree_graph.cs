using System;
using System.Collections.Generic;

namespace Leetcode.leetcode.discovery.microsoft.tree_graph
{
    namespace p1
    {

// * Definition for a binary tree node.
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
            public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
            {
                var list1 = new List<TreeNode>();
                var list2 = new List<TreeNode>();
                FindAncestors(root, p, list1);
                FindAncestors(root, q, list2);
                var len = Math.Min(list1.Count, list2.Count);
                TreeNode rs = null;
                for (int i = 0; i < len; i++)
                {
                    if (list1[i] == list2[i])
                    {
                        rs = list1[i];
                    }
                    else break;
                }
                return rs;
            }

            private bool FindAncestors(TreeNode root, TreeNode p, List<TreeNode> list)
            {
                if (root == null) return false;
                list.Add(root);
                if (root == p)
                {
                    return true;
                }
                else if(FindAncestors(root.left,p,list)||FindAncestors(root.right,p,list))
                {
                    return true;
                }
                else
                {
                    list.RemoveAt(list.Count - 1);
                    return false;
                }
            }
        }
    }
}