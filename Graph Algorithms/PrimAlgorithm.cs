using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prim
{
    class PrimAlgorithm
    {
        // List<int>[] is array of lists and every list contains adjancet vertices of i-th vertice in undirected graph
        // Cost is list of distances from vertice "u" to adjancet vertice "v"
        static int[] Prim(List<int>[] adj, List<int>[] cost)
        {
            int n = adj.Length;
            var dist = new int[n];
            var prev = new int[n];
            var visited = new bool[n];

            for (int i = 1; i < n; i++)
            {
                dist[i] = int.MaxValue;
            }

            dist[3] = 0; // random pick starting node

            for (int j = 0; j < n; j++) // scan vertice times
            {
                int u = ExtractMin(dist, visited); // extract vertice with min distance
                if (u == -1) // if there's no path or there's no unvisited vertices
                {
                    continue;
                }
                visited[u] = true; // set to true so we don't need to explore this vertice any more

                for (int i = 0; i < adj[u].Count; i++)
                {
                    int v = adj[u][i]; // neighbour of vertice "u"
                    int w = cost[u][i]; // distance from vertice "u" to his neighbour "v"
                    if (!visited[v] && dist[v] > w) // if vertice isn't visited and distance is bigger than "w"
                    {
                        dist[v] = w; // set new distance
                        prev[v] = u; // change priority
                    }
                }
            }
            return prev;
        }

        static int ExtractMin(int[] dist, bool[] visited)
        {
            int minDist = int.MaxValue; // set minimal distance to max value
            int minVertex = -1;
            for (int i = 1; i < dist.Length; i++) // scan through all distances
            {
                if (visited[i]) // if already visited, no need to explore sice we can't go back in path
                {
                    continue;
                }
                if (dist[i] < minDist) // if distance is smaller than current distance of vertice "i"
                {
                    minVertex = i; // then vertice with Min distance is minVertex
                }
                minDist = Math.Min(minDist, dist[i]); // check which of all distances from vertice "i" is smallest
            }
            return minVertex; // return vertice with min distance
        }

    }
}