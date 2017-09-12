using System;
using System.Text;

namespace SortingAlgorithms
{
    class SelectionSort
    {
       static int[] SelectionSort(int[] a)
        {
            for (int i = 0; i < a.Length; i++) //Iterate through array
            {
                var minIndex = i;
                for (int j = i + 1; j < a.Length; j++) //Iterate through rest of unsorted part of array
                {
                    if (a[j] < a[minIndex]) //Find the smallest number
                    {
                        minIndex = j; //Get the index of smallest number
                    }
                }
                //Swap the i with the smallest number
                var temp = a[i];
                a[i] = a[minIndex];
                a[minIndex] = temp;
            }
            return a;
        }
    }
}

