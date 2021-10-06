using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{
    [TestClass]
    public class BinarySearchUT
    {
        [TestMethod]
        public void TestMethod1()
        {            
            var lst = new List<int>(){};            
            var res = BinarySearch.FindMaxOfLess(lst, 3);
            Assert.AreEqual(-1, res);
        }

        [TestMethod]
        public void TestMethod2()
        {            
            var lst = new List<int>(){1};            
            var res = BinarySearch.FindMaxOfLess(lst, 3);
            Assert.AreEqual(0, res);
        }        
        
        [TestMethod]
        public void TestMethod3()
        {            
            var lst = new List<int>(){1};            
            var res = BinarySearch.FindMaxOfLess(lst, 1);
            Assert.AreEqual(0, res);
        }        
        
        [TestMethod]
        public void TestMethod4()
        {            
            var lst = new List<int>(){1};            
            var res = BinarySearch.FindMaxOfLess(lst, 0);
            Assert.AreEqual(-1, res);
        }               
        
         [TestMethod]
        public void TestMethod5()
        {            
            var lst = new List<int>(){1, 4, 6};            
            var res = BinarySearch.FindMaxOfLess(lst, 5);
            Assert.AreEqual(1, res);
        }        
    
    }
}
