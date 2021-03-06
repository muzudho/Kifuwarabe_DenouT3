﻿namespace Grayscale.Kifuwarakaku.Entities.Features
{
#if DEBUG
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Nett;
#else
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Grayscale.Kifuwarakaku.Entities.Configuration;
    using Nett;
#endif

    /// <summary>
    /// 棋譜ツリー・ログ・ライター
    /// </summary>
    public abstract class Util_KifuTreeLogWriter
    {
        /// <summary>
        /// このクラスを使う前にセットしてください。
        /// </summary>
        public static void Init(IEngineConf engineConf)
        {
            Util_KifuTreeLogWriter.REPORT_ENVIRONMENT = new KyokumenPngEnvironmentImpl(
                        Path.Combine(engineConf.LogDirectory, "_log_KifuTreeLog/"),//argsDic["outFolder"],
                        Path.Combine(engineConf.DataDirectory, "img/gkLog/"),//argsDic["imgFolder"],
                        engineConf.GetResourceBasename("Koma1PngBasename"),//argsDic["kmFile"],
                        engineConf.GetResourceBasename("Suji1PngBasename"),//argsDic["sjFile"],
                        "20",//argsDic["kmW"],
                        "20",//argsDic["kmH"],
                        "8",//argsDic["sjW"],
                        "12"//argsDic["sjH"]
                        );
        }

        public static KyokumenPngEnvironment REPORT_ENVIRONMENT;

        /// <summary>
        /// 棋譜ツリー・ログの書出し
        /// 
        /// TODO: フォルダーパスが長く成りすぎるのを、なんとかしたい。折り返すとか、～中略～にするとか、rootから始めないとか。
        /// </summary>
        public static void A_Write_KifuTreeLog(
            KaisetuBoards logF_kiki,
            KifuTree kifu
            )
        {
#if DEBUG
            int logFileCounter = 0;

            //----------------------------------------
            // 既存の棋譜ツリー・ログを空に。
            //----------------------------------------
            {
                string rootFolder = Path.Combine(Util_KifuTreeLogWriter.REPORT_ENVIRONMENT.OutFolder, ConvMoveStrSfen.KIFU_TREE_LOG_ROOT_FOLDER);
                if (Directory.Exists(rootFolder))
                {
                    try
                    {
                        Directory.Delete(rootFolder, true);
                    }
                    catch (IOException)
                    {
                        // ディレクトリーが空でなくて、ディレクトリーを削除できなかったときに
                        // ここにくるが、
                        // ディレクトリーの中は空っぽにできていたりする。
                        //
                        // とりあえず続行。
                    }
                }
            }

            //----------------------------------------
            // カレントノードまでの符号を使って、フォルダーパスを作成。
            //----------------------------------------
            StringBuilder tree_folder = new StringBuilder();
            kifu.ForeachHonpu(kifu.CurNode, (int temezumi2, KyokumenWrapper kWrap, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
            {
                tree_folder.Append($"{ConvMoveStrSfen.ToMoveStrSfenForFilename(node.Key)}/");
            });
            //sb_folder.Append( $"{Conv_MoveStr_Sfen.ToMoveStr_Sfen_ForFilename(kifu.CurNode.Key)}/");

            string moveText1 = ConvMoveStrSfen.ToMoveStrSfen(kifu.CurNode.Key);
            KifuNode kifuNode1 = (KifuNode)kifu.CurNode;

            // 評価明細のログ出力。
            Util_KifuTreeLogWriter.AA_Write_ForeachLeafs_ForDebug(
                ref logFileCounter,
                moveText1,
                kifuNode1,
                kifu,
                tree_folder.ToString(),
                Util_KifuTreeLogWriter.REPORT_ENVIRONMENT
                );

            if (0 < logF_kiki.boards.Count)//ﾛｸﾞが残っているなら
            {
                bool enableLog = true;// false;
                                      //
                                      // ログの書き出し
                                      //
                Util_GraphicalLog.WriteHtml5(
                    enableLog,
                    "#評価ログ",
                    $"[{Conv_KaisetuBoards.ToJsonStr(logF_kiki)}]"
                );

                // 書き出した分はクリアーします。
                logF_kiki.boards.Clear();
            }
#endif
        }

        /// <summary>
        /// 棋譜ツリーの、ノードに格納されている、局面評価明細を、出力していきます。
        /// </summary>
        public static void AA_Write_ForeachLeafs_ForDebug(
            IEngineConf engineConf,
            ref int logFileCounter,
            string nodePath,
            KifuNode node,
            KifuTree kifu,
            string treeFolder,
            KyokumenPngEnvironment reportEnvironment
            )
        {

            // 次ノードの有無
            if (0 < node.Count_ChildNodes)
            {
                // 葉ノードではないなら

                int logFileCounter_temp = logFileCounter;
                // 先に奥の枝から。
                node.Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> nextNode, ref bool toBreak) =>
                {

                    float score = ((KifuNode)nextNode).Score;

                    // 再帰
                    Util_KifuTreeLogWriter.AA_Write_ForeachLeafs_ForDebug(
                        engineConf,
                        ref logFileCounter_temp,
                        $"{nodePath} {ConvMoveStrSfen.ToMoveStrSfenForFilename(nextNode.Key)}",
                        (KifuNode)nextNode,
                        kifu,
                        $"{treeFolder}{(int)score}点_{ConvMoveStrSfen.ToMoveStrSfen(nextNode.Key)}/",
                        reportEnvironment
                        );

                });
                logFileCounter = logFileCounter_temp;
            }

            // 盤１個分の png 画像ログ出力
            Util_KifuTreeLogWriter.AAA_Write_Node(
                engineConf,
                ref logFileCounter,
                nodePath,
                node,
                kifu,
                treeFolder,
                reportEnvironment
            );

        }

        /// <summary>
        /// 盤１個分のログ。
        /// </summary>
        private static void AAA_Write_Node(
            IEngineConf engineConf,
            ref int logFileCounter,
            string nodePath,
            KifuNode node,
            KifuTree kifu,
            string relFolder,
            KyokumenPngEnvironment reportEnvironment
            )
        {
            string fileName = "";


            // 出力先
            fileName = Conv_Filepath.ToEscape($"_log_{((int)node.Score)}点_{logFileCounter}_{nodePath}.png");
            relFolder = Conv_Filepath.ToEscape(relFolder);
            //
            // 画像ﾛｸﾞ
            //
            if (true)
            {
                int srcMasu_orMinusOne = -1;
                int dstMasu_orMinusOne = -1;
                if (null != node.Key)
                {
                    srcMasu_orMinusOne = Conv_SyElement.ToMasuNumber(((RO_Star)node.Key.LongTimeAgo).Masu);
                    dstMasu_orMinusOne = Conv_SyElement.ToMasuNumber(((RO_Star)node.Key.Now).Masu);
                }

                KyokumenPngArgs_FoodOrDropKoma foodKoma;
                if (null != node.Key.FoodKomaSyurui)
                {
                    switch (Util_Komasyurui14.NarazuCaseHandle((PieceType)node.Key.FoodKomaSyurui))
                    {
                        case PieceType.None: foodKoma = KyokumenPngArgs_FoodOrDropKoma.NONE; break;
                        case PieceType.P: foodKoma = KyokumenPngArgs_FoodOrDropKoma.FU__; break;
                        case PieceType.L: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KYO_; break;
                        case PieceType.N: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KEI_; break;
                        case PieceType.S: foodKoma = KyokumenPngArgs_FoodOrDropKoma.GIN_; break;
                        case PieceType.G: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KIN_; break;
                        case PieceType.R: foodKoma = KyokumenPngArgs_FoodOrDropKoma.HI__; break;
                        case PieceType.B: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KAKU; break;
                        default: foodKoma = KyokumenPngArgs_FoodOrDropKoma.UNKNOWN; break;
                    }
                }
                else
                {
                    foodKoma = KyokumenPngArgs_FoodOrDropKoma.NONE;
                }


                // 評価明細に添付
                Util_KyokumenPng_Writer.Write1(
                    Conv_KifuNode.ToRO_Kyokumen1(node),
                    srcMasu_orMinusOne,
                    dstMasu_orMinusOne,
                    foodKoma,
                    ConvMoveStrSfen.ToMoveStrSfen(node.Key),
                    relFolder,
                    fileName,
                    reportEnvironment
                    );
                logFileCounter++;
            }

            //
            // 評価明細
            //
            {
                Util_KifuTreeLogWriter.AAAA_Write_HyokaMeisai(engineConf, fileName, node, relFolder, reportEnvironment);
            }
        }


        /// <summary>
        /// 評価明細の書き出し。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        /// <param name="treeFolder"></param>
        /// <param name="env"></param>
        public static void AAAA_Write_HyokaMeisai(
            IEngineConf engineConf,
            string id,
            KifuNode node,
            string treeFolder,
            KyokumenPngEnvironment env
            )
        {
            StringBuilder sb = new StringBuilder();

            // 見出し
            sb.Append(id);
            sb.Append("    ");
            sb.Append(((int)node.Score).ToString());
            sb.Append("    ");
            switch (node.Value.KyokumenConst.KaisiPside)
            {
                case Playerside.P1: sb.Append("P2が指し終えた局面。手番P1"); break;
                case Playerside.P2: sb.Append("P1が指し終えた局面。手番P2"); break;
                case Playerside.Empty: sb.Append("手番Empty"); break;
            }
            sb.AppendLine();

            foreach (KeyValuePair<string, KyHyokaMeisai_Koumoku> entry in node.KyHyokaSheet_Mutable.Items)
            {
                KyHyokaMeisai_Koumoku koumoku = ((KyHyokaMeisai_Koumoku)entry.Value);

                sb.Append("    ");

                sb.Append(entry.Key);//項目名
                sb.Append("  ");
                sb.Append(koumoku.UtiwakeValue);//評価値
                sb.Append("  ");

                sb.Append(koumoku.Utiwake);//内訳
                sb.AppendLine();
            }
            sb.AppendLine();

            ////------------------------------
            //// TODO: 局面ハッシュ
            ////------------------------------
            //sb.Append("hash:");
            //sb.AppendLine(Conv_Sky.ToKyokumenHash(node.Value.ToKyokumenConst).ToString());
            //sb.AppendLine();

            File.AppendAllText($"{env.OutFolder}{treeFolder}{engineConf.GetResourceBasename("HyokaMeisaiLogTxtBasename")}", sb.ToString());
        }









        //        /// <summary>
        //        /// 評価明細ログの書出し
        //        /// </summary>
        //        private static void B_Write_Html5(
        //            //Shogisasi shogisasi,
        //            KaisetuBoards logF_kiki,
        //            KifuTree kifu,
        //            IErrorController logTag
        //            )
        //        {
        //#if DEBUG
        //            try
        //            {
        //                if (0 < logF_kiki.boards.Count)//ﾛｸﾞが残っているなら
        //                {
        //                    bool enableLog = true;// false;
        //                    //
        //                    // ログの書き出し
        //                    //
        //                    Util_GraphicalLog.WriteHtml5(
        //                        enableLog,
        //                        "#評価ログ",
        //                        $"[{Conv_KaisetuBoards.ToJsonStr(logF_kiki)}]"
        //                    );

        //                    // 書き出した分はクリアーします。
        //                    logF_kiki.boards.Clear();
        //                }
        //            }
        //            catch (Exception ex) { logTag.Panic(ex, "HTML5ログを出力しようとしたときです。"); throw; }
        //#endif
        //        }




    }
}
