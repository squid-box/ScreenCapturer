namespace ScreenCapturerTests.UnitTests
{
    using System.Windows.Forms;

    using NUnit.Framework;

    using ScreenCapturer.Code;

    [TestFixture]
    class CapturerTests
    {
        private Capturer _capturer;

        private MouseEventArgs _mouseLeftDown1, _mouseLeftUp1, _mouseDragDown, _mouseDragUp;

        [SetUp]
        public void SetUp()
        {
            _capturer = new Capturer();

            // Regular click in one spot.
            _mouseLeftDown1 = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
            _mouseLeftUp1 = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);

            // Click-and-drag
            _mouseDragDown = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
            _mouseDragUp = new MouseEventArgs(MouseButtons.Left, 1, 100, 100, 0);
        }

        [TearDown]
        public void TearDown()
        {
            _capturer = null;
        }

        [Test]
        public void TakeShotTest()
        {
            try
            {
                // No shots in capturer at start.
                Assert.AreEqual(0, _capturer.Shots.Count);

                _capturer.IsTakingShots = true;

                // Take shot and check
                _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));
                Assert.AreEqual(1, _capturer.Shots.Count);

                _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));
                _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));
                Assert.AreEqual(3, _capturer.Shots.Count);

                _capturer.TakeShot(new ScreenshotArgs(_mouseDragDown, _mouseDragUp, false));
                Assert.AreEqual(4, _capturer.Shots.Count);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Most likely a "The handle is invalid" exception.  No UI on test runner.
                Assert.Ignore("Test runner does not have access to UI.");
            }
        }

        [Test]
        public void CheckShotsBufferTest()
        {
            try
            {
                // No shots in capturer at start.
                Assert.AreEqual(0, _capturer.Shots.Count);

                _capturer.IsTakingShots = true;

                // Fill the Shots to the "buffer limit"
                for (var i = 0; i < _capturer.Buffersize + 1; i++)
                {
                    _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));
                }

                // Assert that limit is reached.
                Assert.AreEqual(_capturer.Buffersize, _capturer.Shots.Count);

                // Assert that limit is not breached.
                _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));
                Assert.AreEqual(_capturer.Buffersize, _capturer.Shots.Count);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Most likely a "The handle is invalid" exception.  No UI on test runner.
                Assert.Ignore("Test runner does not have access to UI.");
            }
        }

        [Test]
        public void CheckIsTakingShotsWorksTest()
        {
            try
            {
                // No shots in capturer at start.
                Assert.AreEqual(0, _capturer.Shots.Count);

                _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));
                Assert.AreEqual(0, _capturer.Shots.Count);

                _capturer.IsTakingShots = true;

                _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));
                Assert.AreEqual(1, _capturer.Shots.Count);

                _capturer.IsTakingShots = false;
                _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));
                Assert.AreEqual(1, _capturer.Shots.Count);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Most likely a "The handle is invalid" exception.  No UI on test runner.
                Assert.Ignore("Test runner does not have access to UI.");
            }
        }

        [Test]
        public void SaveFilesClearShotsTest()
        {
            try
            {
                // No shots in capturer at start.
                Assert.AreEqual(0, _capturer.Shots.Count);

                _capturer.IsTakingShots = true;
                _capturer.TakeShot(new ScreenshotArgs(_mouseLeftDown1, _mouseLeftUp1, false));

                // Save shot to file, then remove the zip immediately.
                _capturer.SaveShots();
                System.IO.Directory.Delete(ScreenCapturer.Properties.Settings.Default.SaveFolder, true);

                Assert.AreEqual(0, _capturer.Shots.Count);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Most likely a "The handle is invalid" exception.  No UI on test runner.
                Assert.Ignore("Test runner does not have access to UI.");
            }
        }
    }
}
