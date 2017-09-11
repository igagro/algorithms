using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickSort
{
    class QuickSort
    {
        static Random rnd = new Random();      

        // Solution with random pivot
        static void QuickSortRandmomPivot(int[] a, int l, int r)
        {
            if (l < r)
            {
                int pivot = rnd.Next(l, r); // get the random number between sequence of l to r
                int tempPivot = a[l];//
                a[l] = a[pivot];     // Swap value of first index with value of pivot index
                a[pivot] = tempPivot;//

                int q = PartitionRandom(a, l, r); // use Parition function to arrange arr a with all the integers, that are less or equal than pivot, on left and all larger integers than pivot on right
                QuickSortRandmomPivot(a, l, q - 1); // recursive call of left half or arr, that is integers less or equal than pivot
                QuickSortRandmomPivot(a, q + 1, r); // recursive call of right half or arr, that is integers larger than pivot
            }
        }

        static int PartitionRandom(int[] a, int l, int r)
        {
            int x = a[l]; // pivot
            int j = l;

            for (int i = l + 1; i <= r; i++) // iterate through arr to at most r
            {
                if (a[i] <= x) // if value of current i is smaller then pivot
                {
                    j++; // size of smaller integers is increasing
                    int temp = a[j];//
                    a[j] = a[i];    // Swap those two integers
                    a[i] = temp;    //
                }
            }
            int tempB = a[l];//
            a[l] = a[j];     // at the end swap first element(pivot) with the last element of arr with smaller integers than pivot
            a[j] = tempB;    //
            return j;        //
        }

        // Solution without random pivot
        static void QuickSort(int[] a, int l, int r)
        {
            if (l < r)
            {
                int q = Partition(a, l, r); // use Parition function to arrange arr a with all the integers, that are less or equal than pivot, on left and all larger integers than pivot on right
                QuickSortRandmomPivot(a, l, q - 1); // recursive call of left half or arr, that is integers less or equal than pivot
                QuickSortRandmomPivot(a, q + 1, r); // recursive call of right half or arr, that is integers larger than pivot
            }
        }

        static int Partition(int[] a, int l, int r)
        {
            int x = a[l]; // pivot
            int j = l;

            for (int i = l + 1; i <= r; i++) // iterate through arr to at most r
            {
                if (a[i] <= x) // if value of current i is smaller then pivot
                {
                    j++; // size of smaller integers is increasing
                    int temp = a[j];//
                    a[j] = a[i];    // Swap those two integers
                    a[i] = temp;    //
                }
            }
            int tempB = a[l];//
            a[l] = a[j];     // at the end swap first element(pivot) with the last element of arr with smaller integers than pivot
            a[j] = tempB;    //
            return j;        //
        }

        // Solution with radnom pivot and 3-way partition, used for most equal elements
        static void QuickSortRandmomPivot3Partition(int[] a, int l, int r)
        {
            if (l < r)
            {
                int pivot = rnd.Next(l, r); // get the random number between sequence of l to r
                int tempPivot = a[l];//
                a[l] = a[pivot];     // Swap value of first index with value of pivot index
                a[pivot] = tempPivot;//

                var q = Partition3(a, l, r); // use Parition function to arrange arr a with all the integers, that are less or equal than pivot, on left and all larger integers than pivot on right
                var m1 = q[0];
                var m2 = q[1];
                QuickSortRandmomPivot3Partition(a, l, m1 - 1); // recursive call of left half or arr, that is integers less or equal than pivot
                QuickSortRandmomPivot3Partition(a, m2 + 1, r); // recursive call of right half or arr, that is integers larger than pivot
            }
        }

        static int[] Partition3(int[] a, int l, int r)
        {
            int pivot = a[l];
            int lt = l;
            int gt = r;
            int i = l + 1;

            while (i <= gt)
            {
                if (a[i] < pivot)
                {
                    int temp = a[lt];
                    a[lt] = a[i];
                    a[i] = temp;
                    lt += 1;
                    i += 1;
                }
                else if (a[i] > pivot)
                {
                    int temp = a[i];
                    a[i] = a[gt];
                    a[gt] = temp;
                    gt -= 1;
                }
                else
                {
                    i += 1;
                }
            }
            int[] m = { lt, gt };
            return m;
        }


        // Main method for testing above solutions
        static void Main(string[] args)
        {
            //Add numbers separated by space
            var temp = Console.ReadLine().Split(' ');
            var a = Array.ConvertAll(temp, int.Parse);
            QuickSortRandmomPivot3Partition(a, 0, a.Length - 1);

            foreach (var number in a)
            {
                Console.Write(number + " ");
            }
            Console.ReadLine();
        }
    }
}