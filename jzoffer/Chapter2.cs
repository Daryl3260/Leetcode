using System;
using System.Collections.Generic;

namespace Leetcode.jzoffer.Chapter2
{
    namespace p1
    {
        public class Solution
        {
            public static void Test ()
            {
                Console.WriteLine ($"{new Solution().FindReplicate(new[] { 4, 3, 1, 0, 2, 5, 3 })}");
            }

            public int FindReplicate (int[] arr)
            {
                if (arr == null || arr.Length == 0) return -1;
                for (var i = 0; i < arr.Length;)
                {
                    var val = arr[i];
                    if (val == i) i++;
                    else
                    {
                        if (arr[val] == val) return val;
                        else
                        {
                            var temp = arr[i];
                            arr[i] = arr[val];
                            arr[val] = temp;
                        }
                    }
                }

                return -1;
            }
        }
    }

    namespace p1plus
    {
        public class Solution
        {
            public static void Test ()
            {
                Console.WriteLine (new Solution ().FindReplicate (new [] { 2, 3, 5, 4, 3, 2, 6, 7 }));
            }

            public int FindReplicate (int[] arr)
            {
                return FindBetween (arr, 1, arr.Length - 1);
            }

            private int FindBetween (int[] arr, int lo, int hi)
            {
                var len = hi - lo + 1;
                var count = CountBetween (arr, lo, hi);
                if (len == count) return -1;
                else
                {
                    //count>len
                    if (hi - lo < 3)
                    {
                        var set = new HashSet<int> ();
                        foreach (var i in arr)
                        {
                            if (lo <= i && i <= hi)
                            {
                                if (set.Contains (i)) return i;
                                else set.Add (i);
                            }
                        }

                        return -1;
                    }
                    else
                    {
                        var mid = (lo + hi) >> 1;
                        var left = FindBetween (arr, lo, mid);
                        if (left != -1) return left;
                        else return FindBetween (arr, mid + 1, hi);
                    }
                }
            }

            private int CountBetween (int[] arr, int lo, int hi)
            {
                var count = 0;
                foreach (var i in arr)
                {
                    if (lo <= i && i <= hi) count++;
                }

                return count;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public bool SearchInMatrix (int[][] matrix, int target)
            {
                if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return false;
                var rows = matrix.Length;
                var cols = matrix[0].Length;
                var i = 0;
                var j = cols - 1;
                while (true)
                {
                    if (i == rows || j < 0) return false;
                    else if (matrix[i][j] == target) return true;
                    else if (matrix[i][j] > target)
                    {
                        j--;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
    }

    namespace p3
    {
        public class Node
        {
            public Node Next { get; set; }
            public int Val { get; set; }
        }

        public class Solution
        {
            public static void Test ()
            {
                var solution = new Solution ();
                var head = new Node { Val = 1 };
                head.Next = new Node { Val = 2 };
                head.Next.Next = new Node { Val = 3 };
                var list = solution.ReversePrint (head);
                foreach (var item in list)
                {
                    Console.WriteLine ($"{item}");
                }
            }
            public List<int> ReversePrint (Node head)
            {
                if (head == null) return new List<int> ();
                var rs = new List<int> ();
                AddToList (head, rs);
                return rs;
            }

            private void AddToList (Node node, List<int> rs)
            {
                if (node.Next == null)
                {
                    rs.Add (node.Val);
                }
                else
                {
                    AddToList (node.Next, rs);
                    rs.Add (node.Val);
                }
            }
        }
    }

    public class TreeNode
    {
        public int Val { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
    }

    namespace p4
    {
        public class Solution
        {
            public TreeNode Rebuild (int[] preorder, int[] inorder)
            {
                if (preorder == null || preorder.Length == 0) return null;
                return Construct (preorder, 0, preorder.Length - 1, inorder, 0, inorder.Length - 1);
            }

            private TreeNode Construct (int[] preorder, int l1, int h1, int[] inorder, int l2, int h2)
            {
                var root = new TreeNode { Val = preorder[l1] };
                var idx = -1;
                for (var i = l2; i <= h2; i++)
                {
                    if (inorder[i] == root.Val)
                    {
                        idx = i;
                        break;
                    }
                }

                var leftLen = idx - l2;
                var rightLen = h2 - idx;
                if (leftLen > 0)
                {
                    return Construct (preorder, l1 + 1, l1 + leftLen, inorder, l2, idx - 1);
                }

                if (rightLen > 0)
                {
                    return Construct (preorder, l1 + leftLen + 1, h1, inorder, idx + 1, h2);
                }
                return root;
            }
        }
    }

    namespace p5
    {
        public class TreeNode
        {
            public int Val { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public TreeNode Parent { get; set; }
        }
        public class Solution
        {
            public TreeNode FindNext (TreeNode node)
            {
                if (node.Right != null) return LeftMost (node.Right);
                else
                {
                    while (true)
                    {
                        if (node.Parent == null) return null;
                        else if (node == node.Parent.Left) return node.Parent;
                        else node = node.Parent;
                    }
                }
            }

            private TreeNode LeftMost (TreeNode node)
            {
                while (node.Left != null) node = node.Left;
                return node;
            }
        }
    }

    namespace p6
    {
        public class MyQueue
        {
            public int Count => _stack1.Count + _stack2.Count;
            private Stack<int> _stack1 = new Stack<int> ();
            private Stack<int> _stack2 = new Stack<int> ();

            public void Enqueue (int val)
            {
                _stack1.Push (val);
            }

            public int? Peek ()
            {
                if (Count == 0) return null;
                if (_stack2.Count == 0)
                {
                    while (_stack1.Count > 0) _stack2.Push (_stack1.Pop ());
                }

                return _stack2.Peek ();
            }

            public int? Dequeue (){
                var abc = 10;
                Console.WriteLine (abc);
                if (abc < 10)
                {
                    Console.WriteLine ("abc<10");
                }
                return null;
            }
            public static void Test(){
                Console.WriteLine("abc");
                var abc = 10;
                if(abc<11){
                    Console.WriteLine("less than 11");
                }
            }
        }
    }
}