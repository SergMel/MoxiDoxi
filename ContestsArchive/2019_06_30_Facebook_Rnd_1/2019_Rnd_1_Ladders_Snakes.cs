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


public struct Edge
{
    public int s;
    public int e;
    public int w;
}

public class WeightedItem : IComparable<WeightedItem>
{
    public int id;
    public int w;

    public int CompareTo(WeightedItem other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));

        return w - other.w;
    }

}

public class Heap<T> where T : IComparable<T>
{
    List<T> arr;
    bool isMax;

    public Heap(int n, bool isMax = true)
    {
        this.isMax = isMax;
        arr = new List<T>(n);
    }

    public Heap(bool isMax = true)
    {
        this.isMax = isMax;
        arr = new List<T>();
    }

    private void Heapify(int i)
    {
        var ch1 = 2 * i + 1;
        var ch2 = 2 * i + 2;

        if (isMax)
        {
            if (ch2 < arr.Count)
            {
                if (arr[ch2].CompareTo(arr[ch1]) < 0)
                {
                    if (arr[i].CompareTo(arr[ch1]) < 0)
                    {
                        Swap(i, ch1);
                        Heapify(ch1);
                    }
                }
                else // arr[ch2] <= arr[ch1]
                {
                    if (arr[i].CompareTo(arr[ch2]) < 0)
                    {
                        Swap(i, ch2);
                        Heapify(ch2);
                    }
                }
            }
            else if (ch1 < arr.Count && arr[ch1].CompareTo(arr[i]) > 0)
            {
                Swap(i, ch1);
                Heapify(ch1);
            }
        }
        else
        {
            if (ch2 < arr.Count)
            {
                if (arr[ch2].CompareTo(arr[ch1]) > 0)
                {
                    if (arr[i].CompareTo(arr[ch1]) > 0)
                    {
                        Swap(i, ch1);
                        Heapify(ch1);
                    }
                }
                else // arr[ch2] <= arr[ch1]
                {
                    if (arr[i].CompareTo(arr[ch2]) > 0)
                    {
                        Swap(i, ch2);
                        Heapify(ch2);
                    }
                }
            }
            else if (ch1 < arr.Count && arr[ch1].CompareTo(arr[i]) < 0)
            {
                Swap(i, ch1);
                Heapify(ch1);
            }
        }
    }

    public T Pop()
    {
        if (arr.Count < 1)
            throw new Exception("Extracting min from empty heap");
        var min = arr[0];
        arr[0] = arr[arr.Count - 1];
        arr.RemoveAt(arr.Count - 1);
        Heapify(0);
        return min;
    }

    public T Peek()
    {
        if (arr.Count < 1)
            throw new Exception("Extracting min from empty heap");
        var min = arr[0];
        return min;
    }

    public int Count()
    {
        return arr.Count;
    }

    public void Add(T val)
    {
        if (val == null)
            throw new ArgumentNullException();
        arr.Add(val);
        Up(arr.Count - 1);
    }


    private void Up(int i)
    {
        while (i > 0)
        {
            var next = (i - 1) / 2;
            if (isMax)
            {
                if (arr[next].CompareTo(arr[i]) < 0)
                {
                    Swap(i, next);
                    i = next;
                }
                else
                {
                    break;
                }
            }
            else
            {
                if (arr[next].CompareTo(arr[i]) > 0)
                {
                    Swap(i, next);
                    i = next;
                }
                else
                {
                    break;
                }
            }
        }
    }


    private void Swap(int i, int j)
    {
        var tmp = arr[i];
        arr[i] = arr[j];
        arr[j] = tmp;
    }
}


public class Graph
{
    public Dictionary<int, List<Edge>> dic;
    public List<Edge> edges;

    public Graph(int n)
    {
        dic = new Dictionary<int, List<Edge>>(n);
        edges = new List<Edge>(n);
    }

    public void AddEdge(Edge edg)
    {
        dic.GetOrAddNew(edg.e).Add(edg);
        dic.GetOrAddNew(edg.s).Add(edg);
        edges.Add(edg);
    }

    public static int Invalid = int.MinValue;

    public int DFS(int s, int e)
    {
        if (!dic.ContainsKey(s) || !dic.ContainsKey(e))
        {
            return Invalid;
        }

        Stack<int> stack = new Stack<int>();
        stack.Push(s);
        Dictionary<int, int> dist = new Dictionary<int, int>(dic.Count);
        dist[s] = 0;
        while (stack.Count > 0)
        {
            var cur = stack.Pop();
            if (cur == e)
                return dist[cur];
            foreach (var el in dic.GetOrAddNew(cur))
            {
                var next = cur == el.e ? el.s : el.e;
                if (s == next || dist.ContainsKey(next))
                {
                    continue;
                }
                dist[next] = dist[cur] + el.w;
                stack.Push(next);
            }
        }
        return Invalid;
    }


    public int Dijkstra(int s, int e)
    {
        if (!dic.ContainsKey(s) || !dic.ContainsKey(e))
        {
            return Invalid;
        }

        Heap<WeightedItem> hp = new Heap<WeightedItem>(false);
        hp.Add(new WeightedItem() { id = s, w = 0 });
        Dictionary<int, int> dist = new Dictionary<int, int>(dic.Count);
        dist[s] = 0;
        while (hp.Count() > 0)
        {
            var cur = hp.Pop();
            if (cur.id == e)
                return dist[cur.id];
            foreach (var el in dic.GetOrAddNew(cur.id))
            {
                var next = cur.id == el.e ? el.s : el.e;
                if (s == next || dist.ContainsKey(next))
                {
                    continue;
                }
                dist[next] = dist[cur.id] + el.w;
                hp.Add(new WeightedItem() { id = next, w = dist[next] });
            }
        }
        return Invalid;
    }

}

public static class Extensions
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

    public static void outputEdges(this StringBuilder sb, List<Edge> edges)
    {
        foreach (var el in edges)
        {
            sb.Append(el.s);
            sb.Append(' ');
            sb.Append(el.e);
            sb.Append(' ');
            sb.Append(el.w);
            sb.AppendLine();
        }
    }

    public static List<Edge> readWEdges(this StreamReader str, int n)
    {
        List<Edge> ret =
           new List<Edge>(n);
        for (int i = 0; i < n; i++)
        {
            var arr = str.readInts();
            ret.Add(new Edge()
            {
                s = arr[0],
                e = arr[1],
                w = arr[2]
            });
        }
        return ret;
    }

}

class UF
{
    private Dictionary<long, int> dic = new Dictionary<long, int>();
    private List<long> items = new List<long>();

    private List<int> parents;
    private List<int> ranks;
    private int N;
    public UF()
    {
        N = 0;
        parents = new List<int>();
        ranks = new List<int>();
    }

    private int GroupCount
    {
        get { return N; }
    }

    private int GetRoot(int i)
    {
        if (parents[i] == i)
            return i;
        while (parents[i] != i)
        {
            parents[i] = parents[parents[i]];
            i = parents[i];
        }
        return parents[i];
    }

    public bool IsConnected(long ii, long jj)
    {
        var i = dic[ii];
        var j = dic[jj];
        return GetRoot(i) == GetRoot(j);
    }
    public void Union(long ii, long jj)
    {

        if (!dic.ContainsKey(ii))
        {
            items.Add(ii);
            dic[ii] = items.Count - 1;
            N++;
            parents.Add(items.Count - 1);
            ranks.Add(0);
        }
        int i = dic[ii];

        if (!dic.ContainsKey(jj))
        {
            items.Add(jj);
            dic[jj] = items.Count - 1;
            N++;
            parents.Add(items.Count - 1);
            ranks.Add(0);
        }
        int j = dic[ii];

        // Console.Error.WriteLine($"i = {i}, j = {j}");
        if (i < 0 || i >= parents.Count || j < 0 || j >= parents.Count)
            throw new ArgumentOutOfRangeException();

        var ri = GetRoot(i);
        var rj = GetRoot(j);
        if (ri == rj)
            return;

        N--;
        if (ranks[ri] < ranks[rj])
        {
            parents[ri] = rj;
        }
        else if (ranks[ri] > ranks[rj])
        {
            parents[rj] = ri;
        }
        else
        {
            parents[ri] = rj;
            ranks[rj]++;
        }
    }
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
                    var nk = reader.readInts();
                    var n = nk[0];
                    var k = nk[1];
                    var str = reader.readString();
                    if (n == k)
                    {
                        sb.writeCase(ex + 1, "0");
                        continue;
                    }
                    long sum = 0;
                    int minDiff = 0;
                    
                    for (int i = n-1; i >=0; i--)
                    {
                        if (minDiff > 0)
                        {
                            minDiff = 0;
                        }
                        var cur = str[i];
                        if (cur == 'A')
                        {
                            minDiff++;
                        }
                        else
                        {
                            minDiff--;
                        }
                        if (minDiff < -k)
                        {
                            sum += pow(2, i + 1);
                            sum = sum % MOD;
                            minDiff+=2;
                        }
                    }
                    sb.writeCase(ex+1, sum.ToString());
                }
            }
        }

        File.WriteAllText("output.txt", sb.ToString());

    }

}
