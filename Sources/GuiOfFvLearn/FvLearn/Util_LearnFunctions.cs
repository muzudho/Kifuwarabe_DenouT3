﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;

namespace Grayscale.Kifuwarakaku.GuiOfFvLearn.Features
{
    public abstract class Util_LearnFunctions
    {
        /// <summary>
        /// FVを、-999.0～999.0(*bairitu)に矯正。
        /// </summary>
        public static void FvParamRange_PP(FeatureVector fv)
        {
            //--------------------------------------------------------------------------------
            // 変換前のデータを確認。 
            //--------------------------------------------------------------------------------
            Util_Inspection.Inspection1(fv);

            //--------------------------------------------------------------------------------
            // 点数を、順位に変換します。
            //--------------------------------------------------------------------------------
            Util_Ranking.Perform_Ranking(fv);

            //--------------------------------------------------------------------------------
            // トポロジー的に加工したあとのデータを確認。 
            //--------------------------------------------------------------------------------
            Util_Zooming.ZoomTo_FvParamRange(fv);

        }
        /// <summary>
        /// FVの保存。
        /// </summary>
        /// <param name="uc_Main"></param>
        public static void Do_Save(Uc_Main uc_Main)
        {
            FeatureVector fv = uc_Main.LearningData.Fv;


            // ファイルチューザーで指定された、fvフォルダーのパス
            string fvFolderPath = Path.GetDirectoryName(uc_Main.TxtFvFilepath.Text);

            // ファイルチューザーで指定された、Dataフォルダーのパス（fvフォルダーの親）
            string dataFolderPath = Directory.GetParent(fvFolderPath).FullName;

            //----------------------------------------
            // 時間
            //----------------------------------------
            string ymd;
            string hms;
            {
                DateTime dt = DateTime.Now;

                // 年月日
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(dt.Year);
                    sb.Append("-");
                    sb.Append(dt.Month);
                    sb.Append("-");
                    sb.Append(dt.Day);
                    ymd = sb.ToString();
                    uc_Main.TxtAutosaveYMD.Text = ymd;
                }

                // 時分秒
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(dt.Hour);
                    sb.Append("-");
                    sb.Append(dt.Minute);
                    sb.Append("-");
                    sb.Append(dt.Second);
                    hms = sb.ToString();
                    uc_Main.TxtAutosaveHMS.Text = hms;
                }
            }

            //----------------------------------------
            // バックアップ
            //----------------------------------------
            //
            // 失敗した場合、バックアップせず続行します
            //
            {
                // バックアップの失敗判定
                bool backup_failuer = false;

                // フォルダーのリネーム
                try
                {
                    string srcPath = Path.Combine(dataFolderPath, "fv");
                    string dstPath = Path.Combine(dataFolderPath, $"fv_{ymd}_{hms}");

                    Directory.Move(srcPath, dstPath);
                }
                catch (IOException)
                {
                    // フォルダーを、Windowsのファイル・エクスプローラーで開いているなどすると、失敗します。
                    backup_failuer = true;
                }

                if (!backup_failuer)
                {
                    // fvフォルダーの新規作成
                    Directory.CreateDirectory(fvFolderPath);
                }
            }

            //----------------------------------------
            // -999～999 に調整
            //----------------------------------------
            Util_LearnFunctions.FvParamRange_PP(uc_Main.LearningData.Fv);// 自動で -999～999(*bairitu) に矯正。


            // 駒割
            File.WriteAllText(uc_Main.TxtFvFilepath.Text, Format_FeatureVector_Komawari.Format_Text(fv));
            // スケール
            Util_FeatureVectorOutput.Write_Scale(uc_Main.EngineConf, fv, fvFolderPath);
            // KK
            Util_FeatureVectorOutput.Write_KK(uc_Main.EngineConf, fv, fvFolderPath);
            // 1pKP,2pKP
            Util_FeatureVectorOutput.Write_KP(uc_Main.EngineConf, fv, fvFolderPath);
            // PP 盤上
            Util_FeatureVectorOutput.Write_PP_Banjo(uc_Main.EngineConf, fv, fvFolderPath);
            // PP １９枚の持駒
            Util_FeatureVectorOutput.Write_PP_19Mai(uc_Main.EngineConf, fv, fvFolderPath);
            // PP ５枚の持駒、３枚の持駒
            Util_FeatureVectorOutput.Write_PP_5Mai(uc_Main.EngineConf, fv, fvFolderPath);
            Util_FeatureVectorOutput.Write_PP_3Mai(uc_Main.EngineConf, fv, fvFolderPath);
        }

        /// <summary>
        /// 本譜の手をランクアップ。
        /// </summary>
        public static void Do_RankUpHonpu(ref bool ref_isRequestShowGohosyu, Uc_Main uc_Main, string sfenMoveStr, float tyoseiryo)
        {
            //----------------------------------------
            // 1P は正の数がグッド、2P は負の数がグッド。
            //----------------------------------------
            float tyoseiryo_bad = -tyoseiryo;//減点に使われる数字です。[局面評価更新]ボタンの場合。
            float tyoseiryo_good = 0.0f;//加点に使われる数字です。

            float badScore_temp = tyoseiryo_bad;
            if (uc_Main.LearningData.Kifu.CurNode.Value.KyokumenConst.KaisiPside == Playerside.P2)
            {
                tyoseiryo_bad *= -1.0f;//2Pは、負数の方が高得点です。
            }

            //
            // 合法手一覧
            //
            uc_Main.LearningData.Kifu.CurNode.Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
            {
                // 本譜手はまだ計算しない。
                if (key == sfenMoveStr)
                {
                    goto gt_NextLoop1;
                }

                // 盤上の駒、持駒を数えます。
                N54List childNode_n54List = Util_54List.Calc_54List(node.Value.KyokumenConst);

                float real_tyoseiryo; //実際に調整した量。
                Util_FvScoreing.UpdateKyokumenHyoka(
                    childNode_n54List,
                    node.Value.KyokumenConst,
                    uc_Main.LearningData.Fv,
                    tyoseiryo_bad,
                    out real_tyoseiryo
                    );//相手が有利になる点
                tyoseiryo_good += -real_tyoseiryo;
            gt_NextLoop1:
                ;
            });

            //
            // 本譜手
            //
            if (uc_Main.LearningData.Kifu.CurNode.HasChildNode(sfenMoveStr))
            {
                // 盤上の駒、持駒を数えます。
                N54List currentNode_n54List = Util_54List.Calc_54List(uc_Main.LearningData.Kifu.CurNode.Value.KyokumenConst);

                float real_tyoseiryo; //実際に調整した量。
                Util_FvScoreing.UpdateKyokumenHyoka(
                    currentNode_n54List,
                    uc_Main.LearningData.Kifu.CurNode.GetChildNode(sfenMoveStr).Value.KyokumenConst,
                    uc_Main.LearningData.Fv,
                    tyoseiryo_good,
                    out real_tyoseiryo
                    );//自分が有利になる点
            }
            else
            {
                Debug.Fail($"指し手[{sfenMoveStr}]に対応する次ノードは作成されていませんでした。\n" +
                    uc_Main.LearningData.DumpToAllGohosyu(
                        uc_Main.LearningData.Kifu.CurNode.Value.KyokumenConst));
            }

            ////----------------------------------------
            //// 合法手一覧を作成したい。
            ////----------------------------------------
            //uc_Main.LearningData.Aa_Yomi(uc_Main.LearningData.Kifu.CurNode.Key);

            // 局面の合法手表示の更新を要求します。
            ref_isRequestShowGohosyu = true;
        }

    }
}
