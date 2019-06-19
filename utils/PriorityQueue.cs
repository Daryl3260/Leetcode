using System.Collections.Generic;

namespace Leetcode.utils
{
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