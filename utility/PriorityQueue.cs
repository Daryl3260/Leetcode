using System.Collections.Generic;

namespace Leetcode.leetcode_cn
{
    public class PriorityQueue<T>//Max Heap
    {
        List<T> arr;
        //IComparer<T> comparer;
        public delegate int CompareDelegate(T a, T b);
        CompareDelegate compare;
        public PriorityQueue(CompareDelegate compare)
        {
            arr = new List<T>();
            this.compare += compare;
        }

        public void Clear()
        {
            arr.Clear();
        }
        //public PriorityQueue(IComparer<T> comparer)
        //{
        //    this.comparer = comparer;
        //    arr = new List<T>();
        //}
        public void Add(T val)
        {
            arr.Add(val);
            MoveUp(arr.Count - 1);
        }
        public T Pop()
        {
            T rs = arr[0];
            T tmp = arr[0];
            arr[0] = arr[arr.Count - 1];
            arr[arr.Count - 1] = tmp;
            arr.RemoveAt(arr.Count - 1);
            if (arr.Count > 0)
            {
                MoveDown(0);
            }
            return rs;
        }
        public bool IsEmpty()
        {
            return arr.Count > 0;
        }
        public int Count()
        {
            return arr.Count;
        }
        public T Peek()
        {
            return arr[0];
        }
        private void MoveDown(int idx)
        {
            int len = arr.Count;
            int lc = LC(idx);
            int rc = RC(idx);
            if (lc >= len)
            {
                return;
            }
            else if (rc >= len)
            {
                if (compare(arr[idx],arr[lc])<0)
                {
                    T tmp = arr[idx];
                    arr[idx] = arr[lc];
                    arr[lc] = tmp;
                    MoveDown(lc);
                }
            }
            else
            {
                int maxChild = compare(arr[lc], arr[rc]) < 0 ? rc : lc;
                if (compare(arr[idx], arr[maxChild]) < 0)
                {
                    T tmp = arr[idx];
                    arr[idx] = arr[maxChild];
                    arr[maxChild] = tmp;
                    MoveDown(maxChild);
                }
            }
        }
        private void MoveUp(int idx)
        {
            if (idx == 0) return;
            else
            {
                int parent = Parent(idx);
                if (compare(arr[parent], arr[idx]) < 0)
                {
                    T tmp = arr[parent];
                    arr[parent] = arr[idx];
                    arr[idx] = tmp;
                    MoveUp(parent);
                }
                
            }
        }
        private int LC(int idx)
        {
            return idx * 2 + 1;
        }
        private int RC(int idx)
        {
            return idx * 2 + 2;
        }
        private int Parent(int idx)
        {
            return (idx - 1) / 2;
        }

    }
}