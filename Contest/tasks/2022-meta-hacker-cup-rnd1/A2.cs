// https://www.facebook.com/codingcompetitions/hacker-cup/2022/round-1/problems/A2
// A1

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

        dic[key]+=val;
    }

    private static List<int> BuildLps(int[] p)
    {
        List<int> lps = new List<int>();
        int i = 1;
        int len = 0;
        lps.Add(0);

        while (i < p.Length)
        {
            if (p[i] == p[len])
            {
                i++;
                len++;
                lps.Add(len);
            }
            else
            {
                if (len > 0)
                {
                    len = lps[len - 1];
                }
                else
                {
                    i++;
                    lps.Add(len);
                }
            }
        }
        return lps;
    }
    public static List<int> FindAllPatterns(int[] p, int[] s)
    {
        if (s == null || p == null) throw new Exception();
        if (p.Length > s.Length) return new List<int>();

        if (p.Length == 0 || s.Length == 0) return new List<int>();
        List<int> ret = new List<int>();
        var lps = BuildLps(p);
        int j = 0;
        int i = 0;
        while (i < s.Length)
        {
            if (p[j] == s[i])
            {
                i++;
                j++;
            }

            if (j == p.Length)
            {
                ret.Add(i - j);
                j = lps[j - 1];
            }
            else if (i < s.Length && p[j] != s[i])
            {
                if (j != 0)
                {
                    j = lps[j - 1];
                }
                else
                {
                    i = i + 1;
                }

            }
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
            var nk = Array.ConvertAll(f.ReadLine().Trim().Split(), int.Parse);
            var n = nk[0];
            var k = nk[1];

            var A = Array.ConvertAll(f.ReadLine().Trim().Split(), int.Parse);
            var B = Array.ConvertAll(f.ReadLine().Trim().Split(), int.Parse);

            var extB = B.Concat(B).ToArray();

            var pts = FindAllPatterns(A, extB);
            pts = pts.Select(el => el % A.Length).Distinct().ToList();
            var exists = pts.Count > 0;
            var hasZero = pts.Any(el => el ==0);
            var hasNonZero = pts.Any(el => el != 0);
            if( !exists ||
                k == 0 && !hasZero ||
                k == 1 && !hasNonZero ||
                n == 2 && !hasZero && k % 2 == 0 ||
                n == 2 && !hasNonZero && k % 2 == 1
                ) {
                sb.Append($"Case #{cs + 1}: NO");
            } else {
                sb.Append($"Case #{cs + 1}: YES");
            }
            sb.AppendLine();
        }
        
        File.WriteAllText("output.txt", sb.ToString());
    }

}
