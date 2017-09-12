using System;

namespace SortingAlgorithms
{
    class CountSort
    {
        static int[] CountSort(int[] a, int m)
        {
            var c = new int[m + 1]; // arr of integers no larger then m+1

            for (int i = 0; i < a.Length; i++) // increment value of index in c by 1 based on those integers occurences in arr a 
            {
                c[a[i]] += 1;
            }

            var b = new int[a.Length]; // result arr

            for (int j = 1; j <= m; j++) // how many input elements are less than or equal to j by keeping a running sum of the array c.
            {
                c[j] += c[j - 1];
            }

            for (int i = a.Length - 1; i >= 0; i--) //adding integers to result arr
            {
                (b[c[a[i]] - 1]) = a[i]; // add integer in his final position
                c[a[i]] -= 1; // decrement value in index of arr c, which is equal as a[i], by one
            }
            return b;
        }       
    }
}