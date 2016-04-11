using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P005_Tushin_____.L500____Util;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P296_ConvJsa____.L500____Converter;
using Grayscale.P325_PnlTaikyoku.L___250_Struct;
using Grayscale.P341_Ittesasu___.L___250_OperationA;
using Grayscale.P341_Ittesasu___.L250____OperationA;
using Grayscale.P341_Ittesasu___.L500____UtilA;
using Grayscale.P355_KifuParserA.L___500_Parser;
using Grayscale.P355_KifuParserA.L500____Parser;
using Grayscale.P372_KyokuParser.L___500_Parser;
using Grayscale.P373_ConvStartpo.L500____Converter;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P461_Server_____.L250____Util
{


    public class Util_Functions_Server
    {
        /// <summary>
        /// 「棋譜ツリーのカレントノード」の差替え、
        /// および
        /// 「ＧＵＩ用局面データ」との同期。
        /// 
        /// (1) 駒をつまんでいるときに、マウスの左ボタンを放したとき。
        /// (2) 駒の移動先の升の上で、マウスの左ボタンを放したとき。
        /// (3) 成る／成らないダイアログボックスが出たときに、マウスの左ボタンを押下したとき。
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="newNode"></param>
        public static void SetCurNode_Srv(
            Model_Taikyoku model_Taikyoku,// Taikyokuの内容をManualへ移す。
            Model_Manual model_Manual,
            Node<Starbeamable, KyokumenWrapper> newNode,
            out string jsaFugoStr,
            KwErrorHandler errH
            )
        {
            Debug.Assert(null != newNode, "新規ノードがヌル。");

            model_Taikyoku.Kifu.SetCurNode(newNode);

            model_Manual.SetGuiSky(newNode.Value.KyokumenConst);
            model_Manual.GuiTemezumi = model_Taikyoku.Kifu.CurNode.Value.KyokumenConst.Temezumi;
            model_Manual.GuiPside = model_Taikyoku.Kifu.CurNode.Value.KyokumenConst.KaisiPside;

            jsaFugoStr = Conv_SasiteStr_Jsa.ToSasiteStr_Jsa(newNode, errH);
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// １手進む
        /// ************************************************************************************************************************
        /// 
        /// ＜棋譜読込用＞＜マウス入力非対応＞
        /// 
        /// 「棋譜並べモード」と「vsコンピューター対局」でも、これを使いますが、
        /// 「棋譜並べモード」では送られてくる SFEN が「position startpos moves 8c8d」とフルであるのに対し、
        /// 「vsコンピューター対局」では、送られてくる SFEN が「8c8d」だけです。
        /// 
        /// それにより、処理の流れが異なります。
        /// 
        /// </summary>
        public static bool ReadLine_TuginoItteSusumu_Srv(
            ref string inputLine,
            Model_Taikyoku model_Taikyoku,//SetCurNodeがある。[コマ送り][再生]などで使用。
            Model_Manual model_Manual,
            out bool toBreak,
            string hint,
            KwErrorHandler errH
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            //KwErrorHandler errH = OwataMinister.SERVER_KIFU_YOMITORI;

            bool successful = false;
            KifuParserA_Impl kifuParserA_Impl = new KifuParserA_Impl();
            KifuParserA_Result result = new KifuParserA_ResultImpl();
            KifuParserA_Genjo genjo = new KifuParserA_GenjoImpl( inputLine);

            try
            {

                if (kifuParserA_Impl.State is KifuParserA_StateA0_Document)
                {
                    // 最初はここ

#if DEBUG
                    errH.Logger.WriteLine_AddMemo("(^o^)... ...");
                    errH.Logger.WriteLine_AddMemo("ｻｲｼｮﾊｺｺ☆　：　" + memberName + "." + sourceFilePath + "." + sourceLineNumber);
#endif
                    inputLine = kifuParserA_Impl.Execute_Step(
                        ref result,
                        model_Taikyoku,
                        genjo,
                        errH
                        );

                    Debug.Assert(result.Out_newNode_OrNull == null, "ここでノードに変化があるのはおかしい。");

                    if (genjo.IsBreak())
                    {
                        goto gt_EndMethod;
                    }
                    // （１）position コマンドを処理しました。→startpos
                    // （２）日本式棋譜で、何もしませんでした。→moves
                }

                if (kifuParserA_Impl.State is KifuParserA_StateA1_SfenPosition)
                {
                    //------------------------------------------------------------
                    // このブロックでは「position ～ moves 」まで一気に(*1)を処理します。
                    //------------------------------------------------------------
                    //
                    //          *1…初期配置を作るということです。
                    // 

                    {
#if DEBUG
                        string message = "(^o^)ﾂｷﾞﾊ　ﾋﾗﾃ　ﾏﾀﾊ　ｼﾃｲｷｮｸﾒﾝ　ｦ　ｼｮﾘｼﾀｲ☆ inputLine=[" + inputLine + "]";
                        errH.Logger.WriteLine_AddMemo(message);
#endif

                        inputLine = kifuParserA_Impl.Execute_Step(
                            ref result,
                            model_Taikyoku,
                            genjo,
                            errH
                            );
                        Debug.Assert(result.Out_newNode_OrNull == null, "ここでノードに変化があるのはおかしい。");


                        if (genjo.IsBreak())
                        {
                            goto gt_EndMethod;
                        }
                    }


                    {
#if DEBUG
                        errH.Logger.WriteLine_AddMemo("(^o^)ﾂｷﾞﾊ　ﾑｰﾌﾞｽ　ｦ　ｼｮﾘｼﾀｲ☆");
#endif

                        inputLine = kifuParserA_Impl.Execute_Step(
                            ref result,
                            model_Taikyoku,
                            genjo,
                            errH
                            );
                        Debug.Assert(result.Out_newNode_OrNull == null, "ここでノードに変化があるのはおかしい。");


                        if (genjo.IsBreak())
                        {
                            goto gt_EndMethod;
                        }
                        // moves を処理しました。
                        // ここまでで、「position ～ moves 」といった記述が入力されていたとすれば、入力欄から削除済みです。
                    }


                    // →moves
                }

                //
                // 対COMP戦で関係があるのはここです。
                //
                if (kifuParserA_Impl.State is KifuParserA_StateA2_SfenMoves)
                {
#if DEBUG
                    errH.Logger.WriteLine_AddMemo("ﾂｷﾞﾊ　ｲｯﾃ　ｼｮﾘｼﾀｲ☆");
#endif

                    inputLine = kifuParserA_Impl.Execute_Step(
                        ref result,
                        model_Taikyoku,
                        genjo,
                        errH
                        );

                    if (null != result.Out_newNode_OrNull)
                    {
                        string jsaFugoStr;
                        Util_Functions_Server.SetCurNode_Srv(model_Taikyoku, model_Manual, result.Out_newNode_OrNull, out jsaFugoStr, errH);
                    }

                    if (genjo.IsBreak())
                    {
                        goto gt_EndMethod;
                    }


                    // １手を処理した☆？
                }


                if (null != genjo.StartposImporter_OrNull)
                {
                    // 初期配置が平手でないとき。
                    // ************************************************************************************************************************
                    // SFENの初期配置の書き方(*1)を元に、駒を並べます。
                    // ************************************************************************************************************************
                    // 
                    //     *1…「position startpos moves 7g7f 3c3d 2g2f」といった書き方。
                    //     
                    ParsedKyokumen parsedKyokumen = Conv_StartposImporter.ToParsedKyokumen(
                        model_Manual,
                        genjo.StartposImporter_OrNull,//指定されているはず。
                        genjo,
                        errH
                        );

                    //------------------------------
                    // 駒の配置
                    //------------------------------
                    string jsaFugoStr;
                    Util_Functions_Server.SetCurNode_Srv(model_Taikyoku, model_Manual, parsedKyokumen.KifuNode, out jsaFugoStr, errH);// GUIに通知するだけ。

                    ////------------------------------
                    //// 駒を、駒袋から駒台に移動させます。
                    ////------------------------------
                    //model_Operating.Manual.SetGuiSky(
                    //    SkyConst.NewInstance(
                    //        parsedKyokumen.buffer_Sky,
                    //        -1//そのまま
                    //    )
                    //);
                }


                successful = true;
            }
            catch (Exception ex)
            {
                Util_Message.Show(ex.GetType().Name+"："+ ex.Message);
                toBreak = true;
                successful = false;
            }

        gt_EndMethod:
            toBreak = genjo.IsBreak();
            return successful;
        }






        /// <summary>
        /// ************************************************************************************************************************
        /// [巻戻し]ボタン
        /// ************************************************************************************************************************
        /// </summary>
        public static bool Makimodosi_Srv(
            out Finger movedKoma,
            out Finger foodKoma,
            out string jsaFugoStr,
            Model_Taikyoku model_Taikyoku,
            KwErrorHandler errH
            )
        {
            bool successful = false;

            //------------------------------
            // 棋譜から１手削ります
            //------------------------------
            Node<Starbeamable, KyokumenWrapper> removeeLeaf = model_Taikyoku.Kifu.CurNode;
            int korekaranoTemezumi = removeeLeaf.Value.KyokumenConst.Temezumi - 1;//１手前へ。

            if (removeeLeaf.IsRoot())
            {
                // ルート
                jsaFugoStr = "×";
                movedKoma = Fingers.Error_1;
                foodKoma = Fingers.Error_1;
                goto gt_EndMethod;
            }


            //------------------------------
            // 符号
            //------------------------------
            // [巻戻し]ボタン
            jsaFugoStr = Conv_SasiteStr_Jsa.ToSasiteStr_Jsa(removeeLeaf,errH);




            //------------------------------
            // 前の手に戻します
            //------------------------------
            IttemodosuResult ittemodosuResult = new IttemodosuResultImpl(Fingers.Error_1, Fingers.Error_1, null, Komasyurui14.H00_Null___, null);
            {
                //Ks14 foodKomaSyurui;//取った駒があれば、取った駒の種類。
                //SkyConst susunda_Sky_orNull;
                //
                // 一手巻き戻す
                //
                Util_IttemodosuRoutine.Before1(
                    new IttemodosuArgImpl(
                        model_Taikyoku.Kifu.CurNode,
                        removeeLeaf.Key,
                        korekaranoTemezumi
                    ),
                    out ittemodosuResult,
                    errH
                    );
                Util_IttemodosuRoutine.Before2(
                    ref ittemodosuResult,
                    errH
                    );
                Util_IttemodosuRoutine.After3_ChangeCurrent(
                    model_Taikyoku.Kifu
                    );
            }
            movedKoma = ittemodosuResult.FigMovedKoma;
            foodKoma = ittemodosuResult.FigFoodKoma;

            successful = true;


        gt_EndMethod:
            return successful;
        }





        /// <summary>
        /// ************************************************************************************************************************
        /// [コマ送り]ボタン
        /// ************************************************************************************************************************
        /// 
        /// vsコンピューター対局でも、タイマーによって[コマ送り]が実行されます。
        /// 
        /// </summary>
        public static bool Komaokuri_Srv(
            ref string inputLine,
            Model_Taikyoku model_Taikyoku,
            Model_Manual model_Manual,
            KwErrorHandler errH
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {

            if(""==inputLine)
            {
                goto gt_EndMethod;
            }

            bool toBreak = false;
            Util_Functions_Server.ReadLine_TuginoItteSusumu_Srv(
                ref inputLine,
                model_Taikyoku,//SetCurNodeがある。
                model_Manual,
                out toBreak,
                "hint",
                errH
                );

        gt_EndMethod:
            ;
            return true;
        }




        /// <summary>
        /// ************************************************************************************************************************
        /// 駒を動かします(1)。マウスボタンが押下されたとき。
        /// ************************************************************************************************************************
        /// 
        /// 成る、成らない関連。
        /// 
        /// </summary>
        public static void Komamove1a_50Srv(
            out bool torareruKomaAri,
            out RO_Star koma_Food_after,
            Starlight dst,
            Finger fig_btnTumandeiruKoma,
            RO_Star foodee_koma,//取られる対象の駒
            Model_Manual model_Manual,
            KwErrorHandler errH
            )
        {

            Finger btnKoma_Food_Koma;



            // 取られることになる駒のボタン
            btnKoma_Food_Koma = Util_Sky_FingersQuery.InMasuNow(model_Manual.GuiSkyConst, foodee_koma.Masu).ToFirst();
            if (Fingers.Error_1 == btnKoma_Food_Koma)
            {
                koma_Food_after = null;
                torareruKomaAri = false;
                btnKoma_Food_Koma = Fingers.Error_1;
                goto gt_EndBlock1;
            }
            else
            {
                //>>>>> 取る駒があったとき
                torareruKomaAri = true;
            }





            Komasyurui14 koma_Food_pre_Syurui = Util_Starlightable.AsKoma(model_Manual.GuiSkyConst.StarlightIndexOf(btnKoma_Food_Koma).Now).Komasyurui;


            // その駒は、駒置き場に移動させます。
            SyElement akiMasu;
            switch (foodee_koma.Pside)
            {
                case Playerside.P2:

                    akiMasu = Util_IttesasuRoutine.GetKomadaiKomabukuroSpace(Okiba.Gote_Komadai, model_Manual.GuiSkyConst);
                    if (!Masu_Honshogi.IsErrorBasho(akiMasu))
                    {
                        // 駒台に空きスペースがありました。
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        koma_Food_after = new RO_Star(
                            Playerside.P2,
                            akiMasu,//駒台へ
                            Util_Komasyurui14.NarazuCaseHandle(koma_Food_pre_Syurui)
                        );
                    }
                    else
                    {
                        // エラー：　駒台に空きスペースがありませんでした。
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                        koma_Food_after = new RO_Star(
                            Playerside.P2,
                            Util_Masu10.OkibaSujiDanToMasu(
                                Okiba.Gote_Komadai,
                                Util_Koma.CTRL_NOTHING_PROPERTY_SUJI,
                                Util_Koma.CTRL_NOTHING_PROPERTY_DAN
                            ),
                            Util_Komasyurui14.NarazuCaseHandle(koma_Food_pre_Syurui)
                        );
                    }

                    break;

                case Playerside.P1://thru
                default:

                    akiMasu = Util_IttesasuRoutine.GetKomadaiKomabukuroSpace(Okiba.Sente_Komadai, model_Manual.GuiSkyConst);
                    if (!Masu_Honshogi.IsErrorBasho(akiMasu))
                    {
                        // 駒台に空きスペースがありました。
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                        koma_Food_after = new RO_Star(
                            Playerside.P1,
                            akiMasu,//駒台へ
                            Util_Komasyurui14.NarazuCaseHandle(koma_Food_pre_Syurui)
                        );
                    }
                    else
                    {
                        // エラー：　駒台に空きスペースがありませんでした。
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                        koma_Food_after = new RO_Star(
                            Playerside.P1,
                            Util_Masu10.OkibaSujiDanToMasu(
                                Okiba.Sente_Komadai,
                                Util_Koma.CTRL_NOTHING_PROPERTY_SUJI,
                                Util_Koma.CTRL_NOTHING_PROPERTY_DAN
                            ),
                            Util_Komasyurui14.NarazuCaseHandle(koma_Food_pre_Syurui)
                        );
                    }

                    break;
            }



        gt_EndBlock1:


            SkyConst sky2;
            if (btnKoma_Food_Koma != Fingers.Error_1)
            {
                //------------------------------
                // 取られる駒があった場合
                //------------------------------
                sky2 = SkyConst.NewInstance_OverwriteOrAdd_Light(
                        model_Manual.GuiSkyConst,
                        -1,//そのまま
                        //
                        // 指した駒
                        //
                        fig_btnTumandeiruKoma,
                        dst,
                        //
                        // 取られた駒
                        //
                        btnKoma_Food_Koma,
                        new RO_Starlight(
                            koma_Food_after
                        )
                    );
            }
            else
            {
                //------------------------------
                // 取られる駒がなかった場合
                //------------------------------
                RO_Star movedKoma = Util_Starlightable.AsKoma(model_Manual.GuiSkyConst.StarlightIndexOf(fig_btnTumandeiruKoma).Now);

                sky2 = SkyConst.NewInstance_OverwriteOrAdd_Light(
                        model_Manual.GuiSkyConst,
                        -1,//そのまま
                        //
                        // 指した駒
                        //
                        fig_btnTumandeiruKoma,
                        dst,
                        //
                        // 手得計算
                        //
                        movedKoma.Komasyurui,
                        0,
                        movedKoma.Masu
                    );
            }

            model_Manual.SetGuiSky(sky2);
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
            // 棋譜は変更された。
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        }


    }


}
