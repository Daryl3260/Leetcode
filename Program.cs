using System;
using System.Collections.Generic;
using Leetcode.leetcode.microsoft.SearchAndSort.p9;


namespace Leetcode
{
    class Program
    {
        class Comparator : IEqualityComparer<Wrapper>
        {
            private static int sId;

            static Comparator()
            {
                sId = 1;
            }
            public bool Equals(Wrapper x, Wrapper y)
            {
                Console.WriteLine("Equals");
                return true;
            }

            public int GetHashCode(Wrapper obj)
            {
                Console.WriteLine("HashCode");
                return ++sId;
            }
        }
        class Wrapper:IEquatable<Wrapper>
        {
            private static int sId;
            private int id=++sId;
            public bool Equals(Wrapper other)
            {
                return true;
            }

            public override bool Equals(object obj)
            {
                return true;
            }

            public override int GetHashCode()//先调用GetHashCode，如果相同再调用Equals
            {
                Console.WriteLine("GetHashCode called");
                return id;
            }
        }

        static void PrintValueAsInt32(object o)
        {
            
            int? val = o as int?;
            Console.WriteLine(val.HasValue?val.Value.ToString():"null");
        }
        static void Main(string[] args)
        {
//            [1]
//                [2,3,4,5,6,7,8,9,10]
//            [6]
//                [1,2,3,4,5,7,8,9,10]
            leetcode.microsoft.SearchAndSort.p9.Solution s = new Solution();
            var median = s.FindMedianSortedArrays(new[] {6}, new[] {1,2, 3, 4, 5, 7, 8, 9, 10});
            Console.WriteLine($"{median}");
        }
    }
}
