﻿using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Util_Sky_KomaQuery
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 指定のマスにある駒を返します。（本将棋用）
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="masu">マス番号</param>
        /// <returns>スプライト番号。なければエラー番号。</returns>
        public static RO_Star InMasuNow(SkyConst src_Sky, SyElement masu)
        {
            RO_Star koma = null;

            Finger fig = Util_Sky_FingersQuery.InMasuNow(src_Sky, masu).ToFirst();

            if (Fingers.Error_1 == fig)
            {
                // 指定の升には駒がない。
                goto gt_EndMethod;
            }

            koma = Util_Koma.FromFinger(src_Sky, fig);

        gt_EndMethod:
            return koma;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 指定のマスにある駒を返します。（本将棋用）
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="masu">マス番号</param>
        /// <returns>スプライト番号。なければエラー番号。</returns>
        public static RO_Star InMasuPsideNow(SkyConst src_Sky, SyElement masu, Playerside pside)
        {
            RO_Star koma = null;

            Finger fig = Util_Sky_FingersQuery.InMasuNow(src_Sky, masu).ToFirst();

            if (Fingers.Error_1 == fig)
            {
                // 指定の升には駒がない。
                goto gt_EndMethod;
            }

            koma = Util_Koma.FromFinger(src_Sky, fig);
            if (koma.Pside != pside)
            {
                // サイドが異なる
                koma = null;
                goto gt_EndMethod;
            }

        gt_EndMethod:
            return koma;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 指定のマスにある駒を返します。（本将棋用）
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="masu">マス番号</param>
        /// <returns>スプライト番号。なければエラー番号。</returns>
        public static RO_Star InMasuPsideKomasyuruiNow(SkyConst src_Sky, SyElement masu, Playerside pside, PieceType syurui)
        {
            RO_Star koma = null;

            Finger fig = Util_Sky_FingersQuery.InMasuNow(src_Sky, masu).ToFirst();

            if (Fingers.Error_1 == fig)
            {
                // 指定の升には駒がない。
                goto gt_EndMethod;
            }

            koma = Util_Koma.FromFinger(src_Sky, fig);
            if (koma.Pside != pside || koma.Komasyurui != syurui)
            {
                // サイド または駒の種類が異なる
                koma = null;
                goto gt_EndMethod;
            }

        gt_EndMethod:
            return koma;
        }

    }
}
