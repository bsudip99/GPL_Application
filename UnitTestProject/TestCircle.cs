﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace UnitTestProject
{
    /// <summary>
    /// Declare class as TestCircle
    /// </summary>
    [TestClass]
    public class TestCircle
    {
        /// <summary>
        /// Get and set the test context that provide
        ///information about and functionality for the current test run
        /// </summary>
        private TestContext testContextInstance;

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
        public void UniTestCir()
        {
            var circ = new GPLApplication.Circle();
            Color c = Color.Black;
            int x = 80, y = 70,  radius= 65;
            circ.set(c, x, y, radius);
            Assert.AreEqual(65, circ.radius);

        }
    }
}
