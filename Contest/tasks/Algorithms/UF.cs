using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class UF<T>
    {
        private Dictionary<T, int> dic;
        private List<T> items;

        private List<int> parents;
        private List<int> ranks;
        private int N;
        public UF()
        {
            N = 0;
            parents = new List<int>();
            ranks = new List<int>();
            dic = new Dictionary<T, int>();
            items = new List<T>();
        }

        public UF(int capacity)
        {
            N = 0;

            parents = new List<int>(capacity);
            ranks = new List<int>(capacity);
            dic = new Dictionary<T, int>(capacity);
            items = new List<T>(capacity);
        }

        private int GroupCount
        {
            get { return N; }
        }

        private int GetRoot(int i)
        {
            if (parents[i] == i)
                return i;
            while (parents[i] != i)
            {
                parents[i] = parents[parents[i]];
                i = parents[i];
            }
            return parents[i];
        }

        public bool IsConnected(T ii, T jj)
        {

            if (object.Equals(ii, jj))
            {
                return true;
            }
            if (!dic.ContainsKey(ii) || !dic.ContainsKey(jj))
            {
                return false;
            }

            var i = dic[ii];
            var j = dic[jj];
            return GetRoot(i) == GetRoot(j);
        }
        public void Union(T ii, T jj)
        {

            if (!dic.ContainsKey(ii))
            {
                items.Add(ii);
                dic[ii] = items.Count - 1;
                N++;
                parents.Add(items.Count - 1);
                ranks.Add(0);
            }
            int i = dic[ii];

            if (!dic.ContainsKey(jj))
            {
                items.Add(jj);
                dic[jj] = items.Count - 1;
                N++;
                parents.Add(items.Count - 1);
                ranks.Add(0);
            }
            int j = dic[jj];

            if (i == j)
                return;

            // Console.Error.WriteLine($"i = {i}, j = {j}");
            if (i < 0 || i >= parents.Count || j < 0 || j >= parents.Count)
                throw new ArgumentOutOfRangeException();

            var ri = GetRoot(i);
            var rj = GetRoot(j);
            if (ri == rj)
                return;

            N--;
            if (ranks[ri] < ranks[rj])
            {
                parents[ri] = rj;
            }
            else if (ranks[ri] > ranks[rj])
            {
                parents[rj] = ri;
            }
            else
            {
                parents[ri] = rj;
                ranks[rj]++;
            }
        }    
    }
}