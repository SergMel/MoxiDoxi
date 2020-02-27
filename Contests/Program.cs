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
    static long modPow(long v, long p, long mod)
    {
        if (p == 0)
            return 1;
        if (p == 1)
            return v;

        long add = 1;
        while (p > 1)
        {
            if (p % 2 == 1)
            {
                add *= v;
                add %= mod;
            }
            v = (v * v) % mod;
            p = p / 2;
        }
        return (v * add) % mod;
    }


    public static List<T> GetOrAddNew<K, T>(this Dictionary<K, List<T>> dic, K key)
    {
        if (dic == null) throw new ArgumentNullException();

        return (dic[key] = dic.ContainsKey(key) ? dic[key] : new List<T>());
    }

    public static V GetValueOrDefault<K, V>(this Dictionary<K, V> dic, K key)
    {
        if (dic == null) throw new ArgumentNullException();
        if (dic.ContainsKey(key))
            return dic[key];
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


    public static void AddOrSetRemove<K>(this Dictionary<K, long> dic, K key, long add)
    {
        if (dic == null) throw new ArgumentNullException();

        dic[key] = dic.GetValueOrDefault(key) + add;

        if (dic[key] == 0)
        {
            dic.Remove(key);
        }
    }


    public static void AddOrSetRemove<K>(this Dictionary<K, int> dic, K key, int add)
    {
        if (dic == null) throw new ArgumentNullException();

        dic[key] = dic.GetValueOrDefault(key) + add;


        if (dic[key] == 0)
        {
            dic.Remove(key);
        }
    }

    public static void RemoveKeyWhen<K, V>(this Dictionary<K, V> dic, K key, V val)
    {
        if (dic == null) throw new ArgumentNullException();

        if (dic.ContainsKey(key) && object.Equals(dic[key], val))
        {
            dic.Remove(key);
        }
    }

    public static void RemoveAllWhen<K, V>(this Dictionary<K, V> dic, V val)
    {
        if (dic == null) throw new ArgumentNullException();

        var keys = dic.Keys.ToList();
        foreach (var item in keys)
        {
            if (object.Equals(dic[item], val))
            {
                dic.Remove(item);
            }
        }
    }

    public static Dictionary<T, int> ToCountDictionary<T>(this IEnumerable<T> enm)
    {
        return enm.GroupBy(el => el).ToDictionary(k => k.Key, val => val.Count());
    }

    public static Dictionary<T, int> ToIndexDictionary<T>(this IEnumerable<T> enm)
    {
        return enm.Select((el, i) => new { val = el, index = i }).ToDictionary(key => key.val, val => val.index);
    }

    static long readLong()
    {
        return long.Parse(Console.ReadLine().Trim());
    }

    static int readInt()
    {
        return int.Parse(Console.ReadLine().Trim());
    }

    static double readDouble()
    {
        return double.Parse(Console.ReadLine().Trim());
    }

    static long[] readLongs()
    {
        return Array.ConvertAll(Console.ReadLine().Trim().Split(), long.Parse);
    }

    static int[] readInts(int add = 0)
    {
        return Array.ConvertAll(Console.ReadLine().Trim().Split(), el => int.Parse(el) + add);
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

    static void Write2DimArr<T>(this T[,] arr, string delimeter = " ", string delimeter2 = "\n")
    {
        if (arr == null)
            throw new ArgumentNullException(nameof(arr));

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                WriteObject(sb, arr[i, j], delimeter);
            }
            sb.Append(delimeter2);
        }
        Console.Write(sb.ToString());
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

    static bool isLess(string cur, string str)
    {
        if (cur.Length > str.Length)
            return false;

        if (cur.Length < str.Length)
            return true;

        for (int i = 0; i < cur.Length; i++)
        {
            var curd = (int)char.GetNumericValue(cur[i]);
            var strd = (int)char.GetNumericValue(str[i]);
            if (curd < strd)
                return true;
            if (curd > strd)
                return false;
        }
        return false;
    }

    static List<long> factorial(long n, long m)
    {
        var ret = new long[n + 1];
        ret[0] = 1;
        for (long i = 1; i < n + 1; i++)
        {
            ret[i] = (ret[i - 1] * i) % m;
        }
        return ret.ToList();

    }

    private static int GetAfter(int c, List<int> lst)
    {
        var l = -1;
        var r = lst.Count;

        while (r - l > 1)
        {
            var cur = (r + l) / 2;

            if (lst[cur] <= c)
            {
                l = cur;
            }
            else
            {
                r = cur;
            }
        }
        return r;

    }




    private static string GoogleString(int t, string add)
    {
        return string.Format("Case #{0}: {1}", t, add);
    }

    private static int getPow(long v)
    {
        var cnt = 0;
        while (v > 0)
        {
            cnt++;
            v = v >> 1;
        }
        return cnt;
    }

    private static int getSteps(long v, long target)
    {
        if (target > v)
            throw new Exception();
        var cnt = 0;
        while (v > target)
        {
            cnt++;
            v = v >> 1;
        }
        return cnt;
    }

    static int findLcaLen(int[,] sparse, int[] levels, int v1, int v2, int m)
    {
        if (levels[v1] < levels[v2])
        {
            var tmp = v1;
            v1 = v2;
            v2 = tmp;
        }
        int len = 0;
        for (int i = m; i >= 0; i--)
        {
            if (levels[sparse[v1, i]] >= levels[v2])
            {
                len += (1 << i);
                v1 = sparse[v1, i];
            }
        }
        if (v1 == v2) return len;

        for (int i = m; i >= 0; i--)
        {
            if (sparse[v1, i] != sparse[v2, i])
            {
                v1 = sparse[v1, i];
                v2 = sparse[v2, i];
                len += (1 << (i + 1));
            }
        }
        return len + 2;

    }

    private static int greater(List<int> lst, int vl)
    {
        int l = -1;
        int r = lst.Count;
        while (r - l > 1)
        {
            var cur = (r + l) / 2;
            if (lst[cur] <= vl)
            {
                l = cur;
            }
            else
            {
                r = cur;
            }
        }
        return r;
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

    class Item : IComparable<Item>
    {
        public int Id;
        public int Value;
        public long Cnt;

        public int CompareTo(Item other)
        {
            return this.Value - other.Value;
        }

        public int CompareTo(object other)
        {
            return CompareTo((int)other);
        }

    }

    private static void Main(string[] args)
    {
        var n = readInt();
        var a = readLongs();
        var t = readLongs();

        var dic = new Dictionary<long, List<int>>();
        for (int i = 0; i < n; i++)
        {
            var el = a[i];
            dic.GetOrAddNew(el).Add(i);
        }
        long total = 0;
        var lst = dic.Select(el => el.Key).OrderBy(el => el).ToList();
        Heap<Item> excess = new Heap<Item>();
        long prev = -1;
        foreach (var el in lst)
        {
            if (excess.Count() < 1)
            {
                foreach (var item in dic[el])
                {
                    excess.Add(new Item
                    {
                        Id = item,
                        Cnt = el,
                        Value = (int)t[item]
                    });
                }
            }
            else
            {
                long cur = prev; ;

                while (excess.Count() > 0)
                {
                    var curItem = excess.Peek();
                    if (cur < el)
                    {
                        excess.Pop();
                        total += (cur - curItem.Cnt) * curItem.Value;
                    }
                    else
                    {
                        prev = el;
                        break;
                    }
                    cur++;
                }
                foreach (var item in dic[el])
                {
                    excess.Add(new Item()
                    {
                        Id = item,
                        Cnt = el,
                        Value = (int)t[item]
                    });
                }
            }
            prev = el;

        }
        long cur2 = prev;
        while (excess.Count() > 0)
        {
            var curItem = excess.Pop();

            total += (cur2 - curItem.Cnt) * curItem.Value;

            cur2++;

        }

        total.WriteLine();

    }
}
