using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo
{
    public interface IIdWeight
    {
        int Id { get; }
        int Weight { get; }
    }

    public class DijkstraMinHeap
    {
        private List<IdWeight> IndexToEntity;
        private List<int> IdToIndex;
        private int count;

        public DijkstraMinHeap(int size)
        {

            if (size <= 0)
                throw new ArgumentOutOfRangeException();

            IndexToEntity = Enumerable.Range(0, size).Select(el => new IdWeight(el) { Weight = int.MaxValue }).ToList();
            IdToIndex = new List<int>(Enumerable.Range(0, size));
            count = size;
        }
        public IIdWeight GetExtr()
        {
            if (count < 1)
                throw new InvalidOperationException();
            return IndexToEntity[0];
        }
        public IIdWeight Pop()
        {
            if (count < 1)
                throw new InvalidOperationException();

            var res = IndexToEntity[0];

            swap(0, count - 1);
            count--;
            Down(0);
            return res;
        }

        public int Count()
        {
            return count;
        }

        private void swap(int i1, int i2)
        {
            IdToIndex[IndexToEntity[i1].Id] = i2;
            IdToIndex[IndexToEntity[i2].Id] = i1;

            var tmp = IndexToEntity[i1];
            IndexToEntity[i1] = IndexToEntity[i2];
            IndexToEntity[i2] = tmp;
        }

        private void Up(int i)
        {
            if (i == 0) return;

            var parent = ((i + 1) / 2) - 1;

            if (IndexToEntity[parent] > IndexToEntity[i])
            {
                swap(parent, i);
                Up(parent);
            }
        }
        private void Down(int i)
        {
            var child1 = 2 * i + 1;
            var child2 = 2 * i + 2;
            var exc = child2;
            if (child2 >= count || IndexToEntity[child1] <= IndexToEntity[child2])
            {
                exc = child1;
            }
            if (exc < count && IndexToEntity[i] > IndexToEntity[exc])
            {
                swap(exc, i);
                Down(exc);
            }
        }

        public IIdWeight GetById(int id)
        {
            if (id < 0 || id >= IdToIndex.Count)
                throw new ArgumentOutOfRangeException();

            var index = IdToIndex[id];

            if (index >= count)
                return null;

            return IndexToEntity[index];
        }

        public void UpdateWeight(int id, int newWeight)
        {
            if (id < 0 || id >= IdToIndex.Count)
                throw new ArgumentOutOfRangeException();

            var index = IdToIndex[id];

            if (index > count)
                throw new InvalidOperationException();

            var entity = IndexToEntity[index];
            if (entity.Weight > newWeight)
            {
                entity.Weight = newWeight;
                Up(index);
            }
            else
            {
                entity.Weight = newWeight;
                Down(index);
            }

        }

        private class IdWeight : IIdWeight
        {
            public int Id { get; private set; }
            public int Weight { get; set; }

            public IdWeight(int id)
            {
                this.Id = id;
            }

            public static bool operator >(IdWeight first, IdWeight second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();
                return first.Weight > second.Weight;
            }

            public static bool operator <(IdWeight first, IdWeight second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();
                return first.Weight < second.Weight;
            }

            public static bool operator >=(IdWeight first, IdWeight second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();
                return first.Weight >= second.Weight;
            }

            public static bool operator <=(IdWeight first, IdWeight second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();
                return first.Weight <= second.Weight;
            }
        }
    }

}
