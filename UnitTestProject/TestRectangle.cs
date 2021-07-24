using GPLApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Rectangle = GPLApplication.Rectangle;

namespace UnitTestProject
{
    /// <summary>
    /// Declare class name as TestRectangle
    /// </summary>
    [TestClass]
    public class TestRectangle
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
        public void UniTestRect()
        {
            var rect = new Rectangle();
            Color c = Color.Black;
            int x = 80, y = 70, height = 85, width = 65;
            rect.set(c, x, y, height, width);
            Assert.AreEqual(70, rect.y);
           
        }


    }
}
