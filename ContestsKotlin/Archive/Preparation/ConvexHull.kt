// https://www.geeksforgeeks.org/convex-hull-set-2-graham-scan/
package preparation.convexhull
import java.util.Queue
import java.util.LinkedList
import kotlin.collections.*
import kotlin.math.*
import java.util.Scanner
import java.lang.Math.floorMod
import java.lang.Math.floorDiv
import java.io.File
import kotlin.comparisons.compareBy

fun readLn() = readLine()!!.trim() // string line
fun readInt() = readLn().trim().toInt() // single int
fun readStrings() = readLn().trim().split(" ") // list of strings
fun readInts() = readStrings().map { it.trim().toInt() } // list of ints
fun readLongs() = readStrings().map { it.trim().toLong() } // list of ints
fun readLong() = readLn().trim().toLong() // list of strings
fun writeGoogleAnswer(case: Int, str: String) = println("Case #$case: $str")

data class Point(val x: Long, val y: Long)

fun crossProduct(p1:Point, p2:Point, p3:Point) = (p2.x - p1.x ) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y)

fun convexHull(inpt:List<Point>):List<Point>{
    if(inpt.size == 0){
        return listOf<Point>()
    }
    val pnt0 = inpt.minWith(compareBy({ it.y }, { it.x }))!!
    
    var srtd = inpt.filter { it!=pnt0 }.sortedWith(Comparator<Point>{v1, v2 -> 
        // println("$v1 x $v2 = ${crossProduct(pnt0, v1, v2)}")
        crossProduct(pnt0, v1, v2)
        val cp = crossProduct(pnt0, v1, v2)
        if(cp > 0L)
            -1
        else if(cp < 0L)
            1
        else {
            val dist = (v1.x - pnt0.x).toDouble().pow(2) + (v1.y - pnt0.y).toDouble().pow(2) - (v2.x - pnt0.x).toDouble().pow(2) - (v2.y - pnt0.y).toDouble().pow(2)
            if (dist > 0.0) 1 else if (dist < 0.0) -1 else 0        
        }         
    })
    // println(srtd.joinToString())
    var pnts = mutableListOf<Point>()
    for (i in 0..srtd.lastIndex) {
        
        if(i < srtd.lastIndex && crossProduct(pnt0, srtd[i], srtd[i+1]) == 0L){
            continue
        }
        pnts.add(srtd[i])        
    }

    if(pnts.size < 2){
        return listOf()
    }
    // println(pnts.joinToString() )
    var res = mutableListOf<Point>(pnt0, pnts[0], pnts[1])
    // println(res.joinToString() )
    for (i in 2..pnts.lastIndex) {
        // println("Cycle $i: ${pnts[i]}")
        while(crossProduct(res[res.lastIndex - 1], res.last(), pnts[i]) <= 0L ){
            // println("remove: ${res[res.lastIndex]}")
            res.removeAt(res.lastIndex)
        }
        // println("add: ${pnts[i]}")
        res.add(pnts[i])        
    }
    return res


}

fun main(args: Array<String>) {
    var  points = mutableListOf(Point(0, 3), Point(1, 1), Point(2, 2), Point(4, 4), 
    Point(0, 0), Point(1, 2), Point(3, 1), Point(3, 3));    
    println(convexHull(points).joinToString())

}
