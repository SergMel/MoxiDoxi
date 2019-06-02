using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoTemplates.CodingGame
{
    public class Winamax
    {

        static bool IsValidPos(char[,] field, int h, int w)
        {
            // Console.Error.WriteLine($"Is valid h:{h}, w:{w}");
            if (h >= 0 && h < field.GetLength(0) &&
                w >= 0 && w < field.GetLength(1))
            {
                // Console.Error.WriteLine($"Is valid h:{h}, w:{w}, value:{field[h,w]}");
                return field[h, w] == '.' || field[h, w] == 'H';
            }
            return false;
        }

        static bool IsValidStep(char[,] field, int h, int w)
        {
            if (h >= 0 && h < field.GetLength(0) &&
                w >= 0 && w < field.GetLength(1))
            {
                return field[h, w] == '.' || field[h, w] == 'H' || field[h, w] == 'X';
            }
            return false;
        }

        static List<char> getSteps(char[,] field, int h, int w)
        {
            var val = field[h, w];
            var shoutCnt = (int)char.GetNumericValue(val);
            List<char> ret = new List<char>();

            // up
            if (IsValidPos(field, h - shoutCnt, w))
            {
                bool f = false;
                for (int i = h - 1; i >= h - shoutCnt + 1; i--)
                {
                    if (!IsValidStep(field, i, w))
                    {
                        f = true;
                        break;
                    }
                }
                if (!f)
                {
                    ret.Add('^');
                }
            }

            // down
            if (IsValidPos(field, h + shoutCnt, w))
            {
                bool f = false;
                for (int i = h + 1; i <= h + shoutCnt - 1; i++)
                {
                    if (!IsValidStep(field, i, w))
                    {
                        f = true;
                        break;
                    }
                }
                if (!f)
                {
                    ret.Add('v');
                }
            }

            // left
            if (IsValidPos(field, h, w - shoutCnt))
            {
                bool f = false;
                for (int i = w - 1; i >= w - shoutCnt + 1; i--)
                {
                    if (!IsValidStep(field, i, w))
                    {
                        f = true;
                        break;
                    }
                }
                if (!f)
                {
                    ret.Add('<');
                }
            }

            // right
            if (IsValidPos(field, h, w + shoutCnt))
            {
                bool f = false;
                for (int i = w + 1; i <= w + shoutCnt - 1; i++)
                {
                    if (!IsValidStep(field, i, w))
                    {
                        f = true;
                        break;
                    }
                }
                if (!f)
                {
                    ret.Add('>');
                }
            }

            return ret;
        }

        static Tuple<int, int, bool, int> goTo(char[,] field, Tuple<int, int> from, char dir)
        {
            Console.Error.WriteLine("before goto");
            writeErrorField(field);
            Console.Error.WriteLine($"go from: {from} to {dir}");
            Console.Error.WriteLine(dir);
            Tuple<int, int> displ = null;
            if (dir == '<')
                displ = Tuple.Create(0, -1);
            else if (dir == '>')
                displ = Tuple.Create(0, 1);
            else if (dir == '^')
                displ = Tuple.Create(-1, 0);
            else
                displ = Tuple.Create(1, 0);

            int steps = (int)char.GetNumericValue(field[from.Item1, from.Item2]);

            var h0 = from.Item1;
            var w0 = from.Item2;

            field[h0, w0] = dir;

            for (int i = 1; i < steps; i++)
            {
                var h = from.Item1 + displ.Item1 * i;
                var w = from.Item2 + displ.Item2 * i;
                if (field[h, w] == '.')
                    field[h, w] = dir;

            }
            var h2 = from.Item1 + displ.Item1 * steps;
            var w2 = from.Item2 + displ.Item2 * steps;
            var res = field[h2, w2] == 'H';
            field[h2, w2] = res ? '0' : (steps - 1).ToString()[0];
            writeErrorField(field);
            Console.Error.WriteLine("after goto");
            return Tuple.Create(h2, w2, res, steps - 1);
        }


        static void comeBack(char[,] field, Tuple<int, int> from, char dir, bool wasHole, int steps)
        {

            Tuple<int, int> displ = null;
            if (dir == '<')
                displ = Tuple.Create(0, 1);
            else if (dir == '>')
                displ = Tuple.Create(0, -1);
            else if (dir == '^')
                displ = Tuple.Create(1, 0);
            else
                displ = Tuple.Create(-1, 0);



            var h0 = from.Item1 + displ.Item1 * steps;
            var w0 = from.Item2 + displ.Item2 * steps;

            field[h0, w0] = wasHole ? 'H' : '.';


            for (int i = 1; i < steps + 1; i++)
            {
                var h = from.Item1 + displ.Item1 * i;
                var w = from.Item2 + displ.Item2 * i;
                if (field[h, w] == dir)
                    field[h, w] = '.';

            }
            var h2 = from.Item1 + displ.Item1 * steps;
            var w2 = from.Item2 + displ.Item2 * steps;
            field[h2, w2] = (steps + 1).ToString()[0];
        }

        static Tuple<int, int> SelectCell(char[,] field)
        {
            Tuple<int, int> ret = null;
            int min = int.MaxValue;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    var val = field[i, j];
                    if (char.IsDigit(val))
                    {
                        var numval = (int)char.GetNumericValue(val);

                        if (numval > 0 && numval < min)
                        {
                            min = numval;
                            ret = Tuple.Create(i, j);
                        }
                    }
                }

            }
            writeErrorField(field);
            Console.Error.WriteLine($"selected: {ret}, value:{field[ret.Item1, ret.Item2]}");

            return ret;
        }

        static bool Solve(char[,] field)
        {
            // writeField(field);
            writeErrorField(field);

            var cell = SelectCell(field);
            // Console.Error.WriteLine(cell);

            if (cell == null)
                return true;

            var ngbs = getSteps(field, cell.Item1, cell.Item2);
            if (ngbs.Count < 1)
                return false;

            foreach (var el in ngbs)
            {
                var newPos = goTo(field, cell, el);
                if (Solve(field))
                {
                    // Console.Error.WriteLine("solved");
                    return true;
                }
                comeBack(field, Tuple.Create(newPos.Item1, newPos.Item2), el, newPos.Item3, newPos.Item4);
            }

            return false;
        }

        static void writeField(char[,] field)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    Console.Write(field[i, j] == '0' || field[i, j] == 'X' ? '.' : field[i, j]);
                Console.WriteLine();
            }
        }

        static void writeErrorField(char[,] field)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    Console.Error.Write(field[i, j]);
                Console.Error.WriteLine();
            }
            Console.Error.WriteLine("=================================");

        }

        static void Main(string[] args)
        {

            string[] inputs = Console.ReadLine().Split(' ');
            int width = int.Parse(inputs[0]);
            int height = int.Parse(inputs[1]);
            char[,] field = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                string row = Console.ReadLine();
                for (int j = 0; j < width; j++)
                    field[i, j] = row[j];
            }

            Solve(field);
            writeField(field);



        }

    }
}
