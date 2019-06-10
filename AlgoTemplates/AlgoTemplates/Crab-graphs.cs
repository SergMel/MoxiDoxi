using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoTemplates.Crab_graphs
{

	public class Edge
	{
		public int Start { get; set; }
		public int End { get; set; }
		public int Capacity { get; set; }

		public Edge Paired { get; set; }
	}

	public class FlexGraph
	{
		public Dictionary<int, List<Edge>> Edges;
		public int VCount => Edges.Count;


		public FlexGraph()
		{
			Edges = new Dictionary<int, List<Edge>>();
		}

		public void AddEdge(Edge edge)
		{
			if (!Edges.ContainsKey(edge.Start))
				Edges[edge.Start] = new List<Edge>();
			Edges[edge.Start].Add(edge);
		}

		public void RemoveVertex(int id)
		{
			this.Edges.Remove(id);
		}

	}
	public class ResidualGraph
	{
		public List<List<Edge>> Edges;
		public int VCount { get; private set; }

		public ResidualGraph(int size)
		{
			VCount = size;
			Edges = Enumerable.Repeat((List<Edge>)null, size).ToList();
		}

		public void AddEdge(int start, int end, int capacity)
		{
			var edge = new Edge()
			{
				Start = start,
				End = end,
				Capacity = capacity
			};
			var edge2 = new Edge()
			{
				Start = end,
				End = start,
				Capacity = 0,
				Paired = edge,
			};
			edge.Paired = edge2;
			(Edges[start] = Edges[start] ?? new List<Edge>()).Add(edge);
			(Edges[end] = Edges[end] ?? new List<Edge>()).Add(edge2);

		}
	}

	public class Pathes
	{
		public List<Edge>[] Preious { get; set; }
		public int[] Lengths { get; set; }

	}

	public class Crab_graphs
	{
		public static Pathes BFS(ResidualGraph graph, int start, int end)
		{
			Queue<int> q = new Queue<int>();
			int[] pathLength = Enumerable.Repeat<int>(-1, graph.VCount).ToArray();
			List<Edge>[] prev = new List<Edge>[graph.VCount];
			q.Enqueue(start);
			pathLength[start] = 0;
			prev[start] = new List<Edge>();
			int curLength = 0;
			while (q.Count > 0)
			{
				var itm = q.Dequeue();
				if (pathLength[end] > -1 && pathLength[end] < curLength)
					break;

				var nbrs = graph.Edges[itm]?.Where(el => el.Capacity > 0) ?? Enumerable.Empty<Edge>();
				foreach (var item in nbrs)
				{
					if (pathLength[item.End] >= 0 && pathLength[item.End] < pathLength[item.Start] + 1)
						continue;
					if (pathLength[item.End] < 0)
						q.Enqueue(item.End);
					pathLength[item.End] = pathLength[item.Start] + 1;
					curLength = pathLength[item.End];
					(prev[item.End] = prev[item.End] ?? new List<Edge>()).Add(item);
				
				}
			}
			return pathLength[end] == -1 ? (Pathes)null : new Pathes
			{
				Lengths = pathLength,
				Preious = prev
			};
		}

		public static FlexGraph BuildLGraph(Pathes pathes, ResidualGraph origin, int start, int end)
		{
			FlexGraph ret = new FlexGraph();

			Queue<int> q = new Queue<int>();
			q.Enqueue(end);
			while (q.Count > 0)
			{
				var i = q.Dequeue();
				var itms = pathes.Preious[i];

				if (itms == null)
					throw new InvalidOperationException();
				if (itms.Count == 0 && i == start)
					break;
				else if (itms.Count == 0 || i == start)
					throw new InvalidOperationException();
				foreach (var edge in itms)
				{
					if (!ret.Edges.ContainsKey(edge.Start) || pathes.Lengths[edge.End] == pathes.Lengths[edge.Start] + 1)
					{
						if (!ret.Edges.ContainsKey(edge.Start))
							q.Enqueue(edge.Start);
						ret.AddEdge(edge);
					}
				}
			}
			return ret;
		}

		public static int Dinic(FlexGraph graph, int start, int end)
		{
			int flow = 0;
			int f = 0;
			do
			{
				f = Dinic(graph, start, end, int.MaxValue);
				flow += f;
			} while (f > 0);
			return flow;
		}

		public static int Dinic(FlexGraph graph, int start, int end, int flow)
		{
			if (start == end)
				return flow;
			if (!graph.Edges.ContainsKey(start))
				return 0;
			foreach (var item in graph.Edges[start])
			{
				var f = Dinic(graph, item.End, end, Math.Min(item.Capacity, flow));
				if (f > 0)
				{
					item.Capacity -= f;
					item.Paired.Capacity += f;
					return f;
				}
			}
			graph.RemoveVertex(start);
			return 0;
		}

		public static int FullAlg(ResidualGraph graph, int start, int end)
		{
			Pathes p = null;
			int flow = 0;
			while ((p = BFS(graph, start, end)) != null)
			{
				var fg = BuildLGraph(p, graph, start, end);
				var res = Dinic(fg, start, end);
				flow += res;
			}
			return flow;
		}


		/*
		 * Complete the crabGraphs function below.
		 */
		static int crabGraphs(int n, int tt, int[][] graph)
		{
			/*
			 * Write your code here.
			 */
			ResidualGraph residualGraph = new ResidualGraph(2 * n + 2);
			int s = 0; int t = 1;
			for (int i = 0; i < graph.Length; i++)
			{
				residualGraph.AddEdge(2 * graph[i][0], 2 * graph[i][1] + 1, 1);
				residualGraph.AddEdge(2 * graph[i][1], 2 * graph[i][0] + 1, 1);
			}
			for (int i = 1; i <= n; i++)
			{
				residualGraph.AddEdge(0, 2 * i, tt);
				residualGraph.AddEdge(2 * i + 1, 1, 1);


			}
			var res = FullAlg(residualGraph, 0, 1) ;
			return res;

		}

		public static void Main(string[] args)
		{
						int c = Convert.ToInt32(Console.ReadLine());

			for (int cItr = 0; cItr < c; cItr++)
			{
				string[] ntm = Console.ReadLine().Split(' ');

				int n = Convert.ToInt32(ntm[0]);

				int t = Convert.ToInt32(ntm[1]);

				int m = Convert.ToInt32(ntm[2]);

				int[][] graph = new int[m][];

				for (int graphRowItr = 0; graphRowItr < m; graphRowItr++)
				{
					graph[graphRowItr] = Array.ConvertAll(Console.ReadLine().Split(' '), graphTemp => Convert.ToInt32(graphTemp));
				}

				int result = crabGraphs(n, t, graph);

				Console.WriteLine(result);
			}

			
		}
	}
}
