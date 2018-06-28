using Algo;
using System;
using System.Diagnostics;

namespace AlgoTest
{
    public class DijkstraMinHeapTest
    {

        public static void RunTest()
        {
            DijkstraMinHeap heap = new DijkstraMinHeap(1);
            heap.UpdateWeight(0, 5);
            Debug.Assert(heap.Count() == 1);

            var el = heap.Pop();
            Debug.Assert(heap.Count() == 0);
            Debug.Assert(el.Id == 0);
            Debug.Assert(el.Weight == 5);


            /////////////////////////
            heap = new DijkstraMinHeap(2);
            Debug.Assert(heap.Count() == 2);
            heap.UpdateWeight(1, 5);
            heap.UpdateWeight(0, 7);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 1);
            Debug.Assert(el.Id == 1);
            Debug.Assert(el.Weight == 5);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 0);
            Debug.Assert(el.Id == 0);
            Debug.Assert(el.Weight == 7);

            /////////////////////////
            heap = new DijkstraMinHeap(2);
            Debug.Assert(heap.Count() == 2);
            heap.UpdateWeight(0, 5);
            heap.UpdateWeight(1, 7);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 1);
            Debug.Assert(el.Id == 0);
            Debug.Assert(el.Weight == 5);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 0);
            Debug.Assert(el.Id == 1);
            Debug.Assert(el.Weight == 7);

            /////////////////////////
            heap = new DijkstraMinHeap(10);
            Debug.Assert(heap.Count() == 10);

            heap.UpdateWeight(3, 3);
            heap.UpdateWeight(4, 7);
            heap.UpdateWeight(5, 0);
            heap.UpdateWeight(2, 1);
            heap.UpdateWeight(7, 2);
            heap.UpdateWeight(1, 6);
            heap.UpdateWeight(9, 9);
            heap.UpdateWeight(8, 8);
            heap.UpdateWeight(6, 5);
            heap.UpdateWeight(0, 4);


            el = heap.Pop();
            Debug.Assert(heap.Count() == 9);
            Debug.Assert(el.Id == 5);
            Debug.Assert(el.Weight == 0);
            Debug.Assert(heap.GetById(el.Id) == null);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 8);
            Debug.Assert(el.Id == 2);
            Debug.Assert(el.Weight == 1);
            Debug.Assert(heap.GetById(el.Id) == null);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 7);
            Debug.Assert(el.Id == 7);
            Debug.Assert(el.Weight == 2);
            Debug.Assert(heap.GetById(el.Id) == null);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 6);
            Debug.Assert(el.Id == 3);
            Debug.Assert(el.Weight == 3);
            Debug.Assert(heap.GetById(el.Id) == null);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 5);
            Debug.Assert(el.Id == 0);
            Debug.Assert(el.Weight == 4);
            Debug.Assert(heap.GetById(el.Id) == null);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 4);
            Debug.Assert(el.Id == 6);
            Debug.Assert(el.Weight == 5);
            Debug.Assert(heap.GetById(el.Id) == null);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 3);
            Debug.Assert(el.Id == 1);
            Debug.Assert(el.Weight == 6);
            Debug.Assert(heap.GetById(el.Id) == null);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 2);
            Debug.Assert(el.Id == 4);
            Debug.Assert(heap.GetById(el.Id) == null);
            Debug.Assert(el.Weight == 7);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 1);
            Debug.Assert(el.Id == 8);
            Debug.Assert(heap.GetById(el.Id) == null);
            Debug.Assert(el.Weight == 8);

            el = heap.Pop();
            Debug.Assert(heap.Count() == 0);
            Debug.Assert(el.Id == 9);
            Debug.Assert(heap.GetById(el.Id) == null);
            Debug.Assert(el.Weight == 9);


        }
    }
}
