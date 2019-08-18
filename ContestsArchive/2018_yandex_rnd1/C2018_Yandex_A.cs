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

class C2018_Rnd1_Yandex_A
{
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

    static void Output<T>(IEnumerable<T> lst, char delimeter = ' ')
    {
        StringBuilder sb = new StringBuilder();
        foreach (var el in lst)
        {
            sb.Append(el);
            sb.Append(delimeter);
        }
        Console.Write(sb);
    }

    static void Output<T>(T val, char delimeter = ' ')
    {
        Console.Write(val);
        Console.Write(delimeter);
    }


    static void OutputLine<T>(T val)
    {
        Console.WriteLine(val);
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

    class Team
    {
        public List<int> scores;
        public int M;
    }

    class Point : IComparable<Point>
    {
        public int teamId;
        public int costPerStep;
        public int val;
        public int CompareTo(Point other)
        {
            if (other == null) throw new ArgumentNullException();
            var diff = (this.costPerStep - other.costPerStep);
            return diff < 0 ? -1 : (diff > 0 ? 1 : 0);
        }
    }

    static void Main(string[] args)
    {
        var arr = readLongs();
        var n = arr[0];
        var k = arr[1];

        if (k == 1)
        {
            OutputLine(n);
            return;
        }
        else if (n == 1)
        {
            OutputLine(1);
            return;
        }
        else if (k >= n)
        {
            OutputLine(2);
            return;
        }

        long block = k+1;
        var cnt = n / block;
        var rem = n % block;
        var res = 2*cnt;
        if (rem == 1)
        {
            res+=1;
        }
        else if(rem > 1)
        {
            res+=2;
        }
        OutputLine(res);

    }
}