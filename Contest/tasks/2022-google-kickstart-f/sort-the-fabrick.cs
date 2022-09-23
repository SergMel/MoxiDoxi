// https://codingcompetitions.withgoogle.com/kickstart/round/00000000008cb409/0000000000beefbb

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

    static int read_int() => int.Parse(Console.ReadLine().Trim());
    static int[] read_ints() => Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
    static string[] read_strings() => Console.ReadLine().Trim().Split();
    static void write_google(int i, string s) {
        Console.WriteLine($"Case #{i}: {s}");
    }
    struct Fabric{
        public Fabric(string c, int d, int u)
        {
            this.c = c;
            this.u = u;
            this.d = d;
        }
        public string c;
        public int d;
        public int u;
    }
    public static void Main()
    {
        var t_count = read_int();
        for (int t = 0; t < t_count; t++)
        {
            var n = read_int();
            Fabric[] arr1 = new Fabric[n];
            Fabric[] arr2 = new  Fabric[n];
            for (int n_i = 0; n_i < n; n_i++)
            {
                var cdu = read_strings();
                arr1[n_i] = new Fabric(cdu[0], int.Parse(cdu[1]), int.Parse(cdu[2]));
                arr2[n_i] = arr1[n_i];
            }

            arr1 = arr1.OrderBy(el=>el.c).ThenBy(el=>el.u).ToArray();
            arr2 = arr2.OrderBy(el => el.d).ThenBy(el => el.u).ToArray();
            var cnt = Enumerable.Range(0, n).Where(el=>arr1[el].u == arr2[el].u).Count();
            write_google(t+1, cnt.ToString());
        }

    }

}
