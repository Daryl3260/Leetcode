using System;

namespace Leetcode.learn.learnDelegate
{
    public delegate void MethodInvoker();

    public class LearnDelegate
    {
        public void TestMixedCapture()
        {
            MethodInvoker[] methodInvokers = new MethodInvoker[2];
            int outside = 0;
            for (int i = 0; i < methodInvokers.Length; i++)
            {
                int inside = 0;
                methodInvokers[i] = delegate
                {
                    Console.WriteLine($"{outside}, {inside}");
                    outside++;
                    inside++;
                };
            }

            var first = methodInvokers[0];
            var second = methodInvokers[1];
            first();
            first();
            first();
            second();
            second();
        }
    }
}