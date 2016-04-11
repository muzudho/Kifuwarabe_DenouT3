using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P571_KifuWarabe_.L500____KifuWarabe;

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
