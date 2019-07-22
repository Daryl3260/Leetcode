using System;

namespace Leetcode.learn
{
    public struct Foo
    {
        public int Value { get; set; }

        public Foo(int value)
        {
            this.Value = value;
        }
    }
    public class TestCheck
    {
        public static int count;

        static TestCheck()
        {
            count = 10;
        }

        public TestCheck()
        {
            
        }
        public static void Test()
        {
            var foo =new Foo(10);
            Console.WriteLine($"{foo.Value}");
        }
    }
    class MyClass<T>
    {
        private T _val;
        public T Val
        {
            get { return _val; }
            set { _val = value; }
        }

        public void Process<TU>(T val1,TU val2)
        {
            
        }

        public T this[int index] => _val;
    }

    public class Outer<T>
    {
        public class Inner<U, V>
        {
            static Inner()
            {
                Console.WriteLine($"Outer<{typeof(T).Name}>.Inner<{typeof(U).Name},{typeof(V).Name}>");
            }
            public static void DummyMethod(){}
        }
    }
}