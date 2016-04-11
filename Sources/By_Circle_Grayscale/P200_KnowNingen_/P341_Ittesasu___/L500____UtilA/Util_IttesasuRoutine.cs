using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P234_Komahaiyaku.L500____Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P239_ConvWords__.L500____Converter;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P324_KifuTree___.L250____Struct;
using Grayscale.P339_ConvKyokume.L500____Converter;
using Grayscale.P341_Ittesasu___.L___250_OperationA;
using Grayscale.P341_Ittesasu___.L250____OperationA;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P341_Ittesasu___.L500____UtilA
{


    public abstract class Util_IttesasuRoutine
    {


        /// <summary>
        /// 一手指します。
        /// </summary>
        /// <param name="ittesasuArg"></param>
        /// <param name="ittesasu_mutable"></param>
        /// <param name="ittesasuResult"></param>
        /// <param name="errH"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public static void Before1(
            IttesasuArg ittesasuArg,
            out IttesasuResult ittesasuResult,
            KwErrorHandler errH,
            string hint
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            int exceptionArea = 0;

            try
            {
                //------------------------------
                // 用意
                //------------------------------
                exceptionArea = 1010;
                ittesasuResult = new IttesasuResultImpl(Fingers.Error_1, Fingers.Error_1, null, Komasyurui14.H00_Null___, null);
                SkyConst kaisi_Sky = ittesasuArg.KaisiKyokumen.KyokumenConst;// 一手指し開始局面（不変）
                Node<Starbeamable, KyokumenWrapper> editNodeRef;// 編集対象ノード（巻き戻し時と、進む時で異なる）

                exceptionArea = 1040;
                //------------------------------
                // 符号(局面)の追加
                //------------------------------
                {
                    //進むときは、必ずノードの追加と、カレントの移動がある。

                    //現局面ノードのクローンを作成します。
                    editNodeRef = new KifuNodeImpl(ittesasuArg.KorekaranoSasite, new KyokumenWrapper(
                        SkyConst.NewInstance_ReversePside(kaisi_Sky,ittesasuArg.KorekaranoTemezumi_orMinus1))
                        );
                    ittesasuResult.Susunda_Sky_orNull = editNodeRef.Value.KyokumenConst;
                }


                exceptionArea = 1050;
                //------------------------------
                // 動かす駒を移動先へ。
                //------------------------------
                Debug.Assert(null != ittesasuArg.KorekaranoSasite, "これからの指し手がヌルでした。");
                Finger figMovedKoma;
                Util_IttesasuRoutine.Do24_UgokasuKoma_IdoSakiHe(
                    out figMovedKoma,
                    ittesasuArg.KorekaranoSasite,
                    ittesasuArg.KaisiTebanside,
                    kaisi_Sky,
                    errH,
                    hint
                    );
                ittesasuResult.FigMovedKoma = figMovedKoma; //動かした駒更新
                Debug.Assert(Fingers.Error_1 != ittesasuResult.FigMovedKoma, "動かした駒がない☆！？エラーだぜ☆！");


                exceptionArea = 1060;
                RO_Star korekaranoKoma = Util_Starlightable.AsKoma(ittesasuArg.KorekaranoSasite.Now);
                Starlight afterStar;
                {
                    afterStar = Util_IttesasuRoutine.Do36_KomaOnDestinationMasu(
                        korekaranoKoma.Komasyurui,
                        ittesasuArg.KorekaranoSasite,
                        ittesasuResult.Susunda_Sky_orNull
                        );
                }



                exceptionArea = 1070;
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
                        out food_akiMasu,
                        errH
                        );

                    if (Fingers.Error_1 != figFoodKoma)
                    {
                        //>>>>> 指した先に駒があったなら
                        ittesasuResult.FoodKomaSyurui = food_koma.Komasyurui;
                    }
                    else
                    {
                        ittesasuResult.FoodKomaSyurui = Komasyurui14.H00_Null___;
                    }
                }
                Debug.Assert(figMovedKoma != Fingers.Error_1, "駒を動かせなかった？1");


                exceptionArea = 1080;
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


                exceptionArea = 1090;
                ittesasuResult.FigFoodKoma = figFoodKoma; //取った駒更新

                //
                // ノード
                ittesasuResult.Set_SyuryoNode_OrNull = editNodeRef;// この変数を返すのがポイント。棋譜とは別に、現局面。
            }
            catch (Exception ex)
            {
                //>>>>> エラーが起こりました。

                // どうにもできないので  ログだけ取って無視します。
                string message = "Util_IttesasuRoutine#Execute（B）： exceptionArea=" + exceptionArea + "\n" + ex.GetType().Name + "：" + ex.Message;
                errH.Logger.WriteLine_Error(message);
                throw ex;
            }
        }

        /// <summary>
        /// 棋譜ツリーのカレントを変更します。
        /// </summary>
        /// <param name="isMakimodosi"></param>
        /// <param name="ittesasuReference"></param>
        /// <param name="errH"></param>
        public static void Before2(
            ref IttesasuResult ittesasuReference,
            KwErrorHandler errH
            )
        {
            Node<Starbeamable, KyokumenWrapper> editNodeRef = ittesasuReference.Get_SyuryoNode_OrNull;
            Starbeamable nextSasite = editNodeRef.Key;
            if (ittesasuReference.FoodKomaSyurui != Komasyurui14.H00_Null___)
            {
                // 元のキーの、取った駒の種類だけを差替えます。
                nextSasite = Util_Sky258A.BuildSasite(editNodeRef.Key.LongTimeAgo, editNodeRef.Key.Now, ittesasuReference.FoodKomaSyurui);

                // 現手番
                Playerside genTebanside = ((KifuNode)editNodeRef).Value.KyokumenConst.KaisiPside;

                // キーを差替えたノード
                editNodeRef = new KifuNodeImpl(nextSasite, new KyokumenWrapper(ittesasuReference.Susunda_Sky_orNull));//, genTebanside
            }


            string nextSasiteStr = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(nextSasite);



            ittesasuReference.Set_SyuryoNode_OrNull = editNodeRef;// この変数を返すのがポイント。棋譜とは別に、現局面。
        }

        /// <summary>
        /// 棋譜ツリーのカレントを変更します。
        /// </summary>
        /// <param name="kifu_mutable"></param>
        /// <param name="nextSasiteStr"></param>
        /// <param name="edit_childNode_Ref"></param>
        /// <param name="errH"></param>
        public static void After3_ChangeCurrent(
            KifuTree kifu_mutable,
            string nextSasiteStr,
            Node<Starbeamable, KyokumenWrapper> edit_childNode_Ref,
            KwErrorHandler errH
            )
        {

            if (!((KifuNode)kifu_mutable.CurNode).HasTuginoitte(nextSasiteStr))
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

            Node<Starbeamable, KyokumenWrapper> temp = kifu_mutable.CurNode;
            kifu_mutable.SetCurNode( edit_childNode_Ref);//次ノードを、これからのカレントとします。
            edit_childNode_Ref.SetParentNode( temp);
        }



        /// <summary>
        /// 動かす駒を移動先へ。
        /// </summary>
        /// <param name="figMovedKoma"></param>
        /// <param name="sasite">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="obsoluted_kifu_mutable"></param>
        /// <param name="isMakimodosi"></param>
        private static void Do24_UgokasuKoma_IdoSakiHe(
            out Finger figMovedKoma,
            Starbeamable sasite,
            Playerside kaisi_tebanside,
            SkyConst kaisi_Sky,
            KwErrorHandler errH,
            string hint
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            int exceptionArea = 0;

            try
            {
                exceptionArea = 99001000;
                figMovedKoma = Fingers.Error_1;

                //------------------------------------------------------------
                // 選択  ：  動かす駒
                //------------------------------------------------------------
                // 進むとき
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                //Debug.Assert(null != sasite, "Sasu24_UgokasuKoma_IdoSakiHe: 指し手がヌルでした。");
                if (Util_Sky_BoolQuery.IsDaAction(sasite))// 多分、ここで sasite がヌルになるエラーがある☆
                {
                    //----------
                    // 駒台から “打”
                    //----------
                    exceptionArea = 99002000;

                    RO_Star srcKoma = Util_Starlightable.AsKoma(sasite.LongTimeAgo);
                    RO_Star dstKoma = Util_Starlightable.AsKoma(sasite.Now);


                    exceptionArea = 99002100;
                    // FIXME: 駒台の、どの駒を拾うか？
                    figMovedKoma = Util_Sky_FingerQuery.InOkibaSyuruiNow_IgnoreCase(
                        kaisi_Sky,
                        Conv_SyElement.ToOkiba(srcKoma.Masu),
                        Util_Komahaiyaku184.Syurui(dstKoma.Haiyaku),
                        errH
                        );
                    Debug.Assert(figMovedKoma != Fingers.Error_1, "駒を動かせなかった？14");
                }
                else
                {
                    exceptionArea = 99003000;
                    //----------
                    // 将棋盤から
                    //----------

                    RO_Star srcKoma = Util_Starlightable.AsKoma(sasite.LongTimeAgo);
                    Debug.Assert( !Masu_Honshogi.IsErrorBasho( srcKoma.Masu), "srcKoma.Masuエラー。15");
                    RO_Star dstKoma = Util_Starlightable.AsKoma(sasite.Now);


                    exceptionArea = 99003100;
                    figMovedKoma = Util_Sky_FingerQuery.InShogibanMasuNow(
                        kaisi_Sky,
                        dstKoma.Pside,
                        Util_Masu10.OkibaSujiDanToMasu(
                            Conv_SyElement.ToOkiba(Masu_Honshogi.Masus_All[Conv_SyElement.ToMasuNumber(dstKoma.Masu)]),
                            Conv_SyElement.ToMasuNumber(srcKoma.Masu)
                            ),
                            errH
                            );
                    Debug.Assert(figMovedKoma != Fingers.Error_1, "駒を動かせなかった？13");
                }
            }
            catch(Exception ex)
            {
                //>>>>> エラーが起こりました。

                // どうにもできないので  ログだけ取って無視します。
                errH.DonimoNaranAkirameta(ex, "Util_IttesasuRoutine#Sasu24_UgokasuKoma_IdoSakiHe： exceptionArea=" + exceptionArea+"\n"+
                    "hint=["+hint+"]");
                throw ex;
            }
        }



        /// <summary>
        /// [巻戻し]時の、駒台にもどる動きを吸収。
        /// </summary>
        /// <param name="syurui2"></param>
        /// <param name="sasite">棋譜に記録するために「指す前／指した後」を含めた手。</param>
        /// <param name="kifu"></param>
        /// <param name="isMakimodosi"></param>
        /// <returns></returns>
        private static Starlight Do36_KomaOnDestinationMasu(
            Komasyurui14 syurui2,
            Starbeamable sasite,
            SkyConst src_Sky)
        {
            Starlight dst;

            RO_Star srcKoma = Util_Starlightable.AsKoma(sasite.LongTimeAgo);//移動元
            RO_Star dstKoma = Util_Starlightable.AsKoma(sasite.Now);//移動先

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
            Starlight dst,
            SkyConst susunda_Sky_orNull_before,//駒を取られたとき、局面を変更します。
            out Finger out_figFoodKoma,
            out RO_Star out_food_koma,
            out Playerside pside,
            out SyElement akiMasu,
            KwErrorHandler errH
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
                if (null != errH.Dlgt_OnLog1Append_or_Null)
                {
                    errH.Dlgt_OnLog1Append_or_Null("駒取った=" + out_food_koma.Komasyurui + Environment.NewLine);
                }
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

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("エラー：　先後がおかしいです。");
                            sb.AppendLine("dst.Pside=" + dstKoma.Pside);
                            throw new Exception(sb.ToString());
                        }
                }

                //
                // 駒台に駒を置く動き
                //
                {
                    // 駒台の空きスペース
                    akiMasu = Util_IttesasuRoutine.GetKomadaiKomabukuroSpace(okiba, susunda_Sky_orNull_before);


                    if (Masu_Honshogi.IsErrorBasho( akiMasu))
                    {
                        //>>>>> エラー：　駒台に空きスペースがありませんでした。

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("エラー：　駒台に空きスペースがありませんでした。");
                        sb.AppendLine("駒台=" + Okiba.Gote_Komadai);
                        throw new Exception(sb.ToString());
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


            src_Sky.Foreach_Starlights((Finger finger, Starlight komaP, ref bool toBreak) =>
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
            for (int i = 0; i < Util_Masu10.KOMADAI_KOMABUKURO_SPACE_LENGTH;i++ )
            {
                if (!exists[i])
                {
                    akiMasu = Masu_Honshogi.Masus_All[i + Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba))];
                    goto gt_EndMethod;
                }
            }

        gt_EndMethod:

            //System.C onsole.WriteLine("ゲット駒台駒袋スペース＝" + akiMasu);

            return akiMasu;
        }


    }


}
