package main

import "sort"

func getCnt(N int32, A []int64, B []int64) int64 {
	dist := int64(0)
	for i := int32(0); i < N; i++ {
		dist += B[i] - A[i]
	}
	return dist
}

func getSecondsElapsed(C int64, N int32, A []int64, B []int64, K int64) int64 {
	// Write your code here

	sort.Slice(A, func(i, j int) bool { return A[i] < A[j] })
	sort.Slice(B, func(i, j int) bool { return B[i] < B[j] })

	tdist := getCnt(N, A, B)

	secs := (K / tdist) * C
	rem := K % tdist

	i := 0
	for rem > 0 {
		td := B[i] - A[i]

		if i == 0 {
			secs += A[i]
		} else {
			secs += A[i] - B[i-1]
		}

		if td >= rem {
			secs += rem
			rem = 0
		} else {
			secs += td
			rem -= td
		}

		i++
	}

	return secs
}

func main() {
	getSecondsElapsed(10, 2, []int64{1, 6}, []int64{3, 7}, 7)
	//root := NewRoot(33)
	//root.AddNode(5)

	//fmt.Println(root)

}
