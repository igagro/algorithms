using System;

namespace SortingAlgorithms
{
    class Merge_Sort
    {
        static void Merge(int[] numbers, int left, int mid, int right)
        {
            int[] mergedArray = new int[numbers.Length];

            int lastLeft = (mid - 1);
            int i = left;
            int num_elements = (right - left + 1);

            //Compare two numbers
            while ((left <= lastLeft) && (mid <= right))
            {
                if (numbers[left] <= numbers[mid]) //if number from left arr is smaller then add them to most left position i mergedArray arr
                {
                    mergedArray[i] = numbers[left]; //add numbers[left] to most lestt position
                    i++; //incrementing i and first position of left arr
                    left++;
                }
                else
                {
                    mergedArray[i] = numbers[mid]; //add numbers[mid] to mosta left position
                    i++; //incrementing i and first position of right arr
                    mid++;
                }
            }

            while (left <= lastLeft) //if there is numbers from the left then add them to mergedArray arr
            {
                mergedArray[i] = numbers[left];
                i++;
                left++;
            }

            while (mid <= right) //if there is numbers from right then add them to mergedArray arr
            {
                mergedArray[i] = numbers[mid];
                i++;
                mid++;
            }

            for (int j = 0; j < num_elements; j++) //add number from mergedArray arr to original arr
            {
                numbers[right] = mergedArray[right]; // add from last to first
                right--;
            }
        }

        static void MergeSortRecursive(int[] numbers, int left, int right)
        {
            int mid;

            if (right > left) // base case for recursive function. That is, when one number is left
            {
                mid = (right + left) / 2;
                MergeSortRecursive(numbers, left, mid); // left arr
                MergeSortRecursive(numbers, (mid + 1), right); // right arr

                Merge(numbers, left, (mid + 1), right);
            }
        }
    }
}