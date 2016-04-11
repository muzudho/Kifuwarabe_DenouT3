using Grayscale.P003_Log________.L___500_Struct;
using System;
using System.Diagnostics;

namespace Grayscale.P003_Log________.L500____Struct
{

    /// <summary>
    /// エラー・ハンドラーです。
    /// </summary>
    public class ErrorHandlerImpl : KwErrorHandler
    {
        /// <summary>
        /// ロガー。
        /// </summary>
        public KwLogger Logger
        {
            get
            {
                return this.logger;
            }
        }
        private KwLogger logger;

        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。
        /// </summary>
        public DLGT_OnLogAppend Dlgt_OnLog1Append_or_Null{get;set;}
        public DLGT_OnLogClear Dlgt_OnLog1Clear_or_Null { get; set; }

        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その２＞。主にフォームにログ出力するのに使う。
        /// </summary>
        public DLGT_OnLogAppend Dlgt_OnNaibuDataAppend_or_Null { get; set; }
        public DLGT_OnLogClear Dlgt_OnNaibuDataClear_or_Null { get; set; }


        public ErrorHandlerImpl( KwLogger logTag)
        {
            this.logger = logTag;
        }

        /// <summary>
        /// 「どうにもならん、あきらめた」
        /// 
        /// 例外が発生したが、対応できないのでログだけ出します。
        /// デバッグ時は、ダイアログボックスを出します。
        /// </summary>
        /// <param name="okottaBasho"></param>
        public void DonimoNaranAkirameta( string okottaBasho)
        {
            //>>>>> エラーが起こりました。
            string message = "起こった場所：" + okottaBasho;
            Debug.Fail(message);

            // どうにもできないので  ログだけ取って、上に投げます。
            this.Logger.WriteLine_Error(message);
            // ログ出力に失敗することがありますが、無視します。
        }

        /// <summary>
        /// 「どうにもならん、あきらめた」
        /// 
        /// 例外が発生したが、対応できないのでログだけ出します。
        /// デバッグ時は、ダイアログボックスを出します。
        /// </summary>
        /// <param name="okottaBasho"></param>
        public void DonimoNaranAkirameta( Exception ex, string okottaBasho)
        {
            //>>>>> エラーが起こりました。
            string message = ex.GetType().Name + " " + ex.Message + "：" + okottaBasho;
            Debug.Fail(message);

            // どうにもできないので  ログだけ取って、上に投げます。
            this.Logger.WriteLine_Error(message);
            // ログ出力に失敗することがありますが、無視します。
        }
    }

}
