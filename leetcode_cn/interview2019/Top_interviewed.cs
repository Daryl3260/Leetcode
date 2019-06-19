

using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Leetcode.leetcode_cn
{
    namespace problem_string.p1
    {
        public class Solution {
            public void ReverseString(char[] s)
            {
                if (s == null || s.Length < 2) return;
                int i = 0;
                int j = s.Length - 1;
                while (i < j)
                {
                    char tmp = s[i];
                    s[i] = s[j];
                    s[j] = tmp;
                    i++;
                    j--;
                }
            }
        }
    }

    namespace problem_array.p1
    {
        public class Solution {
            public int MaxProduct(int[] nums)
            {
                if (nums == null || nums.Length < 1) return 0;
                else if (nums.Length==1)
                {
                    return nums[0];
                }
                int? posiMax = null;
                int? negaMax = null;
                int max = int.MinValue;
                foreach (int elem in nums)
                {
                    if (elem == 0)
                    {
                        max = Math.Max(0, max);
                        posiMax = null;
                        negaMax = null;
                    }
                    else if (posiMax == null && negaMax == null)
                    {
                        if (elem > 0)
                        {
                            posiMax = elem;
                            negaMax = null;
                            max = Math.Max(max, posiMax.Value);
                        }
                        else
                        {
                            posiMax = null;
                            negaMax = elem;
                            max = Math.Max(max, negaMax.Value);
                        }
                    }
                    else
                    {
                        if (elem > 0)
                        {
                            if (posiMax != null)
                            {
                                posiMax *= elem;
                            }
                            else
                            {
                                posiMax = elem;
                            }
                            max = Math.Max(max, posiMax.Value);
                            if (negaMax != null)
                            {
                                negaMax *= elem;
                            }
                            else
                            {
                                negaMax = null;
                            }
                            if(negaMax!=null) max = Math.Max(max, negaMax.Value);
                        }
                        else//elem < 0
                        {
                            var oldPosi = posiMax;
                            var oldNega = negaMax;
                            if (oldPosi != null)
                            {
                                negaMax = oldPosi * elem;
                            }
                            else
                            {
                                negaMax = elem;
                            }
                            max = Math.Max(max, negaMax.Value);
                            if (oldNega != null)
                            {
                                posiMax = oldNega * elem;
                            }
                            else
                            {
                                posiMax = null;
                            }

                            if (posiMax != null) max = Math.Max(max, posiMax.Value);
                        }
                    }
                }
                return max;
            }
        }    
    }

    namespace problem_array.p2
    {
        public class Solution {
            public int MajorityElement(int[] nums)
            {
                int val = nums[0];
                int n = 1;
                for (int i = 1; i < nums.Length; i++)
                {
                    int elem = nums[i];
                    if (n == 0)
                    {
                        val = elem;
                        n = 1;
                    }
                    else
                    {
                        if (val == elem)
                        {
                            n++;
                        }
                        else
                        {
                            n--;
                        }
                    }
                }

                return val;
            }
        }    
    }

    namespace problem_array.p3
    {
        public class Solution {
            public void Rotate(int[] nums, int k)
            {
                k %= nums.Length;
                if (k==0) return;
                int len = nums.Length;
                LocalInvert(nums,0,len-k-1);
                LocalInvert(nums,len-k,len-1);
                LocalInvert(nums,0,len-1);
            }

            private void LocalInvert(int[] nums, int i, int j)
            {
                while (i < j)
                {
                    int tmp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = tmp;
                    i++;
                    j--;
                }
            }
        }
    }

    namespace problem_array.p6
    {
        public class Solution
        {
            private int[] _arr;
            private Random _random;
            public Solution(int[] nums)
            {
                _random = new Random();
                if (nums == null)
                {
                    _arr = null;
                }
                else
                {
                    _arr = new int[nums.Length];
                    for (int i = 0; i < nums.Length; i++)
                    {
                        _arr[i] = nums[i];
                    }
                }
            }
    
            /** Resets the array to its original configuration and return it. */
            public int[] Reset()
            {
                return copy();
            }
    
            /** Returns a random shuffling of the array. */
            public int[] Shuffle()
            {
                int[] arr = copy();
                if (arr == null || arr.Length == 1) return arr;
                for (int i = 0; i < arr.Length-1; i++)
                {
                    int j = _random.Next(i, arr.Length);
                    int tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                }
                return arr;
            }

            private int[] copy()
            {
                if (_arr == null) return null;
                int[] rs = new int[_arr.Length];
                for (int i = 0; i < _arr.Length; i++)
                {
                    rs[i] = _arr[i];
                }

                return rs;
            }
        }

/**
 * Your Solution object will be instantiated and called as such:
 * Solution obj = new Solution(nums);
 * int[] param_1 = obj.Reset();
 * int[] param_2 = obj.Shuffle();
 */    
    }

    namespace problem_array.p7
    {
        public class Solution 
        {
            public bool IncreasingTriplet(int[] nums)
            {
                if (nums == null || nums.Length < 3) return false;
                int len = nums.Length;
                int[] minLeft = new int[len];
                int[] maxRight = new int[len];
                minLeft[1] = nums[0];
                maxRight[len - 2] = nums[len - 1];
                for (int i = 2; i < len; i++)
                {
                    minLeft[i] = Math.Min(nums[i - 1], minLeft[i - 1]);
                }

                for (int i = len - 3; i > -1; i--)
                {
                    maxRight[i] = Math.Max(nums[i + 1], maxRight[i + 1]);
                }

                for (int i = 1; i < len - 1; i++)
                {
                    int left = minLeft[i];
                    int right = maxRight[i];
                    if (left < nums[i] && nums[i] < right)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }

    namespace problem_array.p8
    {
        public class Solution {
            public bool SearchMatrix(int[,] matrix, int target)
            {
                if (matrix == null || matrix.Length < 1) return false;
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                int i = 0;
                int j = cols - 1;
                while (true)
                {
                    if (i < 0 || j < 0 || i == rows || j == cols) return false;
                    int val = matrix[i, j];
                    if (val == target) return true;
                    else if (val < target)
                    {
                        i++;
                    }
                    else
                    {
                        j--;
                    }
                }
            }
        }
    }

    namespace problem_array.p9
    {
        public class Solution {
            public int[] ProductExceptSelf(int[] nums)
            {
                if (nums == null||nums.Length==0) return nums;
                else if(nums.Length==1)return new int[]{1};
                int len = nums.Length;
                int[] ascending = new int[len];
                int[] descending = new int[len];
                int[] rs = new int[len];
                ascending[0] = 1;
                descending[len - 1] = 1;
                for(int i =1;i<len;i++)
                {
                    ascending[i] = ascending[i - 1] * nums[i-1];
                    descending[len - i - 1] = descending[len - i] * nums[len - i];
                }

                for (int i = 0; i < len; i++)
                {
                    rs[i] = ascending[i] * descending[i];
                }

                return rs;
            }
        }
    }
}