namespace Grayscale.Kifuwarakaku.Entities.Logger
{
    /// <summary>
    /// ログの書き込み先情報。
    /// </summary>
    public interface ILogRecord
    {
        /// <summary>
        /// ファイル名。
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// 拡張子を除くファイル名。
        /// </summary>
        string FileStem { get; }

        /// <summary>
        /// ドットを含む拡張子。
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// ログ出力の有無。
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// タイムスタンプの有無。
        /// </summary>
        bool TimeStampPrintable { get; }
    }
}
