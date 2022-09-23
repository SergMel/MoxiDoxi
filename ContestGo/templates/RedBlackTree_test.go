package templates

import (
	"math/rand"
	"testing"
)

func (node *RedBlackTreeNode) GetBlackCount() (bool, int) {
	if node == nil {
		return true, 1
	}

	leftb, leftDepth := node.Left.GetBlackCount()
	if !leftb {
		return false, 0
	}
	rightb, rightDepth := node.Right.GetBlackCount()
	if !rightb {
		return false, 0
	}
	if leftDepth == rightDepth {
		if node.Color == Black {
			return true, 1 + rightDepth
		} else {
			return true, rightDepth
		}
	}
	return false, 0
}

func (node *RedBlackTreeNode) RedNbsCheck() bool {
	if node == nil {
		return false
	}

	if node.Color == Red {
		if node.Left != nil && node.Left.Color == Red {
			return true
		}
		if node.Right != nil && node.Right.Color == Red {
			return true
		}
	}
	return node.Left.RedNbsCheck() || node.Right.RedNbsCheck()
}
func TestRoot(t *testing.T) {
	root := NewRoot(33)
	if root.Value != 33 {
		t.Fatalf(`Value = %q, schould be 33`, root.Value)
	}
}

func TestMin1(t *testing.T) {
	tree := NewRedBlackTree()
	tree.AddValue(33)
	res, _ := tree.Min()
	if res != 33 {
		t.Fatalf(`Min = %q, schould be 33`, res)
	}
}

func TestMin2(t *testing.T) {
	tree := NewRedBlackTree()
	tree.AddValue(33)
	tree.AddValue(5)
	res, _ := tree.Min()
	if res != 5 {
		t.Fatalf(`Min = %q, schould be 5`, res)
	}
}

func TestMin3(t *testing.T) {
	tree := NewRedBlackTree()
	tree.AddValue(33)
	tree.AddValue(11)
	tree.AddValue(10)
	tree.AddValue(15)
	tree.AddValue(5)
	tree.AddValue(20)
	res, _ := tree.Min()
	if res != 5 {
		t.Fatalf(`Min = %v, schould be 5`, res)
	}
}

func TestFind1(t *testing.T) {
	tree := NewRedBlackTree()
	tree.AddValue(33)
	tree.AddValue(11)
	tree.AddValue(10)
	tree.AddValue(15)
	tree.AddValue(5)
	tree.AddValue(20)
	_, res, _ := tree.FindLessOrEqual(12)
	if res != 11 {
		t.Fatalf(`LessOrEqual = %q, schould be 11`, res)
	}
}

func TestFind2(t *testing.T) {
	tree := NewRedBlackTree()
	tree.AddValue(33)
	tree.AddValue(11)
	tree.AddValue(10)
	tree.AddValue(15)
	tree.AddValue(5)
	tree.AddValue(20)
	_, res, _ := tree.FindLessOrEqual(5)
	if res != 5 {
		t.Fatalf(`LessOrEqual = %q, schould be 5`, res)
	}
}
func TestFind3(t *testing.T) {
	tree := NewRedBlackTree()
	tree.AddValue(33)
	tree.AddValue(11)
	tree.AddValue(10)
	tree.AddValue(15)
	tree.AddValue(5)
	tree.AddValue(20)
	found, res, _ := tree.FindLessOrEqual(4)
	if found {
		t.Fatalf(`LessOrEqual found %q, schould not find`, res)
	}
}

func TestDepth1(t *testing.T) {
	tree := NewRedBlackTree()
	tree.AddValue(33)
	tree.AddValue(11)
	tree.AddValue(10)
	tree.AddValue(15)
	tree.AddValue(5)
	tree.AddValue(20)
	b, _ := tree.Root.GetBlackCount()
	if !b {
		t.Fatalf(`Tree is not balanced`)
	}
}

func TestDepth2(t *testing.T) {
	exists := map[int]bool{}
	tree := NewRedBlackTree()
	mx := 1000001
	for i := 1; i < 1000; i++ {
		v := rand.Intn(1000000)
		_, ok := exists[v]
		if ok {
			continue
		}
		if v < mx {
			mx = v
		}
		tree.AddValue(v)
	}

	b, dp := tree.Root.GetBlackCount()
	t.Log(`Black depth: `, dp)
	if !b {
		t.Fatalf(`Tree is not balanced`)
	}
	if mn, _ := tree.Min(); mn != mx {
		t.Fatalf(`Found min value = %v, schould be %v`, mn, mx)
	}
}

func TestRedNbs1(t *testing.T) {
	tree := NewRedBlackTree()
	tree.AddValue(33)
	tree.AddValue(11)
	tree.AddValue(10)
	tree.AddValue(15)
	tree.AddValue(5)
	tree.AddValue(20)
	b := tree.Root.RedNbsCheck()
	if b {
		t.Fatalf(`Tree is 2 red nbs`)
	}
}

func TestRedNbs2(t *testing.T) {
	exists := map[int]bool{}
	tree := NewRedBlackTree()
	mx := 1000001
	for i := 1; i < 1000; i++ {
		v := rand.Intn(1000000)
		_, ok := exists[v]
		if ok {
			continue
		}
		if v < mx {
			mx = v
		}
		tree.AddValue(v)
	}

	b := tree.Root.RedNbsCheck()
	if b {
		t.Fatalf(`Tree is 2 red nbs`)
	}
}
