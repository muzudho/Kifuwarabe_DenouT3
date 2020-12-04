using System;
using Grayscale.P003Log.I500Struct;
using Grayscale.P003Log.L500Struct;
using Grayscale.P571KifuWarabe.L500KifuWarabe;

namespace Grayscale.P580_Form_______
{
    /// <summary>
    /// プログラムのエントリー・ポイントです。
    /// </summary>
    class Program
    {
        /// <summary>
        /// Ｃ＃のプログラムは、
        /// この Main 関数から始まり、 Main 関数を抜けて終わります。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            KwErrorHandler errH = Util_OwataMinister.ENGINE_DEFAULT;

            // 将棋エンジン　きふわらべ
            KifuWarabeImpl kifuWarabe = new KifuWarabeImpl();
            kifuWarabe.AtBegin(errH);
            bool isTimeoutShutdown_temp;
            kifuWarabe.AtBody(out isTimeoutShutdown_temp, errH);    // 将棋サーバーからのメッセージの受信や、
                                    // 思考は、ここで行っています。
            kifuWarabe.AtEnd();
        }
    }
}
