using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P269UtilSasu.L500Util;
using Grayscale.P339ConvKyokume.L500Converter;
using Grayscale.P353ConvSasuEx.L500Converter;
using System;
using System.Collections.Generic;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P354UtilSasuEx.L500Util
{

    /// <summary>
    /// ************************************************************************************************************************
    /// あるデータを、別のデータに変換します。
    /// ************************************************************************************************************************
    /// </summary>
    public abstract class Util_SasuEx
    {

        /// <summary>
        /// これが通称【水際のいんちきプログラム】なんだぜ☆
        /// 必要により、【成り】の指し手を追加します。
        /// </summary>
        public static Dictionary<string, SasuEntry> CreateNariMove(
            SkyConst src_Sky,
            Dictionary<string, SasuEntry> a_moveBetuEntry
            )
        {
            //----------------------------------------
            // 『進める駒』と、『移動先升』
            //----------------------------------------
            Dictionary<string, SasuEntry> result_komabetuEntry = new Dictionary<string, SasuEntry>();

            try
            {
                Dictionary<string, IMove> newMoveList = new Dictionary<string, IMove>();

                foreach (KeyValuePair<string, SasuEntry> entry in a_moveBetuEntry)
                {
                    // 
                    // ・移動元の駒
                    // ・移動先の駒
                    //
                    RO_Star srcKoma = Util_Starlightable.AsKoma(entry.Value.NewMove.LongTimeAgo);
                    RO_Star dstKoma = Util_Starlightable.AsKoma(entry.Value.NewMove.Now);

                    // 成りができる動きなら真。
                    bool isPromotionable;
                    if (!Util_Sasu269.IsPromotionable(out isPromotionable, srcKoma, dstKoma))
                    {
                        // ｴﾗｰ
                        goto gt_Next1;
                    }

                    if (isPromotionable)
                    {
                        IMove move = new RO_Starbeam(
                            srcKoma,// 移動元
                            new RO_Star(
                                dstKoma.Pside,
                                dstKoma.Masu,
                                Util_Komasyurui14.ToNariCase(dstKoma.Komasyurui)//強制的に【成り】に駒の種類を変更
                            ),// 移動先
                            Komasyurui14.H00_Null___//取った駒不明
                        );

                        // TODO: 一段目の香車のように、既に駒は成っている場合があります。無い指し手だけ追加するようにします。
                        string moveStr = ConvMoveStrSfen.ToMoveStrSfen(move);//重複防止用のキー
                        if (!newMoveList.ContainsKey(moveStr))
                        {
                            newMoveList.Add(moveStr, move);
                        }
                    }

                gt_Next1:
                    ;
                }

                //hubNode.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> nextNode, ref bool toBreak) =>
                //{
                //});


                // 新しく作った【成り】の指し手を追加します。
                foreach (IMove newMove in newMoveList.Values)
                {
                    // 指す前の駒
                    RO_Star sasumaenoKoma = Util_Starlightable.AsKoma(newMove.LongTimeAgo);

                    // 指した駒
                    RO_Star sasitaKoma = Util_Starlightable.AsKoma(newMove.Now);

                    // 指す前の駒を、盤上のマス目で指定
                    Finger figSasumaenoKoma = Util_Sky_FingersQuery.InMasuNow(src_Sky,
                        sasumaenoKoma.Masu).ToFirst();

                    string moveStr = ConvMoveStrSfen.ToMoveStrSfen(newMove);

                    if (!result_komabetuEntry.ContainsKey(moveStr))
                    {
                        // 指し手が既存でない局面だけを追加します。

                        // 『進める駒』と、『移動先升』
                        result_komabetuEntry.Add(moveStr, new SasuEntry(newMove, figSasumaenoKoma, sasitaKoma.Masu, true));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Convert04.cs#AddNariMoveでｴﾗｰ。:{ex.GetType().Name}:{ex.Message}");
            }

            return result_komabetuEntry;
        }

        //public static void PutAddAll_ToHubNode(
        //    Dictionary<string, SasuEntry> result_komabetuEntry,
        //    SkyConst src_Sky,
        //    KifuNode hubNode_mutable,
        //    IErrorController errH
        //    )
        //{
        //    foreach (KeyValuePair<string, SasuEntry> entry in result_komabetuEntry)
        //    {
        //        Util_SasuEx.PutAdd_ToHubNode(
        //            entry.Key,
        //            entry.Value,
        //            src_Sky,
        //            hubNode_mutable,
        //            errH
        //            );
        //    }
        //}

        //public static void PutAdd_ToHubNode(
        //    string moveStr_sfen,
        //    SasuEntry sasuEntry,
        //    SkyConst src_Sky,
        //    KifuNode hubNode_mutable,
        //    IErrorController errH
        //    )
        //{
        //    if (!hubNode_mutable.ContainsKey_ChildNodes(moveStr_sfen))//チェックを追加
        //    {
        //        hubNode_mutable.PutAdd_ChildNode(
        //            moveStr_sfen,
        //            Conv_SasuEntry.ToKifuNode(sasuEntry, src_Sky, errH)
        //        );
        //    }
        //}

    }
}
