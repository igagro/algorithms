using System;
using System.Collections.Generic;

namespace NetworkFlow
{
    class Bipartite_Matching
    {
        static public void Solve()
        {
            bool[][] bipartiteGraph = ReadData();
            int[] matching = FindMatching(bipartiteGraph);
        }

        // This is input method if you're using console application, but you can use it for any type of application. See comments where I explained how input schema looks
        static bool[][] ReadData()
        {
            var temp = Console.ReadLine().Split(' ');
            var arrTemp = Array.ConvertAll(temp, int.Parse);
            // number of left column vertices
            int numLeft = arrTemp[0];
            // number of right column vertices
            int numRight = arrTemp[1];
            bool[][] adjMatrix = new bool[numLeft][];
            for (int i = 0; i < numLeft; i++)
            {
                adjMatrix[i] = new bool[numRight];
            }
            // if left column vertice is matching with right column vertice input is 1, otherwise is 0
            // so if they're matching we store boolean true in array, otherwise we store boolean false
            for (int i = 0; i < numLeft; i++)
            {
                temp = Console.ReadLine().Split(' ');
                arrTemp = Array.ConvertAll(temp, int.Parse);
                for (int j = 0; j < numRight; j++)
                {
                    adjMatrix[i][j] = (arrTemp[j] == 1);
                }
            }
            return adjMatrix;
        }

        static void MaxFlow(FlowGraph graph, int from, int to)
        {
            while (true)
            {
                bool foundPath = false;
                var queue = new Queue<int>();
                var parentIds = new int[graph.Size()];
                for (int i = 0; i < parentIds.Length; i++)
                {
                    parentIds[i] = -1; //unvisited nodes are -1
                }
                queue.Enqueue(0);
                //Breadth-first search, finding shortest path
                while (queue.Count != 0 && !foundPath)
                {
                    int node = queue.Dequeue();
                    var ids = graph.GetIds(node); // all the edges from current node
                    foreach (int id in ids)
                    {
                        var edge = graph.GetEdge(id);
                        if (edge.flow < edge.capacity && parentIds[edge.to] == -1) // if there's capacity left and "edge pointing" vertice isn't visited
                        {
                            if (edge.to == edge.from)
                            {
                                continue;
                            }
                            parentIds[edge.to] = id;
                            if (edge.to == graph.Size() - 1) // we found path
                            {
                                foundPath = true;
                                break;
                            }
                            queue.Enqueue(edge.to);
                        }
                    }
                }
                if (!foundPath)
                {
                    break;
                }
                //find the value of the flow
                to = graph.Size() - 1;
                var minCap = -1;
                //calculate min capacity
                while (to != 0)
                {
                    var id = parentIds[to];
                    var edge = graph.GetEdge(id);
                    if (minCap == -1 || (edge.capacity - edge.flow) < minCap)
                    {
                        minCap = edge.capacity - edge.flow;
                    }
                    to = edge.from;
                }
                to = graph.Size() - 1;
                //adding flow to edges
                while (to != 0)
                {
                    var id = parentIds[to];
                    var edge = graph.GetEdge(id);
                    graph.AddFlow(id, minCap);
                    to = edge.from;
                }
            }
        }

        static FlowGraph ReadGraph(bool[][] bipartiteGraph)
        {
            var n = bipartiteGraph.Length;
            var m = bipartiteGraph[0].Length;
            var vertex_count = n + m + 2;
            var graph = new FlowGraph(vertex_count);
            for (int i = 0; i < n; i++)
            {
                graph.addEdge(0, i + 1, 1);
            }
            for (int i = 0; i < m; i++)
            {
                graph.addEdge(n + 1 + i, n + m + 1, 1);
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (!bipartiteGraph[i][j])
                    {
                        continue;
                    }
                    graph.addEdge(i + 1, n + 1 + j, 1);
                }
            }
            return graph;
        }

        class Edge
        {
            public int from, to, capacity, flow;

            public Edge(int from, int to, int capacity)
            {
                this.from = from;
                this.to = to;
                this.capacity = capacity;
                flow = 0;
            }
        }

        class FlowGraph
        {
            /* List of all - forward and backward - edges */
            private List<Edge> edges;

            /* These adjacency lists store only indices of edges from the edges list */
            private List<int>[] graph;

            public FlowGraph(int n)
            {
                graph = new List<int>[n];
                for (int i = 0; i < n; ++i)
                {
                    graph[i] = new List<int>();
                }
                edges = new List<Edge>();
            }

            public void addEdge(int from, int to, int capacity)
            {
                /* Note that we first append a forward edge and then a backward edge,
                 * so all forward edges are stored at even indices (starting from 0),
                 * whereas backward edges are stored at odd indices. */
                var forwardEdge = new Edge(from, to, capacity);
                var backwardEdge = new Edge(to, from, 0);
                graph[from].Add(edges.Count);
                edges.Add(forwardEdge);
                graph[to].Add(edges.Count);
                edges.Add(backwardEdge);
            }

            public int Size()
            {
                return graph.Length;
            }

            public List<int> GetIds(int from)
            {
                return graph[from];
            }

            public Edge GetEdge(int id)
            {
                return edges[id];
            }

            public void AddFlow(int id, int flow)
            {
                /* To get a backward edge for a true forward edge (i.e id is even), we should get id + 1
                 * due to the described above scheme. On the other hand, when we have to get a "backward"
                 * edge for a backward edge (i.e. get a forward edge for backward - id is odd), id - 1
                 * should be taken.*/

                edges[id].flow += flow;
                edges[id ^ 1].flow -= flow;
            }
        }

        static int[] FindMatching(bool[][] bipartiteGraph)
        {
            int numLeft = bipartiteGraph.Length;
            int numRight = bipartiteGraph[0].Length;

            int[] matching = new int[numLeft];
            FlowGraph graph = ReadGraph(bipartiteGraph);
            MaxFlow(graph, 0, graph.Size() - 1);
            for (int i = 0; i < numLeft; i++)
            {
                foreach (int id in graph.GetIds(i + 1))
                {
                    Edge edge = graph.GetEdge(id);
                    if (edge.flow == 1)
                    {
                        matching[i] = edge.to - numLeft;
                        break;
                    }
                    matching[i] = -1;
                }
            }

            return matching;
        }
    }
}