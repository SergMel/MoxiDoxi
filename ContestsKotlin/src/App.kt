import java.util.Queue
import java.util.LinkedList
import kotlin.collections.*
import kotlin.math.*
import java.util.Scanner
import java.io.File
import java.util.TreeMap


fun main(args: Array<String>) {
    File("out.txt").delete()

    with(Scanner(File("in.txt"))) {
       for (q in 1..nextInt()) {
            val n = nextInt()
            val m = nextInt()
            val e = nextInt()
            val k = nextInt()
            
            nextLine()
            val x = nextLine().trim().split(" ").map { it.trim().toInt() }.toMutableList()
            
            val ax = nextInt()
            val bx = nextInt()
            val cx = nextInt()

            for (i in k until n) {
                x.add((ax*x[i-2]+bx*x[i-1] + cx)% m )
            }
            
            nextLine()
            val y = nextLine().trim().split(" ").map { it.trim().toInt() }.toMutableList()
            
            val ay = nextInt()
            val by = nextInt()
            val cy = nextInt()
            for (i in k until n) {
                y.add((ay*y[i-2]+by*y[i-1] + cy)% m )
            }
            
            nextLine()
            val I = nextLine().trim().split(" ").map { it.trim().toInt() }.toMutableList()
            
            val aI = nextInt()
            val bI = nextInt()
            val cI = nextInt()
            for (i in k until e) {
                I.add((aI*I[i-2]+bI*I[i-1] + cI)% (n*m+n) )
            }

            nextLine()
            val w = nextLine().trim().split(" ").map { it.trim().toLong() }.toMutableList()
            
            val aw = nextInt()
            val bw = nextInt()
            val cw = nextInt()
            for (i in k until e) {
                w.add((aw.toLong()*w[i-2].toLong()+bw.toLong()*w[i-1].toLong() + cw.toLong())% ( 1000000000L) )
            }

            val tree = MutableList<TreeMap<Int, Long>>(n+1){TreeMap<Int, Long>()}

            for (i in 0 until n ) {
                for (j in 0..m) {
                    tree[i].put(i*m+j, 1L)
                }                
            }
            for (j in 0..n) {
                tree[n].put(n*m+j, 1L)
            }


            var res = 0L
            
            val hs = HashSet<Int>()

            for (i in 0 until n ) {
                if(x[i] == y[i]) {
                    hs.add(i)
                    res += m-1
                }  else{
                    res+=m-2
                }          
            }

            for (i in 0 until e) {
                val el = I[i]
                val wt = w[i]
                val ind = el / m
                if(ind < n){
                    val tr = tree[ind]!!
                    
                }

            }
            for (el in I) {
                val ind = el / 
            }

            File("out.txt").appendText("Case #$q: \n")
            File("out.txt").appendText((1..n).map { dp[n][it] }.joinToString("\n") )
            File("out.txt").appendText("\n")
       }

       close()
    }
}
