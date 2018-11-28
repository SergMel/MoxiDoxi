﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LP
{
	public class Solution
	{

		static private int FindPivotColumn(double[,] matrix)
		{
			for (int i = 0; i < matrix.GetLength(1) - 1; i++)
			{
				if (matrix[0, i] > 0)
					return i;
			}
			return -1;
		}

		static private int FindPivotRow(double[,] matrix, int column, HashSet<int> exclude = null)
		{
			int res = -1;
			int lastColumn = matrix.GetLength(1) - 1;
			for (int i = 1; i < matrix.GetLength(0); i++)
			{
				if ((exclude != null && exclude.Contains(i)) || matrix[i, column] <= 0) continue;
				if (res == -1) res = i;
				else if (matrix[i, lastColumn] / matrix[i, column] < matrix[res, lastColumn] / matrix[res, column])
					res = i;
			}
			return res;
		}

		static void pivot(double[,] matrix, int pivotrow, int pivotcol)
		{
			double pivotval = matrix[pivotrow, pivotcol];
			for (int c = 0; c < matrix.GetLength(1); c++)
			{
				matrix[pivotrow, c] = matrix[pivotrow, c] / pivotval;
			}
			matrix[pivotrow, pivotcol] = 1.0;

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (i == pivotrow) continue;
				var pivotcolval = matrix[i, pivotcol];
				matrix[i, pivotcol] = 0;
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (j == pivotcol)
					{
						continue;
					}
					matrix[i, j] = matrix[i, j] - matrix[pivotrow, j] * pivotcolval;
				}
			}
		}

		static private Dictionary<int, int> FindBFS(double[,] matrix, Dictionary<int, int> basis)
		{
			var or = Copy(matrix);

			// check(or, matrix, basis);
			Dictionary<int, int> ret = new Dictionary<int, int>(basis);
			foreach (var item in basis)
			{
				pivot(matrix, item.Key, item.Value);
				// check(or, matrix, basis);
			}

			int pivotcol;
			while ((pivotcol = FindPivotColumn(matrix)) >= 0)
			{
				var pivotrow = FindPivotRow(matrix, pivotcol);
				pivot(matrix, pivotrow, pivotcol);
				ret[pivotrow] = pivotcol;
				// check(or, matrix, ret);
			}

			return ret;
		}
		public static double[,] Copy(double[,] matrix)
		{
			double[,] ret = new double[matrix.GetLength(0), matrix.GetLength(1)];

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					ret[i, j] = matrix[i, j];
				}
			}
			return ret;
		}

		static double[,] BuildAuzMatrix(double[,] matrix)
		{
			double[,] ret = new double[matrix.GetLength(0), matrix.GetLength(1) - 1 + matrix.GetLength(0)];
			for (int i = matrix.GetLength(1) - 1; i < ret.GetLength(1) - 1; i++)
			{
				ret[0, i] = -1;
			}
			for (int i = 1; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1) - 1; j++)
				{
					ret[i, j] = matrix[i, j];
				}
			}
			for (int i = 0; i < matrix.GetLength(0) - 1; i++)
			{
				ret[i + 1, matrix.GetLength(1) - 1 + i] = 1;
				ret[i + 1, ret.GetLength(1) - 1] = matrix[i + 1, matrix.GetLength(1) - 1];
			}
			return ret;
		}


		static private double CalculateMax(double[,] matrix)
		{

			double[,] initMatrix = BuildAuzMatrix(matrix);

			var basis = Enumerable.Range(0, matrix.GetLength(0) - 1).ToDictionary(k => k + 1, val => val + matrix.GetLength(1) - 1);
			var basis_aux = FindBFS(initMatrix, basis);

			// ============================================

			basis = new Dictionary<int, int>();
			HashSet<int> cols = new HashSet<int>(basis_aux.Select(el => el.Value));
			for (int i = 1; i < matrix.GetLength(0); i++)
			{
				if (basis_aux[i] < matrix.GetLength(1) - 1)
					basis[i] = basis_aux[i];
				else
				{
					for (int j = 0; j < matrix.GetLength(1) - 1; j++)
					{
						if (cols.Contains(j))
							continue;
						;
						if (initMatrix[i, j] > 0)
						{
							basis[i] = j;
							cols.Add(j);
						}
						else if (initMatrix[i, j] < 0 && initMatrix[i, initMatrix.GetLength(1) - 1] == 0)
						{
							basis[i] = j;
							cols.Add(j);
						}
					}
				}
			}

			Debug.Assert(basis.Count == matrix.GetLength(0) - 1);

			// ============================================

			for (int c = 0; c < matrix.GetLength(1) - 1; c++)
			{
				matrix[0, c] += initMatrix[0, c];
			}
			matrix[0, matrix.GetLength(1) - 1] = initMatrix[0, initMatrix.GetLength(1) - 1];
			for (int i = 1; i < matrix.GetLength(0); i++)
			{
				for (int c = 0; c < matrix.GetLength(1) - 1; c++)
				{
					matrix[i, c] = initMatrix[i, c];
				}
				matrix[i, matrix.GetLength(1) - 1] = initMatrix[i, initMatrix.GetLength(1) - 1];
			}
			// ============================================

			FindBFS(matrix, basis);


			return -1 * matrix[0, matrix.GetLength(1) - 1];
		}
		static int[] liars(int n, int[][] sets)
		{
			var colCount = 2 * n + 1;
			var rowCount = sets.Length + 1 + n;
			double[,] matrix = new double[rowCount, colCount];
			double[,] matrix2 = new double[rowCount, colCount];

			for (int i = 0; i < n; i++)
			{
				matrix[0, i] = 1;
				matrix2[0, i] = -1;
			}
			for (int i = 1; i < sets.Length + 1; i++)
			{
				matrix[i, colCount - 1] = sets[i - 1][2];
				matrix2[i, colCount - 1] = sets[i - 1][2];
				for (int c = sets[i - 1][0] - 1; c <= sets[i - 1][1] - 1; c++)
				{
					matrix[i, c] = 1;
					matrix2[i, c] = 1;
				}
			}
			int cc = 0;
			for (int i = sets.Length + 1; i < rowCount; i++)
			{

				matrix[i, cc] = 1;
				matrix[i, cc + n] = 1;
				matrix[i, colCount - 1] = 1;

				matrix2[i, cc] = 1;
				matrix2[i, cc + n] = 1;
				matrix2[i, colCount - 1] = 1;
				cc++;
			}

			var maxLiars = CalculateMax(matrix);
			var minLiars = -CalculateMax(matrix2);
			return new int[] { (int)minLiars, (int)maxLiars };
		}

		public static void output(double[,] matrix, int pivotRow, int pivotColumnt)
		{
			string file = @"C:\Personal\tmp\output.txt";
			File.AppendAllText(file, Environment.NewLine + "=========================================================");
			File.AppendAllText(file, $"Pivot row: {pivotRow}, column: {pivotColumnt}" + Environment.NewLine);

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				File.AppendAllText(file, Environment.NewLine);
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					File.AppendAllText(file, matrix[i, j].ToString() + " ");
				}
			}
		}

		public static void check(double[,] origin, double[,] matrix, Dictionary<int, int> basis)
		{
			double[] sol = new double[origin.GetLength(1) - 1];
			foreach (var pr in basis)
			{
				sol[pr.Value] = matrix[pr.Key, matrix.GetLength(1) - 1] / matrix[pr.Key, pr.Value];
			}

			for (int r = 1; r < origin.GetLength(0); r++)
			{
				double res = 0;
				for (int c = 0; c < sol.Length; c++)
				{
					res += sol[c] * origin[r, c];
				}
				Debug.Assert(res == origin[r, origin.GetLength(1) - 1]);
			}

		}

		public static void Main(string[] args)
		{

			string[] nm = Console.ReadLine().Split(' ');

			int n = Convert.ToInt32(nm[0]);

			int m = Convert.ToInt32(nm[1]);

			int[][] sets = new int[m][];

			for (int setsRowItr = 0; setsRowItr < m; setsRowItr++)
			{
				sets[setsRowItr] = Array.ConvertAll(Console.ReadLine().Split(' '), setsTemp => Convert.ToInt32(setsTemp));
			}

			int[] result = liars(n, sets);

			Console.WriteLine(string.Join(" ", result));

		}
	}
}
