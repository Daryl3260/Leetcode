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
        public class Node
        {
            public int Val { get; set; }
            public Node Next { get; set; }
        }

        public class Solution
        {
            public static void Test()
            {
                var head = new Node {Val = 1};
                head.Next = new Node {Val = 2};
                head.Next.Next = new Node {Val = 2};
                head.Next.Next.Next = new Node {Val = 3};
                head = new Solution().DeleteDuplicate(head);
                while (head != null)
                {
                    Console.WriteLine($"{head.Val}");
                    head = head.Next;
                }
            }

            public Node DeleteDuplicate(Node head)
            {
                var header = new Node();
                header.Next = head;
                var p = head;
                while (p != null)
                {
                    var q = p;
                    while (q != null && q.Val == p.Val) q = q.Next;
                    p.Next = q;
                    p = q;
                }

                return header.Next;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public static void Test()
            {/*
            "mississippi"
"mis*is*p*."
            */
                Console.WriteLine($"{new Solution().IsMatch("a","c*d*e*a")}");//"mississippi","mis*is*p*."
            }
            private bool[][] trapped;
            public bool IsMatch(string s, string p)
            {
                if (string.IsNullOrEmpty(s))
                {
                    if (string.IsNullOrEmpty(p)) return true;
                    for (var i = 0; i < p.Length;)
                    {
                        if (i < p.Length - 1 && p[i + 1] == '*') i += 2;
                        else return false;
                    }
                    return true;
                }
                else if (string.IsNullOrEmpty(p)) return false;

                var sLen = s.Length;
                var plen = p.Length;
                trapped = new bool[sLen+1][];
                for (var i = 0; i < sLen; i++)
                {
                    for (var j = 0; j < plen; j++)
                    {
                        trapped[i] = new bool[plen+1];
                    }
                }
                return SubMatch(s, 0, p, 0);
            }

            private bool SubMatch(string s, int i, string p, int j)
            {
                bool rs;
                if (i == s.Length)
                {
//                    return j == p.Length || (j == p.Length - 2 && p[j + 1] == '*');
                    if (j == p.Length) return true;
                    for (var k = j; k < p.Length;)
                    {
                        if (k < p.Length - 1 && p[k + 1] == '*') k += 2;
                        else return false;
                    }

                    return true;
                }
                else if (j == p.Length || trapped[i][j])
                {
                    return false;
                }
                
                if (j < p.Length - 1 && p[j + 1] == '*')
                {
                    if (s[i] == p[j] || p[j] == '.')
                    {
                        rs = SubMatch(s, i + 1, p, j) || SubMatch(s, i, p, j + 2);
                    }
                    else
                    {
                        rs = SubMatch(s, i, p, j + 2);
                    }
                    
                }
                else
                {
                    if (s[i] == p[j] || p[j] == '.') rs = SubMatch(s, i + 1, p, j + 1);
                    else rs = false;
                }
                if (!rs)
                {
                    trapped[i][j] = true;
                }
                return rs;
            }
        }
    }
}