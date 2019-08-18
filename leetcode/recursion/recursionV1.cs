using System.Collections.Generic;

namespace Leetcode.leetcode.recursion
{
    namespace p1
    {
        public class Solution
        {
            public IList<int> GetRow(int rowIndex)
            {
                var rs = new List<int>();
                if (rowIndex == 0)
                {
                    rs.Add(1);
                    return rs;
                }
                rs.Add(1);
                rs.Add(1);
                var next = new List<int>();
                for (var i = 1; i < rowIndex; i++)
                {
                    next.Add(1);
                    for (var j = 0; j < rs.Count - 1; j++)
                    {
                        next.Add(rs[j]+rs[j+1]);
                    }
                    next.Add(1);
                    var temp = rs;
                    rs = next;
                    next = temp;
                    next.Clear();
                }

                return rs;
            }
        }
    }
}