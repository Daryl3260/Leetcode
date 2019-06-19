namespace Leetcode.learn
{
    namespace AddisonWesley.Michaelis.EssentialCSharp.Chapter18.Listing18_15
{
    using System;
    using System.IO;
    using System.Net;
    using System.Linq;
    using System.Threading.Tasks;

    public class TestClass18_15
    {
        private static async Task WriteWebRequestSizeAsync(
            string url)//因为用async标记了，整个方法会被编译器重写，如果设置的返回值为Task类型，编译器包装返回一个task
        {
            try
            {
                WebRequest webRequest =
                    WebRequest.Create(url);

                WebResponse response =
                    await webRequest.GetResponseAsync();//创建一个新的线程来执行，任务，然后主线程控制流返回
                using(StreamReader reader =
                    new StreamReader(
                        response.GetResponseStream()))//上一个await的线程继续执行到这里
                {
                    string text =
                        await reader.ReadToEndAsync();//新的await，开启新的线程
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

//            return new Task(()=>{});async返回语句由编译器完成，不能自己写
        }

        public static void Test(string[] args)
        {
            string url = "http://www.163.com";
            if(args.Length > 0)
            {
                url = args[0];
            }

            Console.Write(url);

            Task task = WriteWebRequestSizeAsync(url);
            Console.Write(" ");
            while(!task.Wait(100))
            {
                Console.Write(".");
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