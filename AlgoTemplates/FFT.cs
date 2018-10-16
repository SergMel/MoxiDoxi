using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;

namespace AlgoTemplates
{
    public static class FFT
    {
		
		private static int GetClosestPower(int val)
		{
			return 1 << (int) Math.Ceiling(Math.Log(val, 2));
		}

		public static List<Complex> RunFFT(List<int> arr)
		{
			return RunFFT(arr.Select(el => (Complex)el).ToList(), 1);
		}

		public static List<Complex> RunFFT(List<Complex> arr)
		{
			return RunFFT(arr, 1);
		}

		public static List<Complex> RunIFFT(List<Complex> arr)
		{
			return RunFFT(arr, -1);
		}

		private static List<Complex> RunFFT(List<Complex> arr, int reverse)
		{
			var N = GetClosestPower(arr.Count);
			List<Complex> lst = new List<Complex>((int)N);
			foreach (var item in arr)
			{
				lst.Add(item);
			}
			for (int i = arr.Count; i < N; i++)
			{
				lst.Add(0);
			}

			internalFFT(lst, reverse);
			return lst;
		}

		static void internalFFT(List<Complex> arr, int reverse)
		{
			if (arr.Count == 1)
				return;

			var ev = arr.Where((el, i) => i % 2 == 0).ToList();
			var odd = arr.Where((el, i) => i % 2 == 1).ToList();

			internalFFT(ev, reverse);
			internalFFT(odd, reverse);
			var factor = (reverse *(2 * Math.PI)) / arr.Count;
			var powers = Enumerable.Range(0, arr.Count).Select(el => Complex.FromPolarCoordinates(1, factor * el)).ToList();

			for (int i = 0; i < arr.Count / 2; i++)
			{
				arr[i] = ev[i] + powers[i] * odd[i];
				arr[i + arr.Count / 2] = ev[i] - powers[i] * odd[i];
			}
		}

	}
}
