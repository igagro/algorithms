using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    class Bidirectional_Dijkstras
    {
        // adj[0] and cost[0] store the initial graph, adj[1] and cost[1] store the reversed graph.
        // Each graph is stored as array of adjacency lists for each node. adj stores the edges,
        // and cost stores their costs.
        // distance[0] and distance[1] correspond to distance estimates in the forward and backward searches.
        // Two priority queues, one for forward and one for backward search.
        // visited[v] == true iff v was visited either by forward or backward search
        // proc for procesed vertices in initial graph, procR for procesed vertices in reversed graph
        // s is source vertice, t is end vertice
        public static long BidirectionalDijkstra(List<int>[][] adj, List<int>[][] cost, long[][] dist, bool[] visited, Heap heap, List<int> proc, Heap heapR, List<int> procR, int s, int t)
        {
            var graph = 0;
            var v = new Node(0, 0);
            heap.list.Add(new Node(-1, -1));
            heapR.list.Add(new Node(-1, -1));
            dist[0][s] = dist[1][t] = 0;
            heap.list.Add(new Node(0, s));
            heapR.list.Add(new Node(0, t));
            do
            {
                v = heap.ExtractMin();
                visited[v.value] = true;
                graph = 0;
                Process(v, adj, cost, dist, proc, heap, visited, graph, s, t);

                if (procR.Contains(v.value))
                {
                    return ShortestPath(s, dist, proc, t, procR, v.value);
                }

                v = heapR.ExtractMin();
                visited[v.value] = true;
                graph = 1;
                Process(v, adj, cost, dist, procR, heapR, visited, graph, s, t);

                if (proc.Contains(v.value))
                {
                    return ShortestPath(t, dist, procR, s, proc, v.value);
                }
            } while (heap.list.Count > 1 && heapR.list.Count > 1);

            return -1;
        }

        static void Process(Node u, List<int>[][] adj, List<int>[][] cost, long[][] dist, List<int> proc, Heap heap, bool[] visited, int graph, int s, int t)
        {
            for (int i = 0; i < adj[graph][u.value].Count; i++)
            {
                var v = adj[graph][u.value][i]; // neighbour of vertice "u"
                var w = cost[graph][u.value][i]; // distance from vertice "u" to his neighbour "v"

                if (dist[graph][v] > dist[graph][u.value] + w) // if distance of vertice "v" is larger than distance of "u" plus distance from "u" to "v"
                {
                    dist[graph][v] = dist[graph][u.value] + w; // then set new dsitance which is smaller
                    heap.Insert(new Node(dist[graph][v], v));
                }
            }
            proc.Add(u.value);
        }

        static long ShortestPath(int s, long[][] dist, List<int> proc, int t, List<int> procR, int value)
        {
            var distance = dist[0][value] + dist[1][value];
            foreach (var u in proc)
            {
                if (dist[0][u] + dist[1][u] < distance)
                {
                    distance = dist[0][u] + dist[1][u];
                }
            }
            return distance;
        }
    }

    class Node
    {
        public long dist { get; set; }
        public int value { get; set; }

        public Node(long dist, int value)
        {
            this.dist = dist;
            this.value = value;
        }
    }

    class Heap
    {
        public List<Node> list;
        public int n;
        int minIndex;
        int leftChild;
        int rightChild;

        //Using Insert() method (1-based)
        public Heap()
        {
            list = new List<Node>();
            //n = 0; // size is 0 since there is no elements in tree
            //list[0].dist = int.MaxValue; // since this is for 1-based array we set 0 element as max vaelu
        }

        public void Insert(Node node)
        {
            list.Add(node);
            SiftUp(list.Count - 1); // Move element up
        }

        public void SiftDown(int i)
        {
            minIndex = i;
            leftChild = 2 * i;
            rightChild = 2 * i + 1;
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
                SiftDown(minIndex);
            }
        }

        public Node ExtractMin()
        {
            n = list.Count - 1;
            Node result = list[1];
            list[1] = list[n];
            list.RemoveAt(n);
            n--;
            SiftDown(1);
            return result;
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