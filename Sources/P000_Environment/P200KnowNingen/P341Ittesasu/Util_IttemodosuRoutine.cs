using Grayscale.P003Log.I500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P234Komahaiyaku.L500Util;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P324KifuTree.L250Struct;
using Grayscale.P339ConvKyokume.L500Converter;
using Grayscale.P341Ittesasu.I250OperationA;
using Grayscale.P341Ittesasu.L250OperationA;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P341Ittesasu.L500UtilA
{

    /// <summary>
    /// 一手戻すルーチン。
    /// </summary>
    public abstract class Util_IttemodosuRoutine
    {


        /// <summary>
        /// 一手戻します。
        /// </summary>
        /// <param name="ittemodosuArg"></param>
        /// <param name="ittesasu_mutable"></param>
        /// <param name="ittemodosuResult"></param>
        /// <param name="errH"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public static void Before1(
            IttemodosuArg ittemodosuArg,
            out IttemodosuResult ittemodosuResult,
            IErrorController errH
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            bool isMakimodosi = true;
            ittemodosuResult = new IttemodosuResultImpl(Fingers.Error_1, Fingers.Error_1, null, Komasyurui14.H00_Null___, null);

            //
            // 一手指し開始局面（不変）
            // 一手指し終了局面（null or 可変）
            //
            Playerside kaisi_tebanside = ((KifuNode)ittemodosuArg.KaisiNode).Value.KyokumenConst.KaisiPside;
            SkyConst kaisi_Sky = ittemodosuArg.KaisiNode.Value.KyokumenConst;

            //
            // 編集対象ノード（巻き戻し時と、進む時で異なる）
            //
            Node<IMove, KyokumenWrapper> editNodeRef;

            //------------------------------
            // 符号の追加（一手進む）
            //------------------------------
            {
                // 戻る時。
                ittemodosuResult.Susunda_Sky_orNull = null;
                editNodeRef = ittemodosuArg.KaisiNode;
            }


            //
            // 動かす駒を移動先へ。
            //
            Finger figMovedKoma;
            Util_IttemodosuRoutine.Do25_UgokasuKoma_IdoSakiHe(
                out figMovedKoma,
                ittemodosuArg.Move,
                kaisi_tebanside,
                kaisi_Sky,
                errH
                );
            ittemodosuResult.FigMovedKoma = figMovedKoma; //動かした駒更新


            if (Fingers.Error_1 == ittemodosuResult.FigMovedKoma)
            {
                goto gt_EndMethod;
            }


            //
            // 巻き戻しなら、非成りに戻します。
            //
            Komasyurui14 syurui2 = Util_IttemodosuRoutine.Do30_MakimodosiNara_HinariNiModosu(ittemodosuArg.Move, isMakimodosi);


            IMoveHalf dst;
            {
                dst = Util_IttemodosuRoutine.Do37_KomaOnDestinationMasu(syurui2, ittemodosuArg.Move,
                    kaisi_Sky);
            }



            // Sky 局面データは、この関数の途中で何回か変更されます。ローカル変数に退避しておくと、同期が取れなくなります。

            //------------------------------------------------------------
            // あれば、取られていた駒を取得
            //------------------------------------------------------------
            Finger figFoodKoma;//取られていた駒
            Util_IttemodosuRoutine.Do62_TorareteitaKoma_ifExists(
                ittemodosuArg.Move,
                kaisi_Sky,//巻き戻しのとき
                ittemodosuResult.Susunda_Sky_orNull,
                out figFoodKoma,//変更される場合あり。
                errH
                );
            ittemodosuResult.FigFoodKoma = figFoodKoma; //取られていた駒更新

            //------------------------------------------------------------
            // 駒の移動
            //------------------------------------------------------------
            if (Fingers.Error_1 != figFoodKoma)
            {
                //------------------------------------------------------------
                // 指されていた駒と、取られていた駒の移動
                //------------------------------------------------------------

                //------------------------------
                // 指し手の、取った駒部分を差替えます。
                //------------------------------
                RO_Star koma = Util_Starlightable.AsKoma(ittemodosuArg.Move.Now);
                kaisi_Sky = SkyConst.NewInstance_OverwriteOrAdd_Light(
                    kaisi_Sky,
                    ittemodosuArg.KorekaranoTemezumi_orMinus1,
                    //
                    // 指されていた駒
                    //
                    figMovedKoma,
                    dst,
                    //
                    // 取られていた駒
                    //
                    figFoodKoma,
                    new RO_Starlight(
                        new RO_Star(
                            Conv_Playerside.Reverse(koma.Pside),//先後を逆にして駒台に置きます。
                            koma.Masu,// マス
                            (Komasyurui14)ittemodosuArg.Move.FoodKomaSyurui
                        )
                    ));
            }
            else
            {
                //------------------------------------------------------------
                // 指されていた駒の移動
                //------------------------------------------------------------
                kaisi_Sky = SkyConst.NewInstance_OverwriteOrAdd_Light(
                    kaisi_Sky,
                    ittemodosuArg.KorekaranoTemezumi_orMinus1,
                    //
                    // 指されていた駒
                    //
                    figMovedKoma,
                    dst
                    );
            }
            editNodeRef.Value.SetKyokumen(kaisi_Sky);
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
            // この時点で、必ず現局面データに差替えあり
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■


            // ノード
            ittemodosuResult.SyuryoNode_OrNull = editNodeRef;// この変数を返すのがポイント。棋譜とは別に、現局面。

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// 棋譜ツリーのカレントを変更します。
        /// </summary>
        /// <param name="isMakimodosi"></param>
        /// <param name="ittemodosuReference"></param>
        /// <param name="errH"></param>
        public static void Before2(
            ref IttemodosuResult ittemodosuReference,
            IErrorController errH
            )
        {
            Node<IMove, KyokumenWrapper> editNodeRef = ittemodosuReference.SyuryoNode_OrNull;
            IMove nextMove = editNodeRef.Key;
            if (ittemodosuReference.FoodKomaSyurui != Komasyurui14.H00_Null___)
            {
                // 元のキーの、取った駒の種類だけを差替えます。
                nextMove = Util_Sky258A.BuildMove(editNodeRef.Key.LongTimeAgo, editNodeRef.Key.Now, ittemodosuReference.FoodKomaSyurui);

                // 現手番
                Playerside genTebanside = ((KifuNode)editNodeRef).Value.KyokumenConst.KaisiPside;

                // キーを差替えたノード
                editNodeRef = new KifuNodeImpl(nextMove, new KyokumenWrapper(ittemodosuReference.Susunda_Sky_orNull));//, genTebanside
            }


            string nextMoveStr = ConvMoveStrSfen.ToMoveStrSfen(nextMove);




            ittemodosuReference.SyuryoNode_OrNull = editNodeRef;// この変数を返すのがポイント。棋譜とは別に、現局面。


            //Util_IttesasuRoutine.iIttemodosuAfter3_ChangeCurrent(kifu_mutable);
        }

        public static void After3_ChangeCurrent(
            KifuTree kifu_mutable
            )
        {
            //------------------------------------------------------------
            // 取った駒を戻す
            //------------------------------------------------------------
            Node<IMove, KyokumenWrapper> removedLeaf = kifu_mutable.PopCurrentNode();
        }


        /// <summary>
        /// 動かす駒を移動先へ。
        /// </summary>
        /// <param name="figMovedKoma"></param>
        /// <param name="move">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="obsoluted_kifu_mutable"></param>
        /// <param name="isMakimodosi"></param>
        private static void Do25_UgokasuKoma_IdoSakiHe(
            out Finger figMovedKoma,
            IMove move,
            Playerside kaisi_tebanside,
            SkyConst kaisi_Sky,
            IErrorController errH
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
            // [巻戻し]のとき
            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            // 打った駒も、指した駒も、結局は将棋盤の上にあるはず。

            RO_Star koma = Util_Starlightable.AsKoma(move.Now);

            // 動かす駒
            figMovedKoma = Util_Sky_FingerQuery.InShogibanMasuNow(
                kaisi_Sky,
                koma.Pside,
                koma.Masu,//[巻戻し]のときは、先位置が　駒の居場所。
                errH
                );
            Debug.Assert(figMovedKoma != Fingers.Error_1, "駒を動かせなかった？5");
        }

        /// <summary>
        /// [巻戻し]時の、駒台にもどる動きを吸収。
        /// </summary>
        /// <param name="syurui2"></param>
        /// <param name="move">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="kifu"></param>
        /// <param name="isMakimodosi"></param>
        /// <returns></returns>
        private static IMoveHalf Do37_KomaOnDestinationMasu(
            Komasyurui14 syurui2,
            IMove move,
            SkyConst src_Sky
            )
        {
            IMoveHalf dst;

            RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);//移動元
            RO_Star dstKoma = Util_Starlightable.AsKoma(move.Now);//移動先


            SyElement masu;

            if (
                Okiba.Gote_Komadai == Conv_SyElement.ToOkiba(srcKoma.Masu)
                || Okiba.Sente_Komadai == Conv_SyElement.ToOkiba(srcKoma.Masu)
                )
            {
                //>>>>> １手前が駒台なら

                // 駒台の空いている場所
                masu = Util_IttesasuRoutine.GetKomadaiKomabukuroSpace(Conv_SyElement.ToOkiba(srcKoma.Masu), src_Sky);
                // 必ず空いている場所があるものとします。
            }
            else
            {
                //>>>>> １手前が将棋盤上なら

                // その位置
                masu = srcKoma.Masu;//戻し先
            }



            dst = new RO_Starlight(
                //move.Finger,
                new RO_Star(dstKoma.Pside,
                masu,//戻し先
                syurui2)
                );

            return dst;
        }

        /// <summary>
        /// あれば、取られていた駒を取得
        /// </summary>
        /// <param name="move"></param>
        /// <param name="kaisi_Sky"></param>
        /// <param name="susunda_Sky_orNull"></param>
        /// <param name="out_figFoodKoma"></param>
        /// <param name="errH"></param>
        private static void Do62_TorareteitaKoma_ifExists(
            IMove move,
            SkyConst kaisi_Sky,//巻き戻しのとき
            SkyConst susunda_Sky_orNull,
            out Finger out_figFoodKoma,
            IErrorController errH
        )
        {
            if (Komasyurui14.H00_Null___ != (Komasyurui14)move.FoodKomaSyurui)
            {
                //----------------------------------------
                // 取られていた駒があった場合
                //----------------------------------------
                RO_Star koma = Util_Starlightable.AsKoma(move.Now);

                // 駒台から、駒を検索します。
                Okiba okiba;
                if (Playerside.P2 == koma.Pside)
                {
                    okiba = Okiba.Gote_Komadai;
                }
                else
                {
                    okiba = Okiba.Sente_Komadai;
                }


                // 取った駒は、種類が同じなら、駒台のどの駒でも同じです。
                out_figFoodKoma = Util_Sky_FingerQuery.InOkibaSyuruiNowIgnoreCase(kaisi_Sky, okiba, (Komasyurui14)move.FoodKomaSyurui, errH);
            }
            else
            {
                //----------------------------------------
                // 駒は取られていなかった場合
                //----------------------------------------
                out_figFoodKoma = Fingers.Error_1;
            }
        }

        /// <summary>
        /// 巻き戻しなら、非成りに戻します。
        /// </summary>
        /// <param name="move">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="isBack"></param>
        /// <returns></returns>
        private static Komasyurui14 Do30_MakimodosiNara_HinariNiModosu(
            IMove move,
            bool isBack)
        {
            //------------------------------------------------------------
            // 確定  ：  移動先升
            //------------------------------------------------------------
            Komasyurui14 syurui2;
            {
                //----------
                // 成るかどうか
                //----------

                RO_Star koma = Util_Starlightable.AsKoma(move.Now);


                if (Util_Sky_BoolQuery.IsNattaMove(move))
                {
                    if (isBack)
                    {
                        // 正順で成ったのなら、巻戻しでは「非成」に戻します。
                        syurui2 = Util_Komasyurui14.NarazuCaseHandle(Util_Komahaiyaku184.Syurui(koma.Haiyaku));
                    }
                    else
                    {
                        syurui2 = Util_Komahaiyaku184.Syurui(koma.Haiyaku);
                    }
                }
                else
                {
                    syurui2 = Util_Komahaiyaku184.Syurui(koma.Haiyaku);
                }
            }

            return syurui2;
        }

    }


}
