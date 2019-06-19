using System.Collections.Generic;

namespace Leetcode.hifreq2018
{
    namespace TreeProb
    {
        namespace p1
        {
            public class Solution
            {
                public IList<IList<int>> GetSkyline(int[][] buildings)
                {
                    if (buildings == null || buildings.Length < 1)
                    {
                        return new List<IList<int>>();
                    }
                    int len = buildings.Length;
                    int left = buildings[0][0];
                    int right = buildings[0][1];
                    for (int i = 0; i < len; i++)
                    {
                        int r = buildings[i][1];
                        right = right < r ? r : right;
                    }

                    int[] heights = new int[(right+1)-(left-1)+1];
                    left--;
                    foreach (var building in buildings)
                    {
                        int l = building[0];
                        int r = building[1];
                        int h = building[2];
                        for (int i = l - left; i <= r - left; i++)
                        {
                            int height = heights[i];
                            if (height < h)
                            {
                                heights[i] = h;
                            }
                        }
                    }
                    List<IList<int>> rs = new List<IList<int>>();
                    for (int i = 1; i < heights.Length; i++)
                    {
                        int x = left + i;
                        int h = heights[i];
                        int lastH = heights[i - 1];
                        if (h > lastH)
                        {
                            rs.Add(MakeList(x,h));
                        }
                        else if(h<lastH)
                        {
                            rs.Add(MakeList(x-1,h));
                        }
                    }
                    
                    return rs;
                }

                private List<int> MakeList(int x, int y)
                {
                    List<int> rs = new List<int>();
                    rs.Add(x);
                    rs.Add(y);
                    return rs;
                }
            }
        }

        namespace p1.better
        {
            public class Solution {
                public IList<IList<int>> GetSkyline(int[][] buildings) {
                    List<int[]> heights = new List<int[]>();
                    foreach (var building in buildings)
                    {
                        int left = building[0];
                        int right = building[1];
                        int height = building[2];
                        heights.Add(new []{left,-height});
                        heights.Add(new []{right,height});
                    }
                    heights.Sort(new Comparator());
                    PriorityQueue<int> heap = new PriorityQueue<int>(new IntComparator());
                    int prev = 0;
                    heap.Add(0);
                    List<IList<int>> rs = new List<IList<int>>();
                    foreach (var height in heights)
                    {
                        int h = height[1];
                        int x = height[0];
                        if (h < 0)//start
                        {
                            heap.Add(-h);
                        }
                        else//end
                        {
                            heap.Remove(h);
                        }

                        var cur = heap.Peek();
                        if (prev != cur)
                        {
                            prev = cur;
                            List<int> coor = new List<int>();
                            coor.Add(x);
                            coor.Add(cur);
                            rs.Add(coor);
                        }
                    }

                    return rs;
                }

                class IntComparator : Comparer<int>
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
                class Comparator : Comparer<int[]>
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
            }
        }
    }
}