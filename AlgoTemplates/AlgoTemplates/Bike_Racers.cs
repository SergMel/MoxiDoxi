using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AlgoTemplates.Bike_Racers
{
	public class Solution
	{
		public class Edge
		{
			public int Left { get; set; }
			public int Right { get; set; }
			public bool Matched { get; set; }
		}

		public class BipartiteGraph
		{
			public Dictionary<int, List<Edge>> Edges { get; set; } = new Dictionary<int, List<Edge>>();
	public HashSet<int> Left { get; set; } = new HashSet<int>();
			public HashSet<int> All { get; set; } = new HashSet<int>();

			public int VCount => Edges.Count;

			public IEnumerable<int> GetLeftUnmached()
			{
				foreach (var item in Left)
				{
					if (Edges[item].All(el=>!el.Matched))
						yield return item;
				}
			}

			public bool IsRightUnMatched(int v)
			{
				return !Left.Contains(v) && !GetAdjEdges(v).Any();
			}

			public IEnumerable<Edge> GetAdjEdges(int i)
			{
				bool left = Left.Contains(i);
				return left ? Edges[i].Where(el => !el.Matched): Edges[i].Where(el => el.Matched);
			}

			public void ReverseMatching(Edge edge)
			{
				edge.Matched = !edge.Matched;
			}

			public void AddEdge(int left, int right)
			{
				Edge edge = new Edge()
				{
					Left = left,
					Right = right,
				};
				if (!Edges.ContainsKey(left))
					Edges[left] = new List<Edge>();
				if (!Edges.ContainsKey(right))
					Edges[right] = new List<Edge>();
				Edges[left].Add(edge);
				Edges[right].Add(edge);
				Left.Add(left);
				All.Add(left);
				All.Add(right);
			}

			public IEnumerable<Edge> GetMatchedEdges()
			{
				foreach (var item in Left)
				{
					foreach (var edge in Edges[item].Where(el=>el.Matched))
					{
						yield return edge;
					}
				}
			}
		}

		public static Dictionary<int, int> BuildPath(BipartiteGraph graph)
		{
			Dictionary<int, int> dist = graph.All.ToDictionary(el => el, v=>-1);

			Queue<int> q = new Queue<int>();
			foreach (var item in graph.GetLeftUnmached())
			{
				q.Enqueue(item);
				dist[item] = 0;
			}
			bool existsPath = false;
			while (q.Count > 0)
			{
				var item = q.Dequeue();
				foreach (var edge in graph.GetAdjEdges(item))
				{
					var nextV = edge.Left == item ? edge.Right : edge.Left;
					if (dist[nextV] == -1)
					{
						dist[nextV] = dist[item] + 1;
						existsPath = existsPath ? existsPath : graph.IsRightUnMatched(nextV);
						if (!existsPath)
						{
							q.Enqueue(nextV);
						}
					}
				}
			}
			if (existsPath)
				return dist;

			return null;
		}

		public static int Augment(int v, BipartiteGraph graph, Dictionary<int, int> dist)
		{
			if (graph.IsRightUnMatched(v))
				return 1;

			var curDist = dist[v];

			foreach (var item in graph.GetAdjEdges(v))
			{
				var nextI = item.Left == v ? item.Right : item.Left;
				if (dist[nextI] - dist[v] == 1)
				{
					var flow = Augment(nextI, graph, dist);
					if (flow == 1)
					{
						graph.ReverseMatching(item);
						return 1;
					}
				}
			}
			return 0;
		}

		public static int Augment(BipartiteGraph graph, Dictionary<int, int> dist)
		{
			int totalFlow = 0;
			foreach (var item in graph.GetLeftUnmached())
			{
				totalFlow += Augment(item, graph, dist);

			}
			return totalFlow;
		}

		public static int CalculateFlow(BipartiteGraph grapth)
		{
			var flow = 0;
			var dist = BuildPath(grapth);
			while(dist != null)
			{
				flow += Augment(grapth, dist);
				dist = BuildPath(grapth);
			}
			return flow;
		}

		/*
     * Complete the bikeRacers function below.
     */
		static long bikeRacers(int[][] bikers, int[][] bikes, long k)
		{
			int bsi = bikers.Length;
			double b = 1;
			BipartiteGraph graph;
			double t = -1;

			List<double> lst = new List<double>();
			for (int i = 0; i < bikers.Length; i++)
			{
				for (int j = 0; j < bikes.Length; j++)
				{
					long x = bikers[i][0] - bikes[j][0];
					long y = bikers[i][1] - bikes[j][1];
					var dist = x * x + y * y;
					lst.Add(dist);
					if (t <= dist)
						t = dist;
				}
			}
			lst.Sort();
			double eps = double.MaxValue;
			for (int i = 1; i < lst.Count; i++)
			{
				if (eps > lst[i] - lst[i - 1] && lst[i] - lst[i - 1] != 0)
					eps = lst[i] - lst[i - 1];
			}
			double cur = (t + b) / 2;
			do
			{
				graph = new BipartiteGraph();
				for (int i = 0; i < bikers.Length; i++)
				{
					for (int j = 0; j < bikes.Length; j++)
					{
						long x = bikers[i][0] - bikes[j][0];
						long y = bikers[i][1] - bikes[j][1];
						double dist = x * x + y * y;
						if (dist <= cur )
						{
							graph.AddEdge(i, j + bsi);
						}
					}
				}
				var val = CalculateFlow(graph);
				if (graph.VCount - graph.Left.Count < k || graph.Left.Count < k || val < k)
				{
					b = cur;
					if ( t - cur  <= eps)
					{
						cur = t;
						continue;
					}
				}
				else
				{
					if (cur - b <= eps)
						break;
					t = cur;
				}
				cur = (b + t) / 2;

			} while (true);

			long res = 0;
			foreach (var item in graph.GetMatchedEdges())
			{
				long x = bikers[item.Left][0] - bikes[item.Right - bsi][0];
				long y = bikers[item.Left][1] - bikes[item.Right - bsi][1];
				res = Math.Max(res,  x * x + y * y);
			}
			return res;

		}

		public static void Main(string[] args)
		{
			string[] nmk = Console.ReadLine().Split(' ');

			int n = Convert.ToInt32(nmk[0]);

			int m = Convert.ToInt32(nmk[1]);

			int k = Convert.ToInt32(nmk[2]);

			int[][] bikers = new int[n][];

			for (int bikersRowItr = 0; bikersRowItr < n; bikersRowItr++)
			{
				bikers[bikersRowItr] = Array.ConvertAll(Console.ReadLine().Split(' '), bikersTemp => Convert.ToInt32(bikersTemp));
			}

			int[][] bikes = new int[m][];

			for (int bikesRowItr = 0; bikesRowItr < m; bikesRowItr++)
			{
				bikes[bikesRowItr] = Array.ConvertAll(Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), bikesTemp => Convert.ToInt32(bikesTemp));
			}

			long result = bikeRacers(bikers, bikes, k);

			Console.WriteLine(result);

		}
	}
}
