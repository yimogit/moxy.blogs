using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Moxy.Tests
{
    [TestClass]
    public class BaseTest
    {
        [TestMethod]
        public void 位运算符测试()
        {
            var a = false;
            a |= true;
            Assert.IsTrue(a);

            var b = true;
            b |= false;
            Assert.IsTrue(b);

            var c = false;
            var c2 = false;
            c = c || c2 == false;
            //等于下面
            c2 |= c;
            Assert.IsTrue(c);
            Assert.IsTrue(c2);
            // & 使用测试
            var ab = a &= b;
            Assert.IsTrue(ab);
            var abc = ab &= !c;
            Assert.IsFalse(abc);
            // |
            var testStr = "";
            Func<string, bool> funcTest = (str) =>
                {
                    testStr = str;
                    return false;
                };
            //会执行 funcTest
            var test = true | funcTest("test1");
            //不会会执行 funcTest
            test = true || funcTest("test2");
            Assert.AreEqual(testStr, "test1");
        }
    }
}
