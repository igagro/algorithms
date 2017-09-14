using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms
{
    class Kruskal_Algorithm
    { 
        class Edge
        {
            public int _u;
            public int _v;
            public int _w;

            public Edge(int u, int v, int w)
            {
                _u = u;
                _v = v;
                _w = w;
            }
        }

        // List<int>[] is array of lists and every list contains adjancet vertices of i-th vertice in directed graph
        // Cost is list of distances from vertice "u" to adjancet vertice "v"
        static List<Edge> Kruskal(List<int>[] adj, List<int>[] cost)
        {
            var set = new DisjointSetUBR(adj.Length);

            for (int i = 1; i < adj.Length; i++)
            {
                set.MakeSet(i); // We make separate disjoint set for each vertice in graph
            }

            var toSort = new List<Edge>(); // List to sort egde by weight
            var x = new List<Edge>(); // Resulting list

            for (int i = 1; i < adj.Length; i++) // Scan through all vertices
            {
                for (int j = 0; j < adj[i].Count; j++) // Scan through all neighbours
                {
                    toSort.Add(new Edge(i, adj[i][j], cost[i][j])); // Add edge from "i" to "j" to list
                }
            }

            var sorted = toSort.OrderBy(s => s._w); // Sort list by edge weight

            foreach (var edge in sorted)
            {
                if (set.Find(edge._u) != set.Find(edge._v)) // If they aren't in same set (if this edge won't make a cycle)
                {
                    x.Add(edge); // Add to resulting list
                    set.Union(edge._u, edge._v); // Set this two vertices in same set
                }
            }
            return x;
        }
    }

    // Disjoint set using union by rank
    class DisjointSetUBR
    {
        int[] parent;
        int[] rank; // height of tree

        public DisjointSetUBR(int n)
        {
            parent = new int[n];
            rank = new int[n];
        }

        public DisjointSetUBR(int[] arr)
        {
            parent = new int[arr.Length + 1];
            rank = new int[arr.Length + 1];
        }

        public void MakeSet(int i)
        {
            parent[i] = i;
            //rank[i] = 0; not needed in C# 
        }

        public int Find(int i)
        {
            while (i != parent[i]) // If i is not root of tree we set i to his parent until we reach root (parent of all parents)
            {
                i = parent[i];
            }
            return i;
        }

        // Path compression, O(log*n). For practical values of n, log* n <= 5
        public int FindPath(int i)
        {
            if (i != parent[i])
            {
                parent[i] = FindPath(parent[i]);
            }
            return parent[i];
        }

        public void Union(int i, int j)
        {
            int i_id = Find(i); // Find the root of first tree (set) and store it in i_id
            int j_id = Find(j); // // Find the root of second tree (set) and store it in j_id

            if (i_id == j_id) // If roots are equal (they have same parents) than they are in same tree (set)
            {
                return;
            }

            if (rank[i_id] > rank[j_id]) // If height of first tree is larger than second tree
            {
                parent[j_id] = i_id; // We hang second tree under first, parent of second tree is same as first tree
            }
            else
            {
                parent[i_id] = j_id; // We hang first tree under second, parent of first tree is same as second tree
                if (rank[i_id] == rank[j_id]) // If heights are same
                {
                    rank[j_id]++; // We hang first tree under second, that means height of tree is incremented by one
                }
            }
        }
    }

}