using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AlgoTemplates
{
    public class Candies
    {
		// Complete the candies function below.

		// Complete the candies function below.
		static long candies(int n, int[] arr)
		{
			List<int> mins = new List<int>(n);
			List<int> cnds = Enumerable.Repeat(0, n).ToList();
			for (int i = 0; i < n; i++)
			{
				if ((i == 0 || arr[i - 1] >= arr[i]) && (i == n - 1 || arr[i + 1] >= arr[i]))
				{
					mins.Add(i);
					cnds[i] = 1;
				}
			}
			HashSet<int> leftBlocked = new HashSet<int>();
			HashSet<int> rightBlocked = new HashSet<int>();
			for (int k = 1; k < n; k++)
			{
				bool update = false;
				foreach (var i in mins)
				{
					if (!rightBlocked.Contains(i))
					{
						if (i + k >= n || cnds[i + k] > 0)
						{
							rightBlocked.Add(i);
						}
						else
						{
							var ri = i + k + 1;
							var li = i + k - 1;
							if (ri >= n)
								rightBlocked.Add(i);
							var ar = ri < n ? arr[ri] : int.MaxValue;
							var al = arr[li];
							var a = arr[i + k];
							if (al < a && a <= ar)
							{
								cnds[i + k] = cnds[i + k - 1] + 1;
								update = true;
							}
							else if (al == a && a <= ar)
							{
								cnds[i + k] = 1;
								update = true;
							}
							else if ((ri >= n || cnds[ri] != 0) && al <= a && a > ar)
							{
								cnds[i + k] = Math.Max(cnds[li], ri < n ? cnds[i + k + 1] : -1) + 1;
								rightBlocked.Add(i);
								update = true;
							}
							else
							{
								rightBlocked.Add(i);
							}
						}
					}
					if (!leftBlocked.Contains(i))
					{
						if (i - k < 0 || cnds[i - k] > 0)
						{
							leftBlocked.Add(i);
						}
						else
						{
							var ri = i - k + 1;
							var li = i - k - 1;
							if (li < 0)
								leftBlocked.Add(i);
							var ar = arr[ri];
							var al = li >= 0 ? arr[li] : int.MaxValue;
							var a = arr[i - k];
							if (al >= a && a > ar)
							{
								cnds[i - k] = cnds[i - k + 1] + 1;
								update = true;
							}
							else if (ar == a && a <= al)
							{
								cnds[i - k] = 1;
								update = true;
							}
							else if ((li < 0 || cnds[li] != 0) && ar <= a && a > al)
							{
								cnds[i - k] = Math.Max(cnds[ri], li >= 0 ? cnds[li] : -1) + 1;
								leftBlocked.Add(i);
								update = true;
							}
							else
							{
								leftBlocked.Add(i);
							}
						}
					}
				}
				if (!update)
					break;
			}
			//Check(cnds, arr);
			return cnds.Sum();
		}

		private static void Check(List<int> cnds, int[] arr)
		{
			for (int i = 0; i < cnds.Count; i++)
			{
				Debug.Assert(cnds[i] > 0);
				if (i > 0)
				{
					if (arr[i] > arr[i-1])
					{
						Debug.Assert(cnds[i] > cnds[i-1]);

					}
					if (arr[i] < arr[i - 1])
					{
						Debug.Assert(cnds[i] < cnds[i - 1]);

					}
				}
				

			}
		}

		public static void Main(string[] args)
		{
						int n = Convert.ToInt32(Console.ReadLine());

			int[] arr = new int[n];

			for (int i = 0; i < n; i++)
			{
				int arrItem = Convert.ToInt32(Console.ReadLine().Trim());
				arr[i] = arrItem;
			}

			long result = candies(n, arr);

			Console.WriteLine(result);

		}

	}
}
