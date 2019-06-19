using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leetcode.learn
{
    public class LearnAsyncAwait
    {
        public static async Task AsyncTest()
        {
            await Test();
        }

        private static Task Test()
        {
            return Task.Run((() =>
            {
                int? nullable = 5;
                object boxed = nullable;
                Console.WriteLine(boxed.GetType());//box the wrapped value

                int normal = (int) boxed;
                nullable = (int?) boxed;
                Console.WriteLine(normal);
                Console.WriteLine(nullable.Value);
                Console.WriteLine(nullable);
                nullable = new int?();
                boxed = nullable;
                Console.WriteLine(boxed == null);
                nullable = (int?) boxed;
                Console.WriteLine(nullable.HasValue);
                try
                {
                    int unwrapped = (int) boxed;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }));
        }

        private static void Display(int? x)
        {
            Console.WriteLine($"HasValue:{x.HasValue}");
            if (x.HasValue)
            {
                Console.WriteLine($"Value:{x.Value}");
                Console.WriteLine($"Explicit conversion:{(int)x}");
                Console.WriteLine($"Conversion by Value:{x.Value}");
            }
            Console.WriteLine($"GetValueOrDefault():{x.GetValueOrDefault()}");
            Console.WriteLine($"GetValueOrDefault(10):{x.GetValueOrDefault(10)}");
            Console.WriteLine($"ToString():\"{x.ToString()}\"");
            Console.WriteLine($"GetHashCode():{x.GetHashCode()}");
            Console.WriteLine();
        }
    }
}