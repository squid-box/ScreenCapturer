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
        private Point _first;
        private Point _second;
        private DateTime _dt;
        
        #region ArePointsFarFromEachother

        [Test]
        public void ArePointsFarFromEachotherAdjacentPointsTest()
        {
            _first = new Point(0, 0);
            _second = new Point(1, 1);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(_first, _second));

            _first = new Point(-2, -4);
            _second = new Point(1,5);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(_first, _second));
        }

        [Test]
        public void ArePointsFarFromEachotherDistantPoints()
        {
            _first = new Point(5000, 9001);
            _second = new Point(-4711, -12345);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(_first, _second));
        }

        [Test]
        public void ArePointsFarFromEachotherOppositePoints()
        {
            _first = new Point(32, -32);
            _second = new Point(-32, 32);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(_first, _second));
        }

        [Test]
        public void ArePointsFarFromEachotherIdenticalPoints()
        {
            _first = new Point(0, 0);
            _second = new Point(0, 0);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(_first, _second));

            _first = new Point(17, 4711);
            _second = _first;
            Assert.IsFalse(Utility.ArePointsFarFromEachother(_first, _second));

            _first = new Point(500, 500);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(_first, _first));
        }

        [Test]
        public void ArePointsFarFromEachotherReverseParameters()
        {
            _first = new Point(100, 25);
            _second = new Point(-32, 32);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(_first, _second));
            Assert.IsTrue(Utility.ArePointsFarFromEachother(_second, _first));

            _first = new Point(10, 5);
            _second = new Point(7, 8);
            Assert.IsFalse(Utility.ArePointsFarFromEachother(_first, _second));
            Assert.IsFalse(Utility.ArePointsFarFromEachother(_second, _first));
        }

        [Test]
        public void ArePointsFarFromEachotherComponentsAreClose()
        {
            // X components are close.
            _first = new Point(0, 30);
            _second = new Point(5, 500);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(_first, _second));

            // Y components are close.
            _first = new Point(10000, 25);
            _second = new Point(-5000, 19);
            Assert.IsTrue(Utility.ArePointsFarFromEachother(_first, _second));
        }

        #endregion

        #region DateFileToString

        [Test]
        public void DateFileToStringReturnsValidPathTest()
        {
            // 2012-01-01 00:00:00
            _dt = new DateTime(2012, 1, 1, 0, 0, 0);
            var result = Utility.DateToFileString(_dt);

            Assert.IsTrue(result.IndexOfAny(Path.GetInvalidPathChars()) == -1);
        }

        [Test]
        public void DateFileToStringReturnsExpectedValueTest()
        {
            // 2012-01-01 00:00:00
            _dt = new DateTime(2012, 1, 1, 0, 0, 0);
            var result = Utility.DateToFileString(_dt);

            Assert.AreEqual("2012-01-01_00.00.00", result);
        }

        #endregion
    }
}
