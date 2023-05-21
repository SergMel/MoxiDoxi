using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/


static class Ext
{

    internal static void Move(this Actions actions, int cnt, Tile from, Tile to)
    {
        actions.Output.Add(String.Format("MOVE {0} {1} {2} {3} {4}", cnt,
            from.x, from.y, to.x, to.y));
    }


    internal static void Spawn(this Actions actions, int cnt, Tile tile)
    {
        actions.Output.Add(String.Format("SPAWN {0} {1} {2}", cnt,
            tile.x, tile.y));
    }

    internal static void Build(this Actions actions, Tile tile)
    {
        actions.Output.Add(string.Format("BUILD {0} {1}", tile.x, tile.y));
    }

    internal static double Dist(this Tile tile, double x, double y)
    {
        var dx = tile.x - x;
        var dy = tile.y - y;
        return dx * dx + dy * dy;
    }
}

public class Actions
{
    public List<string> Output = new List<string>();

    HashSet<Tile> built = new HashSet<Tile>();
    HashSet<Tile> moved = new HashSet<Tile>();
    HashSet<Tile> movedTo = new HashSet<Tile>();

    public bool HasUsed(Tile tile) => built.Contains(tile) || moved.Contains(tile) || movedTo.Contains(tile);

    public bool HasMoved(Tile tile) => moved.Contains(tile);
    public bool HasBuilt(Tile tile) => built.Contains(tile);
    public bool HasMovedTo(Tile tile) => movedTo.Contains(tile);


}

public class Tile
{
    public int x, y, scrapAmount, owner, units;
    public bool recycler, canBuild, canSpawn, inRangeOfRecycler;

    public Tile(int x, int y, int scrapAmount, int owner, int units, bool recycler, bool canBuild, bool canSpawn,
            bool inRangeOfRecycler)
    {
        this.x = x;
        this.y = y;
        this.scrapAmount = scrapAmount;
        this.owner = owner;
        this.units = units;
        this.recycler = recycler;
        this.canBuild = canBuild;
        this.canSpawn = canSpawn;
        this.inRangeOfRecycler = inRangeOfRecycler;
    }

    public bool IsMine => this.owner == ME;
    public bool IsOpponents => this.owner == OPP;
    public bool IsNeutral => this.owner == NOONE;
    public bool IsGrass => this.scrapAmount < 1;
    public int UnitsDiff => this.IsMine ? this.units : (this.IsOpponents ? (-this.units) : 0);


    public override string ToString()
    {
        return string.Format($"x:{x}, y:{y}, owner: {owner}, units: {units}, scrap: {scrapAmount}");
    }

    public override bool Equals(object obj)
    {
        if (obj == null || typeof(Tile) != obj.GetType()) return false;
        return this.Equals(obj as Tile);
    }

    public bool Equals(Tile other) => other != null && other.x == this.x && other.y == this.y;

    public override int GetHashCode() => HashCode.Combine(this.x, this.y);

    public int SqDist(Tile tile)
    {
        var dx = tile.x - this.x;
        var dy = tile.y - this.y;
        return dx * dx + dy * dy;
    }

    public Tile Copy()
    {
        return new Tile(this.x, this.y, this.scrapAmount, this.owner, this.units, this.recycler, this.canBuild, this.canSpawn,
            this.inRangeOfRecycler);


    }
}

public class Player
{
    const int ME = 1;
    const int OPP = 0;
    const int NOONE = -1;

    static Random rnd = new Random();


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



    class State
    {
        List<Tile> tiles = new List<Tile>();
        List<Tile> myTiles = new List<Tile>();
        List<Tile> oppTiles = new List<Tile>();
        List<Tile> neutralTiles = new List<Tile>();
        List<Tile> myUnits = new List<Tile>();
        List<Tile> oppUnits = new List<Tile>();
        List<Tile> myRecyclers = new List<Tile>();
        List<Tile> oppRecyclers = new List<Tile>();
        int height;
        int width;

        public static State Read(int height, int width)
        {
            State ret = new State();
            ret.height = height;
            ret.width = width;


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var inputs = Console.ReadLine().Split(' ');
                    int scrapAmount = int.Parse(inputs[0]);
                    int owner = int.Parse(inputs[1]); // 1 = me, 0 = foe, -1 = neutral
                    int units = int.Parse(inputs[2]);
                    int recycler = int.Parse(inputs[3]);
                    int canBuild = int.Parse(inputs[4]);
                    int canSpawn = int.Parse(inputs[5]);
                    int inRangeOfRecycler = int.Parse(inputs[6]);

                    Tile tile = new Tile(
                            j,
                            i,
                            scrapAmount,
                            owner,
                            units,
                            recycler == 1,
                            canBuild == 1,
                            canSpawn == 1,
                            inRangeOfRecycler == 1);

                    ret.tiles.Add(tile);

                    if (tile.owner == ME)
                    {
                        ret.myTiles.Add(tile);
                        if (tile.units > 0)
                        {
                            ret.myUnits.Add(tile);
                        }
                        else if (tile.recycler)
                        {
                            ret.myRecyclers.Add(tile);
                        }
                    }
                    else if (tile.owner == OPP)
                    {
                        ret.oppTiles.Add(tile);
                        if (tile.units > 0)
                        {
                            ret.oppUnits.Add(tile);
                        }
                        else if (tile.recycler)
                        {
                            ret.oppRecyclers.Add(tile);
                        }
                    }
                    else
                    {
                        ret.neutralTiles.Add(tile);
                    }
                }
            }

            return ret;

        }

        public List<Tile> MyTiles => this.myTiles;
        public List<Tile> MyUnits => this.myUnits;
        public List<Tile> OppUnits => this.oppUnits;
        public List<Tile> OppTiles => this.oppTiles;
        public List<Tile> NeutralTiles => this.neutralTiles;
        public int Height => this.height;
        public int Width => this.width;


        private bool checkCondition(Tile tile, bool includeOpp, bool includeOppUnit, bool includeNeutral, bool includeMine, bool includeMineUnit)
        {
            if (tile.recycler || tile.IsGrass) return false;
            if (tile.inRangeOfRecycler && tile.scrapAmount == 1) return false;

            if (includeOpp && tile.IsOpponents && tile.units == 0) return true;
            if (includeNeutral && tile.IsNeutral) return true;
            if (includeOppUnit && tile.IsOpponents && tile.units > 0) return true;
            if (includeMine && tile.IsMine && tile.units == 0) return true;
            if (includeMineUnit && tile.IsMine && tile.units > 0) return true;

            return false;
        }

        public IEnumerable<Tile> GetNbs(Tile tile, bool includeOppWithoutUnit, bool includeOppUnit, bool includeNeutral,
            bool includeMineWithoutUnit, bool includeMineUnit)
        {
            int x = tile.x;
            int y = tile.y;

            if (x - 1 >= 0)
            {
                var leftTile = this.tiles[y * width + x - 1];
                if (checkCondition(leftTile, includeOppWithoutUnit, includeOppUnit, includeNeutral, includeMineWithoutUnit, includeMineUnit)) yield return leftTile;
            }
            if (x + 1 < width)
            {
                var rightTile = this.tiles[y * width + x + 1];
                if (checkCondition(rightTile, includeOppWithoutUnit, includeOppUnit, includeNeutral, includeMineWithoutUnit, includeMineUnit)) yield return rightTile;
            }
            if (y - 1 >= 0)
            {
                var upperTile = this.tiles[(y - 1) * width + x];
                if (checkCondition(upperTile, includeOppWithoutUnit, includeOppUnit, includeNeutral, includeMineWithoutUnit, includeMineUnit)) yield return upperTile;
            }
            if (y + 1 < height)
            {
                var bottomTile = this.tiles[(y + 1) * width + x];
                if (checkCondition(bottomTile, includeOppWithoutUnit, includeOppUnit, includeNeutral, includeMineWithoutUnit, includeMineUnit)) yield return bottomTile;
            }
        }

        public int ScrapCollectable(Tile tile) => this.GetAll(tile)
            .Select(el => el.scrapAmount > tile.scrapAmount ? tile.scrapAmount : el.scrapAmount).Sum();


        public int GetUnitsDiffCount(Tile tile, int depth = 2)
        {

            var dpthList = new Dictionary<Tile, int>();

            Queue<Tile> q = new Queue<Tile>();
            q.Enqueue(tile);
            dpthList[tile] = depth;
            var ret = 0;
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                ret += cur.UnitsDiff;
                if (dpthList[cur] == 0) continue;

                foreach (var next in this.GetAll(cur))
                {
                    if (dpthList.ContainsKey(next)) continue;
                    dpthList[next] = dpthList[cur] - 1;
                    q.Enqueue(next);
                }
            }

            Console.Error.WriteLine($"x:{tile.x}, y:{tile.y}, diff: {ret}");
            return ret;
        }

        public IEnumerable<Tile> GetOpponentOwned(Tile tile) => this.GetNbs(tile, true, true, false, false, false);
        public IEnumerable<Tile> GetMine(Tile tile) => this.GetNbs(tile, false, false, false, true, true);
        public IEnumerable<Tile> GetNotMine(Tile tile) => this.GetNbs(tile, true, true, true, false, false);
        public IEnumerable<Tile> GetNeutral(Tile tile) => this.GetNbs(tile, false, false, true, false, false);

        public IEnumerable<Tile> GetAll(Tile tile) => this.GetNbs(tile, true, true, true, true, true);

        public int GetNextMaximumOpponentsCount(Tile tile)
        {
            int ret = 0;
            if (tile.IsMine) ret = -tile.units;

            foreach (var nb in this.GetAll(tile))
            {
                if (nb.recycler)
                {
                    continue;
                }

                if (nb.IsOpponents)
                {
                    ret += nb.units;

                }
            }
            return ret < 0? 0:ret;
        }

        public (
            int oppUnits,
            int directions, int freeDir, int emptyOpDir, int occOpDir, int mineDir,
            int oppRecyclers, int mineRecyclers)
            GetNbDetails(Tile tile)
        {

            int oppUnits = 0;
            int directions = 0;
            int freeDir = 0;
            int emptyOpDir = 0;
            int occOpDir = 0;
            int mineDir = 0;
            int oppRecyclers = 0;
            int mineRecyclers = 0;

            foreach (var nb in this.GetAll(tile))
            {
                if (nb.IsOpponents && nb.recycler)
                {
                    oppRecyclers++;
                    continue;
                }
                if (nb.IsMine && nb.recycler)
                {
                    mineRecyclers++;
                    continue;
                }

                directions++;
                if (nb.IsOpponents)
                {
                    oppUnits += nb.units;

                    emptyOpDir += (nb.units > 0) ? 0 : 1;
                    occOpDir += (nb.units > 0) ? 1 : 0;

                }
                else if (nb.IsMine)
                {
                    mineDir += 1;
                }
                else
                {
                    freeDir += 1;
                }
            }
            var ret = (oppUnits, directions, freeDir, emptyOpDir, occOpDir, mineDir, oppRecyclers, mineRecyclers);

            Console.Error.WriteLine($"Tile:{tile}, result: {ret}");
            return ret;
        }

        public (
            int oppMaxUnits,
            int mineMaxUnits
            )
            GetTile1StepAnalysis(Tile tile)
        {

            int oppMaxUnits = tile.IsOpponents ? tile.units : 0;
            int mineMaxUnits = tile.IsMine ? tile.units : 0;

            foreach (var nb in this.GetAll(tile))
            {

                if (nb.IsOpponents)
                {
                    oppMaxUnits += nb.units;

                }
                else if (nb.IsMine)
                {
                    mineMaxUnits += nb.units;
                }
                else
                {
                }
            }
            var ret = (oppMaxUnits, mineMaxUnits);
            return ret;
        }

        public IEnumerable<(Tile Tile,
            int oppUnits,
            int directions, int freeDir, int emptyOpDir, int occOpDir, int mineDir,
            int oppRecyclers, int mineRecyclers)>
            GetSpawnableTilesWithDetails()
        {


            foreach (var tile in this.myTiles.Where(el => el.canSpawn && !(el.inRangeOfRecycler && el.scrapAmount <= 1)))
            {
                var dtls = GetNbDetails(tile);


                yield return (tile, dtls.oppUnits, dtls.directions, dtls.freeDir, dtls.emptyOpDir, dtls.occOpDir, dtls.mineDir, dtls.oppRecyclers, dtls.mineRecyclers);
            }

        }


        public IEnumerable<Tile> GetSpawnableTiles() =>
            this.myTiles.Where(el => el.canSpawn && !(el.inRangeOfRecycler && el.scrapAmount <= 1));
        public IEnumerable<Tile> GetSpawnableTilesCloseToNotMine() => this.myTiles.Where(el =>
            el.canSpawn &&
            !(el.inRangeOfRecycler && el.scrapAmount <= 1) &&
            this.GetNotMine(el).Count() > 0
        );

        public IEnumerable<Tile> GetSuitableForOptimalBuildTiles() =>
            this.myTiles.Where(el => el.canBuild && !el.inRangeOfRecycler)
                .OrderByDescending(el => this.ScrapCollectable(el));

        public IEnumerable<Tile> GetBuildableTilesOrderedBy2() =>
            this.myTiles.Where(el => el.canBuild)
            .Where(el => GetOpponentOwned(el).Count() > 0)
            .OrderByDescending(el => el.inRangeOfRecycler)
            .ThenByDescending(el => this.GetOpponentOwned(el).Count())
            .ThenByDescending(el => this.ScrapCollectable(el));

        public Tile ClosestOpp(Tile tile)
        {
            var visited = new HashSet<Tile>();

            Queue<Tile> q = new Queue<Tile>();
            q.Enqueue(tile);
            visited.Add(tile);

            while (q.Count > 0)
            {
                var cur = q.Dequeue();

                if (cur.IsOpponents)
                {
                    return cur;
                }

                foreach (var next in this.GetAll(cur))
                {
                    if (visited.Contains(next)) continue;
                    visited.Add(next);
                    q.Enqueue(next);
                }
            }


            return null;
        }

        public Tile ClosestNeutral(Tile tile)
        {
            var visited = new HashSet<Tile>();

            Queue<Tile> q = new Queue<Tile>();
            q.Enqueue(tile);
            visited.Add(tile);

            while (q.Count > 0)
            {
                var cur = q.Dequeue();

                if (!cur.IsOpponents && !cur.IsMine)
                {
                    return cur;
                }

                foreach (var next in this.GetAll(cur))
                {
                    if (visited.Contains(next)) continue;
                    visited.Add(next);
                    q.Enqueue(next);
                }
            }


            return null;
        }

        public (double, double) MyCenter()
        {
            double x = 0;
            double y = 0;

            foreach (var tile in this.MyTiles)
            {
                x += tile.x;
                y += tile.y;
            }
            return (x / MyTiles.Count, y / MyTiles.Count);
        }

        public (double, double) OppCenter()
        {
            double x = 0;
            double y = 0;

            foreach (var tile in this.OppTiles)
            {
                x += tile.x;
                y += tile.y;
            }
            return (x / OppTiles.Count, y / OppTiles.Count);
        }

    }


    class SimpleStrategy
    {

        State state;
        int myMatter;

        public SimpleStrategy(State state, int myMatter)
        {
            this.state = state;
            this.myMatter = myMatter;
        }

        public Actions Actions = new Actions();

        public int dx;
        public int middle;

        public bool Good()
        {
            return true;
        }

        public int GetScore(Tile tile, int requiredUnits)
        {

            if (tile.IsMine && requiredUnits > 0) return 2;

            if (tile.IsMine && requiredUnits == 0) return 0;
            if (tile.IsNeutral && requiredUnits == 1) return 1;
            if (tile.IsNeutral && requiredUnits > 1) return 2;

            return 2;
        }

        public int GetRequiredAdditionalUnits(Tile tile, int oppUnits)
        {
            if (tile.IsMine && oppUnits <= tile.units) return 0;
            
            if (tile.IsMine) return oppUnits - tile.units;

            return oppUnits + 1;
        }

        public void FindSolutions(Tile tile)
        {
            // var us = tile.UnitsDiff;
            var nbs = this.state.GetAll(tile)
                .Select(el => 
                    {
                        var maxOppCount  = this.state.GetNextMaximumOpponentsCount(el);
                        var score = GetScore(el, maxOppCount);
                        return (tile: el,additionalUnits: GetRequiredAdditionalUnits(el, maxOppCount), score: score);
                    }
                )
                .Where(el => el.additionalUnits > 0)
                .ToList();

            var dp = new int[nbs.Count + 1, tile.units];

            for (int movedUnits = 1; movedUnits < tile.units; movedUnits++)
            {
                for (int i = 1; i < nbs.Count; i++)
                {
                    var nb = nbs[i];
                    var units = nb.additionalUnits;

                    var candidate = units > movedUnits ? 0 : (dp[i - 1, units - movedUnits] + GetScore(nb.tile, units));
                    if (candidate > dp[i-1, movedUnits]) {
                        dp[i, movedUnits] = candidate;
                    } else {
                        dp[i, movedUnits] = dp[i - 1, movedUnits];
                    }

                }
            }

            int tileRequiredUnits = GetRequiredAdditionalUnits(tile, this.state.GetNextMaximumOpponentsCount(tile));
            List<(int price, int score )> sols = new List<(int price, int score)>();
            for (int movedUnits = 1; movedUnits < tile.units; movedUnits++)
            {
                int price = tileRequiredUnits
            }

            foreach (var nb in nbs)
            {

            }

        }

        private bool DirectMoveTo(Tile fromTile, Tile toTile)
        {
            var anTo = this.state.GetTile1StepAnalysis(toTile);
            var anFrom = this.state.GetTile1StepAnalysis(fromTile);


            if (anTo.oppMaxUnits + anFrom.oppMaxUnits < fromTile.units)
            {
                this.Actions.Move(anTo.oppMaxUnits + 1, fromTile, toTile);
                return true;
            }

            return false; ;
        }

        private bool SpawnInPlace(Tile tile)
        {
            var an = this.state.GetTile1StepAnalysis(tile);

            if (an.oppMaxUnits <= tile.units)
            {
                return true;

            }
            int countToSpawn = an.oppMaxUnits - tile.units;

            if (countToSpawn * 10 > this.myMatter) return false;

            this.Actions.Spawn(countToSpawn, tile);
            this.myMatter -= countToSpawn * 10;
            return true;
        }

        public void MoveToCenter(double cx, double cy, double dist, Tile tile,
            List<Tile> opNbs, List<Tile> freeNbs, List<Tile> myNbs)
        {

            if (this.Actions.HasMoved(tile)) return;
            var curCnt = tile.units;
            foreach (var nb in opNbs.OrderBy(el => el.Dist(cx, cy)))
            {
                if (this.Actions.HasBuilt(nb)) continue;
                if (curCnt > nb.units)
                {
                    this.Actions.Move(nb.units, tile, nb);
                    curCnt -= nb.units;
                }
            }
            while (curCnt > 0)
            {
                foreach (var nb in freeNbs.OrderBy(el => el.Dist(cx, cy)))
                {
                    if (this.Actions.HasBuilt(nb)) continue;
                    if (this.DirectMoveTo(tile, nb)) return;
                }

                foreach (var nb in freeNbs.OrderBy(el => el.Dist(cx, cy)))
                {
                    if (this.Actions.HasBuilt(nb)) continue;
                    if (this.DirectMoveTo(tile, nb)) return;
                }
            }
            if (this.SpawnInPlace(tile)) return;
        }


        public void MoveToCenter(double cx, double cy, double dist, Tile tile,
            List<Tile> opNbs, List<Tile> freeNbs)
        {

            if (this.Actions.HasMoved(tile)) return;

            foreach (var nb in opNbs.OrderBy(el => el.Dist(cx, cy)))
            {
                if (this.Actions.HasMovedTo(nb)) continue;
                if (this.Actions.HasBuilt(nb)) continue;
                if (this.DirectMoveTo(tile, nb)) return;
            }
            foreach (var nb in freeNbs.OrderBy(el => el.Dist(cx, cy)))
            {
                if (this.Actions.HasMovedTo(nb)) continue;
                if (this.Actions.HasBuilt(nb)) continue;
                if (this.DirectMoveTo(tile, nb)) return;
            }
            if (this.SpawnInPlace(tile)) return;
        }

        public void MoveToCenter()
        {

            (double cx, double cy) = this.state.OppCenter();
            var myunits = this.state.MyUnits
                .Select(el => (
                    itm: el,
                    dist: el.Dist(cx, cy),
                    opNbs: this.state.GetOpponentOwned(el).ToList(),
                    freeNbs: this.state.GetNeutral(el).ToList()
                )).ToList();

            foreach (var unit in myunits)
            {
                MoveToCenter(cx, cy, unit.dist, unit.itm, unit.opNbs, unit.freeNbs);
            }

            foreach (var unit in myunits)
            {
                MoveToCenter(cx, cy, unit.dist, unit.itm, unit.opNbs, unit.freeNbs);
            }
        }

        public List<String> Move()
        {

            (double cx, double cy) = this.state.OppCenter();
            var myunits = this.state.MyUnits
                .Select(el => (
                    itm: el,
                    dist: el.Dist(cx, cy),
                    opNbs: this.state.GetOpponentOwned(el).ToList(),
                    freeNbs: this.state.GetNeutral(el).ToList()
                )).ToList();

            HashSet<Tile> remaining = myunits.Select(el => el.itm).ToHashSet();

            foreach (var el in myunits.Where(el => this.state.GetOpponentOwned(el.itm).Count())

        }

    }

    class ExtremeStrategy
    {

        State state;
        int myMatter;

        public ExtremeStrategy(State state, int myMatter)
        {
            this.state = state;
            this.myMatter = myMatter;
        }

        public int dx;
        public int middle;

        public bool Good()
        {
            if (state.MyUnits.Count <= 4) return true;

            return false;

        }

        public List<String> Move()
        {

            List<String> actions = new List<String>();
            int matter = myMatter;

            var possibleTiles = state.GetSpawnableTilesWithDetails()
                .OrderBy(el => el.Tile.units)
                .ThenByDescending(el => el.emptyOpDir + el.freeDir)
                .ThenByDescending(el => el.directions);
            // Console.Error.WriteLine($"Possible tiles count: {possibleTiles.Count()}");
            foreach (var tile in possibleTiles)
            {
                if (tile.oppUnits >= matter / 10)
                {
                    continue;
                }
                var cnt = 1 + tile.oppUnits / 10;
                actions.Add(String.Format("SPAWN {0} {1} {2}", cnt, tile.Tile.x, tile.Tile.y));
                matter -= cnt * 10;
            }


            foreach (var tile in this.state.MyUnits)
            {
                var tiled = state.GetNbDetails(tile);
                var nbs = this.state.GetAll(tile)
                    .OrderByDescending(el => el.IsOpponents)
                    .ThenBy(el => el.IsMine);
                foreach (var nb in nbs)
                {
                    var nbd = state.GetNbDetails(nb);
                    var tou = nb.IsOpponents ? nb.units : 0;
                    tou += nbd.oppUnits;
                    if (tou < tile.units)
                    {
                        actions.Add(String.Format("MOVE {0} {1} {2} {3} {4}", tile.units, tile.x, tile.y, nb.x, nb.y));
                        break;
                    }
                    else if (tile.canSpawn && (tiled.oppUnits + nbd.oppUnits) < (tile.units + matter / 10))
                    {
                        var toAdd = 1 + (tiled.oppUnits + nbd.oppUnits - tile.units) / 10;
                        actions.Add(String.Format("MOVE {0} {1} {2} {3} {4}", tile.units, tile.x, tile.y, nb.x, nb.y));
                        actions.Add(String.Format("SPAWN {0} {1} {2}", toAdd, tile.x, tile.y));
                        matter -= toAdd * 10;
                    }
                }


            }

            return actions;

        }

    }


    class RaysStrategy
    {

        State state;
        int myMatter;

        public RaysStrategy(State state, int myMatter)
        {
            this.state = state;
            this.myMatter = myMatter;
        }

        public int dx;
        public int middle;

        public bool Good()
        {
            if (state.OppUnits.Count < 1 || state.MyUnits.Count < 1) return false;
            var dx1 = state.OppUnits.Select(el => el.x).Max() - state.MyUnits.Select(el => el.x).Min();
            var dx2 = state.OppUnits.Select(el => el.x).Min() - state.MyUnits.Select(el => el.x).Max();
            dx = Math.Sign(dx1);

            middle = dx > 0
                ? (state.OppUnits.Select(el => el.x).Min() + state.MyUnits.Select(el => el.x).Max()) / 2
                : (state.MyUnits.Select(el => el.x).Min() + 1 + state.OppUnits.Select(el => el.x).Max()) / 2;

            Console.Error.WriteLine($"dx: {dx}");
            Console.Error.WriteLine($"middle: {middle}");
            return dx1 * dx2 > 0;
        }

        public List<String> Move()
        {

            List<String> actions = new List<String>();

            var unitsCount = state.MyUnits.Count;

            var dist = (this.state.Height - unitsCount) / unitsCount;
            var extraDist = (this.state.Height - unitsCount) % unitsCount;

            List<(int x, int y)> poss = new List<(int, int)>();
            int prev = -1;
            for (int i = 0; i < unitsCount; ++i)
            {
                var add = (i + extraDist) >= dist ? 1 : 0;
                var new_y = prev == -1 ? 0 : (prev + dist + add + 1);
                prev = new_y;
                poss.Add((middle, new_y));
            }

            int ii = 0;
            foreach (var unt in this.state.MyUnits.OrderBy(el => el.y))
            {
                actions.Add(String.Format("MOVE {0} {1} {2} {3} {4}", unt.units, unt.x, unt.y, poss[ii].x, poss[ii].y));
                ii++;
            }

            int matter = this.myMatter;

            foreach (var mytile in this.state.GetSuitableForOptimalBuildTiles())
            {
                if (matter > 10)
                {
                    actions.Add(String.Format("BUILD {0} {1}", mytile.x, mytile.y));
                    matter -= 10;
                }
            }

            return actions;

        }

    }


    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int width = int.Parse(inputs[0]);
        int height = int.Parse(inputs[1]);

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            var myMatter = int.Parse(inputs[0]);
            var oppMatter = int.Parse(inputs[1]);
            var state = State.Read(height, width);

            List<String> actions = new List<String>();

            ExtremeStrategy extrStrategy = new ExtremeStrategy(state, myMatter);

            RaysStrategy strategy = new RaysStrategy(state, myMatter);
            if (extrStrategy.Good())
            {
                Console.Error.WriteLine("Extreame strategy");
                actions = extrStrategy.Move();
            }
            else if (strategy.Good())
            {
                Console.Error.WriteLine("Rays strategy");
                actions = strategy.Move();
            }
            else
            {
                Console.Error.WriteLine("Default strategy");
                int toSpawnMatter = (3 * myMatter) / 4;
                int toBuildMatter = myMatter - toSpawnMatter;

                if (toSpawnMatter >= 10)
                {
                    var spawnCandidates = state.GetSpawnableTiles()
                        .Select(el => new { score = state.GetUnitsDiffCount(el), item = el })
                        .Where(el => el.score <= 2)
                        .OrderBy(el => el.score)
                        .ToList();

                    Console.Error.WriteLine(spawnCandidates.Count);

                    if (spawnCandidates.Count > 0)
                    {
                        foreach (var spCandidate in spawnCandidates)
                        {
                            var cntToSpawn = 3 - spCandidate.score;
                            // Console.Error.WriteLine($"cntToSpawn: {cntToSpawn}");
                            if (toSpawnMatter / 10 > cntToSpawn)
                            {
                                toSpawnMatter -= cntToSpawn * 10;
                                actions.Add(String.Format("SPAWN {0} {1} {2}", cntToSpawn, spCandidate.item.x, spCandidate.item.y));
                            }

                        }
                    }
                }


                foreach (var buildTile in state.GetBuildableTilesOrderedBy2())
                {
                    if (toBuildMatter < 10) break;

                    actions.Add(String.Format("BUILD {0} {1}", buildTile.x, buildTile.y));
                    toBuildMatter -= 10;
                }


                HashSet<Tile> used = new HashSet<Tile>();
                foreach (Tile tile in state.MyUnits)
                {

                    List<int> dirs = new List<int>() { 0, 0, 0, 0 };

                    if (tile.units / 4 > 0)
                    {
                        dirs[0] = tile.units / 4;
                        dirs[1] = tile.units / 4;
                        dirs[2] = tile.units / 4;
                        dirs[3] = tile.units / 4;
                    }
                    var rem = tile.units % 4;
                    if (rem >= 1) dirs[0]++;
                    if (rem >= 2) dirs[1]++;
                    if (rem >= 3) dirs[2]++;

                    var allnbs = state.GetAll(tile).ToList();

                    foreach (var amount in dirs.Where(el => el > 0))
                    {

                        Tile target = null; // TODO: pick a destination
                        foreach (var optile in state.GetOpponentOwned(tile))
                        {
                            if (!used.Contains(optile))
                            {
                                target = optile;
                                used.Add(optile);
                                break;
                            }
                        }
                        if (target == null)
                        {
                            foreach (var optile in state.GetNeutral(tile))
                            {
                                if (used.Contains(optile)) continue;
                                target = optile;
                                used.Add(optile);
                                break;

                            }
                        }


                        if (target == null && allnbs.Count > 0)
                        {
                            target = state.ClosestOpp(tile);

                        }
                        if (target == null)
                        {
                            target = state.ClosestNeutral(tile);

                        }
                        // Console.Error.WriteLine("Movement");           
                        // Console.Error.WriteLine(target);
                        // Console.Error.WriteLine(tile);
                        if (target != null)
                        {

                            actions.Add(String.Format("MOVE {0} {1} {2} {3} {4}", amount, tile.x, tile.y, target.x, target.y));
                        }
                    }
                }
            }
            if (actions.Count <= 0)
            {
                Console.WriteLine("WAIT");
            }
            else
            {
                Console.WriteLine(string.Join(";", actions.ToArray()));
                //actions.stream().collect(Collectors.joining(";"));
            }
        }
    }
}
