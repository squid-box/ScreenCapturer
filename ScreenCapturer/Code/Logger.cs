namespace ScreenCapturer.Code
{
    using System;
    using System.IO;

    /// <summary>
    /// Simple logging utility
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Internal instance of Logger.
        /// </summary>
        private static Logger _logger;

        private StreamWriter _sw;

        private LogLevel _level;

        public static LogLevel Level
        {
            get
            {
                return (LogLevel) Properties.Settings.Default.LogLevel;
            }
        }

        /// <summary>
        /// Get singleton instance of Logger.
        /// </summary>
        public static Logger Instance
        {
            get
            {
                if (_logger == null)
                {
                    _logger = new Logger();
                }

                return _logger;
            }
        }
        
        private Logger()
        {
            _level = (LogLevel) Properties.Settings.Default.LogLevel;

            _sw = new StreamWriter(Properties.Settings.Default.LogPath, true, System.Text.Encoding.UTF8);
            _sw.AutoFlush = true;
        }

        private void writeLog(string prefix, string message)
        {
            _sw.WriteLine("[{0}] | {1} | {2}", prefix, Utility.DateToFileString(DateTime.Now), message);
            _sw.Flush();
        }

        /// <summary>
        /// Debug message.
        /// </summary>
        /// <param name="message">Information to be logged.</param>
        public void Debug(string message)
        {
            if (_level >= LogLevel.Debug)
                writeLog("DBG", message);
        }

        /// <summary>
        /// Warning message.
        /// </summary>
        /// <param name="message">Information to be logged.</param>
        public void Warning(string message)
        {
            if (_level >= LogLevel.Warning)
                writeLog("WRN", message);
        }

        /// <summary>
        /// Error message.
        /// </summary>
        /// <param name="message">Information to be logged.</param>
        public void Error(string message)
        {
            if (_level >= LogLevel.Error)
                writeLog("ERR", message);
        }

        /// <summary>
        /// Logs a given exception.
        /// </summary>
        /// <param name="e">Exception to be logged.</param>
        public void Exception(string message, Exception e)
        {
            if (_level >= LogLevel.Exception)
            {
                writeLog("EXC", message);
                writeLog("EXC", string.Format("Caught exception {0} : {1}", e.GetType(), e.Message));
                writeLog("EXC", e.StackTrace);
            }
        }
    }
}
