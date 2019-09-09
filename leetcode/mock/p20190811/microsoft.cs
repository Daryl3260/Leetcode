using System;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode.mock.p20190811
{
    namespace p1
    {
        /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
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
            public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
            {
                var oa = headA;
                var ob = headB;
                if (headA == null || headB == null) return null;
                var ac = Length(headA);
                var bc = Length(headB);
                headA = Invert(headA);
                var ab = Length(headB);
                headB = Invert(headB);
                if (headB != oa)
                {
                    headB = Invert(headB);
                    headA = Invert(headA);
                    return null;
                }
                else
                {
                    headB = Invert(headB);
                    headA = Invert(headA);
                    var abc = (ac + bc + ab - 3) / 2;
                    var a = abc + 1 - bc;
                    var p = headA;
                    for (var i = 0; i < a; i++)
                    {
                        p = p.next;
                    }

                    return p;
                }
            }

            private ListNode Invert(ListNode head)
            {
                var header1 = new ListNode(-1);
                header1.next = head;
                var header2 = new ListNode(-1);
                while (header1.next != null)
                {
                    var node = header1.next;
                    header1.next = node.next;
                    node.next = header2.next;
                    header2.next = node;
                }

                return header2.next;
            }

            private int Length(ListNode head)
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

    namespace p2
    {
        public class Rectangle
        {
            public Tuple<int,int> leftBottom { get; set; }
            public int Width { get;}
            public int Height { get;}
            public int Size => Width * Height;
            public List<Tuple<int,int>> Points => _points;
            private List<Tuple<int, int>> _points;

            public Rectangle(int A,int B,int C,int D)
            {
                leftBottom = new Tuple<int, int>(A,B);
                Width = C - A;
                Height = D - B;
                _points = new List<Tuple<int, int>>
                {
                    leftBottom,new Tuple<int, int>(A,D),new Tuple<int, int>(C,D),new Tuple<int, int>(C,B)
                };
            }
        }
        public class Solution
        {

            public static void Test()
            {
                var s = new Solution();
                Console.WriteLine($"{s.ComputeArea(-5,-3,5,0,-3,-3,3,3)}");
            }
            public int ComputeArea(int A, int B, int C, int D, int E, int F, int G, int H)
            {
                var r1 = new Rectangle(A,B,C,D);
                var r2 = new Rectangle(E,F,G,H);
                return AllArea(r1, r2);
            }

            

            public int AllArea(Rectangle r1, Rectangle r2)
            {
                var r1InsideR2 = new List<Tuple<int,int>>();
                foreach (var point in r1.Points)
                {
                    if (IsInside(r2, point))
                    {
                        r1InsideR2.Add(point);
                    }
                }
                var r2InsideR1 = new List<Tuple<int,int>>();
                foreach (var point in r2.Points)
                {
                    if (IsInside(r1, point))
                    {
                        r2InsideR1.Add(point);
                    }
                }
                if (r1InsideR2.Count == 0)
                {
                    

                    if (r2InsideR1.Count == 0) return r1.Size + r2.Size;
                    else if (r2InsideR1.Count == 2) return AllArea(r2, r1);
                    else return r1.Size;
                }
                else if (r1InsideR2.Count == 4) return r2.Size;
                else if(r1InsideR2.Count==2)
                {
                    var p1 = r1InsideR2[0];
                    var p2 = r1InsideR2[1];
                    if (p1.Item1 == p2.Item1)
                    {
                        Tuple<int, int> p3 = null;
                        foreach (var r1Point in r1.Points)
                        {
                            if (r1Point != p1 && r1Point != p2)
                            {
                                p3 = r1Point;
                                break;
                            }
                        }

                        var p1X = p1.Item1;
                        var p3X = p3.Item1;
                        var r2X = 0;
                        foreach (var r2Point in r2.Points)
                        {
                            var x = r2Point.Item1;
                            if ((p1X <= x && x <= p3X) || (p1X >= x && x >= p3X))
                            {
                                r2X = x;
                                break;
                            }
                        }
                        return r2.Size + r1.Size - Math.Abs(r2X - p1X) * r1.Height;
                    }
                    else//same y
                    {
                        Tuple<int, int> p3 = null;
                        foreach (var r1Point in r1.Points)
                        {
                            if (r1Point != p1 && r1Point != p2)
                            {
                                p3 = r1Point;
                                break;
                            }
                        }

                        var p1Y = p1.Item2;
                        var p3Y = p3.Item2;
                        var r2Y = 0;
                        var ys = new HashSet<int>();
                        foreach (var r2Point in r2.Points)
                        {
                            var y = r2Point.Item2;
                            if ((p1Y <= y && y <= p3Y) || (p1Y >= y && y >= p3Y))
                            {
                                ys.Add(y);
                            }
                        }

                        if (ys.Count==1)
                        {
                            r2Y = ys.First();
                        }
                        else
                        {
                            
                        }
                        return r2.Size + r1.Size - Math.Abs(r2Y - p1Y) * r1.Width;
                    }
                }
                else//1
                {
                    var p1 = r1InsideR2[0];
                    Tuple<int, int> p2 = null;
                    foreach (var r2Point in r2.Points)
                    {
                        if (IsInside(r1, r2Point)&&r2Point.Item1!=p1.Item1&&r2Point.Item2!=p1.Item2)
                        {
                            p2 = r2Point;
                        }
                    }

                    if (p2 == null) return r1.Size+ r2.Size;
                    return r1.Size+ r2.Size - Math.Abs(p1.Item1 - p2.Item1) * Math.Abs(p1.Item2 - p2.Item2);
                }
            }

            private bool IsInside(Rectangle rect, Tuple<int, int> point)
            {
                var x = point.Item1;
                var y = point.Item2;
                return rect.leftBottom.Item1 <= x && x <= rect.leftBottom.Item1 + rect.Width &&
                       rect.leftBottom.Item2 <= y && y <= rect.leftBottom.Item2 + rect.Height;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public static void Test()
            {
//                [186,419,83,408]
//                6249
                Console.WriteLine($"{new Solution().CoinChange(new []{186,419,83,408},6249)}");
            }
            public class AComparer : IComparer<int>
            {
                public int Compare(int x, int y)
                {
                    return -(x - y);
                }
            }
            public int CoinChange(int[] coins, int amount)
            {
                if (amount == 0) return 0;
                var backup = new Dictionary<int,int?>();
                foreach (var coin in coins)
                {
                    backup[coin] = 1;
                }
                Array.Sort(coins,new AComparer());
                var rs = Coins(coins, amount, backup);
                return rs ?? -1;
            }

            private int? Coins(int[] coins, int amount, Dictionary<int, int?> backup)
            {
                if (backup.ContainsKey(amount)) return backup[amount];
                var rs = new int?();
                for (var i = coins.Length - 1; i > -1; i--)
                {
                    var val = coins[i];
                    if (val < amount)
                    {
                        var cnt = Coins(coins, amount - val, backup);
                        if (cnt.HasValue && (!rs.HasValue || rs.Value > cnt.Value + 1))
                        {
                            rs = cnt.Value + 1;
                        }
                    }
                }

                backup[amount] = rs;
                return rs;
            }
        }
    }
}