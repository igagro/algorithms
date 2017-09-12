using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithm
{
    class Dijkstras
    {
        // List<int>[] is array of lists and every list contains adjancet vertices of i-th vertice in directed graph
        // Cost is list of distances from vertice "u" to adjancet vertice "v"
        static int Dijkstras(List<int>[] adj, List<int>[] cost, int s, int t)
        {
            int n = adj.Length;
            var dist = new int[n];
            var prev = new int[n];
            var visited = new bool[n];
            var heap = new Heap(dist.Length);
            heap.list.Add(new Node(-1, -1));

            for (int i = 1; i < n; i++)
            {
                dist[i] = int.MaxValue; // Fill all indexes with max value of integer since there can't be distance larger than max (this is instead of infinite)
            }

            dist[s] = 0; // distance of first vertice
            heap.list.Add(new Node(0, s));

            while (heap.list.Count > 1)
            {
                Node u = heap.ExtractMin(); // extract vertice with Min distance
                if (visited[u.value]) // if there's no path or there's no vetices unvisited 
                {
                    continue;
                }
                visited[u.value] = true; // set to true so we don't need to explore any more
                for (int i = 0; i < adj[u.value].Count; i++) // scan throuh all neighbours off vertice "u"
                {
                    int v = adj[u.value][i]; // neighbour of vertice "u"
                    int w = cost[u.value][i]; // distance from vertice "u" to his neighbour "v"

                    if (dist[v] > dist[u.value] + w) // if distance of vertice "v" is larger than distance of "u" plus distance from "u" to "v"
                    {
                        dist[v] = dist[u.value] + w; // then set new dsitance which is smaller
                        prev[v] = u.value; // array for reconstructing path
                        heap.Insert(new Node(dist[v], v));
                        //heap.ChangePriority(v, new Node(dist[v],v));
                    }
                }
            }
            return dist[t] == int.MaxValue ? -1 : dist[t]; // return distance from "s" to "t"
        }
    }

    class Node
    {
        public int dist { get; set; }
        public int value { get; set; }

        public Node(int dist, int value)
        {
            this.dist = dist;
            this.value = value;
        }
    }

    class Heap
    {
        public List<Node> list;
        public int n;

        //Using Insert() method (1-based)
        public Heap(int maxSize)
        {
            list = new List<Node>(maxSize);
            n = 0; // size is 0 since there is no elements in tree
        }

        public void Insert(Node node)
        {
            list.Add(node);
            SiftUp(list.Count - 1); // Move element up
        }

        public void SiftDown(int i, int n)
        {
            int minIndex = i;
            int leftChild = 2 * i;
            int rightChild = 2 * i + 1;
            Node temp;

            if (leftChild <= n && list[leftChild].dist < list[minIndex].dist)
            {
                minIndex = leftChild;
            }
            if (rightChild <= n && list[rightChild].dist < list[minIndex].dist)
            {
                minIndex = rightChild;
            }
            if (i != minIndex)
            {
                temp = list[minIndex];
                list[minIndex] = list[i];
                list[i] = temp;
                SiftDown(minIndex, n);
            }
        }

        public void BuildHeapZeroBasedMinHeap()
        {
            int n = list.Count - 1;
            int size = n;
            for (int i = n / 2; i > 0; i--)
            {
                SiftDown(i, size);
            }
        }

        public Node ExtractMin()
        {
            n = list.Count - 1;
            Node result = list[1];
            list[1] = list[n];
            list.RemoveAt(n);
            n--;
            SiftDown(1, n);
            return result;
        }

        public void ChangePriority(int i, Node p)
        {
            Node oldp = list[i];
            list[i] = p;
            if (p.dist > oldp.dist)
            {
                SiftDown(i, n);
            }
            else
            {
                SiftUp(i);
            }
        }

        public void SiftUp(int i)
        {
            Node k = list[i];
            int parent = i / 2;

            while (list[parent].dist > k.dist && i > 0)
            {
                list[i] = list[parent];
                i = parent;
                parent = i / 2;
            }
            list[i] = k;
        }
    }
}
