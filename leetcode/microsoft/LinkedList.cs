using System;
using System.Collections.Generic;
using System.Linq;
using Leetcode.hifreq2018.LinkedList.p2;

namespace Leetcode.leetcode.microsoft.LinkedList
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
                if (l2 == null) return l1;
                var len1 = Len(l1);
                var len2 = Len(l2);
                if (len1 < len2)
                {
                    var temp = l1;
                    l1 = l2;
                    l2 = temp;
                    var tInt = len1;
                    len1 = len2;
                    len2 = tInt;
                }

                var extra = 0;
                var p1 = l1;
                var p2 = l2;
                ListNode prev = null;
                while (p2 != null)
                {
                    p1.val += p2.val + extra;
                    extra = p1.val / 10;
                    p1.val %= 10;
                    prev = p1;
                    p1 = p1.next;
                    p2 = p2.next;
                }

                if (extra > 0)
                {
                    while (p1 != null && extra > 0)
                    {
                        p1.val += extra;
                        extra = p1.val / 10;
                        p1.val %= 10;
                        prev = p1;
                        p1 = p1.next;
                    }

                    if (p1 == null && extra > 0)
                    {
                        prev.next = new ListNode(extra);
                    }
                }

                return l1;
            }

            private ListNode Invert(ListNode node)
            {
                ListNode h1 = new ListNode(-1);
                ListNode h2 = new ListNode(-2);
                h1.next = node;
                while (h1.next != null)
                {
                    var n = h1.next;
                    h1.next = n.next;
                    n.next = h2.next;
                    h2.next = n;
                }

                return h2.next;
            }

            private int Len(ListNode node)
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

    namespace p2
    {
        /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
        public class Solution
        {
            public ListNode MergeTwoLists(ListNode l1, ListNode l2)
            {
                ListNode header = new ListNode(-1);
                var p = header;
                ListNode p1 = l1;
                var p2 = l2;
                while (p1 != null && p2 != null)
                {
                    if (p1.val < p2.val)
                    {
                        var node = p1;
                        p1 = p1.next;
                        node.next = null;
                        p.next = node;
                        p = p.next;
                    }
                    else
                    {
                        var node = p2;
                        p2 = p2.next;
                        node.next = null;
                        p.next = node;
                        p = p.next;
                    }
                }

                if (p1 == null)
                {
                    p.next = p2;
                }
                else
                {
                    p.next = p1;
                }

                return header.next;
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
 *     public ListNode(int x) { val = x; }
 * }S
 */
        public class Solution
        {
            public ListNode MergeKLists(ListNode[] lists)
            {
                ListNode header = new ListNode(-1);
                var p = header;
                ListNode min;
                var minIdx = -1;
                while (true)
                {
                    min = null;
                    for (int i = 0; i < lists.Length; i++)
                    {
                        var node = lists[i];
                        if (node != null && (min == null || node.val < min.val))
                        {
                            min = node;
                            minIdx = i;
                        }
                    }

                    if (min == null) break;
                    else
                    {
                        p.next = min;
                        MoveNext(lists, minIdx);
                        p.next.next = null;
                        p = p.next;
                    }
                }

                return header.next;
            }

            private void MoveNext(ListNode[] listNodes, int idx)
            {
                listNodes[idx] = listNodes[idx].next;
            }
        }
    }

    namespace p4
    {
        /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
        public class Solution
        {
            public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
            {
                var hA = headA;
                var hB = headB;
                var acp = Len(headA);
                var bcp = Len(head: headB);
                headA = Invert(headA);
                var abp = Len(headB);
                headB = Invert(headB);
                if (headB != hA)
                {
                    headA = Invert(headA);
                    headB = Invert(headB);
                    return null;
                }

                headA = Invert(headA);
                var tmp = headA;
                headA = headB;
                headB = tmp;
                var abc = ((acp - 1) + (bcp - 1) + (abp - 1)) / 2;
                var a = abc - (bcp - 1);
                var rs = headA;
                for (int i = 0; i < a; i++)
                {
                    rs = rs.next;
                }

                return rs;
            }

            private ListNode Invert(ListNode head)
            {
                var h1 = new ListNode(-1);
                var h2 = new ListNode(-2);
                h1.next = head;
                while (h1.next != null)
                {
                    var node = h1.next;
                    h1.next = node.next;
                    node.next = h2.next;
                    h2.next = node;
                }

                return h2.next;
            }

            private int Len(ListNode head)
            {
                var count = 0;
                while (head != null)
                {
                    count++;
                    head = head.next;
                }

                return count;
            }
        }
    }

    namespace p5
    {

// Definition for a Node.
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
                    var p = head;
                    while (p != null)
                    {
                        var copy = new Node
                        {
                            val = p.val
                        };
                        copy.next = p.next;
                        p.next = copy;
                        p = copy.next;
                    }

                    p = head;
                    while (p != null)
                    {
                        var copy = p.next;
                        if (p.random != null) copy.random = p.random.next;
                        else copy.random = null;
                        p = copy.next;
                    }

                    var copyHead = head.next;
                    p = head;
                    while (p != null)
                    {
                        var copy = p.next;
                        var nextP = copy.next;
                        if (nextP == null)
                        {
                            p.next = null;
                            copy.next = null;
                            break;
                        }

                        copy.next = nextP.next;
                        p.next = nextP;
                        p = p.next;
                    }

                    return copyHead;
                }
            }
        }

        namespace mock
        {
            namespace p1
            {
                public class Solution
                {
                    public int[] DistributeCandies(int candies, int num_people)
                    {
                        var rs = new int[num_people];
                        var idx = 0;
                        var count = 1;
                        while (candies > 0)
                        {
                            if (count >= candies)
                            {
                                rs[idx] += candies;
                                break;
                            }
                            else
                            {
                                rs[idx] += count;
                                candies -= count;
                                count++;
                                idx = (idx + 1) % num_people;
                            }
                        }

                        return rs;
                    }
                }
            }

            namespace p2
            {
                public class Solution
                {
                    public IList<int> PathInZigZagTree(int label)
                    {
                        var rs = new LinkedList<int>();
                        var heap = new List<int>();
                        var numOfOneRow = 1;
                        var pointer = 1;
                        bool even = true;
                        bool foundLabel = false;
                        while (!foundLabel)
                        {
                            if (even)
                            {
                                for (int i = 0; i < numOfOneRow; i++)
                                {
                                    heap.Add(pointer++);
                                    if (pointer - 1 == label)
                                    {
                                        foundLabel = true;
                                        break;
                                    }
                                }

                                numOfOneRow <<= 1;
                                even = false;
                            }
                            else
                            {
                                var top = pointer + (numOfOneRow - 1);
                                var t = top;
                                for (var i = 0; i < numOfOneRow; i++)
                                {
                                    heap.Add(top--);
                                    if (top + 1 == label)
                                    {
                                        foundLabel = true;
                                        break;
                                    }
                                }

                                pointer = t + 1;
                                numOfOneRow <<= 1;
                                even = true;
                            }
                        }

                        var labelIdx = heap.Count - 1;
                        while (true)
                        {
                            rs.AddFirst(heap[labelIdx]);
                            if (labelIdx == 0) break;
                            labelIdx = (labelIdx - 1) >> 1;
                        }

                        return new List<int>(rs);
                    }
                }
            }

            namespace P3
            {
                public class Solution
                {
                    public int MinHeightShelves(int[][] books, int shelf_width)
                    {
                        return -1;
                    }

//                private int MinHeightFrom(int[][] books, int bookIdx, int shelf_width, int widthHere,int heightHere)
//                {
//                    var book = books[bookIdx];
//                    var thickness = book[0];
//                    var height = book[1];
//                    if (widthHere < thickness)
//                    {
//                        return height+MinHeightFrom(books,bookIdx+1,shelf_width,)
//                    }
//                }
                }
            }

            namespace p4
            {
                public class Solution
                {
                    public enum State
                    {
                        Not,
                        And,
                        Or
                    }

                    public bool ParseBoolExpr(string expression)
                    {
                        return ParseBoolExprBetween(expression, 0, expression.Length - 1);
                    }

                    private bool ParseBoolExprBetween(string expression, int left, int right) //&()
                    {
                        var first = expression[left];
                        if (first == '!')
                        {
                            return !ParseBetweenQuote(expression, left + 2, right - 1, State.Not);
                        }

                        if (first == '|')
                        {
                            return ParseBetweenQuote(expression, left + 2, right - 1, State.Or);
                        }

                        if (first == '&')
                        {
                            return ParseBetweenQuote(expression, left + 2, right - 1, State.And);
                        }

                        if (left != right)
                        {
                            throw new Exception($"invalid parse between {left},{right}");
                        }

                        return ParseChar(expression, left);
                    }

                    private bool ParseBetweenQuote(string expr, int left, int right, State state) //[],within ()
                    {
                        bool tag;
                        var first = expr[left];
                        int r;
                        if (first == '!' || first == '|' || first == '&')
                        {
                            r = FindRightQ(expr, left + 2);
                            tag = ParseBoolExprBetween(expr, left, r);
                        }
                        else
                        {
                            tag = ParseChar(expr, left);
                            r = left;
                        }

                        if (state == State.And && (!tag)) return false;
                        if (state == State.Or && tag) return true;
                        if (state == State.Not)
                        {
                            if (r == right) return tag;
                            else throw new Exception($"multiple expr in one Not expr {left},{right}");
                        }

                        if (r == right) return tag;
                        else
                        {
                            return ParseBetweenQuote(expr, r + 2, right, state);
                        }
                    }

                    private bool ParseChar(string expr, int idx)
                    {
                        return expr[idx] == 't';
                    }

                    private int FindRightQ(string expr, int start)
                    {
                        var count = 0;
                        var rs = -1;
                        for (var i = start; i < expr.Length; i++)
                        {
                            if (expr[i] == '(') count++;
                            else if (expr[i] == ')')
                            {
                                if (count > 0) count--;
                                else
                                {
                                    rs = i;
                                    break;
                                }
                            }
                        }

                        return rs;
                    }
                }
            }
        }
    }
}