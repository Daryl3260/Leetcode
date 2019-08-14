﻿using System;
using System.Collections.Generic;
using Leetcode.leetcode.facebook.SearchAndSort.p6;
using LearnPT = Leetcode.learn.LearnPartial;

class WinForms{}

namespace Namespace1
{
    class WinForms{}
}

namespace Namespace2
{
    class WinForms
    {
    }
}

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
            jzoffer.Chapter2.p3.Solution.Test();
        }
    }
}
