// https://www.geeksforgeeks.org/kmp-algorithm-for-pattern-searching/

using System;
using System.Collections.Generic;

namespace Algorithms
{
    public static class KMP
    {
        private static List<int> BuildLps(string p) {
            List<int> lps = new List<int>();
            int i = 1;
            int len = 0;
            lps.Add(0);

            while(i < p.Length) {
                if (p[i] == p[len]) {
                    i++;
                    len++;
                    lps.Add(len);
                } else {
                    if(len > 0 ) {
                        len = lps[len - 1];
                    } else {
                        i++;
                        lps.Add(len);
                    }
                }
            }
            return lps;
        }
        public static List<int> FindAllPatterns(string s, string p) {
            if(s == null || p == null) throw new Exception();
            if (p.Length > s.Length) return new List<int>();
            
            if(p.Length == 0 || s.Length == 0)  return new List<int>();
            List<int> ret = new List<int>();
            var lps = BuildLps(p);
            int j = 0;
            int i = 0;
            while(i < s.Length) {
                if(p[j] == s[i]){
                    i++;
                    j++;
                }

                if (j==p.Length){
                    ret.Add(i-j);
                    j = lps[j-1];
                } else if(i < s.Length && p[j] != s[i]) {
                    if(j!=0) {
                        j = lps[j-1];
                    } else {
                        i = i +1;
                    }
                     
                }
            }
            return ret;

        }
    }
}