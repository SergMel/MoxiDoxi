// https://codeforces.com/gym/101745
// https://contest.yandex.ru/contest/7636/standings/
// https://codeforces.com/gym/101745/attachments/download/6759/statements.pdf
// Editorial: https://codeforces.com/blog/entry/58135


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
using System.Numerics;

static class SolutionTemplate
{
    public static List<T> GetOrAddNew<K, T>(this Dictionary<K, List<T>> dic, K key)
    {
        if (dic == null) throw new ArgumentNullException();

        return (dic[key] = dic.ContainsKey(key) ? dic[key] : new List<T>());
    }

    public static V GetValueOrDefault<K, V>(this Dictionary<K, V> dic, K key)
    {
        if (dic == null) throw new ArgumentNullException();

        if (dic.ContainsKey(key)) return dic[key];
        return default(V);
    }
    public static void AddOrSet<K>(this Dictionary<K, long> dic, K key, long add)
    {
        if (dic == null) throw new ArgumentNullException();

        dic[key] = dic.GetValueOrDefault(key) + add;
    }

    public static void AddOrSet<K>(this Dictionary<K, int> dic, K key, int add)
    {
        if (dic == null) throw new ArgumentNullException();

        dic[key] = dic.GetValueOrDefault(key) + add;
    }

    public static int AddExt<T>(this HashSet<T> hs, T val)
    {
        if (hs == null) throw new ArgumentNullException();

        if (!hs.Contains(val)) hs.Add(val);
        return hs.Count();
    }

    static long readLong()
    {
        return long.Parse(Console.ReadLine().Trim());
    }

    static int readInt()
    {
        return int.Parse(Console.ReadLine().Trim());
    }

    static long[] readLongs()
    {
        return Array.ConvertAll(Console.ReadLine().Trim().Split(), long.Parse);
    }

    static int[] readInts()
    {
        return Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
    }

    static string[] readStrings()
    {
        return Console.ReadLine().Trim().Split();
    }

    static string readString()
    {
        return Console.ReadLine().Trim();
    }

    public static int readInt(this StreamReader str)
    {
        if (str == null) throw new ArgumentNullException();
        return int.Parse(str.ReadLine().Trim());
    }


    public static int[] readInts(this StreamReader str)
    {
        if (str == null) throw new ArgumentNullException();
        return Array.ConvertAll(str.ReadLine().Trim().Split(), int.Parse);
    }


    static bool OutputList<T>(IEnumerable<T> lst, char delimeter = ' ')
    {
        StringBuilder sb = new StringBuilder();
        foreach (var el in lst)
        {
            sb.Append(el);
            sb.Append(delimeter);
        }
        Console.Write(sb);
        return true;
    }

    static bool OutputLine<T>(IEnumerable<T> lst, char delimeter = ' ')
    {
        StringBuilder sb = new StringBuilder();
        foreach (var el in lst)
        {
            sb.Append(el);
            sb.Append(delimeter);
        }
        Console.WriteLine(sb);
        return true;
    }

    static bool Output<T>(T val, char delimeter = ' ')
    {
        Console.Write(val);
        Console.Write(delimeter);
        return true;
    }
    static bool OutputLine<T>(T val)
    {
        Console.WriteLine(val);
        return true;
    }


    static List<Tuple<int, int>> readEdges(int n)
    {
        List<Tuple<int, int>> res = new List<Tuple<int, int>>(n);
        for (int i = 0; i < n; i++)
        {
            var arr = readInts();
            res.Add(Tuple.Create(arr[0], arr[1]));
        }
        return res;
    }


    static Dictionary<int, List<Tuple<int, int>>> readWDicEdges(int n)
    {
        Dictionary<int, List<Tuple<int, int>>> dic =
            new Dictionary<int, List<Tuple<int, int>>>(n);
        for (int i = 0; i < n - 1; i++)
        {
            var arr = readInts();
            var tpl = Tuple.Create(arr[0], arr[1], arr[2]);
            dic[tpl.Item1] = dic.ContainsKey(tpl.Item1) ? dic[tpl.Item1] : new List<Tuple<int, int>>();
            dic[tpl.Item2] = dic.ContainsKey(tpl.Item2) ? dic[tpl.Item2] : new List<Tuple<int, int>>();

            dic[tpl.Item1].Add(Tuple.Create(tpl.Item2, tpl.Item3));
            dic[tpl.Item2].Add(Tuple.Create(tpl.Item1, tpl.Item3));

        }
        return dic;
    }


    static List<Tuple<int, int>> readWEdges(int n)
    {
        List<Tuple<int, int>> res = new List<Tuple<int, int>>(n);
        for (int i = 0; i < n; i++)
        {
            var arr = readInts();
            res.Add(Tuple.Create(arr[0], arr[1]));
        }
        return res;
    }

    static Dictionary<int, List<int>> readDicEdges(int n)
    {
        Dictionary<int, List<int>> dic =
            new Dictionary<int, List<int>>(n);

        for (int i = 0; i < n - 1; i++)
        {
            var arr = readInts();
            var tpl = Tuple.Create(arr[0], arr[1]);
            dic[tpl.Item1] = dic.ContainsKey(tpl.Item1) ? dic[tpl.Item1] : new List<int>();
            dic[tpl.Item2] = dic.ContainsKey(tpl.Item2) ? dic[tpl.Item2] : new List<int>();

            dic[tpl.Item1].Add(tpl.Item2);
            dic[tpl.Item2].Add(tpl.Item1);
        }
        return dic;
    }

    private static Random rnd = new Random();
    private const int symbCnt = 'z' - 'a' + 1;
    private static Tuple<char[], char[]> getString()
    {
        int n = rnd.Next(3, 151);
        int k = rnd.Next(3, n + 1);

        char[] stamp = new char[k];
        for (int i = 0; i < k; i++)
        {
            stamp[i] = (char)('a' + rnd.Next(0, Math.Min(k, 'z' - 'a')));
        }

        HashSet<int> hs = new HashSet<int>(Enumerable.Range(0, n));

        char[] ret = new char[n];
        for (int i = 0; (i < n - k + 1) || hs.Count() > 0; i++)
        {
            var s = rnd.Next(0, n - k + 1);
            for (int j = 0; j < k; j++)
            {
                ret[s + j] = stamp[j];
                if (hs.Contains(s + j))
                    hs.Remove(s + j);
            }
        }

        return Tuple.Create(ret, stamp);
    }

    private static void experiment()
    {
        do
        {
            var exper = getString();
            var str = new string(exper.Item1);
            var stamp = new string(exper.Item2);

            if (str.Length == 1)
            {
                Console.WriteLine(str);
                return;
            }

            List<int> firstI = new List<int>();
            List<int> lastI = new List<int>();

            for (int i = 0; i < str.Length; i++)
            {
                var el = str[i];

                if (el == str[0]) firstI.Add(i);
                if (el == str[str.Length - 1]) lastI.Add(i);
            }

            List<Tuple<int, int>> variants = new List<Tuple<int, int>>();
            foreach (var left in firstI)
            {
                foreach (var right in lastI)
                {
                    if (right < left)
                    {
                        continue;
                    }
                    variants.Add(Tuple.Create(left, right));
                }
            }

            lastI = lastI.OrderByDescending(el => el).ToList();

            List<string> res = new List<string>();
            var visited = new HashSet<string>();
            bool found = false;
            foreach (var el in variants)
            {
                var s = el.Item1;
                var e = el.Item2;

                var sb = str.Substring(s, e - s + 1);
                var ls = 0;
                foreach (var item in firstI)
                {
                    if (s - ls <= 1) break;
                    if (item - ls > 1) break;

                    for (int i = 0; i < Math.Min(sb.Length, s - item); i++)
                    {
                        if (str[item + i] != sb[i]) break;
                        ls = Math.Max(item + i, ls);
                    }
                }
                if (s - ls > 1) continue;

                var le = str.Length - 1;
                foreach (var item in lastI)
                {
                    if (le - e <= 1) break;
                    if (le - item > 1) break;

                    for (int i = 0; i < Math.Min(sb.Length, item - e); i++)
                    {
                        if (str[item - i] != sb[sb.Length - 1 - i]) break;
                        le = Math.Min(item - i, le);
                    }
                }
                if (le - e <= 1)
                {
                    if (visited.Contains(sb))
                        continue;
                    if (sb == stamp)
                    {
                        found = true;
                        break;
                    }
                    visited.Add(sb);

                }
            }
            if (found) continue;
            else
            {
                Console.WriteLine(stamp);
                Console.WriteLine(str);
                break;
            }

        } while (true);
    }

    private static void Main2(string[] args)
    {
        experiment();
    }

    private static void Main(string[] args)
    {
        var str = readString();

        if (str.Length == 1) { Console.WriteLine(str); return; }

        List<int> firstI = new List<int>();
        List<int> lastI = new List<int>();

        for (int i = 0; i < str.Length; i++)
        {
            var el = str[i];

            if (el == str[0]) firstI.Add(i);
            if (el == str[str.Length - 1]) lastI.Add(i);
        }

        List<Tuple<int, int>> variants = new List<Tuple<int, int>>();
        foreach (var left in firstI)
        {
            foreach (var right in lastI)
            {
                if (right < left)
                {
                    continue;
                }
                variants.Add(Tuple.Create(left, right));
            }
        }

        lastI = lastI.OrderByDescending(el => el).ToList();

        List<string> res = new List<string>();
        var visited = new HashSet<string>();
        foreach (var el in variants)
        {
            var s = el.Item1;
            var e = el.Item2;

            var sb = str.Substring(s, e - s + 1);
            if (visited.Contains(sb))
                continue;
            else
                visited.Add(sb);
            bool[,] dp = new bool[str.Length, sb.Length];
            dp[0, 0] = sb[0] == str[0];

            for (int i = 1; i < str.Length; i++)
            {
                for (int j = 0; j < sb.Length; j++)
                {
                    if (str[i] != sb[j])
                        continue;
                    if (j == 0)
                        dp[i, j] = Enumerable.Range(0, sb.Length).Select(index => dp[i - 1, index]).FirstOrDefault(ee => ee);
                    else
                        dp[i, j] = dp[i - 1, j - 1] || dp[i - 1, sb.Length - 1];

                }
            }
            if (dp[str.Length - 1, sb.Length - 1])
                res.Add(sb);
        }
        res.Sort(StringComparer.Ordinal);
        OutputList(res, '\n');
    }
}