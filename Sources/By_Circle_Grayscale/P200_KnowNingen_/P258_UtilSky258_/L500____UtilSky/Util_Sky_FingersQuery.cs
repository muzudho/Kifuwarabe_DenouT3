using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P234_Komahaiyaku.L500____Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P256_SeizaFinger.L250____Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P258_UtilSky258_.L500____UtilSky
{

    /// <summary>
    /// 指定局面から、『指差し番号』を問い合わせます。
    /// 
    /// Okiba,Playerside,Komasyurui,Suji を引数に使うシンプルなもの。
    /// </summary>
    public abstract class Util_Sky_FingersQuery
    {
        /// <summary>
        /// **********************************************************************************************************************
        /// 駒のハンドルを返します。
        /// **********************************************************************************************************************
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="kifuD"></param>
        /// <returns></returns>
        public static Fingers InOkibaPsideNow(SkyConst src_Sky, Okiba okiba, Playerside pside)
        {
            Fingers fingers = new Fingers();

            src_Sky.Foreach_Starlights((Finger finger, Starlight dd, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(dd.Now);

                if (Conv_SyElement.ToOkiba(koma.Masu) == okiba
                    && pside == koma.Pside
                    )
                {
                    fingers.Add(finger);
                }
            });

            return fingers;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 駒のハンドル(*1)を返します。
        /// ************************************************************************************************************************
        /// 
        ///         *1…将棋の駒１つ１つに付けられた番号です。
        /// 
        /// </summary>
        /// <param name="syurui"></param>
        /// <param name="hKomas"></param>
        /// <returns></returns>
        public static Fingers InKomasyuruiNow(SkyConst src_Sky, Komasyurui14 syurui, KwErrorHandler errH)
        {
            Fingers figKomas = new Fingers();

            foreach (Finger figKoma in Finger_Honshogi.Items_KomaOnly)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);


                if (Util_Komasyurui14.Matches(syurui, Util_Komahaiyaku184.Syurui(koma.Haiyaku)))
                {
                    figKomas.Add(figKoma);
                }
            }

            return figKomas;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 駒のハンドルを返します。　：　置き場、種類
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="syurui"></param>
        /// <param name="kifu"></param>
        /// <returns></returns>
        public static Fingers InOkibaKomasyuruiNow(SkyConst src_Sky, Okiba okiba, Komasyurui14 syurui)
        {
            Fingers komas = new Fingers();

            foreach (Finger figKoma in Finger_Honshogi.Items_KomaOnly)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);


                if (
                    okiba == Conv_SyElement.ToOkiba(koma.Masu)
                    && Util_Komasyurui14.Matches(syurui, koma.Komasyurui)// Util_Komahaiyaku184.Syurui(koma.Haiyaku)
                    )
                {
                    komas.Add(figKoma);
                }
            }

            return komas;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 局面上のオブジェクトを返します。置き場、先後サイド、駒の種類で絞りこみます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="src_Sky">局面データ。</param>
        /// <param name="okiba">置き場。</param>
        /// <param name="pside">先後サイド。</param>
        /// <param name="komaSyurui">駒の種類。</param>
        /// <returns></returns>
        public static Fingers InOkibaPsideKomasyuruiNow(SkyConst src_Sky, Okiba okiba, Playerside pside, Komasyurui14 komaSyurui)
        {
            Fingers fingers = new Fingers();

            src_Sky.Foreach_Starlights((Finger finger, Starlight light, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(light.Now);

                if (
                    okiba == Conv_SyElement.ToOkiba(koma.Masu)
                    && pside == koma.Pside
                    && komaSyurui == koma.Komasyurui
                    )
                {
                    fingers.Add(finger);
                }
            });

            return fingers;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 指定のマスにあるスプライトを返します。（本将棋用）
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="masu">マス番号</param>
        /// <param name="logTag">ログ名</param>
        /// <returns>スプライト番号。なければエラー番号。</returns>
        public static Fingers InMasuNow(SkyConst src_Sky, SyElement masu)
        {
            Fingers found = new Fingers();

            foreach (Finger finger in Finger_Honshogi.Items_KomaOnly)
            {
                RO_Star koma = Util_Koma.FromFinger(src_Sky, finger);

                if (Masu_Honshogi.Basho_Equals(koma.Masu,masu))
                {
                    found.Add(finger);
                }
            }

            return found;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 指定の筋にあるスプライトを返します。（本将棋用）二歩チェックに利用。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="src_Sky"></param>
        /// <param name="okiba">置き場</param>
        /// <param name="pside">先後</param>
        /// <param name="pside">駒種類</param>
        /// <param name="suji">筋番号1～9</param>
        /// <returns></returns>
        public static Fingers InOkibaPsideKomasyuruiSujiNow(SkyConst src_Sky, Okiba okiba, Playerside pside, Komasyurui14 ks, int suji)
        {
            Fingers found = new Fingers();

            foreach (Finger finger in Finger_Honshogi.Items_KomaOnly)
            {
                RO_Star koma2 = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);

                int suji2;
                Util_MasuNum.TryMasuToSuji(koma2.Masu, out suji2);

                if (
                    Conv_SyElement.ToOkiba(koma2.Masu)==okiba
                    && koma2.Pside == pside
                    && koma2.Komasyurui == ks
                    && suji2 == suji
                    )
                {
                    found.Add(finger);
                }
            }

            return found;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 指定した置き場にある駒のハンドルを返します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="okiba"></param>
        /// <returns></returns>
        public static Fingers InOkibaNow(SkyConst src_Sky, Okiba okiba, KwErrorHandler errH)
        {
            Fingers komas = new Fingers();

            foreach (Finger figKoma in Finger_Honshogi.Items_KomaOnly)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);


                if (okiba == Conv_SyElement.ToOkiba(koma.Masu))
                {
                    komas.Add(figKoma);
                }
            }

            return komas;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 駒のハンドルを返します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="pside"></param>
        /// <param name="hKomas"></param>
        /// <returns></returns>
        public static Fingers InPsideNow(SkyConst src_Sky, Playerside pside, KwErrorHandler errH)
        {
            Fingers fingers = new Fingers();

            src_Sky.Foreach_Starlights((Finger finger, Starlight ds, ref bool toBreak) =>
            {

                RO_Star koma = Util_Starlightable.AsKoma(ds.Now);

                if (pside == koma.Pside)
                {
                    fingers.Add(finger);
                }

            });

            return fingers;
        }
    }
}
