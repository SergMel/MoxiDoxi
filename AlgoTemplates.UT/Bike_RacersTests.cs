using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static AlgoTemplates.Bike_Racers.Solution;

namespace AlgoTemplates.UT
{
    class Bike_RacersTests
    {
		public static void Test()
		{
			BipartiteGraph graph = new BipartiteGraph();
			graph.AddEdge(2, 24);
			graph.AddEdge(7, 26);
			graph.AddEdge(7, 37);
			graph.AddEdge(8, 39);
			graph.AddEdge(11, 24);
			graph.AddEdge(11, 36);
			graph.AddEdge(12, 21);
			graph.AddEdge(13, 28);
			graph.AddEdge(13, 29);
			graph.AddEdge(16, 28);
			var flow = CalculateFlow(graph);
			Debug.Assert(flow == 7);

		}
	}
}
