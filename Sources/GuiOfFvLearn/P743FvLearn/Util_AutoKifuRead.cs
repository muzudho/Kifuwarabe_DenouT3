using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P743FvLearn.L508AutoMoveRush;

namespace Grayscale.P743FvLearn.L510AutoKifuRead
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
            Uc_Main uc_Main, ILogTag logTag)
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
                UtilAutoMoveRush.DoMoveRush(
                    out isEndKifuread,
                    ref isRequest_ShowGohosyu,
                    ref isRequest_ChangeKyokumenPng,
                    renzokuTe,
                    uc_Main, logTag);

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
