using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;

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
