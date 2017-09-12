using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithm
{
    class BellmanFord
    {
        static int BellmanFord(List<int>[] adj, List<int>[] cost, int infinite)
        {
            int n = adj.Length;
            var dist = new int[n];
            var prev = new int[n];

            for (int i = 1; i < n; i++) // Note : need to find better max value
            {
                dist[i] = infinite + 1; // Fill all indexes with max value of integer since there can't be distance larger than max (this is instead of infinite)
            }

            dist[1] = 0;

            for (int j = 1; j < n; j++) // repeat v-1 times
            {
                for (int i = 0; i < n; i++) // scan throuh all neighbours off vertice "i"
                {
                    int v = adj[j][i]; // neighbour of vertice "i"
                    int w = cost[j][i]; // distance from vertice "i" to his neighbour "v"

                    if (dist[v] > dist[j] + w) // if distance of vertice "v" is larger than distance of "i" plus distance from "i" to "v"
                    {
                        dist[v] = dist[j] + w; // then set new dsitance which is smaller
                        prev[v] = j; // array for reconstructing path
                    }
                }
            }
            return 0;
        }
    }
}