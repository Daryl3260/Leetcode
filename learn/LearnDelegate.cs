using System;

namespace Leetcode.learn.learnDelegate
{
    public class BaseClass
    {
        public void Method(int? val)
        {
            Console.WriteLine($"BaseClass.Method:{val}");
        }
    }

    public class DerivedClass : BaseClass
    {
        public void Method(object val)
        {
            Console.WriteLine($"DerivedClass.Method:{val}");
        }

        public static void Test()
        {
            var baseInstance = new BaseClass();
            var derivedInstance = new DerivedClass();
            int? val = null;
            baseInstance.Method(val);
            derivedInstance.Method(val);
        }
    }
}