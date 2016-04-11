using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P325_PnlTaikyoku.L___250_Struct;

namespace Grayscale.P325_PnlTaikyoku.L250____Struct
{
    public class Model_ManualImpl : Model_Manual
    {


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// GUI用局面データ。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        /// 局面が進むごとに更新されていきます。
        /// 
        /// </summary>
        public SkyConst GuiSkyConst { get { return this.guiSkyConst; } }
        public void SetGuiSky(SkyConst sky)
        {
            this.guiSkyConst = sky;
        }
        private SkyConst guiSkyConst;
        public int GuiTemezumi { get; set; }
        public Playerside GuiPside { get; set; }

        public Model_ManualImpl()
        {
            //
            // 駒なし
            //
            this.GuiTemezumi = 0;
            Playerside firstPside = Playerside.P1;
            this.GuiPside = firstPside;
            this.guiSkyConst = Util_SkyWriter.New_Komabukuro(firstPside);// 描画モデル作成時

        }
    }
}
