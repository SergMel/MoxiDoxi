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
    public class GCD
    {
        public static BigInteger Euclidean(BigInteger n1, BigInteger n2)
        {
            if (n1 < 1 || n2 < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

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

        public static int Euclidean(int n1, int n2)
        {
            if (n1 < 1 || n2 < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

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

        public static long Inverse(long u, long v)
        {

            long inv, u1, u3, v1, v3, t1, t3, q;
            int iter;
            /* Step X1. Initialise */
            u1 = 1;
            u3 = u;
            v1 = 0;
            v3 = v;
            /* Remember odd/even iterations */
            iter = 1;
            /* Step X2. Loop while v3 != 0 */
            while (v3 != 0)
            {
                /* Step X3. Divide and "Subtract" */
                q = u3 / v3;
                t3 = u3 % v3;
                t1 = u1 + q * v1;
                /* Swap */
                u1 = v1; v1 = t1; u3 = v3; v3 = t3;
                iter = -iter;
            }
            /* Make sure u3 = gcd(u,v) == 1 */
            if (u3 != 1)
                return 0;   /* Error: No inverse exists */
                            /* Ensure a positive result */
            if (iter < 0)
                inv = v - u1;
            else
                inv = u1;
            return inv;
        }

        public static BigInteger Inverse(BigInteger u, BigInteger v)
        {

            BigInteger inv, u1, u3, v1, v3, t1, t3, q;
            int iter;
            /* Step X1. Initialise */
            u1 = 1;
            u3 = u;
            v1 = 0;
            v3 = v;
            /* Remember odd/even iterations */
            iter = 1;
            /* Step X2. Loop while v3 != 0 */
            while (v3 != 0)
            {
                /* Step X3. Divide and "Subtract" */
                q = u3 / v3;
                t3 = u3 % v3;
                t1 = u1 + q * v1;
                /* Swap */
                u1 = v1; v1 = t1; u3 = v3; v3 = t3;
                iter = -iter;
            }
            /* Make sure u3 = gcd(u,v) == 1 */
            if (u3 != 1)
                return 0;   /* Error: No inverse exists */
                            /* Ensure a positive result */
            if (iter < 0)
                inv = v - u1;
            else
                inv = u1;
            return inv;
        }

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
    static List<int>[] graphArr(int n)
    {
        return Enumerable.Range(0, n).Select(el => new List<int>()).ToArray();
    }

    static List<int>[] readEdgesArr(int n, int m, bool directed = false, int add = 0)
    {
        var gr = graphArr(n);
        for (int i = 0; i < m; i++)
        {
            var arr = readInts();
            gr[arr[0] + add].Add(arr[1] + add);
            if (!directed) gr[arr[1] + add].Add(arr[0] + add);
        }
        return gr;
    }

    static List<int>[] readUndEdgesArr(int n, int m, int add = 0)
    {
        return readEdgesArr(n, m, directed: false, add: add);
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

    static long[] powcache;

    static void fillCahce(int n)
    {
        powcache = new long[n + 1];
        powcache[0] = 1;
        for (int i = 1; i <= n; i++)
        {
            powcache[i] = (powcache[i - 1] * inverse) % mod;
        }
    }


    const long mod = 1000000007L;
    const long inverse = 500000004L;

    private static int LessOrEqual(List<int> arr, int val)
    {
        if (arr.Count == 0)
        {
            return -1;
        }
        int l = -1;
        int r = arr.Count;
        while (r - l > 1)
        {
            var cur = (l + r) / 2;
            if (arr[cur] <= val)
            {
                l = cur;
            }
            else
            {
                r = cur;
            }
        }

        return l;
    }

    static List<long> primeFactors(long n)
    {
        HashSet<long> ret = new HashSet<long>();

        while (n % 2 == 0)
        {
            if (ret.Count == 0)
                ret.Add(2);
            n = n / 2;
        }

        for (int i = 3; i * i <= n; i = i + 2)
        {

            while (n % i == 0)
            {
                if (!ret.Contains(i))
                    ret.Add(i);
                n = n / i;
            }
        }
        if (n > 2)
            ret.Add(n);
        return ret.ToList();
    }

    private static long pow(long v, long p, long mod)
    {
        if (p == 0)
            return 1;
        if (p == 1)
            return v % mod;
        v = v % mod;
        var vv = ((v * v) % mod);
        var rr = pow(vv, p / 2, mod);

        if (p % 2 == 0)
        {
            return rr;
        }
        return (rr * v) % mod;
    }

    class State
    {
        public int PlusIndex;
        public int MinusIndex;

        public long minD = long.MinValue;
        public long maxD = long.MaxValue;
    }

    private static void Main(string[] args)
    {
        var n = readInt();
        var arr = readLongs();

        Stack<State> st = new Stack<State>();
        State st1 = new State
        {
            PlusIndex = -1,
            MinusIndex = -1,

        };

        st.Push(st1);
        long d = -1;
        while (st.Count > 0)
        {
            var cur = st.Pop();
            var nextI = Math.Max(cur.MinusIndex, cur.PlusIndex) + 1;

            if (nextI >= arr.Length)
            {
                if (cur.minD <= cur.maxD)
                {
                    if (cur.minD >= 0)
                    {
                        d = cur.minD;
                        break;

                    }
                    else
                    {
                        d = 0;
                        break;
                    }

                }
                continue;
            }

            var nextV = arr[nextI];
            var pV = cur.PlusIndex == -1 ? long.MinValue : arr[cur.PlusIndex];
            var mV = cur.MinusIndex == -1 ? long.MaxValue : arr[cur.MinusIndex];
            if (pV < nextV)
            {
                var nexState = new State()
                {
                    PlusIndex = nextI,
                    MinusIndex = cur.MinusIndex,
                    minD = cur.minD,
                    maxD = Math.Min(cur.maxD, mV == long.MaxValue ? long.MaxValue : nextV - 1 + mV)
                };

                if (nexState.minD <= nexState.maxD)
                {
                    st.Push(nexState);
                }
            }
            if (mV > nextV)
            {
                var nexState = new State()
                {
                    PlusIndex = cur.PlusIndex,
                    MinusIndex = nextI,
                    minD = Math.Max(cur.minD == long.MinValue ? nextV : cur.minD, pV == long.MinValue ? long.MinValue : nextV + 1 + pV),
                    maxD = cur.maxD
                };

                if (nexState.minD <= nexState.maxD)
                {
                    st.Push(nexState);
                }
            }

        }
        if (d == -1)
        {
            (-1).WriteLine();
            return;

        }
        d.WriteLine();
        long prev = -1;
        foreach (var el in arr)
        {
            if (prev == -1)
            {
                if ((d - el) >= 0)
                    prev = Math.Min(el, d - el);
                else
                    prev = el;
            }
            else
            {
                if (prev < el && prev < d - el)
                {
                    prev = Math.Min(el, d - el);
                }
                else if (prev < el)
                {
                    prev = el;
                }
                else
                {
                    prev = d - el;
                }
            }
            prev.Write();
        }



    }

}

