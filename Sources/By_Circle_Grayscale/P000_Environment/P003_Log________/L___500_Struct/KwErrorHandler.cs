using System;

namespace Grayscale.P003_Log________.L___500_Struct
{

    /// <summary>
    /// ログを書くタイミングで。
    /// </summary>
    /// <param name="log"></param>
    public delegate void DLGT_OnLogAppend(string log);
    public delegate void DLGT_OnLogClear();

    /// <summary>
    /// エラーの対応はお任せ、エラーハンドラー☆！
    /// </summary>
    public interface KwErrorHandler
    {

        /// <summary>
        /// ロガー。
        /// </summary>
        KwLogger Logger { get; }

        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。任意に着脱可。
        /// </summary>
        DLGT_OnLogAppend Dlgt_OnLog1Append_or_Null { get; set; }
        DLGT_OnLogClear Dlgt_OnLog1Clear_or_Null { get; set; }

        ///// <summary>
        ///// 用途は任意のイベント・ハンドラー＜その２＞。主にフォームにログ出力するのに使う。任意に着脱可。
        ///// </summary>
        //DLGT_OnLogAppend Dlgt_OnNaibuDataAppend_or_Null { get; set; }
        //DLGT_OnLogClear Dlgt_OnNaibuDataClear_or_Null { get; set; }

        /// <summary>
        /// 「どうにもならん、あきらめた」
        /// 
        /// 例外が発生したが、対応できないのでログだけ出して例外をさらに上へ投げ返すとき。
        /// </summary>
        /// <param name="okottaBasho"></param>
        void DonimoNaranAkirameta( string okottaBasho);


        /// <summary>
        /// 「どうにもならん、あきらめた」
        /// 
        /// 例外が発生したが、対応できないのでログだけ出して例外をさらに上へ投げ返すとき。
        /// </summary>
        /// <param name="okottaBasho"></param>
        void DonimoNaranAkirameta(Exception ex, string okottaBasho);

    }
}
