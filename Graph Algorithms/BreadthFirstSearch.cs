using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms
{
    class BreadthFirstSearch
    {
        // List<int>[] is array of lists and every list contains adjancet vertices of i-th vertice
        static int BreadthFirstSearch(List<int>[] adj, int x, int y)
        {
            var dist = new int[adj.Length]; // Array of all vertices
            int u = 0;
            for (int i = 1; i < adj.Length; i++)
            {
                dist[i] = -1; // Fill all indexes with -1 since there can't be distance less than 0 (this is instead of infinite)
            }

            dist[x] = 0; // Ofcourse distance of first vertice is 0
            var queue = new Queue<int>();
            queue.Enqueue(x); // Add first vertice to queue

            while (queue.Count > 0)
            {
                u = queue.Dequeue();

                foreach (var neighbour in adj[u]) // Scan through all neigbours of first vertice in queue ("u" vertice)
                {
                    if (dist[neighbour] == -1) // If not explored yet
                    {
                        queue.Enqueue(neighbour); // Add vertice to queue
                        dist[neighbour] = dist[u] + 1; // Increment distance of vertice by one
                        if (neighbour == y) // If we found path then return distance from x to y
                        {
                            return dist[neighbour]; // return distance from x to y
                        }
                    }
                }
            }
            return -1; // If there is no path
        }        
    }
}