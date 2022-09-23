using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{
    [TestClass]
    public class KMPUT
    {
        [TestMethod]
        public void TestMethod1()
        {            
            var lst = new List<int>(){};            
            var res = KMP.FindAllPatterns("ABABDABACDABABCABAB", "ABABCABAB");
            CollectionAssert.AreEqual(new List<int>{10}, res);
            
        }

        [TestMethod]
        public void TestMethod2()
        {            
            var lst = new List<int>(){};            
            var res = KMP.FindAllPatterns("THIS IS A TEST TEXT", "TEST");
            CollectionAssert.AreEqual(new List<int>{10}, res);
            
        }
    
        [TestMethod]
        public void TestMethod3()
        {            
            var lst = new List<int>(){};            
            var res = KMP.FindAllPatterns("AABAACAADAABAABA", "AABA");
            CollectionAssert.AreEqual(new List<int>{0,9,12}, res);
            
        }    

        [TestMethod]
        public void TestMethod4()
        {            
            var lst = new List<int>(){};            
            var res = KMP.FindAllPatterns("AAAAAAAAAAAAAAAAAB", "AAAAB");
            CollectionAssert.AreEqual(new List<int>{13}, res);
            
        }          

        [TestMethod]
        public void TestMethod5()
        {            
            var lst = new List<int>(){};            
            var res = KMP.FindAllPatterns("ABABABCABABABCABABABC", "ABABAC");
            CollectionAssert.AreEqual(new List<int>{}, res);
            
        }           
    }
}
