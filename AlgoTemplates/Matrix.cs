using System;
using System.Collections.Generic;
using System.Text;
// https://www.hackerrank.com/challenges/matrix/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=graphs
namespace AlgoTemplates.Matrix
{

	public class Solution
	{
		class Node
		{
			public int id;
			public int value;
			public List<Node> children = new List<Node>();
		}

		static void AddEdge(Dictionary<int, List<Tuple<int, int>>> dic, int v1, int v2, int w)
		{
			dic[v1] = dic.ContainsKey(v1) ? dic[v1] : new List<Tuple<int, int>>();
			dic[v2] = dic.ContainsKey(v2) ? dic[v2] : new List<Tuple<int, int>>();

			dic[v1].Add(Tuple.Create(v2, w));
			dic[v2].Add(Tuple.Create(v1, w));
		}

		static Node BuildTree(int[][] roads)
		{
			Dictionary<int, List<Tuple<int, int>>> dic =
				new Dictionary<int, List<Tuple<int, int>>>();
			foreach (var lst in roads)
			{
				AddEdge(dic, lst[0], lst[1], lst[2]);
			}

			return BuildTree(dic);

		}

		static HashSet<int> BuldMashines(int[] machines)
		{
			HashSet<int> hs = new HashSet<int>(machines);

			return hs;
		}

		static Node BuildTree(Dictionary<int, List<Tuple<int, int>>> dic)
		{
			Node root = new Node
			{
				id = 0
			};
			Queue<Node> q = new Queue<Node>();
			q.Enqueue(root);
			HashSet<int> visited = new HashSet<int>();
			visited.Add(0);
			while (q.Count > 0)
			{
				var cur = q.Dequeue();
				foreach (var tpl in dic[cur.id])
				{
					if (visited.Contains(tpl.Item1))
						continue;
					visited.Add(tpl.Item1);
					Node nd = new Node { id = tpl.Item1, value = tpl.Item2 };
					cur.children.Add(nd);
					q.Enqueue(nd);

				}
			}
			return root;

		}

		static int findMinTime(Node nd, HashSet<int> machines, ref long cum)
		{
			if (machines.Contains(nd.id))
			{
				foreach (var el in nd.children)
				{
					long tmp = 0;
					cum += findMinTime(el, machines, ref tmp);
					cum += tmp;
				}
				return nd.value;
			}
			else
			{
				long sum = 0;
				int max = 0;
				foreach (var el in nd.children)
				{
					int val = findMinTime(el, machines, ref cum);
					sum += val;
					if (val > max)
					{
						max = val;
					}
				}
				cum += (sum - max);
				return Math.Min(max, nd.value);
			}

		}

		// Complete the minTime function below.
		static long minTime(int[][] roads, int[] machines)
		{
			Node root = BuildTree(roads);
			var hs = BuldMashines(machines);
			long res = 0;
			findMinTime(root, hs, ref res);
			return res;

		}

		public static void Main(string[] args)
		{
		
			string[] nk = Console.ReadLine().Split(' ');

			int n = Convert.ToInt32(nk[0]);

			int k = Convert.ToInt32(nk[1]);

			int[][] roads = new int[n - 1][];

			for (int i = 0; i < n - 1; i++)
			{
				roads[i] = Array.ConvertAll(Console.ReadLine().Split(' '), roadsTemp => Convert.ToInt32(roadsTemp));
			}

			int[] machines = new int[k];

			for (int i = 0; i < k; i++)
			{
				int machinesItem = Convert.ToInt32(Console.ReadLine());
				machines[i] = machinesItem;
			}

			var result = minTime(roads, machines);

			Console.WriteLine(result);

		}
	}
}
