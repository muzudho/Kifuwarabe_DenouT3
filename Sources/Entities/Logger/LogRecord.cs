namespace Grayscale.Kifuwarakaku.Entities.Logger
{
    /// <summary>
    /// ログの書き込み先情報。
    /// </summary>
    public class LogRecord : ILogRecord
    {
        /// <summary>
        /// ファイル名。
        /// </summary>
        public string FileName { get { return $"{this.FileStem}{this.Extension}"; } }

        /// <summary>
        /// 拡張子抜きのファイル名。
        /// </summary>
        public string FileStem { get; private set; }

        /// <summary>
        /// ドット付きの拡張子。
        /// 拡張子は .log 固定。ファイル削除の目印にします。
        /// </summary>
        public string Extension { get { return ".log"; } }

        /// <summary>
        /// ログ出力の有無。
        /// </summary>
        public bool Enabled { get { return this.enabled; } }
        private bool enabled;

        /// <summary>
        /// タイムスタンプの有無。
        /// </summary>
        public bool TimeStampPrintable { get; private set; } = false;

        public LogRecord(string fileStem, bool enabled, bool timeStampPrintable)
        {
            this.FileStem = fileStem;
            this.enabled = enabled;
            this.TimeStampPrintable = timeStampPrintable;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            ILogRecord p = obj as ILogRecord;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return ($"{this.FileStem}{this.Extension}" == $"{p.FileStem}{p.Extension}");
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
