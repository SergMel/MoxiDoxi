// https://www.geeksforgeeks.org/binary-search-tree-data-structure/
// https://www.geeksforgeeks.org/find-the-minimum-element-in-a-binary-search-tree/

using System;
using System.Collections.Generic;

public class BinarySearchTree
{
    public class Node
    {
        public int Val;
        public Node Left;
        public Node Right;
    }

    public static int FindMin(Node root) {
        if (root == null ) throw new Exception();
       
        while(root.Left != null ){
            root = root.Left;
        }
        return root.Val;

    }
}