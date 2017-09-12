using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringAlgorithms
{
    class BurrowsWheelerTransform
    {
        static string BWT(string text)
        {
            var list = new List<string>();
            var queue = new Queue<char>();
            var sb = new StringBuilder();
            var n = text.Length;
            for (int i = 0; i < text.Length; i++)
            {
                queue.Enqueue(text[i]); // fill queue with with all charachters
            }

            for (int i = 0; i < n; i++)
            {
                queue.Enqueue(queue.Dequeue()); // put the first on last position
                var original = new Queue<char>(queue); // keep the copy of queue since we will dequeue from queue and add to StringBuilder
                for (int j = 0; j < n; j++)
                {
                    sb.Append(queue.Dequeue()); // adding characters to StringBuilder
                }
                list.Add(sb.ToString()); // Add shifted string to list
                sb.Clear(); // clear StringBuilder
                queue = original; // return queue to state before dequeue
            }

            list.Sort(); // sort list 

            for (int i = 0; i < n; i++)
            {
                sb.Append(list[i][n - 1]); // print only last character of each string in list
            }
            return sb.ToString();
        }
    }
}