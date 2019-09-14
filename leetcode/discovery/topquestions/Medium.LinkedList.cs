using System;
using System.Collections.Generic;

namespace Leetcode.leetcode.discovery.topquestions
{
    namespace Medium.LinkedList
    {
        namespace p1
        {

            public class ListNode
            {
                public int val;
                public ListNode next;

                public ListNode(int x)
                {
                    val = x;
                }
            }

            public class Solution
            {
                public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
                {
                    if (l1 == null) return l2;
                    else if (l2 == null) return l1;
                    var extra = 0;
                    LongShort(l1, l2, out var longList, out var shortList);
                    var p1 = longList;
                    var p2 = shortList;
                    ListNode previous = null;
                    while (p1 != null && p2 != null)
                    {
                        p1.val += p2.val + extra;
                        extra = p1.val / 10;
                        p1.val = p1.val % 10;
                        previous = p1;
                        p1 = p1.next;
                        p2 = p2.next;
                    }

                    if (p1 == null)
                    {
                        if (extra > 0)
                        {
                            previous.next = new ListNode(extra);
                        }

                        return longList;
                    }
                    else
                    {
                        if (extra == 0) return longList;
                        while (p1 != null && p1.val == 9)
                        {
                            previous = p1;
                            p1.val = 0;
                            p1 = p1.next;
                        }

                        if (p1 == null)
                        {
                            previous.next = new ListNode(1);
                            return longList;
                        }
                        else
                        {
                            p1.val++;
                            return longList;
                        }
                    }

                }

                private void LongShort(ListNode l1, ListNode l2, out ListNode longList, out ListNode shortList)
                {
                    var len1 = Length(l1);
                    var len2 = Length(l2);
                    if (len1 > len2)
                    {
                        longList = l1;
                        shortList = l2;
                    }
                    else
                    {
                        longList = l2;
                        shortList = l1;
                    }
                }

                private int Length(ListNode node)
                {
                    var count = 0;
                    while (node != null)
                    {
                        count++;
                        node = node.next;
                    }

                    return count;
                }
            }

            namespace better
            {
                public class Solution
                {
                    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
                    {
                        if (l1 == null) return l2;
                        if (l2 == null) return l1;
                        LongShort(l1, l2, out var longList, out var shortList);
                        var p1 = longList;
                        var p2 = shortList;
                        var extra = 0;
                        ListNode previous = null;
                        while (p1 != null)
                        {
                            if (p2 == null && extra == 0) break;
                            if (p2 != null)
                            {
                                p1.val += p2.val + extra;
                                extra = p1.val / 10;
                                p1.val %= 10;
                                previous = p1;
                                p1 = p1.next;
                                p2 = p2.next;
                            }
                            else
                            {
                                p1.val += extra;
                                extra = p1.val / 10;
                                p1.val %= 10;
                                previous = p1;
                                p1 = p1.next;
                            }
                        }

                        if (extra > 0) previous.next = new ListNode(extra);
                        return longList;
                    }

                    public void LongShort(ListNode l1, ListNode l2, out ListNode longList, out ListNode shortList)
                    {
                        var len1 = Length(l1);
                        var len2 = Length(l2);
                        if (len1 > len2)
                        {
                            longList = l1;
                            shortList = l2;
                        }
                        else
                        {
                            longList = l2;
                            shortList = l1;
                        }
                    }

                    public int Length(ListNode node)
                    {
                        var count = 0;
                        while (node != null)
                        {
                            count++;
                            node = node.next;
                        }

                        return count;
                    }
                }
            }
        }

        namespace p2
        {

            public class ListNode
            {
                public int val;
                public ListNode next;

                public ListNode(int x)
                {
                    val = x;
                }
            }

            public class Solution
            {
                public ListNode OddEvenList(ListNode head)
                {
                    if (head == null || head.next == null || head.next.next == null) return head;
                    var evenHeader = new ListNode(-1);
                    var oddHeader = new ListNode(-1);
                    var p1 = evenHeader;
                    var p2 = oddHeader;
                    var p = head;
                    var count = 0;
                    while (p != null)
                    {
                        var next = p.next;
                        p.next = null;
                        if ((count & 0x1) == 0)
                        {
                            p1.next = p;
                            p1 = p1.next;
                        }
                        else
                        {
                            p2.next = p;
                            p2 = p2.next;
                        }

                        count++;
                        p = next;
                    }

                    p1.next = oddHeader.next;
                    return evenHeader.next;
                }
            }
        }

        namespace p3
        {

            public class ListNode
            {
                public int val;
                public ListNode next;

                public ListNode(int x)
                {
                    val = x;
                }
            }

            public class Solution
            {
                public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
                {
                    if (headA == headB) return headA;
                    if (headA == null || headB == null) return null;
                    var listA = new List<ListNode>();
                    var listB = new List<ListNode>();
                    var p1 = headA;
                    var p2 = headB;
                    while (p1 != null)
                    {
                        listA.Add(p1);
                        p1 = p1.next;
                    }

                    while (p2 != null)
                    {
                        listB.Add(p2);
                        p2 = p2.next;
                    }

                    if (listA[listA.Count - 1] != listB[listB.Count - 1]) return null;
                    var i = listA.Count - 1;
                    var j = listB.Count - 1;
                    while (i > -1 && j > -1 && listA[i] == listB[j])
                    {
                        i--;
                        j--;
                    }

                    return listA[++i];

                }
            }
        }
    }

    namespace Hard.LinkedList
    {
        namespace p1
        {

            public class ListNode
            {
                public int val;
                public ListNode next;

                public ListNode(int x)
                {
                    val = x;
                }
            }

            public class Solution
            {
                public class Wrapper : IComparable<Wrapper>
                {
                    public static int sid = 0;
                    public int id;
                    public ListNode head;

                    public Wrapper(ListNode node)
                    {
                        head = node;
                        id = sid++;
                    }

                    public int CompareTo(Wrapper other)
                    {
                        if (head.val != other.head.val) return head.val - other.head.val;
                        return id - other.id;
                    }
                }

                public ListNode MergeKLists(ListNode[] lists)
                {
                    if (lists == null || lists.Length == 0) return null;
                    var priorityQueue = new SortedSet<Wrapper>();
                    foreach (var list in lists)
                    {
                        if (list == null) continue;
                        priorityQueue.Add(new Wrapper(list));
                    }

                    if (priorityQueue.Count == 0) return null;
                    var header = new ListNode(-1);
                    var p = header;
                    while (priorityQueue.Count > 0)
                    {
                        var min = priorityQueue.Min;
                        priorityQueue.Remove(min);
                        var head = min.head;
                        var next = head.next;
                        head.next = null;
                        p.next = head;
                        p = p.next;
                        if (next != null)
                        {
                            min.head = next;
                            priorityQueue.Add(min);
                        }
                    }

                    return header.next;
                }
            }
        }

        namespace p2
        {

            public class ListNode
            {
                public int val;
                public ListNode next;

                public ListNode(int x)
                {
                    val = x;
                }
            }

            public class Solution
            {
                public ListNode SortList(ListNode head)
                {
                    var len = Length(head);
                    if (len < 2) return head;
                    Divide(head, out var h1, out var h2);
                    var head1 = SortList(h1);
                    var head2 = SortList(h2);
                    return Merge(head1, head2);
                }

                public int Length(ListNode head)
                {
                    var count = 0;
                    while (head != null)
                    {
                        count++;
                        head = head.next;
                    }

                    return count;
                }

                public void Divide(ListNode head, out ListNode h1, out ListNode h2)
                {
                    var len = Length(head);
                    var header = new ListNode(-1);
                    header.next = head;
                    var p = header;
                    for (var i = 0; i < len / 2; i++)
                    {
                        p = p.next;
                    }

                    h2 = p.next;
                    p.next = null;
                    h1 = header.next;
                }

                public ListNode Merge(ListNode h1, ListNode h2)
                {
                    var header = new ListNode(-1);
                    var p = header;
                    while (h1 != null || h2 != null)
                    {
                        if (h1 != null && (h2 == null || h1.val < h2.val))
                        {
                            var next = h1.next;
                            h1.next = null;
                            p.next = h1;
                            p = p.next;
                            h1 = next;
                        }
                        else
                        {
                            var next = h2.next;
                            h2.next = null;
                            p.next = h2;
                            p = p.next;
                            h2 = next;
                        }
                    }

                    return header.next;
                }
            }
        }

        namespace p3
        {

            public class Node
            {
                public int val;
                public Node next;
                public Node random;

                public Node()
                {
                }

                public Node(int _val, Node _next, Node _random)
                {
                    val = _val;
                    next = _next;
                    random = _random;
                }

                public class Solution
                {
                    public Node CopyRandomList(Node head)
                    {
                        if (head == null) return null;
                        var dict = new Dictionary<Node, Node>();
                        var header0 = new Node();
                        header0.next = head;
                        var header1 = new Node();
                        var p0 = header0;
                        var p1 = header1;
                        while (p0.next != null)
                        {
                            var node = new Node();
                            node.val = p0.next.val;
                            dict[p0.next] = node;
                            p1.next = node;
                            p0 = p0.next;
                            p1 = p1.next;
                        }

                        p0 = header0.next;
                        p1 = header1.next;
                        while (p0 != null)
                        {
                            if (p0.random != null)
                            {
                                p1.random = dict[p0.random];
                            }

                            p0 = p0.next;
                            p1 = p1.next;
                        }

                        return header1.next;
                    }
                }
            }
        }
    }
}