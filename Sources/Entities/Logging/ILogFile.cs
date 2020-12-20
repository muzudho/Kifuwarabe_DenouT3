namespace Grayscale.Kifuwarakaku.Entities.Logging
{
    /// <summary>
    /// ログの書き込み先情報。
    /// </summary>
    public interface ILogFile
    {
        /// <summary>
        /// ファイル名。
        /// </summary>
        string Name { get; }
    }
}
