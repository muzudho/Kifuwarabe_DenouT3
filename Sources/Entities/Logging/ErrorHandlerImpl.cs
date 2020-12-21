namespace Grayscale.Kifuwarakaku.Entities.Logging
{

    /// <summary>
    /// エラー・ハンドラーです。
    /// </summary>
    public class ErrorHandlerImpl : ILogEvent
    {
        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。
        /// </summary>
        public OnAppendLogDelegate OnAppendLog { get; set; }
        public OnClearLogDelegate OnClearLog { get; set; }
    }
}
