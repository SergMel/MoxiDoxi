// https://www.metacareers.com/profile/coding_puzzles
// https://www.metacareers.com/profile/coding_puzzles/?puzzle=280063030479374

using System.Collections.Generic;
using System.Linq;
class Solution {

  public double getMinExpectedHorizontalTravelDistance(int N, int[] H, int[] A, int[] B) {
    // Write your code here
    const decimal bs = 1_000_000.0M;
   
    Dictionary<int, List<int>> starts = new Dictionary<int, List<int>>(N);
    Dictionary<int, List<int>> ends = new Dictionary<int, List<int>>(N);

    HashSet<int> points = new HashSet<int>(2 * N);

    for (int i = 0; i < N; i++)
    {
        _dic_add_to_list(starts,A[i], i);
        _dic_add_to_list(ends, B[i], i);
        points.Add(A[i]);
        points.Add(B[i]);
    }

    var sorted_points = points.ToArray();
    Array.Sort (sorted_points);

    SortedSet<int> heights_cut = new SortedSet<int>();
    Dictionary<int, int> indices_cut = new Dictionary<int, int>();

    var graph = Enumerable.Range(0, N).Select(el => new Node{Id = el}).ToDictionary(el=> el.Id, v=>v);
    int prev_pnt = -1;        

    List<(int id, decimal x, decimal flow)> roots = new List<(int id, decimal x, decimal flow)>();
    foreach (var pnt in sorted_points)
    {
        if (prev_pnt == -1)
        {
            prev_pnt = pnt;
        }
        else if(heights_cut.Count >0 )
        {               
            
            var max_h = heights_cut.Max;
            var max_i = indices_cut[max_h];

            roots.Add((max_i, (pnt + prev_pnt) / 2.0M, (pnt - prev_pnt) / bs )) ;           
            
        }
        var ends_to_remove = _dic_get_default(ends, pnt);
        foreach (var end_i in ends_to_remove)
        {
            var end_i_height = H[end_i];
            heights_cut.Remove (end_i_height);
            indices_cut.Remove (end_i_height);
        }

        foreach (var end_i in ends_to_remove)
        {
            var below_height = heights_cut.GetViewBetween(0, H[end_i]).Max;
            if (below_height == 0) continue;
            var below_index = indices_cut[below_height];

            _dic_get_default (graph, end_i).Right = below_index;
        }

        var starts_to_add = _dic_get_default(starts, pnt);
        foreach (var start_i in starts_to_add)
        {
            var below_height =
                heights_cut.GetViewBetween(0, H[start_i]).Max;
            if (below_height == 0) continue;
            var below_index = indices_cut[below_height];
            _dic_get_default (graph,start_i).Left =  below_index;
        }

        foreach (var start_i in starts_to_add)
        {
            var start_i_height = H[start_i];
            heights_cut.Add (start_i_height);
            indices_cut.Add (start_i_height, start_i);
        }
        prev_pnt = pnt;
    }

    var roots_indicies = roots.Select(el => el.id).Distinct().ToList();

    var topology_sorted = topology_sort(roots_indicies, graph, N);

    var corner_prob = new decimal[N];    

    decimal res = 0;

    foreach (var item in roots)
    {
        corner_prob[item.id] += item.flow / 2.0M;
        res += ((B[item.id] - A[item.id]) / 2.0M) * item.flow;
    }

    decimal[] left_corner_expected_dist = new decimal[N];
    decimal[] right_corner_expected_dist= new decimal[N];
    update_roots_expected_dist(roots_indicies, graph, N, A, B, left_corner_expected_dist, right_corner_expected_dist);

    Stack<int> reverse_topology_sorted = new Stack<int>();
    
    foreach (var i in topology_sorted)
    {
        reverse_topology_sorted.Push(i);

        var item = graph[i];
        var left = item.Left;
        var right = item.Right;
        if(left != null) {
            corner_prob[left.Value] += corner_prob[i] / 2.0M;
            res += corner_prob[i] * (B[left.Value] - A[left.Value]) / 2.0M;
        }
        if(right != null) {
            corner_prob[right.Value] += corner_prob[i] / 2.0M;
            res += corner_prob[i] * (B[right.Value] - A[right.Value]) / 2.0M;
        }
    }

    var left_avg = new decimal[N];    
    var right_avg = new decimal[N];    

    foreach (var i in reverse_topology_sorted)
    {
        var item = graph[i];
        var left = item.Left;
        var right = item.Right;
        if(left != null) {
            // left_avg[i] += corner_prob[i] * (B[left.Value] - A[left.Value]) / 2 + (left_avg[left.Value] + right_avg[left.Value]) / 2; 
            left_avg[left.Value] += (corner_prob[i] / 2.0M) * (A[item.Id] - A[left.Value]) ;
            right_avg[left.Value] += (corner_prob[i] / 2.0M) * (B[left.Value] - A[item.Id]);
        }
        if(right != null) {
            left_avg[right.Value] += (corner_prob[i] / 2.0M) * (B[item.Id] - A[right.Value]) ;
            right_avg[right.Value] += (corner_prob[i] / 2.0M) * (B[right.Value] - B[item.Id]);
        }
    }
    
    foreach (var item in roots)
    {
        left_avg[item.id] += (item.flow / 2.0M ) * (item.x - A[item.id]);
        right_avg[item.id] += (item.flow / 2.0M ) * (B[item.id] - item.x);
    }

    var max_diff = 0.0M;
    foreach (var i in reverse_topology_sorted)
    {
        var item = graph[i];
        var left = item.Left;
        var right = item.Right;
        
        var diff = left_avg[i] - right_avg[i] + corner_prob[i] * (left_corner_expected_dist[i] - right_corner_expected_dist[i]);

        max_diff = Math.Max(max_diff, Math.Abs(diff));
    }

    return (double)(res - max_diff);
  }
  
    static Stack<int> topology_sort(List<int> roots, Dictionary<int, Node> graph, int N)
    {   
        bool[] visited = new bool[N];
        Stack<int> st = new Stack<int>();
        foreach(var i in roots)
        {
            if (visited[i]) continue;

            topology_sort (graph, N, i, visited, st);   
        }
        return st;
    }

     static void update_roots_expected_dist(
        List<int> roots,
        Dictionary<int, Node> graph,
        int N,       
        int[] A,
        int[] B,
        decimal[] left_corner_expected_dist, 
        decimal[] right_corner_expected_dist
    )
    {
        var visited = new bool[N];

        foreach (var i in roots)
        {
            update_roots_expected_dist(graph, N ,i, A, B, left_corner_expected_dist, right_corner_expected_dist, visited);
        }
    }

    static void update_roots_expected_dist(
        Dictionary<int, Node> graph,
        int N,
        int i,
        int[] A,
        int[] B,
        decimal[] left_corner_expected_dist, 
        decimal[] right_corner_expected_dist, 
        bool[] visited
    )
    {
        if(visited[i]) {
            return;
        }
        visited[i] = true;
        var item = graph[i];
        var left = item.Left;
        var right = item.Right;

        if(left != null) {
           update_roots_expected_dist(graph, N, left.Value, A, B, left_corner_expected_dist, right_corner_expected_dist, visited);
           left_corner_expected_dist[i] = 0.5M * (left_corner_expected_dist[left.Value] + right_corner_expected_dist[left.Value] ) + 0.5M * (B[left.Value] - A[left.Value]  );
        }
        if(right != null) {
            update_roots_expected_dist(graph, N, right.Value, A, B, left_corner_expected_dist, right_corner_expected_dist, visited);
            right_corner_expected_dist[i] = 0.5M * (left_corner_expected_dist[right.Value] + right_corner_expected_dist[right.Value] ) + 0.5M * (B[right.Value] - A[right.Value]  );
        }           
    }

    static void topology_sort(
        Dictionary<int, Node> graph,
        int N,
        int i,
        bool[] visited,
        Stack<int> st
    )
    {
        if(graph[i].Left != null && !visited[graph[i].Left.Value]){
            topology_sort (graph, N, graph[i].Left.Value, visited, st);
        }
        if(graph[i].Right != null && !visited[graph[i].Right.Value]){
            topology_sort (graph, N, graph[i].Right.Value, visited, st);
        }

        visited[i] = true;
        st.Push (i);
    }
  
    static void _dic_add_to_list<T, V, C>(
        IDictionary<T, C> dic,
        T key,
        V val
    )
        where C : IList<V>, new()
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = new C();

        dic[key].Add(val);
    }

    static void _dic_add_to_list_unique<T, V, C>(
        IDictionary<T, C> dic,
        T key,
        V val
    )
        where C : IList<V>, new()
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) dic[key] = new C();
        if(!dic[key].Contains(val) ) dic[key].Add(val);
    }

    static V _dic_get_default<T, V>(IDictionary<T, V> dic, T key)
        where V : new()
    {
        if (dic == null) throw new ArgumentNullException();

        if (!dic.ContainsKey(key)) return new V();

        return dic[key];
    }

    class Node{
        public int Id {get;set;}
        public int? Left {get;set;}
        public int? Right {get;set;}
    }

}
