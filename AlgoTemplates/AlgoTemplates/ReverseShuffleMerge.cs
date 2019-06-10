using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoTemplates.ReverseShuffleMerge
{
	using System.CodeDom.Compiler;
	using System.Collections.Generic;
	using System.Collections;
	using System.ComponentModel;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.Serialization;
	using System.Text.RegularExpressions;
	using System.Text;
	using System;

	public class Solution
	{

		// Complete the reverseShuffleMerge function below.
		static string reverseShuffleMerge(string s)
		{
			Dictionary<char, int> cnt = new Dictionary<char, int>();
			LinkedList<char> ll = new LinkedList<char>();
			s = new string(s.Reverse().ToArray());
			foreach (var c in s)
			{
				ll.AddLast(c);

				if (!cnt.ContainsKey(c))
					cnt[c] = 0;
				cnt[c]++;
			}

			foreach (var key in cnt.Keys.ToList())
				cnt[key] /= 2;

			var cur = ll.First;

			int total = s.Length / 2;
			// cnt.Where(el=>el.Value > 0).Select(el=> $"{el.Key}: {el:Value}").ToList().ForEach(Console.WriteLine);
			while (total > 0)
			{
				if (cur.Next == null)
				{
					while (total > 0)
					{
						if (cnt[cur.Value] <= 0)
						{
							cur = cur.Previous;
						}
						else
						{
							var tmp = cur.Previous;
							cnt[cur.Value]--;
							ll.Remove(cur);
							total--;
							cur = tmp;

						}

						// if (ll.Count() < 60)
						//{ll.ToList().ForEach(Console.Write);
						//Console.WriteLine();}

					}
					continue;
				}

				if (cnt[cur.Value] <= 0)
				{
					cur = cur.Next;
					continue;
				}
				if (cur.Value > cur.Next.Value)
				{

					var tmp = cur.Previous ?? cur.Next;
					cnt[cur.Value]--;
					ll.Remove(cur);
					total--;
					cur = tmp;

				}
				else
				{
					cur = cur.Next;
				}
			}

			var sb = new StringBuilder();
			foreach (var c in ll)
			{
				sb.Append(c);
			}
			return sb.ToString();


		}

		public static void Main(string[] args)
		{
			
			string s = Console.ReadLine();

			string result = reverseShuffleMerge(s);

			Console.WriteLine(result);

		}
	}

}
