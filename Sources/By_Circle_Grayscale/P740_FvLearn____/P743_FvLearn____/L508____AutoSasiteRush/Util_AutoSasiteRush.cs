using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P743_FvLearn____.L250____Learn;
using Grayscale.P743_FvLearn____.L260____View;
using Grayscale.P743_FvLearn____.L450____Tyoseiryo;
using Grayscale.P743_FvLearn____.L470____StartZero;
using Grayscale.P743_FvLearn____.L480____Functions;
using System.IO;
using System.Windows.Forms;
using Grayscale.P743_FvLearn____.L___490_StopLearning;
using Grayscale.P743_FvLearn____.L490____StopLearning;
using Grayscale.P743_FvLearn____.L508____AutoSasiteRush;
using Grayscale.P743_FvLearn____.L506____AutoSasiteSort;

namespace Grayscale.P743_FvLearn____.L508____AutoSasiteRush
{
    /// <summary>
    /// 自動で棋譜を読み取ります。
    /// </summary>
    public abstract class Util_AutoSasiteRush
    {
        /// <summary>
        /// 評価更新繰り返し回数。1以上の数字が必要。
        /// </summary>
        public const int RENZOKU_KAISU = 3; // 100の前提で作ってあるが、やるほど弱くなるので数回にしておく。
        // 60だと 34 ぐらいで終わる。
        // 150 だと 400 はすぐカンスト。
        // 256意味ない

        /// <summary>
        /// 繰り返し、指し手を進めます。
        /// </summary>
        public static void Do_SasiteRush(
            out bool out_isEndKifuread,
            ref bool isRequest_ShowGohosyu,
            ref bool isRequest_ChangeKyokumenPng,
            int renzokuTe,
            Uc_Main uc_Main, KwErrorHandler errH
            )
        {
            out_isEndKifuread = false;

            bool isRequestDoEvents = false;
            bool isSaved = false;

            //
            // N手を連続で自動実行。
            // 本譜の手が残っている間。
            //
            for (int pushedButton = 0; pushedButton < renzokuTe && 0 < uc_Main.LstSasite.Items.Count; pushedButton++)
            { // 指し手ループ
                isSaved = false;//リセット
                float tyoseiryo;
                float.TryParse(uc_Main.TxtTyoseiryo.Text, out tyoseiryo);

                HonpuSasiteListItemImpl sasiteItem = (HonpuSasiteListItemImpl)uc_Main.LstSasite.Items[0];
                string sfenSasiteStr = sasiteItem.Sfen;


                if (sfenSasiteStr == "")
                {
                    goto gt_EndSasiteList;
                }



                if (uc_Main.ChkIgnoreThink.Checked)
                {
                    // 最初の20手は学習しない。
                    int tesu;
                    if (!int.TryParse(uc_Main.TxtIgnoreLearn.Text, out tesu))
                    {
                        goto gt_EndTesu;
                    }

                    if (uc_Main.LearningData.Kifu.CurNode.Value.KyokumenConst.Temezumi < tesu+1)
                    {
                        goto gt_EndLearn;
                    }
                gt_EndTesu:
                    ;
                }

                //----------------------------------------
                // FIXME: 暫定：ボタン連打は可能に。
                //----------------------------------------
                //this.btnUpdateKyokumenHyoka.Enabled = false;//ボタン連打防止。

                //----------------------------------------
                // 指し手の順位を変える回数
                //----------------------------------------
                int loopLimit = 1; // 通常1回
                if (uc_Main.ChkHyakuretuken.Checked)
                {
                    loopLimit = Util_AutoSasiteRush.RENZOKU_KAISU;
                }

                //----------------------------------------
                // 指し手の順位を変えるループです。
                //----------------------------------------
                int pushCount;
                bool isEndAutoLearn;
                Util_AutoSortingRush.Do_SortSasiteRush(
                    out pushCount,
                    out isEndAutoLearn,
                    ref isRequest_ShowGohosyu,
                    ref isRequest_ChangeKyokumenPng,
                    ref isRequestDoEvents,
                    loopLimit,
                    ref tyoseiryo,
                    sfenSasiteStr,
                    uc_Main, errH
                    );

                if (isEndAutoLearn)
                {
                    //棋譜の自動読取の終了
                    out_isEndKifuread = true;
                    goto gt_EndMethod;
                }

                // 調整量の自動調整
                if (uc_Main.ChkTyoseiryoAuto.Checked)
                {
                    Util_Tyoseiryo.Up_Bairitu_AtEnd(ref isRequestDoEvents, uc_Main, pushCount, ref tyoseiryo);
                }

                if (uc_Main.ChkStartZero.Checked)// 自動で、平手初期局面の点数を 0 点に近づけるよう調整します。
                {
                    Util_StartZero.Adjust_HirateSyokiKyokumen_0ten_AndFvParamRange(ref isRequestDoEvents, uc_Main.LearningData.Fv, errH);
                }

                if (uc_Main.ChkAutoParamRange.Checked)
                {
                    //// 自動で -999～999 に矯正。
                    Util_LearnFunctions.FvParamRange_PP(uc_Main.LearningData.Fv, errH);// 自動で -999～999(*bairitu) に矯正。

                    // 合法手表示の更新を要求
                    isRequest_ShowGohosyu = true;
                }
            gt_EndLearn:
                ;


                //----------------------------------------
                // 投了図まで自動実行
                //----------------------------------------
                if (uc_Main.ChkRenzokuAutoRun.Checked)
                {
                    // [一手指す]ボタン押下
                    Util_LearningView.Ittesasu_ByBtnClick(
                        ref isRequest_ShowGohosyu,
                        ref isRequest_ChangeKyokumenPng,
                        uc_Main.LearningData, uc_Main, errH);

                    // 局面PNG画像の更新を要求
                    //isRequest_ChangeKyokumenPng = true;

                }
                else
                {
                    // 自動ループしないなら、終了。
                    break;
                }

                if (isRequest_ShowGohosyu)
                {
                    // 合法手一覧を更新
                    Util_LearningView.Aa_ShowGohosyu2(uc_Main.LearningData, uc_Main, errH);
                    isRequest_ShowGohosyu = false;

                    // 重い処理のあとは。
                    isRequestDoEvents = true;
                }

                if (isRequest_ChangeKyokumenPng)
                {
                    uc_Main.LearningData.ChangeKyokumenPng(uc_Main);
                    isRequest_ChangeKyokumenPng = false;

                    // 重い処理のあとは。
                    isRequestDoEvents = true;
                }

                //----------------------------------------
                // N手で学習終了
                //----------------------------------------
                if (uc_Main.ChkEndLearnTesu.Checked)
                {
                    // N手で学習を終了します。
                    int tesu;
                    if (!int.TryParse(uc_Main.TxtEndLearnTesu.Text, out tesu))
                    {
                        goto gt_EndTesu;
                    }

                    if (tesu <= uc_Main.LearningData.Kifu.CurNode.Value.KyokumenConst.Temezumi)
                    {
                        // 自動ループしないなら、終了。
                        break;
                    }

                gt_EndTesu:
                    ;
                }

                // オートセーブ
                //
                // 20手間隔で。
                //
                if (
                    uc_Main.ChkAutosave.Checked && uc_Main.LearningData.Kifu.CurNode.Value.KyokumenConst.Temezumi % 20 == 0
                )
                {
                    Util_LearnFunctions.Do_Save(uc_Main, errH);
                    isSaved = true;
                }

                if (isRequestDoEvents)
                {
                    Application.DoEvents();
                    isRequestDoEvents = false;
                }
            }//指し手ループ

            gt_EndSasiteList:
            ;

            // 終局時は、オートセーブ
            // ※20手で終局した場合は、20手で保存されたあと、2連続で終局時として保存されることになる。
            if (uc_Main.ChkAutosave.Checked)
            {
                if (!isSaved)
                {
                    Util_LearnFunctions.Do_Save(uc_Main, errH);
                    isSaved = true;
                }
            }

            if (isRequestDoEvents)
            {
                Application.DoEvents();
                isRequestDoEvents = false;
            }

            if (uc_Main.ChkAutoKifuNext.Checked)
            {
                //----------------------------------------
                // 棋譜がなくなるまで、繰り返すなら
                //----------------------------------------

                // 今読んだ棋譜を移す先（成功時）
                uc_Main.SeikoIdo();

                bool isEmptyKifu;
                uc_Main.Do_NextKifuSet(out isEmptyKifu, ref isRequest_ShowGohosyu, ref isRequest_ChangeKyokumenPng, errH);

                if (isEmptyKifu)
                {
                    //棋譜の自動読取の終了
                    out_isEndKifuread = true;
                    goto gt_EndMethod;
                }

                if (isRequest_ShowGohosyu)
                {
                    // 合法手一覧を更新
                    Util_LearnFunctions.FvParamRange_PP(uc_Main.LearningData.Fv, errH);// 自動で -999～999(*bairitu) に矯正。
                    Util_LearningView.Aa_ShowGohosyu2(uc_Main.LearningData, uc_Main, errH);
                    isRequest_ShowGohosyu = false;
                }

                if (isRequest_ChangeKyokumenPng)
                {
                    uc_Main.LearningData.ChangeKyokumenPng(uc_Main);
                    isRequest_ChangeKyokumenPng = false;
                }
            }
            else
            {
                //棋譜の自動読取の終了
                out_isEndKifuread = true;
                goto gt_EndMethod;
            }


        gt_EndMethod:
            ;
        }

    }
}
