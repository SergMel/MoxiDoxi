using AlgoTest;
using System;

namespace AlgoTemplates.UT
{
    class Program
    {
        static void Main(string[] args)
        {
			FFTTest.RunTests();
			DijkstraMinHeapTest.RunTest();
			MaxSegTreeTest.RunTest();
			HeapTest.RunTests();

			Console.WriteLine("Hello World!");
        }
    }
}
