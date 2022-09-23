// https://www.facebook.com/codingcompetitions/hacker-cup/2022/round-1/problems/B2

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

    public static long calculateEuclDist(long[] a, long[] x)
    {
        Array.Sort(a);
        long n = x.Length;

        long ret = 0;
        long S = 0;
        long Sx = 0;
        for (int i = 0; i < x.Length; i++)
        {
            Sx += x[i].mod7();
            S += ((x[i].mod7() - a[0].mod7()).mod7() * (x[i].mod7() - a[0].mod7())).mod7();
            S = S.mod7();
            Sx = Sx.mod7();
        }

        ret = S;
        for (int i = 1; i < a.Length; i++)
        {
            long delta = (a[i].mod7() - a[i - 1].mod7()).mod7();
            var newS = S - (2L * (delta * Sx).mod7()).mod7() + ((2L * a[i - 1].mod7()).mod7() * (delta * n).mod7()).mod7() 
                + ((delta * delta).mod7() * n).mod7();
            newS = newS.mod7();
            ret += newS;
            ret = ret.mod7();
            S = newS;
        }
        return ret;

    }

    public static void Main()
    {
        // const long mod = 1_000_000_007L;
        using var f = File.OpenText("input.txt");

        var T = int.Parse(f.ReadLine());
        StringBuilder sb = new StringBuilder();
        for (int cs = 0; cs < T; cs++)
        {
            var n = int.Parse(f.ReadLine());

            var xts = new long[n];
            var yts = new long[n];
            for (int i = 0; i < n; i++)
            {
                var xy = Array.ConvertAll(f.ReadLine().Trim().Split(), long.Parse);
                xts[i] = xy[0];
                yts[i] = xy[1];
            }

            var q = int.Parse(f.ReadLine());

            var xp = new long[q];
            var yp = new long[q];
            for (int i = 0; i < q; i++)
            {
                var xy = Array.ConvertAll(f.ReadLine().Trim().Split(), long.Parse);
                xp[i] = xy[0];
                yp[i] = xy[1];
            }

            var res = calculateEuclDist(xp, xts);
            res += calculateEuclDist(yp, yts);
            res = res % mod;
            res = res < 0 ? mod + res : res;

            sb.Append($"Case #{cs + 1}: {res}");
            sb.AppendLine();
        }

        File.WriteAllText("output.txt", sb.ToString());
    }

}
