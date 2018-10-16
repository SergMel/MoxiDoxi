﻿using Algo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlgoTest
{
    public class HeapTest
    {
        public static void RunTests()
        {
            Heap<int> heap = new Heap<int>(1);
            heap.Add(1);
            Debug.Assert(heap.Pop() == 1);

            heap.Add(2);
            Debug.Assert(heap.Pop() == 2);

            heap.Add(2);
            heap.Add(1);
            Debug.Assert(heap.Pop() == 2);
            Debug.Assert(heap.Pop() == 1);

            heap.Add(2);
            heap.Add(1);
            heap.Add(3);
            Debug.Assert(heap.Pop() == 3);
            Debug.Assert(heap.Pop() == 2);
            Debug.Assert(heap.Pop() == 1);

            heap.Add(1);
            heap.Add(2);
            heap.Add(1);
            Debug.Assert(heap.Pop() == 2);
            Debug.Assert(heap.Pop() == 1);
            Debug.Assert(heap.Pop() == 1);
        }
    }
}