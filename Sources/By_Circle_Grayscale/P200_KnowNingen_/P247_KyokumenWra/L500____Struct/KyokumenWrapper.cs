using Grayscale.P224_Sky________.L500____Struct;
using System.Diagnostics;

namespace Grayscale.P247_KyokumenWra.L500____Struct
{

    /// <summary>
    /// 局面ラッパー。
    /// </summary>
    public class KyokumenWrapper
    {

        //*
        #region sky型
        public SkyConst KyokumenConst { get { return this.kyokumen; } }
        public void SetKyokumen(SkyConst sky) { this.kyokumen = sky; }
        private SkyConst kyokumen;
        #endregion
        // */

        /*
        #region SfenstringImpl型
        public SkyConst ToKyokumenConst {
            get {
                SkyConst result = Util_Sky.ImportSfen(this.sfenstringImpl);

                StartposImporter.Assert_HirateHonsyogi(new SkyBuffer(result), "this.startposString=[" + this.sfenstringImpl + "]");

                return result;
            }
        }
        public void SetKyokumen(SkyConst sky) { this.sfenstringImpl = Util_Sky.ExportSfen(sky); }
        private SfenstringImpl sfenstringImpl;
        #endregion
        // */

        public KyokumenWrapper(SkyConst sky)
        {
            Debug.Assert(sky.Count == 40, "sky.Starlights.Count=[" + sky.Count + "]");//将棋の駒の数

            //*
            #region sky型
            this.kyokumen = sky;
            #endregion
            // */

            /*
            #region SfenstringImpl型
            this.sfenstringImpl = Util_Sky.ExportSfen(sky);
            #endregion
            // */
        }
    }
}
