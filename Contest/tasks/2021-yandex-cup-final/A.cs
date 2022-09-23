// https://contest.yandex.ru/contest/30228/problems/
// Write any using statements here
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

class Solution
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

    // public static V _readOrCreate<K, V>(this Dictionary<K, V> dic, K key, V def = default(V))
    // {
    //     if (!dic.ContainsKey(key)) dic[key] = def;
    //     return dic[key];
    // }

    // public static HashSet<V> _readOrCreateHS<K, V>(this Dictionary<K, HashSet<V>> dic, K key, V val)
    // {
    //     if (!dic.ContainsKey(key)) dic[key] = new HashSet<V>();
    //     dic[key].Add(val);
    //     return dic[key];
    // }


    public static bool ArrayEquals(int[] arr, int st1, int st2, int len)
    {
        for (int j = st1; j < st1 + len; j++)
        {
            if (arr[j] != arr[j - st1 + st2]) return false;
        }
        return true;
    }

    public static long GetHC(int[] arr, int st, int en, int k)
    {
        long res = 0;
        for (int i = st; i <= en; i++)
        {
            res += (res) * k + arr[i] - 1;
        }
        return res;
    }

    static Random rnd = new Random(DateTime.Now.Millisecond);
    static void Test()
    {
        // for (int n = 1; n < 1000; n+=100)
        // {
        //     Console.WriteLine(n);
        //     for (int k = 1; k < 1000; k+=100)
        //     {
                // Console.WriteLine($"i: {i}");
                // var n = rnd.Next(900_000, 1000_000);
                // var k = rnd.Next(900_000, 1000_000);
                var n = 10000;
                var k = 1000_000;
                var arr = Enumerable.Range(0, n).Select(el => rnd.Next(1, k+1)).ToArray();

                var sol1 = Solve(n, k, arr);
                var sol2 = bf(n, k, arr);
                if (sol1 != (sol2.Item1, sol2.Item2) || sol1.Item1 > n || sol1.Item2 > n)
                {
                    Console.WriteLine($"{n} {k}");
                    Console.WriteLine(string.Join(' ', arr));
                    Console.WriteLine(sol1);
                    Console.WriteLine((sol2.Item1, sol2.Item2));
                    Console.WriteLine(string.Join(' ',sol2.Item3.ToArray()));
                }
        //     }
        // }
    }

    public static IEnumerable<List<int>> Add(List<int> arr, int cnt, int k)
    {
        if (cnt == 0) yield return arr.ToList();
        else
        {
            var res = Add(arr, cnt - 1, k);
            foreach (var lst in res)
            {
                for (int i = 1; i <= k; i++)
                {
                    var ret = lst.ToList();
                    ret.Add(i);
                    yield return ret;
                }
            }

        }

    }

    public static bool CheckPref(int[] arr, List<int> pref)
    {
        if(arr.Length == pref.Count) return false;
        for (int i = 0; i < arr.Length - pref.Count; i++)
        {
            bool ne = false;
            for (int j = 0; j < pref.Count; j++)
            {
                if (pref[j] != arr[i + j])
                {
                    ne = true;
                    break;
                }
            }
            if (!ne) return true;

        }
        return false;
    }

    public static (int, int, List<int>) bf(int n, int k, int[] arr)
    {
        for (int cnt = 1; cnt <= n; cnt++)
        {
            for (int toadd = 0; toadd <= cnt; toadd++)
            {
                var pref = arr.TakeLast(cnt-toadd).ToList();
                var v = Add(pref, toadd, k);
                var res = v.Where(el => !CheckPref(arr, el)).FirstOrDefault();
                if (res != null)

                    return (cnt, toadd, res);
            }

        }

        throw new Exception();
    }


    public static (int, int) Solve(int n, int k, int[] arr)
    {
        var last = arr.Last();
        if (k == 1)
        {
            // Console.WriteLine($"{n+1} {n}");
            return (n, 0);
        }
        if (n == 1)
        {
            // Console.WriteLine($"{n+1} {n}");
            return (1, 0);
        }
        if (arr.Where(el => el == last).Count() == 1)
        {
            // Console.WriteLine($"1 0");
            return (1, 0);
        }

        var tcnt = n - 1;

        for (int cnt = 1; cnt <= tcnt; cnt++)
        {
            HashSet<long>[] dist = Enumerable.Range(0, cnt + 1).Select(el => new HashSet<long>()).ToArray();

            // index to start partern matching
            for (int st = 0; st < n - cnt; st++)
            {
                var pattern_start = st;
                var pattern_end = st + cnt - 1;

                // count to include to already equals ranges
                for (int i = 1; i <= cnt; i++)
                {
                    var equal_pattern_start = st;
                    var equal_pattern_end = st + i - 1;
                    var non_equal_pattern_start = st + i;
                    var non_equal_pattern_end = pattern_end;

                    if (equal_pattern_end == n - 1) continue;

                    if (!ArrayEquals(arr, arr.Length - i, equal_pattern_start, i)) continue;

                    var hashcode = GetHC(arr, non_equal_pattern_start, non_equal_pattern_end, k);
                    dist[cnt - i].Add(hashcode);
                }
                dist[cnt].Add(GetHC(arr, pattern_start, pattern_end, k));
            }
            var pw = 1L;
            for (int i = 0; i < dist.Length; i++)
            {
                if (dist[i].Count() < pw)
                {
                    //Console.WriteLine($"{cnt} {i}");
                    return (cnt, i);
                }
                pw *= k;
            }
        }
        return (n, 0);

    }

    public static void Main()
    {
        var (n, k) = _readIntTuple2();
        var arr = _readInts();
        var v2 = Solve(n, k, arr);

        Console.WriteLine($"{v2.Item1} {v2.Item2}");
        // Test();
    }
}
