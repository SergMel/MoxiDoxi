package Archive.Y2019

import java.util.Queue
import java.util.LinkedList
import Mod.mdiv
import Mod.mmod
import Mod.mmult
import Mod.mplus
import Mod.mpow
import NumTh.inverse
import kotlin.collections.*
import kotlin.math.*
import java.util.Scanner
import java.io.File

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


class Node(var index:Int, var value:Long, val range:Pair<Int, Int>,  val left:Node?, val right:Node?){
    constructor(index:Int, value:Long, range:Pair<Int, Int>):this(index, value, range, null, null) {        
    }

    public fun update(index:Int, value:Long){
        if(index < range.first || index > range.second){
            return 
        }
        if(index == range.first && index == range.second){
            this.value = value
            return 
        }
        if(index <= left!!.range.second) {
            left.update(index, value)
        } else {
            right!!.update(index, value)
        }
        if(left.value >= right!!.value){
            this.index = right.index
            this.value = right.value
        } else{
            this.index = left.index
            this.value = left.value
        }
        
    }

    public fun getMin(l:Int, r:Int):Pair<Int, Long>{
        if(l <= range.first && r >= range.second ){
            return Pair(index, value)
        }       

        if (r < range.first || l > range.second){
            throw Exception("should not reach")
        }

        var li = -1
        var lvalue = -1L
        if(l <= left!!.range.second){
            val tmp = left.getMin(l, r)
            li = tmp.first
            lvalue = tmp.second
        }
        var ri = -1
        var rvalue = -1L
        if(r >= right!!.range.first){
            val tmp = right.getMin(l, r)
            ri = tmp.first
            rvalue = tmp.second
        }

        if(li != -1 && ri != -1){
            return if(lvalue < rvalue) Pair(li, lvalue) else Pair(ri, rvalue)
        } else if(li!=-1){
            return Pair(li, lvalue)
        } else {
            return Pair(ri, rvalue)
        }
    }

}

fun build(C:List<Long>, left:Int, right:Int):Node{
    if(left == right){
        return Node(left, C[left], Pair(left, right))
    }
    val middle = (left+right) / 2
    val leftNode = build(C, left, middle)
    val rightNode = build(C, middle+1, right)
    if (C[leftNode.index] < C[rightNode.index]){
        return Node(leftNode.index, C[leftNode.index], Pair(left, right), leftNode, rightNode)
     } else {
        return Node(rightNode.index, C[rightNode.index], Pair(left, right), leftNode, rightNode)
     }
}

fun output(root:Node?){
    if(root == null ){
        return
    }
    if(root.left == null){
        println("i: ${root.index}, value:${root.value}")
        return 
    }
    output(root.left)
    output(root.right)
}


fun main(args: Array<String>) {
    File("out.txt").delete()
    with(Scanner(File("in.txt"))) {

       outer@for (q in 1..nextInt()) {
            val n = nextInt()         
            val m = nextInt()
            nextLine()
            val C = List(n){nextLong()}.map { if(it == 0L) Long.MAX_VALUE else it }.toList()

            val root = build(C, 0, n-1)            

            val dp = MutableList(n){-1L}
            for (i in 0 until min(m, n-1)) {
                dp[i+1] = 0L
            }
            var cur = m+1
            var last = -1
            while(cur < n){
                val (index, value) = root.getMin(cur - m, cur-1)
                // println("cur: ${cur}")
                // println("index: ${index}, value: ${value}")
                if(value == Long.MAX_VALUE){
                    File("out.txt").appendText("Case #$q: -1\n")
                    continue@outer
                }
                dp[cur] = value
                // println("i: ${i}")
                // println("index: ${index}, value: ${value}")
                for (i in cur..min(n-1, (index+m))) {
                    // println("i: ${i}")
                    dp[i] = dp[cur]
                    root.update(i, if( C[i] == Long.MAX_VALUE) Long.MAX_VALUE else  dp[i]+C[i])                    
                }
                cur = index+m+1

            
            }
            // output(root)
            // println(dp.joinToString(" "))
            File("out.txt").appendText("Case #$q: ")
            File("out.txt").appendText(dp[n-1].toString())
            File("out.txt").appendText("\n")
       }

       close()
    }
}