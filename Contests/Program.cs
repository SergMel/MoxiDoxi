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
using static IO;


public static class IO
{
    public static long readLong()
    {
        return long.Parse(Console.ReadLine().Trim());
    }

    public static int readInt()
    {
        return int.Parse(Console.ReadLine().Trim());
    }

    public static double readDouble()
    {
        return double.Parse(Console.ReadLine().Trim());
    }

    public static long[] readLongs()
    {
        return Array.ConvertAll(Console.ReadLine().Trim().Split(), long.Parse);
    }

    public static int[] readInts(int add = 0)
    {
        return Array.ConvertAll(Console.ReadLine().Trim().Split(), el => int.Parse(el) + add);
    }

    public static string[] readStrings()
    {
        return Console.ReadLine().Trim().Split();
    }

    public static string readString()
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


    public static void Write(this object obj, string delimeter = " ")
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        var str = obj as string;
        var enm = obj as IEnumerable;
        StringBuilder sb = new StringBuilder();

        if (str == null && enm != null)
            WriteArray(sb, enm, delimeter);
        else
        {
            WriteObject(sb, obj, delimeter);
        }
        Console.Write(sb.ToString());
    }

    public static void WriteLine(this object obj, string delimeter = " ")
    {
        obj.Write(delimeter);
        Console.WriteLine();
    }
}

public static class NumTh
{
    public static List<long> getAllDivisors(long val)
    {
        List<long> lst = new List<long>();
        var v = 2;
        while (v * v <= val)
        {
            if (val % v == 0)
            {
                lst.Add(v);
            }
            v++;
        }
        return lst;
    }

    public static Dictionary<long, int> getPrimeDivisors(long val)
    {
        var res = new Dictionary<long, int>();
        int cnt = 0;
        while (val % 2 == 0)
        {
            val /= 2;
            cnt++;
        }
        if (cnt > 0)
        {
            res[2] = cnt;
        }

        var v = 3;
        while (v * v <= val)
        {
            if (val % v != 0)
            {
                continue;
            }
            cnt = 0;
            while (val % v == 0)
            {
                cnt++;
                val /= v;
            }
            res[v] = cnt;
            v += 2;
        }
        return res;
    }

    #region Euclidian

    public static int Euclidean(int n1, int n2)
    {
        n1 = Math.Abs(n1);
        n2 = Math.Abs(n2);

        if (n1 == n2) return n1;
        else if (n1 < n2)
        {
            var tmp = n1;
            n1 = n2;
            n2 = tmp;
        }

        while (n1 % n2 > 0)
        {
            var tmp = n2;
            n2 = n1 % n2;
            n1 = tmp;
        }
        return n2;
    }

    public static long Euclidean(long n1, long n2)
    {
        n1 = Math.Abs(n1);
        n2 = Math.Abs(n2);

        if (n1 == n2) return n1;
        else if (n1 < n2)
        {
            var tmp = n1;
            n1 = n2;
            n2 = tmp;
        }

        while (n1 % n2 > 0)
        {
            var tmp = n2;
            n2 = n1 % n2;
            n1 = tmp;
        }
        return n2;
    }


    public static BigInteger Euclidean(BigInteger n1, BigInteger n2)
    {
        n1 = BigInteger.Abs(n1);
        n2 = BigInteger.Abs(n2);

        if (n1 == n2) return n1;
        else if (n1 < n2)
        {
            var tmp = n1;
            n1 = n2;
            n2 = tmp;
        }

        while (n1 % n2 > 0)
        {
            var tmp = n2;
            n2 = n1 % n2;
            n1 = tmp;
        }
        return n2;
    }

    #endregion Euclidian

    #region Euclidean extended

    public static long Inverse(this long a, long m)
    {
        var (inv, y, d) = EuclideanExt(a, m);
        if (d != 1)
        {
            throw new Exception("No inverse value");
        }

        return inv < 0 ? m + inv : inv;
    }

    public static BigInteger Inverse(this BigInteger a, BigInteger m)
    {
        var (inv, y, d) = EuclideanExt(a, m);
        if (d != 1)
        {
            throw new Exception("No inverse value");
        }
        return inv < 0 ? m + inv : inv;
    }

    public static int Inverse(this int a, int m)
    {
        var (inv, y, d) = EuclideanExt(a, m);
        if (d != 1)
        {
            throw new Exception("No invers value");
        }
        return inv < 0 ? m + inv : inv;
    }

    // x * a + y * b = gcd
    public static (BigInteger, BigInteger, BigInteger) EuclideanExt(BigInteger a, BigInteger b)
    {
        var signa = a.Sign;
        var signb = b.Sign;

        bool reverse = false;

        a = BigInteger.Abs(a);
        b = BigInteger.Abs(b);

        if (a > b)
        {
            reverse = true;
            (a, b) = (b, a);
        }

        var (m11, m12, m21, m22) = (new BigInteger(1L), new BigInteger(0L), new BigInteger(0L), new BigInteger(1L));
        while (a > 0)
        {
            var tmp = b / a;
            (a, b) = (b % a, a);
            (m11, m12, m21, m22) = (m12 - m11 * tmp, m11, m22 - m21 * tmp, m21);
        }
        var (x, y, d) = (m12, m22, b);
        if (reverse)
        {
            (x, y) = (y, x);
        }
        return (signa * x, signb * y, d);
    }

    // x * a + y * b = gcd
    public static (long, long, long) EuclideanExt(long a, long b)
    {
        var signa = Math.Sign(a);
        var signb = Math.Sign(b);

        bool reverse = false;

        a = Math.Abs(a);
        b = Math.Abs(b);

        if (a > b)
        {
            reverse = true;
            (a, b) = (b, a);
        }

        var (m11, m12, m21, m22) = (1L, 0L, 0L, 1L);
        while (a > 0)
        {
            Console.WriteLine($"{m11} {m12} {m21} {m22}");
        
            var tmp = b / a;
            Console.WriteLine($"{b} {a}");
            (a, b) = (b % a, a);
            Console.WriteLine($"{b} {a}");
            (m11, m12, m21, m22) = (m12 - m11 * tmp, m11, m22 - m21 * tmp, m21);
        }
        var (x, y, d) = (m12, m22, b);
        if (reverse)
        {
            (x, y) = (y, x);
        }
        return (signa * x, signb * y, d);
    }

    // x * a + y * b = gcd
    public static (int, int, int) EuclideanExt(int a, int b)
    {
        var signa = Math.Sign(a);
        var signb = Math.Sign(b);

        a = Math.Abs(a);
        b = Math.Abs(b);

        bool reverse = false;

        if (a > b)
        {
            reverse = true;
            (a, b) = (b, a);
        }
        var (m11, m12, m21, m22) = (1, 0, 0, 1);
        while (a > 0)
        {
            var tmp = b / a;
            (a, b) = (b % a, a);
            (m11, m12, m21, m22) = (m12 - m11 * tmp, m11, m22 - m21 * tmp, m21);
        }
        var (x, y, d) = (m12, m22, b);
        if (reverse)
        {
            (x, y) = (y, x);
        }
        return (signa * x, signb * y, d);
    }

    #endregion Euclidean extended
}


public static class Mod
{
    static long MOD = 2;
    public static void SetMod(long mod)
    {
        MOD = mod;
    }

    public static long modPow(this long v, long p, long mod)
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

    public static long modMultiply(this long v, long v2, long mod)
    {
        return ((v % mod) * (v2 % mod)) % mod;
    }

    public static long modDivide(this long v, long v2, long mod)
    {
        return (NumTh.Inverse(v2, mod) * (v % mod)) % mod;
    }

    public static long modInverse(this long v, long mod)
    {
        return NumTh.Inverse(v, mod);
    }

    public static long modAdd(this long v, long v2, long mod)
    {
        return ((v % mod) + (v2 % mod)) % mod;
    }

    public static long modSubtract(this long v, long v2, long mod)
    {
        return ((v % mod) - (v2 % mod)) % mod;
    }

    public static long modPow(this long v, long p)
    {
        return v.modPow(v, MOD);
    }

    public static long modMultiply(this long v, long v2)
    {
        return v.modMultiply(v2, MOD);
    }

    public static long modDivide(this long v, long v2)
    {
        return v.modDivide(v2, MOD);
    }

    public static long modInverse(this long v)
    {
        return NumTh.Inverse(v, MOD);
    }

    public static long modAdd(this long v, long v2)
    {
        return v.modAdd(v2, MOD);
    }

    public static long modSubtract(this long v, long v2)
    {
        return v.modSubtract(v2, MOD);
    }
}

public static class Dic
{
    public static List<T> GetOrAddNew<K, T>(this Dictionary<K, List<T>> dic, K key)
    {
        if (dic == null) throw new ArgumentNullException();

        return (dic[key] = dic.ContainsKey(key) ? dic[key] : new List<T>());
    }

    public static HashSet<T> GetOrAddNewHS<K, T>(this Dictionary<K, HashSet<T>> dic, K key)
    {
        if (dic == null) throw new ArgumentNullException();

        return (dic[key] = dic.ContainsKey(key) ? dic[key] : new HashSet<T>());
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

    public static Dictionary<T, List<int>> ToIndiciesDictionary<T>(this IEnumerable<T> enm)
    {
        return enm.Select((el, i) => new { val = el, index = i }).GroupBy(el => el.val).
            ToDictionary(key => key.Key, val => val.Select(v => v.index).ToList());
    }
}

public static class Graph
{
    public static Dictionary<int, HashSet<int>> readGraphHs(int m)
    {
        Dictionary<int, HashSet<int>> res = new Dictionary<int, HashSet<int>>();
        for (int i = 0; i < m; i++)
        {
            var arr = readInts();
            res.GetOrAddNewHS(arr[0]).Add(arr[1]);
            res.GetOrAddNewHS(arr[1]).Add(arr[0]);
        }
        return res;
    }

    public static Dictionary<int, List<int>> readGraph(int m)
    {
        Dictionary<int, List<int>> res = new Dictionary<int, List<int>>();
        for (int i = 0; i < m; i++)
        {
            var arr = readInts();
            res.GetOrAddNew(arr[0]).Add(arr[1]);
            res.GetOrAddNew(arr[1]).Add(arr[0]);
        }
        return res;
    }
}

static class SolutionTemplate
{



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

    static List<int> getDeviders(int val)
    {
        int i = 2;
        List<int> res = new List<int>();
        while (i <= val)
        {
            while (val % i == 0)
            {
                res.Add(i);
            }
            i++;
        }
        return res;
    }

    static long[] factorials;

    static void precompute(int n)
    {
        factorials = new long[n + 1];
        factorials[0] = 1;
        for (int i = 1; i <= n; i++)
        {
            factorials[i] = Mod.modMultiply(factorials[i - 1], i);
        }
    }

    private static void Main(string[] args)
    {
        Mod.modInverse(2, 998244353L).WriteLine();
    }

}
