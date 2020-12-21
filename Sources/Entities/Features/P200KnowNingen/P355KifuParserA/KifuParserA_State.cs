﻿using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.P355_KifuParserA.I500Parser
{
    public interface KifuParserA_State
    {
        string Execute(
            ref KifuParserA_Result result,
            Model_Taikyoku model_Taikyoku,
            out KifuParserA_State nextState,
            KifuParserA owner,
            KifuParserA_Genjo genjo
            );
    }
}
