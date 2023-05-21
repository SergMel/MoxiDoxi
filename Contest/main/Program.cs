
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

static class Solution
{
    static Random rnd = new Random(DateTime.Now.Millisecond);


    static int read_int() => int.Parse(Console.ReadLine().Trim());
    static int[] read_ints() => Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
    static long[] read_longs() => Array.ConvertAll(Console.ReadLine().Trim().Split(), long.Parse);
    static string read_string() => Console.ReadLine().Trim();
    static string[] read_strings() => Console.ReadLine().Trim().Split();


    static V _dic_get_default<T, V>(IDictionary<T, V> dic, T key)
        where V : new()
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = new V();

        return dic[key];
    }
    static void _dic_increment_int<T>(this IDictionary<T, int> dic, T key)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key]++;
    }

    static void _dic_increment_long<T>(this IDictionary<T, long> dic, T key)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key]++;
    }

    static void _dic_add_long<T>(this IDictionary<T, long> dic, T key, long val)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key] += val;
    }

    static void _shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    const long mod = 1_000_000_007L;
    static long mod7(this long val)
    {
        return val % mod;
    }

    static void _dic_add_int<T>(this IDictionary<T, int> dic, T key, int val)
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = 0;

        dic[key] += val;
    }

    public class Unknown
    {
        [JsonPropertyName("pattern")]
        public List<int> Pattern { get; set; }
        [JsonPropertyName("result")]
        public int Result { get; set; }
    }

    public static List<List<int>> Solve(List<List<int>> grid, List<Unknown> rules)
    {
        // Write your code here

        Dictionary<Tuple<int, int, int, int>, int> dic = new Dictionary<Tuple<int, int, int, int>, int>();

        foreach (var rule in rules)
        {
            rule.Pattern.Sort();
            dic[Tuple.Create(rule.Pattern[0], rule.Pattern[1],
             rule.Pattern[2], rule.Pattern[3])] = rule.Result;
        }
        List<List<int>> res = new List<List<int>>();
        for (int i = 0; i < grid.Count - 1; i++)
        {
            res.Add(new List<int>());
            for (int j = 0; j < grid[0].Count - 1; j++)
            {
                res[i].Add(0);
                var lst = new List<int> { grid[i][j], grid[i][j + 1], grid[i + 1][j], grid[i + 1][j + 1] };
                lst.Sort();
                var tpl = Tuple.Create(lst[0], lst[1], lst[2], lst[3]);
                if (dic.ContainsKey(tpl))
                {
                    res[i][j] = dic[tpl];
                }
               
            }
        }

        return res;
    }

    public static void Main() {

        //    for (int i = 0; i <= 30; i ++)
        //         for (int j = 0; j <= 30; j++)
        //             for (int k = 0; k <= 20; k++)
        //                 for (int l = 0; l <= 20; l++){
        //                     Console.WriteLine($"{i} {j} {k} {l}");
        //                     var res = Solve(i, j, k, l);
        //                     if (res.Count >= 1000) {
        //                         throw new Exception();
        //                     }
        //                     Console.WriteLine(string.Join(' ', res));
        //                 }

        var req = new List<List<int>>{
           new List<int>()  {1,1,1},
           new List<int>()  {1,1,4},
           new List<int>()  {1,4,6},
        };
        var patterns = new List<Unknown>{
            new Unknown{
                 Pattern = new   List<int>{1,1,1,1},
                 Result = 5,
            },
            new Unknown{
                 Pattern = new   List<int>{1,1,1,4},
                 Result = 6,
            }
        };
        var res = Solve(req, patterns);
        if (res.Count >= 1000)
        {
            throw new Exception();
        }
        Console.WriteLine(string.Join(' ', res));

    }
}
