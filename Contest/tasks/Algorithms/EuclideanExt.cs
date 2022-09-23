using System;

namespace Algorithms
{
    public static class Euclidean
    {
        public static (int Quotient, int Reminder) PositiveRem(int a, int b)
        {
            if (b == 0)
            {
                throw new Exception($"{nameof(PositiveRem)}, {nameof(a)} : {a}, {nameof(b)} : {b}");
            }            

            var rem = a % b;                        

            if (rem < 0 && b < 0) {
                rem = rem - b;
            } else if (rem < 0) {
                rem = b + rem;
            }
            var q = (a - rem) / b;

            return (q, rem);
        }

        public static (int x, int y, int gcd) EuclideanExt(int a, int b)
        {
            if(a == 0 && b ==0) return (0, 0, 0);

            if (b == 0) {
                return (a >= 0 ? 1 : -1, 0, Math.Abs(a));
            }

            (int x1, int y1, int d ) = EuclideanExt(b, a%b);
            return (y1, x1 - (a/b) * y1, d);
        }
    }
}