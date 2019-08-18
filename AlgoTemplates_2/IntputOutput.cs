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

    static StringBuilder WriteArray(StringBuilder sb, IEnumerable lst, string delimeter)
    {
        if (lst == null)
            throw new ArgumentNullException(nameof(lst));
        if (sb == null)
            throw new ArgumentNullException(nameof(sb));

        foreach (var el in lst)
        {
            sb.Append(el);
            if (delimeter != null)
                sb.Append(delimeter);
        }
        return sb;
    }

    static StringBuilder WriteObject(StringBuilder sb, object obj, string delimeter)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));
        if (sb == null)
            throw new ArgumentNullException(nameof(sb));

        sb.Append(obj);
        if (delimeter != null)
            sb.Append(delimeter);
        return sb;
    }

    static void Write(this object obj, string delimeter = " ")
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        var enm = obj as IEnumerable;
        StringBuilder sb = new StringBuilder();
        if (enm != null)
            WriteArray(sb, enm, delimeter);
        else
        {
            WriteObject(sb, obj, delimeter);
        }
        Console.Write(sb.ToString());
    }
    static void WriteLine(this object obj, string delimeter = " ")
    {
        obj.Write(delimeter);
        Console.WriteLine();
    }
    static void WriteS(params object[] obj)
    {
        obj.Write(" ");
    }
    static void WriteLineS(params object[] obj)
    {
        obj.WriteLine(" ");
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



    private static void Main2(string[] args)
    {
    }
}