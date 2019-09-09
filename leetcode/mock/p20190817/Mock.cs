using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode.mock.p20190817.Mock
{
    namespace p1
    {


        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int x)
            {
                val = x;
            }
        }

        public class Solution
        {
            public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
            {
                if (root == null || p == null || q == null) return null;
                if (p == q) return p;
                if ((p.val <= root.val && q.val >= root.val) || (p.val >= root.val && q.val <= root.val)) return root;
                if (root.val <= p.val && root.val <= q.val) return LowestCommonAncestor(root.right, p, q);
                else return LowestCommonAncestor(root.left, p, q);
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public int CompareVersion(string version1, string version2)
            {
                return Compare(Convert(version1), Convert(version2));
            }

            private int Compare(List<string> v1, List<string> v2)
            {
                var len = Math.Max(v1.Count, v2.Count);
                var v1Count = v1.Count;
                var v2Count = v2.Count;
                for (int i = 0; i < len-v1Count; i++)
                {
                    v1.Add("0");
                }

                for (int i = 0; i < len - v2Count; i++)
                {
                    v2.Add("0");
                }

                
                for (var i = 0; i < len; i++)
                {
                    var str1 = v1[i];
                    var str2 = v2[i];
                    int cp;
                    if (str1.Length != str2.Length)
                    {
                        var strlen = Math.Max(str1.Length, str2.Length);
                        var str1Len = str1.Length;
                        var str2Len = str2.Length;
                        var builder = new StringBuilder();
                        for (var j = 0; j < strlen - str1Len; j++)
                        {
                            builder.Append('0');
                        }

                        str1 = builder.ToString() + str1;
                        builder.Clear();
                        for (var j = 0; j < strlen - str2Len; j++)
                        {
                            builder.Append('0');
                        }

                        str2 = builder.ToString() + str2;
                    }
                    cp = String.Compare(str1, str2);
                    if (cp != 0) return cp;
                }

                return 0;
            }
            private List<string> Convert(string version)
            {
                var arr = version.Split('.');
                var rs = new List<string>();
                foreach (var str in arr)
                {
                    rs.Add(str.TrimStart('0'));
                }
                return rs;
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public static void Test()
            {
                var solution = new Solution();
                Console.WriteLine($"{solution.Multiply("123","456")}");
            }
            public string Multiply(string num1, string num2)
            {
                if (num1 == "0" || num2 == "0") return "0";
                else if (num1 == "1") return num2;
                else if (num2 == "1") return num1;
                var sum = "0";
                if (num1.Length < num2.Length)
                {
                    var temp = num1;
                    num1 = num2;
                    num2 = temp;
                }

                for (var i = 0; i < num2.Length; i++)
                {
                    var production = SingleMultiply(num1, num2[num2.Length-1 - i]);
                    sum = Add(sum,ShiftEnd(production, i));
                }
                return sum;
            }

            private string SingleMultiply(string num, char x)
            {
                if (x == '0') return "0";
                else if (x == '1') return num;
                var arr = new List<char>();
                var extra = 0;
                var n2 = x - '0';
                for (var i = num.Length - 1; i > -1; i--)
                {
                    var n1 = num[i] - '0';
                    var production = n1 * n2+extra;
                    extra = production / 10;
                    arr.Add((char)(production%10+'0'));
                }
                if(extra>0)arr.Add((char)('0'+extra));
                var builder = new StringBuilder(arr.Count);
                for (var i = arr.Count - 1; i > -1; i--)
                {
                    builder.Append(arr[i]);
                }
                return builder.ToString().TrimStart('0');
            }

            private string Add(string num1, string num2)
            {
                var len = Math.Max(num1.Length, num2.Length);
                num1 = ShiftStart(num1, len - num1.Length);
                num2 = ShiftStart(num2, len - num2.Length);
                var arr = new List<char>();
                var extra = 0;
                for (var i = len - 1; i > -1; i--)
                {
                    var n1 = num1[i] - '0';
                    var n2 = num2[i] - '0';
                    var sum = n1 + n2 + extra;
                    if (sum > 9)
                    {
                        extra = 1;
                        sum %= 10;
                    }
                    else extra = 0;
                    arr.Add((char)(sum+'0'));
                }
                if(extra>0)arr.Add('1');
                var builder = new StringBuilder(arr.Count);
                for (var i = arr.Count - 1; i > -1; i--)
                {
                    builder.Append(arr[i]);
                }

                return builder.ToString();
            }

            private string ShiftStart(string num, int n)
            {
                var builder = new StringBuilder(n);
                for (int i = 0; i < n; i++)
                {
                    builder.Append('0');
                }
                return builder.ToString() + num;
            }
            private string ShiftEnd(string num, int n)
            {
                if (n == 0) return num;
                var builder = new StringBuilder(n);
                for (int i = 0; i < n; i++)
                {
                    builder.Append('0');
                }
                return num + builder.ToString();
            }
        }
    }
}