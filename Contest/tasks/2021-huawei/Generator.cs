using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

public class Generator
{

    Random rnd = new Random(DateTime.Now.Millisecond);

    public IEnumerable<string> GenerateLines(int n, int m, int constraintCount, int flowCount, int maxCapacity, int maxFlow)
    {
        
        var edges = new List<(int, int, int, int, int, int)>();
        
        yield return $"{n} {m} {constraintCount} {flowCount}";
        
        Dictionary<int, List<int>> nodeToEdges = new Dictionary<int, List<int>>();

        for (int i = 0; i < m; i++)
        {
            var v = rnd.Next(n);
            var u = 0;
            do
            {
                u = rnd.Next(n);
            } while (v == u);

            var distance = 1;
            var capacity = rnd.Next(maxCapacity - 1) + 1;
            edges.Add((i, i, u, v, distance, capacity));

            if (!nodeToEdges.ContainsKey(u)) nodeToEdges[u] = new List<int>();
            if (!nodeToEdges.ContainsKey(v)) nodeToEdges[v] = new List<int>();
            nodeToEdges[u].Add(i);
            nodeToEdges[v].Add(i);

            yield return $"{i} {i} {u} {v} {distance} {capacity}";
        }

        var constrains = new List<(int, int, int)>();

        var nodeIds = nodeToEdges.Where(el => el.Value.Count >= 2).Select(el => el.Key).ToList();

        for (int i = 0; i < constraintCount; i++)
        {
            var nodeId = nodeIds[rnd.Next(nodeIds.Count)];
            var lst = nodeToEdges[nodeId];

            var id1 = rnd.Next(lst.Count);
            var id2 = id1;
            do
            {
                id2 = rnd.Next(lst.Count);
            } while (id1 == id2);
            constrains.Add((nodeId, id1, id2));

            yield return $"{nodeId} {id1} {id2}";
        }

        var flows = new List<(int, int, int, int)>();
        for (int i = 0; i < flowCount; i++)
        {
            var v = rnd.Next(n);
            var u = 0;
            do
            {
                u = rnd.Next(n);
            } while (v == u);

            var flow = rnd.Next(maxFlow - 1) + 1;

            flows.Add((i, u, v, flow));

            yield return  $"{i} {u} {v} {flow}";

        }
    }

    public void Generate(int n, int m, int constraintCount, int flowCount, int maxCapacity, int maxFlow)
    {

        StringBuilder sb = new StringBuilder();
        var edges = new List<(int, int, int, int, int, int)>();
        
        sb.AppendLine($"{n} {m} {constraintCount} {flowCount}");
        
        Dictionary<int, List<int>> nodeToEdges = new Dictionary<int, List<int>>();

        for (int i = 0; i < m; i++)
        {
            var v = rnd.Next(n);
            var u = 0;
            do
            {
                u = rnd.Next(n);
            } while (v == u);

            var distance = 1;
            var capacity = rnd.Next(maxCapacity - 1) + 1;
            edges.Add((i, i, u, v, distance, capacity));

            if (!nodeToEdges.ContainsKey(u)) nodeToEdges[u] = new List<int>();
            if (!nodeToEdges.ContainsKey(v)) nodeToEdges[v] = new List<int>();
            nodeToEdges[u].Add(i);
            nodeToEdges[v].Add(i);

            sb.AppendLine($"{i} {i} {u} {v} {distance} {capacity}");
        }

        var constrains = new List<(int, int, int)>();

        var nodeIds = nodeToEdges.Where(el => el.Value.Count >= 2).Select(el => el.Key).ToList();

        for (int i = 0; i < constraintCount; i++)
        {
            var nodeId = nodeIds[rnd.Next(nodeIds.Count)];
            var lst = nodeToEdges[nodeId];

            var id1 = rnd.Next(lst.Count);
            var id2 = id1;
            do
            {
                id2 = rnd.Next(lst.Count);
            } while (id1 == id2);
            constrains.Add((nodeId, id1, id2));

            sb.AppendLine($"{nodeId} {id1} {id2}");
        }

        var flows = new List<(int, int, int, int)>();
        for (int i = 0; i < flowCount; i++)
        {
            var v = rnd.Next(n);
            var u = 0;
            do
            {
                u = rnd.Next(n);
            } while (v == u);

            var flow = rnd.Next(maxFlow - 1) + 1;

            flows.Add((i, u, v, flow));

            sb.AppendLine($"{i} {u} {v} {flow}");

        }

        File.WriteAllText("testdata.txt", sb.ToString());

    }

}