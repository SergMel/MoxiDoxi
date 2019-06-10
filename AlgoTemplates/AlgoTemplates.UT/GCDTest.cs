using Algo;
using AlgoTemplates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace AlgoTest
{
    public class GCDTest
	{
		
        public static void RunTests()
        {
			var res = GCD.Euclidean(9, 6);
			Debug.Assert(res == 3);

			res = GCD.Euclidean(109, 10010);
			Debug.Assert(res == 1);

			res = GCD.Euclidean(10010, 109);
			Debug.Assert(res == 1);
		}
	}
}
