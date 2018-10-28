using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moxy.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Tests.UtilsTest
{
    [TestClass]
    public class SecurityHelperTest
    {
        [TestMethod]
        public void 序列化()
        {
            var result1 = JsonHelper.Serialize(null);
            var result2 = JsonHelper.Serialize("233");
            var result3 = JsonHelper.Serialize(1);
            var result4 = JsonHelper.Serialize(new { test = 1 });
        }
    }
}
