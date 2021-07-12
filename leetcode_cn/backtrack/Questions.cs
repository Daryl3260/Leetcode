using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.leetcode_cn.backtrack
{
    namespace p1
    {
        public class Solution
        {
            public IList<string> GenerateParenthesis(int n)
            {
                if (n == 0) return new List<string>();
                var rs = new List<string>();
                var builder = new StringBuilder();
                OneCheck(rs, n, 0, 0, builder);
                return rs;
            }

            public void OneCheck(List<string> rs, int n, int left, int right, StringBuilder builder)
            {
                if (left == n && right == n)
                {
                    rs.Add(builder.ToString());
                    return;
                }

                var len = builder.Length;
                var extraLeft = left - right;
                var ll = n - left;

                if (extraLeft > 0)
                {
                    builder.Append(')');
                    OneCheck(rs, n, left, right + 1, builder);
                    builder.Remove(len, 1);
                }

                if (ll > 0)
                {
                    builder.Append('(');
                    OneCheck(rs, n, left + 1, right, builder);
                    builder.Remove(len, 1);
                }
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public IList<IList<int>> Permute(int[] nums)
            {
                if (nums == null || nums.Length == 0) return new List<IList<int>>();
                var currentList = new List<int>();
                var rs = new List<IList<int>>();
                var taken = new bool[nums.Length];
                OneCheck(currentList, nums, taken, rs);
                return rs;
            }

            // from idx to the end
            public void OneCheck(List<int> currentList, int[] nums, bool[] taken, List<IList<int>> rs)
            {
                if (currentList.Count == nums.Length)
                {
                    var list = new List<int>(currentList);
                    rs.Add(list);
                    return;
                }

                for (var i = 0; i < nums.Length; i++)
                {
                    if (taken[i]) continue;
                    taken[i] = true;
                    currentList.Add(nums[i]);
                    OneCheck(currentList, nums, taken, rs);
                    currentList.RemoveAt(currentList.Count - 1);
                    taken[i] = false;
                }
            }
        }
    }
}
