using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoTemplates.MaximumSubarraySum
{
    using System.CodeDom.Compiler;
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

   public class Solution
    {

        // Complete the maximumSum function below.
        static long maximumSum(long[] a, long m)
        {
            // Console.WriteLine(Math.Log10(long.MaxValue));
            List<long> sums = new List<long>((int)Math.Min((long)a.Length, m));
            sums.Add(a[0] % m);
            long ret = sums[0];
            Dictionary<long, Tuple<int, int>> dic = new Dictionary<long, Tuple<int, int>>(a.Length);
            dic[sums[0]] = Tuple.Create(0, 0);
            // Console.WriteLine(a.Take(14).Select(el=>el%m).Sum()%m);
            // Console.WriteLine(a.Take(126).Select(el=>el%m).Sum()%m);
            long sm = sums[0];
            for (int i = 1; i < a.Length; i++)
            {
                // val:18936041, bval:18936342
                // val:125:125
                // bval:13:13
                var nextsum = (sm + a[i] % m) % m;
                sm = nextsum;
                if (dic.ContainsKey(nextsum))
                {
                    if (dic[nextsum].Item1 > i)
                    {
                        dic[nextsum] = Tuple.Create(i, dic[nextsum].Item2);
                    }
                    else if (dic[nextsum].Item2 < i)
                    {
                        dic[nextsum] = Tuple.Create(dic[nextsum].Item1, i);
                    }
                }
                else
                {
                    sums.Add(nextsum);
                    dic[nextsum] = Tuple.Create(i, i);

                    ret = Math.Max(ret, nextsum);
                    // if (ret == 20136553 || ret == 20135061)
                    // Console.WriteLine(ret);
                    if (ret == m - 1)
                        return ret;
                }

            }
            sums.Sort();

            for (int i = sums.Count - 2; i >= 0; i--)
            {
                long val = sums[i];
                for (int j = i + 1; j < sums.Count; j++)
                {
                    var bval = sums[j];
                    if (ret >= val - bval + m)
                        break;

                    if (dic[val].Item2 > dic[bval].Item1)
                    {

                        //if (val-bval+m == 5)
                        //{
                        //  Console.WriteLine($"val:{val}, bval:{bval}");
                        //}
                        ret = Math.Max(ret, val - bval + m);
                        //if (val-bval+m == 20136553 || val-bval+m == 20135061)
                        //{
                        //  Console.WriteLine(ret);
                        // Console.WriteLine($"val:{val}, bval:{bval}");
                        //Console.WriteLine($"val:{dic[val].Item1}:{dic[val].Item2}");
                        //Console.WriteLine($"bval:{dic[bval].Item1}:{dic[bval].Item2}");
                        //}

                        if (ret == m - 1) { return ret; }
                        break;
                    }
                }
            }
            return ret;

        }

        public static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@"C:\Personal\tmp\Output.txt", false);
            TextReader reader = new StreamReader(@"C:\Personal\tmp\Input.txt");
            int q = Convert.ToInt32(reader.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string[] nm = reader.ReadLine().Split(' ');

                int n = Convert.ToInt32(nm[0]);

                long m = Convert.ToInt64(nm[1]);

                long[] a = Array.ConvertAll(reader.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries ), aTemp => Convert.ToInt64(aTemp))
                ;
                long result = maximumSum(a, m);
                textWriter.WriteLine(result);
                //Console.WriteLine(result);
            }

            textWriter.Flush();
            textWriter.Close();
        }
    }

}
