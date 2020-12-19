namespace Grayscale.Kifuwarakaku.Entities.Logging
{
    /// <summary>
    /// ログを書くタイミングで。
    /// </summary>
    /// <param name="log"></param>
    public delegate void OnAppendLogDelegate(string log);
    public delegate void OnClearLogDelegate();

    /// <summary>
    /// エラーの対応はお任せ、エラーハンドラー☆！
    /// </summary>
    public interface ILogEvent
    {
        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。任意に着脱可。Null可。
        /// </summary>
        OnAppendLogDelegate OnAppendLog { get; set; }
        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。任意に着脱可。Null可。
        /// </summary>
        OnClearLogDelegate OnClearLog { get; set; }
    }
}
