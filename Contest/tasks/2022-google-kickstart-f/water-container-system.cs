// https://codingcompetitions.withgoogle.com/kickstart/round/00000000008cb409/0000000000bef79e

// Write any using statements here
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;

static class Solution
{
    static V _dic_get_default<T, V>(this IDictionary<T, V> dic, T key)
        where V : new()
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = new V();

        return dic[key];
    }

    static void _dic_increment_long<T>(this IDictionary<T, long> dic, T key)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key]++;
    }

    static void _dic_add_long<T>(this IDictionary<T, long> dic, T key, long val)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key] += val;
    }
    const long mod = 1_000_000_007L;

    static long mod7(this long val)
    {
        return val % mod;
    }

    static void _dic_increment_int<T>(this IDictionary<T, int> dic, T key)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key]++;
    }

    static int read_int() => int.Parse(Console.ReadLine().Trim());
    static int[] read_ints() => Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
    static string[] read_strings() => Console.ReadLine().Trim().Split();
    static void write_google(int i, string s)
    {
        Console.WriteLine($"Case #{i}: {s}");
    }

    static void _dic_add_int<T>(this IDictionary<T, int> dic, T key, int val)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key] += val;
    }

    static void add_levels(int level, Dictionary<int, int> levels,
     Dictionary<int, List<int>> edges,
     int cur, int prev)
    {

        levels._dic_increment_int(level);
        foreach (var next in edges._dic_get_default(cur))
        {
            if (next == prev) continue;
            add_levels(level + 1, levels, edges, next, cur);
        }
    }

    public static void Main()
    {
        var t_count = read_int();
        for (int t = 0; t < t_count; t++)
        {
            var nq = read_ints();
            var n = nq[0];
            var q = nq[1];

            var edges = new Dictionary<int, List<int>>();
            for (int n_i = 0; n_i < n - 1; n_i++)
            {
                var ij = read_ints();
                edges._dic_get_default(ij[0]).Add(ij[1]);
                edges._dic_get_default(ij[1]).Add(ij[0]);
            }
            var levels = new Dictionary<int, int>();
            add_levels(1, levels, edges, 1, -1);
            var cum_levels = new Dictionary<int, int>();
            cum_levels._dic_add_int(1, 1);
            var max_level = levels.Keys.Max();
            for (int i = 2; i <= max_level; i++)
            {
                cum_levels._dic_add_int(i, cum_levels[i-1] + levels[i] );
            }

            int total = 0;
            int cur_level = 1;
            for (int q_i = 0; q_i < q; q_i++)
            {
                read_int();
                total ++;
                while(cur_level <= max_level && total >= cum_levels[cur_level]) {
                    cur_level++;
                }
                
            }
            write_google(t + 1, cum_levels[cur_level - 1].ToString());

        }

    }

}
