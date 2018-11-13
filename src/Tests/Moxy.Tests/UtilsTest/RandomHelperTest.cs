using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using Moxy.Utils;

namespace Moxy.Tests
{
    [TestClass]
    public class RandomHelperTest
    {
        [TestMethod]
        public void NewGuid()
        {
            var str1 = Guid.NewGuid().GetHashCode().ToString("x");
            Assert.IsTrue(str1.Length <= 32);
            var str2 = Guid.NewGuid().ToString("N");
            Assert.IsTrue(str2.Length == 32);
        }
    }
}
