using System;
using Algorithms;

namespace AlgoTemplates_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UF!");
            UFTests.Tests();
            Console.WriteLine("BIT!");
            BitUT.Tests();
            Console.WriteLine("Heap!");
            HeapTest.RunTests();
            Console.WriteLine("Finished!");
        }
    }
}
