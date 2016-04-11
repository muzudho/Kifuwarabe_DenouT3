using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using System;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号


namespace Grayscale.P258_UtilSky258_.L500____UtilSky
{

    /// <summary>
    /// 指定局面から、『指差し番号』を問い合わせます。
    /// 
    /// 特殊なもの。
    /// </summary>
    public abstract class Util_Sky_FingersQueryFx
    {

        /// <summary>
        /// ４分割します。
        /// </summary>
        /// <param name="fs_banjoSeme">fingers</param>
        /// <param name="fs_banjoKurau"></param>
        /// <param name="fs_motiSeme"></param>
        /// <param name="fs_motiKurau"></param>
        /// <param name="src_Sky"></param>
        /// <param name="tebanSeme"></param>
        /// <param name="tebanKurau"></param>
        /// <param name="errH_OrNull"></param>
        public static void Split_BanjoSeme_BanjoKurau_MotiSeme_MotiKurau(
            out Fingers fs_banjoSeme,//戦駒（利きを調べる側）
            out Fingers fs_banjoKurau,//戦駒（喰らう側）
            out Fingers fs_motiSeme,// 持駒（利きを調べる側）
            out Fingers fs_motiKurau,// 持駒（喰らう側）
            SkyConst src_Sky,
            Playerside tebanSeme,
            Playerside tebanKurau,
            KwErrorHandler errH_OrNull
        )
        {
            Fingers fs_banjoSeme_temp = new Fingers();// （１）盤上駒_攻め手
            Fingers fs_banjoKurau_temp = new Fingers();// （２）盤上駒_食らう側
            Fingers fs_motiSeme_temp = new Fingers();// （３）持ち駒_攻め手
            Fingers fs_motiKurau_temp = new Fingers();// （４）持ち駒_食らう側

            src_Sky.Foreach_Starlights((Finger finger, Starlight light, ref bool toBreak) =>
            {
                RO_Star star = Util_Starlightable.AsKoma(light.Now);//駒

                if (Conv_SyElement.ToOkiba(star.Masu) == Okiba.ShogiBan)
                {
                    //
                    // 盤上
                    //
                    if (tebanSeme == star.Pside)
                    {
                        fs_banjoSeme_temp.Add(finger);// （１）盤上駒_攻め手
                    }
                    else if (tebanKurau == star.Pside)
                    {
                        fs_banjoKurau_temp.Add(finger);// （２）盤上駒_食らう側
                    }
                }
                else if (Conv_SyElement.ToOkiba(star.Masu) == Okiba.Sente_Komadai)
                {
                    //
                    // P1駒台
                    //
                    if (tebanSeme == Playerside.P1)
                    {
                        // 攻手がP1のとき、P1駒台は。
                        fs_motiSeme_temp.Add(finger);// （３）持ち駒_攻め手
                    }
                    else if (tebanSeme == Playerside.P2)
                    {
                        // 攻手がP2のとき、P1駒台は。
                        fs_motiKurau_temp.Add(finger);// （４）持ち駒_食らう側
                    }
                    else
                    {
                        throw new Exception("駒台の持ち駒を調べようとしましたが、先手でも、後手でもない指定でした。");
                    }
                }
                else if (Conv_SyElement.ToOkiba(star.Masu) == Okiba.Gote_Komadai)
                {
                    //
                    // P2駒台
                    //
                    if (tebanSeme == Playerside.P1)
                    {
                        // 攻手がP1のとき、P2駒台は。
                        fs_motiKurau_temp.Add(finger);// （３）持ち駒_攻め手
                    }
                    else if (tebanSeme == Playerside.P2)
                    {
                        // 攻手がP2のとき、P2駒台は。
                        fs_motiSeme_temp.Add(finger);// （４）持ち駒_食らう側
                    }
                    else
                    {
                        throw new Exception("駒台の持ち駒を調べようとしましたが、先手でも、後手でもない指定でした。");
                    }
                }
                else
                {
                    throw new Exception("駒台の持ち駒を調べようとしましたが、盤上でも、駒台でもない指定でした。");
                }
            });
            fs_banjoSeme = fs_banjoSeme_temp;// （１）盤上駒_攻め手
            fs_banjoKurau = fs_banjoKurau_temp;// （２）盤上駒_食らう側
            fs_motiSeme = fs_motiSeme_temp;// （３）持ち駒_攻め手
            fs_motiKurau = fs_motiKurau_temp;// （４）持ち駒_食らう側
        }


        /// <summary>
        /// 持駒を取得。
        /// </summary>
        /// <param name="fingers_banjoSeme"></param>
        /// <param name="fingers_banjoKurau"></param>
        /// <param name="fingers_motiSeme"></param>
        /// <param name="fingers_motiKurau"></param>
        /// <param name="src_Sky"></param>
        /// <param name="tebanSeme"></param>
        /// <param name="tebanKurau"></param>
        /// <param name="errH_OrNull"></param>
        public static void Split_Moti1p_Moti2p(
            out Fingers fingers_moti1p,// 持駒 1P
            out Fingers fingers_moti2p,// 持駒 2=
            SkyConst src_Sky,
            KwErrorHandler errH_OrNull
        )
        {
            Fingers fingers_moti1p_temp = new Fingers();// （３）持ち駒_攻め手
            Fingers fingers_moti2p_temp = new Fingers();// （４）持ち駒_食らう側

            src_Sky.Foreach_Starlights((Finger finger, Starlight dd, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(dd.Now);

                if (Conv_SyElement.ToOkiba(koma.Masu) == Okiba.Sente_Komadai)
                {
                    //
                    // 1P 駒台
                    //
                    fingers_moti1p_temp.Add(finger);
                }
                else if (Conv_SyElement.ToOkiba(koma.Masu) == Okiba.Gote_Komadai)
                {
                    //
                    // 2P 駒台
                    //
                    fingers_moti2p_temp.Add(finger);
                }
            });
            fingers_moti1p = fingers_moti1p_temp;
            fingers_moti2p = fingers_moti2p_temp;
        }


        public static void Split_Jigyoku_Aitegyoku(
            out RO_Star koma_Jigyoku_orNull,
            out RO_Star koma_Aitegyoku_orNull,
            SkyConst src_Sky,
            Playerside jiPside,
            Playerside aitePside
            )
        {
            RO_Star koma_Jigyoku_temp = null;
            RO_Star koma_Aitegyoku_temp = null;

            src_Sky.Foreach_Starlights((Finger finger, Starlight ds, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(ds.Now);

                if (
                    Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu)
                    && jiPside == koma.Pside
                    && Komasyurui14.H06_Gyoku__ == koma.Komasyurui
                    )
                {
                    //
                    // 自玉の位置
                    //
                    koma_Jigyoku_temp = koma;
                }
                else if (
                    Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu)
                    && aitePside == koma.Pside
                    && Komasyurui14.H06_Gyoku__ == koma.Komasyurui
                    )
                {
                    //
                    // 相手玉の位置
                    //
                    koma_Aitegyoku_temp = koma;
                }
            });
            koma_Jigyoku_orNull = koma_Jigyoku_temp;
            koma_Aitegyoku_orNull = koma_Aitegyoku_temp;
        }

        /// <summary>
        /// 1P玉と、2P玉を取得します。
        /// </summary>
        /// <param name="koma_1PGyoku_orNull"></param>
        /// <param name="koma_2PGyoku_orNull"></param>
        /// <param name="src_Sky"></param>
        public static void Split_1PGyoku_2PGyoku(
            out RO_Star koma_1PGyoku_orNull,
            out RO_Star koma_2PGyoku_orNull,
            SkyConst src_Sky
        )
        {
            RO_Star koma_1PGyoku_temp = null;
            RO_Star koma_2PGyoku_temp = null;

            src_Sky.Foreach_Starlights((Finger finger, Starlight ds, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(ds.Now);

                if(
                    Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu)
                    && Komasyurui14.H06_Gyoku__ == koma.Komasyurui
                    )
                {
                    if (Playerside.P1 == koma.Pside)
                    {
                        koma_1PGyoku_temp = koma;// 1P玉の位置
                    }
                    else if (Playerside.P2 == koma.Pside)
                    {
                        koma_2PGyoku_temp = koma;// 2P玉の位置
                    }
                }
            });
            koma_1PGyoku_orNull = koma_1PGyoku_temp;
            koma_2PGyoku_orNull = koma_2PGyoku_temp;
        }

    }
}
