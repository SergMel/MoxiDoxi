using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace comparison
{
    static class Program
    {
        const int timeLimit = 1950;
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private static Stopwatch watch = Stopwatch.StartNew();
        class Pair<T1, T2>
        {
            public T1 first;
            public T2 second;
        }

        const short MAX_EDGE_ID = 1 << 14;
        const short MAX_GROUP_ID = 4501;
        class Flow
        {
            public short id;
            public short source;
            public short target;
            public int flow;
        };

        class Constrain
        {
            public short node_id;
            public Pair<short, short> edge_ids = new Pair<short, short>();
        };

        class OrderedEdge
        {
            public short id;
            public short v;
            public short group_id;
        };


        class State
        {
            public List<Flow> flow_by_id;
            public List<OrderedEdge> ordered_edges;
            public List<Pair<short, short>> ordered_edges_ids;
            public List<Pair<short, short>> input_in_ordered_edges;
            public List<int> edge_capacity;
            public List<byte> node_flow_size;
            public List<byte> group_id_flow_size;
            public List<bool> is_node_blocked;
            public BitArray blocked = new BitArray(MAX_EDGE_ID * MAX_EDGE_ID);
        }

        static State state = new State();
        static Input inn = new Input();

        class Edge
        {
            public short id;
            public short group_id;
            public short v;
            public short u;
            public int distance;
            public int capacity;
        };


        static int blocked_hacher(int x, short y)
        {
            return (int)(((int)x << 14) + (int)y);
        }

        class Input
        {
            public short n;
            public short m;
            public List<Edge> edges;
            public List<Flow> flows;
            public List<Constrain> constrains;

            public void Read()
            {
                int constrained;
                int flow_count;
                
                var tmp = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
                this.n = (short)tmp[0];
                this.m = (short)tmp[1];
                constrained = tmp[2];
                flow_count = tmp[3];

                this.edges = Enumerable.Range(0, m).Select(el => new Edge()).ToList();
                foreach (var edge in this.edges)
                {
                    tmp = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
                    edge.id = (short)tmp[0];
                    edge.group_id = (short)tmp[1];
                    edge.v = (short)tmp[2];
                    edge.u = (short)tmp[3];
                    edge.distance = tmp[4];
                    edge.capacity = tmp[5];

                }
                this.constrains = Enumerable.Range(0, constrained).Select(el => new Constrain()).ToList();
                foreach (var constrain in this.constrains)
                {
                    var shorttmp = Array.ConvertAll(Console.ReadLine().Trim().Split(), short.Parse);
                    constrain.node_id = shorttmp[0];
                    constrain.edge_ids.first = shorttmp[1];
                    constrain.edge_ids.second = shorttmp[2];
                }
                this.flows = Enumerable.Range(0, flow_count).Select(el => new Flow()).ToList();
                foreach (var flow in this.flows)
                {
                    tmp = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
                    flow.id = (short)tmp[0];
                    flow.source = (short)tmp[1];
                    flow.target = (short)tmp[2];
                    flow.flow = tmp[3];
                }
                // if(watch.ElapsedMilliseconds > 500) throw new Exception();
            }
        }


        static void init_state()
        {
            state.flow_by_id = new List<Flow>(inn.flows);

            state.ordered_edges = new List<OrderedEdge>(inn.m * 2);
            for (int i = 0; i < inn.m; i++)
            {
                var e = inn.edges[i];
                state.ordered_edges.Add(new OrderedEdge
                {
                    id = e.id,
                    v = e.v,
                    group_id = e.group_id
                });
                state.ordered_edges.Add(new OrderedEdge
                {
                    id = e.id,
                    v = e.u,
                    group_id = e.group_id
                });
            }
            state.ordered_edges.Sort((OrderedEdge a, OrderedEdge b) =>
            {
                var ae = inn.edges[a.id];
                var be = inn.edges[b.id];
                int au = a.v == ae.u ? ae.v : ae.u;
                int bu = b.v == be.u ? be.v : be.u;
                if (au == bu) return ae.capacity - be.capacity;
                return au - bu;
            });

            state.ordered_edges_ids = Enumerable.Range(0, inn.n).Select(el => new Pair<short, short>()).ToList();

            state.input_in_ordered_edges = Enumerable.Range(0, inn.m).Select(el => new Pair<short, short>()).ToList();
            for (short i = 0; i < inn.m * 2; i++)
            {
                var e = inn.edges[state.ordered_edges[i].id];
                if (e.v == state.ordered_edges[i].v)
                {
                    state.input_in_ordered_edges[e.id].first = i;
                }
                else
                {
                    state.input_in_ordered_edges[e.id].second = i;
                }

                int prid = i == 0 ? -1 : state.ordered_edges[i - 1].id;
                int pru =
                        i == 0 ? -1 : (inn.edges[prid].u == state.ordered_edges[i - 1].v ? inn.edges[prid].v : inn.edges[prid].u);

                int id = state.ordered_edges[i].id;
                int u = (inn.edges[id].u == state.ordered_edges[i].v ? inn.edges[id].v : inn.edges[id].u);

                if (i == 0 || u != pru)
                {
                    state.ordered_edges_ids[u].first = i;
                }
                state.ordered_edges_ids[u].second = (short)(1 + i);
            }

            state.edge_capacity = new List<int>(new int[inn.m]);
            for (short i = 0; i < inn.m; i++)
            {
                state.edge_capacity[i] = inn.edges[i].capacity;
            }

            state.node_flow_size = new List<byte>(new byte[inn.n]);

            state.group_id_flow_size = new List<byte>(new byte[MAX_GROUP_ID]);

            state.is_node_blocked = new List<bool>(new bool[inn.n]);

            foreach (var constrain in inn.constrains)
            {
                state.blocked[blocked_hacher(constrain.edge_ids.first, constrain.edge_ids.second)] = true;
                state.blocked[blocked_hacher(constrain.edge_ids.second, constrain.edge_ids.first)] = true;
            }
        }


        class Output
        {
            public List<Pair<int, List<int>>> paths = new List<Pair<int, List<int>>>();

            public void write()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(paths.Count.ToString());
                foreach (var path in paths)
                {
                    sb.Append(path.first);

                    foreach (var it in path.second)
                    {
                        sb.Append(' ');
                        sb.Append(it);
                    }
                    sb.AppendLine();

                }
                Console.WriteLine(sb.ToString());
            }
        }
        const byte SFL = 200;
        const byte GFL = 100;

        static int used_timer = 0;
        static List<int> used;
        //    static vector<short> d(in.n);
        static List<short> pr;
        static List<int> find_path(Flow flow, short limit = 30000, byte node_flow_limit = SFL, byte group_flow_limit = GFL)
        {
            if (state.node_flow_size[flow.source] >= node_flow_limit)
            {
                return null;
            }

            used_timer++;
            used = used == null ? new List<int>(new int[inn.n]) : used;
            pr = pr == null ? new List<short>(new short[inn.n]) : pr;

            pr[flow.source] = 0;
            used[flow.source] = used_timer;
            List<short> q = new List<short>();
            int q_start = 0;
            q.Add(flow.source);

            int d = 0;
            int next_d = 1;
            while (q_start < q.Count())
            {
                if(watch.ElapsedMilliseconds > timeLimit) return null;
                if (q_start == next_d)
                {
                    ++d;
                    next_d = q.Count();
                }
                short v = q[q_start++];
                if (v == flow.target) break;
                if (d > limit) break;

                int e_id = state.ordered_edges[pr[v]].id;

                for (int i = state.ordered_edges_ids[v].first; i < state.ordered_edges_ids[v].second; i++)
                {
                    var e = state.ordered_edges[i];
                    if (state.node_flow_size[e.v] >= node_flow_limit ||
                        state.group_id_flow_size[e.group_id] >= group_flow_limit ||
                        state.edge_capacity[e.id] < flow.flow ||
                        state.blocked[blocked_hacher(e_id, e.id)] ||
                        (state.is_node_blocked[e.v] && e.v != flow.target) ||
                        used[e.v] == used_timer)
                        continue;

                    used[e.v] = used_timer;
                    //            d[e.v] = d[v] + 1;
                    q.Add(e.v);
                    pr[e.v] = (short)i;
                }
            }
            if (used[flow.target] != used_timer) return null;
            //    std::cerr << "Found path for " << flow.id << std::endl;
            List<int> ans = new List<int>();
            int v2 = flow.target;
            while (true)
            {
                int e_id = state.ordered_edges[pr[v2]].id;
                int to = inn.edges[e_id].v == v2 ? inn.edges[e_id].u : inn.edges[e_id].v;
                ans.Add(pr[v2]);
                v2 = to;
                if (to == flow.source) break;
            }
            ans.Reverse();
            return ans;
        }

        static short[] add_state_group_timer;
        static short add_state_timer;
        static void add_state(Flow flow, List<int> path, int add)
        {
            if (add_state_group_timer == null) add_state_group_timer = new short[MAX_GROUP_ID];
            add_state_timer++;

            state.node_flow_size[flow.source] += (byte)add;
            foreach (int id in path)
            {
                int e_id = state.ordered_edges[id].id;
                state.edge_capacity[e_id] -= add > 0 ? flow.flow : -flow.flow;
                if (add_state_group_timer[inn.edges[e_id].group_id] < add_state_timer ) {
                    add_state_group_timer[inn.edges[e_id].group_id] = add_state_timer;
                    state.group_id_flow_size[inn.edges[e_id].group_id] += (byte)add;
                }
                
                state.node_flow_size[state.ordered_edges[id].v] += (byte)add;
            }
        }

        private static Random rng = new Random(DateTime.Now.Millisecond);



        static void solve()
        {
            Output outp = new Output();

            init_state();

            inn.flows.Sort((a, b) =>
            {
                return a.flow - b.flow;
            });

            BitArray found = new BitArray(15000);

            for (int j = 6; j < 9; j++)
            {
                for (int i = 0; i < 6; i++)
                {
                    foreach (var flow in inn.flows)
                    {
                        if (found[flow.id]) continue;
                        var path_opt = find_path(flow, (short)(1 << i), (byte)Math.Min(SFL, (1 << j)), (byte)Math.Min(GFL, (1 << j)));
                        if (path_opt != null)
                        {
                            found[flow.id] = true;
                            add_state(flow, path_opt, 1);
                            outp.paths.Add(
                                new Pair<int, List<int>>()
                                {
                                    first = flow.id,
                                    second = new List<int>(path_opt)
                                });
                        }
                    }
                }
            }
            // int cnt = 0;
            while (watch.ElapsedMilliseconds < timeLimit)
            {
                // cnt++;
                outp.paths.Shuffle();
                foreach (var it in outp.paths)
                {
                    if (watch.ElapsedMilliseconds > timeLimit ) break;

                    var flow = state.flow_by_id[it.first];
                    add_state(flow, it.second, -1);
                    foreach (var id in it.second)
                    {
                        state.is_node_blocked[state.ordered_edges[id].v] = true;
                    }
                    state.is_node_blocked[flow.target] = false;
                    var path_opt = find_path(flow, (short)(it.second.Count() * 2), SFL, GFL);
                    if (path_opt != null)
                    {
                        add_state(flow, path_opt, 1);
                        it.second = new List<int>(path_opt);
                    }
                    else
                    {
                        add_state(flow, it.second, 1);
                    }
                    foreach (var id in it.second)
                    {
                        state.is_node_blocked[state.ordered_edges[id].v] = false;
                    }
                }
                foreach (var flow in inn.flows)
                {
                    if (watch.ElapsedMilliseconds > timeLimit ) break;

                    if (found[flow.id]) continue;
                    var path_opt = find_path(flow, 50);
                    if (path_opt != null)
                    {
                        found[flow.id] = true;
                        add_state(flow, path_opt, 1);
                        outp.paths.Add(new Pair<int, List<int>>()
                        {
                            first = flow.id,
                            second = path_opt
                        });
                    }
                }
            }
            foreach (var it in outp.paths)
            {
                List<int> path = new List<int>();
                foreach (var x in it.second)
                {
                    path.Add(state.ordered_edges[x].id);
                }
                it.second = new List<int>(path);
            }

            outp.write();
        }
        public static void Main(string[] args)
        {
            (new Generator()).Generate(100, 8000, 4000, 4000, 4000, 1000);
            // inn.Read();
            // solve();
        }
    }
}