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

namespace Grayscale.P743_FvLearn____.L510____AutoKifuRead
{

    /// <summary>
    /// 自動学習
    /// </summary>
    public abstract class Util_AutoKifuRead
    {




        /// <summary>
        /// 局面評価を更新。
        /// </summary>
        public static void Do_UpdateKyokumenHyoka(
            ref bool isRequest_ShowGohosyu,
            ref bool isRequest_ChangeKyokumenPng,
            Uc_Main uc_Main, KwErrorHandler errH)
        {

            int renzokuTe;
            if (!int.TryParse(uc_Main.TxtRenzokuTe.Text, out renzokuTe))
            {
                // パース失敗時は 1回実行。
                renzokuTe = 1;
            }

            while(true)//無限ループ
            {// 棋譜ループ


                bool isEndKifuread;
                //----------------------------------------
                // 繰り返し、指し手を進めます。
                //----------------------------------------
                Util_AutoSasiteRush.Do_SasiteRush(
                    out isEndKifuread,
                    ref isRequest_ShowGohosyu,
                    ref isRequest_ChangeKyokumenPng,
                    renzokuTe,
                    uc_Main, errH);

                if (isEndKifuread)
                {
                    //棋譜の自動読取の終了
                    goto gt_EndKifuList;
                }

                // 無限ループなので。
                Application.DoEvents();

            }//棋譜ループ

        gt_EndKifuList://棋譜の自動読取の終了
            ;
        }


    }
}
