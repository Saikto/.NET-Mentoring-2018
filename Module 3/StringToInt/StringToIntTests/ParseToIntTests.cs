using System;
using StringToIntLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringToIntTests
{
    [TestClass]
    public class ParseToIntTests
    {
        [TestMethod]
        public void CheckPositive()
        {
            string str = "123";
            int result = str.ParseToInt();

            Assert.AreEqual(123, result);
        }

        [TestMethod]
        public void CheckNegative()
        {
            string str = "-56738";
            int result = str.ParseToInt();

            Assert.AreEqual(-56738, result);
        }

        [TestMethod]
        public void CheckValueWithPlusSign()
        {
            string str = "+1293192";
            int result = str.ParseToInt();

            Assert.AreEqual(1293192, result);
        }

        [TestMethod]
        public void CheckStarsFromZero()
        {
            string str = "0327327";
            int result = str.ParseToInt();

            Assert.AreEqual(327327, result);
        }

        [TestMethod]
        public void CheckNullThrowsArgumentException()
        {
            string str = null;

            Assert.ThrowsException<ArgumentException>(() => str.ParseToInt());
        }

        [TestMethod]
        public void CheckEmptyThrowsArgumentException()
        {
            string str = "";

            Assert.ThrowsException<ArgumentException>(() => str.ParseToInt());
        }

        [TestMethod]
        public void CheckWhiteSpaceThrowsArgumentException()
        {
            string str = " ";

            Assert.ThrowsException<ArgumentException>(() => str.ParseToInt());
        }

        [TestMethod]
        public void CheckIncorrectSymbolThrowsFormatException()
        {
            string str = "2134h";

            Assert.ThrowsException<FormatException>(() => str.ParseToInt());
        }

        [TestMethod]
        public void CheckBigStringThrowsOverflowException()
        {
            string str = "84044431958077281477490767992692042349751181354054";
            string str1 = "-84044431958077281477490767992692042349751181354054";

            Assert.ThrowsException<OverflowException>(() => str.ParseToInt());
            Assert.ThrowsException<OverflowException>(() => str1.ParseToInt());
        }
    }
}
