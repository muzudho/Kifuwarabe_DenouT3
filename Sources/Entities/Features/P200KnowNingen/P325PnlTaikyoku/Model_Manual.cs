using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.P325PnlTaikyoku.I250Struct
{
    public interface Model_Manual
    {
        void SetGuiSky(SkyConst sky);

        int GuiTemezumi { get; set; }

        SkyConst GuiSkyConst { get; }

        Playerside GuiPside { get; set; }
    }
}
