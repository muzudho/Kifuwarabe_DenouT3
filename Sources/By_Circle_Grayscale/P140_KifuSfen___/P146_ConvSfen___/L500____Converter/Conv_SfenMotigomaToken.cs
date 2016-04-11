
namespace Grayscale.P146_ConvSfen___.L500____Converter
{
    /// <summary>
    /// SFENの持ち駒文字列トークン。
    /// </summary>
    public abstract class Conv_SfenMotigomaToken
    {
        /// <summary>
        /// 持ち駒の枚数を数えます。
        /// </summary>
        /// <param name="moti"></param>
        /// <returns></returns>
        public  static int CountMaisu(string moti)
        {
            int result;

            if (moti.Length == 1)
            {
                // 「K」などを想定。
                result = 1;
            }
            else if (moti != "")
            {
                result = int.TryParse(moti.Substring(0, moti.Length - 1), out result) ? result : 0;
            }
            else
            {
                result = 0;
            }

            return result;
        }

    }
}
