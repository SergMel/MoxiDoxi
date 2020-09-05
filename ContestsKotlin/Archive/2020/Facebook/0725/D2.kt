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
        println("pop")
        println(arr.map { it.dist }.joinToString())
        println(arr.map { it.value }.joinToString())
        val res = arr[0]
        arr[0] = arr[arr.lastIndex]
        arr.removeAt(arr.lastIndex)
        if(size != 0){
            down(0)
        }
        return res

    }
}

fun build(hp:MinHeap, cur:Int, pnt:Int, vl:Long,  depth:Int, maxDepth:Int, visited:HashSet<Int>, C:List<Long>, childs:List<MutableList<Int>>  ){  
    if (vl != Long.MAX_VALUE){
        hp.add(depth, vl + C[cur])
    }
    
    visited.add(cur)
    if(maxDepth <=  0)
    {
        return
    }
    
    
    for (el in childs[cur]) {
        if(el == pnt){continue}
        if(visited.contains(el)){continue}

        build(hp, el, cur, vl, depth+1, maxDepth -1, visited, C, childs)
    }
    
}

fun main(args: Array<String>) {
    File("out.txt").delete()
    with(Scanner(File("in.txt"))) {

       outer@for (q in 1..nextInt()) {
            val n = nextInt()         
            val m = nextInt()
            val a = nextInt() - 1
            val b = nextInt() - 1
            val parents = MutableList(n) { -1 }
            val C = MutableList(n) { -1L }
            val childs = List(n){mutableListOf<Int>()}
            nextLine()
            for (i in 0 until n) {
                val p = nextInt() - 1
                val c = nextLong()
                nextLine()
                parents[i] = if(p==-1) 0 else p
                C[i] = if (c == 0L) Long.MAX_VALUE else c
                if(p != -1){
                    childs[p].add(i)
                    childs[i].add(p)
                }                
            }
            C[a] = 0L

            var cur = a
            val hs = HashSet<Int>()
            var path = mutableListOf<Int>()
            path.add(cur)
            hs.add(a)
            while(cur != 0){                
                cur = parents[cur]
                path.add(cur)
                hs.add(cur)
            }

            val tmpPath= mutableListOf<Int>()            
            cur = b
            tmpPath.add(cur)
            while(!hs.contains(cur)){
                cur = parents[cur]
                tmpPath.add(cur)
                // println("build")
            }

    
            var removeindex = path.indexOf(cur)
            path = path.take(removeindex+1).toMutableList()
            for (i in tmpPath.lastIndex - 1 downTo 0) {
                path.add(tmpPath[i])
            }
            
            val hp = MinHeap()
            var steps = m
            var value = 0L
            val pathHs = path.toHashSet()
            
            println(path.joinToString() )
            for (i in 1..path.lastIndex) {
                steps--
                val dst = path.lastIndex - i
                
                if(steps < 0) {
                    while(hp.peek() != null &&  hp.peek()!!.dist - dst >= m   )
                    {
                        hp.pop()
                    }
                    if(hp.peek() == null){
                        File("out.txt").appendText("Case #$q: 0\n")
                        continue@outer   
                    }
                    val nextItem = hp.pop()
                    steps = nextItem.dist - dst
                    value = nextItem.value
                }

                hp.add(dst, value+C[path[i]])

                val el = path[i]
                if(steps >= 0){
                    if(i == path.lastIndex){
                        break
                    }
                    build(hp, el, path[i+1], value, path.lastIndex - i, m, visited, C, childs)                                        
                    println("build 1:${path[i]}")
                    println(hp.arr.map{ "${it.dist}:${it.value};;"}.joinToString() )
                    
                    steps--                    
                } else {
                    var itm:Item;
                    do{                        
                        itm = hp.pop()
                        println("do")
                        println(i)
                        println(path.lastIndex )
                        println(itm.dist)
                    }while(itm.dist - (path.lastIndex - i) >= m && hp.size > 0)
                    if( itm.dist - (path.lastIndex - i) > m){
                        File("out.txt").appendText("Case #$q: -1\n")
                        continue@outer
                    }
                    value = itm.value
                    steps = m - (itm.dist - (path.lastIndex - i))
                    if(i != path.lastIndex){
                        build(hp, el, path[i+1], value, path.lastIndex - i, m, visited, C, childs)                    
                        println("build 2:${path[i]}")
                    println(hp.arr.map{ "${it.dist}:${it.value};;"}.joinToString() )
                    
                    }
                }   
            }


            // output(root)
            // println(dp.joinToString(" "))
            File("out.txt").appendText("Case #$q: ")
            File("out.txt").appendText(value.toString())
            File("out.txt").appendText("\n")
       }

       close()
    }
}
