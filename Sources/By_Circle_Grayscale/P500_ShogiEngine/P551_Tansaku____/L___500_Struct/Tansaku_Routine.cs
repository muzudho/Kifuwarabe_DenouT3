using Grayscale.P003Log.I500Struct;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P542_Scoreing___.L___250_Args;
using Grayscale.P554_TansaFukasa.I500Struct;

namespace Grayscale.P551_Tansaku____.L___500_Tansaku
{
    public interface Tansaku_Routine
    {

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
        void WAA_Yomu_Start(
            KifuTree kifu,
            bool isHonshogi,
            Mode_Tansaku mode_Tansaku,
            float alphabeta_otherBranchDecidedValue,
            EvaluationArgs args,
            KwErrorHandler log
            );

    }
}
