using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms
{
    class DepthFirstSearch
    {
        // List<int>[] is array of lists and every list contains adjancet vertices of i-th vertice
        static List<int> TopologicalSort(List<int>[] adj, bool[] visited)
        {
            var clock = 1; // start sort from number one
            var previsit = new int[adj.Length];
            var postvisit = new int[adj.Length];

            return DepthFirst(adj, visited, previsit, postvisit, clock);
        }

        static List<int> DepthFirst(List<int>[] adj, bool[] visited, int[] previsit, int[] postvisit, int clock)
        {
            var list = new List<int>(); // resulting list (post array)
            for (int i = 1; i < adj.Length; i++) // scan through each vertice
            {
                if (!visited[i]) // if not explored
                {
                    clock = Explore(adj, i, visited, previsit, postvisit, clock, list);
                }
            }
            return list;
        }

        static int Explore(List<int>[] adj, int v, bool[] visited, int[] previsit, int[] postvisit, int clock, List<int> list)
        {
            visited[v] = true; // set visited to true for vertice v
            previsit[v] = clock; // Previsit
            ++clock;

            foreach (var neighbour in adj[v]) // scan through each neighbour of vertice v
            {
                if (visited[neighbour] == false) // if we didn't visited neighbour
                {
                    clock = Explore(adj, neighbour, visited, previsit, postvisit, clock, list); // do again for the current neighbour
                }
            }
            postvisit[v] = clock; // Postvisit
            list.Add(v); // adding to resulting list (we could use only array postvisit, and then sort array in descending. But adding vertice to list is faster)
            ++clock;

            return clock;
        }

    }
}