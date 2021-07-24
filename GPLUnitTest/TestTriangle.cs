using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace UnitTestProject
{
    /// <summary>
    /// Declar class name as TestTriangle
    /// </summary>
    [TestClass]
    public class TestTriangle
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Get and set the test context that provide
        ///information about and functionality for the current test run
        /// </summary>
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
        /// <summary>
        /// Used to check the resultant value with the actual value
        /// </summary>
        [TestMethod]
        public void UniTestTri()
        {
            
            var tri = new GPLApplication.Triangle();
            Color c = Color.Black;
            int  xcordinate1 = 30, ycordinate1=40, xcordinate2=50, ycordinate2=60, xcordinate3=70, ycordinate3=80, xcordinate4=90, ycordinate4 = 90, xcordinate5=100, ycordinate5=110, xcordinate6=120, ycordinate6 = 130;
            tri.set(c, xcordinate1, ycordinate1, xcordinate2, ycordinate2, xcordinate3, ycordinate3, xcordinate4, ycordinate4, xcordinate5, ycordinate5, xcordinate6, ycordinate6);
            Assert.AreEqual(70, tri.xcordinate3);

        }
    }
}
