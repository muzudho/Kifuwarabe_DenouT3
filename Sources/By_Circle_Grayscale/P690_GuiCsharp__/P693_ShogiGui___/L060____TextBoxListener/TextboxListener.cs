
namespace Grayscale.P693_ShogiGui___.L060____TextBoxListener
{

    /// <summary>
    /// テキストボックスのテキスト丸ごと返すメソッドです。
    /// </summary>
    /// <returns></returns>
    public delegate string ReadTextMethod();

    /// <summary>
    /// 引数として１行のテキストを１つ受け取り、何も返さないリスナーです。
    /// </summary>
    /// <returns></returns>
    public delegate void WriteLineListener(string text);

    /// <summary>
    /// ************************************************************************************************************************
    /// テキストボックスが、将棋エンジンの振り(*1)をしています。
    /// ************************************************************************************************************************
    /// 
    ///         *1…将棋エンジンがあれば、これを“皮”（ラッピング）にすると、ＧＵＩに対応すると思います。
    /// 
    /// </summary>
    public class TextboxListener
    {

        #region プロパティ類

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 将棋エンジン(*1)をこの中に入れておきます。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        ///         *1…その実、将棋エンジンの振りをさせて、このメインパネルのメソッドが入っています。
        /// 
        /// </summary>
        public static TextboxListener DefaultInstance
        {
            get
            {
                return TextboxListener.defaultInstance;
            }
        }

        public static void SetTextboxListener(
            ReadTextMethod readTextMethod1,
            WriteLineListener writeLineListener1
            )
        {
            TextboxListener.defaultInstance = new TextboxListener(readTextMethod1, writeLineListener1);
        }
        private static TextboxListener defaultInstance;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 入力先１です。　引数を受け取らず、テキストボックスのテキストまるごと返すだけのメソッド型メンバーです。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        private ReadTextMethod readTextMethod1;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 出力先です。　引数として１行のテキストを１つ受け取り、何も返さないメソッド型メンバーです。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        private WriteLineListener writeLineListener;

        #endregion


        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクターです。
        /// ************************************************************************************************************************
        /// </summary>
        public TextboxListener(
            ReadTextMethod readTextMethod1,
            WriteLineListener writeLineMethod
            )
        {
            this.readTextMethod1 = readTextMethod1;
            this.writeLineListener = writeLineMethod;
        }



        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// １行の文字列を返します。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public string ReadText1()
        {
            string value;

            if (null == this.readTextMethod1)
            {
                // 入力先が設定されていませんでした。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                value = "";
                goto gt_EndMethod;
            }

            value = this.readTextMethod1();

        gt_EndMethod:
            return value;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 出力欄（上段）に１行のテキストをセットします。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public void WriteLine(string text)
        {
            if (null == this.writeLineListener)
            {
                // 出力先１が設定されていませんでした。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                goto gt_EndMethod;
            }

            this.writeLineListener(text);

        gt_EndMethod:
            ;
        }

    }
}
