using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{
    [TestClass]
    public class MultiplicativeInverseUT
    {
        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void EuclideanExt_Test1()
        {
            MultiplicativeInverse.inverse(6, 4);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void EuclideanExt_Test2()
        {
            MultiplicativeInverse.inverse(3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void EuclideanExt_Test3()
        {
            MultiplicativeInverse.inverse(3, -5);
        }

        [TestMethod]
        public void EuclideanExt_Test4()
        {
            Assert.AreEqual(2, MultiplicativeInverse.inverse(3, 5));
            Assert.AreEqual(2, MultiplicativeInverse.inverse(8, 5));
            Assert.AreEqual(1, MultiplicativeInverse.inverse(9, 4));
            Assert.AreEqual(9, MultiplicativeInverse.inverse(9, 10));
        }
    }
}
