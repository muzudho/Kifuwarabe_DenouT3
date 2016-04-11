using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P234_Komahaiyaku.L500____Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P324_KifuTree___.L250____Struct;
using Grayscale.P339_ConvKyokume.L500____Converter;
using Grayscale.P341_Ittesasu___.L___250_OperationA;
using Grayscale.P341_Ittesasu___.L250____OperationA;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P341_Ittesasu___.L500____UtilA
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
            KwErrorHandler errH
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
            Node<Starbeamable, KyokumenWrapper> editNodeRef;

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
                ittemodosuArg.Sasite,
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
            Komasyurui14 syurui2 = Util_IttemodosuRoutine.Do30_MakimodosiNara_HinariNiModosu(ittemodosuArg.Sasite, isMakimodosi);


            Starlight dst;
            {
                dst = Util_IttemodosuRoutine.Do37_KomaOnDestinationMasu(syurui2, ittemodosuArg.Sasite,
                    kaisi_Sky);
            }



            // Sky 局面データは、この関数の途中で何回か変更されます。ローカル変数に退避しておくと、同期が取れなくなります。

            //------------------------------------------------------------
            // あれば、取られていた駒を取得
            //------------------------------------------------------------
            Finger figFoodKoma;//取られていた駒
            Util_IttemodosuRoutine.Do62_TorareteitaKoma_ifExists(
                ittemodosuArg.Sasite,
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
                RO_Star koma = Util_Starlightable.AsKoma(ittemodosuArg.Sasite.Now);
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
                            (Komasyurui14)ittemodosuArg.Sasite.FoodKomaSyurui
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
            KwErrorHandler errH
            )
        {
            Node<Starbeamable, KyokumenWrapper> editNodeRef = ittemodosuReference.SyuryoNode_OrNull;
            Starbeamable nextSasite = editNodeRef.Key;
            if (ittemodosuReference.FoodKomaSyurui != Komasyurui14.H00_Null___)
            {
                // 元のキーの、取った駒の種類だけを差替えます。
                nextSasite = Util_Sky258A.BuildSasite(editNodeRef.Key.LongTimeAgo, editNodeRef.Key.Now, ittemodosuReference.FoodKomaSyurui);

                // 現手番
                Playerside genTebanside = ((KifuNode)editNodeRef).Value.KyokumenConst.KaisiPside;

                // キーを差替えたノード
                editNodeRef = new KifuNodeImpl(nextSasite, new KyokumenWrapper(ittemodosuReference.Susunda_Sky_orNull));//, genTebanside
            }


            string nextSasiteStr = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(nextSasite);




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
            Node<Starbeamable, KyokumenWrapper> removedLeaf = kifu_mutable.PopCurrentNode();
        }


        /// <summary>
        /// 動かす駒を移動先へ。
        /// </summary>
        /// <param name="figMovedKoma"></param>
        /// <param name="sasite">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="obsoluted_kifu_mutable"></param>
        /// <param name="isMakimodosi"></param>
        private static void Do25_UgokasuKoma_IdoSakiHe(
            out Finger figMovedKoma,
            Starbeamable sasite,
            Playerside kaisi_tebanside,
            SkyConst kaisi_Sky,
            KwErrorHandler errH
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

            RO_Star koma = Util_Starlightable.AsKoma(sasite.Now);

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
        /// <param name="sasite">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="kifu"></param>
        /// <param name="isMakimodosi"></param>
        /// <returns></returns>
        private static Starlight Do37_KomaOnDestinationMasu(
            Komasyurui14 syurui2,
            Starbeamable sasite,
            SkyConst src_Sky
            )
        {
            Starlight dst;

            RO_Star srcKoma = Util_Starlightable.AsKoma(sasite.LongTimeAgo);//移動元
            RO_Star dstKoma = Util_Starlightable.AsKoma(sasite.Now);//移動先


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
                //sasite.Finger,
                new RO_Star(dstKoma.Pside,
                masu,//戻し先
                syurui2)
                );

            return dst;
        }

        /// <summary>
        /// あれば、取られていた駒を取得
        /// </summary>
        /// <param name="sasite"></param>
        /// <param name="kaisi_Sky"></param>
        /// <param name="susunda_Sky_orNull"></param>
        /// <param name="out_figFoodKoma"></param>
        /// <param name="errH"></param>
        private static void Do62_TorareteitaKoma_ifExists(
            Starbeamable sasite,
            SkyConst kaisi_Sky,//巻き戻しのとき
            SkyConst susunda_Sky_orNull,
            out Finger out_figFoodKoma,
            KwErrorHandler errH
        )
        {
            if (Komasyurui14.H00_Null___ != (Komasyurui14)sasite.FoodKomaSyurui)
            {
                //----------------------------------------
                // 取られていた駒があった場合
                //----------------------------------------
                RO_Star koma = Util_Starlightable.AsKoma(sasite.Now);

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
                out_figFoodKoma = Util_Sky_FingerQuery.InOkibaSyuruiNow_IgnoreCase(kaisi_Sky, okiba, (Komasyurui14)sasite.FoodKomaSyurui, errH);
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
        /// <param name="sasite">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="isBack"></param>
        /// <returns></returns>
        private static Komasyurui14 Do30_MakimodosiNara_HinariNiModosu(
            Starbeamable sasite,
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

                RO_Star koma = Util_Starlightable.AsKoma(sasite.Now);


                if (Util_Sky_BoolQuery.IsNatta_Sasite(sasite))
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
