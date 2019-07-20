using System;

namespace Leetcode.learn.LearnPartial
{
    public partial class Example<TFirst,TSecond> : IEquatable<string> where TFirst:class
    {
        public bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Example<TFirst, TSecond>) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    public partial class PartialMethodDemo
    {
        public PartialMethodDemo()
        {
            OnConstructorStart();
            Console.WriteLine($"Generated Constructor");
            OnConstructorEnd();
        }

        partial void OnConstructorStart();
        partial void OnConstructorEnd();
    }
}