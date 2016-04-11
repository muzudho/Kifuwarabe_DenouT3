using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;



namespace Grayscale.P214_Masu_______.L500____Util
{


    /// <summary>
    /// 足し算、引き算をしたいときなどに使います。
    /// </summary>
    public abstract class Util_MasuNum
    {

        #region 整数変換(基礎)

        /// <summary>
        /// 将棋盤、駒台に筋があります。番号は1～。
        /// </summary>
        /// <param name="masu"></param>
        /// <returns>該当なければ-1</returns>
        public static bool TryMasuToSuji(SyElement masu, out int result)
        {
            int masuNumber = Conv_SyElement.ToMasuNumber(masu);
            return Util_MasuNum.TryMasuToSuji(masuNumber, out result);
        }

        /// <summary>
        /// 将棋盤、駒台に筋があります。番号は1～。
        /// </summary>
        /// <param name="masu"></param>
        /// <returns>該当なければ-1</returns>
        public static bool TryMasuToSuji(int masuNumber, out int result)
        {
            bool successful = true;


            Okiba okiba = Conv_SyElement.ToOkiba(masuNumber);

            switch (okiba)
            {
                case Okiba.ShogiBan:
                    result = (masuNumber - Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))) / 9 + 1;
                    break;

                case Okiba.Sente_Komadai:
                case Okiba.Gote_Komadai:
                    result = (masuNumber - Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))) / 10 + 1;
                    break;

                case Okiba.KomaBukuro:
                    result = (masuNumber - Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))) / 10 + 1;
                    break;

                default:
                    // エラー
                    result = -1;
                    successful = false;
                    goto gt_EndMethod;
            }

        gt_EndMethod:
            return successful;
        }

        /// <summary>
        /// 将棋盤、駒台に筋があります。
        /// </summary>
        /// <param name="masu"></param>
        /// <returns>該当なければ-1</returns>
        public static bool TryMasuToDan(SyElement masu, out int result)
        {
            int masuNumber = Conv_SyElement.ToMasuNumber(masu);
            return Util_MasuNum.TryMasuToDan(masuNumber, out result);
        }

        /// <summary>
        /// 将棋盤、駒台に筋があります。
        /// </summary>
        /// <param name="masu"></param>
        /// <returns>該当なければ-1</returns>
        public static bool TryMasuToDan(int masuNumber, out int result)
        {
            bool successful = true;

            Okiba okiba = Conv_SyElement.ToOkiba(masuNumber);

            switch (okiba)
            {
                case Okiba.ShogiBan:
                    result = (masuNumber - Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))) % 9 + 1;
                    break;

                case Okiba.Sente_Komadai:
                case Okiba.Gote_Komadai:
                    result = (masuNumber - Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))) % 10 + 1;
                    break;

                case Okiba.KomaBukuro:
                    result = (masuNumber - Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))) % 10 + 1;
                    break;

                default:
                    // エラー
                    result = -1;
                    successful = false;
                    goto gt_EndMethod;
            }

        gt_EndMethod:
            return successful;
        }



        #endregion

    }
}
