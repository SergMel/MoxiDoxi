// https://www.geeksforgeeks.org/activity-selection-problem-greedy-algo-1/
// https://www.geeksforgeeks.org/kruskals-minimum-spanning-tree-algorithm-greedy-algo-2/

using System;
using System.Linq;

namespace Algorithms
{
    public static class KruskalsMinSpanTree
    {

        static bool CheckNode(int[] arr, int v) {
            if (v < 0 || arr.Length <= v) {
                return false;
            }
            return true;
        }

        static int GetRoot(int[] parents, int v) {
            if (!CheckNode(parents, v)) {
                throw new Exception("out of bounds");
            }
            while (parents[v] != v) {
                parents[v] = parents[parents[v]];
                v = parents[v];
            }
            return v;
        }

        static bool IsConnected(int[] parents,int v1, int v2) {
            return GetRoot(parents, v1) == GetRoot(parents, v2);
        }

        static int Union(int[] parents, int[] rank, int v1, int v2) {
            v1 = GetRoot(parents, v1);
            v2 = GetRoot(parents, v2);
            if(v1 == v2) {
                return v1;
            }
            if(rank[v1] > rank[v2]) {
                parents[v2] = v1;                
            } else if(rank[v2] > rank[v1]) {
                parents[v1] = v2;
            } else {
                parents[v2] = v1;
                rank[v1] ++;
            }
            return parents[v1];
        }

        static int[] InitializeParents(int n) {
            return Enumerable.Range(0, n).ToArray();
        } 

        static int[] InitializeRanks(int n) {
            return Enumerable.Repeat(1, n).ToArray();
        }

        // index from 0
        public static int GetWeight(int[] node1, int[] node2, int[] weights) {
            if (node1.Length != node2.Length || node1.Length != weights.Length) {
                throw new Exception("Lengths are not equal");
            }
            if(node1.Length == 0) {
                throw new Exception("empty graph");
            }
            if(node1.Length == 1) {
                return 0;
            }
            var n = node1.Union(node2).Distinct().Count();
            var sorted = Enumerable.Range(0, node1.Length).Select(el => new {w = weights[el], v1 = node1[el], v2 = node2[el]}).OrderBy(el=>el.w).ToArray();
            
            weights = sorted.Select(el=>el.w).ToArray();
            node1 = sorted.Select(el=>el.v1).ToArray();
            node2 = sorted.Select(el=>el.v2).ToArray();
            
            var parents = InitializeParents(n);
            var ranks = InitializeRanks(n);

            var joinedCnt = 0;
            var res = 0;
            for (int i = 0; i < node1.Length && joinedCnt < n ; i++)
            {
                var v1 = node1[i];
                var v2 = node2[i];
                var w = weights[i];

                if (!IsConnected(parents, v1, v2)) {
                    Union(parents, ranks, v1, v2);
                    joinedCnt ++;
                    res += w;
                }
            }

            if (joinedCnt != n -1 ) {
                throw new Exception("Not connected");
            }
            return res;
        }
    }
}