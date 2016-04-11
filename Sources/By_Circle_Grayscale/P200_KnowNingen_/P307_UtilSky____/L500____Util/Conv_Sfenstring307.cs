using Grayscale.P145_SfenStruct_.L250____Struct;
using Grayscale.P146_ConvSfen___.L500____Converter;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P276_SeizaStartp.L500____Struct;


namespace Grayscale.P307_UtilSky____.L500____Util
{
    public abstract class Conv_Sfenstring307 : Conv_Sfenstring146
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sky"></param>
        /// <param name="sfenStartpos"></param>
        public static SkyConst ToSkyConst(SfenstringImpl startposString, Playerside pside, int temezumi)
        {
            StartposImporter startposImporter;
            string restText;
            StartposImporter.TryParse(
                startposString.ValueStr,
                out startposImporter,
                out restText
                );

            return startposImporter.ToSky(pside, temezumi);
        }

    }
}
