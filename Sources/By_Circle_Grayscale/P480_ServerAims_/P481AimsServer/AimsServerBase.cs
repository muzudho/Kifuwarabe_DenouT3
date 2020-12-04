using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P481AimsServer.I060Phase;

namespace Grayscale.P481AimsServer.I070ServerBase
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
