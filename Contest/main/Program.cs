//https://www.metacareers.com/profile/coding_puzzles

// Write any using statements here
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

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
    static long[] read_longs() => Array.ConvertAll(Console.ReadLine().Trim().Split(), long.Parse);
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

    class SType{
        public SType(long q, long l, long v)
        {
            this.q = q;
            this.l = l;
            this.v = v;
        }
        public long q ;
        public long l;
        public long v;
    }

    public static void Main()
    {
        var t_count = read_int();
        for (int t = 0; t < t_count; t++)
        {
            var dnx = read_longs();
            var d = dnx[0];
            var n = dnx[1];
            var x = dnx[2];
            SType[] types = new SType[n];
            for (int n_i = 0; n_i < n; n_i++)
            {
                var qlv = read_longs();
                types[n_i] = new SType(qlv[0], qlv[1], qlv[2]);
            }

            types = types.OrderByDescending(el=>el.v).ToArray();
            
            BigInteger total = 0; 
            var daysleft = d;
            var current_day_left_seeds = x;
            var index = 0;
            while(index < types.Count())
            {
                var item = types[index];
                if (item.l > daysleft) {
                    index ++;
                    continue;
                }
                if (current_day_left_seeds > 0 && current_day_left_seeds < x) {
                    if (current_day_left_seeds >= item.q) {
                        current_day_left_seeds -= item.q;
                        total += BigInteger.Multiply(new BigInteger(item.q), new BigInteger(item.v));
                        if(current_day_left_seeds == 0) {
                            current_day_left_seeds = x;
                            daysleft--;
                        }    
                        index ++;                   
                        continue;
                    } else {
                        total += BigInteger.Multiply(new BigInteger(current_day_left_seeds), new BigInteger(item.v));
                        item.q -= current_day_left_seeds;
                        daysleft --;
                        continue;
                    }
                    
                }

                var days_to_full_seed = daysleft - item.l;
                if(days_to_full_seed > 0) {
                    days_to_full_seed = Math.Min(days_to_full_seed, item.q / x);
                    var non_full = item.q % x;
                    total += BigInteger.Multiply(BigInteger.Multiply(new BigInteger(days_to_full_seed), new BigInteger(x)), new BigInteger(item.v));
                    item.q -= days_to_full_seed * x;
                    daysleft -= days_to_full_seed;
                    if(item.q == 0) {
                        index ++;
                    }                    

                } else {
                    index ++;
                    continue;
                }

            }

            write_google(t + 1, total.ToString());

        }

    }

}
