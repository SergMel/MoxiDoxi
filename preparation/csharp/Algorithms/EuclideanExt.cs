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
            if (a ==0 && b == 0) {
                return (0,0,0);
            }
            else if (a == 0)
            {
                return (0, 1, b);
            }
            else if (b == 0)
            {
                return (1, 0, a);
            }

            var (q, rem) = PositiveRem(a, b);             

            var (x1, y1, gcd1) = EuclideanExt(b, rem);
            
            return  ( y1, x1 - q * y1, gcd1);
        }
    }
}