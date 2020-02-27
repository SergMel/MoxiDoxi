using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Algorithms
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

        public static ulong Inverse(ulong u, ulong v)
        {

            ulong inv, u1, u3, v1, v3, t1, t3, q;
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
            for (int i = 1; i < simple; i++)
            {
                Debug.Assert(GCD.Euclidean(i, simple) == 1);
            }


        }
    }
}
