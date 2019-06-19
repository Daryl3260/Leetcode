using System;

namespace Leetcode.learn
{
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