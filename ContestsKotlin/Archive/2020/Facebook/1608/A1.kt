// https://www.facebook.com/codingcompetitions/hacker-cup/2020/round-1/problems/A1
package Archive

import java.util.Queue
import java.util.LinkedList
import kotlin.collections.*
import kotlin.math.*
import java.util.Scanner
import java.util.TreeMap
import java.lang.Math.floorMod
import java.lang.Math.floorDiv
import java.io.File
import kotlin.comparisons.compareBy

import kotlin.collections.*
import kotlin.math.*


fun main(args: Array<String>) {
    val mod = 1000000007L
    File("out.txt").delete()

    with(Scanner(File("in.txt"))) {

       outer@for (q in 1..nextInt()) {
            val n = nextInt()
            val k = nextInt()
             val wall = nextLong()
            nextLine()
            val larr = nextLine().trim().split(" ").map { it.trim().toLong() }.toMutableList()
            val (al,bl,cl,dl) = nextLine().trim().split(" ").map { it.trim().toLong() }
            
            val harr = nextLine().trim().split(" ").map { it.trim().toLong() }.toMutableList()
            val (ah,bh,ch,dh) = nextLine().trim().split(" ").map { it.trim().toLong() }

            // val warr = (1..n).map { wall }.toList()
            
            for (i in k..n-1) {
                larr.add(((al*larr[larr.lastIndex-1] + bl*larr[larr.lastIndex] + cl) % dl) + 1)
                // warr.add(w)
                harr.add(((ah*harr[harr.lastIndex-1] + bh*harr[harr.lastIndex] + ch) % dh) + 1)
                
            }
            // println(larr)
            // println(warr)
            // println(harr)

            var per = 0L
            
            
            var res = 1L
            var hm = HashMap<Long, Long>()
            
            for (i in 0 until n) {
                val l = larr[i]            
                val h = harr[i]

                hm.filter { it.key < l - wall }.forEach{hm.remove(it.key)}
                
                val height = hm.values.max() ?: 0L
                val xlast =  hm.keys.max()?:l - wall
                //println("Height: ${height}")
                // println("Height: ${xlast}")
                if(height >= h) {

                    per+= (l -  xlast) * 2
                } else{
                    per+= (l -  xlast) * 2 + (h - height) * 2
                }
                // println(per)
                per = floorMod(per, mod)
                res = floorMod(per * res, mod)
                hm[l] = h
                
            }

            // output(root)
            // println(dp.joinToString(" "))
            File("out.txt").appendText("Case #$q: ")
            File("out.txt").appendText(res.toString())
            File("out.txt").appendText("\n")
       }    

       close()
    }
}

