// https://codingcompetitions.withgoogle.com/kickstart/round/00000000004362d6/00000000008b44ef#problem

using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

static class Solution
{
    private static int _readInt() => int.Parse(Console.ReadLine().Trim());
    private static long _readLong() => long.Parse(Console.ReadLine().Trim());
    private static int[] _readInts() => Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
    private static long[] _readLongs() => Array.ConvertAll(Console.ReadLine().Trim().Split(), long.Parse);

    private static (int, int) _readIntTuple2()
    {
        var tmp = _readInts();
        return (tmp[0], tmp[1]);
    }

    private static (int, int, int) _readIntTuple3()
    {
        var tmp = _readInts();
        return (tmp[0], tmp[1], tmp[3]);
    }

    private static (int, int, int, int) _readIntTuple4()
    {
        var tmp = _readInts();
        return (tmp[0], tmp[1], tmp[3], tmp[4]);
    }

    public static V _readOrCreate<K, V>(this Dictionary<K, V> dic, K key, V def = default(V))
    {
        if (!dic.ContainsKey(key)) dic[key] = def;
        return dic[key];
    }

    public static V _readOrDefault<K, V>(this Dictionary<K, V> dic, K key, V def = default(V))
    {
        if (!dic.ContainsKey(key)) return def;
        return dic[key];
    }

    public static HashSet<V> _readOrCreateHS<K, V>(this Dictionary<K, HashSet<V>> dic, K key, V val)
    {
        if (!dic.ContainsKey(key)) dic[key] = new HashSet<V>();
        dic[key].Add(val);
        return dic[key];
    }

    public static void _googleOutCase(int cs, string str)
    {
        Console.WriteLine($"Case #{cs}: {str}");
    }


    public static void Main()
    {
        var cases = _readInt();


        for (int cs = 1; cs <= cases; cs++)
        {
            var (n, k) = _readIntTuple2();
            var barr = _readInts();

            long res = long.MaxValue;
            Dictionary<long, int> right = new Dictionary<long, int>();
            right[0] = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                long sm = 0;
                for (int j = i; j >= 0; j--)
                {
                    sm += barr[j];
                    if (sm <= k)
                    {
                        res = Math.Min(res, i - j + 1 + right._readOrDefault(k - sm, int.MaxValue / 2));
                    } else break; // TLE without break;
                }

                sm = 0;
                for (int j = i; j < n; j++)
                {
                    sm += barr[j];
                    if(sm > k) break;  // TLE without break;
                    right[sm] = Math.Min(right._readOrCreate(sm, int.MaxValue), j - i + 1);
                }

            }

            _googleOutCase(cs, (res >= int.MaxValue / 2 ? -1 : res).ToString());
        }
    }
}