using System;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode.contest.week142
{
    namespace p1
    {
        public class Solution
        {
            //Return the minimum, maximum, mean, median, and mode of the sample respectively,
            public double[] SampleStats(int[] count)
            {

                double[] rs = new double[5];

                var modeIdx = -1;
                var modeNum = -1;
                var list = new List<double>();
                for (int i = 0; i < count.Length; i++)
                {
                    if (count[i] > 0)
                    {
                        var n = count[i];
                        for (int j = 0; j < n; j++)
                        {
                            list.Add(i);
                        }

                        if (count[i] > modeNum)
                        {
                            modeNum = count[i];
                            modeIdx = i;
                        }
                    }
                }

                rs[0] = list[0];
                rs[1] = list[list.Count - 1];
                rs[2] = list.Average(elem => elem);
                rs[4] = modeIdx;
                if ((list.Count & 1) == 1)
                {
                    rs[3] = list[list.Count / 2];
                }
                else
                {
                    var left = list[list.Count / 2 - 1];
                    var right = list[list.Count / 2];
                    rs[3] = (left + right) / 2.0;
                }

                return rs;
            }
        }
    }

    namespace p1.better
    {
        public class Solution
        {
            public double[] SampleStats(int[] count)
            {
                var rs = new double[5];

                for (int i = 0; i < count.Length; i++)
                {
                    if (count[i] > 0)
                    {
                        rs[0] = i;
                        break;
                    }
                }

                for (int i = count.Length - 1; i > -1; i--)
                {
                    if (count[i] > 0)
                    {
                        rs[1] = i;
                        break;
                    }
                }

                var modeIdx = -1;
                for (int i = 0; i < count.Length; i++)
                {
                    if (count[i] == 0) continue;
                    if (modeIdx == -1 || count[modeIdx] < count[i])
                    {
                        modeIdx = i;
                    }
                }

                rs[4] = modeIdx;
                var dict = new SortedDictionary<int, int>();
                for (int i = 0; i < count.Length; i++)
                {
                    if (count[i] > 0) dict[i] = count[i];
                }

                long num = dict.Values.Sum(elem => elem);
                long sum = 0;
                var mean = 0.0;
                foreach (var pair in dict)
                {
                    var key = pair.Key;
                    var value = pair.Value;
                    sum += key * value;
                }

                mean = sum * 1.0 / num;

                rs[2] = mean;
                if ((num & 1) == 1)
                {
                    rs[3] = FindNth(dict, (num + 1) / 2);
                }
                else
                {
                    var left = FindNth(dict, num / 2);
                    var right = FindNth(dict, num / 2 + 1);
                    rs[3] = (left + right) / 2.0;
                }

                return rs;
            }

            private int FindNth(SortedDictionary<int, int> dict, long n)
            {
                long sum = 0;
                foreach (var entry in dict)
                {
                    var key = entry.Key;
                    var value = entry.Value;
                    if (sum < n && sum + value >= n)
                    {
                        return key;
                    }
                    else sum += value;
                }

                return -1;
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public class MyComparer:Comparer<int[]>
            {
                public override int Compare(int[] x, int[] y)
                {
                    return x[2] - y[2];
                }
            }

            private static MyComparer _myComparer=new MyComparer();
            public bool CarPooling(int[][] trips, int capacity)
            {
                SortedDictionary<int,List<int[]>> startDict = new SortedDictionary<int, List<int[]>>();
                var endDict = new SortedDictionary<int, List<int[]>>();
                foreach (var trip in trips)
                {
                    var start = trip[1];
                    var end = trip[2];
                    if (!startDict.ContainsKey(start))
                    {
                        startDict[start]=new List<int[]>();
                    }

                    if (!endDict.ContainsKey(end))
                    {
                        endDict[end]=new List<int[]>();
                    }
                    startDict[start].Add(trip);
                    endDict[end].Add(trip);
                }
                

                var freeSpace = capacity;
                var tobeFreed = new SortedSet<int>();
                foreach (var entry in startDict)
                {
                    
                    var start = entry.Key;
                    while (true)
                    {
                        if (tobeFreed.Count == 0) break;
                        var iter = tobeFreed.GetEnumerator();
                        iter.MoveNext();
                        var top = iter.Current;
                        if (top > start) break;
                        tobeFreed.Remove(top);
                        var topTripList = endDict[top];
                        foreach (var trip in topTripList)
                        {
                            freeSpace += trip[0];
                        }
                    }
                    var list = entry.Value;
                    list.Sort(_myComparer);
                    var allPassengers = list.Sum(elem => elem[0]);
                    if (freeSpace < allPassengers)
                    {
                        return false;
                    }
                    else
                    {
                        foreach (var trip in list)
                        {
                            tobeFreed.Add(trip[2]);
                        }
                        
                        freeSpace -= allPassengers;
                    }
                }

                return true;
            }
        }
    }
    
    /**
 * // This is MountainArray's API interface.
 * // You should not implement it, or speculate about its implementation
 * 
 */

    namespace p3
    {
        class MountainArray
        {
            public int Get(int index)
            {
                return -1;
            }
            public int Length()
            {
                return -1;
            }
            
        }

        class Solution
        {
            
            public int FindInMountainArray(int target, MountainArray mountainArr)
            {
                _len = mountainArr.Length();
                _mountainArray = mountainArr;
                _peak = FindPeak(1, _len - 1);
                if (GetHeight(_peak) == target) return _peak;
                var rs = FindBetween(target, 0,_peak-1 );
                return rs != -1 ? rs : FindBetween(target, _peak + 1, _len - 1);
            }

            private int FindBetween(int target, int left, int right)
            {
                if (right - left < 4)
                {
                    for (int i = left; i <= right; i++)
                    {
                        if (GetHeight(i) == target) return i;
                    }

                    return -1;
                }

                if (left < _peak)
                {
                    var mid = (left + right) / 2;
                    if (GetHeight(mid) == target) return mid;
                    if (target < GetHeight(mid))
                    {
                        return FindBetween(target, left, mid - 1);
                    }
                    else
                    {
                        return FindBetween(target, mid + 1, right);
                    }
                }
                else
                {
                    var mid = (left + right) / 2;
                    if (GetHeight(mid) == target) return mid;
                    if (target < GetHeight(mid))
                    {
                        return FindBetween(target, mid + 1, right);
                    }
                    else
                    {
                        return FindBetween(target, left, mid - 1);

                    }
                }
            }

            private int _peak;
            private int _len;
            private MountainArray _mountainArray;
            private Dictionary<int,int> _dict = new Dictionary<int, int>();
            private int GetHeight(int idx)
            {
                var arr = _mountainArray;
                if (_dict.ContainsKey(idx)) return _dict[idx];
                var rs = arr.Get(idx);
                _dict[idx] = rs;
                return rs;
            }

            private int FindPeak(int left, int right)
            {
                if (right - left < 4)
                {
                    for (int i = left; i <= right; i++)
                    {
//                        if (i == 0 || i == _len - 1) continue;
                        if (GetHeight(i) > GetHeight(i - 1) && GetHeight(i) > GetHeight(i + 1)) return i;
                    }
                    throw new Exception($"Can't find peak between {left},{right}");
                }

                var mid = (left + right) / 2;
                if (GetHeight(mid) > GetHeight(mid - 1) && GetHeight(mid) > GetHeight(mid + 1)) return mid;
                if (GetHeight(mid) > GetHeight(mid - 1))
                {
                    return FindPeak(mid + 1, right);
                }
                else
                {
                    return FindPeak(left, mid - 1);
                }
            }
        }
    }

}