package Archive

import Mod.mdiv
import Mod.mmod
import Mod.mmult
import Mod.mplus
import Mod.mpow
import Mod.setMod
import NumTh.inverse
import kotlin.collections.*
import kotlin.math.*
import java.util.Queue
import java.util.LinkedList
import java.util.PriorityQueue
import NumTh.gcd

fun readLn() = readLine()!!.trim() // string line
fun readInt() = readLn().trim().toInt() // single int
fun readStrings() = readLn().trim().split(" ") // list of strings
fun readInts() = readStrings().map { it.trim().toInt() } // list of ints
fun readLongs() = readStrings().map { it.trim().toLong() } // list of ints
fun readLong() = readLn().trim().toLong() // list of strings
fun writeGoogleAnswer(case: Int, str: String) = println("Case #$case: $str")

object NumTh {
    fun euclideanExt(la: Long, lb: Long): Triple<Long, Long, Long> {
        val signa = sign(la.toDouble()).toLong()
        val signb = sign(lb.toDouble()).toLong()

        var a = abs(la)
        var b = abs(lb)
        val reverse = a > b
        if (reverse) {
            a = b.also { b = a }
        }

        var m11 = 1L
        var m12 = 0L
        var m21 = 0L
        var m22 = 1L

        while (a > 0) {
            val tmp = b / a
            a = (b % a).also { b = a }
            m11 = (m12 - m11 * tmp).also { m12 = m11.also { m21 = (m22 - m21 * tmp).also { m22 = m21 } } }
        }
        var x = m12
        var y = m22
        var d = b
        if (reverse) {
            x = x.also { y = x }
        }
        return Triple(signa * x, signb * y, d)
    }

    fun euclideanExt(la: Int, lb: Int): Triple<Int, Int, Int> {
        val res = euclideanExt(la.toLong(), lb.toLong())
        return Triple(res.first.toInt(), res.second.toInt(), res.third.toInt())
    }

    public fun inverse(la: Long, mod: Long): Long {
        val res = euclideanExt(la mmod mod, mod).first
        return if (res < 0) res + mod else res
    }

    public fun inverse(la: Int, mod: Int): Int {
        val res = euclideanExt(la mmod mod, mod).first
        return if (res < 0) res + mod else res
    }

    infix fun Long.gcd(lb: Long) = euclideanExt(this, lb).third

    infix fun Int.gcd(lb: Int) = euclideanExt(this, lb).third

    fun chinese(qs: List<Int>, ans: List<Int>): Pair<Int, Int> {

        if (qs.count() == 1) {
            return Pair(qs[0], ans[0] % qs[0])
        }
        val rng1 = 0 until qs.count() / 2
        val res1 = chinese(qs.slice(rng1), ans.slice(rng1))
        val rng2 = qs.count() / 2 until qs.count()
        val res2 = chinese(qs.slice(rng2), ans.slice(rng2))
        // println(res1)
        // println( res2 )
        // println( inverse(res2.first, res1.first) )
        // println( inverse(res1.first, res2.first) )
        val mod = res1.first * res2.first
        val res = (res1.second * res2.first * inverse(res2.first, res1.first) + res2.second * res1.first * inverse(res1.first, res2.first)) % mod
        return Pair(mod, res)
    }

    public fun Long.getPDivisors(): MutableList<Long> {
        if (this == 0L) {
            throw Exception()
        }
        var n = abs(this)
        val res = mutableListOf<Long>()
        while (n % 2L == 0L) {
            res.add(2L)
            n /= 2L
        }

        var cur = 3L
        while (n > 1) {
            if (n % cur != 0L) {
                cur += 2
                continue
            }
            while (n % cur == 0L) {
                res.add(cur)
                n /= cur
            }
            cur += 2
        }
        return res
    }

    public fun Long.getAllDivisors(): MutableList<Long> {
        if (this == 0L) {
            throw Exception()
        }
        val n = abs(this)
        val res = mutableListOf<Long>()
        var v = 2L
        while (v * v < n) {
            if (n % v == 0L) {
                res.add(v)
            }
            v++
        }
        if (v * v == n) {
            res.add(v)
        }
        return res
    }

    public fun Int.getAllDivisors(): MutableList<Int> {
        if (this == 0) {
            throw Exception()
        }
        val n = abs(this)
        val res = mutableListOf<Int>()
        var v = 2
        while (v * v < n) {
            if (n % v == 0) {
                res.add(v)
            }
            v++
        }
        if (v * v == n) {
            res.add(v)
        }
        return res
    }

    public fun Int.getPDivisors(): MutableList<Int> {
        if (this == 0) {
            throw Exception()
        }
        var n = abs(this)
        val res = mutableListOf<Int>()
        while (n % 2 == 0) {
            res.add(2)
            n /= 2
        }

        var cur = 3
        while (n > 1) {
            if (n % cur != 0) {
                cur += 2
                continue
            }
            while (n % cur == 0) {
                res.add(cur)
                n /= cur
            }
            cur += 2
        }
        return res
    }
}

object Mod {
    private var _div = 2L

    public val div: Long
        get() = _div

    public fun setMod(div: Long) {
        if (div <= 0) {
            throw Exception()
        }
        _div = div
    }

    public fun setMod(div: Int) {
        if (div <= 0) {
            throw Exception()
        }
        _div = div.toLong()
    }

    public fun Long.mmod(): Long {
        return if (this < 0) _div + (this % _div) else this % _div
    }

    public fun Int.mmod(): Int {
        return (if (this < 0) _div + (this % _div) else this % _div).toInt()
    }

    public infix fun Long.mrem(v: Long): Long {
        if (v == 0L) {
            throw Exception()
        }
        return abs(this) % v
    }

    public fun Long.mpow(exp: Long, divisor: Long): Long {
        if (this == 0L && exp <= 0) {
            throw Exception()
        }

        if (exp == 0L) {
            return 1L
        }
        if (exp == 1L) {
            return this
        }
        var lv = this
        var lexp = exp

        var res = 1L
        while (lexp > 0) {
            res = (res * if (lexp % 2 == 1L) lv else 1L) % divisor
            lv = (lv * lv) % divisor
            lexp /= 2L
        }
        return res
    }

    public fun Int.mpow(exp: Int, divisor: Long): Int {

        return (this.toLong()).mpow(exp.toLong(), divisor.toLong()).toInt()
    }

    public fun Long.mmult(v: Long, mod: Long): Long {
        if (mod <= 0) {
            throw Exception()
        }
        return (this * v) mmod mod
    }

    public fun Int.mmult(v: Int, mod: Int): Int {
        return this.toLong().mmult(v.toLong(), mod.toLong()).toInt()
    }

    public infix fun Long.mmult(v: Long): Long {
        return this.mmult(v, _div)
    }

    public infix fun Int.mmult(v: Int): Int {
        return this.toLong().mmult(v.toLong(), _div).toInt()
    }

    public fun Long.mdiv(v: Long, mod: Long): Long {
        if (mod <= 0) {
            throw Exception()
        }
        return this.mmult(inverse(v, mod), mod)
    }

    public fun Int.mdiv(v: Int, mod: Int): Int {
        if (mod <= 0) {
            throw Exception()
        }
        return this.toLong().mdiv(v.toLong(), mod.toLong()).toInt()
    }

    public infix fun Long.mdiv(v: Long): Long {
        return this.mdiv(v, _div)
    }

    public infix fun Int.mdiv(v: Int): Int {
        return this.toLong().mdiv(v.toLong(), _div).toInt()
    }

    public fun Long.mminus(v: Long, mod: Long): Long {
        return this.mplus(-1L*v, mod)
    }

    public fun Int.mminus(v: Int, mod: Int): Int {
        return this.mplus(-1*v, mod)
    }

    public fun Long.mplus(v: Long, mod: Long): Long {
        if (mod <= 0) {
            throw Exception()
        }
        return (this + v) mmod mod
    }

    public fun Int.mplus(v: Int, mod: Int): Int {
        return this.toLong().mplus(v.toLong(), mod.toLong()).toInt()
    }

    public infix fun Long.mminus(v: Long): Long {
        return this.mplus(-1L*v, _div)
    }

    public infix fun Int.mminus(v: Int): Int {
        return this.mplus(-1*v, _div.toInt()).toInt()
    }

    public infix fun Long.mplus(v: Long): Long {
        return this.mplus(v, _div)
    }

    public infix fun Int.mplus(v: Int): Int {
        return this.mplus(v, _div.toInt())
    }

    public infix fun Long.mmod(mod: Long): Long {
        if (mod <= 0L) {
            throw Exception()
        }
        return if (this < 0) mod + (this % mod) else this % mod
    }

    public infix fun Int.mmod(mod: Int): Int {
        if (mod <= 0) {
            throw Exception()
        }
        return (if (this < 0) mod + (this % mod) else this % mod).toInt()
    }

    public infix fun Long.mpow(exp: Long): Long {
        return this.mpow(exp, _div)
    }

    public infix fun Int.mpow(exp: Int): Int {
        return this.mpow(exp, _div)
    }
}

object Comb {

    var facts: LongArray = LongArray(0)
        private set

    public fun Int.mprecompute() {
        if (this < 0) {
            throw Exception()
        }
        facts = LongArray(this + 1)
        facts[0] = 1L
        for (i in 1..this) {
            facts[i] = facts[i - 1] mmult i.toLong()
        }
    }

    public fun Long.mfact(mod: Long): Long {
        if (this == 1L || this == 0L) {
            return 1L
        }

        var res = 1L
        for (i in 2L..this) {
            res = (res * i) mmod mod
        }
        return res
    }

    public fun Long.mfact(): Long {
        return this.mfact(Mod.div)
    }

    public fun Int.mfact(mod: Int): Int {
        return this.toLong().mfact(mod.toLong()).toInt()
    }

    public fun Int.fact(): Int {
        return this.mfact(Mod.div.toInt())
    }
}

fun getMaxBit(v: Long): Int {
    var res = 0
    var cur = v
    while (cur > 0) {
        res++
        cur = cur shr 1
    }
    return res
}

fun isPrime(vl: Long): Boolean {

    var test = 2L
    while (test * test <= vl && (vl % test != 0L)) {
        test++
    }
    return (vl == 2L) || (vl % test) != 0L
}

fun getBit(v: Long, s: Int): Long {
    return (v shr s) and 1
}

fun getNext1(v1: Long, s: Int): Int {
    var cur = s
    while (getBit(v1, cur) == 0L && cur < 64) {
        cur++
    }
    return cur
}

fun replace10(v1: Long, v2: Long, st: Int, reverse: Boolean, res: MutableList<String>): Int {
    var onep1 = getNext1(v1, st + 1)
    var onep2 = getNext1(v2, st + 1)
    var last = min(onep1, onep2)
    if (last == 64) {
        last = st + 2
    }
    last -= 1
    for (i in st..last - 1) {
        res.add(if (reverse) "S" else "W")
    }
    res.add(if (reverse) "N" else "E")
    return last + 1
}

fun replace11(v1: Long, v2: Long, st: Int, reverse: Boolean, res: MutableList<String>): Int? {
    var onep1 = getNext0(v1, st + 1)
    var onep2 = getNext0(v2, st + 1)
    if (onep1 != onep2) {
        return null
    }
    res.add(if (reverse) "S" else "W")

    var last = onep1
    for (i in (st + 1)..(last - 1)) {
        res.add(if (reverse) "E" else "N")
    }
    res.add(if (reverse) "N" else "E")
    return last + 1
}

fun getNext0(v1: Long, s: Int): Int {
    var cur = s
    while (getBit(v1, cur) == 1L && cur < 64) {
        cur++
    }
    return cur
}

fun pow(v: Long, pow: Long, mod: Long): Long {

    if (pow == 0L) {
        return 1L
    }
    if (pow == 1L) {
        return v
    }
    var res = if (pow % 2L == 1L) v else 1L
    val tmp = pow((v*v) % mod, pow / 2, mod)
    res *= (tmp * tmp) % mod
    res = res % mod
    return res
}

class UF {
    val rank = HashMap<Int, Int>()
    val parent = HashMap<Int, Int>()
    val counts = HashMap<Int, Int>()
    var groupCnt = 0

    fun add(v: Int) {
        if (!rank.containsKey(v)) {
            groupCnt ++
            // println("$v: $groupCnt")
            rank[v] = 1
            parent[v] = v
            counts[v] = 1
        }
    }

    fun getParent(v: Int): Int {

        add(v)
        var p1 = v

        while (parent[p1] != p1) {

            val tmp = p1
            p1 = parent[p1]!!
            parent[tmp] = parent[p1]!!
        }
        return p1
    }

    fun union(v1: Int, v2: Int) {

        // println("union: $v1, $v2")
        var p1 = getParent(v1)
        var p2 = getParent(v2)
        if (p1 == p2) {
            return
        }

        groupCnt --
        // println(groupCnt)
        if (rank[p2]!! > rank[p1]!!) {
            parent[p1] = p2
            counts[p2] = counts[p2]!! + counts[p1]!!
        } else if (rank[p2]!! < rank[p1]!!) {
            parent[p2] = p1
            counts[p1] = counts[p1]!! + counts[p2]!!
        } else {
            parent[p1] = p2
            counts[p2] = counts[p2]!! + counts[p1]!!
        }
    }

    fun connected(v1: Int, v2: Int): Boolean {
        return getParent(v1) == getParent(v2)
    }
}

fun getMaxDist(n:Int, ang:Double):Double{
   // println("angle: $ang")
    val cnt = ceil( n / 2.0).toInt()
    val b = 0.5/ (Math.sin(PI * 0.5 / n.toDouble()))
    var maxDist = -1.0
    for (i in 0 until cnt) {
        val angle =  ang+ i * PI / n.toDouble()
        maxDist = max(maxDist, max(b*Math.cos(angle), b*Math.sin(angle)))
    }
    //println(maxDist)
    //println(ang)
    return maxDist
}
fun getVal(v:Long):Long{
    var cur = v
    var res = 0L
    var pow = 1L
    while(cur> 0){
        res+= if (cur % 2 == 1L) pow * 9L else 0
        pow*=10
        cur = cur shr 1
    }
    return res
}
fun getMax(arr:List<Int>): Int{
    
    println("? ${arr.size} ${arr.joinToString(" ")}")
    val res = readInt()
    if(res == -1){
        throw Exception()
    }
    return res
}
fun main(args: Array<String>) {
    loop@ for(t in 1..readInt()){
        val (n, k) = readInts()
        val A = List<HashSet<Int>>(k) { readInts().drop(1).toHashSet() }
        
        val fullCollection = (1..n).toHashSet()
        val Sfull = A.flatten().toHashSet()
        if(k == 1){
            val moutside = getMax(fullCollection.filter { !Sfull.contains(it) })   
            println("! ${moutside.toString().repeat(k)}")
            if(readLn() == "Incorrect"){
                return
            }
            continue@loop
        }
        
        var si = 1
        var ei = n
        var mx:Int = getMax(Sfull.toList())
        
        while(si < ei){
            val middle = (si+ei) / 2
            val left = getMax((si..middle).toList())
       
            if(left == mx) {
                ei = middle                
            } else{
                si = middle + 1
            }
        }
        
        if(!Sfull.contains(si)){
            println("! ${(1 until k).map { mx}.joinToString(" ")}")
            if(readLn() == "Incorrect"){
                return
            }
            continue
        }
        val asi = A.asSequence().filter { it.contains(si) }.first()

        val moutside = getMax(fullCollection.asSequence().filter { !asi.contains(it) }.toList())
        
        val res = (0 until k).map{if (A[it].contains(si)) moutside else mx}.joinToString(" ") 
        println("! $res")
        if(readLn() == "Incorrect"){
            return
        }
    }
}
