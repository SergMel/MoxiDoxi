import kotlin.math.*

private fun readLn() = readLine()!! // string line
private fun readInt() = readLn().toInt() // single int
private fun readStrings() = readLn().split(" ") // list of strings
private fun readInts() = readStrings().map { it.toInt() } // list of ints
private fun readLongs() = readStrings().map { it.toLong() } // list of ints

fun check(arr:List<Int>, steps:Int, m:Int):Boolean
{
    //println("$steps $m")
    var prevMin:Int = 0
    for(el in arr)
    {
        //println("el = $el")
        if (el < prevMin && el+steps >=prevMin ||  (el+steps) >= m && (el+steps) % m >= prevMin )
        {
            //println("continue")
            continue
        }
        else
        {
            if (el >= prevMin)
            {
                //println("prevMin=$el")
                prevMin = el
            }
            else 
            {
                //println("Return false $el")
                return false
            }
        }
    }
    return true
}

fun main(args: Array<String>) {   

    val N = readInt()
    va
    for(i in 1..N) {
        val arr = readInts()

    }
    val arr =readInts()
    if (n <= 1)
    {
        println(0)
        return
    }
    var l = -1
    var r = m - 1
    var cur:Int
    while(r - l > 1)
    {
        cur = (l+r) / 2
        if (check(arr, cur, m))
        {
            r = cur
        }
        else 
        {
            l = cur
        }
        
    }
    

    println(r)

}   