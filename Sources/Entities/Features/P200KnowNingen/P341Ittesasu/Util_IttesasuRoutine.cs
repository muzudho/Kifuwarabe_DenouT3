﻿namespace Grayscale.Kifuwarakaku.Entities.Features
{
#if DEBUG
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using Grayscale.Kifuwarakaku.Entities.Logging;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#else
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#endif

    public abstract class Util_IttesasuRoutine
    {


        /// <summary>
        /// 一手指します。
        /// </summary>
        /// <param name="ittesasuArg"></param>
        /// <param name="ittesasu_mutable"></param>
        /// <param name="ittesasuResult"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public static void Before1(
            IttesasuArg ittesasuArg,
            out IttesasuResult ittesasuResult,
            string hint
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            //------------------------------
            // 用意
            //------------------------------
            ittesasuResult = new IttesasuResultImpl(Fingers.Error_1, Fingers.Error_1, null, PieceType.None, null);
            SkyConst kaisi_Sky = ittesasuArg.KaisiKyokumen.KyokumenConst;// 一手指し開始局面（不変）
            Node<IMove, KyokumenWrapper> editNodeRef;// 編集対象ノード（巻き戻し時と、進む時で異なる）

            //------------------------------
            // 符号(局面)の追加
            //------------------------------
            {
                //進むときは、必ずノードの追加と、カレントの移動がある。

                //現局面ノードのクローンを作成します。
                editNodeRef = new KifuNodeImpl(ittesasuArg.KorekaranoMove, new KyokumenWrapper(
                    SkyConst.NewInstance_ReversePside(kaisi_Sky, ittesasuArg.KorekaranoTemezumi_orMinus1))
                    );
                ittesasuResult.Susunda_Sky_orNull = editNodeRef.Value.KyokumenConst;
            }

            //------------------------------
            // 動かす駒を移動先へ。
            //------------------------------
            Debug.Assert(null != ittesasuArg.KorekaranoMove, "これからの指し手がヌルでした。");
            Finger figMovedKoma;
            Util_IttesasuRoutine.Do24_UgokasuKoma_IdoSakiHe(
                out figMovedKoma,
                ittesasuArg.KorekaranoMove,
                ittesasuArg.KaisiTebanside,
                kaisi_Sky,
                hint
                );
            ittesasuResult.FigMovedKoma = figMovedKoma; //動かした駒更新
            Debug.Assert(Fingers.Error_1 != ittesasuResult.FigMovedKoma, "動かした駒がない☆！？エラーだぜ☆！");

            RO_Star korekaranoKoma = Util_Starlightable.AsKoma(ittesasuArg.KorekaranoMove.Now);
            IMoveHalf afterStar;
            {
                afterStar = Util_IttesasuRoutine.Do36_KomaOnDestinationMasu(
                    korekaranoKoma.Komasyurui,
                    ittesasuArg.KorekaranoMove,
                    ittesasuResult.Susunda_Sky_orNull
                    );
            }

            // Sky 局面データは、この関数の途中で何回か変更されます。ローカル変数に退避しておくと、同期が取れなくなります。

            //------------------------------------------------------------
            // 駒を取る
            //------------------------------------------------------------
            Finger figFoodKoma = Fingers.Error_1;
            RO_Star food_koma = null;
            Playerside food_pside = Playerside.Empty;
            SyElement food_akiMasu = Masu_Honshogi.Query_Basho(Masu_Honshogi.nError);
            {
                Util_IttesasuRoutine.Do61_KomaToru(
                    afterStar,
                    ittesasuResult.Susunda_Sky_orNull,
                    out figFoodKoma,
                    out food_koma,
                    out food_pside,
                    out food_akiMasu
                    );

                if (Fingers.Error_1 != figFoodKoma)
                {
                    //>>>>> 指した先に駒があったなら
                    ittesasuResult.FoodKomaSyurui = food_koma.Komasyurui;
                }
                else
                {
                    ittesasuResult.FoodKomaSyurui = PieceType.None;
                }
            }
            Debug.Assert(figMovedKoma != Fingers.Error_1, "駒を動かせなかった？1");

            if (Fingers.Error_1 != figFoodKoma)
            {
                //------------------------------------------------------------
                // 指した駒と、取った駒の移動
                //------------------------------------------------------------

                //------------------------------
                // 局面データの書き換え
                //------------------------------
                ittesasuResult.Susunda_Sky_orNull = SkyConst.NewInstance_OverwriteOrAdd_Light(
                    ittesasuResult.Susunda_Sky_orNull,
                    ittesasuArg.KorekaranoTemezumi_orMinus1,
                    //
                    // 指した駒
                    //
                    figMovedKoma,//指した駒番号
                    afterStar,//指した駒
                              //
                              // 取った駒
                              //
                    figFoodKoma,
                    new RO_Starlight(
                        new RO_Star(
                            food_pside,
                            food_akiMasu,//駒台の空きマスへ
                            food_koma.ToNarazuCase()// 取られた駒の種類。表面を上に向ける。
                        )
                    )
                );
            }
            else
            {
                //------------------------------------------------------------
                // 指した駒の移動
                //------------------------------------------------------------

                ittesasuResult.Susunda_Sky_orNull = SkyConst.NewInstance_OverwriteOrAdd_Light(
                    ittesasuResult.Susunda_Sky_orNull,//駒を取って変化しているかもしれない？
                    ittesasuArg.KorekaranoTemezumi_orMinus1,// これから作る局面の、手目済み。
                                                            //
                                                            // 指した駒
                                                            //
                    figMovedKoma,
                    afterStar,
                    //
                    // 手得計算
                    //
                    korekaranoKoma.Komasyurui,
                    0,// TODO: suji or index
                    korekaranoKoma.Masu
                    );
            }
            editNodeRef.Value.SetKyokumen(ittesasuResult.Susunda_Sky_orNull);
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
            // この時点で、必ず現局面データに差替えあり
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
            //
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
            // 局面データに変更があったものとして進めます。
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

            ittesasuResult.FigFoodKoma = figFoodKoma; //取った駒更新

            //
            // ノード
            ittesasuResult.Set_SyuryoNode_OrNull = editNodeRef;// この変数を返すのがポイント。棋譜とは別に、現局面。
        }

        /// <summary>
        /// 棋譜ツリーのカレントを変更します。
        /// </summary>
        /// <param name="isMakimodosi"></param>
        /// <param name="ittesasuReference"></param>
        public static void Before2(
            ref IttesasuResult ittesasuReference
            )
        {
            Node<IMove, KyokumenWrapper> editNodeRef = ittesasuReference.Get_SyuryoNode_OrNull;
            IMove nextMove = editNodeRef.Key;
            if (ittesasuReference.FoodKomaSyurui != PieceType.None)
            {
                // 元のキーの、取った駒の種類だけを差替えます。
                nextMove = Util_Sky258A.BuildMove(editNodeRef.Key.LongTimeAgo, editNodeRef.Key.Now, ittesasuReference.FoodKomaSyurui);

                // 現手番
                Playerside genTebanside = ((KifuNode)editNodeRef).Value.KyokumenConst.KaisiPside;

                // キーを差替えたノード
                editNodeRef = new KifuNodeImpl(nextMove, new KyokumenWrapper(ittesasuReference.Susunda_Sky_orNull));//, genTebanside
            }


            string nextMoveStr = ConvMoveStrSfen.ToMoveStrSfen(nextMove);



            ittesasuReference.Set_SyuryoNode_OrNull = editNodeRef;// この変数を返すのがポイント。棋譜とは別に、現局面。
        }

        /// <summary>
        /// 棋譜ツリーのカレントを変更します。
        /// </summary>
        /// <param name="kifu_mutable"></param>
        /// <param name="nextMoveStr"></param>
        /// <param name="edit_childNode_Ref"></param>
        public static void After3_ChangeCurrent(
            KifuTree kifu_mutable,
            string nextMoveStr,
            Node<IMove, KyokumenWrapper> edit_childNode_Ref
            )
        {

            if (!((KifuNode)kifu_mutable.CurNode).HasTuginoitte(nextMoveStr))
            {
                //----------------------------------------
                // 次ノード追加（なければ）
                //----------------------------------------
                kifu_mutable.GetSennititeCounter().CountUp_New(Conv_Sky.ToKyokumenHash(edit_childNode_Ref.Value.KyokumenConst), "After3_ChangeCurrent(次の一手なし)");
                ((KifuNode)kifu_mutable.CurNode).PutTuginoitte_New(edit_childNode_Ref);//次ノートを追加します。
            }
            else
            {
                //----------------------------------------
                // 次ノード上書き（あれば）
                //----------------------------------------
                kifu_mutable.GetSennititeCounter().CountUp_New(Conv_Sky.ToKyokumenHash(edit_childNode_Ref.Value.KyokumenConst), "After3_ChangeCurrent（次の一手あり）");
                ((KifuNode)kifu_mutable.CurNode).PutTuginoitte_Override(edit_childNode_Ref);//次ノートを上書きします。
            }

            Node<IMove, KyokumenWrapper> temp = kifu_mutable.CurNode;
            kifu_mutable.SetCurNode(edit_childNode_Ref);//次ノードを、これからのカレントとします。
            edit_childNode_Ref.SetParentNode(temp);
        }



        /// <summary>
        /// 動かす駒を移動先へ。
        /// </summary>
        /// <param name="figMovedKoma"></param>
        /// <param name="move">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="obsoluted_kifu_mutable"></param>
        /// <param name="isMakimodosi"></param>
        private static void Do24_UgokasuKoma_IdoSakiHe(
            out Finger figMovedKoma,
            IMove move,
            Playerside kaisi_tebanside,
            SkyConst kaisi_Sky,
            string hint
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            figMovedKoma = Fingers.Error_1;

            //------------------------------------------------------------
            // 選択  ：  動かす駒
            //------------------------------------------------------------
            // 進むとき
            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            //Debug.Assert(null != move, "Sasu24_UgokasuKoma_IdoSakiHe: 指し手がヌルでした。");
            if (Util_Sky_BoolQuery.IsDaAction(move))// 多分、ここで move がヌルになるエラーがある☆
            {
                //----------
                // 駒台から “打”
                //----------
                RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);
                RO_Star dstKoma = Util_Starlightable.AsKoma(move.Now);

                // FIXME: 駒台の、どの駒を拾うか？
                figMovedKoma = Util_Sky_FingerQuery.InOkibaSyuruiNowIgnoreCase(
                    kaisi_Sky,
                    Conv_SyElement.ToOkiba(srcKoma.Masu),
                    Util_Komahaiyaku184.Syurui(dstKoma.Haiyaku)
                    );
                Debug.Assert(figMovedKoma != Fingers.Error_1, "駒を動かせなかった？14");
            }
            else
            {
                //----------
                // 将棋盤から
                //----------

                RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);
                Debug.Assert(!Masu_Honshogi.IsErrorBasho(srcKoma.Masu), "srcKoma.Masuエラー。15");
                RO_Star dstKoma = Util_Starlightable.AsKoma(move.Now);

                figMovedKoma = Util_Sky_FingerQuery.InShogibanMasuNow(
                    kaisi_Sky,
                    dstKoma.Pside,
                    Util_Masu10.OkibaSujiDanToMasu(
                        Conv_SyElement.ToOkiba(Masu_Honshogi.Masus_All[Conv_SyElement.ToMasuNumber(dstKoma.Masu)]),
                        Conv_SyElement.ToMasuNumber(srcKoma.Masu)
                        )
                        );
                Debug.Assert(figMovedKoma != Fingers.Error_1, "駒を動かせなかった？13");
            }
        }



        /// <summary>
        /// [巻戻し]時の、駒台にもどる動きを吸収。
        /// </summary>
        /// <param name="syurui2"></param>
        /// <param name="move">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="kifu"></param>
        /// <param name="isMakimodosi"></param>
        /// <returns></returns>
        private static IMoveHalf Do36_KomaOnDestinationMasu(
            PieceType syurui2,
            IMove move,
            SkyConst src_Sky)
        {
            IMoveHalf dst;

            RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);//移動元
            RO_Star dstKoma = Util_Starlightable.AsKoma(move.Now);//移動先

            // 次の位置


            dst = new RO_Starlight(
                new RO_Star(dstKoma.Pside,
                dstKoma.Masu,
                syurui2)
                );

            return dst;
        }



        /// <summary>
        /// 駒を取る動き。
        /// </summary>
        private static void Do61_KomaToru(
            IMoveHalf dst,
            SkyConst susunda_Sky_orNull_before,//駒を取られたとき、局面を変更します。
            out Finger out_figFoodKoma,
            out RO_Star out_food_koma,
            out Playerside pside,
            out SyElement akiMasu
            )
        {
            RO_Star dstKoma = Util_Starlightable.AsKoma(dst.Now);

            //----------
            // 将棋盤上のその場所に駒はあるか
            //----------
            out_figFoodKoma = Util_Sky_FingersQuery.InMasuNow(susunda_Sky_orNull_before, dstKoma.Masu).ToFirst();//盤上


            if (Fingers.Error_1 != out_figFoodKoma)
            {
                //>>>>> 指した先に駒があったなら

                //
                // 取られる駒
                //
                out_food_koma = Util_Starlightable.AsKoma(susunda_Sky_orNull_before.StarlightIndexOf(out_figFoodKoma).Now);
#if DEBUG
                Logger.Trace($"駒取った={out_food_koma.Komasyurui}{Environment.NewLine}");
#endif
                //
                // 取られる駒は、駒置き場の空きマスに移動させます。
                //
                Okiba okiba;
                switch (dstKoma.Pside)
                {
                    case Playerside.P1:
                        {
                            okiba = Okiba.Sente_Komadai;
                            pside = Playerside.P1;
                        }
                        break;
                    case Playerside.P2:
                        {
                            okiba = Okiba.Gote_Komadai;
                            pside = Playerside.P2;
                        }
                        break;
                    default:
                        {
                            //>>>>> エラー：　先後がおかしいです。
                            throw new Exception($@"エラー：　先後がおかしいです。
dst.Pside={dstKoma.Pside}");
                        }
                }

                //
                // 駒台に駒を置く動き
                //
                {
                    // 駒台の空きスペース
                    akiMasu = Util_IttesasuRoutine.GetKomadaiKomabukuroSpace(okiba, susunda_Sky_orNull_before);


                    if (Masu_Honshogi.IsErrorBasho(akiMasu))
                    {
                        //>>>>> エラー：　駒台に空きスペースがありませんでした。
                        throw new Exception($@"エラー：　駒台に空きスペースがありませんでした。
駒台={Okiba.Gote_Komadai}");
                    }
                    //>>>>> 駒台に空きスペースがありました。
                }
            }
            else
            {
                out_food_koma = null;
                pside = Playerside.Empty;
                akiMasu = Masu_Honshogi.Query_Basho(Masu_Honshogi.nError);
            }
        }



        /// <summary>
        /// ************************************************************************************************************************
        /// 駒台の空いている升を返します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="okiba">先手駒台、または後手駒台</param>
        /// <param name="uc_Main">メインパネル</param>
        /// <returns>置ける場所。無ければヌル。</returns>
        public static SyElement GetKomadaiKomabukuroSpace(Okiba okiba, SkyConst src_Sky)
        {
            SyElement akiMasu = Masu_Honshogi.Query_Basho(Masu_Honshogi.nError);

            // 先手駒台または後手駒台の、各マスの駒がある場所を調べます。
            bool[] exists = new bool[Util_Masu10.KOMADAI_KOMABUKURO_SPACE_LENGTH];//駒台スペースは40マスです。


            src_Sky.Foreach_Starlights((Finger finger, IMoveHalf komaP, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(komaP.Now);

                if (Conv_SyElement.ToOkiba(koma.Masu) == okiba)
                {
                    exists[
                        Conv_SyElement.ToMasuNumber(koma.Masu) - Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))
                        ] = true;
                }
            });


            //駒台スペースは40マスです。
            for (int i = 0; i < Util_Masu10.KOMADAI_KOMABUKURO_SPACE_LENGTH; i++)
            {
                if (!exists[i])
                {
                    akiMasu = Masu_Honshogi.Masus_All[i + Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))];
                    goto gt_EndMethod;
                }
            }

        gt_EndMethod:

            //Logger.Trace($"ゲット駒台駒袋スペース＝{akiMasu}");

            return akiMasu;
        }


    }


}
