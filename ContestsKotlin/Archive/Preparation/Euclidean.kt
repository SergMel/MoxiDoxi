// https://www.geeksforgeeks.org/euclidean-algorithms-basic-and-extended/
package preparation.euclidean
// https://www.geeksforgeeks.org/euclidean-algorithms-basic-and-extended/
import java.util.Queue
import java.util.LinkedList
import kotlin.collections.*
import kotlin.math.*
import java.util.Scanner
import java.lang.Math.floorMod
import java.lang.Math.floorDiv
import java.io.File

fun readLn() = readLine()!!.trim() // string line
fun readInt() = readLn().trim().toInt() // single int
fun readStrings() = readLn().trim().split(" ") // list of strings
fun readInts() = readStrings().map { it.trim().toInt() } // list of ints
fun readLongs() = readStrings().map { it.trim().toLong() } // list of ints
fun readLong() = readLn().trim().toLong() // list of strings
fun writeGoogleAnswer(case: Int, str: String) = println("Case #$case: $str")

fun GCD(v1:Long, v2:Long):Long{
    if(v1==0L && v2==0L){
        throw Exception("Both 0")
    } else if(v1==0L){
        return v2
    } else if (v2 == 0L) {
        return v1
    }

    var a = abs(v1)
    var b = abs(v2)

    a = max(a,b).also { b = min(a, b) }
    while (a % b != 0L){
        a = b.also{b = a % b}
    }
    return b

}

fun euclideanExtAlg(a:Long, b:Long):Triple<Long, Long, Long>{
    if(a == 0L && b == 0L){
        throw Exception("Both 0")
    }
    if(abs(a) < abs(b)){
        throw Exception("abs(a) < abs(b)")
    }
    if(b == 0L){
        return Triple(1L, 0L, a)
    }
    
    val res = euclideanExtAlg(b, floorMod(a, b))
    return Triple(res.second, res.first - floorDiv(a, b) * res.second, res.third)    
}


fun euclideanExt(a:Long, b:Long):Triple<Long, Long, Long>{
    if(abs(a) < abs(b)){
        val res = euclideanExtAlg(b, a)
        return Triple(res.second, res.first, res.third)
    } else {
        val res = euclideanExtAlg(a, b)
        return Triple(res.first, res.second, res.third)
    }
}

fun main(args: Array<String>) {
    
    println(GCD(-15, 10))
    println(GCD(10, -15))
    println(GCD(10, 15))
    println(GCD(10, 1))
    println(GCD(1, 10))
    println(GCD(10, 0))
    println(GCD(0, 10))

    println(euclideanExt(-15, 10))
    println(euclideanExt(10, -15))
    println(euclideanExt(15, 10))
    println(euclideanExt(10, 15))
    println(euclideanExt(10, 1))
    println(euclideanExt(1, 10))
    println(euclideanExt(10, 0))
    println(euclideanExt(0, 10))

}
