using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoTemplates.TableDivision
{
    public class Result
    {
        const long mod = 1000000007L;
        public static long[,] multiply(long[,] m1, long[,] m2)
        {
            long[,] ret = new long[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < ret.GetLength(0); i++)
                for (int j = 0; j < ret.GetLength(1); j++)
                    for (int k = 0; k < m1.GetLength(1); k++)
                        ret[i, j] = (ret[i, j] + m1[i, k] * m2[k, j] % mod) % mod;
            return ret;
        }

        public static long[] multiplyc(long[,] m1, long[] m2)
        {
            long[] ret = new long[m1.GetLength(0)];
            for (int i = 0; i < ret.GetLength(0); i++)
                for (int k = 0; k < m1.GetLength(1); k++)
                    ret[i] = (ret[i] + m1[i, k] * m2[k] % mod) % mod;
            return ret;
        }

        public static int countDivisions(int N, int K, int M, List<int> A)
        {
            // Your code goes here...

            int sz = K * (M + 1);
            long[,] matrix = new long[sz, sz];

            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    if (j % (M + 1) <= i % (M + 1))
                        matrix[i, j] = A[K - 1 - j / (M + 1)];
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }

            long[] f = new long[sz];
            f[0] = 0;
            for (int i = 1; i < M + 1; i++)
            {
                f[i] = 1;
                Console.WriteLine($"n:{i / (M + 1)}, m:{i % (M + 1)} = {f[i]}");
            }
            for (int k = M + 1; k < sz; k++)
            {
                int ln = k / (M + 1);
                int lm = k % (M + 1);
                for (int i = 1; i <= Math.Min(ln, K); i++)
                {
                    for (int j = 1; j <= lm; j++)
                    {
                        f[k] += A[i - 1] * f[(ln - i) * (M + 1) + j];
                    }
                }
                Console.WriteLine($"n:{k / (M + 1)}, m:{k % (M + 1)} = {f[k]}");
            }

            int fn = (M + 1) * N + M;

            long end = f.Length - 1;

            if (end >= fn)
            {
                return (int)f[(fn - (end - f.Length + 1))];

            }
            for (int d = 1; d <= K; d++)
            {
                for (int k = 0; k < sz; k++)
                {
                    int ln = k / (M + 1);
                    int lm = k % (M + 1);
                    for (int i = 1; i <= Math.Min(ln, K); i++)
                    {
                        for (int j = 1; j <= lm; j++)
                        {
                            f[k] += A[i - 1] * f[(ln - i) * (M + 1) + j];
                        }
                    }
                    // Console.WriteLine($"n:{k / (M+1)}, m:{k%(M+1)} = {f[k]}");
                }
                end += M + 1;
                if (end >= fn)
                {
                    return (int)f[(fn - (end - f.Length + 1))];

                }
            }


            var op = matrix;
            while (end < fn)
            {
                op = multiply(matrix, op);
                end += M + 1;
            }
            f = multiplyc(op, f);
            return (int)f[(fn - (end - f.Length + 1))];

        }
    }

    public class Solution
    {
        public static void Main(string[] args)
        {
           
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int N = Convert.ToInt32(firstMultipleInput[0]);

            int K = Convert.ToInt32(firstMultipleInput[1]);

            int M = Convert.ToInt32(firstMultipleInput[2]);

            List<int> A = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList();

            var ans = Result.countDivisions(N, K, M, A);

            Console.WriteLine(ans);
}
    }

}
