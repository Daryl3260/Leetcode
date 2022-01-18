using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.interview2021_spring.linkedlist
{
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

    namespace p1
    {
        public class Solution
        {
            public ListNode ReverseList(ListNode head)
            {
                var header = new ListNode(-1);
                var p = head;
                while (p != null)
                {
                    var pNext = p.next;

                    p.next = header.next;
                    header.next = p;

                    p = pNext;
                }

                return header.next;
            }
        }
    }

    namespace p2
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
        public class Solution
        {
            public ListNode MergeTwoLists(ListNode list1, ListNode list2)
            {
                var header1 = new ListNode();

                var h1 = header1;

                var p1 = list1;
                var p2 = list2;

                while (p1 != null || p2 != null)
                {
                    if (p2 == null || (p1 != null && p1.val <= p2.val))
                    {
                        var p1Next = p1.next;

                        p1.next = null;

                        h1.next = p1;
                        h1 = h1.next;

                        p1 = p1Next;
                    }
                    else
                    {
                        var p2Next = p2.next;

                        p2.next = null;
                        h1.next = p2;
                        h1 = h1.next;

                        p2 = p2Next;
                    }
                }

                return header1.next;
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
        public class Solution
        {
            public ListNode MergeKLists(ListNode[] lists)
            {
                if (lists == null || lists.Length == 0) return null;

                var header = new ListNode();

                var prev = header;

                while (true)
                {
                    var smallIdx = 0;

                    for (var i = 1; i < lists.Length; i++)
                    {
                        var smallList = lists[smallIdx];

                        var list = lists[i];

                        if (list != null && (smallList == null || list.val < smallList.val))
                        {
                            smallIdx = i;
                        }
                    }

                    if (lists[smallIdx] == null)
                    {
                        break;
                    }
                    else
                    {
                        var smallList = lists[smallIdx];

                        lists[smallIdx] = smallList.next;

                        smallList.next = null;

                        prev.next = smallList;
                        prev = prev.next;
                    }
                }

                return header.next;
            }
        }
    }

    namespace p3.s2
    {
        public class Solution
        {
            public ListNode MergeKLists(ListNode[] lists)
            {
                var header = new ListNode(-1);
                var prev = header;

                if (lists == null || lists.Length == 0) return null;

                PriorityQueue<ListNode> pq = new PriorityQueue<ListNode>(
                    (x, y) =>
                    {
                        return -(x.val - y.val);
                    });

                foreach (var node in lists)
                {
                    if (node != null)
                    {
                        pq.Add(node);
                    }
                }

                while (pq.Count() > 0)
                {
                    var smallest = pq.Pop();
                    var next = smallest.next;

                    smallest.next = null;
                    prev.next = smallest;
                    prev = prev.next;

                    if (next != null)
                    {
                        pq.Add(next);
                    }
                }

                return header.next;
            }
        }
    }
}
