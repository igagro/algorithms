using System;
using System.Text;

namespace SelectionSort
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

        // Main method for testing solution
        static void Main(string[] args)
        {
            // Add integers separated by space
            var temp = Console.ReadLine().Split(' ');
            var a = Array.ConvertAll(temp, int.Parse);
            var result = SelectionSort(a);
            var sb = new StringBuilder();

            foreach (var number in result)
            {
                sb.Append(number + " ");
            }
            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
    }
}

