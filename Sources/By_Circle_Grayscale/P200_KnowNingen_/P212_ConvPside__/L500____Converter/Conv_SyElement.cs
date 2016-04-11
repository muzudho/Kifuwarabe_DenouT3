using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L___250_Masu;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;

namespace Grayscale.P212_ConvPside__.L500____Converter
{
    public abstract class Conv_SyElement
    {

        public static int ToMasuNumber(SyElement syElm)
        {
            int result;

            if (syElm is New_Basho)
            {
                result = ((New_Basho)syElm).MasuNumber;
            }
            else
            {
                result = Masu_Honshogi.nError;
            }

            return result;
        }

        public static Okiba ToOkiba(SyElement masu)
        {
            int masuNumber = Conv_SyElement.ToMasuNumber(masu);
            return Conv_SyElement.ToOkiba(masuNumber);
        }

        public static Okiba ToOkiba(int masuNumber)
        {
            Okiba result;

            if ((int)Masu_Honshogi.nban11_１一 <= masuNumber && masuNumber <= (int)Masu_Honshogi.nban99_９九)
            {
                // 将棋盤
                result = Okiba.ShogiBan;
            }
            else if ((int)Masu_Honshogi.nsen01 <= masuNumber && masuNumber <= (int)Masu_Honshogi.nsen40)
            {
                // 先手駒台
                result = Okiba.Sente_Komadai;
            }
            else if ((int)Masu_Honshogi.ngo01 <= masuNumber && masuNumber <= (int)Masu_Honshogi.ngo40)
            {
                // 後手駒台
                result = Okiba.Gote_Komadai;
            }
            else if ((int)Masu_Honshogi.nfukuro01 <= masuNumber && masuNumber <= (int)Masu_Honshogi.nfukuro40)
            {
                // 駒袋
                result = Okiba.KomaBukuro;
            }
            else
            {
                // 該当なし
                result = Okiba.Empty;
            }

            return result;
        }

        //public static Okiba GetOkiba(SyElement masu)
        //{
        //    Okiba okiba;

        //    int masuHandle = Conv_SyElement.AsMasuNumber(masu);

        //    if (0 <= masuHandle && masuHandle <= 80)
        //    {
        //        // 将棋盤
        //        okiba = Okiba.ShogiBan;
        //    }
        //    else if (81 <= masuHandle && masuHandle <= 120)
        //    {
        //        // 先手駒台
        //        okiba = Okiba.Sente_Komadai;
        //    }
        //    else if (121 <= masuHandle && masuHandle <= 160)
        //    {
        //        // 後手駒台
        //        okiba = Okiba.Gote_Komadai;
        //    }
        //    else if (161 <= masuHandle && masuHandle <= 200)
        //    {
        //        // 駒袋
        //        okiba = Okiba.KomaBukuro;
        //    }
        //    else
        //    {
        //        // エラー
        //        okiba = Okiba.Empty;
        //    }

        //    return okiba;
        //}


    }
}
