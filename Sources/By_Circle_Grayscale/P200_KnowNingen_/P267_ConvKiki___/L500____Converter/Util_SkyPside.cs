using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P211_WordShogi__.L510____Komanokiki;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P256_SeizaFinger.L250____Struct;
using Grayscale.P266_KyokumMoves.L500____Util;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P267_ConvKiki___.L500____Converter
{
    public abstract class Util_SkyPside
    {
        /// <summary>
        /// 駒の利きを調べます。
        /// </summary>
        /// <param name="src_Sky"></param>
        /// <returns></returns>
        public static MasubetuKikisuImpl ToMasubetuKikisu(
            SkyConst src_Sky,
            Playerside tebanside
            )
        {

            // ①現手番の駒の移動可能場所_被王手含む
            List_OneAndMulti<Finger, SySet<SyElement>> komaBETUSusumeruMasus;

            Util_KyokumenMoves.LA_Split_KomaBETUSusumeruMasus(
                3,
                //node_forLog,
                out komaBETUSusumeruMasus,//進めるマス
                true,//本将棋か
                src_Sky,//現在の局面
                tebanside,//手番
                false//相手番か
#if DEBUG
                ,
                null
#endif
            );

            MasubetuKikisuImpl result = new MasubetuKikisuImpl();

            //
            // 「升ごとの敵味方」を調べます。
            //
            foreach (Finger figKoma in Finger_Honshogi.Items_KomaOnly)// 全駒
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);

                result.HMasu_PlayersideList[Conv_SyElement.ToMasuNumber(koma.Masu)] = koma.Pside;
            }

            //
            // 駒のない升は無視します。
            //

            //
            // 駒のあるマスに、その駒の味方のコマが効いていれば　味方＋１
            //
            foreach (Finger figKoma in Finger_Honshogi.Items_KomaOnly)// 全駒
            {
                //
                // 駒
                //
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);

                // 将棋盤上の戦駒のみ判定
                if (Okiba.ShogiBan != Conv_SyElement.ToOkiba(koma.Masu))
                {
                    goto gt_Next1;
                }


                //
                // 駒の利きカウント FIXME:貫通してないか？
                //
                komaBETUSusumeruMasus.Foreach_Entry((Finger figKoma2, SySet<SyElement> kikiZukei, ref bool toBreak) =>
                {
                    IEnumerable<SyElement> kikiMasuList = kikiZukei.Elements;
                    foreach (SyElement masu in kikiMasuList)
                    {
                        // その枡に利いている駒のハンドルを追加
                        if (result.HMasu_PlayersideList[Conv_SyElement.ToMasuNumber(masu)] == Playerside.Empty)
                        {
                            // 駒のないマスは無視。
                        }
                        else if (Playerside.P1 == koma.Pside)
                        {
                            // 利きのあるマスにある駒と、この駒のプレイヤーサイドが同じ。
                            result.Kikisu_AtMasu_1P[Conv_SyElement.ToMasuNumber(masu)] += 1;
                        }
                        else
                        {
                            // 反対の場合。
                            result.Kikisu_AtMasu_2P[Conv_SyElement.ToMasuNumber(masu)] += 1;
                        }
                    }
                });

            gt_Next1:
                ;
            }

            return result;

        }

    }
}
