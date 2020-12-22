// アルファ法のデバッグ出力をする場合。
//#define DEBUG_ALPHA_METHOD

namespace Grayscale.Kifuwarakaku.UseCases.Features
{
#if DEBUG
    using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
#else
    using System.Collections.Generic;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#endif

    /// <summary>
    /// 探索ルーチン
    /// </summary>
    public class Tansaku_FukasaYusen_Routine : Tansaku_Routine
    {

        public Tansaku_Genjo CreateGenjo(
            KifuTree kifu,
            bool isHonshogi,
            Mode_Tansaku mode_Tansaku
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
        /// <returns></returns>
        public void WAA_Yomu_Start(
            KifuTree kifu,
            bool isHonshogi,
            Mode_Tansaku mode_Tansaku,
            float alphabeta_otherBranchDecidedValue,
            EvaluationArgs args
            )
        {
            Tansaku_Genjo genjo = this.CreateGenjo(kifu, isHonshogi, mode_Tansaku);
            KifuNode node_yomi = (KifuNode)kifu.CurNode;
            int wideCount2 = 0;

            // 
            // （１）合法手に一対一対応した子ノードを作成し、ハブ・ノードにぶら下げます。
            //
            Dictionary<string, SasuEntry> moveBetuEntry;
            int yomiDeep;
            float a_childrenBest;
            Tansaku_FukasaYusen_Routine.CreateEntries_BeforeLoop(
                genjo,
                node_yomi,
                out moveBetuEntry,
                out yomiDeep,
                out a_childrenBest
                );
            int moveBetuEntry_count = moveBetuEntry.Count;

            if (Tansaku_FukasaYusen_Routine.CanNotNextLoop(yomiDeep, wideCount2, moveBetuEntry_count, genjo))
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
                    out a_childrenBest
                    );
            }
            else
            {
            }



            float child_bestScore = Tansaku_FukasaYusen_Routine.WAAA_Yomu_Loop(
                genjo,
                alphabeta_otherBranchDecidedValue,
                node_yomi,
                moveBetuEntry.Count,
                args
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
                    //    "MoveRoutine#Yomi_NextNodes(00)新ログ",
                    //    "[" + Util_GraphicalLog.BoardFileLog_ToJsonStr(genjo.Args.LogF_moveKiki) + "]"
                    //);

                    // 書き出した分はクリアーします。
                    genjo.Args.LogF_moveKiki.boards.Clear();
                }
#endif
        }


        public static bool CanNotNextLoop(
            int yomiDeep,
            int wideCount2,
            int moveBetuEntry_count,
            Tansaku_Genjo genjo
            )
        {
            return (genjo.Args.YomuLimitter.Length <= yomiDeep + 1)//読みの深さ制限を超えているとき。
                || //または、
                (1 < yomiDeep && genjo.Args.YomuLimitter[yomiDeep] < wideCount2)//読みの１手目より後で、指定の横幅を超えているとき。
                || //または、
                (moveBetuEntry_count < 1)//合法手がないとき
                ;
        }

        /// <summary>
        /// ループに入る前に。
        /// </summary>
        /// <param name="genjo"></param>
        /// <param name="node_yomi"></param>
        /// <param name="out_moveBetuEntry"></param>
        /// <param name="out_yomiDeep"></param>
        /// <param name="out_a_childrenBest"></param>
        private static void CreateEntries_BeforeLoop(
            Tansaku_Genjo genjo,
            KifuNode node_yomi,
            out Dictionary<string, SasuEntry> out_moveBetuEntry,
            out int out_yomiDeep,
            out float out_a_childrenBest
            )
        {
            out_moveBetuEntry = Tansaku_FukasaYusen_Routine.WAAAA_Create_ChildNodes(
                genjo,
                node_yomi.Key,
                node_yomi.Value.KyokumenConst);

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
            out float out_a_childrenBest
            )
        {
            // 局面に評価値を付けます。
            Util_Scoreing.DoScoreing_Kyokumen(
                node_yomi,//mutable
                args
                );
            // 局面の評価値。
            out_a_childrenBest = node_yomi.Score;

#if DEBUG_ALPHA_METHOD
                    logTag.Logger.WriteLineAddMemo("1. 手(" + node_yomi.Value.ToKyokumenConst.Temezumi + ")読(" + yomiDeep + ") 兄弟最善=[" + a_siblingDecidedValue + "] 子ベスト=[" + a_childrenBest + "]");
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
            //                        logTag
            //                    );
            //#endif
        }

        /// <summary>
        /// 棋譜ツリーに、ノードを追加していきます。再帰します。
        /// </summary>
        /// <param name="genjo"></param>
        /// <param name="alphabeta_otherBranchDecidedValue"></param>
        /// <param name="args"></param>
        /// <returns>子の中で最善の点</returns>
        private static float WAAA_Yomu_Loop(
            Tansaku_Genjo genjo,
            float a_parentsiblingDecidedValue,
            KifuNode node_yomi,
            int moveBetuEntry_count,
            EvaluationArgs args
            )
        {
            float a_childrenBest;

            //
            // まず前提として、
            // 現手番の「被王手の局面」だけがピックアップされます。
            // これはつまり、次の局面がないときは、その枝は投了ということです。
            //

            // 
            // （１）合法手に一対一対応した子ノードを作成し、ハブ・ノードにぶら下げます。
            //
            Dictionary<string, SasuEntry> moveBetuEntry2;
            int yomiDeep2;
            Tansaku_FukasaYusen_Routine.CreateEntries_BeforeLoop(
                genjo,
                node_yomi,
                out moveBetuEntry2,
                out yomiDeep2,
                out a_childrenBest
                );

            int wideCount1 = 0;
            foreach (KeyValuePair<string, SasuEntry> entry in moveBetuEntry2)
            {
                if (Tansaku_FukasaYusen_Routine.CanNotNextLoop(
                    yomiDeep2,
                    wideCount1,
                    moveBetuEntry2.Count,
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
                        out a_childrenBest
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
                        childNode1 = Conv_SasuEntry.ToKifuNode(entry.Value, node_yomi.Value.KyokumenConst);
                        node_yomi.PutAdd_ChildNode(entry.Key, childNode1);
                    }

                    // これを呼び出す回数を減らすのが、アルファ法。
                    // 枝か、葉か、確定させにいきます。
                    float a_myScore = Tansaku_FukasaYusen_Routine.WAAA_Yomu_Loop(
                        genjo,
                        a_childrenBest,
                        childNode1,
                        moveBetuEntry2.Count,
                        args);
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
                logTag.Logger.WriteLineAddMemo("3. 手(" + node_yomi.Value.ToKyokumenConst.Temezumi + ")読(" + yomiDeep + ") 兄弟最善=[" + a_siblingDecidedValue + "] 子ベスト=[" + a_childrenBest + "] 自点=[" + a_myScore + "]");
#endif
                    if (alpha_cut)
                    {
#if DEBUG_ALPHA_METHOD
                        logTag.Logger.WriteLineAddMemo("アルファ・カット☆！");
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


                // gt_NextLoop:
                //    ;
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
        /// <returns>複数のノードを持つハブ・ノード</returns>
        private static Dictionary<string, SasuEntry> WAAAA_Create_ChildNodes(
            Tansaku_Genjo genjo,
            IMove src_Sky_move,
            SkyConst src_Sky
            )
        {
            //----------------------------------------
            // ハブ・ノードとは
            //----------------------------------------
            //
            // このハブ・ノード自身は空っぽで、ハブ・ノードの次ノードが、次局面のリストになっています。
            //
            Dictionary<string, SasuEntry> moveBetuEntry;

            //----------------------------------------
            // ①現手番の駒の移動可能場所_被王手含む
            //----------------------------------------

            //----------------------------------------
            // 盤１個分のログの準備
            //----------------------------------------
#if DEBUG
                MmLogGenjoImpl mm_log_orNull = null;
                KaisetuBoard logBrd_move1;
                Tansaku_FukasaYusen_Routine.Log1(genjo, src_Sky_move, src_Sky, out mm_log_orNull, out logBrd_move1);
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
            //                Logger.Trace("komaBETUSusumeruMasusの全要素＝" + Util_List_OneAndMultiEx<Finger, SySet<SyElement>>.CountAllElements(komaBETUSusumeruMasus));
            //#endif
            //#if DEBUG
            //                string jsaMoveStr = Util_Translator_Move.ToMove(genjo.Node_yomiNext, genjo.Node_yomiNext.Value);
            //                Logger.Trace("[" + jsaMoveStr + "]の駒別置ける升 調べ\n" + Util_List_OneAndMultiEx<Finger, SySet<SyElement>>.Dump(komaBETUSusumeruMasus, genjo.Node_yomiNext.Value.ToKyokumenConst));
            //#endif
            //Moveseisei_FukasaYusen_Routine.Log2(genjo, logBrd_move1);//ログ試し




            //----------------------------------------
            // ②利きから、被王手の局面を除いたハブノード
            //----------------------------------------
            if (genjo.Args.IsHonshogi)
            {
                //----------------------------------------
                // 本将棋
                //----------------------------------------

                //----------------------------------------
                // 指定局面での全ての指し手。
                //----------------------------------------
                Maps_OneAndMulti<Finger, IMove> komaBetuAllMoves = Conv_KomabetuSusumeruMasus.ToKomaBetuAllMoves(
                    komaBETUSusumeruMasus, src_Sky);

                //#if DEBUG
                //                    Logger.Trace("komaBETUAllMovesの全要素＝" + Util_Maps_OneAndMultiEx<Finger, SySet<SyElement>>.CountAllElements(komaBETUAllMoves));
                //#endif


                //----------------------------------------
                // 本将棋の場合、王手されている局面は削除します。
                //----------------------------------------
                Maps_OneAndOne<Finger, SySet<SyElement>> starbetuSusumuMasus = Util_LegalMove.LA_RemoveMate(
                    genjo.YomikaisiTemezumi,
                    genjo.Args.IsHonshogi,
                    komaBetuAllMoves,//駒別の全ての指し手
                    src_Sky,
#if DEBUG
                        genjo.Args.LogF_moveKiki,//利き用
#endif
                        "読みNextルーチン");

                //----------------------------------------
                // 『駒別升ズ』を、ハブ・ノードへ変換。
                //----------------------------------------
                //成り以外の手
                moveBetuEntry = Conv_KomabetuMasus.ToMoveBetuSky1(
                    starbetuSusumuMasus,
                    src_Sky
                );

                //----------------------------------------
                // 成りの指し手を作成します。（拡張）
                //----------------------------------------
                //成りの手
                Dictionary<string, SasuEntry> b_moveBetuEntry = Util_SasuEx.CreateNariMove(src_Sky,
                    moveBetuEntry);

                // マージ
                foreach (KeyValuePair<string, SasuEntry> entry in b_moveBetuEntry)
                {
                    if (!moveBetuEntry.ContainsKey(entry.Key))
                    {
                        moveBetuEntry.Add(entry.Key, entry.Value);
                    }
                }
            }
            else
            {
                //----------------------------------------
                // 本将棋じゃないもの
                //----------------------------------------

                //----------------------------------------
                // 駒別置ける升　→　指し手別局面
                //----------------------------------------
                //
                // １対１変換
                //
                moveBetuEntry = Conv_KomabetuMasus.KomabetuMasusToMoveBetuSky(
                    komaBETUSusumeruMasus,
                    src_Sky
                    );

                //#if DEBUG
                //                    Logger.Trace("駒別置ける升="+komaBETUSusumeruMasus.Items.Count+"件。　指し手別局面="+ss.Count+"件。");
                //                    Debug.Assert(komaBETUSusumeruMasus.Items.Count == ss.Count, "変換後のデータ件数が異なります。[" + komaBETUSusumeruMasus.Items.Count + "]→["+ss.Count+"]");
                //#endif
            }

            return moveBetuEntry;// result_hubNode;
        }
#if DEBUG
        private static void Log1(
            Tansaku_Genjo genjo,
            IMove src_Sky_move,
            SkyConst src_Sky,
            out MmLogGenjoImpl out_mm_log,
            out KaisetuBoard out_logBrd_move1
        )
        {
            out_logBrd_move1 = new KaisetuBoard();// 盤１個分のログの準備

                out_mm_log = new MmLogGenjoImpl(
                        genjo.YomikaisiTemezumi,
                        out_logBrd_move1,//ログ？
                        src_Sky.Temezumi,//手済み
                        src_Sky_move,//指し手
                        logTag//ログ
                    );
        }
        private static void Log2(
            Tansaku_Genjo genjo,
            KifuNode node_yomi,
            KaisetuBoard logBrd_move1
        )
        {
                logBrd_move1.moveOrNull = node_yomi.Key;


                RO_Star srcKoma = Util_Starlightable.AsKoma(logBrd_move1.moveOrNull.LongTimeAgo);
                RO_Star dstKoma = Util_Starlightable.AsKoma(logBrd_move1.moveOrNull.Now);


                // ログ試し
                logBrd_move1.Arrow.Add(new Gkl_Arrow(Conv_SyElement.ToMasuNumber(srcKoma.Masu), Conv_SyElement.ToMasuNumber(dstKoma.Masu)));
                genjo.Args.LogF_moveKiki.boards.Add(logBrd_move1);
        }
#endif
    }

}
