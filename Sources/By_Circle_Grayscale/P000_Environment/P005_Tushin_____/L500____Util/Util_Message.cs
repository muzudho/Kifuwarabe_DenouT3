using ProjectDark.KWSupport;
using System;
using System.Windows.Forms;

namespace Grayscale.P005_Tushin_____.L500____Util
{


    /// <summary>
    /// コンソール入出力を、用途で分類しています。
    /// 
    /// 通信は、次の４者間で行われる想定です。
    /// 
    /// ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
    /// ┃               　　　　　　　　　　　　　　　　　　　　　　ゲームプレイ用　 　　　　　　　　　┃
    /// ┃クライアント    　　　　　　ゲームサーバー　　　　　　　　ＧＵＩ　　　　　　　　　　　　　　　┃
    /// ┃┏━━━━━━┓      　    ┏━━━━━━┓     　　　　┏━━━━━━━┓　　　　　　　　　　┃
    /// ┃┃　　　　　　┃　←──　　┃           ┃ 　←──　　 ┃　　　　　　　┃　←─┐　　　　　  ┃
    /// ┃┃　    ①　　┃（Download）┃　　 ② 　　┃ （Request） ┃　④　　　　　┃ 　 　│　　 　　　 ┃
    /// ┃┃将棋エンジン┃ 　　　　　 ┃将棋サーバー┃　　　　　　 ┃ゲームエンジン┃　 　 │　　 　　　 ┃
    /// ┃┃　　　　　　┃  ──→　　┃           ┃   ──→　　 ┃ＡＩＭＳ　　　┃　──┘　　 　　　 ┃
    /// ┃┗━━━━━━┛ （Upload） ┗━━━━━━┛  （Response）┗━━━━━━━┛（whisper）ﾃﾞﾊﾞｯｸﾞ用┃
    /// ┃               　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　（Show）ｴﾗｰ用　　　┃
    /// ┃               　　　　　　　│　　　↑　　　　　　　　　　　　　　　　　　 　　　　　　　　　┃
    /// ┃               　　　（Pull）│　　　│（Push）　　　　　　　　　　　　　　　　　　　　　　　　┃
    /// ┃               　　　　　　　↓　　　│　　　　　　　　　　　　　　　　　　 　　　　　　　　　┃
    /// ┃               　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　 　　　　　　　　　┃
    /// ┃               　　　　　棋譜入力等　　　　　　　　　　　　　　　　　　　　 　　　　　　　　　┃
    /// ┃               　　　　　補助用Ｃ＃フォーム　　　　　　　　　　　　　　　　 　　　　　　　　　┃
    /// ┃               　　　　　┏━━━━━━━━┓　　　　　　　　　　　　　　　　　　　　　　　　　┃
    /// ┃               　　　　　┃　　　③　　　　┃　　　　　　　　　　　　　　　　 　　　　　　　　┃
    /// ┃               　　　　　┃ 将棋ターミナル ┃　　　　　　　　　　　 　　　　　　　　　　　　　┃
    /// ┃               　　　　　┗━━━━━━━━┛　　　　　　　　　　　　　　　　　　　　　　　　　┃
    /// ┃               　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　 　　　　　　　　　┃
    /// ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
    /// 
    /// その通信方向に、便宜的に名称を付けました。
    /// 
    /// </summary>
    public abstract class Util_Message
    {




        /// <summary>
        /// 将棋サーバーから何かメッセージが届いていないか、見てみます。入っていなければヌル。
        /// 将棋エンジンが使います。
        /// </summary>
        /// <returns></returns>
        public static string Download_Nonstop()
        {
            string line = TimeoutReader.ReadLine(3000);//3秒だけブロック
            //string line = Console.In.ReadLine();//改行が入ってくるまで、ブロックしてしまう。

            return line;
        }

        /// <summary>
        /// 将棋サーバーに向かってメッセージを送り出します。
        /// 将棋エンジンが使います。
        /// </summary>
        /// <param name="message"></param>
        public static void Upload(string message)
        {
            Console.Out.WriteLine(message);
        }

        /// <summary>
        /// TODO:未実装
        /// </summary>
        /// <returns></returns>
        public static string Pull()
        {
            return "";
        }

        /// <summary>
        /// TODO:未実装
        /// </summary>
        /// <param name="message"></param>
        public static void Push(string message)
        {
        }


        /// <summary>
        /// TODO:未実装
        /// </summary>
        /// <returns></returns>
        public static string Response()
        {
            return "";
        }

        /// <summary>
        /// TODO:未実装
        /// </summary>
        /// <param name="message"></param>
        public static void Request(string message)
        {
        }

        /// <summary>
        /// GUI用。デバッグライト
        /// FIXME: 将棋エンジンでも使っていることがありますが、表示されません。
        /// </summary>
        /// <param name="message"></param>
        public static void Whisper(string message)
        {
            System.Console.WriteLine(message);
        }

        /// <summary>
        /// GUI用。エラーライト
        /// FIXME: 将棋エンジンでも使っていることがありますが、表示されません。
        /// </summary>
        /// <param name="message"></param>
        public static void Show(string message)
        {
            System.Console.WriteLine(message);
            MessageBox.Show(message);
        }

        /// <summary>
        /// デバッグ用。
        /// コンソール・アプリケーションを、一時停止目的で、入力待ち状態にします。
        /// </summary>
        public static void Debugger_ManualIn_PleasePushKey()
        {
            System.Console.ReadKey();
        }


    }

}
