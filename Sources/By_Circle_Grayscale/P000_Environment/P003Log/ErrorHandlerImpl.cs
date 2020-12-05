using Grayscale.P003Log.I500Struct;
using System;
using System.Diagnostics;

namespace Grayscale.P003Log.L500Struct
{

    /// <summary>
    /// エラー・ハンドラーです。
    /// </summary>
    public class ErrorHandlerImpl : IKwErrorHandler
    {
        /// <summary>
        /// ロガー。
        /// </summary>
        public IKwLogger Logger
        {
            get
            {
                return this.logger;
            }
        }
        private IKwLogger logger;

        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。
        /// </summary>
        public DLGTOnLogAppend DlgtOnLog1AppendOrNull{get;set;}
        public DLGTOnLogClear DlgtOnLog1ClearOrNull { get; set; }

        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その２＞。主にフォームにログ出力するのに使う。
        /// </summary>
        public DLGTOnLogAppend DlgtOnNaibuDataAppendOrNull { get; set; }
        public DLGTOnLogClear DlgtOnNaibuDataClearOrNull { get; set; }


        public ErrorHandlerImpl( IKwLogger logTag)
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
            this.Logger.WriteLineError(message);
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
            this.Logger.WriteLineError(message);
            // ログ出力に失敗することがありますが、無視します。
        }
    }

}
