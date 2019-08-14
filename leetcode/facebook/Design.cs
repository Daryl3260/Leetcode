using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode.facebook.Design
{
    namespace p1
    {
        public class LRUCache
        {
            public class Node<T>
            {
                public T Val { get; set; }
                public Node<T> Prev { get; set; }
                public Node<T> Next { get; set; }
            }

            private Node<Tuple<int, int>> _header;
            private Node<Tuple<int, int>> _trailer; //item1:key,item2:value
            private int _capacity;
            private Dictionary<int, Node<Tuple<int, int>>> _dict;

            public LRUCache(int capacity)
            {
                _capacity = capacity;
                _header = new Node<Tuple<int, int>>();
                _trailer = new Node<Tuple<int, int>>();
                _dict = new Dictionary<int, Node<Tuple<int, int>>>();
                _header.Next = _trailer;
                _trailer.Prev = _header;
            }

            private void MoveToFront<TP>(Node<TP> node, Node<TP> header)
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
                node.Prev = header;
                node.Next = header.Next;
                node.Prev.Next = node;
                node.Next.Prev = node;
            }

            public int Get(int key)
            {
                if (!_dict.ContainsKey(key)) return -1;
                var valueNode = _dict[key];
                var rs = valueNode.Val.Item2;
                MoveToFront(valueNode, _header);
                return rs;
            }


            public void Put(int key, int value)
            {
                if (_dict.ContainsKey(key))
                {
                    var valueNode = _dict[key];
                    valueNode.Val = new Tuple<int, int>(key, value);
                    MoveToFront(valueNode, _header);
                }
                else
                {
                    if (_capacity == 0) return;
                    if (_dict.Count == _capacity)
                    {
                        var lastNode = _trailer.Prev;
                        var lastNodeKey = lastNode.Val.Item1;
                        _dict.Remove(lastNodeKey);
                        MoveToFront(lastNode, _header);
                        lastNode.Val = new Tuple<int, int>(key, value);
                        _dict[key] = lastNode;
                    }
                    else
                    {
                        var node = new Node<Tuple<int, int>>
                        {
                            Val = new Tuple<int, int>(key, value), Prev = _header, Next = _header.Next
                        };
                        node.Prev.Next = node;
                        node.Next.Prev = node;
                        _dict[key] = node;
                    }
                }
            }
        }

/**
 * Your LRUCache object will be instantiated and called as such:
 * LRUCache obj = new LRUCache(capacity);
 * int param_1 = obj.Get(key);
 * obj.Put(key,value);
 */
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

        public class BSTIterator
        {

            private List<int> _inorderList = new List<int>();
            private int _idx;

            public BSTIterator(TreeNode root)
            {
                if (root != null)
                {
                    InorderTraverse(root);
                }

                _idx = 0;
            }

            private void InorderTraverse(TreeNode root)
            {
                if (root == null) return;
                InorderTraverse(root.left);
                _inorderList.Add(root.val);
                InorderTraverse(root.right);
            }

            /** @return the next smallest number */
            public int Next()
            {
                return _inorderList[_idx++];
            }

            /** @return whether we have a next smallest number */
            public bool HasNext()
            {
                return _idx < _inorderList.Count;
            }
        }

/**
 * Your BSTIterator object will be instantiated and called as such:
 * BSTIterator obj = new BSTIterator(root);
 * int param_1 = obj.Next();
 * bool param_2 = obj.HasNext();
 */
    }

    namespace p3
    {
        public class WordDictionary
        {
            public class Node
            {
                public char Val { get; set; }
                public bool EndMark { get; set; }
                private Node[] _children = new Node[26];
                public Node[] Children => _children;
            }

            public Node Root { get; set; }
            /** Initialize your data structure here. */
            public WordDictionary()
            {
                Root = new Node();
            }

            /** Adds a word into the data structure. */
            public void AddWord(string word)
            {
                if (string.IsNullOrEmpty(word)) return;
                SubAdd(Root,word,0);
            }

            private void SubAdd(Node node, string word, int idx)
            {
                while (true)
                {
                    if (string.IsNullOrEmpty(word)) return;
                    if (idx == word.Length)
                    {
                        node.EndMark = true;
                        break;
                    }
                    else
                    {
                        var letter = word[idx];
                        var i = letter - 'a';
                        if (node.Children[i] == null)
                        {
                            node.Children[i]=new Node{Val = letter};
                        }

                        node = node.Children[i];
                        idx++;
//                        SubAdd(node.Children[i],word,idx+1);
                    }
                }
            }

            /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
            public bool Search(string word)
            {
                if (string.IsNullOrEmpty(word)) return false;
                return SubSearch(Root, word, 0);
            }

            private bool SubSearch(Node node, string word, int idx)
            {
                if (node == null) return false;
                if (idx == word.Length)
                {
                    return node.EndMark;
                }

                var letter = word[idx];
                if (letter == '.')
                {
                    return node.Children.Any(child => SubSearch(child, word, idx + 1));
                }
                else
                {
                    return SubSearch(node.Children[letter - 'a'], word, idx + 1);
                }
            }
        }

/**
 * Your WordDictionary object will be instantiated and called as such:
 * WordDictionary obj = new WordDictionary();
 * obj.AddWord(word);
 * bool param_2 = obj.Search(word);
 */
    }

    namespace p4
    {
        //add extra null to let children = 2*parents
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

        public class Codec
        {
            private const string NULL = "null";

            private List<int?> LevelTraverse(TreeNode root)
            {
                var queue = new Queue<Tuple<TreeNode,int>>();
                var oneLevel = new List<TreeNode>();
                var currentLevel = 0;
                var rs = new List<int?>();
                queue.Enqueue(new Tuple<TreeNode, int>(root,0));
                while (queue.Count > 0)
                {    
                    var top = queue.Dequeue();
                    var level = top.Item2;
                    var topNode = top.Item1;
                    if (level > currentLevel)
                    {
                        currentLevel = level;
                        foreach (var node in oneLevel)
                        {
                            rs.Add(node?.val);
                        }
                        oneLevel.Clear();
                    }
                    oneLevel.Add(topNode);
                    if (topNode != null)
                    {
                        queue.Enqueue(new Tuple<TreeNode, int>(topNode.left,level+1));
                        queue.Enqueue(new Tuple<TreeNode, int>(topNode.right,level+1));
                    }
                }

                foreach (var node in oneLevel)
                {
                    rs.Add(node?.val);
                }
                return rs;
            }
            // Encodes a tree to a single string.
            public string serialize(TreeNode root)
            {
                var nodeList = LevelTraverse(root);
                var builder = new StringBuilder(nodeList.Count);
                foreach (var val in nodeList)
                {
                    builder.Append(val==null?NULL:val.Value.ToString());
                    builder.Append(',');
                }

                return builder.ToString();
            }

            // Decodes your encoded data to tree.
            public TreeNode deserialize(string data)
            {
                var values = data.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                if (values[0] == NULL)
                {
                    return null;
                }
                var lastLevel = new List<TreeNode>();
                var nextLevel = new List<TreeNode>();
                lastLevel.Add(new TreeNode(int.Parse(values[0])));
                var root = lastLevel[0];
                var idx = 1;
                while (idx<values.Length)
                {
                    for (var i = 0; i < lastLevel.Count; i++)
                    {
                        var parent = lastLevel[i];
                        var leftStr = values[idx++];
                        var rightStr = values[idx++];
                        if (leftStr == NULL)
                        {
                            parent.left = null;
                        }
                        else
                        {
                            parent.left = new TreeNode(int.Parse(leftStr));
                            nextLevel.Add(parent.left);
                        }

                        if (rightStr == NULL)
                        {
                            parent.right = null;
                        }
                        else
                        {
                            parent.right = new TreeNode(int.Parse(rightStr));
                            nextLevel.Add(parent.right);
                        }
                    }

                    var temp = lastLevel;
                    lastLevel = nextLevel;
                    nextLevel = temp;
                    nextLevel.Clear();
                }
                return root;
            }
        }

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.deserialize(codec.serialize(root));
    }
}