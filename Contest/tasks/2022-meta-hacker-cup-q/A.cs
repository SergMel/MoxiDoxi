// https://www.facebook.com/codingcompetitions/hacker-cup/2022/qualification-round

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

    public static void Main()
    {
        const long mod = 1_000_000_007L;
        using var f = File.OpenText("input.txt");

        var T = int.Parse(f.ReadLine());
        StringBuilder sb = new StringBuilder();
        for (int cs = 0; cs < T; cs++)
        {
            var nk = Array.ConvertAll(f.ReadLine().Trim().Split(), int.Parse);
            var n = nk[0];
            var k = nk[1];
            var s_arr = Array.ConvertAll(f.ReadLine().Trim().Split(), int.Parse);

            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach(var el in s_arr) {
                dic._dic_increment_int(el);
            }
            var cnt1 = 0;
            var cnt2 = 0;
            foreach(var pr in dic) {
                if(pr.Value > 2) {
                    cnt1 = k+1;
                    break;
                } else if (pr.Value == 2) {
                    cnt1 ++;
                    cnt2 ++;
                } else {
                    if (cnt1 > cnt2) {
                        cnt2 ++;
                    } else {
                        cnt1 ++;
                    }
                }

            }
            if(cnt1 > k || cnt2 > k) {
                sb.AppendLine($"Case #{cs+1}: NO");
            } else 
            {
                sb.AppendLine($"Case #{cs+1}: YES");
            }
            

            
        }
        File.WriteAllText("output.txt", sb.ToString());
    }

}
