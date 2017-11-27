using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms
{
    class EulerianCycle
    {
        // Input graph is represetnt as adjancency list of vertices and edges between them
        public static string EulerianCycle(List<int>[] edges)
        {
            // Create boolean adjacency list mirror to track visited, iff all visited then we have Eulerian path
            bool[][] visited = new bool[edges.Length][];
            for (int i = 0; i < edges.Length; ++i)
            {
                if (edges[i] != null)
                {
                    visited[i] = new bool[edges[i].Count];
                }
            }

            // Create initial Cycle
            var start = 0; // Starting from first vertice
            var curr = start;
            var cycle = new List<int>
            {
                start
            };
            do
            {
                int next = -1; // This variable is for storing next vertice in path, but it's also telling us if we have a cycle
                for (int i = 0; i < edges[curr].Count; i++)
                {
                    if (!visited[curr][i])
                    {
                        next = edges[curr][i];
                        visited[curr][i] = true;
                        break; // if we visited this edge we continue to next vertice
                    }
                }
                cycle.Add(next);
                curr = next;
                if (next == -1) // If "next" remained -1 this means there isn't no more cycles so we return "0"
                {
                    return "0";
                }
            } while (curr != start); // Doing until we return to starting position

            // Create final Cycle
            while (true)
            {
                // Looking for next vertice with unvisited outgoing edge
                bool stop = true;
                for (int i = 0; i < visited.Length; ++i)
                {
                    for (int j = 0; visited[i] != null && j < visited[i].Length; ++j)
                    {
                        if (!visited[i][j])
                        {
                            start = i;
                            curr = i;
                            stop = false;
                        }
                    }
                    if (!stop) // If we found unvisited egde, we stop. Now starting vertice is one with unvisited edge
                    {
                        break;
                    }
                }
                if (stop) // If there's no more unvisited edges it means we're done
                {
                    break;
                }


                List<int> inBetween = new List<int>(); // After "stucked" on starting vertice, we need to continue to find next (at the end this is middle cycle) cycle
                var before = new List<int>(cycle.GetRange(0, cycle.IndexOf(curr) + 1)); // Cycle before we get "stucked"
                var after = new List<int>(cycle.GetRange(cycle.IndexOf(curr) + 1, cycle.Count() - (cycle.IndexOf(curr) + 1))); // Last part of cycle

                do
                {
                    int next = -1;
                    for (int i = 0; i < visited[curr].Length; ++i)
                    {
                        if (!visited[curr][i])
                        {
                            next = edges[curr][i];
                            visited[curr][i] = true;
                            break;
                        }
                    }
                    inBetween.Add(next);
                    curr = next;
                    if (next == -1)
                    {
                        return "0";
                    }
                } while (curr != start);

                cycle = new List<int>();
                for (int i = 0; i < before.Count(); ++i)
                {
                    cycle.Add(before[i]);
                }
                for (int i = 0; i < inBetween.Count(); ++i)
                {
                    cycle.Add(inBetween[i]);
                }
                for (int i = 0; i < after.Count; ++i)
                {
                    cycle.Add(after[i]);
                }
            }

            // Output final Cycle
            var result = new StringBuilder();
            result.AppendLine("1");
            for (int i = 0; i < cycle.Count - 1; i++)
            {
                result.Append(cycle[i] + 1 + " ");
            }
            return result.ToString();
        }
    }
}