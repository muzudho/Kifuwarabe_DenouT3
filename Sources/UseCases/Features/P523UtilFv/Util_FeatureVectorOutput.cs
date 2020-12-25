using System.IO;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Grayscale.Kifuwarakaku.Entities.Features;
using Nett;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{
    public abstract class Util_FeatureVectorOutput
    {

        private class PpItem_P1
        {
            public string Filepath { get; set; }
            public string Title { get; set; }
            public int P1_base { get; set; }
            public PpItem_P1(string filepath, string title, int p1_base)
            {
                this.Filepath = filepath;
                this.Title = title;
                this.P1_base = p1_base;
            }
        }

        public static void Write_Scale(IEngineConf engineConf, FeatureVector fv, string fvDirectory)
        {
            File.WriteAllText(Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv00ScaleInFvDir")), Format_FeatureVector_Scale.Format_Text(fv));
        }

        public static void Write_KK(IEngineConf engineConf, FeatureVector fv, string fvDirectory)
        {
            File.WriteAllText(Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv01KKInFvDir")), Format_FeatureVector_KK.Format_KK(fv));
        }

        public static void Write_KP(IEngineConf engineConf,  FeatureVector fv, string fvDirectory)
        {
            string filepathW1 = Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv02n1pKPInFvDir"));
            string filepathW2 = Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv03n2pKPInFvDir"));
            //----------------------------------------
            // 1P玉
            //----------------------------------------
            {
                File.WriteAllText(filepathW1, Format_FeatureVector_KP.Format_KP(fv, Playerside.P1));
                //sb.AppendLine(filepathW1);
            }

            //----------------------------------------
            // 2p玉
            //----------------------------------------
            {
                File.WriteAllText(filepathW2, Format_FeatureVector_KP.Format_KP(fv, Playerside.P2));
                //sb.AppendLine(filepathW2);
            }
        }

        /// <summary>
        /// PP 盤上の駒
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="fvDirectory"></param>
        public static void Write_PP_Banjo(IEngineConf engineConf, FeatureVector fv, string fvDirectory)
        {
            // P1が盤上の駒
            {
                PpItem_P1[] p1Items = new PpItem_P1[]{
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv04PP1pInFvDir")),"1P歩",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv05PP1pInFvDir")),"1P香",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv06pp1pInFvDir")),"1P桂",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv07pp1pInFvDir")),"1P銀",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv08pp1pInFvDir")),"1P金",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv09pp1pInFvDir")),"1P飛",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv10pp1pInFvDir")),"1P角",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),

                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv18pp2pInFvDir")),"2P歩",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv19pp2pInFvDir")),"2P香",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv20pp2pInFvDir")),"2P桂",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv21pp2pInFvDir")),"2P銀",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv22pp2pInFvDir")),"2P金",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv23pp2pInFvDir")),"2P飛",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv24pp2pInFvDir")),"2P角",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                };
                foreach (PpItem_P1 item in p1Items)
                {
                    File.WriteAllText(item.Filepath, Format_FeatureVector_PP_P1Banjo.Format_PP_P1Banjo(fv, item.Title, item.P1_base));
                    //sb.AppendLine(item.Filepath);
                }
            }
        }

        /// <summary>
        /// PP １９枚の持駒
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="fvDirectory"></param>
        public static void Write_PP_19Mai(IEngineConf engineConf, FeatureVector fv, string fvDirectory)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            //StringBuilder sb_result = new StringBuilder();

            {
                PpItem_P1[] p1Items = new PpItem_P1[]{
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv11PP1pInFvDir")),"1P歩",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv25pp2pInFvDir")),"2P歩",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____),
                };
                foreach (PpItem_P1 item in p1Items)
                {
                    File.WriteAllText(item.Filepath, Format_FeatureVector_PP_P1Moti.Format_PP_P1_Moti19Mai(fv, item.Title, item.P1_base));

                    //sb_result.AppendLine(item.Filepath);
                }
            }
        }


        /// <summary>
        /// PP 5枚の持駒
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="fvDirectory"></param>
        public static void Write_PP_5Mai(IEngineConf engineConf, FeatureVector fv, string fvDirectory)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            //StringBuilder sb = new StringBuilder();

            {
                PpItem_P1[] p1Items = new PpItem_P1[]{
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv12PP1pInFvDir")),"1P香",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv13PP1pInFvDir")),"1P桂",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv14PP1pInFvDir")),"1P銀",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv15PP1pInFvDir")),"1P金",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv26PP2pInFvDir")),"2P香",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv27PP2pInFvDir")),"2P桂",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv28PP2pInFvDir")),"2P銀",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv29PP2pInFvDir")),"2P金",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____),
                };
                foreach (PpItem_P1 item in p1Items)
                {
                    File.WriteAllText(item.Filepath, Format_FeatureVector_PP_P1Moti.Format_PP_P1Moti_5Mai(fv, item.Title, item.P1_base));
                    //sb.AppendLine(item.Filepath);
                }
            }
        }

        /// <summary>
        /// PP ３枚の持駒
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="fvDirectory"></param>
        public static void Write_PP_3Mai(IEngineConf engineConf, FeatureVector fv, string fvDirectory)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            //StringBuilder sb = new StringBuilder();

            {
                PpItem_P1[] p1Items = new PpItem_P1[]{
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv16pp1pInFvDir")),"1P飛",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv17pp1pInFvDir")),"1P角",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv30pp2pInFvDir")),"2P飛",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__),
                    new PpItem_P1( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv31pp2pInFvDir")),"2P角",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___),
                };
                foreach (PpItem_P1 item in p1Items)
                {
                    File.WriteAllText(item.Filepath, Format_FeatureVector_PP_P1Moti.Format_PP_P1Moti_3Mai(fv, item.Title, item.P1_base));
                    //sb.AppendLine(item.Filepath);
                }
            }
        }
    }
}
