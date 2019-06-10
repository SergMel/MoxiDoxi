using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    public class GCD
    {
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

        public static ulong Inverse(ulong a, ulong n)
        {
            ulong x, y = 0;
            EuclideanExt(a, n, out x, out y);
            return x;
        }

        private static ulong EuclideanExt(ulong n1, ulong n2, out ulong x, out ulong y)
        {
            x = 0;
            y = 1;

            bool r = false;
            if (n1 == n2) return n1;

            else if (n1 < n2)
            {
                r = true;
                var tmp = n1;
                n1 = n2;
                n2 = tmp;
            }
            ulong q = 0;
            ulong xl = 1;
            ulong yl = 0;

            while (n2 > 0)
            {
                q = n1 / n2;

                var tmp = n2;
                n2 = n1 % n2;
                n1 = tmp;

                tmp = x;
                x = xl - q * x;
                xl = tmp;

                tmp = y;
                y = yl - q * y;
                yl = tmp;


            }
            x = xl;
            y = yl;
            if (r)
            {
                x = yl;
                y = xl;
            }
            return x * n1 + y * n2;
        }
    }

    public static class GCDUT
    {
        public static void Tests()
        {
            Test1();

        }

        static void Test1()
        {
            Debug.Assert(GCD.Euclidean(2, 3) == 1);
            Debug.Assert(GCD.Euclidean(2, 2) == 2);
            Debug.Assert(GCD.Euclidean(1, 1) == 1);
            Debug.Assert(GCD.Euclidean(1, 2) == 1);
            Debug.Assert(GCD.Euclidean(15, 10) == 5);
            Debug.Assert(GCD.Euclidean(15, 10) == 5);
            int simple = 1000000007;
            for(int i = 1; i < simple;i++)
            {
                Debug.Assert(GCD.Euclidean(i, simple) == 1);
            }
            

        }
    }
}
