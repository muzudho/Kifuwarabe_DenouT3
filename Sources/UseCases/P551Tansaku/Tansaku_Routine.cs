using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P542Scoreing.I250Args;
using Grayscale.P554TansaFukasa.I500Struct;

namespace Grayscale.P551Tansaku.I500Tansaku
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
            IErrorController log
            );

    }
}
