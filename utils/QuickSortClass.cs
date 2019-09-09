using System;

namespace Leetcode.utils
{
    public static class QuickSortClass
    {
        public static void Test()
        {
            var arr = new[] {1,3,5,7,9,8,6,2,1,3,5,2};
            QuickSort(arr);
            foreach (var num in arr)
            {
                Console.Write(num+"\t");
            }
        }

        public static void QuickSort(int[] arr)
        {
            if (arr == null) return;
            SubSort(arr,0,arr.Length-1);
        }

        public static void SubSort(int[] arr, int left, int right)
        {
            if (left >= right) return;
            var mid = Partition(arr, left, right);
            SubSort(arr,left,mid-1);
            SubSort(arr,mid+1,right);
        }

        public static int Partition(int[] arr, int left, int right)
        {
            var pivot = arr[left];
            while (left < right)
            {
                while (left < right && arr[right] >= pivot) right--;
                arr[left] = arr[right];
                while (left < right && arr[left] <= pivot) left++;
                arr[right] = arr[left];
            }

            arr[left] = pivot;
            return left;
        }
        
    }
}