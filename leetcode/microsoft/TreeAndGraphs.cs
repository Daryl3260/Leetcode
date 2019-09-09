using System;
using System.Collections.Generic;
using Leetcode.leetcode.discovery.microsoft.tree_graph.p1;

namespace Leetcode.leetcode.microsoft.TreeAndGraphs
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
            public struct NodeWrapper
            {
                public int Level { get; set; }
                public TreeNode Node { get; set; }
            }

            public IList<IList<int>> LevelOrder(TreeNode root)
            {
                var queue = new Queue<NodeWrapper>();
                int currentLevel = 0;
                var rs = new List<IList<int>>();
                if (root == null) return rs;
                var oneLevel = new List<int>();
                queue.Enqueue(new NodeWrapper
                {
                    Level = 0, Node = root
                });
                while (queue.Count > 0)
                {
                    var top = queue.Dequeue();
                    if (top.Level > currentLevel)
                    {
                        currentLevel = top.Level;
                        var layer = new List<int>(oneLevel);
                        rs.Add(layer);
                        oneLevel.Clear();
                    }

                    oneLevel.Add(top.Node.val);
                    var node = top.Node;
                    if (node.left != null)
                    {
                        queue.Enqueue(new NodeWrapper
                        {
                            Level = currentLevel + 1,
                            Node = node.left
                        });
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(new NodeWrapper
                        {
                            Level = currentLevel + 1,
                            Node = node.right
                        });
                    }
                }

                rs.Add(oneLevel);
                return rs;
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
            public struct Wrapper
            {
                public int Level { get; set; }
                public TreeNode Node { get; set; }
            }

            public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
            {
                var rs = new List<IList<int>>();
                if (root == null) return rs;
                var queue = new Queue<Wrapper>();
                queue.Enqueue(new Wrapper
                {
                    Level = 0, Node = root
                });
                var oneLayer = new List<int>();
                var currentLevel = 0;
                while (queue.Count > 0)
                {
                    var top = queue.Dequeue();
                    var level = top.Level;
                    var node = top.Node;
                    if (currentLevel < level)
                    {
                        var layer = new List<int>(oneLayer.Count);
                        if ((currentLevel & 1) == 1)
                        {
                            for (int i = 0; i < oneLayer.Count; i++)
                            {
                                layer.Add(oneLayer[oneLayer.Count - 1 - i]);
                            }
                        }
                        else
                        {
                            layer.AddRange(oneLayer);
                        }

                        rs.Add(layer);
                        currentLevel = level;
                        oneLayer.Clear();
                    }

                    oneLayer.Add(node.val);
                    if (node.left != null)
                    {
                        queue.Enqueue(new Wrapper
                        {
                            Level = level + 1, Node = node.left
                        });
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(new Wrapper
                        {
                            Level = level + 1, Node = node.right
                        });
                    }
                }

                if ((currentLevel & 1) == 0) rs.Add(oneLayer);
                else
                {
                    var len = oneLayer.Count;
                    for (int i = 0; i < len / 2; i++)
                    {
                        var tmp = oneLayer[i];
                        oneLayer[i] = oneLayer[len - 1 - i];
                        oneLayer[len - 1 - i] = tmp;
                    }

                    rs.Add(oneLayer);
                }

                return rs;
            }
        }
    }

    namespace p3
    {

// Definition for a Node.
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
                    var starter = root;
                    while (starter != null)
                    {
                        var p = starter;
                        while (p != null)
                        {
                            if (p.left != null)
                            {
                                if (p.right != null)
                                {
                                    p.left.next = p.right;
                                }
                                else p.left.next = FindNextStarter(p.next);
                            }

                            if (p.right != null) p.right.next = FindNextStarter(p.next);
                            p = p.next;
                        }

                        starter = FindNextStarter(starter);
                    }

                    return root;
                }

                private Node FindNextStarter(Node node)
                {
                    while (node != null)
                    {
                        if (node.left != null) return node.left;
                        if (node.right != null) return node.right;
                        node = node.next;
                    }

                    return null;
                }
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
                var plist = new List<TreeNode>();
                var qlist = new List<TreeNode>();
                FindAncestors(root, p, plist);
                FindAncestors(root, q, qlist);
                var len = Math.Min(plist.Count, qlist.Count);
                int i;
                for (i = 0; i < len; i++)
                {
                    if (plist[i] != qlist[i]) break;
                }

                return plist[i - 1];
            }

            private bool FindAncestors(TreeNode root, TreeNode target, List<TreeNode> list)
            {
                if (root == null)
                {
                    return false;
                }

                if (root == target)
                {
                    list.Add(root);
                    return true;
                }

                list.Add(root);
                bool lc = FindAncestors(root.left, target, list);
                bool rc = FindAncestors(root.right, target, list);
                if (lc || rc)
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

    namespace p5
    {

// Definition for a Node.
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

        public class Solution
        {
            private Dictionary<Node, Node> _dict = new Dictionary<Node, Node>();

            public Node CloneGraph(Node node)
            {
                Traverse(node);
                var rs = _dict[node];
                foreach (var key in _dict.Keys)
                {
                    var val = _dict[key];
                    foreach (var keyNeighbor in key.neighbors)
                    {
                        val.neighbors.Add(_dict[keyNeighbor]);
                    }
                }

                return rs;
            }

            private void Traverse(Node node)
            {
                if (_dict.ContainsKey(node)) return;
                _dict[node] = new Node(node.val, new List<Node>());
                foreach (var neighbor in node.neighbors)
                {
                    Traverse(neighbor);
                }
            }
        }
    }

    namespace p6
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
            public TreeNode BuildTree(int[] preorder, int[] inorder)
            {
                if (preorder == null || inorder == null || preorder.Length == 0 || inorder.Length == 0) return null;
                return SubBuild(preorder, 0, preorder.Length - 1, inorder, 0, inorder.Length - 1);
            }

            private TreeNode SubBuild(int[] preorder, int pl, int pr, int[] inorder, int il, int ir)
            {
                if (pl > pr)
                {
                    return null;
                }

                if (pl == pr)
                {
                    return new TreeNode(preorder[pl]);
                }

                int rootVal = preorder[pl];
                var rs = new TreeNode(rootVal);
                int idx = -1;
                for (int i = il; i <= ir; i++)
                {
                    if (inorder[i] == rootVal)
                    {
                        idx = i;
                        break;
                    }
                }

                if (idx == -1) throw new Exception($"Can't find rootVal in inorder,{pl},{pr},{il},{ir}");
                var lcLen = idx - il;
                rs.left = SubBuild(preorder, pl + 1, pl + lcLen, inorder, il, idx - 1);
                rs.right = SubBuild(preorder, pl + lcLen + 1, pr, inorder, idx + 1, ir);
                return rs;
            }
        }
    }

    namespace p7
    {
        public class Solution
        {
            public int NumIslands(char[][] grid)
            {
                if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;
                var count = 0;
                var rows = grid.Length;
                var cols = grid[0].Length;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (grid[i][j] == '1')
                        {
                            count++;
                            TagNeighbors(grid,i,j);
                        }
                    }
                }

                return count;
            }

            private void TagNeighbors(char[][] grid, int i, int j)
            {
                var rows = grid.Length;
                var cols = grid[0].Length;
                if (i < 0 || i == rows || j < 0 || j == cols || grid[i][j]=='0') return;
                grid[i][j] = '0';
                TagNeighbors(grid,i-1,j);
                TagNeighbors(grid,i+1,j);
                TagNeighbors(grid,i,j-1);
                TagNeighbors(grid,i,j+1);
            }
        }
    }

    namespace p8{
        public class Solution{
            
        }
    }
}