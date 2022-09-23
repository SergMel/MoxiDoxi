// https://www.geeksforgeeks.org/multiplicative-inverse-under-modulo-m/
using System;
using System.Linq;

namespace Algorithms
{

    public static class MultiplicativeInverse
    {
        private static (int, int, int d) euclideanExt (int a, int b) {           

            if(a == 0 && b ==0) return (0, 0, 0);

            if (b == 0) {
                return (a >= 0 ? 1 : -1, 0, Math.Abs(a));
            }

            (int x1, int y1, int d ) = euclideanExt(b, a%b);
            return (y1, x1 - (a/b) * y1, d);

        }
  
        public static int inverse(int v, int m) {
            if(m <= 0) throw new ArgumentException(nameof(m));

            (int x, int y, int d) = euclideanExt(v, m);
            if (d != 1) throw new Exception("non coprime");

            if (x < 0) {
                return x + m;
            }
            return x;
        }        
    }
}