using System;

namespace SearchAlgorithms
{
    class Binary_Search
    {
        //Recursive algorithm
        int BinarySearch(int[] a, int low, int high, int key)
        {
            if (high < low)
            {
                return low - 1;
            }

            int mid = low + ((high - low) / 2);

            if (key == a[mid])
            {
                return mid;
            }
            else if (key < a[mid])
            {
                return BinarySearch(a, low, mid - 1, key);
            }
            else
            {
                return BinarySearch(a, mid + 1, high, key);
            }
        }

        //Itearative
        int BinarySearchIterative(int[] a, int low, int high, int key)
        {
            while (low <= high)
            {
                int mid = low + ((high - low) / 2);

                if (key == a[mid])
                {
                    return mid;
                }
                else if (key < a[mid])
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return low - 1;
        }
    }
}