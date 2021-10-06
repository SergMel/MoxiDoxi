// https://www.geeksforgeeks.org/longest-common-subsequence-dp-4/
// https://www.geeksforgeeks.org/printing-longest-common-subsequence/

using System;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public static class LCS
    {
        public static string Find(string s1, string s2) {
            if(s1 == null || s2 == null) throw new Exception();

            if(s2.Length == 0 || s1.Length == 0) return string.Empty;

            var dp = new int[s1.Length+1, s2.Length+ 1];
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    dp[i, j] = Math.Max(dp[i-1, j], Math.Max(dp[i,j-1], dp[i-1, j-1] + (s1[i-1] == s2[j-1] ? 1:0) ));
                }
            }


            StringBuilder  sb = new StringBuilder();            
            int r = s1.Length;
            int c = s2.Length;
            var cur = dp[r, c];
            while(cur > 0){
                if(r-1 >= 0 && dp[r-1, c] ==cur ) {
                    r--;
                    continue;
                }
                if(c-1 >= 0 && dp[r, c-1] ==cur ) {
                    c--;
                    continue;
                }

                sb.Insert(0, s1[r-1]);
                r--;
                c--;
                cur--;

            }

            return sb.ToString();
        }
    }
}
