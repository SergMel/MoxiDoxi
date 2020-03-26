package CodeJam.G2019.G1A

import java.util.Stack
import kotlin.math.*


fun readLn() = readLine()!! // string line
fun readInt() = readLn().toInt() // single int
fun readStrings() = readLn().split(" ") // list of strings
fun readInts() = readStrings().map { it.toInt() } // list of ints
fun readLongs() = readStrings().map { it.toLong() } // list of ints
fun readLong() = readLn().toLong() // list of strings
fun writeGoogleAnswer(case: Int, str: String) = println("Case #$case: $str")

data class Item(val r: Int, val c: Int)

fun dfs(map: MutableMap<Int, MutableList<Int>>, visited: HashSet<Int>, cur: Int, n:Int, res:MutableList<Int>):Boolean {
    // println(cur)
    
    if(visited.contains(cur))
    {
        return false
    }
    res.add(cur)
    visited.add(cur)

    if(res.count() == n && map[res[0]]!!.contains(cur))
    {
        return true
    }
    else if (res.count() == n) {
        res.removeAt(res.lastIndex)
        visited.remove(cur)
        return false
    }
    
    map[cur]!!.forEach{itm->        
        if(dfs(map, visited, itm, n, res)){
            return true
        }       
    }
    res.removeAt(res.lastIndex)
    visited.remove(cur)

    return false
}

// https://en.wikipedia.org/wiki/Ore%27s_theorem
fun main(args: Array<String>) {
      val T = readInt()
      val cache = mutableMapOf<Item, MutableList<Int>>()
      for (t in 1..T) {
        val arr = readInts()
        val R = arr[0]
        val C = arr[1]

        if (R <= 3 && C <= 3 || R <= 2 && C <= 4 || R <= 4 && C <= 2) {
            writeGoogleAnswer(t, "IMPOSSIBLE")
            continue
        } else {
            writeGoogleAnswer(t, "POSSIBLE")
        }
        if (cache.containsKey(Item(R, C)))
        {
            var res = cache[Item(R, C)]
            val sb = StringBuilder()
            for (i in 0 until res!!.count()) {
                val r = 1 + res[i] / C
                val c = 1 + res[i] % C
                sb.appendln("$r $c")
            }
            print(sb)
            continue
        }
        else if (cache.containsKey(Item(C, R)))
        {
            var res = cache[Item(C, R)]
            val sb = StringBuilder()
            for (i in 0 until res!!.count()) {
                val c = 1 + res[i] / R
                val r = 1 + res[i] % R
                sb.appendln("$r $c")
            }
            print(sb)
            continue
        }
        val map = mutableMapOf<Int, MutableList<Int>>()

        for (r in 0..R - 1) {
            for (c in 0..C - 1) {
                map[r * C + c] = mutableListOf<Int>()
                for (ri in 0..R - 1) {
                    for (ci in 0..C - 1) {
                        if (ri == r || ci == c || ri - ci == r - c || ri + ci == r + c) {
                            continue
                        }
                        map[r * C + c]!!.add(ri*C + ci)
                    }
                }
            }
        }
        val n = C * R
        for (el in 0 until n) {
            map[el]!!.shuffle()
        }
        
        var res:MutableList<Int>? = null
        for (el in 0 until n) {
            val visited = hashSetOf<Int>()
            res = mutableListOf<Int>()
            if (dfs(map, visited, el, n, res)){
                break
            }
        }
        cache[Item(R, C)] = res!!;
        val sb = StringBuilder()
        for (i in 0 until res.count()) {
            val r = 1 + res[i] / C
            val c = 1 + res[i] % C
            sb.appendln("$r $c")
        }
        
        print(sb)
    }
}
