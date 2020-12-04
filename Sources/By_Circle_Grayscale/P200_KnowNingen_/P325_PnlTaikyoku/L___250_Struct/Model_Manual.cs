using Grayscale.P211WordShogi.L500Word;
using Grayscale.P224Sky.L500Struct;

namespace Grayscale.P325_PnlTaikyoku.L___250_Struct
{
    public interface Model_Manual
    {
        void SetGuiSky(SkyConst sky);

        int GuiTemezumi { get; set; }

        SkyConst GuiSkyConst { get; }

        Playerside GuiPside { get; set; }
    }
}
