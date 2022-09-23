//https://www.geeksforgeeks.org/activity-selection-problem-greedy-algo-1/
using System;
using System.Linq;

namespace Algorithms
{
    public class ActivitySelectionProblem
    {
        public static int get_max_activity(int[] starts, int[] ends)
        {
            if (starts == null || ends == null || starts.Length != ends.Length)
            {
                throw new ArgumentException();
            }
            if (starts.Length == 0) return 0;
            if (starts.Length == 1) return 1;

            var sorted =
                starts
                    .Select((el, ind) => new { start = el, end = ends[ind] })
                    .OrderBy(el => el.end)
                    .ThenBy(el => el.start)
                    .ToList();

            int? prevEnd = null;
            int res = 0;
            foreach (var item in sorted)
            {
                if (prevEnd == null || prevEnd.Value <= item.start)
                {
                    res++;
                    prevEnd = item.end;
                }
            }

            return res;
        }
    }
}
