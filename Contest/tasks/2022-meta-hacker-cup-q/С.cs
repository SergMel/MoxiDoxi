
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

    static void _dic_increment_int<T>(this IDictionary<T, int> dic, T key)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key]++;
    }

    static IEnumerable<(int r, int c)> _get_nbs<T>(this T[][] arr, int r, int c) {
        var r_count = arr.Length;
        if (r_count ==0 ) throw new ArgumentOutOfRangeException();
        var c_count = arr[0].Length;
        if (c_count ==0 ) throw new ArgumentOutOfRangeException();


        if(r-1 >= 0) yield return (r-1, c);
        if(c-1 >= 0) yield return (r, c-1);
        if(r+1 < r_count) yield return (r+1, c);
        if(c+1 < c_count) yield return (r, c+1); 
    }

    static IEnumerable<(int r, int c)> _get_non_blocked_nbs(this bool[][] arr, int r, int c) {
       return arr._get_nbs(r,c).Where(el => arr[el.r][el.c]);
    }

    static bool _is_cul_de_sac(this bool[][] blocked, int r, int c) {
        if(blocked[r][c]) return true;
        var b = blocked._get_nbs(r, c).Where(el => !blocked[el.r][el.c] ).Count() <= 1;
        if (b) blocked[r][c] = true;
        return blocked[r][c];
    }

    public static string to_str(char first, int v) {
        StringBuilder sb = new StringBuilder();
        sb.Append(first);
        for (int i = 0; i < 9; i++)
        {
            if (v%2 == 0) {
                sb.Append('.');
            } else {
                sb.Append('-');
            }
            v = v /2;
        }
        return sb.ToString();
    }

    public static void Main()
    {
        const long mod = 1_000_000_007L;
        using var f = File.OpenText("input.txt");

        var T = int.Parse(f.ReadLine());
        StringBuilder sb = new StringBuilder();
        for (int cs = 0; cs < T; cs++)
        {
            var n = int.Parse(f.ReadLine().Trim());
            var C1 = f.ReadLine().Trim();
            char first = '.';
            if (C1[0] == '.') {
                first = '-';
            }
            sb.AppendLine($"Case #{cs+1}:");
            for (int i = 1; i < n; i++)
            {
                sb.AppendLine(to_str(first, i));
            }

            
        }
        
        File.WriteAllText("output.txt", sb.ToString());
    }

}
