namespace Grayscale.Kifuwarakaku.Entities.Logging
{
    /// <summary>
    /// ログの書き込み先情報。
    /// </summary>
    public class LogRecord : ILogRecord
    {
        public LogRecord(ILogFile logFile, bool enabled, bool timeStampPrintable)
        {
            this.LogFile = logFile;
            this.Enabled = enabled;
            this.TimeStampPrintable = timeStampPrintable;
        }

        /// <summary>
        /// 出力先ファイル。
        /// </summary>
        public ILogFile LogFile { get; private set; }

        /// <summary>
        /// ログ出力の有無。
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// タイムスタンプ出力の有無。
        /// </summary>
        public bool TimeStampPrintable { get; private set; }
    }
}
