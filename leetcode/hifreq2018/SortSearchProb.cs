using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leetcode.hifreq2018.SearchSort
{
    namespace p1
    {
        public class Solution {
            public string LargestNumber(int[] nums) {
                Array.Sort(nums,new Comparator());
                StringBuilder builder = new StringBuilder(nums.Length);
                foreach (var num in nums)
                {
                    builder.Append(num);
                }

                var rs = builder.ToString();
                int idx = 0;
                while (idx < rs.Length && rs[idx] == '0') idx++;
                if (idx < rs.Length)
                {
                    return rs.Substring(idx);
                }
                else
                {
                    return "0";
                }
            }
            class Comparator : Comparer<int>
            {
                public override int Compare(int x, int y)
                {
                    string xy = x + "" + y;
                    string yx = y + "" + x;
                    int len = xy.Length;
                    for (int i = 0; i < len; i++)
                    {
                        var ch1 = xy[i];
                        var ch2 = yx[i];
                        if (ch1 != ch2)
                        {
                            return -(ch1 - ch2);
                        }
                    }
                    return 0;
                }
            }
        }
    }

    namespace p2
    {
        public class Solution {
            public void WiggleSort(int[] nums)
            {
                if (nums == null || nums.Length < 2) return;
                int[] rs = new int[nums.Length];
                Array.Sort(nums);
                int len = nums.Length;
                if ((len & 0x1) == 1)
                {
                    int idx = 0;
                    for (int i = len - 1; i > -1; i -= 2)
                    {
                        rs[i] = nums[idx++];
                    }

                    for (int i = len - 2; i > -1; i -= 2)
                    {
                        rs[i] = nums[idx++];
                    }
                    Array.Copy(rs,nums,len);
                }
                else
                {
                    int idx = 0;
                    for (int i = len - 2; i > -1; i -= 2)
                    {
                        rs[i] = nums[idx++];
                    }

                    for (int i = len - 1; i > -1; i -= 2)
                    {
                        rs[i] = nums[idx++];
                    }
                    Array.Copy(rs,nums,len);
                }
            }
        }
    }

    namespace p3
    {
        public class Solution {
            public int FindPeakElement(int[] nums)
            {
                if (nums == null) return -1;
                else
                {
                    return BinarySearch(nums, 0, nums.Length);
                }
            }

            private int BinarySearch(int[] nums, int from, int to)//[)
            {
                if (from >= to)
                {
                    return -1;
                }
                else
                {
                    int mid = from + ((to - from) >> 1);
                    bool left = mid == 0 || nums[mid - 1] < nums[mid];
                    bool right = mid == nums.Length - 1 || nums[mid + 1] < nums[mid];
                    if (left && right) return mid;
                    else if (left)
                    {
                        return BinarySearch(nums, mid + 1, to);
                    }
                    else
                    {
                        return BinarySearch(nums, from, mid);
                    }
                }
            }
        }
    }

    namespace extra
    {
        public class Solution {
            public IList<IList<int>> GetSkyline(int[][] buildings) {
                List<int[]> points = new List<int[]>();
                PriorityQueue<int> pq = new PriorityQueue<int>(new Comparator());
                foreach (var building in buildings)
                {
                    int left = building[0];
                    int right = building[1];
                    int h = building[2];
                    points.Add(new []{left,-h});
                    points.Add(new []{right,h});
                }

                int prev = 0;
                pq.Add(0);
                points.Sort(new PointComparator());
                var rs = new List<IList<int>>();
                foreach (var point in points)
                {
                    if (point[1] < 0)
                    {
                        pq.Add(-point[1]);
                    }
                    else
                    {
                        pq.Remove(point[1]);
                    }

                    int cur = pq.Peek();
                    if (cur != prev)
                    {
                        prev = cur;
                        List<int> p = new List<int>();
                        p.Add(point[0]);
                        p.Add(cur);
                        rs.Add(p);
                    }
                }

                return rs;
            }

            class PointComparator : Comparer<int[]>
            {
                public override int Compare(int[] x, int[] y)
                {
                    if (x[0] != y[0])
                    {
                        return x[0] - y[0];
                    }
                    else
                    {
                        return x[1] - y[1];
                    }
                }
            }
            class Comparator : Comparer<int>
            {
                public override int Compare(int x, int y)
                {
                    return -(x - y);
                }
            }
            class PriorityQueue<T>
                {
                    private static int sId = 1;
                    private SortedSet<TWrapper> _sortedSet;
                    private static IComparer<T> _comparer;
                    private static bool _isInsertion;
                    class TWrapper
                    {
                        public T val;
                        public int id;

                        public TWrapper(T val)
                        {
                            this.val = val;
                            id = sId++;
                        }
                    }
                    class ComparerWrapper:IComparer<TWrapper>
                    {
                        public int Compare(TWrapper x, TWrapper y)
                        {
                            if (_isInsertion)
                            {
                                var cp = _comparer.Compare(x.val, y.val);
                                if ((cp) != 0)
                                {
                                    return cp;
                                }
                                else
                                {
                                    return x.id - y.id;
                                }
                            }
                            else
                            {
                                return _comparer.Compare(x.val, y.val);
                            }
                        }
                    }
                    public PriorityQueue(IComparer<T> comparer)
                    {
                        _comparer = comparer;
                        _sortedSet = new SortedSet<TWrapper>(new ComparerWrapper());
                    }

                    public void Add(T elem)
                    {
                        TWrapper wrapper = new TWrapper(elem);
                        _isInsertion = true;
                        _sortedSet.Add(wrapper);
                    }

                    public void Remove(T elem)
                    {
                        TWrapper wrapper = new TWrapper(elem);
                        _isInsertion = false;
                        _sortedSet.Remove(wrapper);
                    }

                    public T Peek()
                    {
                        var iter = _sortedSet.GetEnumerator();
                        iter.MoveNext();
                        var rs = iter.Current.val;
                        iter.Dispose();
                        return rs;
                    }
                }
        }
    }

    namespace p4
    {
        public class Solution {
            public int FindDuplicate(int[] nums)
            {
                int sum = 0;
                foreach (var num in nums)
                {
                    sum ^= num;
                }

                int nSum = 0;
                int n = nums.Length - 1;
                for (int i = 1; i <= n; i++)
                {
                    nSum ^= i;
                }

                return sum ^ nSum;
            }
        }
    }

    namespace p4.better
    {
        public class Solution {
            public int FindDuplicate(int[] nums)
            {
                HashSet<int> set = new HashSet<int>();
                foreach (var num in nums)
                {
                    if (set.Contains(num)) return num;
                    else
                    {
                        set.Add(num);
                    }
                }
                return -1;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public IList<int> CountSmaller(int[] nums)
            {
                if (nums == null || nums.Length == 0)
                {
                    return new List<int>();
                }
                LinkedList<int> rs = new LinkedList<int>();
                rs.AddFirst(0);
                int len = nums.Length;
                for (int i = len - 2; i > -1; i--)
                {
                    int val = nums[i];
                    int insertionIdx = FindInsertion(nums, i + 1, len, val);
                    if (insertionIdx != i)
                    {
                        for (int j = i; j < insertionIdx; j++)
                        {
                            nums[j] = nums[j + 1];
                        }
                        nums[insertionIdx] = val;
                    }
                    rs.AddFirst(insertionIdx - i);
                }
                return rs.ToList();
            }

            private int FindInsertion(int[] nums, int from, int to,int target)//[),move to the left
            {
                int mid = (from + to) >> 1;
                int val = nums[mid];
                if (val < target)
                {
                    if (mid == to - 1 || nums[mid+1]>=target)
                    {
                        return mid;
                    }
                    else
                    {
                        return FindInsertion(nums, mid + 1, to, target);
                    }
                }
                else
                {
                    if (mid == from)
                    {
                        return from-1;
                    }
                    else
                    {
                        return FindInsertion(nums, from, mid, target);
                    }
                }
            }
        }
    }

    namespace p5.Better
    {
        public class Solution {
            public IList<int> CountSmaller(int[] nums)
            {
                if (nums == null || nums.Length == 0)
                {
                    return new List<int>();
                }
                else if (nums.Length == 1)
                {
                    return new List<int> {0};
                }

                int len = nums.Length;
                TreeNode root = new TreeNode(nums[len-1]);
                LinkedList<int> rs = new LinkedList<int>();
                rs.AddFirst(0);
                for (int i = len - 2; i > -1; i--)
                {
                    rs.AddFirst(root.Insert(nums[i]));
                }
                return rs.ToList();
            }

            class TreeNode
            {
                public int Val { get; set; }
                public TreeNode Left { get; set; }
                public TreeNode Right { get; set; }
                public int CountOfSons { get; set; }
                public TreeNode(int val)
                {
                    Val = val;
                    CountOfSons = 1;//self included
                    Left = null;
                    Right = null;
                }

                public int Insert(int val)
                {
                    CountOfSons++;
                    if (Val == val)
                    {
                        if (Right != null)
                        {
                            return Right.Insert(val) + LeftChildren;
                        }
                        else
                        {
                            Right = new TreeNode(val);
                            return LeftChildren;
                        }
                    }
                    else if (Val<val)
                    {
                        if (Right != null)
                        {
                            return Right.Insert(val) + LeftChildren+1;
                        }
                        else
                        {
                            Right = new TreeNode(val);
                            return LeftChildren+1;
                        }
                    }
                    else
                    {
                        if (Left != null)
                        {
                            return Left.Insert(val);
                        }
                        else
                        {
                            Left = new TreeNode(val);
                            return 0;
                        }
                    }
                }
                public int LeftChildren
                {
                    get
                    {
                        if (Left != null)
                        {
                            return Left.CountOfSons;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }
    }
}