using System;

namespace Leetcode.utils
{
    public static class InsertSortClass
    {
        public static void Test()
        {
            var arr = new[] {1, 3, 5, 7, 9, 8, 6, 4, 2, 3, 1, 6, 4, 3};
            InsertSort(arr);
            foreach (var num in arr)
            {
                Console.Write($"{num}\t");
            }
        }
        public static void InsertSort(int[] arr)
        {
            if (arr == null || arr.Length < 2) return;
            for (var i = 1; i < arr.Length; i++)
            {
                for (var j = i; j > 0; j--)
                {
                    if (arr[j] < arr[j - 1])
                    {
                        var temp = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = temp;
                    }
                    else break;
                }
            }
        }
    }
}