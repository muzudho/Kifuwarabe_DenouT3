﻿using System.Diagnostics;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Util_Sky_BoolQuery
    {

        /// <summary>
        /// 含まれるか判定。
        /// </summary>
        /// <param name="masus"></param>
        /// <returns></returns>
        public static bool ExistsIn(IMoveHalf sl, SySet<SyElement> masus, SkyConst srcSky)
        {
            bool matched = false;

            foreach (SyElement masu in masus.Elements)
            {
                RO_Star koma = Util_Starlightable.AsKoma(sl.Now);


                Finger finger = Util_Sky_FingerQuery.InShogibanMasuNow(srcSky, koma.Pside, masu);

                if (
                    finger != Fingers.Error_1  //2014-07-21 先後も見るように追記。
                    && koma.Masu == masu
                    )
                {
                    matched = true;
                    break;
                }

            }

            return matched;
        }

        /// <summary>
        /// 相手陣に入っていれば真。
        /// 
        ///         後手は 7,8,9 段。
        ///         先手は 1,2,3 段。
        /// </summary>
        /// <returns></returns>
        public static bool InAitejin(IMoveHalf ms)
        {
            bool result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            int dan;
            Util_MasuNum.TryMasuToDan(koma.Masu, out dan);

            result = (Util_Sky_BoolQuery.IsGote(ms) && 7 <= dan) || (Util_Sky_BoolQuery.IsSente(ms) && dan <= 3);

            return result;
        }

        /// <summary>
        /// 成り
        /// </summary>
        public static bool IsNari(IMoveHalf ms)
        {
            bool result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            result = Util_Komasyurui14.FlagNari[(int)Util_Komahaiyaku184.Syurui(koma.Haiyaku)];

            return result;
        }

        /// <summary>
        /// 不成
        /// </summary>
        public static bool IsFunari(RO_Starlight ms)
        {
            bool result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            result = !Util_Komasyurui14.FlagNari[(int)Util_Komahaiyaku184.Syurui(koma.Haiyaku)];

            return result;
        }

        public static bool IsNareruKoma(IMoveHalf ms)
        {
            bool result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            result = Util_Komasyurui14.FlagNareruKoma[(int)Util_Komahaiyaku184.Syurui(koma.Haiyaku)];


            return result;
        }

        /// <summary>
        /// 不一致判定：　先後、駒種類  が、自分と同じものが　＜ひとつもない＞
        /// </summary>
        /// <returns></returns>
        public static bool NeverOnaji(IMoveHalf ms, SkyConst src_Sky, params Fingers[] komaGroupArgs)
        {
            bool unmatched = true;

            foreach (Fingers komaGroup in komaGroupArgs)
            {
                foreach (Finger figKoma in komaGroup.Items)
                {
                    RO_Star koma1 = Util_Starlightable.AsKoma(ms.Now);
                    RO_Star koma2 = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);



                    if (
                            koma1.Pside == koma2.Pside // 誰のものか
                        && Util_Komahaiyaku184.Syurui(koma1.Haiyaku) == Util_Komahaiyaku184.Syurui(koma2.Haiyaku) // 駒の種類は
                        )
                    {
                        // １つでも一致するものがあれば、終了します。
                        unmatched = false;
                        goto gt_EndLoop;
                    }
                }

            }
        gt_EndLoop:

            return unmatched;
        }

        /// <summary>
        /// 駒台の上にあれば真。
        /// </summary>
        /// <returns></returns>
        public static bool OnKomadai(RO_Starlight ms)
        {
            bool result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            result = (Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(koma.Masu));

            return result;
        }

        /// <summary>
        /// 先後一致判定。
        /// </summary>
        /// <param name="ms2"></param>
        /// <returns></returns>
        public static bool MatchPside(RO_Starlight ms1, RO_Starlight ms2)
        {
            bool result;

            RO_Star koma1 = Util_Starlightable.AsKoma(ms1.Now);
            RO_Star koma2 = Util_Starlightable.AsKoma(ms2.Now);


            result = koma1.Pside == koma2.Pside;

            return result;
        }

        /// <summary>
        /// 先手
        /// </summary>
        /// <returns></returns>
        public static bool IsSente(IMoveHalf ms)
        {
            bool result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            result = Playerside.P1 == koma.Pside;

            return result;
        }

        /// <summary>
        /// 後手
        /// </summary>
        /// <returns></returns>
        public static bool IsGote(IMoveHalf ms)
        {
            bool result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            result = Playerside.P2 == koma.Pside;

            return result;
        }

        /// <summary>
        /// “打” ＜アクション時＞
        /// </summary>
        /// <returns></returns>
        public static bool IsDaAction(IMove move)
        {
            Debug.Assert(null != move, "指し手がヌルでした。");
            bool result;

            RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);
            result = Okiba.ShogiBan != Conv_SyElement.ToOkiba(srcKoma.Masu)//駒台（駒袋）から打ったとき。
                && Okiba.Empty != Conv_SyElement.ToOkiba(srcKoma.Masu);//初期配置から移動しても、打にはしません。

            return result;
        }

        public static bool isEnableSfen(IMove move)
        {
            bool enable = true;

            RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);
            RO_Star dstKoma = Util_Starlightable.AsKoma(move.Now);


            int srcDan;
            if (!Util_MasuNum.TryMasuToDan(srcKoma.Masu, out srcDan))
            {
                enable = false;
            }

            int dan;
            if (!Util_MasuNum.TryMasuToDan(dstKoma.Masu, out dan))
            {
                enable = false;
            }

            return enable;
        }

        /// <summary>
        /// 成った
        /// </summary>
        /// <returns></returns>
        public static bool IsNattaMove(IMove move)
        {
            // 元種類が不成、現種類が成　の場合のみ真。
            bool natta = true;


            RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);
            RO_Star dstKoma = Util_Starlightable.AsKoma(move.Now);


            // 成立しない条件を１つでも満たしていれば、偽　確定。
            if (
                Komahaiyaku185.n000_未設定 == srcKoma.Haiyaku
                //Ks14.H00_Null == Haiyaku184Array.Syurui[(int)this.SrcHaiyaku]
                ||
                Komahaiyaku185.n000_未設定 == dstKoma.Haiyaku
                //Ks14.H15_ErrorKoma == Haiyaku184Array.Syurui[(int)this.Haiyaku]
                ||
                Util_Komasyurui14.FlagNari[(int)Util_Komahaiyaku184.Syurui(srcKoma.Haiyaku)]
                ||
                !Util_Komasyurui14.FlagNari[(int)Util_Komahaiyaku184.Syurui(dstKoma.Haiyaku)]
                )
            {
                natta = false;
            }

            return natta;
        }

        /// <summary>
        /// 移動前と、移動後の場所が異なっていれば真。
        /// </summary>
        /// <returns></returns>
        public static bool DoneMove(RO_Starbeam ss)
        {
            bool result;

            RO_Star koma1 = Util_Starlightable.AsKoma(ss.Now);
            RO_Star koma2 = Util_Starlightable.AsKoma(Util_Sky258A.Src(ss).Now);

            result = Conv_SyElement.ToMasuNumber(koma1.Masu) != Conv_SyElement.ToMasuNumber(koma2.Masu);

            return result;
        }

    }
}
