namespace Leetcode.leetcode_cn
{
    public static class Sorting
    {
        public static void Quicksort(int[] nums)
        {
            if (nums == null || nums.Length < 2) return;
            Subsort(nums,0,nums.Length-1);
        }

        private static void Subsort(int[] nums, int left, int right)
        {
            if (left >= right) return;
            var mid = Partition(nums, left, right);
            Subsort(nums,left,mid-1);
            Subsort(nums,mid+1,right);
        }

        private static int Partition(int[] nums, int left, int right)
        {
            var pivot = nums[left];
            while (left<right)
            {
                while (left < right && nums[right] >= pivot) right--;
                nums[left] = nums[right];
                while (left < right && nums[left] <= pivot) left++;
                nums[right] = nums[left];
            }
            nums[left] = pivot;
            return left;
        }
    }
}