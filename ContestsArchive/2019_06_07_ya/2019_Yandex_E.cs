using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Numerics;

class C2019_Yandex_E
{

    static int? getVal(char c)
    {
        if (c >= 'a' && c <= 'f')
            return 10 + (c - 'a');
        if (c >= 'A' && c <= 'F')
            return 10 + (c - 'A');
        if (c >= '0' && c <= '9')
            return c - '0';
        return null;
    }

    static char? convert(char? A, char? B)
    {
        if (A == null || B == null)
            return null;
        var x = getVal(A.Value);
        var y = getVal(B.Value);
        if (x == null || y == null)
            return null;
        var res = x.Value * 16 + y.Value;
        return (char)res;
    }

    private static void Main2(string[] args)
    {
        var str = Console.ReadLine().Trim();
        Stack<char> st = new Stack<char>();
        Stack<int> cnt = new Stack<int>();
        int max = 0;
        for (int i = 0; i < str.Length; i++)
        {
            var modified = false;
            st.Push(str[i]);
            cnt.Push(0);
            do
            {
                modified = false;
                var el = st.Pop();
                var depth = cnt.Pop();
                if (el == ';')
                {
                    if (st.Count >= 3)
                    {
                        var B = st.Pop();
                        var A = st.Pop();
                        var f = st.Pop();

                        var cB = cnt.Pop();
                        var cA = cnt.Pop();
                        var cf = cnt.Pop();

                        if (f != '&')
                        {
                            st.Push(f);
                            st.Push(A);
                            st.Push(B);
                            st.Push(el);
                            cnt.Push(cf);
                            cnt.Push(cA);
                            cnt.Push(cB);
                            cnt.Push(depth);
                            continue;
                        }
                        var val = convert(A, B);
                        if (val == null)
                        {
                            st.Push(f);
                            st.Push(A);
                            st.Push(B);
                            st.Push(el);
                            cnt.Push(cf);
                            cnt.Push(cA);
                            cnt.Push(cB);
                            cnt.Push(depth);
                            continue;
                        }

                        st.Push(val.Value);
                        depth = Math.Max(depth, Math.Max(cA, Math.Max(cB, cf)));
                        cnt.Push(depth + 1);
                        el = val.Value;
                        max = Math.Max(depth + 1, max);
                        modified = true;
                    }
                    else
                    {
                        st.Push(el);
                        cnt.Push(0);

                    }
                }
                else
                {
                    st.Push(el);
                    cnt.Push(depth);
                }
            }
            while (modified);


        }

        Console.WriteLine(max);

    }
}