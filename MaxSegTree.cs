using System;
using System.Collections.Generic;
using System.Text;

namespace Algo
{
    public class MaxSegTree<T> where T : IComparable<T>
    {
        public MaxSegTree(IList<T> lst) : this(lst, Comparer<T>.Default)
        {
        }

        public MaxSegTree(IList<T> lst, IComparer<T> comparer)
        {
            if (lst == null)
                throw new ArgumentNullException();
            this.comparer = comparer ?? throw new ArgumentNullException();
            buildTree(lst);
        }

        private readonly IComparer<T> comparer;

        private int count;
        private int levelsCount;
        private int firstLeafIndex;
        private T[] treeArray;

        public T GetMax(int start, int end/*non-inclusive*/)
        {
            start = start >= 0 ? start : 0;
            end = end <= treeArray.Length ? end : treeArray.Length;
            T res = default(T);
            FindMax(start, end, 0, count, 0, ref res);
            return res;
        }

        private bool FindMax(int start, int end, int segmentStart, int segmentEnd, int index, ref T res)
        {
            if (start <= segmentStart && end >= segmentEnd)
            {
                res = treeArray[index];
                return true;
            }

            if (start >= segmentEnd || end <= segmentStart)
            {                
                return false;
            }

            var mid = segmentStart + (segmentEnd - segmentStart + 1) / 2;
            T v1 = default(T);
            T v2 = default(T);
            if (FindMax(start, end, segmentStart, mid, 2 * index + 1, ref v1))
            {
                if (FindMax(start, end, mid, segmentEnd, 2 * index + 2, ref v2))
                {
                    if (comparer.Compare(v1, v2) >= 0)
                    {
                        res = v1;
                        return true;
                    }
                    else
                    {
                        res = v2;
                        return true;
                    }
                }
                else
                {
                    res = v1;
                    return true;
                }
            }
            else if(FindMax(start, end, mid, segmentEnd, 2 * index + 2, ref v2))
            {
                res = v2;
                return true;
            }
            else
            {
                return false;
            }            
        }

        private void buildTree(IList<T> lst)
        {
            count = lst.Count;
            levelsCount =(int) Math.Ceiling(Math.Log(lst.Count, 2)) + 1;

            int totalNumber = (int)Math.Pow(2, levelsCount) - 1;
            firstLeafIndex = totalNumber - (int)Math.Pow(2, levelsCount - 1);
            treeArray = new T[totalNumber];
            buildTree(lst, 0, lst.Count, 0);
        }

        private T buildTree(IList<T> lst, int start, int end, int treeIndex)
        {
            if (end - start == 1)
                return treeArray[treeIndex] = lst[start];

            var mid = start + (end - start + 1) / 2;

            var ch1 = 2 * treeIndex + 1;
            var ch2 = 2 * treeIndex + 2;

            var v1 = buildTree(lst, start, mid, ch1);
            var v2 = buildTree(lst, mid, end, ch2);
            if (comparer.Compare(v1, v2) >= 0)
                treeArray[treeIndex] = v1;
            else
                treeArray[treeIndex] = v2;

            return treeArray[treeIndex];
        }

        private void Recalculate(int id)
        {
            if (id == 0) return;

            var parent = (id - 1) / 2;
            if (comparer.Compare(treeArray[parent], treeArray[id]) <= 0)
            {
                treeArray[parent] = treeArray[id];
                Recalculate(parent);
            }
        }

        public void Update(int id, T newValue)
        {
            var itemIndex = firstLeafIndex + id;
            if (itemIndex >= treeArray.Length) throw new ArgumentOutOfRangeException();
            treeArray[itemIndex] = newValue;
            Recalculate(itemIndex);
        }
    }
}