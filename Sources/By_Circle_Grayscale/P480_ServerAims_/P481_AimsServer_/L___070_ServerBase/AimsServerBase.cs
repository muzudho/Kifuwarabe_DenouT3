using Grayscale.P325_PnlTaikyoku.L___250_Struct;
using Grayscale.P481_AimsServer_.L___060_Phase;

namespace Grayscale.P481_AimsServer_.L___070_ServerBase
{
    public interface AimsServerBase
    {
        /// <summary>
        /// フェーズ。
        /// </summary>
        Phase_AimsServer Phase_AimsServer { get; }
        void SetPhase_AimsServer(Phase_AimsServer phase_AimsServer);

        /// <summary>
        /// 対局モデル。棋譜ツリーなど。
        /// </summary>
        Model_Taikyoku Model_Taikyoku { get; }
    }
}
