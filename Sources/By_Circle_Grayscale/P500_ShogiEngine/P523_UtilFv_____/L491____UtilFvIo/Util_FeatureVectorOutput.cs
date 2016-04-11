using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P521_FeatureVect.L500____Struct;
using Grayscale.P523_UtilFv_____.L490____UtilFvFormat;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Grayscale.P523_UtilFv_____.L491____UtilFvIo
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

        public static void Write_Scale(FeatureVector fv, string directory)
        {
            string filepathW = Path.Combine(directory, "fv_00_Scale.csv");
            File.WriteAllText(filepathW, Format_FeatureVector_Scale.Format_Text(fv));
        }

        public static void Write_KK(FeatureVector fv, string directory)
        {
            string filepathW = Path.Combine( directory, "fv_01_KK.csv");
            File.WriteAllText(filepathW, Format_FeatureVector_KK.Format_KK(fv));

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(filepathW);
            //MessageBox.Show("ファイルを書き出しました。\n" + sb.ToString());
        }

        public static void Write_KP(FeatureVector fv, string directory)
        {
            //StringBuilder sb = new StringBuilder();

            string filepathW1 = Path.Combine( directory, "fv_02_1pKP.csv");
            string filepathW2 = Path.Combine( directory, "fv_03_2pKP.csv");
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

            //MessageBox.Show("ファイルを書き出しました。\n" + sb.ToString());
        }

        /// <summary>
        /// PP 盤上の駒
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="directory"></param>
        public static void Write_PP_Banjo(FeatureVector fv, string directory)
        {
            //StringBuilder sb = new StringBuilder();

            // P1が盤上の駒
            {
                PpItem_P1[] p1Items = new PpItem_P1[]{
                    new PpItem_P1( Path.Combine( directory, "fv_04_PP_1p____Fu__.csv"),"1P歩",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new PpItem_P1( Path.Combine( directory, "fv_05_PP_1p____Kyo_.csv"),"1P香",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new PpItem_P1( Path.Combine( directory, "fv_06_pp_1p____Kei_.csv"),"1P桂",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new PpItem_P1( Path.Combine( directory, "fv_07_pp_1p____Gin_.csv"),"1P銀",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new PpItem_P1( Path.Combine( directory, "fv_08_pp_1p____Kin_.csv"),"1P金",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new PpItem_P1( Path.Combine( directory, "fv_09_pp_1p____Hi__.csv"),"1P飛",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new PpItem_P1( Path.Combine( directory, "fv_10_pp_1p____Kaku.csv"),"1P角",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                    new PpItem_P1( Path.Combine( directory, "fv_18_pp_2p____Fu__.csv"),"2P歩",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new PpItem_P1( Path.Combine( directory, "fv_19_pp_2p____Kyo_.csv"),"2P香",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new PpItem_P1( Path.Combine( directory, "fv_20_pp_2p____Kei_.csv"),"2P桂",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new PpItem_P1( Path.Combine( directory, "fv_21_pp_2p____Gin_.csv"),"2P銀",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new PpItem_P1( Path.Combine( directory, "fv_22_pp_2p____Kin_.csv"),"2P金",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new PpItem_P1( Path.Combine( directory, "fv_23_pp_2p____Hi__.csv"),"2P飛",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new PpItem_P1( Path.Combine( directory, "fv_24_pp_2p____Kaku.csv"),"2P角",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                };
                foreach (PpItem_P1 item in p1Items)
                {
                    File.WriteAllText(item.Filepath, Format_FeatureVector_PP_P1Banjo.Format_PP_P1Banjo(fv, item.Title, item.P1_base));
                    //sb.AppendLine(item.Filepath);
                }
            }

            //MessageBox.Show("ファイルを書き出しました。\n" + sb.ToString());
        }

        /// <summary>
        /// PP １９枚の持駒
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="directory"></param>
        public static void Write_PP_19Mai(FeatureVector fv, string directory)
        {
            //StringBuilder sb_result = new StringBuilder();

            {
                PpItem_P1[] p1Items = new PpItem_P1[]{
                    new PpItem_P1( Path.Combine( directory,"fv_11_PP_1pMotiFu__.csv"),"1P歩",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____),
                    new PpItem_P1( Path.Combine( directory,"fv_25_pp_2pMotiFu__.csv"),"2P歩",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____),
                };
                foreach (PpItem_P1 item in p1Items)
                {
                    File.WriteAllText(item.Filepath, Format_FeatureVector_PP_P1Moti.Format_PP_P1_Moti19Mai(fv, item.Title, item.P1_base));

                    //sb_result.AppendLine(item.Filepath);
                }
            }

            //MessageBox.Show("ファイルを書き出しました。\n" + sb_result.ToString());
        }


        /// <summary>
        /// PP 5枚の持駒
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="directory"></param>
        public static void Write_PP_5Mai(FeatureVector fv, string directory)
        {
            //StringBuilder sb = new StringBuilder();

            {
                PpItem_P1[] p1Items = new PpItem_P1[]{
                    new PpItem_P1( Path.Combine( directory,"fv_12_PP_1pMotiKyo_.csv"),"1P香",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____),
                    new PpItem_P1( Path.Combine( directory,"fv_13_pp_1pMotiKei_.csv"),"1P桂",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____),
                    new PpItem_P1( Path.Combine( directory,"fv_14_pp_1pMotiGin_.csv"),"1P銀",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____),
                    new PpItem_P1( Path.Combine( directory,"fv_15_pp_1pMotiKin_.csv"),"1P金",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____),
                    new PpItem_P1( Path.Combine( directory,"fv_26_pp_2pMotiKyo_.csv"),"2P香",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____),
                    new PpItem_P1( Path.Combine( directory,"fv_27_pp_2pMotiKei_.csv"),"2P桂",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____),
                    new PpItem_P1( Path.Combine( directory,"fv_28_pp_2pMotiGin_.csv"),"2P銀",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____),
                    new PpItem_P1( Path.Combine( directory,"fv_29_pp_2pMotiKin_.csv"),"2P金",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____),
                };
                foreach (PpItem_P1 item in p1Items)
                {
                    File.WriteAllText(item.Filepath, Format_FeatureVector_PP_P1Moti.Format_PP_P1Moti_5Mai(fv, item.Title, item.P1_base));
                    //sb.AppendLine(item.Filepath);
                }
            }

            //MessageBox.Show("ファイルを書き出しました。\n" + sb.ToString());
        }

        /// <summary>
        /// PP ３枚の持駒
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="directory"></param>
        public static void Write_PP_3Mai(FeatureVector fv,string directory)
        {
            //StringBuilder sb = new StringBuilder();

            {
                PpItem_P1[] p1Items = new PpItem_P1[]{
                    new PpItem_P1( Path.Combine( directory,"fv_16_pp_1pMotiHi__.csv"),"1P飛",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__),
                    new PpItem_P1( Path.Combine( directory,"fv_17_pp_1pMotiKaku.csv"),"1P角",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___),
                    new PpItem_P1( Path.Combine( directory,"fv_30_pp_2pMotiHi__.csv"),"2P飛",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__),
                    new PpItem_P1( Path.Combine( directory,"fv_31_pp_2pMotiKaku.csv"),"2P角",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___),
                };
                foreach (PpItem_P1 item in p1Items)
                {
                    File.WriteAllText(item.Filepath, Format_FeatureVector_PP_P1Moti.Format_PP_P1Moti_3Mai(fv, item.Title, item.P1_base));
                    //sb.AppendLine(item.Filepath);
                }
            }

            //MessageBox.Show("ファイルを書き出しました。\n" + sb.ToString());
        }


    }
}
