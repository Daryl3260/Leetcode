using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Leetcode.learn
{
    public class RunningASeparateThread
    {
        public const int Repetitions = 1000;

        public static void Test()
        {
            ThreadStart start = DoWork;
            Thread thread = new Thread(start);
            thread.Start();
//            thread.Join();
            for (int i = 0; i < Repetitions; i++) Console.Write("-");
            thread.Join();
        }

        public static void DoWork()
        {
//            Console.WriteLine(Thread.CurrentThread.Name);
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("+");
            }
        }

        public static void TestWithPool()
        {
            ThreadPool.QueueUserWorkItem(DoWork, "+");
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("-");
            }
            Thread.Sleep(1000);
        }

        public static void DoWork(object state)
        {
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write(state);
            }
        }

        public static void TestWithTask()
        {
//            Task task = Task.Run(() =>
//            {
//                for (int i = 0; i < Repetitions; i++)
//                {
//                    Console.Write("+");
//                }
//            });
            Task task = new Task(DoWork);
            task.Start();
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("-");
            }

            task.Wait();
        }

        public static void TestWithFunc()
        {
            Task<string> task = new Task<string>(() =>
            {
                for (int i = 0; i < Repetitions; i++)
                {
                    Console.Write("+");
                }

                return (Task.CurrentId ?? 0 )+ "";
            });
            task.Start();
            Console.Write(Task.CurrentId ?? 2);
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("-");
            }

//            task.Wait();
            Console.Write(task.Result);//自动阻塞当前线程
        }

        public static void TestWithContinue()
        {
            Console.Write("Before");
            Task taskA = Task.Run(() => { Console.WriteLine("Starting"); })
                .ContinueWith(antecedent => Console.WriteLine("Continue with A"));
            Task taskB = taskA.ContinueWith(antecedent =>
            {
                Console.WriteLine(antecedent.GetType().Name);
                Console.WriteLine("Continue with B");
            },TaskContinuationOptions.OnlyOnFaulted);
            Task taskC = taskA.ContinueWith(antecedent => Console.WriteLine("Continue with C"),
                TaskContinuationOptions.OnlyOnCanceled);
            Task taskD = taskA.ContinueWith(antecedent => Console.WriteLine("Continue with D"),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            Task taskE = taskA.ContinueWith(antecedent => Console.WriteLine("Continue with E"));
//            try
//            {
//                taskC.Wait();
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//            }
            Task.WaitAny(taskB, taskC,taskD,taskE);
            Console.WriteLine("Finished");
        }

        public static void TestWithException()
        {
            Task task = Task.Run(() =>
            {
                throw new InvalidOperationException();
            });
            try
            {
                try
                {
                    task.Wait();
                }
                catch (AggregateException exception)
                {
                    exception.Handle(eachExcpetion =>
                    {
                        Console.WriteLine($"ERROR: {eachExcpetion.Message}");
                        return true;
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public static void TestWithContinuedException()
        {
            bool parentTaskFaulted = false;
            Task task = Task.Run(() => { throw new InvalidOperationException(); })
                .ContinueWith(
                    lastTask =>
                    {
                        
                        Console.WriteLine(lastTask.IsFaulted);
                        parentTaskFaulted = lastTask.IsFaulted; 
                        lastTask.Exception.Handle(exception =>
                        {
                            Console.WriteLine(exception.Message);
                            return true;
                        });
                    });
            task.Wait();
//            Task task2 = Task.Run(() => { throw new InvalidOperationException(); });
//            
//            Task task3 = task2.ContinueWith((lastTask => parentTaskFaulted = lastTask.IsFaulted));
//            Task.WaitAll(task, task3);
//            Console.WriteLine($"task.IsFaulted = {task.IsFaulted}");
//            Console.WriteLine($"task2.IsFaulted = {task2.IsFaulted}");
//            Task continuedTask = task.ContinueWith(task1 => { parentTaskFaulted = task1.IsFaulted; },
//                TaskContinuationOptions.OnlyOnFaulted);
//////            task.Start();
//            continuedTask.Wait();
            Trace.Assert(parentTaskFaulted);//类似于c++的assert语句
//            Trace.Assert(task.IsFaulted);
//            task.Exception.Handle(eachException =>
//            {
//                Console.WriteLine($"ERROR:{eachException.Message}");
//                return true;
//            });
        }
        public static Stopwatch clock = new Stopwatch();
        public static void TestWithUnhandledException()
        {
            try
            {
                clock.Start();
                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    Message(sender.ToString());
                    Message("Event handler Starting");
                    Delay(4000);
                };

                Thread thread = new Thread((() =>
                {
                    Message("Throwing exception");
                    throw new Exception();
                }));
                thread.Start();
//                Task taskA = Task.Run(() => { throw new Exception(); });
                Delay(8000);
//                taskA.Wait();

            }

            finally
            {
                Message("Finally block running");
            }
        }

        private static void Delay(int i)
        {
            Message($"Sleeping for {i} ms");
            Thread.Sleep(i);
            Message("Awake");
        }

        private static void Message(string text)
        {
            Console.WriteLine("{0}:{1:0000}:{2}",Thread.CurrentThread.ManagedThreadId,clock.ElapsedMilliseconds,text);
        }

        public static void TestWithCancellationToken()
        {
            string stars = "*".PadRight(Console.WindowWidth - 1, '*');
            Console.WriteLine("Push Enter to exit");
            CancellationTokenSource tokenSource = new CancellationTokenSource();
//            Task task = Task.Run((() => { WritePi(tokenSource.Token);}));
            Task task = Task.Factory.StartNew((() => { WritePi(tokenSource.Token); }),TaskCreationOptions.LongRunning);
//            task.Start();
            var task2 = Task.Run((() => { WritePi(tokenSource.Token);}));
            Console.ReadLine();
            tokenSource.Cancel();
            Console.WriteLine(stars);
            task.Wait();
            Console.WriteLine();
            task.Dispose();
        }

        private static void WritePi(CancellationToken token)
        {
            int i = 1;
            token.Register((() => { Console.WriteLine("Token Cancelled");}));
            
            while (!token.IsCancellationRequested || i == 0x0000ffff)
            {
                Console.Write(ToBinaryString(i++));
            }
        }

        public static string ToBinaryString(int a)
        {
            LinkedList<char> list = new LinkedList<char>();
            while (a > 0)
            {
                var left = (a & 0x1) == 1 ? '1' : '0';
                list.AddFirst(left);
                a >>= 1;
            }
            StringBuilder builder = new StringBuilder();
            foreach (var digit in list)
            {
                builder.Append(digit);
            }

            return builder.ToString();
        }
    }
    namespace AddisonWesley.Michaelis.EssentialCSharp.Chapter18.Listing18_13
    {
        using System;
        using System.IO;
        using System.Net;
        using System.Linq;

        public class TestClass
        {
            public static void Test(string[] args)
            {
                string url = "http://www.IntelliTect.com";
                if(args.Length > 0)
                {
                    url = args[0];
                }

                try
                {
                    Console.Write(url);
                    WebRequest webRequest =
                        WebRequest.Create(url);

                    WebResponse response =
                        webRequest.GetResponse();//耗时

                    Console.Write(".....");

                    using(StreamReader reader =
                        new StreamReader(
                            response.GetResponseStream()))
                    {
                        string text =
                            reader.ReadToEnd();
                        Console.WriteLine(
                            FormatBytes(text.Length));
                    }
                }
                catch(WebException)
                {
                    // ...
                }
                catch(IOException)
                {
                    // ...
                }
                catch(NotSupportedException)
                {
                    // ...
                }
            }

            static public string FormatBytes(long bytes)
            {
                string[] magnitudes =
                    new string[] { "GB", "MB", "KB", "Bytes" };
                long max =
                    (long)Math.Pow(1024, magnitudes.Length);

                return string.Format("{1:##.##} {0}",
                    magnitudes.FirstOrDefault(
                        magnitude =>
                            bytes > (max /= 1024)) ?? "0 Bytes",
                    (decimal)bytes / (decimal)max).Trim();
            }
        }
    }
}