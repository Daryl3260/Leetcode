using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode_cn.interview2021_spring.search
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
            public IList<int> RightSideView(TreeNode root)
            {
                if (root == null)
                {
                    return new List<int>();
                }
                else
                {
                    return LevelTraverse(root);
                }
            }

            public List<int> LevelTraverse(TreeNode root)
            {
                Queue<Tuple<TreeNode, int>> queue = new Queue<Tuple<TreeNode, int>>();

                queue.Enqueue(new Tuple<TreeNode, int>(root, 0));

                var currentLevel = 0;
                var currentList = new List<TreeNode>();
                var rs = new List<int>();

                while (queue.Any())
                {
                    var first = queue.Dequeue();
                    var node = first.Item1;
                    var level = first.Item2;

                    if (currentLevel == level)
                    {
                        currentList.Add(node);
                    }
                    else
                    {
                        rs.Add(currentList[currentList.Count - 1].val);
                        currentList.Clear();
                        currentLevel = level;
                        currentList.Add(node);
                    }

                    if (node.left != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.left, level + 1));
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.right, level + 1));
                    }
                }

                rs.Add(currentList[currentList.Count - 1].val);

                return rs;
            }
        }
    }
}
