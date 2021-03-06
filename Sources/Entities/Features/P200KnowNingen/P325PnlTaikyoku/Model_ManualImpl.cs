﻿namespace Grayscale.Kifuwarakaku.Entities.Features
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
