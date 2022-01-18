using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.interview2021_spring.lookup
{
    namespace p1
    {
        public class LRUCache
        {
            private class Node
            {
                public Node Prev { get; set; }

                public Node Next { get; set; }

                public int Key { get; set; }

                public int Value { get; set; }
            }

            private Node Header;

            private Node Trailer;

            private Dictionary<int, Node> dict;

            private int count => dict.Count;

            private int capacity;

            public LRUCache(int capacity)
            {
                this.capacity = capacity;
                dict = new Dictionary<int, Node>();
                Header = new Node();
                Trailer = new Node();
                Header.Next = Trailer;
                Trailer.Prev = Header;
            }

            public int Get(int key)
            {
                if (dict.TryGetValue(key, out var oldNode))
                {
                    oldNode.Prev.Next = oldNode.Next;
                    oldNode.Next.Prev = oldNode.Prev;

                    oldNode.Prev = Header;
                    oldNode.Next = Header.Next;
                    oldNode.Prev.Next = oldNode;
                    oldNode.Next.Prev = oldNode;

                    return oldNode.Value;
                }
                else
                {
                    return -1;
                }
            }

            public void Put(int key, int value)
            {
                if (dict.TryGetValue(key, out var oldNode))
                {
                    oldNode.Value = value;
                    oldNode.Prev.Next = oldNode.Next;
                    oldNode.Next.Prev = oldNode.Prev;

                    oldNode.Prev = Header;
                    oldNode.Next = Header.Next;
                    oldNode.Prev.Next = oldNode;
                    oldNode.Next.Prev = oldNode;
                }
                else
                {
                    if (count == capacity)
                    {
                        var lastNode = Trailer.Prev;
                        lastNode.Prev.Next = lastNode.Next;
                        lastNode.Next.Prev = lastNode.Prev;
                        dict.Remove(lastNode.Key);
                    }

                    Node node = new Node
                    {
                        Key = key,
                        Value = value
                    };

                    dict[key] = node;

                    node.Prev = Header;
                    node.Next = Header.Next;
                    node.Prev.Next = node;
                    node.Next.Prev = node;
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
        public class Solution
        {
            public int SubarraysDivByK(int[] nums, int k)
            {
                var sums = new int[nums.Length];
                sums[0] = nums[0];

                var dict = new Dictionary<int, IList<int>>();

                for (var i = 1; i < nums.Length; i++)
                {
                    sums[i] = nums[i] + sums[i - 1];
                }

                for (var i = 0; i < nums.Length; i++)
                {
                    var sum = sums[i];
                    var mod = sum % k;
                    if (dict.TryGetValue(mod, out var list))
                    {
                        list.Add(i);
                    }
                    else
                    {
                        var newList = new List<int> { i };
                        dict[mod] = newList;
                    }
                }

                var rs = 0;

                foreach (var entry in dict)
                {
                    var mod = entry.Key;
                    var list = entry.Value;
                    if (mod == 0)
                    {
                        rs += list.Count;
                    }

                    var count = list.Count;

                    rs += (count - 1) * count / 2;

                    if (entry.Key < 0)
                    {
                        if (dict.TryGetValue(-entry.Key, out var opList))
                        {
                            rs += list.Count * opList.Count;
                        }
                    }
                }

                return rs;
            }
        }
    }

    namespace p3
    {
        /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }


        public class Solution
        {
            public ListNode ReverseKGroup(ListNode head, int k)
            {
                var header = new ListNode(-1);
                header.next = head;

                var h = header;
                while (true)
                {
                    var tailHeader = WalkKStep(h, k);
                    if (tailHeader == null)
                    {
                        break;
                    }
                    else
                    {
                        var thisStart = h.next;
                        var nextStart = tailHeader.next;
                        tailHeader.next = null;
                        var newStart = ReverseSubList(thisStart);
                        h.next = newStart;
                        thisStart.next = nextStart;
                        h = thisStart;
                    }
                }

                return header.next;
            }

            public ListNode WalkKStep(ListNode header, int k)
            {
                var h = header;

                while (h != null && k > 0)
                {
                    h = h.next;
                    k--;
                }

                return h;
            }

            public ListNode ReverseSubList(ListNode head)
            {
                var header = new ListNode(-1);

                var p = head;
                while (p != null)
                {
                    var nextP = p.next;
                    p.next = header.next;
                    header.next = p;
                    p = nextP;
                }

                return header.next;
            }
        }
    }
}
