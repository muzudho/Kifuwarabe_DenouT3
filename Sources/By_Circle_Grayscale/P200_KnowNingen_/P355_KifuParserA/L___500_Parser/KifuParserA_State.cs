using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P325_PnlTaikyoku.L___250_Struct;

namespace Grayscale.P355_KifuParserA.L___500_Parser
{
    public interface KifuParserA_State
    {

        string Execute(
            ref KifuParserA_Result result,
            Model_Taikyoku model_Taikyoku,
            out KifuParserA_State nextState,
            KifuParserA owner,
            KifuParserA_Genjo genjo,
            KwErrorHandler errH
            );

    }
}
