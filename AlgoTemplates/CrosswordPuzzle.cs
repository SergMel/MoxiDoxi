using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoTemplates
{
    public class CrosswordPuzzle
    {
        static bool Empty(char c)
        {
            return c == '-';
        }
        static bool IsHStart(char[,] arr, int i, int j)
        {
            if (!Empty(arr[i, j])) return false;
            if (j - 1 < 0) return true;
            if (Empty(arr[i, j - 1])) return false;
            return true;
        }
        static bool IsVStart(char[,] arr, int i, int j)
        {
            if (!Empty(arr[i, j])) return false;
            if (i - 1 < 0) return true;
            if (Empty(arr[i - 1, j])) return false;
            return true;
        }

        static int GetLengthH(char[,] arr, int x, int y)
        {
            int cnt = 0;
            for (int j = y; j < arr.GetLength(1); j++)
            {
                if (Empty(arr[x, j]))
                    cnt++;
                else
                    break;
            }
            return cnt;
        }

        static int GetLengthV(char[,] arr, int x, int y)
        {
            int cnt = 0;
            for (int i = x; i < arr.GetLength(0); i++)
            {
                if (Empty(arr[i, y]))
                    cnt++;
                else
                    break;
            }
            return cnt;
        }
        static List<Tuple<int, int, int, bool>> FindWords(char[,] arr)
        {
            // int l = arr.GetLength(1);
            List<Tuple<int, int, int, bool>> ret = new List<Tuple<int, int, int, bool>>();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (IsHStart(arr, i, j))
                    {
                        var hl = GetLengthH(arr, i, j);
                        if (hl > 1)
                        {
                            ret.Add(Tuple.Create(i, j, hl, true));
                        }
                    }
                    if (IsVStart(arr, i, j))
                    {
                        var vl = GetLengthV(arr, i, j);
                        if (vl > 1)
                        {
                            ret.Add(Tuple.Create(i, j, vl, false));
                        }
                    }
                }

            }
            return ret;
        }

        // Complete the crosswordPuzzle function below.
        static string[] crosswordPuzzle(string[] crossword, string words)
        {
            string[] wrds = words.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            char[,] arr = new char[crossword.Length, crossword.First().Length];
            for (int i = 0; i < crossword.Length; i++)
            {
                var s = crossword[i];
                for (int j = 0; j < s.Length; j++)
                {
                    arr[i, j] = s[j];
                }
            }

            var ps = FindWords(arr);
            HashSet<int> visH = new HashSet<int>();
            HashSet<int> visV = new HashSet<int>();
            List<string> ret = new List<string>();
            if (Try(arr, ps, visH, visV, wrds, 0))
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        sb.Append(arr[i, j]);
                    }
                    ret.Add(sb.ToString());
                }
                return ret.ToArray();
            }
            else
            {
                return null;
            }

        }

        static void UnFillWord(char[,] arr, int x, int y, string w, bool h)
        {
            if (h)
            {
                for (int j = 0; j < w.Length; j++)
                {
                    arr[x, j + y] = '-';

                }
            }
            else
            {
                for (int i = 0; i < w.Length; i++)
                {
                    arr[x + i, y] = '-';
                }
            }
        }

        static bool FillWord(char[,] arr, int x, int y, string w, bool h)
        {
            if (h)
            {
                for (int j = 0; j < w.Length; j++)
                {
                    if (!Empty(arr[x, j + y]) && arr[x, j + y] != w[j])
                        return false;
                    arr[x, j + y] = w[j];

                }
            }
            else
            {
                for (int i = 0; i < w.Length; i++)
                {
                    if (!Empty(arr[i + x, y]) && arr[x + i, y] != w[i])
                        return false;
                    arr[x + i, y] = w[i];

                }
            }
            return true;
        }

        static bool Try(char[,] arr, List<Tuple<int, int, int, bool>> pos,
            HashSet<int> visH, HashSet<int> visV, string[] words, int k)
        {
            if (k >= words.Length)
                return true;
            var l = arr.GetLength(1);
            string w = words[k];
            foreach (var tpl in pos)
            {
                if (tpl.Item4 && visH.Contains(l * tpl.Item1 + tpl.Item2))
                    continue;
                if (!tpl.Item4 && visV.Contains(l * tpl.Item1 + tpl.Item2))
                    continue;
                if (w.Length == tpl.Item3)
                {
                    if (!FillWord(arr, tpl.Item1, tpl.Item2, w, tpl.Item4))
                    {
                        UnFillWord(arr, tpl.Item1, tpl.Item2, w, tpl.Item4);
                        continue;
                    }
                    if (tpl.Item4)
                        visH.Add(l * tpl.Item1 + tpl.Item2);
                    else
                        visV.Add(l * tpl.Item1 + tpl.Item2);
                    if (Try(arr, pos, visH, visV, words, k + 1))
                    {
                        return true;
                    }
                    UnFillWord(arr, tpl.Item1, tpl.Item2, w, tpl.Item4);

                    if (tpl.Item4)
                        visH.Remove(l * tpl.Item1 + tpl.Item2);
                    else
                        visV.Remove(l * tpl.Item1 + tpl.Item2);

                }

            }
            return false;

        }



        public static void Main(string[] args)
        {
           
            string[] crossword = new string[10];

            for (int i = 0; i < 10; i++)
            {
                string crosswordItem = Console.ReadLine();
                crossword[i] = crosswordItem;
            }

            string words = Console.ReadLine();

            string[] result = crosswordPuzzle(crossword, words);

            Console.WriteLine(string.Join("\n", result));

           
        }

    }
}
