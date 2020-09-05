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
            // val wall = nextLong()
            nextLine()
            val larr = nextLine().trim().split(" ").map { it.trim().toLong() }.toMutableList()
            val (al,bl,cl,dl) = nextLine().trim().split(" ").map { it.trim().toLong() }
            
            val warr = nextLine().trim().split(" ").map { it.trim().toLong() }.toMutableList()
            val (aw,bw,cw,dw) = nextLine().trim().split(" ").map { it.trim().toLong() }
            val harr = nextLine().trim().split(" ").map { it.trim().toLong() }.toMutableList()
            val (ah,bh,ch,dh) = nextLine().trim().split(" ").map { it.trim().toLong() }

            // val warr = (1..n).map { wall }.toList()
            
            for (i in k..n-1) {
                larr.add(((al*larr[larr.lastIndex-1] + bl*larr[larr.lastIndex] + cl) % dl) + 1)
                warr.add(((aw*warr[warr.lastIndex-1] + bw*warr[warr.lastIndex] + cw) % dw) + 1)
                // warr.add(w)
                harr.add(((ah*harr[harr.lastIndex-1] + bh*harr[harr.lastIndex] + ch) % dh) + 1)
                
            }
            // println(larr)
            // println(warr)
            // println(harr)

            var per = 0L
            val tm = TreeMap<Long, Triple<Long, Long,  Long>>()
            
            var res = 1L
            
            for (i in 0 until n) {
                val l = larr[i]
                val w = warr[i]
                val h = harr[i]
                val starts = tm.subMap(l, l + w + 1).toList()
                var left = tm.floorEntry(l-1)?.value
                 
                // println("new room: ${l} ${l+w} $h")
                // println(left)
                
                if(left != null && left.second >= l  ){
                    tm.put(left.first, Triple(left.first, l, left.third))                    
                    per+= h - left.third + 2*w - 2 * (left.second - l)
                } else {
                    left = null
                    per+= h +  2*w
                }

                for ((_,el) in starts) {
                    // println(el)
                    if(left == null){
                        // println("extract 1: ${el.third + 2 * (el.second - el.first)}")
                        per -= el.third + 2 * (el.second - el.first)
                    }  else if(left.second < el.first) {
                        // println("extract:2 ${left.third + el.third  + 2 * (el.second - el.first)}")
                        per -= left.third + el.third  + 2 * (el.second - el.first)
                        
                        
                    } else {
                        // println("extract:3 ${left.third + abs(el.third - left.third) + 2 * (el.second - el.first)}")
                        per -= abs(el.third - left.third) + 2 * (el.second - el.first)
                        
                    }
                    // println("removed: ${el}")                        
                    tm.remove(el.first)
                    left = el
                    per = per%mod
                }

                if(left!=null && left.second <= l+w){
                    per+= h-left.third
                } else if(left!=null) {
                    per+=2 * (left.second - l - w) + (h - left.third)
                    tm.put(l+w, Triple(l+w, left.second, left.third))
                }
                else{
                    per+=h
                }
                tm.put(l, Triple(l, l+w, h))
                // println(per)
                per = floorMod(per, mod)
                res*=per
                res = floorMod(res, mod)

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

