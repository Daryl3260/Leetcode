using System;

namespace Leetcode.learn.LearnPartial
{
    public partial class Example<TFirst, TSecond>:EventArgs,IDisposable
    {
        public void Dispose()
        {
            
        }
    }

    public partial class PartialMethodDemo
    {
        partial void OnConstructorStart()
        {
            Console.WriteLine($"OnConstructorStart");
        }

        partial void OnConstructorEnd()
        {
            Console.WriteLine("OnConstructorEnd");
        }
    }
}