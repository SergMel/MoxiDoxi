import kotlin.math.*
 
fun readLn() = readLine()!! // string line
fun readInt() = readLn().toInt() // single int
fun readStrings() = readLn().split(" ") // list of strings
fun readInts() = readStrings().map { it.toInt() } // list of ints
fun readLongs() = readStrings().map { it.toLong() } // list of ints
fun readLong() = readLn().toLong() // list of strings
 


 
fun main() {
    val nl = readInts()
    val n = nl[0]
    val l = nl[1]
    
    val arr = arrayOfNulls<Pair<Int, Int>>(n+1)
    arr[0] = Pair(0, 0)
    for (i in 0 until n) {
        val xb =readInts()
        arr[i+1] = Pair(xb[0], xb[1])        
    }

    if (n == 1)
    {
        println(1)
        return 
    }
    
    val eps = 0.00001
    var res = Double.MAX_VALUE
    var ll =0.0
    var r = sqrt((arr[n]!!.first.toDouble() - l.toDouble()).absoluteValue)/arr[n]!!.second.toDouble()
    var prev = IntArray(n+1)
    while(res.absoluteValue > eps){
        val cur = (r+ll)/2.0
        val dp = DoubleArray(n+1){Double.MAX_VALUE}
        dp[0] = 0.0
        prev = IntArray(n+1)
        for(i in 0..n-1){
            for(j in i+1..n)
            {
                val add = sqrt((arr[j]!!.first.toDouble() - arr[i]!!.first.toDouble() - l.toDouble()).absoluteValue) - cur * arr[j]!!.second.toDouble()
                // println(" ================== ")
                // println(cur)
                // print(i)
                // print(" ")
                // print(j)
                // println(" ")
                // println(dp.joinToString(separator = " "))
                // println("++++++++++++++++++++")

                if (dp[i] + add < dp[j] )
                {
                    dp[j] = dp[i] + add
                    prev[j] = i
                }
            }
        }
        // println(cur)
        // println(dp.joinToString(separator = " "))
        // println("======================")
        
        res = dp[n]
        if (res <= 0)
        {
            r = cur
        } else
        {
            ll = cur
        }
    }
    
    val steps = mutableListOf<Int>()
    var cur = n
    steps.add(cur)

    while(prev[cur] != 0)
    {
        cur = prev[cur]
        steps.add(cur)
    }

    for(i in steps.size -1 downTo 0)
    {
        val el = steps[i]
        if (el != 0)
        {
            print(el)
            print(" ")
        }
    }

    
}



main()



