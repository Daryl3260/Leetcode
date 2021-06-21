﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.leetcode_cn.interview2020.StackAndQueue
{
    namespace p1
    {
        public class Solution
        {
            public int MinMeetingRooms(int[][] intervals)
            {
                Array.Sort(intervals, MyComparer.Instance);
                List<List<int[]>> rooms = new List<List<int[]>>();
                rooms.Add(new List<int[]> { intervals[0] });

                for (var i = 1; i < intervals.Length; i++)
                {
                    var interval = intervals[i];

                    var inserted = false;

                    for (var j = rooms.Count - 1; j > -1; j--)
                    {
                        var room = rooms[j];
                        if (room[room.Count - 1][1] <= interval[0])
                        {
                            room.Add(interval);
                            inserted = true;
                        }

                        if (inserted)
                        {
                            break;
                        }
                    }

                    if (!inserted)
                    {
                        rooms.Add(new List<int[]> { interval });
                    }
                }

                return rooms.Count;
            }

            public class MyComparer : IComparer<int[]>
            {
                private MyComparer() { }

                private static MyComparer instance = new MyComparer();

                public static MyComparer Instance = instance;

                public int Compare(int[] x, int[] y)
                {
                    return x[1] - y[1];
                }
            }
        }
    }

    namespace p3
    {
        public class Solution
        {
            public IList<string> TopKFrequent(string[] words, int k)
            {
                var pq = new PriorityQueue<Element>((x, y) =>
                {
                    if (x.Count != y.Count)
                    {
                        return x.Count - y.Count;
                    }
                    else
                    {
                        return -x.Word.CompareTo(y.Word);
                    }
                });

                var dict = new Dictionary<string, int>();
                foreach (var word in words)
                {
                    if (dict.TryGetValue(word, out var count))
                    {
                        dict[word]++;
                    }
                    else
                    {
                        dict[word] = 1;
                    }
                }

                foreach (var word in dict.Keys)
                {
                    var count = dict[word];
                    pq.Add(new Element { Count = count, Word = word });
                }

                var rs = new List<string>();

                for (var i = 0; i < k; i++)
                {
                    var word = pq.Pop().Word;
                    rs.Add(word);
                }

                return rs;
            }

            public class Element
            {
                public string Word { get; set; }
                public int Count { get; set; }
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public string MinRemoveToMakeValid(string s)
            {
                var indexToIgnore = new List<int>();

                var leftIdxList = new List<int>();
                for (var i = 0; i < s.Length; i++)
                {
                    var ch = s[i];

                    if (ch == '(')
                    {
                        leftIdxList.Add(i);
                    }
                    else if (ch == ')')
                    {
                        if (leftIdxList.Any())
                        {
                            leftIdxList.RemoveAt(leftIdxList.Count - 1);
                        }
                        else
                        {
                            indexToIgnore.Add(i);
                        }
                    }
                }

                indexToIgnore.AddRange(leftIdxList);

                var ignoreArr = new bool[s.Length];

                foreach (var idx in indexToIgnore)
                {
                    ignoreArr[idx] = true;
                }

                var builder = new StringBuilder();
                for (var i = 0; i < s.Length; i++)
                {
                    if (!ignoreArr[i])
                    {
                        builder.Append(s[i]);
                    }
                }

                return builder.ToString();
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public int[] TwoSum(int[] nums, int target)
            {
                var dict = new Dictionary<int, List<int>>();

                for (var i = 0; i < nums.Length; i++)
                {
                    var num = nums[i];
                    if (dict.TryGetValue(num, out var list))
                    {
                        list.Add(i);
                    }
                    else
                    {
                        dict[num] = new List<int> { i };
                    }
                }

                Array.Sort(nums);

                var left = 0;
                var right = nums.Length - 1;

                while (true)
                {
                    var leftVal = nums[left];
                    var rightVal = nums[right];
                    var sum = leftVal + rightVal;
                    if (sum == target)
                    {
                        if (leftVal != rightVal)
                        {
                            return new int[] { dict[leftVal][0], dict[rightVal][0] };
                        }
                        else
                        {
                            return dict[leftVal].ToArray();
                        }
                    }
                    else if (sum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }
        }
    }

    namespace p6
    {
        public class Solution
        {
            public IList<IList<string>> GroupAnagrams(string[] strs)
            {
                return null;
            }
        }
    }
}
