using AlgoTest;
using System;
using System.IO;

namespace AlgoTemplates.UT
{
    class Program
    {

		// Complete the solve function below.
		static double solve(int[][] coordinates)
		{
			long minx = long.MaxValue;
			long miny = long.MaxValue;
			long maxx = long.MinValue;
			long maxy = long.MinValue;
			foreach (var lst in coordinates)
			{
				if (minx > lst[0]) minx = lst[0];
				if (maxx < lst[0]) maxx = lst[0];
				if (miny > lst[1]) miny = lst[1];
				if (maxy < lst[1]) maxy = lst[1];
			}
			var q1 = Math.Max(Math.Abs(minx), Math.Abs(maxx));
			var q2 = Math.Max(Math.Abs(miny), Math.Abs(maxy));
			return Math.Max(Math.Max(maxx - minx, maxy - miny), Math.Sqrt((double)q1 * q1 + (double)q2 * q2));

		}

		static void Main(string[] args)
		{
			FileStream stream = new FileStream(@"C:\Personal\tmp\hr.txt", FileMode.Open);
			StreamReader sr = new StreamReader(stream);
			int n = Convert.ToInt32(sr.ReadLine());

			int[][] coordinates = new int[n][];

			for (int coordinatesRowItr = 0; coordinatesRowItr < n; coordinatesRowItr++)
			{
				coordinates[coordinatesRowItr] = Array.ConvertAll(sr.ReadLine().Split(' '), coordinatesTemp => Convert.ToInt32(coordinatesTemp));
			}
			sr.Close();
			stream.Close();
			double result = solve(coordinates);

			Console.WriteLine(result);
		}
		//     static void Main(string[] args)
		//     {
		////FFTTest.RunTests();
		////DijkstraMinHeapTest.RunTest();
		////MaxSegTreeTest.RunTest();
		////HeapTest.RunTests();
		////GCDTest.RunTests();
		//Crab_graphs.Crab_graphs.Main(args);
		//// Crab_graphsTests.BFSTest();
		//Console.WriteLine("Hello World!");
		//     }
	}
}
