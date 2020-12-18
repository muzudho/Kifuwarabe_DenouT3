using System.IO;
using System.Text;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P440KifuTreeLog.L500Struct;
using Grayscale.P521FeatureVect.L500Struct;
using Grayscale.P523UtilFv.L510UtilFvLoad;
using Grayscale.P542Scoreing.I250Args;
using Grayscale.P542Scoreing.L250Args;
using Grayscale.P571KifuWarabe.L100Shogisasi;
using Grayscale.P571KifuWarabe.L500KifuWarabe;
using Grayscale.P743FvLearn.I400N54List;
using Grayscale.P743FvLearn.L250Learn;
using Grayscale.P743FvLearn.L260View;
using Grayscale.P743FvLearn.L400N54List;
using Grayscale.P743FvLearn.L420Inspection;
using Grayscale.P743FvLearn.L460Scoreing;
using Grayscale.P743FvLearn.L470StartZero;

#if DEBUG || LEARN
using Grayscale.P523UtilFv.L480UtilFvEdit;
#endif

#if DEBUG
using Grayscale.P222LogKaisetu.L250Struct;
#endif

namespace Grayscale.P743FvLearn.L600Operation
{
    public abstract class Util_LearnOperation
    {

        /// <summary>
        /// 指定の指し手の順位調整を行います。
        /// 
        /// 全体が調整されてしまうような☆？
        /// </summary>
        /// <param name="uc_Main"></param>
        /// <param name="tyoseiryo"></param>
        public static void ARankUpSelectedMove(Uc_Main uc_Main, float tyoseiryo, ILogTag errH)
        {
            //----------------------------------------
            // 選択したノードを参考に、減点を行う。
            //----------------------------------------
            foreach (GohosyuListItem item in uc_Main.LstGohosyu.SelectedItems)
            {
                string sfenMoveStr = item.Sfen;
#if DEBUG
                errH.Logger.WriteLineAddMemo("sfenMoveStr=" + sfenMoveStr);
#endif

                if (uc_Main.LearningData.Kifu.CurNode.HasChildNode(sfenMoveStr))
                {
#if DEBUG
                    errH.Logger.WriteLineAddMemo("----------------------------------------");
                    errH.Logger.WriteLineAddMemo("FV 総合点（読込前）1");
                    errH.Logger.WriteLineAddMemo("      PP =" + Util_FeatureVectorEdit.GetTotal_PP(uc_Main.LearningData.Fv));
                    errH.Logger.WriteLineAddMemo("----------------------------------------");
#endif
                    Node<IMove, KyokumenWrapper> nextNode = uc_Main.LearningData.Kifu.CurNode.GetChildNode(sfenMoveStr);

                    // 盤上の駒、持駒を数えます。
                    N54List nextNode_n54List = Util_54List.Calc_54List(nextNode.Value.KyokumenConst, errH);

                    float real_tyoseiryo; //実際に調整した量。
                    Util_FvScoreing.UpdateKyokumenHyoka(
                        nextNode_n54List,
                        nextNode.Value.KyokumenConst,
                        uc_Main.LearningData.Fv,
                        tyoseiryo,
                        out real_tyoseiryo,
                        errH
                        );//相手が有利になる点
#if DEBUG
                    errH.Logger.WriteLineAddMemo("----------------------------------------");
                    errH.Logger.WriteLineAddMemo("FV 総合点（読込後）6");
                    errH.Logger.WriteLineAddMemo("      PP =" + Util_FeatureVectorEdit.GetTotal_PP(uc_Main.LearningData.Fv));
                    errH.Logger.WriteLineAddMemo("----------------------------------------");
#endif
                }
            }

            //----------------------------------------
            // 点数を付け直すために、ノードを一旦、全削除
            //----------------------------------------
            uc_Main.LearningData.Kifu.CurNode.Clear_ChildNodes();

            //----------------------------------------
            // ネクスト・ノードを再作成
            //----------------------------------------
            // TODO:本譜のネクスト・ノードは？
            uc_Main.LearningData.Aa_Yomi(uc_Main.LearningData.Kifu.CurNode.Key, LogTags.Learner);
        }


        /// <summary>
        /// 初期局面の評価値を 0 点にするようにFVを調整します。
        /// </summary>
        public static void Do_ZeroStart(ref bool isRequest_ShowGohosyu, Uc_Main uc_Main, ILogTag errH)
        {
            bool isRequestDoEvents = false;
            Util_StartZero.Adjust_HirateSyokiKyokumen_0ten_AndFvParamRange(ref isRequestDoEvents, uc_Main.LearningData.Fv, errH);

            //// 合法手一覧を作成
            //uc_Main.LearningData.Aa_Yomi(uc_Main.LearningData.Kifu.CurNode.Key, errH);

            // 局面の合法手表示の更新を要求
            isRequest_ShowGohosyu = true;
        }

        /// <summary>
        /// FIXME: 未実装
        /// 指し手の順位上げ。
        /// </summary>
        public static void DoRankUpMove(
            ref bool isRequest_ShowGohosyu,
            ref bool isRequest_ChangeKyokumenPng,
            Uc_Main uc_Main, ILogTag errH)
        {
            // 評価値変化量
            float chosei_bairitu;
            float.TryParse(uc_Main.TxtChoseiBairituB.Text, out chosei_bairitu);

            if (Playerside.P2 == uc_Main.LearningData.Kifu.CurNode.Value.KyokumenConst.KaisiPside)
            {
                chosei_bairitu *= -1; //後手はマイナスの方が有利。
            }

            Util_LearnOperation.ARankUpSelectedMove(uc_Main, chosei_bairitu, errH);

            // 現局面の合法手表示の更新を要求
            isRequest_ShowGohosyu = true;
            // 局面PNG画像更新を要求
            isRequest_ChangeKyokumenPng = true;
        }

        /// <summary>
        /// FIXME: 未実装
        /// 指し手の順位下げ。
        /// </summary>
        public static void DoRankDownMove(
            ref bool isRequest_ShowGohosyu,
            ref bool isRequest_ChangeKyokumenPng,
            Uc_Main uc_Main, ILogTag errH)
        {
            // 評価値変化量
            float badScore;
            float.TryParse(uc_Main.TxtChoseiBairituB.Text, out badScore);
            badScore *= -1.0f;

            if (Playerside.P2 == uc_Main.LearningData.Kifu.CurNode.Value.KyokumenConst.KaisiPside)
            {
                badScore *= -1; //後手はプラスの方が不利。
            }

            Util_LearnOperation.ARankUpSelectedMove(uc_Main, badScore, errH);

            // 現局面の合法手表示の更新を要求
            isRequest_ShowGohosyu = true;
            // 局面PNG画像更新を要求
            isRequest_ChangeKyokumenPng = true;
        }

        /// <summary>
        /// FIXME:未実装
        /// 二駒の評価値を表示。
        /// </summary>
        /// <param name="uc_Main"></param>
        public static void Do_ShowNikomaHyokati(Uc_Main uc_Main)
        {
#if DEBUG || LEARN
            KyHyokaMeisai_Koumoku komawariMeisai;
            KyHyokaMeisai_Koumoku ppMeisai;
#endif
            uc_Main.LearningData.DoScoreing_ForLearning(
                (KifuNode)uc_Main.LearningData.Kifu.CurNode
#if DEBUG || LEARN
                ,
                out komawariMeisai,
                out ppMeisai
#endif
);

            StringBuilder sb = new StringBuilder();
#if DEBUG || LEARN
            sb.Append("駒割=");
            sb.Append(komawariMeisai.UtiwakeValue.ToString());
            sb.Append(" Pp=");
            sb.Append(ppMeisai.UtiwakeValue.ToString());
            //sb.Append("　Pp内訳:");
            //sb.Append(ppMeisai.Utiwake);
#else
            sb.Append("DEBUG または LEARNモードで実行してください。");
#endif

            uc_Main.TxtNikomaHyokati.Text = sb.ToString();
        }

        ///// <summary>
        ///// FVを0～999(*bairitu)に矯正。
        ///// </summary>
        //public static void Do_FvRange999(ref bool isRequest_ShowGohosyu, Uc_Main uc_Main, IErrorController errH)
        //{
        //    Util_LearnFunctions.FvParamRange_PP(uc_Main.LearningData.Fv, errH);

        //    // 局面の合法手表示の更新を要求します。
        //    isRequest_ShowGohosyu = true;
        //}


        public static void Do_OpenFvCsv(Uc_Main uc_Main, ILogTag errH)
        {
            if ("" != uc_Main.TxtFvFilepath.Text)
            {
                uc_Main.OpenFvFileDialog2.InitialDirectory = Path.GetDirectoryName(uc_Main.TxtFvFilepath.Text);
                uc_Main.OpenFvFileDialog2.FileName = Path.GetFileName(uc_Main.TxtFvFilepath.Text);
            }
            else
            {
                uc_Main.OpenFvFileDialog2.InitialDirectory = Application.StartupPath;
            }

            DialogResult result = uc_Main.OpenFvFileDialog2.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    uc_Main.TxtFvFilepath.Text = uc_Main.OpenFvFileDialog2.FileName;

                    StringBuilder sb_result = new StringBuilder();
                    // フィーチャー・ベクターの外部ファイルを開きます。
                    sb_result.Append(Util_FvLoad.OpenFv(uc_Main.LearningData.Fv, uc_Main.TxtFvFilepath.Text, errH));
                    uc_Main.TxtStatus1.Text = sb_result.ToString();

                    // うまくいっていれば、フィーチャー・ベクターのセットアップが終わっているはず。
                    {
                        // 調整量
                        uc_Main.TyoseiryoSettings.SetSmallest(uc_Main.LearningData.Fv.TyoseiryoSmallest_NikomaKankeiPp);
                        uc_Main.TyoseiryoSettings.SetLargest(uc_Main.LearningData.Fv.TyoseiryoLargest_NikomaKankeiPp);
                        //
                        // 調整量（初期値）
                        //
                        uc_Main.TxtTyoseiryo.Text = uc_Main.LearningData.Fv.TyoseiryoInit_NikomaKankeiPp.ToString();


                        // 半径
                        float paramRange = Util_Inspection.FvParamRange( uc_Main.LearningData.Fv);
                        uc_Main.ChkAutoParamRange.Text = "評価更新毎-" + paramRange + "～" + paramRange + "矯正";
                    }

                    uc_Main.BtnUpdateKyokumenHyoka.Enabled = true;

                    break;
                default:
                    break;
            }

            //gt_EndMethod:
            //    ;
        }



        public static void Load_CsaKifu(Uc_Main uc_Main, ILogTag errH)
        {
            uc_Main.LearningData.ReadKifu(uc_Main);

            Util_LearningView.ShowMoveList(uc_Main.LearningData, uc_Main, errH);
        }


        public static void Do_OpenCsaKifu(
            ref bool isRequest_ShowGohosyu,
            ref bool isRequest_ChangeKyokumenPng,
            string kifuFilepath,
            Uc_Main uc_Main, ILogTag errH)
        {
            uc_Main.TxtKifuFilepath.Text = kifuFilepath;

            // 平手初期局面の棋譜ツリーを準備します。
            Util_LearnOperation.Setup_KifuTree(
                ref isRequest_ShowGohosyu,
                ref isRequest_ChangeKyokumenPng,
                uc_Main,errH);

            // 処理が重いので。
            Application.DoEvents();

            // CSA棋譜を読み込みます。
            Util_LearnOperation.Load_CsaKifu(uc_Main,errH);

            // 合法手を調べます。
            uc_Main.LearningData.Aa_Yomi(uc_Main.LearningData.Kifu.CurNode.Key, errH);
            // ノード情報の表示
            Util_LearningView.Aa_ShowNode2(uc_Main.LearningData, uc_Main, LogTags.Learner);

        //gt_EndMethod:
        //    ;
        }

        /// <summary>
        /// 棋譜ツリーをセットアップします。
        /// </summary>
        public static void Setup_KifuTree(
            ref bool isRequest_ShowGohosyu,
            ref bool isRequest_ChangeKyokumenPng,
            Uc_Main uc_Main, ILogTag errH)
        {
            {
                KifuTree kifu_newHirate;
                Util_FvLoad.CreateKifuTree(out kifu_newHirate);
                uc_Main.LearningData.Kifu = kifu_newHirate;
            }

            EvaluationArgs args;
            {
#if DEBUG
                KaisetuBoards logF_kiki = new KaisetuBoards();// デバッグ用だが、メソッドはこのオブジェクトを必要としてしまう。
#endif
                args = new EvaluationArgsImpl(
                    uc_Main.LearningData.Kifu.GetSennititeCounter(),
                    new FeatureVectorImpl(),
                    new ShogisasiImpl(new ProgramSupport()),
                    Util_KifuTreeLogWriter.REPORT_ENVIRONMENT
#if DEBUG
                    ,
                    logF_kiki
#endif
);
            }

            // 合法手を数えたい。
            uc_Main.LearningData.Aaa_CreateNextNodes_Gohosyu(args, errH);

            // 現局面の合法手表示の更新を要求
            isRequest_ShowGohosyu = true;
            
            // 局面PNG画像更新を要求
            isRequest_ChangeKyokumenPng = true;
        }


    }
}
