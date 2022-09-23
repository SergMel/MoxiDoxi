// https://www.metacareers.com/profile/coding_puzzles
// https://www.metacareers.com/profile/coding_puzzles/?puzzle=587690079288608
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

class Solution
{



    public static void Main() {

        // Random rnd = new Random();
        // int cnt = 0 ;
        // var dire = new char[] {'U', 'D', 'L', 'R'};
        // var sol = new Solution();
        // while(true) {
        //     cnt ++;
        //     // Console.WriteLine($"Step: {cnt}");
        //     if(cnt % 1000 == 0) {
        //         Console.WriteLine(cnt );
        //     }
        //     var N = 2 + rnd.Next(1_999_999);
        //     // var N =2 +  (cnt / 100000) ;
        //     // var N = 2000000;
        //     var sb = new StringBuilder();
        //     var dists = new int[N];
        //     for (int i = 0; i < N; i++)
        //     {
        //         sb.Append(dire[rnd.Next(4)]);
        //         dists[i] = 1+ rnd.Next(1_000_000_000);
        //         // dists[i] = 1_000_000_000;
        //     }
        //     try{
        //         var res = sol.getPlusSignCount(N, dists, sb.ToString());
        //     }  catch(Exception ex) {                
        //         Console.WriteLine(ex.ToString());
        //         var sb2 = new StringBuilder();
        //         sb2.AppendLine(N.ToString());
        //         sb2.AppendLine(string.Join(' ', dists));
        //         sb2.AppendLine(sb.ToString());
        //         File.AppendAllText("result.txt", sb2.ToString());
        //         return;
        //     }
        // }
        
        // Console.WriteLine(new Solution().getPlusSignCount(10, new int[] {2, 2, 2, 2, 2, 2, 2, 2,1, 9}, "URDRURDRUL"));
    }

    public long getPlusSignCount(int N, int[] L, string D)
    {
        var (dicHorizontal, dicVertical) = CreateSectors(N, L, D);
        MergeSectors (dicHorizontal);
        MergeSectors (dicVertical);

        (dicHorizontal, dicVertical) =
            update_coordinates(dicHorizontal, dicVertical);
        var y_coords = get_y_coordinates(dicHorizontal, dicVertical);

        var x_coords_dic = get_x_coordinates_dic(dicHorizontal, dicVertical);

        var (ver_start, ver_end) = get_polongs_dic(dicVertical);

        long res = 0;
        if(x_coords_dic.Count < 1) return 0;

        BITree bitTree = new BITree(x_coords_dic.Count);
        foreach (var y in y_coords)
        {
            foreach (var ver_st in _dic_get_default(ver_start, y))
            {
                bitTree.Update(x_coords_dic[ver_st], 1);
            }

            foreach (var hor in _dic_get_default(dicHorizontal, y))
            {
                if(x_coords_dic[hor.s] > 0) {
                    res+= -bitTree.GetSum(x_coords_dic[hor.s] - 1) + bitTree.GetSum(x_coords_dic[hor.e]);
                } else {
                    res+= bitTree.GetSum(x_coords_dic[hor.e]);
                }
            }

            foreach (var ver_e in _dic_get_default(ver_end, y))
            {
                bitTree.Update(x_coords_dic[ver_e], -1);
            }
        }

        // Write your code here
        return res;
    }

        static V _dic_get_default<T, V>(IDictionary<T, V> dic, T key)
        where V : new()
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = new V();

        return dic[key];
    }

    public (Dictionary<long, List<long>>, Dictionary<long, List<long>>)
    get_polongs_dic(Dictionary<long, List<(long s, long e)>> dicVertical)
    {
        var lst =
            dicVertical
                .SelectMany(el =>
                    el
                        .Value
                        .Select(el2 =>
                            new { x = el.Key, y_s = el2.s, y_e = el2.e }))
                .ToList();

        return (
            lst
                .GroupBy(el => el.y_s)
                .ToDictionary(key => key.Key,
                val => val.Select(el => el.x).ToList()),
            lst
                .GroupBy(el => el.y_e)
                .ToDictionary(key => key.Key,
                val => val.Select(el => el.x).ToList())
        );
    }

    public Dictionary<long, int>
    get_x_coordinates_dic(
        Dictionary<long, List<(long s, long e)>> dicHorizontal,
        Dictionary<long, List<(long s, long e)>> dicVertical
    )
    {
        var horX1 =
            dicHorizontal.SelectMany(el => el.Value.Select(el2 => el2.s));
        var horX2 =
            dicHorizontal.SelectMany(el => el.Value.Select(el2 => el2.e));
        var verX = dicVertical.Select(el => el.Key);

        return (horX1.Union(horX2).Union(verX))
            .Distinct()
            .OrderBy(el => el)
            .Select((v, i) => new { v, i })
            .ToDictionary(k => k.v, v => v.i);
    }

    public List<long>
    get_y_coordinates(
        Dictionary<long, List<(long s, long e)>> dicHorizontal,
        Dictionary<long, List<(long s, long e)>> dicVertical
    )
    {
        var horY = dicHorizontal.Select(el => el.Key);
        var verY1 = dicVertical.SelectMany(el => el.Value.Select(el2 => el2.e));
        var verY2 = dicVertical.SelectMany(el => el.Value.Select(el2 => el2.s));

        return (horY.Union(verY1).Union(verY2))
            .Distinct()
            .OrderBy(el => el)
            .ToList();
    }

    public (
        Dictionary<long, List<(long s, long e)>>,
        Dictionary<long, List<(long s, long e)>>
    )
    update_coordinates(
        Dictionary<long, List<(long s, long e)>> dicHorizontal,
        Dictionary<long, List<(long s, long e)>> dicVertical
    )
    {
        var newHor =
            dicHorizontal
                .ToDictionary(key => key.Key,
                val =>
                    (
                    val
                        .Value
                        .Where(el => el.e - el.s - 2 >= 0)
                        .Select(v => (v.s + 1, v.e - 1))
                    ).ToList());
        var toDelete = newHor.Where(el=>el.Value.Count == 0).Select(el=>el.Key).ToList();
        foreach (var key in toDelete)
        {
            newHor.Remove(key);
        }
        var newVer =
            dicVertical
                .ToDictionary(key => key.Key,
                val =>
                    (
                    val
                        .Value
                        .Where(el => el.e - el.s - 2 >= 0)
                        .Select(v => (v.s + 1, v.e - 1))
                    ).ToList());

        toDelete = newVer.Where(el=>el.Value.Count == 0).Select(el=>el.Key).ToList();
        foreach (var key in toDelete)
        {
            newVer.Remove(key);
        }

        return (newHor, newVer);
    }

    public (
        Dictionary<long, List<(long s, long e)>>,
        Dictionary<long, List<(long s, long e)>>
    )
    CreateSectors(int N, int[] L, string D)
    {
        var dicVertical = new Dictionary<long, List<(long s, long e)>>();
        var dicHorizontal = new Dictionary<long, List<(long s, long e)>>();

        long x = 0;
        long y = 0;
        for (int i = 0; i < N; i++)
        {
            var direction = D[i];
            var val = L[i];
            if (direction == 'U')
            {
                var lst = _dic_get_default(dicVertical, x);
                lst.Add((y, y + val));

                y += val;
            }
            else if (direction == 'D')
            {
                var lst = _dic_get_default(dicVertical, x);
                lst.Add((y - val, y));

                y -= val;
            }
            else if (direction == 'L')
            {
                var lst = _dic_get_default(dicHorizontal, y);
                lst.Add((x - val, x));
                x -= val;
            }
            else
            {
                var lst = _dic_get_default(dicHorizontal, y);

                lst.Add((x, x + val));
                x += val;
            }
        }

        return (dicHorizontal, dicVertical);
    }

    public void MergeSectors(Dictionary<long, List<(long s, long e)>> dic)
    {
        foreach (var key in dic.Keys)
        {
            var lst = dic[key];
            var nlst = new List<(long s, long e)>();
            long? st = null;
            long? en = null;

            foreach (var item in lst.OrderBy(el => el.s))
            {
                if (!st.HasValue)
                {
                    st = item.s;
                    en = item.e;
                }
                else
                {
                    if (item.s <= en.Value && item.e > en.Value)
                    {
                        en = item.e;
                    }
                    else if (item.s > en.Value)
                    {
                        nlst.Add((st.Value, en.Value));
                        st = item.s;
                        en = item.e;
                    }
                }
            }
            nlst.Add((st.Value, en.Value));
            dic[key] = nlst;
        }
    }

    class BITree
    {
        public long Count { get; }

        private long[] Arr { get; }

        public BITree(long N)
        {
            if (N <= 0) throw new Exception();
            this.Count = N;
            this.Arr = new long[N + 1];
        }

        private long GetParent(long x)
        {
            if (x == 0) return 0;

            return x - (x & (-x));
        }

        public void Update(long x, long val)
        {
            if (x < 0 || x >= Count) throw new Exception();
            x++;
            while (x <= Count)
            {
                Arr[x] += val;
                x = x + (x & (-x));
            }
        }

        public long GetSum(long x)
        {
            if (x < 0 || x >= Count) throw new Exception();
            x++;
            long sum = 0;
            while (x > 0)
            {
                sum += Arr[x];
                x = x - (x & (-x));
            }
            return sum;
        }
    }
}
