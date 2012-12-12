namespace ScreenCapturerTests.UnitTests
{
    using System;
    using System.Drawing;
    using System.IO;

    using NUnit.Framework;

    using ScreenCapturer.Code;

    /// <summary>
    /// Tests the class ScreenCapturer.Utility
    /// </summary>
    [TestFixture]
    public class UtilityTests
    {
        private Point first;
        private Point second;
        private DateTime dt;
        
        #region ArePointsFarFromEachother

        [Test]
        public void ArePointsFarFromEachother_AdjacentPointsTest()
        {
            first = new Point(0, 0);
            second = new Point(1, 1);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(first, second));

            first = new Point(-2, -4);
            second = new Point(1,5);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(first, second));
        }

        [Test]
        public void ArePointsFarFromEachother_DistantPoints()
        {
            first = new Point(5000, 9001);
            second = new Point(-4711, -12345);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(first, second));
        }

        [Test]
        public void ArePointsFarFromEachother_OppositePoints()
        {
            first = new Point(32, -32);
            second = new Point(-32, 32);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(first, second));
        }

        [Test]
        public void ArePointsFarFromEachother_IdenticalPoints()
        {
            first = new Point(0, 0);
            second = new Point(0, 0);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(first, second));

            first = new Point(17, 4711);
            second = first;
            Assert.IsFalse(Utility.ArePointsFarFromEachother(first, second));

            first = new Point(500, 500);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(first, first));
        }

        [Test]
        public void ArePointsFarFromEachother_ReverseParameters()
        {
            first = new Point(100, 25);
            second = new Point(-32, 32);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(first, second));
            Assert.IsTrue(Utility.ArePointsFarFromEachother(second, first));

            first = new Point(10, 5);
            second = new Point(7, 8);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(first, second));
            Assert.IsFalse(Utility.ArePointsFarFromEachother(second, first));
        }

        [Test]
        public void ArePointsFarFromEachother_ComponentsAreClose()
        {
            // X components are close.
            first = new Point(0, 30);
            second = new Point(5, 500);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(first, second));

            // Y components are close.
            first = new Point(10000, 25);
            second = new Point(-5000, 19);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(first, second));
        }

        #endregion

        #region DateFileToString

        [Test]
        public void DateFileToString_ReturnsValidPathTest()
        {
            // 2012-01-01 00:00:00
            dt = new DateTime(2012, 1, 1, 0, 0, 0);
            var result = Utility.DateToFileString(dt);

            Assert.IsTrue(result.IndexOfAny(Path.GetInvalidPathChars()) == -1);
        }

        [Test]
        public void DateFileToString_ReturnsExpectedValueTest()
        {
            // 2012-01-01 00:00:00
            dt = new DateTime(2012, 1, 1, 0, 0, 0);
            var result = Utility.DateToFileString(dt);

            Assert.AreEqual(result, "2012-01-01_00.00.00");
        }

        #endregion
    }
}
