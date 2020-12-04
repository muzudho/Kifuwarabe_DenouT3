using Grayscale.P145SfenStruct.L250Struct;
using Grayscale.P146ConvSfen.L500Converter;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P276SeizaStartp.L500Struct;


namespace Grayscale.P307UtilSky.L500Util
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
