// アルファ法のデバッグ出力をする場合。
//#define DEBUG_ALPHA_METHOD

using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P222_Log_Kaisetu.L250____Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P354_Util_SasuEx.L500____Util;
using Grayscale.P360_Conv_Sasu__.L500____Converter;
using Grayscale.P362_LegalMove__.L500____Util;
using Grayscale.P266_KyokumMoves.L500____Util;
using Grayscale.P542_Scoreing___.L___250_Args;
using Grayscale.P542_Scoreing___.L500____Util;
using Grayscale.P551_Tansaku____.L___500_Tansaku;
using Grayscale.P551_Tansaku____.L500____Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
using Grayscale.P353_Conv_SasuEx.L500____Converter;
using Grayscale.P202_GraphicLog_.L500____Util;
using Grayscale.P554_TansaFukasa.L___500_Struct;


#if DEBUG
using Grayscale.P266_KyokumMoves.L250____Log;
using Grayscale.P370_LogGraphiEx.L500____Util;
#endif

namespace Grayscale.P554_TansaFukasa.L500____Struct
{

    /// <summary>
    /// 探索ルーチン
    /// </summary>
    public class Tansaku_FukasaYusen_Routine : Tansaku_Routine
    {

        public Tansaku_Genjo CreateGenjo(
            KifuTree kifu,
            bool isHonshogi,
            Mode_Tansaku mode_Tansaku,
            KwErrorHandler errH
            )
        {
            // TODO:ここではログを出力せずに、ツリーの先端で出力したい。
            KaisetuBoards logF_moveKiki = new KaisetuBoards();

            // TODO:「読む」と、ツリー構造が作成されます。
            //int[] yomuLimitter = new int[]{
            //    600, // 読みの1手目の横幅   // 王手回避漏れのために、合法手全読み(約600)は必要です。
            //    100, // 読みの2手目の横幅
            //    100, // 読みの3手目の横幅
            //    //2, // 読みの4手目の横幅
            //    //1 // 読みの5手目の横幅
            //};

            //// ↓これなら１手１秒で指せる☆
            //int[] yomuLimitter = new int[]{
            //    600, // 読みの1手目の横幅   // 王手回避漏れのために、合法手全読み(約600)は必要です。
            //    150, // 読みの2手目の横幅
            //    150, // 読みの3手目の横幅
            //    //2 // 読みの4手目の横幅
            //    //1 // 読みの5手目の横幅
            //};

            //int[] yomuLimitter = new int[]{
            //    600, // 読みの1手目の横幅   // 王手回避漏れのために、合法手全読み(約600)は必要です。
            //    600, // 読みの2手目の横幅
            //    600, // 読みの3手目の横幅
            //};

            //ok
            //int[] yomuLimitter = new int[]{
            //    0,   // 現局面は無視します。
            //    600, // 読みの1手目の横幅   // 王手回避漏れのために、合法手全読み(約600)は必要です。
            //    600, // 読みの2手目の横幅
            //};

            int[] yomuLimitter;
#if DEBUG
            if (mode_Tansaku == Mode_Tansaku.Learning)
            {
                // 学習モードでは、スピード優先で、2手の読みとします。

                // ２手の読み。
                yomuLimitter = new int[]{
                    0,   // 現局面は無視します。
                    600, // 読みの1手目の横幅   // 王手回避漏れのために、１手目は、合法手全読み(約600)は必要です。
                    600, // 読みの2手目の横幅
                };
            }
            else
            {
                /*
                // ２手の読み。
                yomuLimitter = new int[]{
                    0,   // 現局面は無視します。
                    600, // 読みの1手目の横幅   // 王手回避漏れのために、１手目は、合法手全読み(約600)は必要です。
                    600, // 読みの2手目の横幅
                };
                // */

                //*
                // ３手の読み。
                yomuLimitter = new int[]{
                    0,   // 現局面は無視します。
                    600, // 読みの1手目の横幅   // 王手回避漏れのために、１手目は、合法手全読み(約600)は必要です。
                    600, // 読みの2手目の横幅
                    600, // 読みの3手目の横幅
                };
                // */
            }
#else
            if (mode_Tansaku == Mode_Tansaku.Learning)
            {
                //System.Windows.Forms.MessageBox.Show("学習モード");
                // 学習モードでは、スピード優先で、2手の読みとします。

                //* // ２手の読み。
                yomuLimitter = new int[]{
                    0,   // 現局面は無視します。
                    600, // 読みの1手目の横幅   // 王手回避漏れのために、１手目は、合法手全読み(約600)は必要です。
                    600, // 読みの2手目の横幅
                };
                // */

                /* // ３手の読み。
                yomuLimitter = new int[]{
                    0,   // 現局面は無視します。
                    600, // 読みの1手目の横幅   // 王手回避漏れのために、１手目は、合法手全読み(約600)は必要です。
                    600, // 読みの2手目の横幅
                    600, // 読みの3手目の横幅
                };
                // */

            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("本番モード");
                /* // ２手の読み。
                yomuLimitter = new int[]{
                    0,   // 現局面は無視します。
                    600, // 読みの1手目の横幅   // 王手回避漏れのために、１手目は、合法手全読み(約600)は必要です。
                    600, // 読みの2手目の横幅
                };
                // */

                //* // ３手の読み。
                yomuLimitter = new int[]{
                    0,   // 現局面は無視します。
                    600, // 読みの1手目の横幅   // 王手回避漏れのために、１手目は、合法手全読み(約600)は必要です。
                    600, // 読みの2手目の横幅
                    600, // 読みの3手目の横幅
                };
                // */

                /* // ４手の読み。               
                yomuLimitter = new int[]{
                    0,   // 現局面は無視します。
                    600, // 読みの1手目の横幅   // 王手回避漏れのために、１手目は、合法手全読み(約600)は必要です。
                    600, // 読みの2手目の横幅
                    600, // 読みの3手目の横幅
                    600, // 読みの4手目の横幅
                };
                // */
            }
#endif


            Tansaku_Args args = new Tansaku_ArgsImpl(isHonshogi, yomuLimitter, logF_moveKiki);
            Tansaku_Genjo genjo = new Tansaku_GenjoImpl(
                ((KifuNode)kifu.CurNode).Value.KyokumenConst.Temezumi,
                args
                );

            return genjo;
        }

        /// <summary>
        /// 読む。
        /// 
        /// 棋譜ツリーを作成します。
        /// </summary>
        /// <param name="kifu">この棋譜ツリーの現局面に、次局面をぶら下げて行きます。</param>
        /// <param name="enableLog"></param>
        /// <param name="isHonshogi"></param>
        /// <param name="logTag"></param>
        /// <returns></returns>
        public void WAA_Yomu_Start(
            KifuTree kifu,
            bool isHonshogi,
            Mode_Tansaku mode_Tansaku,
            float alphabeta_otherBranchDecidedValue,
            EvaluationArgs args,
            KwErrorHandler errH
            )
        {
            int exceptionArea = 0;

            try
            {
                exceptionArea = 10;
                Tansaku_Genjo genjo = this.CreateGenjo(kifu, isHonshogi, mode_Tansaku, errH);
                KifuNode node_yomi = (KifuNode)kifu.CurNode;
                int wideCount2 = 0;

                // 
                // （１）合法手に一対一対応した子ノードを作成し、ハブ・ノードにぶら下げます。
                //
                Dictionary<string, SasuEntry> sasitebetuEntry;
                int yomiDeep;
                float a_childrenBest;
                Tansaku_FukasaYusen_Routine.CreateEntries_BeforeLoop(
                    genjo,
                    node_yomi,
                    out sasitebetuEntry,
                    out yomiDeep,
                    out a_childrenBest,
                    errH
                    );
                int sasitebetuEntry_count = sasitebetuEntry.Count;

                if (Tansaku_FukasaYusen_Routine.CanNotNextLoop(yomiDeep, wideCount2, sasitebetuEntry_count, genjo))
                {
                    // 1手も読まないのなら。
                    // FIXME: エラー？
                    //----------------------------------------
                    // もう深くよまないなら
                    //----------------------------------------
                    Tansaku_FukasaYusen_Routine.Do_Leaf(
                        genjo,
                        node_yomi,
                        args,
                        out a_childrenBest,
                        errH
                        );
                }
                else
                {
                }



                float child_bestScore = Tansaku_FukasaYusen_Routine.WAAA_Yomu_Loop(
                    genjo,
                    alphabeta_otherBranchDecidedValue,
                    node_yomi,
                    sasitebetuEntry.Count,
                    args,
                    errH
                    );

#if DEBUG
                exceptionArea = 20;
                if (0 < genjo.Args.LogF_moveKiki.boards.Count)//ﾛｸﾞが残っているなら
                {
                    ////
                    //// ログの書き出し
                    ////
                    //Util_GraphicalLog.WriteHtml5(
                    //    true,//enableLog,
                    //    "SasiteRoutine#Yomi_NextNodes(00)新ログ",
                    //    "[" + Util_GraphicalLog.BoardFileLog_ToJsonStr(genjo.Args.LogF_moveKiki) + "]"
                    //);

                    // 書き出した分はクリアーします。
                    genjo.Args.LogF_moveKiki.boards.Clear();
                }
#endif
            }
            catch (Exception ex)
            {
                switch (exceptionArea)
                {
                    case 10:
                        {
                            //>>>>> エラーが起こりました。
                            string message = ex.GetType().Name + " " + ex.Message + "：棋譜ツリーの読みの中盤５０です。：";
                            Debug.Fail(message);

                            // どうにもできないので  ログだけ取って、上に投げます。
                            errH.Logger.WriteLine_Error(message);
                            throw ex;
                        }
#if DEBUG
                    case 20:
                        {
                            errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みの後半９０です。"); throw ex;
                        }
#endif
                    default: throw ex;
                }
            }
        }


        public static bool CanNotNextLoop(
            int yomiDeep,
            int wideCount2,
            int sasitebetuEntry_count,
            Tansaku_Genjo genjo
            )
        {
            return (genjo.Args.YomuLimitter.Length <= yomiDeep + 1)//読みの深さ制限を超えているとき。
                || //または、
                (1 < yomiDeep && genjo.Args.YomuLimitter[yomiDeep] < wideCount2)//読みの１手目より後で、指定の横幅を超えているとき。
                || //または、
                (sasitebetuEntry_count < 1)//合法手がないとき
                ;
        }

        /// <summary>
        /// ループに入る前に。
        /// </summary>
        /// <param name="genjo"></param>
        /// <param name="node_yomi"></param>
        /// <param name="out_sasitebetuEntry"></param>
        /// <param name="out_yomiDeep"></param>
        /// <param name="out_a_childrenBest"></param>
        /// <param name="errH"></param>
        private static void CreateEntries_BeforeLoop(
            Tansaku_Genjo genjo,
            KifuNode node_yomi,
            out Dictionary<string, SasuEntry> out_sasitebetuEntry,
            out int out_yomiDeep,
            out float out_a_childrenBest,
            KwErrorHandler errH
            )
        {
            out_sasitebetuEntry = Tansaku_FukasaYusen_Routine.WAAAA_Create_ChildNodes(
                genjo,
                node_yomi.Key,
                node_yomi.Value.KyokumenConst,
                errH);

            out_yomiDeep = node_yomi.Value.KyokumenConst.Temezumi - genjo.YomikaisiTemezumi + 1;


            //--------------------------------------------------------------------------------
            // ↓↓↓↓アルファベータ法の準備
            //--------------------------------------------------------------------------------
            out_a_childrenBest = Util_Scoreing.Initial_BestScore(node_yomi.Value.KyokumenConst);// プレイヤー1ならmax値、プレイヤー2ならmin値。
            //--------------------------------------------------------------------------------
            // ↑↑↑↑アルファベータ法の準備
            //--------------------------------------------------------------------------------
        }

        /// <summary>
        /// もう深く読まない場合の処理。
        /// </summary>
        private static void Do_Leaf(
            Tansaku_Genjo genjo,
            KifuNode node_yomi,
            EvaluationArgs args,
            out float out_a_childrenBest,
            KwErrorHandler errH
            )
        {
            // 局面に評価値を付けます。
            Util_Scoreing.DoScoreing_Kyokumen(
                node_yomi,//mutable
                args,
                errH
                );
            // 局面の評価値。
            out_a_childrenBest = node_yomi.Score;

#if DEBUG_ALPHA_METHOD
                    errH.Logger.WriteLine_AddMemo("1. 手(" + node_yomi.Value.ToKyokumenConst.Temezumi + ")読(" + yomiDeep + ") 兄弟最善=[" + a_siblingDecidedValue + "] 子ベスト=[" + a_childrenBest + "]");
#endif

#if DEBUG
                bool enableLog = false;
                //
                // ログの書き出し
                //
                Util_GraphicalLog.WriteHtml5(
                    enableLog,
                    "指し手生成ログA",
                    "[" + Conv_KaisetuBoards.ToJsonStr(genjo.Args.LogF_moveKiki) + "]"
                );
            // 書き出した分はクリアーします。
            genjo.Args.LogF_moveKiki.boards.Clear();
#endif

            //#if DEBUG
            //                    //
            //                    // 盤１個分のログの準備
            //                    //
            //                    Util_LogBuilder510.Build_LogBoard(
            //                        nodePath,
            //                        niniNode,
            //                        kifu_forAssert,
            //                        reportEnvironment,
            //                        logF_kiki,
            //                        errH
            //                    );
            //#endif
        }

        /// <summary>
        /// 棋譜ツリーに、ノードを追加していきます。再帰します。
        /// </summary>
        /// <param name="genjo"></param>
        /// <param name="alphabeta_otherBranchDecidedValue"></param>
        /// <param name="args"></param>
        /// <param name="errH"></param>
        /// <returns>子の中で最善の点</returns>
        private static float WAAA_Yomu_Loop(
            Tansaku_Genjo genjo,
            float a_parentsiblingDecidedValue,
            KifuNode node_yomi,
            int sasitebetuEntry_count,
            EvaluationArgs args,
            KwErrorHandler errH
            )
        {
            int exceptionArea = 0;
            float a_childrenBest;

            try
            {
                exceptionArea = 10;
                //
                // まず前提として、
                // 現手番の「被王手の局面」だけがピックアップされます。
                // これはつまり、次の局面がないときは、その枝は投了ということです。
                //




                // 
                // （１）合法手に一対一対応した子ノードを作成し、ハブ・ノードにぶら下げます。
                //
                Dictionary<string, SasuEntry> sasitebetuEntry2;
                int yomiDeep2;
                Tansaku_FukasaYusen_Routine.CreateEntries_BeforeLoop(
                    genjo,
                    node_yomi,
                    out sasitebetuEntry2,
                    out yomiDeep2,
                    out a_childrenBest,
                    errH
                    );

                int wideCount1 = 0;
                foreach (KeyValuePair<string, SasuEntry> entry in sasitebetuEntry2)
                {
                    if (Tansaku_FukasaYusen_Routine.CanNotNextLoop(
                        yomiDeep2,
                        wideCount1,
                        sasitebetuEntry2.Count,
                        genjo
                    ))
                    {
                        //----------------------------------------
                        // もう深くよまないなら
                        //----------------------------------------
                        Tansaku_FukasaYusen_Routine.Do_Leaf(
                            genjo,
                            node_yomi,
                            args,
                            out a_childrenBest,
                            errH
                            );

                        wideCount1++;
                        break;
                    }
                    else
                    {
                        //----------------------------------------
                        // 《９》まだ深く読むなら
                        //----------------------------------------
                        // 《８》カウンターを次局面へ


                        // このノードは、途中節か葉か未確定。

                        //
                        // （２）指し手を、ノードに変換し、現在の局面に継ぎ足します。
                        //
                        KifuNode childNode1;

                        if (node_yomi.ContainsKey_ChildNodes(entry.Key))
                        {
                            childNode1 = (KifuNode)node_yomi.GetChildNode(entry.Key);
                        }
                        else
                        {
                            // 既存でなければ、作成・追加
                            childNode1 = Conv_SasuEntry.ToKifuNode(entry.Value, node_yomi.Value.KyokumenConst, errH);
                            node_yomi.PutAdd_ChildNode(entry.Key, childNode1);
                        }

                        // これを呼び出す回数を減らすのが、アルファ法。
                        // 枝か、葉か、確定させにいきます。
                        float a_myScore = Tansaku_FukasaYusen_Routine.WAAA_Yomu_Loop(
                            genjo,
                            a_childrenBest,
                            childNode1,
                            sasitebetuEntry2.Count,
                            args,
                            errH);
                        Util_Scoreing.Update_Branch(
                            a_myScore,//a_childrenBest,
                            node_yomi//mutable
                            );

                        //----------------------------------------
                        // 子要素の検索が終わった時点
                        //----------------------------------------
                        bool alpha_cut;
                        Util_Scoreing.Update_BestScore_And_Check_AlphaCut(
                            yomiDeep2,// yomiDeep0,
                            node_yomi,
                            a_parentsiblingDecidedValue,
                            a_myScore,
                            ref a_childrenBest,
                            out alpha_cut
                            );

                        wideCount1++;

#if DEBUG_ALPHA_METHOD
                errH.Logger.WriteLine_AddMemo("3. 手(" + node_yomi.Value.ToKyokumenConst.Temezumi + ")読(" + yomiDeep + ") 兄弟最善=[" + a_siblingDecidedValue + "] 子ベスト=[" + a_childrenBest + "] 自点=[" + a_myScore + "]");
#endif
                        if (alpha_cut)
                        {
#if DEBUG_ALPHA_METHOD
                        errH.Logger.WriteLine_AddMemo("アルファ・カット☆！");
#endif
                            //----------------------------------------
                            // 次の「子の弟」要素はもう読みません。
                            //----------------------------------------

                            //*TODO:
                            break;
                            //toBreak1 = true;
                            // */
                        }
                    }


                gt_NextLoop:
                    ;
                }


            }
            catch (Exception ex)
            {
                switch (exceptionArea)
                {
                    case 10:
                        {
                            errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの前半１０です。"); throw ex;
                        }
                    case 20:
                        {
                            errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの前半２０です。"); throw ex;
                        }
                    case 30:
                        {
                            errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの後半７０です。"); throw ex;
                        }
                    default: throw ex;
                }
            }

            return a_childrenBest;
        }

        /// <summary>
        /// 指し手をぶら下げます。
        /// 
        /// ぶらさがるのは、現手番から見た「被王手の次の一手の局面」だけです。
        /// ぶらさがりがなければ、「投了」を選んでください。
        /// </summary>
        /// <param name="enableLog"></param>
        /// <param name="isHonshogi"></param>
        /// <param name="yomuDeep"></param>
        /// <param name="pside_yomiCur"></param>
        /// <param name="node_yomiCur"></param>
        /// <param name="logF_moveKiki"></param>
        /// <param name="logTag"></param>
        /// <returns>複数のノードを持つハブ・ノード</returns>
        private static Dictionary<string, SasuEntry> WAAAA_Create_ChildNodes(
            Tansaku_Genjo genjo,
            Starbeamable src_Sky_sasite,
            SkyConst src_Sky,
            KwErrorHandler errH
            )
        {
            int exceptionArea = 0;

            //----------------------------------------
            // ハブ・ノードとは
            //----------------------------------------
            //
            // このハブ・ノード自身は空っぽで、ハブ・ノードの次ノードが、次局面のリストになっています。
            //
            Dictionary<string, SasuEntry> sasitebetuEntry;

            try
            {
                //----------------------------------------
                // ①現手番の駒の移動可能場所_被王手含む
                //----------------------------------------
                exceptionArea = 10;

                //----------------------------------------
                // 盤１個分のログの準備
                //----------------------------------------
                exceptionArea = 20;
#if DEBUG
                MmLogGenjoImpl mm_log_orNull = null;
                KaisetuBoard logBrd_move1;
                Tansaku_FukasaYusen_Routine.Log1(genjo, src_Sky_sasite, src_Sky, out mm_log_orNull, out logBrd_move1, errH);
#endif

                //----------------------------------------
                // 進めるマス
                //----------------------------------------
                List_OneAndMulti<Finger, SySet<SyElement>> komaBETUSusumeruMasus;
                Util_KyokumenMoves.LA_Split_KomaBETUSusumeruMasus(
                    1,
                    out komaBETUSusumeruMasus,
                    genjo.Args.IsHonshogi,//本将棋か
                    src_Sky,//現在の局面  // FIXME:Lockすると、ここでヌルになる☆
                    src_Sky.KaisiPside,//手番
                    false//相手番か
#if DEBUG
                    ,
                    mm_log_orNull
#endif
                );

//#if DEBUG
//                System.Console.WriteLine("komaBETUSusumeruMasusの全要素＝" + Util_List_OneAndMultiEx<Finger, SySet<SyElement>>.CountAllElements(komaBETUSusumeruMasus));
//#endif
//#if DEBUG
//                string jsaSasiteStr = Util_Translator_Sasite.ToSasite(genjo.Node_yomiNext, genjo.Node_yomiNext.Value, errH);
//                System.Console.WriteLine("[" + jsaSasiteStr + "]の駒別置ける升 調べ\n" + Util_List_OneAndMultiEx<Finger, SySet<SyElement>>.Dump(komaBETUSusumeruMasus, genjo.Node_yomiNext.Value.ToKyokumenConst));
//#endif
                //Sasiteseisei_FukasaYusen_Routine.Log2(genjo, logBrd_move1, errH);//ログ試し




                //----------------------------------------
                // ②利きから、被王手の局面を除いたハブノード
                //----------------------------------------
                if (genjo.Args.IsHonshogi)
                {
                    //----------------------------------------
                    // 本将棋
                    //----------------------------------------

                    exceptionArea = 30;
                    //----------------------------------------
                    // 指定局面での全ての指し手。
                    //----------------------------------------
                    Maps_OneAndMulti<Finger, Starbeamable> komaBETUAllSasites = Conv_KomabetuSusumeruMasus.ToKomaBETUAllSasites(
                        komaBETUSusumeruMasus, src_Sky);

//#if DEBUG
//                    System.Console.WriteLine("komaBETUAllSasitesの全要素＝" + Util_Maps_OneAndMultiEx<Finger, SySet<SyElement>>.CountAllElements(komaBETUAllSasites));
//#endif


                    //----------------------------------------
                    // 本将棋の場合、王手されている局面は削除します。
                    //----------------------------------------
                    Maps_OneAndOne<Finger, SySet<SyElement>> starbetuSusumuMasus = Util_LegalMove.LA_RemoveMate(
                        genjo.YomikaisiTemezumi,
                        genjo.Args.IsHonshogi,
                        komaBETUAllSasites,//駒別の全ての指し手
                        src_Sky,
#if DEBUG
                        genjo.Args.LogF_moveKiki,//利き用
#endif
                        "読みNextルーチン",
                        errH);

                    exceptionArea = 40;

                    //----------------------------------------
                    // 『駒別升ズ』を、ハブ・ノードへ変換。
                    //----------------------------------------
                    //成り以外の手
                    sasitebetuEntry = Conv_KomabetuMasus.ToSasitebetuSky1(
                        starbetuSusumuMasus,
                        src_Sky,
                        errH
                    );

                    //----------------------------------------
                    // 成りの指し手を作成します。（拡張）
                    //----------------------------------------
                    //成りの手
                    Dictionary<string, SasuEntry> b_sasitebetuEntry = Util_SasuEx.CreateNariSasite(src_Sky,
                        sasitebetuEntry,
                        errH);

                    // マージ
                    foreach(KeyValuePair<string, SasuEntry> entry in b_sasitebetuEntry)
                    {
                        if (!sasitebetuEntry.ContainsKey(entry.Key))
                        {
                            sasitebetuEntry.Add(entry.Key, entry.Value);
                        }
                    }
                }
                else
                {
                    //----------------------------------------
                    // 本将棋じゃないもの
                    //----------------------------------------
                    exceptionArea = 50;

                    //----------------------------------------
                    // 駒別置ける升　→　指し手別局面
                    //----------------------------------------
                    //
                    // １対１変換
                    //
                    sasitebetuEntry = Conv_KomabetuMasus.KomabetuMasus_ToSasitebetuSky(
                        komaBETUSusumeruMasus,
                        src_Sky,
                        errH
                        );

//#if DEBUG
//                    System.Console.WriteLine("駒別置ける升="+komaBETUSusumeruMasus.Items.Count+"件。　指し手別局面="+ss.Count+"件。");
//                    Debug.Assert(komaBETUSusumeruMasus.Items.Count == ss.Count, "変換後のデータ件数が異なります。[" + komaBETUSusumeruMasus.Items.Count + "]→["+ss.Count+"]");
//#endif
                }

                exceptionArea = 0;

            }
            catch (Exception ex)
            {
                switch (exceptionArea)
                {
                    case 10:
                        {
                            errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの作成次ノードの前半１０です。"); throw ex;
                        }
                    case 20:
                        {
                            errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの作成次ノードの前半３０です。"); throw ex;
                        }
                    case 30:
                        {
                            errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの作成次ノードの中盤５０です。"); throw ex;
                        }
                    case 40:
                        {
                            errH.DonimoNaranAkirameta(ex, "王手局面除去後に成りの指し手を追加していた時です。"); throw ex;
                        }
                    case 50:
                        {
                            errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの作成次ノードの後半９０です。"); throw ex;
                        }
                    default: throw ex;
                }
            }


            return sasitebetuEntry;// result_hubNode;
        }
#if DEBUG
        private static void Log1(
            Tansaku_Genjo genjo,
            Starbeamable src_Sky_sasite,
            SkyConst src_Sky,
            out MmLogGenjoImpl out_mm_log,
            out KaisetuBoard out_logBrd_move1,
            KwErrorHandler errH
        )
        {
            out_logBrd_move1 = new KaisetuBoard();// 盤１個分のログの準備

            try
            {
                out_mm_log = new MmLogGenjoImpl(
                        genjo.YomikaisiTemezumi,
                        out_logBrd_move1,//ログ？
                        src_Sky.Temezumi,//手済み
                        src_Sky_sasite,//指し手
                        errH//ログ
                    );
            }
            catch (Exception ex)
            {
                errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの作成次ノードの前半２０です。"); throw ex;
            }
        }
        private static void Log2(
            Tansaku_Genjo genjo,
            KifuNode node_yomi,
            KaisetuBoard logBrd_move1,
            KwErrorHandler errH
        )
        {
            try
            {
                logBrd_move1.sasiteOrNull = node_yomi.Key;


                RO_Star srcKoma = Util_Starlightable.AsKoma(logBrd_move1.sasiteOrNull.LongTimeAgo);
                RO_Star dstKoma = Util_Starlightable.AsKoma(logBrd_move1.sasiteOrNull.Now);


                // ログ試し
                logBrd_move1.Arrow.Add(new Gkl_Arrow(Conv_SyElement.ToMasuNumber(srcKoma.Masu), Conv_SyElement.ToMasuNumber(dstKoma.Masu)));
                genjo.Args.LogF_moveKiki.boards.Add(logBrd_move1);
            }
            catch (Exception ex)
            {
                errH.DonimoNaranAkirameta(ex, "棋譜ツリーの読みループの作成次ノードの前半４０です。"); throw ex;
            }
        }
#endif
    }

}
