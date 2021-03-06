﻿using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Entities.Logging;

namespace Grayscale.Kifuwarakaku.GuiOfFvLearn.Features
{
    public abstract class Util_AutoSortingRush
    {

        /// <summary>
        /// 指し手の順位を変えるループです。
        /// </summary>
        /// <param name="out_pushCount"></param>
        /// <param name="out_isEndAutoLearn"></param>
        /// <param name="ref_isRequest_ShowGohosyu"></param>
        /// <param name="ref_isRequest_ChangeKyokumenPng"></param>
        /// <param name="ref_isRequestDoEvents"></param>
        /// <param name="loopLimit"></param>
        /// <param name="ref_tyoseiryo"></param>
        /// <param name="sfenMoveStr"></param>
        /// <param name="uc_Main"></param>
        /// <param name="logTag"></param>
        public static void DoSortMoveRush(
            out int out_pushCount,
            out bool out_isEndAutoLearn,
            ref bool ref_isRequest_ShowGohosyu,
            ref bool ref_isRequest_ChangeKyokumenPng,
            ref bool ref_isRequestDoEvents,
            int loopLimit,
            ref float ref_tyoseiryo,
            string sfenMoveStr,
            Uc_Main uc_Main
            )
        {
            out_isEndAutoLearn = false;
            out_pushCount = 0;

            for (; out_pushCount < loopLimit; out_pushCount++)
            { //指し手順位更新ループ
                //----------------------------------------
                // 強制中断（ループの最初のうちに）
                //----------------------------------------
                //
                // 「Stop_learning.txt」という名前のファイルが .exe と同じフォルダーに置いてあると
                // 学習を終了することにします。
                //
                if (uc_Main.StopLearning.IsStop())
                {
                    out_isEndAutoLearn = true;
                    goto gt_EndMethod;
                }

                // 順位確認
                if (0 < uc_Main.LstGohosyu.Items.Count)
                {
                    GohosyuListItem gohosyuItem = (GohosyuListItem)uc_Main.LstGohosyu.Items[0];

                    if (sfenMoveStr == gohosyuItem.Sfen)
                    {
                        // 1位なら終了
                        Logger.Trace($"items.Count=[{uc_Main.LstGohosyu.Items.Count}] sfenMoveStr=[{sfenMoveStr}] gohosyuItem.Sfen=[{gohosyuItem.Sfen}]");
                        break;
                    }
                }

                // １位ではないのでランクアップ。
                Util_LearnFunctions.Do_RankUpHonpu(ref ref_isRequest_ShowGohosyu, uc_Main, sfenMoveStr, ref_tyoseiryo);

                // 調整量の自動調整
                if (uc_Main.ChkTyoseiryoAuto.Checked)
                {
                    Util_Tyoseiryo.Up_Bairitu_AtStep(ref ref_isRequestDoEvents, uc_Main, out_pushCount, ref ref_tyoseiryo);
                }

                if (uc_Main.ChkStartZero.Checked)// 自動で、平手初期局面の点数を 0 点に近づけるよう調整します。
                {
                    Util_StartZero.Adjust_HirateSyokiKyokumen_0ten_AndFvParamRange(ref ref_isRequestDoEvents, uc_Main.LearningData.Fv);
                }

                // 局面の表示を更新します。
                if (ref_isRequest_ShowGohosyu)
                {
                    // 合法手一覧を更新
                    Util_LearningView.Aa_ShowGohosyu2(uc_Main.LearningData, uc_Main);
                    // 局面PNG画像の更新は、ここでは行いません。
                    ref_isRequest_ShowGohosyu = false;

                    ref_isRequestDoEvents = true;
                }

                if (ref_isRequestDoEvents)
                {
                    Application.DoEvents();
                    ref_isRequestDoEvents = false;
                }

            }// 指し手順位更新ループ
             //----------------------------------------
             // 連打終わり
             //----------------------------------------

        gt_EndMethod:
            ;
        }

    }
}
