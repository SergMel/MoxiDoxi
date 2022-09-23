using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{

    [TestClass]
    public class LazySegmentTreeUT
    {
        Random rnd = new Random((int)(DateTime.Now.Ticks % 10000007));
        struct Operation
        {
            public int v;
            public int i;
            public int j;
        }
        private (List<long>, List<Operation>) GenerateTest(int maxN, int maxV, int OperationCount)
        {
            var n = rnd.Next(1, maxN);
            var lst = new List<long>(n);
            for (int i = 0; i < n; i++)
            {
                var v = rnd.Next(-maxV, maxV);
                lst.Add(v);
            }
            var ops = new List<Operation>(OperationCount);

            for (int i = 0; i < OperationCount; i++)
            {
                var v = rnd.Next(-maxV, maxV);
                var qs = rnd.Next(0, n - 1);
                var qe = rnd.Next(0, n - 1);
                if (qs > qe) (qs, qe) = (qe, qs);
                ops.Add(new Operation
                {
                    v = v,
                    i = qs,
                    j = qe
                });
            }

            return (lst, ops);
        }



        private bool Check(List<long> arr, MinLazySegmentTree mqr)
        {
            if (arr.Count != mqr.Count)
            {
                throw new Exception();
            }

            for (int i = 0; i < arr.Count; i++)
            {
                long min = long.MaxValue;

                for (int j = i; j < arr.Count; j++)
                {
                    min = Math.Min(min, arr[j]);
                    if (min != mqr.GetMin(i, j)) return false;
                }
            }

            return true;
        }

        private void Dump(List<long> lst, List<Operation> ops)
        {
            Console.WriteLine($"Array state:");
            Console.WriteLine(string.Join(' ', lst.ToArray()));
            foreach (var item in ops)
            {
                Console.WriteLine($"v, i, j: {item.v} {item.i} {item.j}");
            }
        }

        [Ignore]
        [TestMethod]
        public void TestRandom()
        {
            for (int tc = 0; tc < 1000; tc++)
            {

                Console.WriteLine($"tc: {tc}");
                var (arr, ops) = tc ==0 ?GenerateTest(1, 10, 1000) :GenerateTest(100, 10000, 100);


                // Dump(arr, ops);

                MinLazySegmentTree mrq = new MinLazySegmentTree(arr.ToArray());
                var res = Check(arr, mrq);
                int cnt = 0;
                if (!res)
                {

                }
                Assert.IsTrue(res);

                foreach (var operation in ops)
                {
                    cnt++;
                    var v = operation.v;
                    var i = operation.i;
                    var j = operation.j;
                    for (int ind = i; ind <= j; ind++)
                    {
                        arr[ind] += v;
                    }
                    mrq.Add(i, j, v);

                    res = Check(arr, mrq);
                    if (!res)
                    {
                        Console.WriteLine($"Operation number: {cnt}");
                    }
                    Assert.IsTrue(res);
                }
            }

        }

        [TestMethod]
        [DataRow(new long[] { 1, -4, -10, 0, -9, -5, 1, -3, -1 })]
        public void TestInitialize(long[] arr)
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(arr);

            Assert.AreEqual(-10, mrq.GetMin(0, 8));
            Assert.AreEqual(-10, mrq.GetMin(1, 8));
            Assert.AreEqual(-10, mrq.GetMin(2, 8));
            Assert.AreEqual(-9, mrq.GetMin(3, 8));
            Assert.AreEqual(-9, mrq.GetMin(4, 8));
            Assert.AreEqual(-5, mrq.GetMin(5, 8));
            Assert.AreEqual(-3, mrq.GetMin(6, 8));
            Assert.AreEqual(-3, mrq.GetMin(7, 8));
            Assert.AreEqual(-1, mrq.GetMin(8, 8));

            Assert.AreEqual(-10, mrq.GetMin(0, 7));
            Assert.AreEqual(-10, mrq.GetMin(1, 7));
            Assert.AreEqual(-10, mrq.GetMin(2, 7));
            Assert.AreEqual(-9, mrq.GetMin(3, 7));
            Assert.AreEqual(-9, mrq.GetMin(4, 7));
            Assert.AreEqual(-5, mrq.GetMin(5, 7));
            Assert.AreEqual(-3, mrq.GetMin(6, 7));
            Assert.AreEqual(-3, mrq.GetMin(7, 7));

            Assert.AreEqual(-10, mrq.GetMin(0, 6));
            Assert.AreEqual(-10, mrq.GetMin(1, 6));
            Assert.AreEqual(-10, mrq.GetMin(2, 6));
            Assert.AreEqual(-9, mrq.GetMin(3, 6));
            Assert.AreEqual(-9, mrq.GetMin(4, 6));
            Assert.AreEqual(-5, mrq.GetMin(5, 6));
            Assert.AreEqual(1, mrq.GetMin(6, 6));

            Assert.AreEqual(-10, mrq.GetMin(0, 5));
            Assert.AreEqual(-10, mrq.GetMin(1, 5));
            Assert.AreEqual(-10, mrq.GetMin(2, 5));
            Assert.AreEqual(-9, mrq.GetMin(3, 5));
            Assert.AreEqual(-9, mrq.GetMin(4, 5));
            Assert.AreEqual(-5, mrq.GetMin(5, 5));


            Assert.AreEqual(-10, mrq.GetMin(0, 4));
            Assert.AreEqual(-10, mrq.GetMin(1, 4));
            Assert.AreEqual(-10, mrq.GetMin(2, 4));
            Assert.AreEqual(-9, mrq.GetMin(3, 4));
            Assert.AreEqual(-9, mrq.GetMin(4, 4));

            Assert.AreEqual(-10, mrq.GetMin(0, 3));
            Assert.AreEqual(-10, mrq.GetMin(1, 3));
            Assert.AreEqual(-10, mrq.GetMin(2, 3));
            Assert.AreEqual(0, mrq.GetMin(3, 3));

            Assert.AreEqual(-10, mrq.GetMin(0, 2));
            Assert.AreEqual(-10, mrq.GetMin(1, 2));
            Assert.AreEqual(-10, mrq.GetMin(2, 2));

            Assert.AreEqual(-4, mrq.GetMin(0, 1));
            Assert.AreEqual(-4, mrq.GetMin(1, 1));

            Assert.AreEqual(1, mrq.GetMin(0, 0));
        }

        [TestMethod]
        [DataRow(new long[] { 0, 0, 0, 0 }, new int[] { 1 }, new int[] { 2 }, new int[] { 3 }, new int[] { 0 })]
        public void TestAdd(long[] arr, int[] s, int[] e, int[] v, int[] r)
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(arr);
            for (int i = 0; i < s.Length; i++)
            {
                var st = s[i];
                var end = e[i];
                var val = v[i];
                var res = r[i];
                mrq.Add(st, end, val);
                Assert.AreEqual(res, mrq.GetMin(0, arr.Length - 1));
            }

        }

        [TestMethod]
        [DataRow(new long[] { 8, 1, 4, -5, -3 }, new int[] { 1 }, new int[] { 2 }, new int[] { 3 }, new int[] { -5 })]
        public void TestAdd2(long[] arr, int[] s, int[] e, int[] v, int[] r)
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(arr);
            for (int i = 0; i < s.Length; i++)
            {
                var st = s[i];
                var end = e[i];
                var val = v[i];
                var res = r[i];
                mrq.Add(st, end, val);
                Assert.AreEqual(res, mrq.GetMin(0, arr.Length - 1));
            }

        }

        [TestMethod]
        [DataRow(new long[] { 0, 3 }, new int[] { 0 }, new int[] { 0 }, new int[] { 8 }, new int[] { 3 })]
        public void TestAdd3(long[] arr, int[] s, int[] e, int[] v, int[] r)
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(arr);
            for (int i = 0; i < s.Length; i++)
            {
                var st = s[i];
                var end = e[i];
                var val = v[i];
                var res = r[i];
                mrq.Add(st, end, val);
                Assert.AreEqual(res, mrq.GetMin(0, arr.Length - 1));
            }

        }

        [TestMethod]
        public void TestAdd4()
        {
            long[] arr = new long[]{4,3,2,1,0,1,2,3,4};
            MinLazySegmentTree mrq = new MinLazySegmentTree(arr);
            mrq.Add(4,4,1);
            mrq.Add(4,4,1);
            mrq.Add(4,4,1);
            mrq.Add(4,4,1);
            mrq.Add(4,4,1);

            Assert.AreEqual(1, mrq.GetMin(4, 8));

            mrq.Add(5,5,1);
            mrq.Add(5,5,1);

            Assert.AreEqual(2, mrq.GetMin(4, 8));
            mrq.Add(5,8,1);

            Assert.AreEqual(3, mrq.GetMin(4, 8));
            mrq.Add(5,5,1);

            Assert.AreEqual(3, mrq.GetMin(4, 8));
            
            mrq.Add(6,6,1);

            mrq.Add(7,7,1);
            mrq.Add(7,7,1);
            mrq.Add(7,7,1);
            
            mrq.Add(5,8,1);

            //  new long[]{4,3,2,1,5,6,5,8 ,6};

            Assert.AreEqual(5, mrq.GetMin(4, 8));
        }

       [TestMethod]
        public void TestAdd5()
        {
            long[] arr = new long[]{1, 0, 1};
            MinLazySegmentTree mrq = new MinLazySegmentTree(arr);
            mrq.Add(1,1,1);
            mrq.Add(2,2,1);

            mrq.Add(1,1,-1);
            mrq.Add(2,2,-1);
            
            Assert.AreEqual(0, mrq.GetMin(1, 2));
        }


        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]

        public void TestNull()
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestEmpty()
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(new long[0]);
        }


        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]

        public void TestOutOfRange1()
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(new long[2] { 1, 2 });
            mrq.Add(-1, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]

        public void TestOutOfRange2()
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(new long[2] { 1, 2 });
            mrq.Add(0, 2, 1);
        }

        [TestMethod]

        public void TestOne()
        {
            MinLazySegmentTree mrq = new MinLazySegmentTree(new long[1] { 0 });
            mrq.Add(0, 0, 1);
            Assert.AreEqual(1, mrq.GetMin(0, 0));

            mrq.Add(0, 0, -1);
            Assert.AreEqual(0, mrq.GetMin(0, 0));
        }
    }
}
