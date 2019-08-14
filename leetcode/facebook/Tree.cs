using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode.facebook.Tree
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

    public class Node
    {
        public int val;
        public IList<Node> neighbors;

        public Node()
        {
        }

        public Node(int _val, IList<Node> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
    }

    namespace p1
    {
        public class Solution
        {
            public bool IsValidBST(TreeNode root)
            {
                return IsValid(root, null, null);
            }

            private bool IsValid(TreeNode root, int? leftBound, int? rightBound)
            {
                if (root == null) return true;
                var val = root.val;
                if (leftBound.HasValue && leftBound.Value >= val) return false;
                if (rightBound.HasValue && rightBound.Value <= val) return false;
                return IsValid(root.left, leftBound, val) && IsValid(root.right, val, rightBound);
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public void Flatten(TreeNode root)
            {
                if (root == null) return;
                SubFlatten(root);
            }

            private TreeNode SubFlatten(TreeNode root)
            {
                var left = root.left;
                var right = root.right;
                root.left = null;
                var down = root;
                if (left != null)
                {
                    down.right = left;
                    down = SubFlatten(left);
                }

                if (right != null)
                {
                    down.right = right;
                    down = SubFlatten(right);
                }

                return down;
            }
        }
    }

    namespace p3
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
            public int MaxPathSum(TreeNode root)
            {
                SubMax(root,out var maxPath,out var maxSinglePath);
                return maxPath ?? 0;
            }

            private void SubMax(TreeNode root, out int? maxPath, out int? maxSinglePath)
            {
                if (root == null)
                {
                    maxPath = null;
                    maxSinglePath = null;
                }
                else
                {
                    SubMax(root.left,out var leftMax,out var leftSingle);
                    SubMax(root.right,out var rightMax,out var rightSingle);
                    var rootLeftSingle = root.val;
                    var rootRightSingle = root.val;
                    var rootSum = root.val;
                    if (leftSingle.HasValue && leftSingle.Value > 0)
                    {
                        rootSum += leftSingle.Value;
                        rootLeftSingle += leftSingle.Value;
                    }

                    if (rightSingle.HasValue && rightSingle.Value > 0)
                    {
                        rootSum += rightSingle.Value;
                        rootRightSingle += rightSingle.Value;
                    }

                    if (leftMax.HasValue)
                    {
                        rootSum = Math.Max(rootSum, leftMax.Value);
                    }

                    if (rightMax.HasValue)
                    {
                        rootSum = Math.Max(rootSum, rightMax.Value);
                    }

                    maxPath = rootSum;
                    maxSinglePath = Math.Max(rootLeftSingle, rootRightSingle);
                }
            }
        }
    }

    namespace p4
    {
        /*
// Definition for a Node.

*/
        public class Solution
        {
            public Node CloneGraph(Node node)
            {
                if (node == null) return null;
                var dict = new Dictionary<Node, Node>();
                DFS(node, dict);
                DFSCopy(node, dict, new HashSet<Node>());
//                foreach (var entry in dict)
//                {
//                    var key = entry.Key;
//                    var value = entry.Value;
//                    foreach (var keyNeighbor in key.neighbors)
//                    {
//                        value.neighbors.Add(dict[keyNeighbor]);
//                    }
//                }
                return dict[node];
            }


            private void DFS(Node node, Dictionary<Node, Node> dict)
            {
                dict[node] = new Node
                {
                    val = node.val, neighbors = new List<Node>()
                };
                foreach (var neighbor in node.neighbors)
                {
                    if (neighbor != null && !dict.ContainsKey(neighbor)) DFS(neighbor, dict);
                }
            }

            private void DFSCopy(Node node, Dictionary<Node, Node> dict, HashSet<Node> visited)
            {
                var copyNode = dict[node];
                foreach (var neighbor in node.neighbors)
                {
                    copyNode.neighbors.Add(dict[neighbor]);
                }

                visited.Add(node);
                foreach (var neighbor in node.neighbors)
                {
                    if (!visited.Contains(neighbor)) DFSCopy(neighbor, dict, visited);
                }
            }

        }
    }

    namespace p5
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
            public IList<int> RightSideView(TreeNode root)
            {
                var rs = new List<int>();
                if (root == null) return rs;
                var queue = new Queue<Tuple<TreeNode,int>>();
                var currentLevel = 0;
                queue.Enqueue(new Tuple<TreeNode, int>(root,0));
                var oneLevel = new Stack<int>();
                while (queue.Count > 0)
                {
                    var top = queue.Dequeue();
                    var node = top.Item1;
                    var level = top.Item2;
                    if (level > currentLevel)
                    {
                        currentLevel = level;
                        rs.Add(oneLevel.Pop());
                        oneLevel.Clear();
                    }
                    oneLevel.Push(node.val);
                    if (node.left != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.left,level+1));
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(node.right,level+1));
                    }
                }
                rs.Add(oneLevel.Pop());
                return rs;
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public int NumIslands(char[][] grid)
            {
                if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;
                var count = 0;
                var rows = grid.Length;
                var cols = grid[0].Length;
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (grid[i][j] == '1')
                        {
                            count++;
                            TagConnectedSpots(grid,i,j);
                        }
                    }
                }
                return count;
            }

            private void TagConnectedSpots(char[][] grid, int i, int j)
            {
                var rows = grid.Length;
                var cols = grid[0].Length;
                if (i < 0 || i == rows || j < 0 || j == cols || grid[i][j] == '0') return;
                grid[i][j] = '0';
                TagConnectedSpots(grid,i+1,j);
                TagConnectedSpots(grid,i,j+1);
                TagConnectedSpots(grid,i-1,j);
                TagConnectedSpots(grid,i,j-1);
            }
        }
    }

    namespace p7
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
                if (root == null || p == null || q == null) return null;
                var pA = new List<TreeNode>();
                var qA = new List<TreeNode>();
                FindAncestors(root, p, pA);
                FindAncestors(root, q, qA);
                var len = Math.Min(pA.Count, qA.Count);
                var i = 0;
                for (; i < len; i++)
                {
                    if (pA[i] != qA[i]) break;
                }
                return pA[i - 1];
            }

            private bool FindAncestors(TreeNode root, TreeNode p,List<TreeNode> ancestors)
            {
                if (root == null) return false;
                if (root == p)
                {
                    ancestors.Add(root);
                    return true;
                }
                ancestors.Add(root);
                var rs = FindAncestors(root.left, p, ancestors) || FindAncestors(root.right, p, ancestors);
                if (rs) return true;
                else
                {
                    ancestors.RemoveAt(ancestors.Count-1);
                    return false;
                }
            }
        }
    }

    namespace p8
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
            public IList<string> BinaryTreePaths(TreeNode root)
            {
                if(root==null)return new List<string>();
                var rs = new List<List<TreeNode>>();
                Traverse(root,new List<TreeNode>(), rs);
                var rt = new List<string>();
                foreach (var path in rs)
                {
                    rt.Add(ToStr(path));
                }
                return rt;
            }

            private void Traverse(TreeNode root,List<TreeNode> list,List<List<TreeNode>> rs)
            {
                list.Add(root);
                if (root.left == null && root.right == null)
                {
                    rs.Add(new List<TreeNode>(list));
                }
                else
                {
                    if (root.left != null)
                    {
                        Traverse(root.left,list,rs);
                    }

                    if (root.right != null)
                    {
                        Traverse(root.right,list,rs);
                    }
                }
                list.RemoveAt(list.Count-1);
            }

            private string ToStr(List<TreeNode> list)
            {
                var builder = new StringBuilder(list.Count);
                var len = list.Count;
                for (var i = 0; i < len-1; i++)
                {
                    builder.Append(list[i].val);
                    builder.Append("->");
                }
                builder.Append(list[len - 1].val);
                return builder.ToString();
            }
        }
    }

}