using System;
using System.Collections.Generic;
using System.Text;

namespace StringAlgorithms
{
    class KnuthMorrisPrattAlgorithm
    {
        // First version of solution
        static void KMP1(string pattern, string text)
        {
            var sb = new StringBuilder().Append(pattern).Append('$').Append(text); // Put special char between pattern and text
            var s = new List<int>(); // longest border array
            var t = sb.ToString();
            sb.Clear();
            //var n = pattern.Length + 1;
            var patterntLength = pattern.Length;
            var totalLength = t.Length;
            s.Add(0);
            var border = 0;
            for (int i = 1; i < totalLength; i++)
            {
                while (border > 0 && t[i] != t[border])
                {
                    border = s[border - 1];
                }
                if (t[i] == t[border])
                {
                    ++border;
                }
                else
                {
                    border = 0;
                }
                if (border == patterntLength)
                {
                    sb.Append(i - 2 * patterntLength + " ");
                }
                s.Add(border);
            }
            Console.WriteLine(sb.ToString());
        }

        // Second version of solution
        static void KMP(string pattern, string text)
        {
            var sb = new StringBuilder(); // Very fast output
            var s = new List<int>(); // list of prefix function for pattern (length of longest border)
            var n = pattern.Length + 1;
            var patterntLength = pattern.Length;
            var totalLength = n + text.Length; // length of pattern + text;
            s.Add(0); // first longest border is 0
            var border = 0;
            int i;

            // In first for loop we calculate prefix functions for pattern
            for (i = 1; i < patterntLength; i++)
            {
                while (border > 0 && pattern[i] != pattern[border]) // looping until border is 0 or two chars are same
                {
                    border = s[border - 1];
                }
                if (pattern[i] == pattern[border]) // if they're same then border is increasing by one
                {
                    ++border;
                }
                else
                {
                    border = 0;
                }
                s.Add(border); // adding to prefix function list
            }
            border = 0; // border is reset to 0

            // For loop for finding patternt matching in text. Starting from first char of text.
            for (++i; i < totalLength; i++)
            {
                while (border > 0 && text[i - n] != pattern[border]) //looping until border is 0 or char from text and char from pattern are same
                {
                    border = s[border - 1]; // border is decreased by one positon in prefix list
                }
                if (text[i - n] == pattern[border]) // if they're same border is increasing by one
                {
                    ++border;
                }
                else
                {
                    border = 0;
                }
                if (border == patterntLength) // if border and pattern length are same we have pattern matching
                {
                    border = s[border - 1];
                    sb.Append(i - 2 * patterntLength + " "); // appending to String builder is fastest way for outputing result
                }
            }
            Console.WriteLine(sb.ToString());
        }
    }
}