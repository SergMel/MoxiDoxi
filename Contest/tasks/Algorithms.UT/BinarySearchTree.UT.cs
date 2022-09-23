using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BinarySearchTree;

namespace Algorithms
{
    [TestClass]
    public class BinarySearchTreeUT
    {
        [TestMethod]
        public void TestMethod1()
        {            
            Node root = new Node(){
                Val = 20,
                Left = new Node{
                    Val = 8,
                    Left = new Node{
                        Val = 4
                    },
                    Right = new Node{
                        Val = 12,
                        Left = new Node{
                            Val = 10
                        },
                        Right = new Node{
                            Val = 14
                        }
                    }
                },
                Right = new Node{
                    Val = 22
                }
            };            
            
            Assert.AreEqual(4, BinarySearchTree.FindMin(root));
        }

    }
}
