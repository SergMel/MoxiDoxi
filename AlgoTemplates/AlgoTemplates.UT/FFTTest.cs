using Algo;
using AlgoTemplates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace AlgoTest
{
    public class FFTTest
	{
		const double eps = 1e-12; 
		private static bool almostEqual(Complex c1, Complex c2)
		{
			var dist = Math.Sqrt(Math.Pow(c1.Real - c2.Real, 2) + Math.Pow(c2.Imaginary - c1.Imaginary, 2));
			return dist <= eps;
		}
		
        public static void RunTests()
        {
			List<int> arr = new List<int> { 1, 2, 3 };
			var res = FFT.RunFFT(arr);

			List<Complex> expected = new List<Complex>
			{
				6,
				new Complex(-2, 2),
				2,
				new Complex(-2, -2),
			};

			Debug.Assert(almostEqual(expected[0], res[0]));
			Debug.Assert(almostEqual(expected[1], res[1]));
			Debug.Assert(almostEqual(expected[2], res[2]));
			Debug.Assert(almostEqual(expected[3], res[3]));

			res = FFT.RunIFFT(res);
			expected = new List<Complex>
			{
				1,
				2,
				3,
				0,
			};

			Debug.Assert(almostEqual(expected[0], res[0] / res.Count));
			Debug.Assert(almostEqual(expected[1], res[1] / res.Count));
			Debug.Assert(almostEqual(expected[2], res[2] / res.Count));
			Debug.Assert(almostEqual(expected[3], res[3] / res.Count));

		}
	}
}
