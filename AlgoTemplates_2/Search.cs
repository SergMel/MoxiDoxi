using System;
using System.Collections.Generic;
namespace Algorithms
{
    public static class Search
    {
        // ordered[ret] >= val
        static int lowerBound<T>(this IList<T> ordered, T val)
        {
            var lst = ordered;
            if (lst == null) throw new ArgumentNullException();

            Comparer<T> comparer = Comparer<T>.Default;

            if (lst.Count < 1) return -1;

            var l = -1;
            var r = lst.Count;

            while (r - l > 1)
            {
                var cur = (l + r) / 2;
                if (comparer.Compare(lst[cur], val) < 0)
                {
                    l = cur;
                }
                else
                {
                    r = cur;
                }

            }
            return l;
        }

        // ordered[ret] > val
        static int upperBound<T>(this IList<T> ordered, T val)
        {
            var lst = ordered;
            if (lst == null) throw new ArgumentNullException();

            Comparer<T> comparer = Comparer<T>.Default;

            if (lst.Count < 1) return -1;

            var l = -1;
            var r = lst.Count;

            while (r - l > 1)
            {
                var cur = (l + r) / 2;
                if (comparer.Compare(lst[cur], val) <= 0)
                {
                    l = cur;
                }
                else
                {
                    r = cur;
                }

            }
            return r == lst.Count ? -1 : r;
        }
    }
}