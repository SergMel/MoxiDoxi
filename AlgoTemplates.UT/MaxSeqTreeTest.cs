using Algo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlgoTest
{
    public class MaxSegTreeTest
    {
        public static void RunTest()
        {
            List<long> items = new List<long> { 1 };
            MaxSegTree<long> tree = new MaxSegTree<long>(items);
            Debug.Assert(tree.GetMax(0, 1) == 1);
            tree.Update(0, 3);
            Debug.Assert(tree.GetMax(0, 1) == 3);
            tree.Update(0, 0);
            Debug.Assert(tree.GetMax(0, 1) == 0);

            //////////////////////////
            items = new List<long> { 5, 1, 4, 3, 6, 0, -1, 3 };
            tree = new MaxSegTree<long>(items);
            Debug.Assert(tree.GetMax(0, 3) == 5);
            tree.Update(3, 10);
            Debug.Assert(tree.GetMax(0, 3) == 5);
            tree.Update(2, 10);
            Debug.Assert(tree.GetMax(0, 3) == 10);
            Debug.Assert(tree.GetMax(0, 9) == 10);
            tree.Update(5, 11);
            Debug.Assert(tree.GetMax(5, 6) == 11);
            Debug.Assert(tree.GetMax(6, 9) == 3);
            Debug.Assert(tree.GetMax(7, 8) == 3);
            tree.Update(6, 12);
            Debug.Assert(tree.GetMax(7, 8) == 3);
            tree.Update(7, 13);
            Debug.Assert(tree.GetMax(7, 8) == 13);
            Debug.Assert(tree.GetMax(0, 8) == 13);
        }
    }
}
