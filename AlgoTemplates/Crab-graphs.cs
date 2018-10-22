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

    class Crab_graphs
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
			while(q.Count > 0 )
			{
				var itm = q.Dequeue();
				if (pathLength[end] > -1 && pathLength[end] < curLength)
					break;

				var nbrs = graph.Edges[itm]?.Where(el=>el.Capacity > 0)??Enumerable.Empty<Edge>();
				foreach (var item in nbrs)
				{
					if (pathLength[item.End] >=0)
						continue;
					pathLength[item.End] = pathLength[item.Start] + 1;
					curLength = pathLength[item.End];
					(prev[item.End] = prev[item.End] ?? new List<Edge>()).Add(item);
					q.Enqueue(item.End);
				}
			}
			return pathLength[end] == -1?(Pathes)null: new Pathes
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
					if (!ret.Edges.ContainsKey(edge.Start) && pathes.Lengths[edge.End] == pathes.Lengths[edge.Start] + 1)
					{
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
				f = Dinic(graph, start, end);
				flow += f;
			} while (f > 0);
			return flow;	
		}

		public static int Dinic(FlexGraph graph, int start, int end, int flow )
		{
			if (start == end)
				return flow;
			if (!graph.Edges.ContainsKey(start))
				return 0;
			foreach (var item in graph.Edges[start])
			{
				var f = Dinic(graph, item.End, end, Math.Min(item.Capacity, flow));
				if (f > 0)
					return f;
			}
			graph.RemoveVertex(start);
			return 0;
		}
    }
}
