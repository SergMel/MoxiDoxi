using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{
    [TestClass]
    public class EuclideanExtUT
    {
        [TestMethod]
        public void TestMethod1()
        {            
            var res = Euclidean.PositiveRem(4,2);
            Assert.AreEqual(2, res.Quotient);
            Assert.AreEqual(0, res.Reminder);
        }

        [TestMethod]
        public void TestMethod2()
        {            
            var res = Euclidean.PositiveRem(3,2);
            Assert.AreEqual(1, res.Quotient);
            Assert.AreEqual(1, res.Reminder);
        }        

        [TestMethod]
        public void TestMethod3()
        {            
            var res = Euclidean.PositiveRem(-3,2);
            Assert.AreEqual(-2, res.Quotient);
            Assert.AreEqual(1, res.Reminder);
        }

        [TestMethod]
        public void TestMethod4()
        {            
            var (q, rem) = Euclidean.PositiveRem(-3,-2);
            Assert.AreEqual(2, q);
            Assert.AreEqual(1, rem);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestMethod5()
        {            
            var (q, rem) = Euclidean.PositiveRem(-3,0);            
        }

        [TestMethod]
        public void TestMethod6()
        {            
            var (q, rem) = Euclidean.PositiveRem(0,-2);
            Assert.AreEqual(0, q);
            Assert.AreEqual(0, rem);
        }

        [TestMethod]
        public void TestMethod7()
        {            
            var (q, rem) = Euclidean.PositiveRem(7,3);
            Assert.AreEqual(2, q);
            Assert.AreEqual(1, rem);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestMethod8()
        {            
            var (q, rem) = Euclidean.PositiveRem(0,0);            
        }

        [TestMethod]
        public void EuclideanExt_Test1()
        { 
            var (x, y, gcd) = Euclidean.EuclideanExt(4, 6);    
            
            Assert.AreEqual(2, gcd);
            Assert.AreEqual(gcd, x*4+6*y);
        }  

        [TestMethod]
        public void EuclideanExt_Test2()
        { 
            var (x, y, gcd) = Euclidean.EuclideanExt(6, 4);    
            
            Assert.AreEqual(2, gcd);
            Assert.AreEqual(gcd, x*6+4*y);
        }

        [TestMethod]
        public void EuclideanExt_Test3()
        { 
            var (x, y, gcd) = Euclidean.EuclideanExt(0, 0);    
            
            Assert.AreEqual(0, gcd);
            Assert.AreEqual(0, x);
            Assert.AreEqual(0, y);
        }

        [TestMethod]
        public void EuclideanExt_Test4()
        { 
            var (x, y, gcd) = Euclidean.EuclideanExt(3, 5);    
            
            Assert.AreEqual(1, gcd);
            Assert.AreEqual(gcd, x*3+5*y);
        }

        [TestMethod]
        public void EuclideanExt_Test5()
        { 
            int a = 0;
            int b = 5;
            int egcd = 5;
            var (x, y, gcd) = Euclidean.EuclideanExt(a, b);    
            
            Assert.AreEqual(egcd, gcd);
            Assert.AreEqual(gcd, x*a+b*y);
        }

        [TestMethod]
        public void EuclideanExt_Test6()
        { 
            int a = 5;
            int b = 0;
            int egcd = 5;
            var (x, y, gcd) = Euclidean.EuclideanExt(a, b);    
            
            Assert.AreEqual(egcd, gcd);
            Assert.AreEqual(gcd, x*a+b*y);
        }

        [TestMethod]
        public void EuclideanExt_Test7()
        { 
            int a = 5;
            int b = -3;
            int egcd = 1;
            var (x, y, gcd) = Euclidean.EuclideanExt(a, b);    
            
            Assert.AreEqual(egcd, gcd);
            Assert.AreEqual(gcd, x*a+b*y);
        }

        [TestMethod]
        public void EuclideanExt_Test8()
        { 
            int a = 10;
            int b = -6;
            int egcd = 2;
            var (x, y, gcd) = Euclidean.EuclideanExt(a, b);    
            
            Assert.AreEqual(egcd, gcd);
            Assert.AreEqual(gcd, x*a+b*y);
        }         
    }
}
