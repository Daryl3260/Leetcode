using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.jzoffer.Chapter3
{
    namespace p1
    {
        public class Solution
        {

            public static void Test()
            {
                var solution = new Solution();
                Console.WriteLine(solution.Power(2, 8));
            }

            public double Power(double num, int exponent)
            {
                if (exponent < 0) return 1.0 / PositivePower(num, exponent);
                return PositivePower(num, exponent);
            }

            public double PositivePower(double num, int exponent)
            {
                if (exponent == 0) return 1.0;
                if (exponent == 1) return num;
                if (num == 0 || num == 1) return num;
                var dict = buildDict(num, exponent);
                return SubPower(num, exponent, dict);
            }

            private Dictionary<int, double> buildDict(double num, int exponent)
            {
                var dict = new Dictionary<int, double>();
                dict[0] = 1.0;
                dict[1] = num;
                var n = num;
                var exp = 1;
                while (exponent > (exp << 1))
                {
                    n *= n;
                    exp <<= 1;
                    dict[exp] = n;
                }

                return dict;
            }

            private double SubPower(double num, int exponent, Dictionary<int, double> dict)
            {
                if (dict.ContainsKey(exponent)) return dict[exponent];
                var max = 0;
                foreach (var key in dict.Keys)
                {
                    if (key < exponent && key > max)
                    {
                        max = key;
                    }
                }

                return SubPower(num, exponent - max, dict) * dict[max];
            }
        }
    }

    namespace p1.better
    {
        public class Solution
        {
            public static void Test()
            {
                var s1 = new p1.Solution();
                var s2 = new Solution();
                var num = 2;
                var exponent = 11;
                Console.WriteLine($"{s1.PositivePower(2, 11)}");
                Console.WriteLine($"{s2.PositivePower(2, 11)}");
            }

            public double Power(double num, int exponent)
            {
                if (exponent < 0) return PositivePower(num, -exponent);
                return PositivePower(num, exponent);
            }

            public double PositivePower(double num, int exponent)
            {
                if (exponent == 0) return 1.0;
                if (exponent == 1) return num;
                var rs = PositivePower(num, exponent >> 1);
                rs *= rs;
                if ((exponent & 0x1) == 1) rs *= num;
                return rs;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public static void Test()
            {
                var solution = new Solution();
                solution.PrintOneToN(5);
            }

            public void PrintOneToN(int n)
            {
                var list = new List<char>();
                list.Add('1');
                while (list.Count < n + 1)
                {
                    Console.WriteLine($"{ToStr(list)}");
                    Next(list);
                }
            }

            public void Next(List<char> list)
            {
                var i = 0;
                for (; i < list.Count; i++)
                {
                    if (list[i] != '9')
                    {
                        list[i] = (char) (list[i] + 1);
                        break;
                    }
                    else
                    {
                        list[i] = '0';
                    }
                }

                if (i == list.Count) list.Add('1');
            }

            public string ToStr(List<char> list)
            {
                var builder = new StringBuilder();
                var len = list.Count;
                for (var i = len - 1; i > -1; i--)
                {
                    builder.Append(list[i]);
                }

                return builder.ToString();
            }
        }
    }

    namespace p3
    {
        public class Node
        {
            public int Val { get; set; }
            public Node Next { get; set; }
        }

        public class Solution
        {
            public Node DeleteNode(Node head, Node node)
            {
                var header = new Node();
                header.Next = head;
                if (node.Next != null)
                {
                    node.Val = node.Next.Val;
                    node.Next = node.Next.Next;
                }
                else
                {
                    var p = header;
                    while (p.Next != node) p = p.Next;
                    p.Next = null;
                }

                return header.Next;
            }
        }

}

    namespace p4
    {
        

        public class Solution
        {
            public static void Test()
            {

                var arr = new[] {1, 2, 3, 4, 5, 6};
                new Solution().BetterReorder(arr);
                foreach (var num in arr)
                {
                    Console.Write(num+"\t");
                }
            }

            public void Reorder(int[] arr)
            {
                if (arr == null || arr.Length < 1) return;
                var odds = new List<int>();
                var evens = new List<int>();
                foreach (var num in arr)
                {
                    if((num&0x1)==1)odds.Add(num);
                    else evens.Add(num);
                }

                var i = 0;
                foreach (var num in odds)
                {
                    arr[i++] = num;
                }

                foreach (var num in evens)
                {
                    arr[i++] = num;
                }
            }

            public void BetterReorder(int[] arr)
            {
                if (arr == null || arr.Length < 1) return;
                var oddBorder = -1;
                for (var i = 1; i < arr.Length; i++)
                {
                    if ((arr[i] & 0x1) == 1)
                    {
                        var temp = arr[oddBorder + 1];
                        arr[oddBorder + 1] = arr[i];
                        arr[i] = temp;
                        oddBorder++;
                    }
                }
            }
            
        }
}

    namespace p5
    {
        public class Node
        {
            public int Val { get; set; }
            public Node Next { get; set; }
        }

        public class Solution
        {
            public Node LastK(Node head,int k)
            {
                if (head == null || k<1) return null;
                var header = new Node();
                header.Next = head;
                var back = head;
                var front = head;
                for (var i = 1; i < k; i++)
                {
                    if (front.Next == null) return null;
                    else front = front.Next;
                }

                while (front.Next != null)
                {
                    front = front.Next;
                    back = back.Next;
                }
                return back;
            }
        }
}

    namespace p6
    {
        public class Node
        {
            public int Val { get; set; }
            public Node Next { get; set; }
        }

        public class Solution
        {
            public Node Interjunction(Node head)
            {
                if (head == null || head.Next == null) return null;
                var header = new Node{Next = head};
                var fast = header;
                var slow = header;
                while (true)
                {
                    if (fast.Next == null || fast.Next.Next == null) return null;
                    fast = fast.Next.Next;
                    slow = slow.Next;
                    if (fast == slow) return fast;
                }
            }
        }
    }

    namespace p7
    {
        public class Solution
        {
            
        }
    }
    
}