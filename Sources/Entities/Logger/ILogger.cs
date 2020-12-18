namespace Grayscale.Kifuwarakaku.Entities.Logger
{
    /// <summary>
    /// ロガー。
    /// </summary>
    public interface ILogger
    {
        string FileName { get; }
        string FileStem { get; }
        string Extension { get; }

        /// <summary>
        /// ログ出力の有無。
        /// </summary>
        bool Enabled { get; }

        bool TimeStampPrintable { get; }



        /// <summary>
        /// メモを、ログ・ファイルの末尾に追記します。
        /// </summary>
        /// <param name="line"></param>
        void WriteLineAddMemo(string line);

        /// <summary>
        /// エラーを、ログ・ファイルに記録します。
        /// </summary>
        /// <param name="line"></param>
        void WriteLineError(string line);

        /// <summary>
        /// メモを、ログ・ファイルに記録します。
        /// 
        /// 一旦、ログ・ファイルを空っぽにしたい場合などに。
        /// </summary>
        /// <param name="line"></param>
        void WriteLineOverMemo(string line);

        /// <summary>
        /// サーバーへ送ったコマンドを、ログ・ファイルに記録します。
        /// </summary>
        /// <param name="line"></param>
        void WriteLineS(string line);

        /// <summary>
        /// サーバーから受け取ったコマンドを、ログ・ファイルに記録します。
        /// </summary>
        /// <param name="line"></param>
        void WriteLineC(string line);
    }
}
