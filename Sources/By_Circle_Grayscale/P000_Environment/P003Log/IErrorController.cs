using System;

namespace Grayscale.P003Log.I500Struct
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
    public interface IErrorController
    {

        /// <summary>
        /// ロガー。
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。任意に着脱可。Null可。
        /// </summary>
        OnAppendLogDelegate OnAppendLog { get; set; }
        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。任意に着脱可。Null可。
        /// </summary>
        OnClearLogDelegate OnClearLog { get; set; }

        /// <summary>
        /// 「どうにもならん、あきらめた」
        /// 
        /// 例外が発生したが、対応できないのでログだけ出して例外をさらに上へ投げ返すとき。
        /// </summary>
        /// <param name="okottaBasho"></param>
        void Panic( string okottaBasho);


        /// <summary>
        /// 「どうにもならん、あきらめた」
        /// 
        /// 例外が発生したが、対応できないのでログだけ出して例外をさらに上へ投げ返すとき。
        /// </summary>
        /// <param name="okottaBasho"></param>
        void Panic(Exception ex, string okottaBasho);

    }
}
