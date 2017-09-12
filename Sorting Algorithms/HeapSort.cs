using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class HeapSort
    {
        class Heap
        {
            public void HeapSort(int[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    Insert(a[i]);
                }
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    a[i] = ExtractMax();
                }
            }

            public void HeapSortInPlace()
            {
                int temp = 0;
                int size = n;
                BuildHeapZeroBased();
                for (int i = 0; i < list.Length - 1; i++)
                {
                    temp = list[0];
                    list[0] = list[size];
                    list[size] = temp;
                    size--;
                    SiftDownZeroBased(0, size);
                }
            }

            int[] list;
            int n;

            //Using Insert() method (1-based)
            public Heap(int maxSize)
            {
                list = new int[maxSize];
                n = 0; // size is 0 since there is no elements in tree
                list[0] = int.MaxValue; // since this is for 1-based array we set 0 element as max value
            }

            //Using BuildHeap() method (0-based)
            public Heap(int[] arr)
            {
                list = arr;
                n = arr.Length - 1;
            }

            public void Insert(int value)
            {
                n++; // increment number of elements
                list[n] = value;
                SiftUp(n); // Move element up
            }

            public void SiftUp(int i)
            {
                int k = list[i];
                int parent = i / 2;

                while (list[parent] < k && i > 0)
                {
                    list[i] = list[parent];
                    i = parent;
                    parent = i / 2;
                }
                list[i] = k;
            }

            public void SiftDownZeroBased(int i, int n)
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
                    SiftDownZeroBased(maxIndex, n);
                }
            }

            public int ExtractMax()
            {
                int result = list[1];
                list[1] = list[n];
                n--;
                SiftDown(1);
                return result;
            }

            public void BuildHeapZeroBased()
            {
                //int n = a.Length-1;
                int size = n;
                for (int i = n / 2; i >= 0; i--)
                {
                    SiftDownZeroBased(i, size);
                }
            }           
        }       
    }
}