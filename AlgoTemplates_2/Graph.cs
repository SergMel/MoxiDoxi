using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    public class Graph
    {

        static List<int> buildPath(int[] prev, int s, int e)
        {
            List<int> ret = new List<int>();
            var cur = e;
            ret.Add(e);

            while (prev[cur] != cur)
            {
                cur = prev[cur];
                ret.Add(cur);
            }
            ret.Reverse();
            return ret;
        }

        static List<int> bfs(List<int>[] gr, int s, int e)
        {
            if (e == s)
                return new List<int> { s };

            Queue<int> q = new Queue<int>();
            Queue<int> steps = new Queue<int>();
            HashSet<int> v = new HashSet<int>();
            int[] prev = new int[gr.Length];


            prev[s] = s;
            v.AddExt(s);
            q.Enqueue(s);
            steps.Enqueue(0);
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                var cursteps = steps.Dequeue();
                foreach (var el in gr[cur])
                {
                    if (v.Contains(el))
                        continue;
                    v.Add(el);
                    q.Enqueue(el);
                    steps.Enqueue(cursteps + 1);

                    prev[el] = cur;
                    if (el == e)
                        return buildPath(prev, s, e);
                }
            }
            return null;
        }

    }
}