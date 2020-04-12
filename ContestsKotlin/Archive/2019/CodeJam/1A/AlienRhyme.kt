package CodeJam.G2019.G1A
import kotlin.collections.*
import kotlin.math.*

fun readLn() = readLine()!! // string line
fun readInt() = readLn().toInt() // single int
fun readStrings() = readLn().split(" ") // list of strings
fun readInts() = readStrings().map { it.toInt() } // list of ints
fun readLongs() = readStrings().map { it.toLong() } // list of ints
fun readLong() = readLn().toLong() // list of strings
fun writeGoogleAnswer(case: Int, str: String) = println("Case #$case: $str")

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
    val signa = sign(la.toDouble()).toInt()
    val signb = sign(lb.toDouble()).toInt()

    var a = abs(la)
    var b = abs(lb)
    val reverse = a > b
    if (reverse) {
        a = b.also { b = a }
    }

    var m11 = 1
    var m12 = 0
    var m21 = 0
    var m22 = 1

    while (a > 0) {
        val tmp = b / a
        a = (b % a).also { b = a }
        m11 = (m12 - m11 * tmp).also { m12 = m11.also { m21 = (m22 - m21 * tmp).also { m22 = m21 } } }
    }
    var x = m12
    var y = m22
    var d = b
    if (reverse) {
        x = y.also { y = x }
    }
    return Triple(signa * x, signb * y, d)
}

fun inverse(la: Long, mod: Long): Long {
    val res = euclideanExt(la, mod).first
    return if (res < 0) res + mod else res
}

fun inverse(la: Int, mod: Int): Int {
    val res = euclideanExt(la, mod).first
    return if (res < 0) res + mod else res
}

fun gcd(la: Long, lb: Long) = euclideanExt(la, lb).third

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

data class Tree(val v: Char) {
    public val childs = hashSetOf<Tree>()
    public var count = 0
}

fun walk(root: Tree): Int {
    var res = 0
    root.childs.forEach {
        res += walk(it)
        if (root.count <= res + 1) {
            return@forEach
        }
     }
     val delta = if (root.v == '0') 0 else if (root.count - res >= 2 ) 2 else 0
     return res + (delta / 2) * 2
}

// https://en.wikipedia.org/wiki/Ore%27s_theorem
fun main(args: Array<String>) {
     // println(chinese(readInts(), readInts()))

    val T = readInt()
    for (t in 1..T) {
        
        val N = readInt()
        val W = List(N) { readLn().reversed() }
        val root = Tree('0')
        W.forEach {
            var cur = root
            // println(it)
            it.forEach {
                // println(it)
                val next = Tree(it)
                cur = if (cur.childs.contains(next)) {
                    // println("contains")
                    cur.childs.elementAt(cur.childs.indexOf(next))
                } else {
                    // println("Not contains")
                    cur.childs.add(next)
                    next
                }
                cur.count++
            }
        }
        // println(root)

        writeGoogleAnswer(t, walk(root).toString())

    }
}
