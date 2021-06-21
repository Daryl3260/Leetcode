using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode_cn.interview2020.LinkedListQuestions
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
            public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                var len1 = Len(l1);
                var len2 = Len(l2);

                if (len1 < len2)
                {
                    var temp = l1;
                    l1 = l2;
                    l2 = temp;
                }

                return AddTwo(l1, l2);
            }

            // list1 is longer or equal to list2
            public ListNode AddTwo(ListNode tail1, ListNode tail2)
            {
                if (tail1 == null)
                {
                    return tail2;
                }

                if (tail2 == null)
                {
                    return tail1;
                }

                var p1 = tail1;
                var p2 = tail2;

                while (p1 != null && p2 != null)
                {
                    var sum = p1.val + p2.val;
                    var extra = sum / 10;
                    var rest = sum % 10;

                    p1.val = rest;
                    if (p1.next != null && p2.next != null)
                    {
                        p1.next.val += extra;
                        p1 = p1.next;
                        p2 = p2.next;
                    }
                    else if (p1.next != null)
                    {
                        if (extra > 0)
                        {
                            if (p1.next.val == 9)
                            {
                                SingleAdd(p1.next);
                            }
                            else
                            {
                                p1.next.val++;
                            }
                        }

                        break;
                    }
                    else
                    {
                        if (extra > 0)
                        {
                            p1.next = new ListNode(extra);
                        }
                        break;
                    }
                }

                return tail1;
            }

            public int Len(ListNode p)
            {
                var count = 0;
                while (p != null)
                {
                    count++;
                    p = p.next;
                }
                return count;
            }

            public void SingleAdd(ListNode listNode)
            {
                var p = listNode;
                while (true)
                {
                    if (p.val == 9 && p.next != null)
                    {
                        p.val = 0;
                        p = p.next;
                    }
                    else if (p.val == 9)
                    {
                        p.val = 0;
                        p.next = new ListNode(1);
                        break;
                    }
                    else
                    {
                        p.val++;
                        break;
                    }
                }
            }

            public ListNode RevertList(ListNode listNode, out int len)
            {
                var header = new ListNode(-1);

                var ptr = listNode;

                len = 0;

                while (ptr != null)
                {
                    len++;
                    var node = ptr;
                    ptr = ptr.next;

                    node.next = header.next;
                    header.next = node;
                }

                return header.next;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int LengthOfLongestSubstring(string s)
            {
                if (string.IsNullOrEmpty(s)) return 0;

                var windowDict = new Dictionary<char, int>();
                var max = 0;
                for (var i = 0; i < s.Length; i++)
                {
                    var ch = s[i];
                    if (windowDict.Any() && windowDict.TryGetValue(ch, out var previousIdx))
                    {
                        var startIdx = i - windowDict.Count;
                        for (var j = startIdx; j <= previousIdx; j++)
                        {
                            windowDict.Remove(s[j]);
                        }
                    }

                    windowDict[ch] = i;

                    max = Math.Max(max, windowDict.Count);
                }

                return max;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public ListNode RemoveNthFromEnd(ListNode head, int n)
            {
                var header = new ListNode(-1);
                header.next = head;

                var preToDel = header;
                var stopWhenIsLast = header;
                for (var i = 0; i < n; i++)
                {
                    stopWhenIsLast = stopWhenIsLast.next;
                }

                while (stopWhenIsLast.next != null)
                {
                    stopWhenIsLast = stopWhenIsLast.next;
                    preToDel = preToDel.next;
                }

                preToDel.next = preToDel.next.next;

                return header.next;
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public ListNode MergeTwoLists(ListNode l1, ListNode l2)
            {
                var header = new ListNode(-1);
                var pre = header;
                var p1 = l1;
                var p2 = l2;
                while (p1 != null && p2 != null)
                {
                    ListNode ptr;
                    if (p1.val < p2.val)
                    {
                        ptr = p1;
                        p1 = p1.next;
                    }
                    else
                    {
                        ptr = p2;
                        p2 = p2.next;
                    }

                    pre.next = ptr;
                    pre = pre.next;
                    pre.next = null;
                }

                if (p1 != null)
                {
                    pre.next = p1;
                }
                else if (p2 != null)
                {
                    pre.next = p2;
                }

                return header.next;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public ListNode ReverseList(ListNode head)
            {
                var header = new ListNode(-1);

                var p = head;
                while (p != null)
                {
                    var next = p.next;

                    p.next = header.next;
                    header.next = p;

                    p = next;
                }

                return header.next;
            }
        }
    }
}
