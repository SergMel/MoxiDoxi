
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

        dic[key]+=val;
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
}
