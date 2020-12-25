using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Grayscale.Kifuwarakaku.Entities.Features;
using Nett;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.UseCases.Features
{
    public abstract class Util_FvLoad
    {
        private class PP_P1Item
        {
            public string Filepath { get; set; }
            public int P1_base { get; set; }
            public PP_P1Item(string filepath, int p1_base)
            {
                this.Filepath = filepath;
                this.P1_base = p1_base;
            }
        }

        /// <summary>
        /// 棋譜ツリーを、平手初期局面 で準備します。
        /// </summary>
        public static void CreateKifuTree(out KifuTree kifu)
        {
            // 初期局面では、Player 1 の手番とします。
            Playerside firstPside = Playerside.P1;

            // 棋譜
            kifu = new KifuTreeImpl(
                    new KifuNodeImpl(
                        Util_Sky258A.RootMove,
                        new KyokumenWrapper(SkyConst.NewInstance(
                            Util_SkyWriter.New_Hirate(firstPside),
                            0//初期局面なので、0手目済み。
                            ))
                    )
            );
            kifu.SetProperty(Word_KifuTree.PropName_Startpos, "startpos");// 平手

            Debug.Assert(!Conv_MasuHandle.OnKomabukuro(
                Conv_SyElement.ToMasuNumber(((RO_Star)kifu.CurNode.Value.KyokumenConst.StarlightIndexOf((Finger)0).Now).Masu)
                ), "駒が駒袋にあった。");
        }


        /// <summary>
        /// フィーチャー・ベクター関連のファイルを全て開きます。
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="tv_orNull">学習でしか使いません。</param>
        /// <param name="rv_orNull">学習でしか使いません。</param>
        /// <param name="fv_komawari_file_path"></param>
        /// <returns></returns>
        public static string OpenFv(IEngineConf engineConf, FeatureVector fv, string fv_komawari_file_path)
        {
            StringBuilder sb_result = new StringBuilder();

            {//駒割
                string filepath = fv_komawari_file_path;
                if (!Util_FeatureVectorInput.Make_FromFile_Komawari(fv, filepath))
                {
                    sb_result.Append($"ファイルオープン失敗 Fv[{filepath}]。");
                    goto gt_EndMethod;
                }
                sb_result.Append("開fv。");
            }

            string fvDirectory = Path.GetDirectoryName(fv_komawari_file_path); // komawari.csvと同じフォルダー

            {//スケール
                string filepath = Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv00ScaleInFvDir"));
                if (!Util_FeatureVectorInput.Make_FromFile_Scale(fv, filepath))
                {
                    sb_result.Append($"ファイルオープン失敗 Fv[{filepath}]。");
                    goto gt_EndMethod;
                }
                sb_result.Append("開Sc。");
            }

            {//KK
                string filepath = Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv01KKInFvDir"));
                if (!Util_FeatureVectorInput.Make_FromFile_KK(fv, filepath))
                {
                    sb_result.Append($"ファイルオープン失敗 KK[{filepath}]。");
                    goto gt_EndMethod;
                }
                sb_result.Append("開KK。");
            }

            {//1pKP
                string filepath = Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv02n1pKPInFvDir"));
                if (!Util_FeatureVectorInput.Make_FromFile_KP(fv, filepath, Playerside.P1))
                {
                    sb_result.Append($"ファイルオープン失敗 1pKP[{filepath}]。");
                    goto gt_EndMethod;
                }
                sb_result.Append("開1pKP。");
            }

            {//2pKP
                string filepath = Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv03n2pKPInFvDir"));
                if (!Util_FeatureVectorInput.Make_FromFile_KP(fv, filepath, Playerside.P2))
                {
                    sb_result.Append($"ファイルオープン失敗 2pKP[{filepath}]。");
                    goto gt_EndMethod;
                }
                sb_result.Append("開2pKP。");
            }

            {//盤上の駒
                List<PP_P1Item> p1List = new List<PP_P1Item>()
                {
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv04PP1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv05PP1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv06pp1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv07pp1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv08pp1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv09pp1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv10pp1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv18pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv19pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv20pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv21pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv22pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv23pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv24pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                };

                foreach (PP_P1Item p1Item in p1List)
                {
                    if (!Util_FeatureVectorInput.Make_FromFile_PP_Banjo(fv, p1Item.Filepath, p1Item.P1_base))
                    {
                        sb_result.Append($"ファイルオープン失敗 PP_Banjo[{p1Item.Filepath}]。");
                        goto gt_EndMethod;
                    }
                    sb_result.Append($"開{Path.GetFileName(p1Item.Filepath)}。");
                }
            }

            {//１９枚の持ち駒
                List<PP_P1Item> p1Items = new List<PP_P1Item>()
                {
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv11PP1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv25pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____)
                };

                foreach (PP_P1Item ppItem in p1Items)
                {
                    if (!Util_FeatureVectorInput.Make_FromFile_PP_Moti19Mai(fv, ppItem.Filepath, ppItem.P1_base))
                    {
                        sb_result.Append($"ファイルオープン失敗 PP_Banjo[{ppItem.Filepath}]。");
                        goto gt_EndMethod;
                    }
                    sb_result.Append($"開{Path.GetFileName(ppItem.Filepath)}。");
                }
            }

            {//３枚の持駒
                List<PP_P1Item> p1Items = new List<PP_P1Item>()
                {
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv12PP1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv13PP1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv14PP1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv15PP1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv26PP2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv27PP2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv28PP2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv29PP2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____),
                };

                foreach (PP_P1Item ppItem in p1Items)
                {
                    if (!Util_FeatureVectorInput.Make_FromFile_PP_Moti3or5Mai(fv, ppItem.Filepath, ppItem.P1_base, 5))
                    {
                        sb_result.Append($"ファイルオープン失敗 PP_Banjo[{ppItem.Filepath}]。");
                        goto gt_EndMethod;
                    }
                    sb_result.Append($"開{Path.GetFileName(ppItem.Filepath)}。");
                }
            }

            {//２枚の持駒
                List<PP_P1Item> p1Items = new List<PP_P1Item>()
                {
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv16pp1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv17pp1pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv30pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__),
                    new PP_P1Item( Path.Combine(fvDirectory, engineConf.GetResourceBasename("Fv31pp2pInFvDir")),FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___),
                };

                foreach (PP_P1Item ppItem in p1Items)
                {
                    if (!Util_FeatureVectorInput.Make_FromFile_PP_Moti3or5Mai(fv, ppItem.Filepath, ppItem.P1_base, 3))
                    {
                        sb_result.Append($"ファイルオープン失敗 PP_Banjo[{ppItem.Filepath}]。");
                        goto gt_EndMethod;
                    }
                    sb_result.Append($"開{Path.GetFileName(ppItem.Filepath)}。");
                }
            }

        gt_EndMethod:
            ;
            return sb_result.ToString();
        }



    }
}
