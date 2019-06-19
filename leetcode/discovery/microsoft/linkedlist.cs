using Leetcode.hifreq2018.LinkedList.p2;

namespace Leetcode.leetcode.discovery.microsoft.linkedlist
{
    namespace p1
    {
        /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
        public class Solution
        {
            public bool HasCycle(ListNode head)
            {
                if (head == null) return false;
                var header = new ListNode(-1);
                header.next = head;
                var fast = header;
                var slow = header;
                while (true)
                {
                    if (fast.next?.next == null) return false;
                    fast = fast.next.next;
                    slow = slow.next;
                    if (fast == slow) return true;
                }
            }
        }
    }
}