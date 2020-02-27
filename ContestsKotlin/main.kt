import kotlin.math.*

private fun readLn() = readLine()!! // string line
private fun readInt() = readLn().toInt() // single int
private fun readStrings() = readLn().split(" ") // list of strings
private fun readInts() = readStrings().map { it.toInt() } // list of ints
private fun readLongs() = readStrings().map { it.toLong() } // list of ints


fun main(args: Array<String>) {   
   
val n = readInt()
val str =  readLn()

val cnt = 'Z' - 'A' + 1
var arr = IntArray(cnt*cnt)
for(i in 0 until str.length - 1) {
    val v = (str[i] - 'A') * cnt + (str[i+1] - 'A')
    arr[v]++
}
var ret = ""
var max = 0
for(i in 0 until arr.size)
{
    var v = arr[i]
    if (v > max)
    {
        max = v
        ret = Character.toString('A' + i / cnt) + Character.toString('A' +i % cnt)
    }
}
println(ret)
}   