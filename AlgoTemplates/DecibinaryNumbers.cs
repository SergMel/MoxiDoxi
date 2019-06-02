using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoTemplates.DecibinaryNumbers
{

	public class Solution
	{
		static long GetValue(int val,  long lr)
		{
			if (val == 0) return 0;
			if (lr == 1 && val < 10)
				return val;

			var bits = FindBitsCount(val, lr);

			int pw = 1;
			long dpw = 1;
			for (int i = 1; i < bits; i++)
			{
				pw *= 2;
				dpw *= 10;
			}
			
			long count = 0;
			for (int i = 0; i <= 9; i++)
			{
				long prev = count;
				count += counts[(val - pw * i) / 2, bits - 1];
				if ( count >= lr )
				{
					return dpw * i + GetValue(val - pw * i, lr - prev);
				}

			}
			return -1;
		}

		static int FindIndex(long x)
		{
			int left = 0;
			int right = topIndex.Length - 1;

			while (true)
			{
				int cur = (left + right) / 2;

				if (topIndex[cur] >= x && (cur - 1 < 0 || topIndex[cur - 1] < x))
				{
					return cur;
				}
				if (x > topIndex[cur])
				{
					left = cur + 1;
				}
				else if (topIndex[cur - 1] >= x)
				{
					right = cur - 1;
				}
			}
		}

		static int FindBitsCount(int val, long lr)
		{			
			for (int i = 1; i < 20; i++)
			{
				if (counts[val / 2, i] >= lr) return i;
			}
			return 19;
		}

		// Complete the decibinaryNumbers function below.
		static long decibinaryNumbers(long x)
		{
			if (x == 1)
				return 0;


			var val = FindIndex(x);

			return GetValue(val, x - topIndex[val - 1]);			
		}
		static long[] topIndex = new long[285113];
		static long[,] counts = new long[142557, 20];
		public static long CalculateCountsFoValue()
		{

			long r = 2;
			counts[0, 0] = 0;
			counts[0, 1] = 1;
			for (int i = 2; i < 20; i++)
			{
				counts[0, i] = 1;
			}
			topIndex[0] = 1;
			topIndex[1] = 2;
			int x = 2;
			long tmpr = r;
			while (r < 1e16)
			{
				if (x != 0 && x % 2 == 1)
				{
					r += counts[x / 2, 19];
					topIndex[x] = r;
					x++;
					continue;
				}

				counts[x / 2, 0] = 0;
				int s1 = 9;
				int s2 = 1;
				int k = 0;
				while (s1 < x)
				{
					counts[x / 2, k + 1] = 0;
					s1 = 2 * s1 + 9;
					s2 *= 2;
					k++;
				}
				long cnt = 0;
				while (x >= s2)
				{
					for (int i = 1; i <= 9 && (x - i * s2) >= 0; i++)
					{
						var prevx = x - i * s2;
						cnt += GetCount(prevx, k);
					}
					counts[x / 2, k + 1] = cnt;
					s2 *= 2;
					k++;
				}

				while (k + 1 <= 19)
				{
					counts[x / 2, k + 1] = cnt;
					k++;
				}
				r += cnt;
				topIndex[x] = r;
				x++;

			}
			return topIndex.Count();


		}

		static long GetCount(int val, int bitsCount)
		{
			if (val == 0 && bitsCount == 0)
				return 1;
			if (bitsCount < 0)
				return 0;
			if (bitsCount == 0 && val > 0)
				return 0;
			val = val / 2;
			return counts[val, bitsCount];
		}

		public static void Main(string[] args)
		{
			int q = Convert.ToInt32(Console.ReadLine());

			for (int qItr = 0; qItr < q; qItr++)
			{
				long x = Convert.ToInt64(Console.ReadLine());

				long result = decibinaryNumbers(x);

				Console.WriteLine(result);
			}
}
	}

}

