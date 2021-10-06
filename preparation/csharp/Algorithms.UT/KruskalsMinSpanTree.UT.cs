using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{
    [TestClass]
    public class KruskalsMinSpanTreeUT
    {
        [TestMethod]
        public void TestMethod1()
        {            
            var nodes1 = new int[]{0,0,0,1,2};
            var nodes2 = new int[]{1,2,3,3,3};
            var weights = new int[]{10,6,5,15,4};

            var res = KruskalsMinSpanTree.GetWeight(nodes1, nodes2, weights );
            
            Assert.AreEqual(19, res);            
        }

    }
}
