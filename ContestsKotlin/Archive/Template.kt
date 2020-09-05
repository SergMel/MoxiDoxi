package template

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

class Item(val dist:Int, val value:Long){

}

class  MinHeap {
    public val arr = mutableListOf<Item>()
    
    val size get() = arr.size
    fun leftChild(pos:Int) = 2*(pos+1) - 1
    fun rightChild(pos:Int) = 2*(pos+1)
    fun parent(pos:Int) = (pos + 1) / 2 - 1

    public fun add(d:Int, v:Long):Unit{
        //println("add")
        arr.add(Item(d, v))
        up(arr.lastIndex)
        // println("heap ${arr.map{it.value}.joinToString()}")
    }

    fun isLeaf(pos:Int):Boolean
    { 
        if ((2*(pos+1) - 1) >= size && (pos+1) <= size) { 
            return true; 
        } 
        return false; 
    } 

    fun swap(i:Int, j:Int){
        if(i == j) return
        var v1 = arr[i]
        var v2 = arr[j]
        arr[i] = v2
        arr[j] = v1
    }

    fun down(pos:Int) 
    { 
        var cur = pos
        while(!isLeaf(cur)){
            val lft = leftChild(cur)
            val rght = rightChild(cur)           
            
            var swapto = cur
            // println(swapto)
            // println(lft)
            // println(arr.joinToString() )
            if(arr[lft].value < arr[cur].value){
                swapto = lft
            }
            if(rght < size && arr[rght].value < arr[swapto].value){
                swapto = rght
            }
            if(swapto == cur){
                break
            }
            swap(cur, swapto)
            cur = swapto
        }
       
    } 

    fun up(pos:Int) 
    {         
        var cur = pos
        while(cur != 0){
            val pnt = parent(cur)         
            
            if(arr[pnt].value > arr[cur].value){
                swap(cur, pnt)
                cur = pnt
            }
            else{
                break;
            }            
        }       
    }     
    
    fun peek():Item? {
        return if(size == 0) null else  arr[0]
    }
    
    fun pop():Item{
        //println("pop")
        //println(arr.map { it.dist }.joinToString())
        //println(arr.map { it.value }.joinToString())
        val res = arr[0]
        arr[0] = arr[arr.lastIndex]
        arr.removeAt(arr.lastIndex)
        if(size != 0){
            down(0)
        }
        return res

    }
}


fun getRange(parent:Int, curent:Int, p:List<Long>, h:List<Long>, tree:List<List<Int>>):Pair<Long, Long>?{
    var l = -p[curent]
    var g = p[curent]
    
    for (el in tree[curent]) {
        if(el == parent){
            continue
        }
        val ch = getRange(curent, el, p, h, tree)
        if (ch == null){
            return null
        }
        l+=ch.first
        g+=ch.second        
    }

    if(h[curent]< l || h[curent]>g || g % 2 != abs(h[curent ])% 2){
        return  null
    }
    return Pair(h[curent], g)
    
}

fun main(args: Array<String>) {
   
    outer@ for (q in 1..readInt()) {
        val (nn, m) = readLongs()
        val n = nn.toInt()
        val p = readLongs()
        val h = readLongs()
        
        val edges = List(n) { mutableListOf<Int>() }

        for (i in 1 until n) {
            val (x, y) = readInts().map{it-1}
            edges[x].add(y)
            edges[y].add(x)
        }

        val res = getRange(0, 0, p, h, edges)

        println(if(res==null) "NO" else "YES")
        


    }
}
