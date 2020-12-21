using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// フィンガーを１つ求めるユーティリティーです。
    /// </summary>
    public abstract class Util_Sky_FingerQuery
    {
        /// <summary>
        /// 指定の場所にある駒を返します。
        /// 
        ///         先後は見ますが、将棋盤限定です。
        /// 
        /// </summary>
        /// <param name="okiba">置き場</param>
        /// <param name="masu">筋、段</param>
        /// <param name="uc_Main">メインパネル</param>
        /// <returns>駒。無ければヌル。</returns>
        public static Finger InMasuNow(SkyConst src_Sky, Playerside pside, SyElement masu)
        {
            Finger foundKoma = Fingers.Error_1;

            foreach (Finger finger in Finger_Honshogi.Items_KomaOnly)
            {

                IMoveHalf sl = src_Sky.StarlightIndexOf(finger);

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
        /// 駒の種類（不成として扱います）を指定して、駒を検索します。
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="komasyurui"></param>
        /// <param name="uc_Main"></param>
        /// <returns>無ければ -1</returns>
        public static Finger InOkibaSyuruiNowIgnoreCase(SkyConst srcSky, Okiba okiba, Komasyurui14 komasyurui)
        {
            Finger found = Fingers.Error_1;

            Komasyurui14 syuruiNarazuCase = Util_Komasyurui14.NarazuCaseHandle(komasyurui);

            foreach (Finger finger in Finger_Honshogi.Items_KomaOnly)
            {
                RO_Star koma = Util_Starlightable.AsKoma(srcSky.StarlightIndexOf(finger).Now);

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
        /// 指定の場所にある駒を返します。
        /// 
        ///         先後は見ますが、将棋盤限定です。
        /// 
        /// </summary>
        /// <param name="okiba">置き場</param>
        /// <param name="masu">筋、段</param>
        /// <param name="uc_Main">メインパネル</param>
        /// <returns>駒。無ければヌル。</returns>
        public static Finger InShogibanMasuNow(SkyConst src_Sky, Playerside pside, SyElement masu)
        {
            Finger foundKoma = Fingers.Error_1;

            foreach (Finger finger in Finger_Honshogi.Items_KomaOnly)
            {

                IMoveHalf sl = src_Sky.StarlightIndexOf(finger);

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
