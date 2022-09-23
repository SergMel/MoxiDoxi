//https://www.geeksforgeeks.org/activity-selection-problem-greedy-algo-1/
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{
    [TestClass]
    public class ActivitySelectionProblemUT
    {
        [TestMethod]
        public void TestMethod1()
        {            
            int[] start  =  {10, 12, 20};
            int[] finish =  {20, 25, 30};
            var res = ActivitySelectionProblem.get_max_activity(start, finish);
            Assert.AreEqual(2, res);
        }

        [TestMethod]
        public void TestMethod2()
        {            
            int[] start  =  {1, 3, 0, 5, 8, 5};
            int[] finish =  {2, 4, 6, 7, 9, 9};
            var res = ActivitySelectionProblem.get_max_activity(start, finish);
            Assert.AreEqual(4, res);
        }   
    }
}
