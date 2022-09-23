using System;
public class MinLazySegmentTree
{
    private long[] tree;
    private long[] lazy;
    public int Count { get; private set; }
    public MinLazySegmentTree()
    {
    }

    public MinLazySegmentTree(long[] arr)
    {
        Build(arr);
    }

    public long Build(long[] arr)
    {
        if (arr == null) throw new ArgumentNullException();
        if (arr.Length == 0) throw new ArgumentException();

        var n = arr.Length;
        this.Count = n;
        var levels = (int)Math.Ceiling(Math.Log2(n));

        var size = (int)Math.Pow(2, levels + 1) - 1;
        tree = new long[size];
        lazy = new long[size];

        return Build(arr, 0, n - 1, 0);
    }

    private int Middle(int i, int j)
    {
        return i + (j - i) / 2;
    }

    private long Build(long[] arr, int i, int j, int ind)
    {
        if (i == j)
        {
            tree[ind] = arr[i];
            return tree[ind];
        }

        var mid = Middle(i, j);
        tree[ind] = Math.Min(Build(arr, i, mid, 2 * ind + 1), Build(arr, mid + 1, j, 2 * ind + 2));
        return tree[ind];
    }

    public void Add(int qs, int qe, long v)
    {
        if (qs < 0 || qe >= Count || qe < qs) throw new ArgumentOutOfRangeException();

        Add(qs, qe, v, 0, Count - 1, 0);
    }

    private void Add(int qs, int qe, long v, int i, int j, int ind)
    {       
        Update(ind); 
        if (i >= qs && j <= qe) { 
            lazy[ind] += v; 
            Update(ind);
            return; 
        }
        if (j < qs || i > qe) return;

        var mdl = Middle(i, j);

        Add(qs, qe, v, i, mdl, 2 * ind + 1);
        Add(qs, qe, v, mdl + 1, j, 2 * ind + 2);
        tree[ind] = Math.Min(tree[2*ind+1], tree[2*ind+2]);
    }

    private void Update(int ind)
    {
        tree[ind] += lazy[ind];
        if (2 * ind + 1 < lazy.Length)
        {
            lazy[2 * ind + 1] += lazy[ind];
            lazy[2 * ind + 2] += lazy[ind];           
        }
        lazy[ind] = 0;
    }

    public long GetMin(int qs, int qe)
    {
        if (qs < 0 || qe >= Count || qe < qs) throw new ArgumentOutOfRangeException();

        return GetMin(qs, qe, 0, Count - 1, 0);
    }

    private long GetMin(int qs, int qe, int i, int j, int ind)
    {
        if (j < qs || i > qe) return long.MaxValue;
        Update(ind);

        if (i >= qs && j <= qe) return tree[ind];

        var mdl = Middle(i, j);

        return Math.Min(GetMin(qs, qe, i, mdl, 2 * ind + 1), GetMin(qs, qe, mdl + 1, j, 2 * ind + 2));

    }
}