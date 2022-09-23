package main

import (
	"fmt"
)

func main() {

	root := New Root(33)
	root.AddNode(5)

	fmt.Println(root)
	fmt.Print("test")
}
