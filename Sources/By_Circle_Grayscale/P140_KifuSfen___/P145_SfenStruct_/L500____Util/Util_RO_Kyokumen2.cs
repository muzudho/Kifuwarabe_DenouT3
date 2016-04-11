using Grayscale.P145_SfenStruct_.L___250_Struct;
using System.Diagnostics;
using System.Text;

namespace Grayscale.P145_SfenStruct_.L500____Util
{

    /// <summary>
    /// ************************************************************************************************************************
    /// SFEN形式の初期配置の書き方の、データの持ち方です。
    /// ************************************************************************************************************************
    /// </summary>
    public class Util_RO_Kyokumen2
    {



        public  static void Assert_Koma40(RO_Kyokumen2_ForTokenize result, string hint)
        {
//#if DEBUG
            StringBuilder sb = new StringBuilder();
            int komaCount = 0;
            result.Foreach_Masu201((int masuHandle, string masuString, ref bool toBreak) =>
            {
                sb.Append("[" + masuString + "]");
                if (masuString != "")
                {
                    komaCount++;
                }
            });

            Debug.Assert(komaCount == 40, "将棋の駒の数が40個ではありませんでした。[" + komaCount + "] " + sb.ToString() + "\n hint="+hint);
//#endif
        }



    }
}
