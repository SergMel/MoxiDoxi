using System;
using System.Collections.Generic;
using System.Linq;

namespace Contest
{
    
    class Program
    {
        static void Reverse(int i, int j, int[] arr){
            for (int k = i; k <= i + (j - i) / 2; k++)
            {
                var tmp = arr[j - k + i];
                arr[j - k + i] = arr[k];
                arr[k] = tmp;
            }            
        }

        static void Main(string[] args)
        {        
            var queries = int.Parse(Console.ReadLine().Trim());
            for (int query = 1; query <= queries; query++){            

                var arr = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
                var N = arr[0];
                var C = arr[1];
                var mxcnt = ((N + 2) * (N-1)) / 2;
                if (C < N - 1 || C > mxcnt) {
                    Console.WriteLine($"Case #{query}: IMPOSSIBLE");
                    continue;
                }
                int[] res = Enumerable.Range(1, N).ToArray();
                int remcnt = C;
                for (int i = N-1; i >= 1; i--)
                {
                    var lowcnt = i - 1;
                    var toUse = Math.Min(remcnt - lowcnt, N - i + 1);
                    Reverse(i-1, i + toUse - 2, res);
                    remcnt -= toUse;

                }
                Console.WriteLine($"Case #{query}: {string.Join(' ', res)}");              
            }            
        }
    }
}
