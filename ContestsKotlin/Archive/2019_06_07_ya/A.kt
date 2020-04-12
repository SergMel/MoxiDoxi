package Archive.Y2019
import kotlin.math.*
import kotlin.collections.*

private fun readLn() = readLine()!! // string line
private fun readInt() = readLn().toInt() // single int
private fun readStrings() = readLn().split(" ") // list of strings
private fun readInts() = readStrings().map { it.toInt() } // list of ints
private fun readLongs() = readStrings().map { it.toLong() } // list of ints

class team(val M:Int, var Scores:MutableList<Int>){

}

fun main(args: Array<String>) {   

    val N = readInt()
    var teams = mutableListOf<team>()
    for(i in 1..N) {
        val arr = readInts()
        teams.add(team(arr[0], arr.drop(1)))
    }
    
}   