//https://www.metacareers.com/profile/coding_puzzles

// Write any using statements here
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;

static class Solution
{
    static V _dic_get_default<T, V>(this IDictionary<T, V> dic, T key)
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

    static IEnumerable<(int r, int c)> _get_nbs<T>(this T[][] arr, int r, int c) {
        var r_count = arr.Length;
        if (r_count ==0 ) throw new ArgumentOutOfRangeException();
        var c_count = arr[0].Length;
        if (c_count ==0 ) throw new ArgumentOutOfRangeException();


        if(r-1 >= 0) yield return (r-1, c);
        if(c-1 >= 0) yield return (r, c-1);
        if(r+1 < r_count) yield return (r+1, c);
        if(c+1 < c_count) yield return (r, c+1); 
    }

    static IEnumerable<(int r, int c)> _get_non_blocked_nbs(this bool[][] arr, int r, int c) {
       return arr._get_nbs(r,c).Where(el => !arr[el.r][el.c]);
    }

    static bool _is_cul_de_sac(this bool[][] blocked, int r, int c) {
        if(blocked[r][c]) return true;
        var b = blocked._get_nbs(r, c).Where(el => !blocked[el.r][el.c] ).Count() <= 1;
        if (b) blocked[r][c] = true;
        return blocked[r][c];
    }

    public static void Main()
    {
        const long mod = 1_000_000_007L;
        using var f = File.OpenText("input.txt");

        var T = int.Parse(f.ReadLine());
        StringBuilder sb = new StringBuilder();
        for (int cs = 0; cs < T; cs++)
        {
            var nk = Array.ConvertAll(f.ReadLine().Trim().Split(), int.Parse);
            var r = nk[0];
            var c = nk[1];
            char[][] field = new char[r][];
            bool[][] field_blocked = new bool[r][];

            for(int ri = 0; ri < r; ri ++) {
                field[ri] = f.ReadLine().Trim().ToArray();
                field_blocked[ri] = field[ri].Select(el => el == '#').ToArray();
            }
            
            Queue<(int r, int c)> culDeSac = new Queue<(int r, int c)>();
            for(int ri =0; ri < r; ri ++){
                for(int ci =0; ci < c; ci++) {
                    if(!field_blocked[ri][ci] && field_blocked._is_cul_de_sac(ri, ci)) {
                        culDeSac.Enqueue((ri, ci));
                    }
                }
            }

            while(culDeSac.Count > 0) {
                var cur = culDeSac.Dequeue();

                foreach (var el in field_blocked._get_non_blocked_nbs(cur.r, cur.c) )
                {
                    if(!field_blocked[el.r][el.c] && field_blocked._is_cul_de_sac(el.r, el.c)) {
                        culDeSac.Enqueue(el);
                    } 
                }
            }

            Boolean flag = false;
            for(int ri =0; ri < r; ri ++){
                for(int ci =0; ci < c; ci++) {
                    if(field[ri][ci] == '^' && field_blocked[ri][ci]) {
                        flag = true;
                        break;
                    }
                }
                if(flag) break;
            }
            if (flag) {
                sb.AppendLine($"Case #{cs+1}: Impossible");
                continue;
            } 

            sb.AppendLine($"Case #{cs+1}: Possible");
            for(int ri =0; ri < r; ri ++){
                for(int ci =0; ci < c; ci++) {
                    if(field_blocked[ri][ci]) {
                        sb.Append(field[ri][ci]);       
                    } else {
                        sb.Append('^');
                    }
                }
                sb.AppendLine();
            }
        }
        File.WriteAllText("output.txt", sb.ToString());
    }

}
