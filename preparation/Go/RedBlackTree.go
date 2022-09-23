package main

import "errors"

const (
	Red   = iota
	Black = iota
)

type RedBlackTree struct {
	Root *RedBlackTreeNode
}

func NewRedBlackTree() *RedBlackTree {
	p := new(RedBlackTree)
	return p
}

func (tree *RedBlackTree) AddValue(v int) {
	if tree.Root == nil {
		tree.Root = NewRoot(v)
	} else {
		newNode := tree.Root.AddNode(v)
		newNode.Transform()
		for ; tree.Root.Parent != nil; tree.Root = tree.Root.Parent {
		}
	}
}

func (tree *RedBlackTree) Min() (int, error) {
	if tree.Root == nil {
		return -1, errors.New("empty tree")
	} else {
		return tree.Root.GetMin().Value, nil
	}
}

func (tree *RedBlackTree) FindLessOrEqual(v int) (bool, int, error) {
	if tree.Root == nil {
		return false, -1, errors.New("empty tree")
	} else {
		node := tree.Root.FindLessOrEqual(v)
		if node == nil {
			return false, 0, nil
		}
		return true, node.Value, nil
	}
}

type RedBlackTreeNode struct {
	Parent *RedBlackTreeNode
	Left   *RedBlackTreeNode
	Right  *RedBlackTreeNode
	Value  int
	Color  int
}

func (node *RedBlackTreeNode) DeleteAtEnd() {

}

func (node *RedBlackTreeNode) DeleteCase1() {
	if node.Right != nil {
		node.Color = Black
		node.Value = node.Right.Value
		node.Right = nil
	} else if node.Left != nil {
		node.Color = Black
		node.Value = node.Left.Value
		node.Left = nil
	} else if node.Parent.Left == node {
		node.Parent.Left = nil
	} else {
		node.Parent.Right = nil
	}
}

func (node *RedBlackTreeNode) DeleteCase2() {
	if node.Right != nil {
		node.Color = Black
		node.Value = node.Right.Value
		node.Right = nil
	} else if node.Left != nil {
		node.Color = Black
		node.Value = node.Left.Value
		node.Left = nil
	} else if node.Parent.Left == node {
		node.Parent.Left = nil
	} else {
		node.Parent.Right = nil
	}
}

func NewRedNode(value int) *RedBlackTreeNode {
	p := new(RedBlackTreeNode)
	p.Value = value
	p.Color = Red
	return p
}

func NewRoot(value int) *RedBlackTreeNode {
	p := new(RedBlackTreeNode)
	p.Value = value
	p.Color = Black
	return p
}

/// Everithing is ok
func IsCase0(node *RedBlackTreeNode) bool {
	if node.Parent == nil || node.Parent.Color == Black {
		return true
	}
	return false
}

func (node *RedBlackTreeNode) GetAllRelatives() (*RedBlackTreeNode, *RedBlackTreeNode, *RedBlackTreeNode, *RedBlackTreeNode) {
	prnt := node.Parent
	if prnt == nil {
		return nil, nil, nil, nil
	}

	sbl := prnt.Left
	if sbl == node {
		sbl = prnt.Right
	}

	gp := prnt.Parent
	if gp == nil {
		return sbl, prnt, nil, gp
	}
	ancle := gp.Left
	if ancle == prnt {
		ancle = gp.Right
	}
	return sbl, prnt, ancle, gp
}

///
func (node *RedBlackTreeNode) Transform() {
	if node == nil {
		return
	}
	if node.Color == Black {
		return
	}
	_, prnt, ancle, gp := node.GetAllRelatives()
	if prnt == nil {
		node.Color = Black
		return
	}
	if prnt.Color == Black {
		return
	}

	if prnt != nil && prnt.Color == Red && ancle != nil && ancle.Color == Red {
		node.Recolor(prnt, ancle, gp).Transform()
		return
	}

	if prnt != nil && prnt.Color == Red && (ancle == nil || ancle.Color == Black) && gp.Left == prnt && prnt.Right == node {
		prnt.LeftRotation()
		node = prnt
	}

	if prnt != nil && prnt.Color == Red && (ancle == nil || ancle.Color == Black) && gp.Right == prnt && prnt.Left == node {
		prnt.RightRotation()
		node = prnt
	}

	_, prnt, _, gp = node.GetAllRelatives()

	if prnt != nil && prnt.Color == Red && node == prnt.Left {
		gp.RightRotation()
		gp.Color = Red
		gp.Parent.Color = Black
		return
	}
	if prnt != nil && prnt.Color == Red && node == prnt.Right {
		gp.LeftRotation()
		gp.Color = Red
		gp.Parent.Color = Black

		return
	}
}

func (node *RedBlackTreeNode) Recolor(prnt *RedBlackTreeNode, ancle *RedBlackTreeNode, gp *RedBlackTreeNode) *RedBlackTreeNode {
	prnt.Color = Black
	ancle.Color = Black
	gp.Color = Red
	return gp
}

// root of rotation
func (node *RedBlackTreeNode) LeftRotation() error {
	if node.Right == nil {
		return errors.New("left rotation impossible")
	}
	gp := node.Parent
	newParent := node.Right
	movingChild := node.Right.Left

	if gp != nil {
		if gp.Left == node {
			gp.Left = newParent
		} else {
			gp.Right = newParent
		}
	}
	newParent.Parent = gp

	node.Parent = newParent
	newParent.Left = node

	node.Right = movingChild
	if movingChild != nil {
		movingChild.Parent = node
	}
	return nil
}

// root of rotation
func (node *RedBlackTreeNode) RightRotation() error {
	if node.Left == nil {
		return errors.New("right rotation impossible")
	}
	gp := node.Parent
	newParent := node.Left
	movingChild := node.Left.Right

	if gp != nil {
		if gp.Left == node {
			gp.Left = newParent
		} else {
			gp.Right = newParent
		}
	}
	newParent.Parent = gp

	node.Parent = newParent
	newParent.Right = node

	node.Left = movingChild
	if movingChild != nil {
		movingChild.Parent = node
	}
	return nil
}

func (root *RedBlackTreeNode) Insert(value int) {
	root.AddNode(value)
	root.Transform()
}
func (node *RedBlackTreeNode) AddNode(value int) *RedBlackTreeNode {
	if value > node.Value {
		if node.Right == nil {
			node.Right = NewRedNode(value)
			node.Right.Parent = node
			return node.Right
		} else {
			return node.Right.AddNode(value)
		}
	} else {
		if node.Left == nil {
			node.Left = NewRedNode(value)
			node.Left.Parent = node
			return node.Left
		} else {
			return node.Left.AddNode(value)
		}
	}
}

func (node *RedBlackTreeNode) FindExact(value int) *RedBlackTreeNode {

	if node.Value == value {
		return node
	}
	if value <= node.Value {
		if node.Left == nil {
			return nil
		} else {
			return node.Left.FindExact(value)
		}
	}
	if value > node.Value {
		if node.Right == nil {
			return nil
		} else {
			return node.Right.FindExact(value)
		}
	}
	return nil
}

func (node *RedBlackTreeNode) FindLessOrEqual(value int) *RedBlackTreeNode {

	if node.Value == value {
		return node
	}

	if node.Value > value {
		if node.Left == nil {
			return nil
		}
		return node.Left.FindLessOrEqual(value)
	}

	if node.Right == nil {
		return node
	}
	v1 := node.Right.FindLessOrEqual(value)
	if v1 == nil {
		return node
	}
	if v1.Value > node.Value {
		return v1
	}
	return node
}

func (node *RedBlackTreeNode) GetMin() *RedBlackTreeNode {

	for ; node.Left != nil; node = node.Left {
	}
	return node
}
