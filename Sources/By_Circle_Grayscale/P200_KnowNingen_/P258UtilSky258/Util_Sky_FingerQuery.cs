using Grayscale.P003Log.I500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P214Masu.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P256SeizaFinger.L250Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P258UtilSky258.L500UtilSky
{

    /// <summary>
    /// フィンガーを１つ求めるユーティリティーです。
    /// </summary>
    public abstract class Util_Sky_FingerQuery
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 指定の場所にある駒を返します。
        /// ************************************************************************************************************************
        /// 
        ///         先後は見ますが、将棋盤限定です。
        /// 
        /// </summary>
        /// <param name="okiba">置き場</param>
        /// <param name="masu">筋、段</param>
        /// <param name="uc_Main">メインパネル</param>
        /// <returns>駒。無ければヌル。</returns>
        public static Finger InMasuNow(SkyConst src_Sky, Playerside pside, SyElement masu, KwErrorHandler errH)
        {
            Finger foundKoma = Fingers.Error_1;

            foreach (Finger finger in Finger_Honshogi.Items_KomaOnly)
            {

                Starlight sl = src_Sky.StarlightIndexOf(finger);

                RO_Star koma = Util_Starlightable.AsKoma(sl.Now);

                int suji1;
                int suji2;
                int dan1;
                int dan2;
                Util_MasuNum.TryMasuToSuji(koma.Masu, out suji1);
                Util_MasuNum.TryMasuToSuji(masu, out suji2);
                Util_MasuNum.TryMasuToDan(koma.Masu, out dan1);
                Util_MasuNum.TryMasuToDan(masu, out dan2);

                if (
                    koma.Pside == pside
                    && suji1 == suji2
                    && dan1 == dan2
                    )
                {
                    foundKoma = finger;
                    break;
                }

            }

            return foundKoma;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 駒の種類（不成として扱います）を指定して、駒を検索します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="komasyurui"></param>
        /// <param name="uc_Main"></param>
        /// <returns>無ければ -1</returns>
        public static Finger InOkibaSyuruiNow_IgnoreCase(SkyConst src_Sky, Okiba okiba, Komasyurui14 komasyurui, KwErrorHandler errH)
        {
            Finger found = Fingers.Error_1;

            Komasyurui14 syuruiNarazuCase = Util_Komasyurui14.NarazuCaseHandle(komasyurui);

            foreach (Finger finger in Finger_Honshogi.Items_KomaOnly)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);

                if (Conv_SyElement.ToOkiba(koma.Masu) == okiba
                    && Util_Komasyurui14.Matches(koma.ToNarazuCase(), syuruiNarazuCase))
                {
                    found = finger;
                    break;
                }
            }

            return found;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 指定の場所にある駒を返します。
        /// ************************************************************************************************************************
        /// 
        ///         先後は見ますが、将棋盤限定です。
        /// 
        /// </summary>
        /// <param name="okiba">置き場</param>
        /// <param name="masu">筋、段</param>
        /// <param name="uc_Main">メインパネル</param>
        /// <returns>駒。無ければヌル。</returns>
        public static Finger InShogibanMasuNow(SkyConst src_Sky, Playerside pside, SyElement masu, KwErrorHandler errH)
        {
            Finger foundKoma = Fingers.Error_1;

            foreach (Finger finger in Finger_Honshogi.Items_KomaOnly)
            {

                Starlight sl = src_Sky.StarlightIndexOf(finger);

                RO_Star koma = Util_Starlightable.AsKoma(sl.Now);

                int suji1;
                int suji2;
                int dan1;
                int dan2;
                Util_MasuNum.TryMasuToSuji(koma.Masu, out suji1);
                Util_MasuNum.TryMasuToSuji(masu, out suji2);
                Util_MasuNum.TryMasuToDan(koma.Masu, out dan1);
                Util_MasuNum.TryMasuToDan(masu, out dan2);

                // 先後は見ますが、将棋盤限定です。
                if (
                    koma.Pside == pside
                    && Conv_SyElement.ToOkiba(koma.Masu) == Okiba.ShogiBan
                    && suji1 == suji2
                    && dan1 == dan2
                    )
                {
                    foundKoma = finger;
                    break;
                }

            }

            return foundKoma;
        }


    }
}
