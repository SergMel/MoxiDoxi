using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AlgoTemplates.Crab_graphs;

namespace AlgoTemplates.UT
{
    static class Crab_graphsTests
    {
		public static void BFSTest()
		{
			ResidualGraph graph = new ResidualGraph(2);
			graph.AddEdge(0, 1, 1);
			var res = Crab_graphs.Crab_graphs.BFS(graph, 0, 1);
			Debug.Assert(res != null);
			Debug.Assert(res.Lengths[1] == 1);
			Debug.Assert(res.Lengths[0] == 0);

			graph = new ResidualGraph(4);
			graph.AddEdge(0, 1, 1);
			graph.AddEdge(0, 2, 1);
			graph.AddEdge(1,3, 1);
			graph.AddEdge(2, 3, 1);
			res = Crab_graphs.Crab_graphs.BFS(graph, 0, 3);
			Debug.Assert(res != null);
			Debug.Assert(res.Preious[3].Count == 2);
			Debug.Assert(res.Preious[2].Count == 1);
			Debug.Assert(res.Preious[1].Count == 1);
			Debug.Assert(res.Preious[0].Count == 0);
		}
	}
    
}
