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

class C2018_Rnd1_Yandex_B
{
    public class BitSum
    {
        List<long> arr;
        public BitSum(long[] lst)
        {
            if (lst == null)
            {
                throw new ArgumentNullException(nameof(lst));
            }
            arr = Enumerable.Repeat(0L, lst.Length + 1).ToList();
            for (int i = 0; i < lst.Length; i++)
            {
                Add(i, arr[i]);
            }
        }

        public BitSum(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }
            arr = Enumerable.Repeat(0L, n + 1).ToList();
        }

        public void Add(int index, long val)
        {
            if (index < 0 || index >= arr.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            index++;
            while (index < arr.Count)
            {
                arr[index] += val;
                index = addLowestBit(index);
            }
            arr[0] += val;
        }

        public long GetSum(int index)
        {
            if (index < 0 || index >= arr.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            index++;
            long sum = 0;
            while (index > 0)
            {
                sum += arr[index];
                index = removeLowestBit(index);
            }
            return sum;
        }

        public long GetSum(int indexStart, int indexEnd)
        {
            return indexStart > 0 ? GetSum(indexEnd) - GetSum(indexStart - 1) : GetSum(indexEnd);
        }

        public int Length
        {
            get
            {
                return this.arr.Count - 1;
            }
        }

        private static int removeLowestBit(int i)
        {
            return i - (i & (-i));
        }

        private static int addLowestBit(int i)
        {
            return i + (i & (-i));
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


    public static int Euclidean(int n1, int n2)
    {
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

    static string[] readStrings()
    {
        return Console.ReadLine().Trim().Split();
    }

    static string readString()
    {
        return Console.ReadLine().Trim();
    }

    static int[] readInts()
    {
        return Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
    }

    static void Output<T>(IEnumerable<T> lst, char delimeter)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var el in lst)
        {
            sb.Append(el);
            sb.Append(delimeter);
        }
        Console.Write(sb);
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

    static int findFirstLess(List<Node> lst, long val)
    {
        if (lst.Count < 1) return -1;

        var l = -1;
        var r = lst.Count;

        while (r - l > 1)
        {
            var cur = (l + r) / 2;
            if (lst[cur].Out > val)
            {
                r = cur;
            }
            else
            {
                l = cur;
            }

        }
        return l;
    }


    class Node
    {
        public long In;
        public long Out;
    }

    static void Main(string[] args)
    {
        var msks = 1 << 10;

        var str = readString();
        List<int>[] indicies = Enumerable.Range(0, 10).Select(el => new List<int>()).ToArray();
        for (int i = 0; i < str.Length; i++)
        {
            var c = str[i];
            indicies[c - '0'].Add(i);
        }

        int[,] arr = new int[msks, str.Length + 1];
        arr[0, 0] = 1;
        for (int msk = 0; msk < msks; msk++)
        {
            for (int j = 0; j <= str.Length; j++)
            {
                HashSet<int> visited = new HashSet<int>();

                for (int i = j + 1; i <= str.Length; i++)
                {
                    var chri = str[i - 1] - '0';

                    if ((msk >> chri & 1) == 1)
                        continue;
                    if (visited.Contains(chri))
                        continue;
                    visited.Add(chri);
                    arr[msk | (1 << chri), i] += arr[msk, j];
                }

            }
        }

        var res = 0;
        for (int i = 0; i <= str.Length; i++)
        {
            res += arr[msks - 1, i];
        }
        Console.WriteLine(res);


    }

}


