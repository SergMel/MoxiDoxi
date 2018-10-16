using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoTemplates
{
    public class GCD
    {
		public static int Euclidean(int n1, int n2)
		{
			if (n1 == n2) return n1;
			else if (n1 < n2)
			{
				var tmp = n1;
				n1 = n2;
				n2 = tmp;
			}

			while (n1 % n2 > 0)
			{
				var tmp = n2;
				n2 = n1 % n2;
				n1 = tmp;
			}
			return n2;
		}
	}
}
