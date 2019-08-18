using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Diagnostics;
using System.Numerics;

public static class ExtensionsF
{

    public static List<T> GetOrAddNew<K, T>(this Dictionary<K, List<T>> dic, K key)
    {
        if (dic == null) throw new ArgumentNullException();

        return (dic[key] = dic.ContainsKey(key) ? dic[key] : new List<T>());
    }

    public static int readInt(this StreamReader str)
    {
        if (str == null) throw new ArgumentNullException(nameof(str));

        return int.Parse(str.ReadLine().Trim());
    }

    public static int[] readInts(this StreamReader str)
    {
        if (str == null) throw new ArgumentNullException(nameof(str));

        return Array.ConvertAll(str.ReadLine().Trim().Split(), int.Parse);
    }

    public static void writeCase(this StringBuilder sb, int id, string val)
    {
        if (sb == null) throw new ArgumentNullException();
        if (id == 0) throw new ArgumentOutOfRangeException(nameof(id));

        sb.Append("Case #");
        sb.Append(id);
        sb.Append(": ");
        sb.AppendLine(val);
    }

    public static string readString(this StreamReader str)
    {
        if (str == null) throw new ArgumentNullException(nameof(str));
        return str.ReadLine().Trim();
    }

    // public static void outputEdges(this StringBuilder sb, List<Edge> edges)
    // {
    //     foreach (var el in edges)
    //     {
    //         sb.Append(el.s);
    //         sb.Append(' ');
    //         sb.Append(el.e);
    //         sb.Append(' ');
    //         sb.Append(el.w);
    //         sb.AppendLine();
    //     }
    // }
}


class Solution
{
    const long MOD = 1000000007L;

    public static long pow(long v, long p, long mod = MOD)
    {
        if (p < 0)
            throw new ArgumentOutOfRangeException(nameof(p));
        if (p == 0)
            return 1;
        if (v == 0)
            return 0;
        if (p == 1)
        {
            return v;
        }
        var halfPow = pow(v, p / 2, mod);
        if (p % 2 == 0)
            return (halfPow * halfPow) % mod;
        else
            return (((halfPow * halfPow) % mod) * v) % mod;
    }

    static void Main(string[] args)
    {
        StringBuilder sb = new StringBuilder();
        using (var stream = new FileStream("input.txt", FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                var t = int.Parse(reader.ReadLine().Trim());

                for (int ex = 0; ex < t; ex++)
                {
                  


                }
            }
        }

        File.WriteAllText("output.txt", sb.ToString());

    }

}
