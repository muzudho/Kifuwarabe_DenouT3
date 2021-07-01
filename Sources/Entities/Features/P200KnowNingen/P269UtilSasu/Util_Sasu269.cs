using System;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Util_Sasu269
    {

        ///// <summary>
        ///// ************************************************************************************************************************
        ///// 先後の交代
        ///// ************************************************************************************************************************
        ///// </summary>
        ///// <param name="pside">先後</param>
        ///// <returns>ひっくりかえった先後</returns>
        //public static Playerside AlternatePside(Playerside pside)
        //{
        //    Playerside result;

        //    switch (pside)
        //    {
        //        case Playerside.P1:
        //            result = Playerside.P2;
        //            break;

        //        case Playerside.P2:
        //            result = Playerside.P1;
        //            break;

        //        default:
        //            result = pside;
        //            break;
        //    }

        //    return result;
        //}




        ///// <summary>
        ///// 変換『「指し手→局面」のコレクション』→『「駒、指し手」のペアのリスト』
        ///// </summary>
        //public static List<Couple<Finger,Masu>> MovebetuSky_ToKamList(
        //    SkyConst src_Sky_genzai,
        //    Dictionary<ShootingStarlightable, SkyBuffer> ss
        //    )
        //{
        //    List<Couple<Finger, Masu>> kmList = new List<Couple<Finger, Masu>>();

        //    // TODO:
        //    foreach(KeyValuePair<ShootingStarlightable,SkyBuffer> entry in ss)
        //    {
        //        RO_Star_Koma srcKoma = Util_Starlightable.AsKoma(entry.Key.LongTimeAgo);
        //        RO_Star_Koma dstKoma = Util_Starlightable.AsKoma(entry.Key.Now);


        //            Masu srcMasu = srcKoma.Masu;
        //            Masu dstMasu = dstKoma.Masu;

        //            Finger figKoma = Util_Sky.Fingers_AtMasuNow(src_Sky_genzai,srcMasu).ToFirst();

        //            kmList.Add(new Couple<Finger, Masu>(figKoma, dstMasu));
        //    }

        //    return kmList;
        //}






        /// <summary>
        /// 「成り」ができる動きなら真。
        /// </summary>
        /// <returns></returns>
        public static bool IsPromotionable(
            out bool isPromotionable,
            RO_Star srcKoma,
            RO_Star dstKoma
            )
        {
            bool successful = true;
            isPromotionable = false;

            if (Okiba.ShogiBan != Conv_SyElement.ToOkiba(srcKoma.Masu))
            {
                successful = false;
                goto gt_EndMethod;
            }

            if (Util_Komasyurui14.IsNari(srcKoma.Komasyurui))
            {
                // 既に成っている駒は、「成り」の指し手を追加すると重複エラーになります。
                // 成りになれない、で正常終了します。
                goto gt_EndMethod;
            }

            int srcDan;
            if (!Util_MasuNum.TryMasuToDan(srcKoma.Masu, out srcDan))
            {
                throw new Exception("段に変換失敗");
            }

            int dstDan;
            if (!Util_MasuNum.TryMasuToDan(dstKoma.Masu, out dstDan))
            {
                throw new Exception("段に変換失敗");
            }

            // 先手か、後手かで大きく処理を分けます。
            switch (dstKoma.Pside)
            {
                case Playerside.P1:
                    {
                        if (srcDan <= 3)
                        {
                            // 3段目から上にあった駒が移動したのなら、成りの資格ありです。
                            isPromotionable = true;
                            goto gt_EndMethod;
                        }

                        if (dstDan <= 3)
                        {
                            // 3段目から上に駒が移動したのなら、成りの資格ありです。
                            isPromotionable = true;
                            goto gt_EndMethod;
                        }
                    }
                    break;
                case Playerside.P2:
                    {
                        if (7 <= srcDan)
                        {
                            // 7段目から下にあった駒が移動したのなら、成りの資格ありです。
                            isPromotionable = true;
                            goto gt_EndMethod;
                        }

                        if (7 <= dstDan)
                        {
                            // 7段目から下に駒が移動したのなら、成りの資格ありです。
                            isPromotionable = true;
                            goto gt_EndMethod;
                        }
                    }
                    break;
                default: throw new Exception("未定義のプレイヤーサイドです。");
            }
        gt_EndMethod:
            ;
            return successful;
        }
        /// <summary>
        /// これが通称【水際のいんちきプログラム】なんだぜ☆
        /// 必要により、【成り】の指し手を追加します。
        /// </summary>
        public static void Add_KomaBETUAllNariMoves(
            Maps_OneAndMulti<Finger, IMove> komaBETUAllMoves,
            Finger figKoma,
            RO_Star srcKoma,
            RO_Star dstKoma
            )
        {
            bool isPromotionable;
            if (!Util_Sasu269.IsPromotionable(out isPromotionable, srcKoma, dstKoma))
            {
                goto gt_EndMethod;
            }

            // 成りの資格があれば、成りの指し手を作ります。
            if (isPromotionable)
            {
                //MessageBox.Show($"成りの資格がある駒がありました。 src=[{srcKoma.Masu.Word}][{srcKoma.Syurui}]");

                IMove move = new RO_Starbeam(
                    //figKoma,//駒
                    srcKoma,// 移動元
                    new RO_Star(
                        dstKoma.Pside,
                        dstKoma.Masu,
                        Util_Komasyurui14.ToNariCase(dstKoma.Komasyurui)//強制的に【成り】に駒の種類を変更
                    ),// 移動先
                    PieceType.None//取った駒不明
                );

                // TODO: 一段目の香車のように、既に駒は成っている場合があります。無い指し手だけ追加するようにします。
                komaBETUAllMoves.AddNotOverwrite(figKoma, move);
            }

        gt_EndMethod:
            ;
        }
        public static void AssertNariMove(Maps_OneAndMulti<Finger, IMove> komabetuAllMove, string hint)
        {
            /*
            foreach(KeyValuePair<Finger, List<ShootingStarlightable>> komaAllMove in komabetuAllMove.Items)
            {
                foreach(ShootingStarlightable move in komaAllMove.Value)
                {
                    Starlightable lightable = move.Now;
                    RO_Star_Koma koma = Util_Starlightable.AsKoma(lightable);

                    if (KomaSyurui14Array.IsNari(koma.Syurui))
                    {
                        MessageBox.Show($"指し手に成りが含まれています。[{koma.Masu.Word}][{koma.Syurui}]", $"デバッグ:{hint}");
                        //Logger.Trace("指し手に成りが含まれています。");
                        goto gt_EndMethod;
                    }
                }
            }
        gt_EndMethod:
            ;
             */
        }




    }
}
