//https://www.geeksforgeeks.org/binary-search/

using System;
using System.Collections.Generic;

public class BinarySearch
{
    public static int FindMaxOfLess(List<int> lst, int v) {
        if (lst == null ) throw new Exception();
        if (lst.Count == 0 ) return -1;

        var l = -1;
        var r = lst.Count;

        while(r-l > 1){
            var mdl = (l+r) /2;
            if(lst[mdl] <= v ) {
                l = mdl;
            } else {
                r = mdl;
            }
        }
        return l;


    }
}