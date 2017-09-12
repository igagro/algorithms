using System;
using System.Collections.Generic;
using System.Linq;

namespace StringAlgorithms
{
    class SuffixArrays
    {
        static IOrderedEnumerable<Suffix> SuffixArrays(string text)
        {
            var list = new List<Suffix>();
            for (int i = 0; i < text.Length; i++)
            {
                list.Add(new Suffix(i, text.Substring(i)));
            }
            list.Add(list[list.Count - 1]);
            list.RemoveAt(list.Count - 1);

            var result = list.OrderBy(s => s.suffix);
            //Array.Sort(arr, (x, y) => x.suffix.CompareTo(y.suffix));
            return result;
        }
    }

    class Suffix
    {
        public int startingPosition { get; set; }
        public string suffix { get; set; }

        public Suffix(int startingPostion, string suffix)
        {
            this.startingPosition = startingPostion;
            this.suffix = suffix;
        }
    }

    class SuffixComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, true);
        }
    }
}