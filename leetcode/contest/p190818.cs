using System;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode.contest.p190818
{
    namespace p1
    {
        public class Solution {
            public int CountCharacters(string[] words, string chars)
            {
                var charsSet = ConstructDict(chars);
                var rs = 0;
                foreach (var word in words)
                {
                    var wordSet = ConstructDict(word);
                    var fulfill = true;
                    for (var i = 0; i < 26; i++)
                    {
                        if (wordSet[i] > charsSet[i])
                        {
                            fulfill = false;
                            break;
                        }
                    }

                    if (fulfill)
                    {
                        rs += word.Length;
                    }
                }

                return rs;
            }

            public int[] ConstructDict(string chars)
            {
                var rs = new int[26];
                foreach (var c in chars)
                {
                    rs[c - 'a']++;
                }
                return rs;
            }
        }
    }

    namespace p2
    {

        public class TreeNode {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        public class Solution {
            public int MaxLevelSum(TreeNode root)
            {
                var rs = 1;
                var maxVal = root.val;
                var currentLevel = 1;
                var nodeQueue = new Queue<TreeNode>();
                var levelQueue = new Queue<int>();
                nodeQueue.Enqueue(root);
                levelQueue.Enqueue(1);
                var levelNodes = new List<int>();
                while (nodeQueue.Count > 0)
                {
                    var node = nodeQueue.Dequeue();
                    var level = levelQueue.Dequeue();
                    if (currentLevel < level)
                    {
                        var sum = levelNodes.Sum(elem => elem);
                        if (sum > maxVal)
                        {
                            maxVal = sum;
                            rs = currentLevel;
                        }

                        currentLevel = level;
                        levelNodes.Clear();
                    }
                    levelNodes.Add(node.val);
                    if (node.left != null)
                    {
                        nodeQueue.Enqueue(node.left);
                        levelQueue.Enqueue(level+1);
                    }

                    if (node.right != null)
                    {
                        nodeQueue.Enqueue(node.right);
                        levelQueue.Enqueue(level+1);
                    }
                    
                }

                return rs;
            }
        }
}

    namespace p3
    {
        public class Solution
        {
            public int MaxDistance(int[][] grid)
            {
                var updated = new List<Tuple<int,int>>();
                var temp = new List<Tuple<int,int>>();
                var rows = grid.Length;
                var cols = grid[0].Length;
                int[][] len = new int[rows][];
                for (var i = 0; i < rows; i++)
                {
                    len[i]=new int[cols];
                    for (var j = 0; j < cols; j++)
                    {
                        len[i][j] = -1;
                    }
                }

                var noLand = true;
                var noWater = true;
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (grid[i][j] == 1)
                        {
                            len[i][j] = 0;
                            updated.Add(new Tuple<int, int>(i, j));
                            noLand = false;
                        }
                        else noWater = false;
                    }
                }
                if (noLand | noWater) return -1;
                while (updated.Count > 0)
                {
                    foreach (var node in updated)
                    {
                        var x = node.Item1;
                        var y = node.Item2;
                        var newValue = len[x][y] + 1;
                        if(Update(x-1,y,newValue,len))temp.Add(new Tuple<int, int>(x-1,y));
                        if(Update(x+1,y,newValue,len))temp.Add(new Tuple<int, int>(x+1,y));
                        if(Update(x,y-1,newValue,len))temp.Add(new Tuple<int, int>(x,y-1));
                        if(Update(x,y+1,newValue,len))temp.Add(new Tuple<int, int>(x,y+1));
                    }

                    var tt = updated;
                    updated = temp;
                    temp = tt;
                    temp.Clear();
                }
                return len.Max(elem => elem.Max(num => num));
            }

            private bool Update(int x, int y,int newValue, int[][] len)
            {
                var rows = len.Length;
                var cols = len[0].Length;
                if (x < 0 || y < 0 || x == rows || y == cols || (len[x][y] != -1 && len[x][y] < newValue))
                {
                    return false;
                }

                len[x][y] = newValue;
                return true;
            }
        }
    }

    namespace p4
    {
        
        public class Solution
        {
            public static void Test()
            {
                var s = "aaa";
                Console.WriteLine($"{new Solution().LastSubstring(s)}");
            }

            private static string str;
            public string LastSubstring(string s)
            {
                if (s.Length == 1) return s;
                str = s;
                var comparer = new MyComparer();
                var maxFirst = s.Max(elem => elem);
                var indexList = new List<int>();
                for (var i = 0; i < s.Length; i++)
                {
                    if(s[i]==maxFirst)indexList.Add(i);
                }

                if (indexList.Count == 1) return str.Substring(indexList[0]);
                var compareLists = new List<Tuple<int,int>>();//start,length
                foreach (var index in indexList)
                {
                    if (index != s.Length - 1)
                    {
                        compareLists.Add(new Tuple<int, int>(index,2));
                    }
                }
                var temp = new List<Tuple<int,int>>();
                while (true)
                {
                    var max = compareLists[0];
                    foreach (var tuple in compareLists)
                    {
                        var maxStr = str.Substring(max.Item1, max.Item2);
                        var tupleStr = str.Substring(tuple.Item1, tuple.Item2);
                        if (string.Compare(maxStr, tupleStr) < 0)
                        {
                            max = tuple;
                        }
                    }

                    var maxStrFinal = str.Substring(max.Item1, max.Item2);
                    foreach (var tuple in compareLists)
                    {
                        if (str.Substring(tuple.Item1, tuple.Item2) == maxStrFinal)
                        {
                            temp.Add(tuple);
                        }
                    }

                    if (temp.Count == 1)
                    {
                        return str.Substring(temp[0].Item1);
                    }
                    var tt = compareLists;
                    compareLists = temp;
                    temp = tt;
                    temp.Clear();
                    Tuple<int, int> tooLong = null;
                    foreach (var tuple in compareLists)
                    {
                        if (tuple.Item1 + tuple.Item2 == str.Length)
                        {
                            tooLong = tuple;
                            break;
                        }
                    }

                    if (tooLong != null) compareLists.Remove(tooLong);
                    compareLists = compareLists.Select(elem => new Tuple<int, int>(elem.Item1, elem.Item2 + 1))
                        .ToList();
                }
            }

            public class MyComparer : Comparer<Tuple<int, int>>
            {
                public override int Compare(Tuple<int, int> x, Tuple<int, int> y)
                {
                    var str1 = str.Substring(x.Item1, x.Item2);
                    var str2 = str.Substring(y.Item1, y.Item2);
                    return string.Compare(str1, str2);
                }
            }
        }
    }
}