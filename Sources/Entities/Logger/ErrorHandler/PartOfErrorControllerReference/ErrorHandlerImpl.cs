using Grayscale.Kifuwarakaku.Entities.Logger;
using System;
using System.Diagnostics;

namespace Grayscale.Kifuwarakaku.Entities.Logger
{

    /// <summary>
    /// エラー・ハンドラーです。
    /// </summary>
    public class ErrorHandlerImpl : IErrorController
    {
        /// <summary>
        /// ロガー。
        /// </summary>
        public ILogger Logger
        {
            get
            {
                return this.logger;
            }
        }
        private ILogger logger;

        /// <summary>
        /// 用途は任意のイベント・ハンドラー＜その１＞。主にフォームにログ出力するのに使う。
        /// </summary>
        public OnAppendLogDelegate OnAppendLog{get;set;}
        public OnClearLogDelegate OnClearLog { get; set; }

        public ErrorHandlerImpl( ILogger logTag)
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
        public void Panic( string okottaBasho)
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
        public void Panic( Exception ex, string okottaBasho)
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
