using Leetcode.hifreq2018.LinkedList.p2;

namespace Leetcode.hifreq2018.LinkedList
{
    namespace p1
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
                    Node header = new Node {next = head};
                    Node p = head;
                    while (p != null)
                    {
                        Node addOn = new Node(p.val, null, null);
                        addOn.next = p.next;
                        p.next = addOn;
                        p = addOn.next;
                    }

                    p = header.next;
                    while (p!= null)
                    {
                        Node addOn = p.next;
//                        if(addOn.next!=null) addOn.next = addOn.next.next;
//                        else
//                        {
//                            addOn.next = null;
//                        }
                        if (p.random != null)
                        {
                            addOn.random = p.random.next;
                        }
                        else
                        {
                            addOn.random = null;
                        }
                        p = addOn.next;
                    }

                    p = header.next;
                    Node h2 = new Node();
                    Node q = h2;
                    while (p != null)
                    {
                        Node addOn = p.next;
                        Node next = addOn.next;
                        q.next = addOn;
                        if (addOn.next != null)
                        {
                            addOn.next = addOn.next.next;
                        }
                        else
                        {
                            addOn.next = null;
                        }
                        q = addOn;
                        p.next = next;
                        p = next;
                    }
                    
                    return h2.next;
                }
            }
        }
    }

    namespace p2
    {
        /**
 * Definition for singly-linked list.
 * 
 */
        public class ListNode
        {
            public int val;
            public ListNode next;

            public ListNode(int x)
            {
                val = x;
                next = null;
            }
        }

        public class Solution {
            public bool HasCycle(ListNode head)
            {
                if (head == null) return false;
                var header = new ListNode(-1) {next = head};
                ListNode fast = header;
                ListNode slow = header;
                while (true)
                {
                    if (fast.next == null || fast.next.next == null) return false;
                    slow = slow.next;
                    fast = fast.next.next;
                    if (fast == slow) return true;
                }
            }
        }
}

    namespace p3
    {
        public class Solution {
            public ListNode SortList(ListNode head)
            {
                if (head == null || head.next == null) return head;
                Split(head,out var h1,out var h2);
                ListNode sortedH1 = SortList(h1);
                ListNode sortedH2 = SortList(h2);
                return Merge(sortedH1, sortedH2);
            }

            private int CountListNode(ListNode head)
            {
                int count = 0;
                while (head != null)
                {
                    count++;
                    head = head.next;
                }
                return count;
            }

            private void Split(ListNode head,out ListNode h1,out ListNode h2)
            {
                ListNode header = new ListNode(-1);
                header.next = head;
                ListNode prev = header;
                ListNode p = header.next;
                int count = CountListNode(head);
                for (int i = 0; i < count/2; i++)
                {
                    p = p.next;
                    prev = prev.next;
                }
                prev.next = null;
                h1 = header.next;
                h2 = p;
            }

            private ListNode Merge(ListNode h1, ListNode h2)
            {
                ListNode header = new ListNode(-1);
                ListNode p = header;
                while (h1 != null && h2 != null)
                {
                    if (h1.val < h2.val)
                    {
                        p.next = h1;
                        h1 = h1.next;
                        p = p.next;
                        p.next = null;
                    }
                    else
                    {
                        p.next = h2;
                        h2 = h2.next;
                        p = p.next;
                        p.next = null;
                    }
                }

                if (h1 != null) p.next = h1;
                else if (h2 != null) p.next = h2;
                return header.next;
            }
        }
    }
}