﻿using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Conv_KomabetuSusumeruMasus
    {
        /// <summary>
        /// 変換「各（自駒が動ける升）」→「各（自駒が動ける手）」
        /// </summary>
        /// <param name="komaBETUSusumeruMasus">駒別の進める升</param>
        /// <param name="siteiNode">指定ノード</param>
        /// <returns></returns>
        public static Maps_OneAndMulti<Finger, IMove> ToKomaBetuAllMoves(
            List_OneAndMulti<Finger, SySet<SyElement>> komaBETUSusumeruMasus,
            SkyConst src_Sky//Node<Starbeamable, KyokumenWrapper> siteiNode
            )
        {
            Maps_OneAndMulti<Finger, IMove> result_komabetuAllMove = new Maps_OneAndMulti<Finger, IMove>();

            komaBETUSusumeruMasus.Foreach_Entry((Finger figKoma, SySet<SyElement> susumuMasuSet, ref bool toBreak) =>
            {
                // 動かす星。
                RO_Star srcStar = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);

                foreach (SyElement susumuMasu in susumuMasuSet.Elements)// 星が進める升。
                {
                    // 移動先の星（升の変更）
                    RO_Star dstStar = new RO_Star(
                        srcStar.Pside,
                        susumuMasu,//Masu_Honshogi.Items_All[Util_Masu10.AsMasuNumber(susumuMasu)],
                        srcStar.Komasyurui// srcStar.Haiyaku//TODO:ここで、駒の種類が「成り」に上書きされているバージョンも考えたい
                    );

                    IMove move = new RO_Starbeam(
                        srcStar,// 移動元
                        dstStar,// 移動先
                        PieceType.None//取った駒不明
                    );
                    result_komabetuAllMove.Put_NewOrOverwrite(figKoma, move);//FIXME: １つの駒に指し手は１つ？？

                    // これが通称【水際のいんちきプログラム】なんだぜ☆
                    // 必要により、【成り】の指し手を追加します。
                    Util_Sasu269.Add_KomaBETUAllNariMoves(
                        result_komabetuAllMove,
                        figKoma,//動かす駒
                        srcStar,//動かす星
                        dstStar//移動先の星
                        );
                }
            });

            return result_komabetuAllMove;
        }

    }
}
