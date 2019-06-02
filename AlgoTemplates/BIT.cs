using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    // binary index tree or Fenwik tree
    public class BitSum
    {
        List<long> arr;
        public BitSum(long[] lst)
        {
            if (lst == null)
            {
                throw new ArgumentNullException(nameof(lst));
            }
            arr = Enumerable.Repeat(0L, lst.Length + 1).ToList();
            for (int i = 0; i < lst.Length; i++)
            {
                Add(i, arr[i]);
            }
        }

        public void Add(int index, long val)
        {
            if (index < 0 || index >= arr.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            index++;
            while (index < arr.Count )
            {
                arr[index] += val;
                index = addLowestBit(index);
            }
            arr[0] += val;
        }

        public long GetSum(int index)
        {
            if (index < 0 || index >= arr.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            index++;
            long sum = 0;
            while (index > 0)
            {
                sum += arr[index];
                index = removeLowestBit(index);
            }
            return sum;
        }

        public long GetSum(int indexStart, int indexEnd)
        {
            return indexStart > 0 ? GetSum(indexEnd) - GetSum(indexStart - 1) : GetSum(indexEnd);
        }



        private static int removeLowestBit(int i)
        {
            return i - (i & (-i));
        }

        private static int addLowestBit(int i)
        {
            return i + (i & (-i));
        }


    }

    public static class BitUT
    {

        public static void Tests()
        {
            Test1();

        }

        static void Test1()
        {
            var arr = new long[] { };
            BitSum bit = new BitSum(arr);
            try
            {
                bit.GetSum(0);
                Debug.Assert(false, "No exception");
            }
            catch
            { }

            arr = new long[] { 0 };
            bit = new BitSum(arr);
            Debug.Assert(bit.GetSum(0) == 0);
            bit.Add(0, 1);
            Debug.Assert(bit.GetSum(0) == 1);
            bit.Add(0, 1);
            Debug.Assert(bit.GetSum(0) == 2);

            arr = new long[] { 0, 0 };
            bit = new BitSum(arr);
            Debug.Assert(bit.GetSum(0) == 0);
            Debug.Assert(bit.GetSum(1) == 0);
            bit.Add(1, 1);
            Debug.Assert(bit.GetSum(0) == 0);
            Debug.Assert(bit.GetSum(1) == 1);
            bit.Add(0, 1);
            Debug.Assert(bit.GetSum(0) == 1);
            Debug.Assert(bit.GetSum(1) == 2);
            bit.Add(0, 3);
            bit.Add(1, 2);
            Debug.Assert(bit.GetSum(0) == 4);
            Debug.Assert(bit.GetSum(1) == 7);


            arr = new long[] { 1, 2 };
            bit = new BitSum(arr);
            Debug.Assert(bit.GetSum(0) == 0);


        }

    }
}