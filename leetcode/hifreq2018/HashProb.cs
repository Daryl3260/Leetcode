using System;
using System.Collections.Generic;

namespace Leetcode.hifreq2018.HashProb
{
    namespace p1
    {
        public class Solution {
            public int TitleToNumber(string s)
            {
                char[] arr = s.ToCharArray();
                char zero = (char)('A' - 1);
                int sum = 0;
                int timer = 1;
                const int exp = 26;
                for (int i = arr.Length - 1; i > -1; i--)
                {
                    sum += timer * (arr[i]-zero);
                    timer *= exp;
                }

                return sum;
            }
        }    
    }

    namespace p2
    {
        public class Solution {
            public int FourSumCount(int[] A, int[] B, int[] C, int[] D)
            {
                int rs = 0;
                var dict1 = ConstructDict(A, B);
                var dict2 = ConstructDict(C, D);
                foreach (var left in dict1.Keys)
                {
                    if (dict2.ContainsKey(-left))
                    {
                        rs += dict1[left] * dict2[-left];
                    }
                }

                return rs;
            }

            private Dictionary<int, int> ConstructDict(int[] A, int[] B)
            {
                Dictionary<int,int> rs = new Dictionary<int, int>();
                int len = A.Length;
                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < len; j++)
                    {
                        int sum = A[i] + B[j];
                        if (rs.ContainsKey(sum))
                        {
                            rs[sum]++;
                        }
                        else
                        {
                            rs[sum] = 1;
                        }
                    }
                }

                return rs;
            }
        }
    }

    namespace p3
    {
        public class RandomizedSet
        {
            private Random _random;
            private HashSet<int> _set;
            
            /** Initialize your data structure here. */
            public RandomizedSet() {
                _random = new Random();
                _set = new HashSet<int>();
            }
    
            /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
            public bool Insert(int val)
            {
                return _set.Add(val);
            }
    
            /** Removes a value from the set. Returns true if the set contained the specified element. */
            public bool Remove(int val)
            {
                return _set.Remove(val);
            }
    
            /** Get a random element from the set. */
            public int GetRandom()
            {
                int len = _set.Count;
                int idx = _random.Next(0, len);
                var iter = _set.GetEnumerator();
                for (int i = 0; i <= idx; i++)
                {
                    iter.MoveNext();
                }

                int rs = iter.Current;
                iter.Dispose();
                return rs;
            }
        }

/**
 * Your RandomizedSet object will be instantiated and called as such:
 * RandomizedSet obj = new RandomizedSet();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */
}
    //index reverted to value in dictionary
    namespace p3.better
    {
        public class RandomizedSet
        {
            private List<int> _list;

            private Dictionary<int, int> _dict;

            private Random _random;
            /** Initialize your data structure here. */
            public RandomizedSet() {
                _list = new List<int>();
                _dict = new Dictionary<int, int>();
                _random = new Random();
            }
    
            /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
            public bool Insert(int val)
            {
                if (_dict.ContainsKey(val)) return false;
                else
                {
                    int idx = _dict.Count;
                    _dict[val] = idx;
                    _list.Add(val);
                    return true;
                }
            }
    
            /** Removes a value from the set. Returns true if the set contained the specified element. */
            public bool Remove(int val)
            {
                if (!_dict.ContainsKey(val)) return false;
                else
                {

                    int idx = _dict[val];
                    int len = _list.Count;
                    int lastVal = _list[len - 1];
                    if (lastVal == val)
                    {
                        _list.RemoveAt(len-1);
                        _dict.Remove(val);
                    }
                    else
                    {
                        _dict.Remove(val);
                        _dict[lastVal] = idx;

                        _list[idx] = lastVal;
                        _list.RemoveAt(len-1);
                    }
                    return true;
                }
            }
    
            /** Get a random element from the set. */
            public int GetRandom()
            {
                return _list[_random.Next(0, _list.Count)];
            }
        }
    }

    namespace p3.p381
    {
        public class RandomizedCollection
        {
            private static int sId = 1;

            struct IntWrapper:IComparable<IntWrapper>
            {
                public static bool IsInsertion = false;
                public int Val { get; set; }
                public int Id { get; set; }

                public IntWrapper(int val) : this()
                {
                    Val = val;
                    Id = sId++;
                }

                public int CompareTo(IntWrapper other)
                {
                    if (IsInsertion)
                    {
                        if (Val != other.Val)
                        {
                            return Val - other.Val;
                        }
                        else
                        {
                            return Id - other.Id;
                        }
                    }
                    else
                    {
                        return Val - other.Val;
                    }
                }
            }

            private List<IntWrapper> _list;

            private SortedDictionary<IntWrapper, int> _dict;

            private Random _random;
            /** Initialize your data structure here. */
            public RandomizedCollection() {
                _list = new List<IntWrapper>();
                _dict = new SortedDictionary<IntWrapper, int>();
                _random = new Random();
            }
    
            /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element. */
            public bool Insert(int val)
            {
                var wrapper = new IntWrapper(val);
                IntWrapper.IsInsertion = false;
                bool rs = !_dict.ContainsKey(wrapper);

                IntWrapper.IsInsertion = true;
                int len = _list.Count;
                _list.Add(wrapper);
                _dict[wrapper] = len;
                return rs;
            }
    
            /** Removes a value from the collection. Returns true if the collection contained the specified element. */
            public bool Remove(int val) {
                var wrapper = new IntWrapper(val);
                IntWrapper.IsInsertion = false;
                if (_dict.ContainsKey(wrapper))
                {
                    int idx = _dict[wrapper];
                    if (idx == _list.Count - 1)
                    {
                        _dict.Remove(wrapper);
                        _list.RemoveAt(_list.Count-1);
                    }
                    else
                    {
                        var lastWrapper = _list[_list.Count - 1];
                        _dict.Remove(wrapper);
                        IntWrapper.IsInsertion = true;
                        _dict[lastWrapper] = idx;
                        _list[idx] = lastWrapper;
                        _list.RemoveAt(_list.Count-1);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
    
            /** Get a random element from the collection. */
            public int GetRandom()
            {
                return _list[_random.Next(0, _list.Count)].Val;
            }
        }

/**
 * Your RandomizedCollection object will be instantiated and called as such:
 * RandomizedCollection obj = new RandomizedCollection();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */
}

    namespace p3.p381.better
    {
        public class RandomizedCollection
        {
            

            struct IntWrapper
            {
                private static int sId = 1;
                public static bool IsInsertion = false;
                public int Val { get;  }
                public int Id { get; }

                public IntWrapper(int val)
                {
                    Val = val;
                    Id = sId++;
                }

                public override bool Equals(object obj)
                {
                    if (IsInsertion)
                    {
                        return this.Val == ((IntWrapper)obj).Val && this.Id == ((IntWrapper)obj).Id;
                    }
                    else
                    {
                        return this.Val == ((IntWrapper) obj).Val;
                    }
                }

                public override int GetHashCode()
                {
                    return Val.GetHashCode();
                }
            }

            private List<IntWrapper> _list;

            private Dictionary<IntWrapper, int> _dict;

            private Random _random;
            /** Initialize your data structure here. */
            public RandomizedCollection() {
                _list = new List<IntWrapper>();
                _dict = new Dictionary<IntWrapper, int>();
                _random = new Random();
            }
    
            /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element. */
            public bool Insert(int val)
            {
                var wrapper = new IntWrapper(val);
                IntWrapper.IsInsertion = false;
                bool rs = !_dict.ContainsKey(wrapper);

                IntWrapper.IsInsertion = true;
                int len = _list.Count;
                _list.Add(wrapper);
                _dict[wrapper] = len;
                return rs;
            }
    
            /** Removes a value from the collection. Returns true if the collection contained the specified element. */
            public bool Remove(int val) {
                var wrapper = new IntWrapper(val);
                IntWrapper.IsInsertion = false;
                if (_dict.ContainsKey(wrapper))
                {
                    int idx = _dict[wrapper];
                    if (idx == _list.Count - 1)
                    {
                        _dict.Remove(wrapper);
                        _list.RemoveAt(_list.Count-1);
                    }
                    else
                    {
                        var lastWrapper = _list[_list.Count - 1];
                        _dict.Remove(wrapper);
                        IntWrapper.IsInsertion = true;
                        _dict[lastWrapper] = idx;
                        _list[idx] = lastWrapper;
                        _list.RemoveAt(_list.Count-1);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
    
            /** Get a random element from the collection. */
            public int GetRandom()
            {
                return _list[_random.Next(0, _list.Count)].Val;
            }
        }

/**
 * Your RandomizedCollection object will be instantiated and called as such:
 * RandomizedCollection obj = new RandomizedCollection();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */
}
}