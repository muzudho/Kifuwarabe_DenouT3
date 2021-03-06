﻿using System.Collections.Generic;
using Grayscale.Kifuwarakaku.Entities.Logging;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// 星別指し手ユーティリティー。
    /// </summary>
    public abstract class ConvStarbetuMoves
    {

        /// <summary>
        /// 変換：星別指し手一覧　→　次の局面の一覧をもった、入れ物ノード。
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="pside_genTeban"></param>
        /// <returns>次の局面一覧を持った、入れ物ノード（ハブ・ノード）</returns>
        public static KifuNode ToNextNodes_AsHubNode(
            Maps_OneAndMulti<Finger, IMove> komabetuAllMove,
            SkyConst src_Sky//Node<Starbeamable, KyokumenWrapper> to_parentNode,//親となる予定のノード
            )
        {
            KifuNode hubNode = new KifuNodeImpl(null, null);//蝶番

#if DEBUG
            string dump = komabetuAllMove.Dump();
#endif

            foreach (KeyValuePair<Finger, List<IMove>> entry1 in komabetuAllMove.Items)
            {
                Finger figKoma = entry1.Key;// 動かす駒

                foreach (IMove move in entry1.Value)// 駒の動ける升
                {
                    string sfenText = ConvMoveStrSfen.ToMoveStrSfen(move);
                    if (hubNode.ContainsKey_ChildNodes(sfenText))
                    {
                        // 既存の指し手なら無視
                        Logger.Trace($"既存の指し手なので無視します1。sfenText=[{sfenText}]");
                    }
                    else
                    {
                        // 指したあとの次の局面を作るだけ☆
                        hubNode.PutAdd_ChildNode(ConvMoveStrSfen.ToMoveStrSfen(move), new KifuNodeImpl(move, new KyokumenWrapper(
                            Util_Sasu341.Sasu(
                                src_Sky,// to_parentNode.Value.ToKyokumenConst,//指定局面
                                figKoma,//動かす駒
                                Util_Starlightable.AsKoma(move.Now).Masu,//移動先升
                                false//成りません。
                        ))));
                    }
                }
            }

            return hubNode;
        }

    }
}
