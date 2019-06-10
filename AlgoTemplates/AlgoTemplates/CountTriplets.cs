using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoTemplates
{
    public class CountTriplets
    {

        // Complete the countTriplets function below.
        static long countTriplets(List<long> arr, long r)
        {
            Dictionary<long, long> counts = new Dictionary<long, long>();
            Dictionary<long, long> duplets = new Dictionary<long, long>();
            long ret = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                long el = arr[i];
                if (el % r == 0)
                {
                    long prev = el / r;
                    if (duplets.ContainsKey(prev))
                        ret += duplets[prev];

                    if (counts.ContainsKey(prev))
                        if (duplets.ContainsKey(el))
                            duplets[el] += counts[prev];
                        else
                            duplets[el] = counts[prev];
                    else
                        duplets[el] = 0;

                }

                if (counts.ContainsKey(el))
                    counts[el]++;
                else
                    counts[el] = 1;                                             
            }
            return ret;
        }

        public static void Main(string[] args)
        {

            string[] nr = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(nr[0]);

            long r = Convert.ToInt64(nr[1]);

            List<long> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt64(arrTemp)).ToList();

            long ans = countTriplets(arr, r);

            Console.WriteLine(ans);
        }
    }
}
