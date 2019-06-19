using System.Collections.Generic;

namespace Leetcode.leetcode_cn.graph_prob
{
    namespace p1
    {
        public class Solution {
            public int LadderLength(string beginWord, string endWord, IList<string> wordList)
            {
                if (!wordList.Contains(endWord)) return 0;
                //else if (IsNeighbor(beginWord, endWord)) return 1;
                HashSet<string> candidate = new HashSet<string>(wordList);
                candidate.Remove(beginWord);
                candidate.Remove(endWord);
                Queue<Wrapper> queue = new Queue<Wrapper>();
                queue.Enqueue(new Wrapper() {Val = beginWord, distance = 1});
                while (true)
                {
                    if (queue.Count == 0) return 0;
                    var first = queue.Dequeue();
                    if (IsNeighbor(first.Val, endWord))
                    {
                        return first.distance + 1;
                    }
                    List<string> nextWords = new List<string>();
                    foreach (var word in candidate)
                    {
                        if (IsNeighbor(first.Val, word))
                        {
                            nextWords.Add(word);
                        }
                    }

                    foreach (var word in nextWords)
                    {
                        queue.Enqueue(new Wrapper(){Val = word,distance = first.distance+1});
                        candidate.Remove(word);
                    }
                }
            }

            struct Wrapper
            {
                public string Val { get; set; }
                public int distance { get; set; }
            }
            private bool IsNeighbor(string left, string right)
            {
                int count = 0;
                int len = left.Length;
                for (int i = 0; i < len; i++)
                {
                    if (left[i] != right[i])
                    {
                        if (count == 0) count++;
                        else return false;
                    }
                }
                return true;
            }
        }
    }
}