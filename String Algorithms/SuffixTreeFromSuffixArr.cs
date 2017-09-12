using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringAlgorithms
{
    class SuffixTreeFromSuffixArr
    {
        //STfromSA
        // Note : int[] order = BuildSuffixArray(string text)
        static Node STfromSA(string text, int[] order, int[] lcpArray)
        {
            var root = new Node(null, new Dictionary<char, Node>(), 0, -1, -1);
            var lcpPrev = 0;
            var curNode = root;
            for (int i = 0; i < text.Length; i++)
            {
                var suffix = order[i];
                while (curNode.stringDepth > lcpPrev)
                {
                    curNode = curNode.parent;
                }
                if (curNode.stringDepth == lcpPrev)
                {
                    curNode = CreateNewLeaf(curNode, text, suffix);
                }
                else
                {
                    var edgeStart = order[i - 1] + curNode.stringDepth;
                    var offset = lcpPrev - curNode.stringDepth;
                    var midNode = BreakEdge(curNode, text, edgeStart, offset);
                    curNode = CreateNewLeaf(midNode, text, suffix);
                }
                if (i < text.Length - 1)
                {
                    lcpPrev = lcpArray[i];
                }
            }
            return root;
        }

        static Node CreateNewLeaf(Node node, string text, int suffix)
        {
            var leaf = new Node(node, new Dictionary<char, Node>(), text.Length - suffix, suffix + node.stringDepth, text.Length - 1);
            node.children[text[leaf.edgeStart]] = leaf;
            return leaf;
        }

        static Node BreakEdge(Node node, string text, int start, int offset)
        {
            var startChar = text[start];
            var midChar = text[start + offset];
            var midNode = new Node(node, new Dictionary<char, Node>(), node.stringDepth + offset, start, start + offset - 1);
            midNode.children[midChar] = node.children[startChar];
            node.children[startChar].parent = midNode;
            node.children[startChar].edgeStart = start + offset;
            node.children[startChar] = midNode;
            return midNode;
        }

        /* Build Suffix Array part*/
        static int[] BuildSuffixArray(string text)
        {
            var order = SortCharacters(text);
            var classes = ComputeCharClasses(text, order);
            var l = 1;
            while (l < text.Length)
            {
                order = SortDoubled(text, l, order, classes);
                classes = UpdateClasses(order, classes, l);
                l *= 2;
            }
            return order;
        }

        static int[] SortCharacters(string text)
        {
            var n = text.Length;
            var order = new int[n];
            var count = new int[256];
            int c;

            for (int i = 0; i < n; i++)
            {
                count[text[i]]++;
            }
            for (int j = 1; j < 256; j++)
            {
                count[j] += count[j - 1];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                c = text[i];
                count[c]--;
                order[count[c]] = i;
            }
            return order;
        }

        static int[] ComputeCharClasses(string text, int[] order)
        {
            var classes = new int[text.Length];
            classes[order[0]] = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (text[order[i]] != text[order[i - 1]])
                {
                    classes[order[i]] = classes[order[i - 1]] + 1;
                }
                else
                {
                    classes[order[i]] = classes[order[i - 1]];
                }
            }
            return classes;
        }

        static int[] SortDoubled(string text, int l, int[] order, int[] classes)
        {
            var count = new int[text.Length];
            var newOrder = new int[text.Length];
            int start;
            int cl;
            for (int i = 0; i < text.Length; i++)
            {
                count[classes[i]]++;
            }
            for (int j = 1; j < text.Length; j++)
            {
                count[j] += count[j - 1];
            }
            for (int i = text.Length - 1; i >= 0; i--)
            {
                start = (order[i] - l + text.Length) % text.Length;
                cl = classes[start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }

        static int[] UpdateClasses(int[] newOrder, int[] classes, int l)
        {
            var n = newOrder.Length;
            var newClass = new int[n];
            int cur;
            int prev;
            int mid;
            int midPrev;
            newClass[newOrder[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                cur = newOrder[i];
                prev = newOrder[i - 1];
                mid = (cur + l) % n;
                midPrev = (prev + l) % n;
                if (classes[cur] != classes[prev] || classes[mid] != classes[midPrev])
                {
                    newClass[cur] = newClass[prev] + 1;
                }
                else
                {
                    newClass[cur] = newClass[prev];
                }
            }
            return newClass;
        }

        /*Build LPC array part*/
        static int[] LCP_Array(string text, int[] order)
        {
            var n = text.Length - 1;
            var lcpArray = new int[n];
            var lcp = 0;
            var posInOrder = InvertSuffixArray(order);
            var suffix = order[0];
            for (int i = 0; i <= n; i++)
            {
                var orderIndex = posInOrder[suffix];
                if (orderIndex == n)
                {
                    lcp = 0;
                    suffix = (suffix + 1) % text.Length;
                    continue;
                }
                var nextSuffix = order[orderIndex + 1];
                lcp = LCPOfSuffixes(text, suffix, nextSuffix, lcp - 1);
                lcpArray[orderIndex] = lcp;
                suffix = (suffix + 1) % text.Length;
            }
            return lcpArray;
        }

        static int[] InvertSuffixArray(int[] order)
        {
            var pos = new int[order.Length];
            for (int i = 0; i < pos.Length; i++)
            {
                pos[order[i]] = i;
            }
            return pos;
        }

        static int LCPOfSuffixes(string text, int i, int j, int equal)
        {
            var n = text.Length;
            var lcp = Math.Max(0, equal);
            while (i + lcp < n && j + lcp < n)
            {
                if (text[i + lcp] == text[j + lcp])
                {
                    lcp++;
                }
                else
                {
                    break;
                }
            }
            return lcp;
        }
    }


    class Node
    {
        public Node parent { get; set; }
        public Dictionary<char, Node> children { get; set; }
        public int stringDepth { get; set; }
        public int edgeStart { get; set; }
        public int edgeEnd { get; set; }

        public Node(Node parent, Dictionary<char, Node> children, int stringDepth, int edgeStart, int edgeEnd)
        {
            this.parent = parent;
            this.children = children;
            this.stringDepth = stringDepth;
            this.edgeStart = edgeStart;
            this.edgeEnd = edgeEnd;
        }
    }

}