using ShamosHoey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ShamosHoeyTest
{
    
    
    /// <summary>
    ///This is a test class for LineSegmentTest and is intended
    ///to contain all LineSegmentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LineSegment2DTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void ToStringTest()
        {
            LineSegment2D target = new LineSegment2D(new Point2D(0,1),new Point2D(2,3));
            Assert.AreEqual("(0,1)->(2,3)", target.ToString());
        }

        [TestMethod()]
        public void ConstructorSwapsTest()
        {
            LineSegment2D target = new LineSegment2D(new Point2D(2, 3), new Point2D(0, 1));
            Assert.AreEqual("(0,1)->(2,3)", target.ToString());
        }

        [TestMethod()]
        public void ConstructorSwapsTest2()
        {
            LineSegment2D target = new LineSegment2D(new Point2D(77, 0), new Point2D(87, 1));
            Assert.AreEqual("(77,0)->(87,1)", target.ToString());
        }

        [TestMethod()]
        public void ConstructorSwapsEqualX()
        {
            LineSegment2D target = new LineSegment2D(new Point2D(2, 3), new Point2D(2, 2));
            Assert.AreEqual("(2,2)->(2,3)", target.ToString());
        }

        [TestMethod()]
        public void CompareToTest()
        {
            LineSegment2D s1 = new LineSegment2D(new Point2D(0, 0), new Point2D(2, 2));
            LineSegment2D s2 = new LineSegment2D(new Point2D(0, 2), new Point2D(2, 4));
            Assert.IsTrue(s1.CompareTo(s2) < 0);
            Assert.IsTrue(s1.CompareTo(s1) == 0);
            Assert.IsTrue(s2.CompareTo(s1) > 0);
            Assert.IsTrue(s2.CompareTo(s2) == 0);
        }

    }
}
