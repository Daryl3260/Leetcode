using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leetcode.learn
{
    public class Synchronization
    {
        private const int _Total = 0xffff;
        private static long _Count = 0;

        public static void MainTest()
        {
            Task task = Task.Run((() => {Decrement();}));
            for (int i = 0; i < _Total; i++)
            {
                _Count++;
            }

            task.Wait();
            Console.WriteLine($"{_Count}");
        }

        static void Decrement()
        {
            for (int i = 0; i < _Total; i++)
            {
                _Count--;
            }
        }
    }

    namespace p1
    {
        public class Synchronization
        {
            readonly static object _Sync = new object();
            private const int _Total = 0xffff;
            private static long _Count = 0;

            public static void MainTest()
            {
                Task task = Task.Run((() =>
                {
                    Decrement();
                }));
                for (int i = 0; i < _Total; i++)
                {
                    bool lockTaken = false;
                    try
                    {
                        Monitor.Enter(_Sync, ref lockTaken);
                        _Count++;
                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            Monitor.Exit(_Sync);
                        }
                    }
                }
                task.Wait();
                Console.WriteLine($"{_Count}");
            }

            static void Decrement()
            {
                for (int i = 0; i < _Total; i++)
                {
                    bool lockTaken = false;
                    try
                    {
                        Monitor.Enter(_Sync, ref lockTaken);
                        _Count--;
                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            Monitor.Exit(_Sync);
                        }
                    }
                }
            }
        }
}

    namespace p2
    {
        public class Synchronization
        {
            private static readonly object _Sync = new object();
            private static int _Count = 0;
            private const int _Total = 0xffff;

            public static void MainTest()
            {
                var t = Task.Run((Action) Decrement);
                for (int i = 0; i < _Total; i++)
                {
                    lock (_Sync)
                    {
                        _Count++;
                    }
                }

                t.Wait();
                Console.WriteLine($"{_Count}");
            }

            private static void Decrement()
            {
                for (int i = 0; i < _Total; i++)
                {
                    lock (_Sync)
                    {
                        _Count--;
                    }
                }
            }
        }   
    }
}