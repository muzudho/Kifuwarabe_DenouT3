using Grayscale.P003Log.I500Struct;
using Grayscale.P325PnlTaikyoku.I250Struct;

namespace Grayscale.P355_KifuParserA.I500Parser
{
    public interface KifuParserA_State
    {

        string Execute(
            ref KifuParserA_Result result,
            Model_Taikyoku model_Taikyoku,
            out KifuParserA_State nextState,
            KifuParserA owner,
            KifuParserA_Genjo genjo,
            IKwErrorHandler errH
            );

    }
}
