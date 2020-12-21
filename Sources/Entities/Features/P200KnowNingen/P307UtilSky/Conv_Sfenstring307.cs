namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Conv_Sfenstring307 : Sfenstring146Conv
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sky"></param>
        /// <param name="sfenStartpos"></param>
        public static SkyConst ToSkyConst(SfenStringImpl startposString, Playerside pside, int temezumi)
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
