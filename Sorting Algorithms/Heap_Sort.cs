using System;

namespace SortingAlgorithms
{
    class Heap_Sort
    {
        class Heap
        {
            public void HeapSortInPlace()
            {
                var temp = 0;
                var size = n;
                BuildHeap();
                for (int i = 0; i < list.Length - 1; i++)
                {
                    temp = list[0];
                    list[0] = list[size];
                    list[size] = temp;
                    size--;
                    SiftDown(0, size);
                }
            }

            public int[] list;
            int n;

            //Using BuildHeap() method
            public Heap(int[] arr)
            {
                list = arr;
                n = arr.Length - 1;
            }

            public void BuildHeap()
            {
                int size = n;
                for (int i = n / 2; i >= 0; i--)
                {
                    SiftDown(i, size);
                }
            }

            public void SiftDown(int i, int n)
            {
                int maxIndex = i;
                int leftChild = 2 * i + 1;
                int rightChild = 2 * i + 2;
                int temp = 0;

                if (leftChild <= n && list[leftChild] > list[maxIndex])
                {
                    maxIndex = leftChild;
                }
                if (rightChild <= n && list[rightChild] > list[maxIndex])
                {
                    maxIndex = rightChild;
                }
                if (i != maxIndex)
                {
                    temp = list[maxIndex];
                    list[maxIndex] = list[i];
                    list[i] = temp;
                    SiftDown(maxIndex, n);
                }
            }
        }
    }
}