using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leetcode.leetcode.facebook.Design.p3;

namespace Leetcode.leetcode.trie
{
    public class Trie<T>
    {
        public class Node
        {
            public T Val { get; set; }
            public bool IsEnd { get; set; }
            private Dictionary<char, Node> _children = new Dictionary<char, Node>();

            public Node this[char key]
            {
                get => _children.ContainsKey(key) ? _children[key] : null;
                set => _children[key] = value;
            }
        }

        public Node _root;

        /** Initialize your data structure here. */
        public Trie()
        {
            _root = new Node();
        }

        public void Insert(string word, T val = default(T))
        {
            if (string.IsNullOrEmpty(word)) return;
            var idx = 0;
            var node = _root;
            while (true)
            {
                if (idx == word.Length)
                {
                    node.IsEnd = true;
                    break;
                }
                else
                {
                    var key = word[idx];
                    if (node[key] == null)
                    {
                        node[key] = new Node {Val = val};

                    }

                    node = node[key];
                    idx++;

                }
            }
        }
        /** Inserts a word into the trie. */

        public T Search(string word)
        {
            if (string.IsNullOrEmpty(word)) return default(T);
            var idx = 0;
            var node = _root;
            while (true)
            {
                if (idx == word.Length)
                {
                    return node.IsEnd ? node.Val : default(T);
                }

                if (node[word[idx]] == null) return default(T);
                node = node[word[idx++]];
            }
        }

        /** Returns if the word is in the trie. */



        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            if (string.IsNullOrEmpty(prefix)) return false;
            var idx = 0;
            var node = _root;
            while (true)
            {
                if (idx == prefix.Length) return true;
                if (node[prefix[idx]] == null) return false;
                node = node[prefix[idx++]];
            }
        }
    }

/**
 * Your Trie object will be instantiated and called as such:
 * Trie obj = new Trie();
 * obj.Insert(word);
 * bool param_2 = obj.Search(word);
 * bool param_3 = obj.StartsWith(prefix);
 */
    namespace p1
    {
        public class MapSum
        {
            public class Trie
            {
                public class Node
                {
                    public int? Val { get; set; }
                    public Node[] _children = new Node[26];

                    public Node this[char key]
                    {
                        get => _children[key - 'a'];
                        set => _children[key - 'a'] = value;
                    }
                }

                private Node _root = new Node();

                public void Insert(string key, int val)
                {
                    if (string.IsNullOrEmpty(key)) return;
                    var idx = 0;
                    var node = _root;
                    while (true)
                    {
                        if (idx == key.Length)
                        {
                            node.Val = val;
                            break;
                        }
                        else
                        {
                            if (node[key[idx]] == null)
                            {
                                node[key[idx]] = new Node();
                            }

                            node = node[key[idx++]];
                        }
                    }
                }

                public int Sum(string prefix)
                {
                    if (string.IsNullOrEmpty(prefix)) return 0;
                    var node = _root;
                    var idx = 0;
                    while (true)
                    {
                        if (idx == prefix.Length) break;
                        var key = prefix[idx];
                        if (node[key] == null) return 0;
                        else
                        {
                            node = node[key];
                            idx++;
                        }
                    }

                    var sum = 0;
                    SubSum(node, ref sum);
                    return sum;
                }


                private void SubSum(Node node, ref int sum)
                {
                    if (node.Val != null)
                    {
                        sum += node.Val.Value;
                    }

                    foreach (var child in node._children)
                    {
                        if (child != null) SubSum(child, ref sum);
                    }
                }
            }

            /** Initialize your data structure here. */
            private Trie _trie;

            public MapSum()
            {
                _trie = new Trie();
            }

            public void Insert(string key, int val)
            {
                _trie.Insert(key, val);
            }

            public int Sum(string prefix)
            {
                return _trie.Sum(prefix);
            }
        }
    }

    namespace p2
    {
        public class Solution
        {
            public class Trie
            {
                private class Node
                {
                    public bool IsEnd { get; set; }
                    private Node[] _children = new Node[26];
                    public int Length => _children.Length;

                    public Node this[int key]
                    {
                        get => _children[key];
                        set => _children[key] = value;
                    }
                }

                private Node _root;

                public Trie()
                {
                    _root = new Node();
                }

                public void Insert(string word)
                {
                    if (string.IsNullOrEmpty(word)) return;
                    var idx = 0;
                    var node = _root;
                    while (true)
                    {
                        if (idx == word.Length)
                        {
                            node.IsEnd = true;
                            break;
                        }

                        var key = word[idx] - 'a';
                        if (node[key] == null)
                        {
                            node[key] = new Node();
                        }

                        node = node[key];
                        idx++;
                    }
                }

                public string FindRoot(string word)
                {
//                    string rs = null;
                    if (string.IsNullOrEmpty(word)) return null;
                    var idx = 0;
                    var node = _root;
                    while (true)
                    {
                        if (node.IsEnd)
                        {
                            return word.Substring(0, idx);
                        }

                        if (idx == word.Length) return null;

                        var ch = word[idx];
                        if (node[ch - 'a'] == null) return null;
                        node = node[ch - 'a'];
                        idx++;
                    }
                }

                private static string ConstructWord(IEnumerable<char> list)
                {
                    var builder = new StringBuilder();
                    foreach (var ch in list)
                    {
                        builder.Append(ch);
                    }

                    return builder.ToString();
                }
            }

            public static void Test()
            {
                /*
                 ["a", "aa", "aaa", "aaaa"]
"a aa a aaaa aaa aaa aaa aaaaaa bbb baba ababa"
                 */
                var solution = new Solution();
                var dict = new List<string>(new[] {"a", "aa", "aaa", "aaaa"});
                var sentence = "a aa a aaaa aaa aaa aaa aaaaaa bbb baba ababa";
                var rs = solution.ReplaceWords(dict, sentence);
                Console.WriteLine($"{rs}");
            }

            public string ReplaceWords(IList<string> dict, string sentence)
            {
                if (string.IsNullOrEmpty(sentence) || dict == null || dict.Count == 0) return sentence;
                var trie = new Trie();
                foreach (var word in dict)
                {
                    trie.Insert(word);
                }

                var words = new HashSet<string>(sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                var replacement = new Dictionary<string, string>();
                foreach (var word in words)
                {
                    var rp = trie.FindRoot(word);
                    if (rp != null)
                    {
                        replacement[word] = rp;
                    }
                }

                sentence = sentence + " ";
                var back = 0;
                var front = 0;
                while (true)
                {
                    if (back >= sentence.Length) break;
                    front = sentence.IndexOf(' ', back);
                    var len = front - back;
                    var word = sentence.Substring(back, len);
                    if (replacement.ContainsKey(word))
                    {
                        var rp = replacement[word];
                        var diff = word.Length - rp.Length;
                        sentence = sentence.Substring(0, back) + rp + sentence.Substring(front);
                        back = front - diff + 1;
                    }
                    else back = front + 1;
                }

                return sentence.Substring(0, sentence.Length - 1);
            }
        }
    }

    namespace p3
    {
        
        public class AutocompleteSystem
        {
            public class SentenceComparator : Comparer<Tuple<string, int>>
            {
                public override int Compare(Tuple<string, int> x, Tuple<string, int> y)
                {
                    if (x.Item2 != y.Item2) return -(x.Item2 - y.Item2);
                    else
                    {
                        var rs = (x.Item1.CompareTo(y.Item1));
                        return rs;
                    }
                }
            }
            public class Node
            {
                public int Freq { get; set; }//0: not end
                public Dictionary<char,Node> _children = new Dictionary<char, Node>();

                public Node this[char key]
                {
                    get => _children.ContainsKey(key) ? _children[key] : null;
                    set => _children[key] = value;
                }
            }

            public static void Test()
            {
                /*
                 * ["AutocompleteSystem","input","input","input","input"]
[[["i love you","island","iroman","i love leetcode"],[5,3,2,2]],["i"],[" "],["l"],["#"]]
                 */
                var sentences = new[] {"i love you","island","iroman","i love leetcode" };
                var times = new[] {5,3,2,2 };
                var testArr = new[] {'i',' ','a','#'};
                var auto = new AutocompleteSystem(sentences, times);
                foreach (var ch in testArr)
                {
                    var rs = auto.Input(ch);
                    foreach (var str in rs)
                    {
                        Console.Write(str+"\t");
                    }

                    Console.WriteLine($"");
                }
            }
            private Node _root;
            public void Insert(string sentence, int time)
            {
                var idx = 0;
                var node = _root;
                if (string.IsNullOrEmpty(sentence)) return;
                while (true)
                {
                    if (idx == sentence.Length)
                    {
                        node.Freq = time;
                        break;
                    }

                    var key = sentence[idx];
                    if (node[key] == null)
                    {
                        node[key] = new Node();
                    }

                    node = node[key];
                    idx++;
                }
            }
            
            public AutocompleteSystem(string[] sentences, int[] times)
            {
                if (sentences == null || sentences.Length == 0) return;
                _root = new Node();
                var len = sentences.Length;
                for (int i = 0; i < len; i++)
                {
                    Insert(sentences[i],times[i]);
                }
                Init();
                
            }
            private static SentenceComparator _comparator = new SentenceComparator();
            private Node node;
            private List<Tuple<string, int>> searchRS = new List<Tuple<string, int>>();
            private List<char> prev = new List<char>();

            private void Init()
            {
                node = _root;
                searchRS.Clear();
                prev.Clear();
            }
            private const int TOPN = 3;
            public IList<string> Input(char c)
            {
                var rs = new List<string>();
                if (c == '#')
                {
                    node.Freq++;
                    Init();
                }
                else
                {
                    var searchNode = node[c];
                    if (searchNode == null)
                    { 
                        node[c]=new Node();
                        node = node[c];
                        searchRS.Clear();
                    }
                    else
                    {
                        prev.Add(c);
                        searchRS.Clear();
                        SubSearch(searchNode,prev,searchRS);
                        searchRS.Sort(_comparator);
                        node = searchNode;
                    }
                }

                var top = Math.Min(TOPN, searchRS.Count);
                for (var i = 0; i < top; i++)
                {
                    rs.Add(searchRS[i].Item1);
                }
                return rs;
            }
            
            

            private void SubSearch(Node node, List<char> prev,List<Tuple<string,int>> rs)
            {
                if (node.Freq > 0)
                {
                    rs.Add(new Tuple<string, int>(new string(prev.ToArray()), node.Freq));
                }

                foreach (var child in node._children)
                {
                    var key = child.Key;
                    var childNode = child.Value;
                    prev.Add(key);
                    SubSearch(childNode,prev,rs);
                    prev.RemoveAt(prev.Count-1);
                }
            }
            
            
        }

/**
 * Your AutocompleteSystem object will be instantiated and called as such:
 * AutocompleteSystem obj = new AutocompleteSystem(sentences, times);
 * IList<string> param_1 = obj.Input(c);
 */
    }

    namespace p4
    {
        public class WordDictionary {
            public class Node
            {
                public bool IsEnd { get; set; }
                public Node[] _children = new Node[26];
            }
            private Node _root = new Node();
            /** Initialize your data structure here. */
            public WordDictionary() {
                
            }
    
            /** Adds a word into the data structure. */
            public void AddWord(string word)
            {
                if (string.IsNullOrEmpty(word)) return;
                var idx = 0;
                var node = _root;
                while (true)
                {
                    if (idx == word.Length)
                    {
                        node.IsEnd = true;
                        break;
                    }

                    var key = word[idx] - 'a';
                    if (node._children[key] == null)
                    {
                        node._children[key] = new Node();
                    }

                    idx++;
                    node = node._children[key];
                }
            }
    
            /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
            public bool Search(string word)
            {
                if (string.IsNullOrEmpty(word)) return false;
                return SubSearch(word, 0, _root);
            }

            private bool SubSearch(string word, int idx,Node node)
            {
                if (idx == word.Length) return node.IsEnd;
                if (word[idx] != '.')
                {
                    var next = node._children[word[idx] - 'a'];
                    return next != null && SubSearch(word, idx + 1, next);
                }
                else
                {
                    foreach (var next in node._children)
                    {
                        if (next != null && SubSearch(word, idx + 1, next)) return true;
                    }
                    return false;
                }
            }
        }
}

    namespace p5
    {
        public class Solution
        {
            public static void Test()
            {
                var solution =  new Solution();
                Console.WriteLine($"{solution.FindMaximumXOR(new []{3, 10, 5, 25, 2, 8})}");
            }
            public class Node
            {
                public int Val { get; set; }
                public Node[] _children = new Node[2];

                public Node this[int idx]
                {
                    get => _children[idx];
                    set => _children[idx] = value;
                }
            }

            public void Insert(int num)
            {
                var marker = 0x1 << 30;
                var node = _root;
                while (marker > 0)
                {
                    var key = (marker & num) == 0 ? 0 : 1;
                    var next = node[key];
                    if (next == null)
                    {
                        node[key]=new Node();
                        next = node[key];
                    }

                    node = next;
                    marker >>= 1;
                }

                node.Val = num;
            }

            public int FindMaxXOR(int num)
            {
                var marker = 0x1 << 30;
                var node = _root;
                while (marker > 0)
                {
                    var preferedKey = (marker & num) == 0 ? 1 : 0;
                    var next = node[preferedKey] == null ? node[1 - preferedKey] : node[preferedKey];
                    node = next;
                    marker >>= 1;
                }

                return node.Val ^ num;
            }
            
            private Node _root = new Node();
            public int FindMaximumXOR(int[] nums)
            {
                foreach (var num in nums)
                {
                    Insert(num);
                }

                var max = 0;
                foreach (var num in nums)
                {
                    var val = FindMaxXOR(num);
                    max = Math.Max(val, max);
                }

                return max;
            }
        }
    }

    namespace p6
    {
        
        
        public class Solution {
            public class Node
            {
                public int Val { get; set; }
                public bool IsEnd { get; set; }
                public Node[] _children = new Node[26];

                public Node this[int idx]
                {
                    get => _children[idx];
                    set => _children[idx] = value;
                }
            }

            public void Insert(string word,int index,Node root)
            {
                if (string.IsNullOrEmpty(word)) return;
                var node = root;
                var idx = 0;
                while (true)
                {
                    if (idx == word.Length)
                    {
                        node.IsEnd = true;
                        node.Val = index;
                        break;
                    }

                    var key = word[idx] - 'a';
                    if (node[key] == null)
                    {
                        node[key]=new Node();
                    }

                    node = node[key];
                    idx++;
                }
            }

            private List<int> FindSelfPalindrome(Node node,bool completeInvert = true)
            {
                var prefix = new List<char>();
                var indexes = new List<int>();
                SubSearchSelfPalindrome(node,prefix,indexes,node,completeInvert);
                return indexes;
            }

            private void SubSearchSelfPalindrome(Node node, List<char> prefix,List<int> indexes,Node top,bool completeInvert)
            {
                if (node.IsEnd)
                {
                    var i = 0;
                    var j = prefix.Count - 1;
                    var palin = true;
                    while (i < j)
                    {
                        if (prefix[i] != prefix[j])
                        {
                            palin = false;
                            break;
                        }
                        else
                        {
                            i++;
                            j--;
                        }
                    }

                    if (palin && (node!=top||completeInvert))
                    {
                        indexes.Add(node.Val);
                    }
                }

                for (var i = 0; i < node._children.Length; i++)
                {
                    if (node[i] == null) continue;
                    var key = i + 'a';
                    prefix.Add((char)key);
                    SubSearchSelfPalindrome(node[i],prefix,indexes,top,completeInvert);
                    prefix.RemoveAt(prefix.Count-1);
                }
                
            }
            
            private Node _original = new Node();
            private Node _inverted = new Node();

            public Node FindSubtraction(string word, Node node)
            {
                var idx = 0;
                while (true)
                {
                    if (idx == word.Length) return node;
                    var key = word[idx] - 'a';
                    if (node[key] == null) return null;
                    node = node[key];
                    idx++;
                }
            }

            public static void Test()
            {
                var solution = new Solution();
                var rs = solution.PalindromePairs(new[] {"a","b","c","ab","ac","aa"});//"abcd", "dcba", "lls", "s", "sssll"
                foreach (var pair in rs)
                {
                    Console.WriteLine($"({pair[0]},{pair[1]})");
                }
            }

            private static string Invert(string str)
            {
                var builder = new StringBuilder(str.Length);
                for (var i = 0; i < str.Length; i++)
                {
                    builder.Append(str[str.Length - 1 - i]);
                }

                return builder.ToString();
            }

            private static bool SelfInverted(string str)
            {
                if (string.IsNullOrEmpty(str)) return true;
                var i = 0;
                var j = str.Length - 1;
                while (i < j)
                {
                    if (str[i] != str[j]) return false;
                    i++;
                    j--;
                }
                return true;
            }
            public IList<IList<int>> PalindromePairs(string[] words) {
                var emptyStrList = new List<int>();
                var selfInverted = new List<int>();
                for (var i =0;i<words.Length;i++)
                {
//                    
                    var word = words[i];
                    if (string.IsNullOrEmpty(word))
                    {
                        emptyStrList.Add(i);
                        selfInverted.Add(i);
                    }
                    else
                    {
                        if(SelfInverted(word))selfInverted.Add(i);
                        Insert(Invert(word),i,_inverted);
                        Insert(word,i,_original);
                    }
                }
                
                var rs = new List<IList<int>>();
                for (var i = 0;i<words.Length;i++)
                {
                    var word = words[i];
                    if (string.IsNullOrEmpty(word)) continue;
                    var subtractionNode = FindSubtraction(word, _inverted);
                    if (subtractionNode != null)
                    {
                        var selfPalin = FindSelfPalindrome(subtractionNode);
                        foreach (var i2 in selfPalin)
                        {
                            if(i2==i)continue;
                            rs.Add(new List<int>{i,i2});
                        }
                    }
                    

                    var invertedWord = Invert(word);
                    subtractionNode = FindSubtraction(invertedWord, _original);
                    if (subtractionNode != null)
                    {
                        var selfPalin = FindSelfPalindrome(subtractionNode,false);
                        foreach (var i2 in selfPalin)
                        {
                            if(i2==i)continue;
                            rs.Add(new List<int>{i2,i});
                        }
                    }
                }

                foreach (var i1 in emptyStrList)
                {
                    foreach (var i2 in selfInverted)
                    {
                        if (i1 != i2)
                        {
                            rs.Add(new List<int>{i2,i1});
                            rs.Add(new List<int>{i1,i2});
                        }
                    }
                }

                return rs;
            }
        }
}
}