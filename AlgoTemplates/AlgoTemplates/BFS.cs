using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoTemplates.BFS
{
	class Solution
	{

		static int[] GetDistances(int source, int n, List<int>[] edges)
		{
			Queue<int> q = new Queue<int>();
			q.Enqueue(source);
			int[] dist = Enumerable.Repeat(-1, n).ToArray();
			dist[source] = 0;
			while (q.Count > 0)
			{
				var cur = q.Dequeue();
				foreach (var nb in edges[cur])
				{
					if (dist[nb] != -1)
						continue;

					dist[nb] = dist[cur] + 6;
					q.Enqueue(nb);
				}
			}
			return dist;

		}

		static void Main(String[] args)
		{
			/* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
			var q = Convert.ToInt32(Console.ReadLine().Trim());
			for (int i = 0; i < q; i++)
			{
				string[] pr = Console.ReadLine()
					.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
				int n = Convert.ToInt32(pr[0]);
				int m = Convert.ToInt32(pr[1]);

				List<int>[] edges = Enumerable.Repeat(0, n).Select(el => new List<int>()).ToArray();
				for (int j = 0; j < m; j++)
				{
					string[] edge = Console.ReadLine()
						.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
					int st = Convert.ToInt32(edge[0].Trim()) - 1;
					int end = Convert.ToInt32(edge[1].Trim()) - 1;

					edges[st].Add(end);
					edges[end].Add(st);
				}

				var source = Convert.ToInt32(Console.ReadLine().Trim()) - 1;

				var res = GetDistances(source, n, edges);
				for (int k = 0; k < n; k++)
				{
					if (k == source)
						continue;
					Console.Write(res[k]);
					if (k != n - 1)
						Console.Write(' ');
				}
				Console.WriteLine();

			}
		}
	}
}
