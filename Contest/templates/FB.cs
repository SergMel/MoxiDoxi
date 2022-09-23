//https://www.metacareers.com/profile/coding_puzzles

// Write any using statements here
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;

class Solution
{


    public static void Main()
    {
        const long mod = 1_000_000_007L;
        using var f = File.OpenText("input.txt");

        var T = int.Parse(f.ReadLine());
        StringBuilder sb = new StringBuilder();
        for (int cs = 0; cs < T; cs++)
        {
            var n = int.Parse(f.ReadLine().Trim());
            var edges = Enumerable.Range(0, n).Select(el => new Dictionary<int, int>()).ToArray();
            for (int i = 1; i <= n - 1; i++)
            {
                var tmp = Array.ConvertAll(f.ReadLine().Trim().Split(), int.Parse); 
                var a = tmp[0];
                var b = tmp[1];
                var c = tmp[2];
                edges[a].Add(b, c);
                edges[b].Add(a, c);
            }

                        
            
        }
        File.WriteAllText("output.txt", sb.ToString());
    }

}
