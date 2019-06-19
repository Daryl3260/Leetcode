using System.Collections.Generic;
using Leetcode.leetcode_cn.top_interviewed.LinkedList.p6;

namespace Leetcode.leetcode_cn.top_interviewed.LinkedList
{
    namespace p6
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
            }
        }

        public class Solution
        {
            public bool IsPalindrome(ListNode head)
            {
                if (head == null || head.next == null) return true;
                List<int> reversed = new List<int>();
                GetInverted(head,reversed);
                int len = reversed.Count;
                ListNode ptr = head;
                for (int i = 0; i < len; i++)
                {
                    if (reversed[i] != ptr.val) return false;
                    ptr = ptr.next;
                }

                return true;
            }

            private void GetInverted(ListNode head, List<int> list)
            {
                if (head.next != null)
                {
                    GetInverted(head.next,list);
                }
                list.Add(head.val);
            }
        }
    }

    namespace p7
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
            public void DeleteNode(ListNode node)
            {
                while (true)
                {
                    node.val = node.next.val;
                    if (node.next.next == null)
                    {
                        node.next = null;
                        break;
                    }
                    else
                    {
                        node = node.next;
                    }
                }
            }
        }
    }
}