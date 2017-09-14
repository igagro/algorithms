using System;

namespace StringAlgorithms
{
    class Suffix_Arrays_Faster
    {
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
    }
}