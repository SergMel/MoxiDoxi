using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{
    [TestClass]
    public class LCSUT
    {
        [TestMethod]
        public void TestMethod1()
        {            
            
            Assert.AreEqual("", LCS.Find("a", "b"));            
        }

        [TestMethod]
        public void TestMethod2()
        {            
            
            Assert.AreEqual("", LCS.Find("", "b"));            
        }
        
        [TestMethod]
        public void TestMethod3()
        {            
            
            Assert.AreEqual("", LCS.Find("a", ""));            
        }
        
        [TestMethod]
        public void TestMethod4()
        {                       
            Assert.AreEqual("a", LCS.Find("a", "bab"));            
        }

        [TestMethod]
        public void TestMethod5()
        {                       
            Assert.AreEqual("GTAB", LCS.Find("AGGTAB", "GXTXAYB"));            
        }   

        [TestMethod]
        public void TestMethod6()
        {                       
            Assert.AreEqual("ADH", LCS.Find("ABCDGH", "AEDFHR"));            
        }                      
    }
}
