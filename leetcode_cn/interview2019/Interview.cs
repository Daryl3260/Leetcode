using System.Collections.Generic;
using System.Linq;

namespace Leetcode.leetcode_cn.interview2019.interview
{
    namespace p3
    {
        public class Solution {
            public int GetSum(int a, int b) {
                List<int> list = new List<int>();
                list.Add(a);
                list.Add(b);
                return list.Sum();
            }
        }
    }

    namespace p4
    {
        public class Solution
        {
            public IList<string> FizzBuzz(int n)
            {
                const string three = "Fizz";
                const string five = "Buzz";
                List<string> rs = new List<string>();
                for (int i = 1; i <= n; i++)
                {
                    if (i % 15 == 0)
                    {
                        rs.Add(three+five);
                    }
                    else if (i % 3 == 0)
                    {
                        rs.Add(three);
                    }
                    else if (i % 5 == 0)
                    {
                        rs.Add(five);
                    }
                    else
                    {
                        rs.Add(i.ToString());
                    }
                }

                return rs;
            }
        }
    }

    namespace p5
    {
        public class Solution
        {
            public int CanCompleteCircuit(int[] gas, int[] cost)
            {
                int allGas = gas.Sum();
                int allCost = cost.Sum();
                if (allGas < allCost) return -1;
                int total = 0;
                int len = gas.Length;
                int starter = 0;
                for (int i = 0; i < len; i++)
                {
                    total += gas[i];
                    int fee = cost[i];
                    if (total >= fee)
                    {
                        total -= fee;
                    }
                    else
                    {
                        total = 0;
                        starter = i + 1;
                    }
                }

                return starter;
            }
        }
    }

    namespace p6
    {
        public class LRUCache {
            class Node
            {
                public int Key { get; set; }
                public int Val { get; set; }
                public Node prev { get; set; }
                public Node next { get; set; }
            }

            private Node header;
            private Node trailer;
            private Dictionary<int, Node> dict;
            private int capacity;
            public LRUCache(int capacity) {
                header = new Node();
                trailer = new Node();
                header.next = trailer;
                trailer.prev = header;
                dict = new Dictionary<int, Node>();
                this.capacity = capacity;
            }
    
            public int Get(int key)
            {
                if (capacity <= 0) return -1;
                if (!dict.ContainsKey(key))
                {
                    return -1;
                }
                else
                {
                    var node = dict[key];
                    node.prev.next = node.next;
                    node.next.prev = node.prev;
                    node.next = header.next;
                    node.prev = header;
                    node.prev.next = node;
                    node.next.prev = node;
                    return node.Val;
                }
            }
    
            public void Put(int key, int value)
            {
                if (capacity <= 0) return;
                if (dict.ContainsKey(key))
                {
                    var node = dict[key];
                    node.prev.next = node.next;
                    node.next.prev = node.prev;
                    node.next = header.next;
                    node.prev = header;
                    node.next.prev = node;
                    node.prev.next = node;
                    node.Val = value;
                }
                else
                {
                    if (dict.Count == capacity)
                    {
                        var last = trailer.prev;
                        last.prev.next = last.next;
                        last.next.prev = last.prev;
                        dict.Remove(last.Key);
                    }

                    Node node = new Node {Key = key, Val = value, next = header.next, prev = header};
                    node.prev.next = node; 
                    node.next.prev = node;
                    dict[key] = node;
                }
            }
        }

/**
 * Your LRUCache object will be instantiated and called as such:
 * LRUCache obj = new LRUCache(capacity);
 * int param_1 = obj.Get(key);
 * obj.Put(key,value);
 */
    }
}