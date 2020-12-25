using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Engine.Configuration;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using Nett;

namespace Grayscale.Kifuwarakaku.GuiOfFvWriter.Features
{
    public partial class Uc_Main : UserControl
    {

        public Uc_Main()
        {
            InitializeComponent();

            EngineConf = new EngineConf();
        }

        IEngineConf EngineConf { get; set; }

        private void btnWriter_Click(object sender, EventArgs e)
        {
        }

        private void btnMakeRandom_Click(object sender, EventArgs e)
        {
            string filepath = EngineConf.GetResourceFullPath("Fv00KomawariSample");

            FeatureVector fv = new FeatureVectorImpl();
            Util_FeatureVectorEdit.Make_Random(fv);

            File.WriteAllText(filepath, Format_FeatureVector_Komawari.Format_Text(fv));
            MessageBox.Show($"サンプルファイルを書き出しました。\nfilepath=[{filepath}]");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string filepathR = EngineConf.GetResourceFullPath("Fv00Komawari");
            string filepathR_KK = EngineConf.GetResourceFullPath("Fv01KK");
            string filepathW = EngineConf.GetResourceFullPath("Fv2Sample");

            FeatureVector fv = new FeatureVectorImpl();

            if (Util_FeatureVectorInput.Make_FromFile_Komawari(fv, filepathR))
            {
            }

            if (Util_FeatureVectorInput.Make_FromFile_KK(fv, filepathR_KK))
            {
            }

            File.WriteAllText(filepathW, Format_FeatureVector_Komawari.Format_Text(fv));

#if DEBUG
            MessageBox.Show($"FVファイルを読み込んで、書き出しました。\nreadFilepath=[{filepathR}]writeFilepath=[{filepathW}]");
#endif
        }

        /// <summary>
        /// 棋譜読込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadKifu_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtKifuFilepath.Text))
            {
                goto gt_EndMethod;
            }


            CsaKifu csaKifu = Util_Csa.ReadFile(this.txtKifuFilepath.Text);

            StringBuilder sb = new StringBuilder();
            List<CsaKifuMove> moveList = csaKifu.MoveList;
            foreach (CsaKifuMove csaMove in moveList)
            {
                sb.Append(csaMove.OptionTemezumi);
                sb.Append("手目 ");
                sb.Append(csaMove.DestinationMasu);
                sb.Append(" ");
                sb.Append(csaMove.Second);
                sb.Append(" ");
                sb.Append(csaMove.Sengo);
                sb.Append(" ");
                sb.Append(csaMove.SourceMasu);
                sb.Append(" ");
                sb.Append(csaMove.Syurui);
                sb.AppendLine();
            }
            this.txtMoveList.Text = sb.ToString();

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// 「ゼロクリアーでファイル作成」（タイアド・ベクター）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMakeZero_Tv_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// リザルト・ベクターを０クリアーで作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateRv0Clear_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 表変形KK 書き出し（旧Fvから）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_HyoHenkeiFvKK_Click(object sender, EventArgs e)
        {
            FeatureVector fv = new FeatureVectorImpl();
            Util_FeatureVectorInput.Make_FromFile_Komawari(fv, EngineConf.GetResourceFullPath("Fv00Komawari"));

            Util_FeatureVectorOutput.Write_KK(EngineConf, fv, EngineConf.DataDirectory);
        }

        /// <summary>
        /// 1P玉KP書出しボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_1pKP_Write_Click(object sender, EventArgs e)
        {
            FeatureVector fv = new FeatureVectorImpl();
            Util_FeatureVectorInput.Make_FromFile_Komawari(fv, EngineConf.GetResourceFullPath("Fv00Komawari"));

            Util_FeatureVectorOutput.Write_KP(EngineConf, fv, EngineConf.DataDirectory);
        }

        /// <summary>
        /// [PP書出し]ボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteFvPp_Click(object sender, EventArgs e)
        {
            FeatureVector fv = new FeatureVectorImpl();
            Util_FeatureVectorInput.Make_FromFile_Komawari(fv, EngineConf.GetResourceFullPath("Fv00Komawari"));

            Util_FeatureVectorOutput.Write_PP_Banjo(EngineConf, fv, EngineConf.DataDirectory);
            Util_FeatureVectorOutput.Write_PP_19Mai(EngineConf, fv, EngineConf.DataDirectory);
            Util_FeatureVectorOutput.Write_PP_5Mai(EngineConf, fv, EngineConf.DataDirectory);
            Util_FeatureVectorOutput.Write_PP_3Mai(EngineConf, fv, EngineConf.DataDirectory);
        }

        /// <summary>
        /// fv_00_Scale.csv書き出し。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteFvScale_Click(object sender, EventArgs e)
        {
            string filepathW = EngineConf.GetResourceFullPath("Fv00Scale");
            MessageBox.Show($"filepathW=[{filepathW}]", "fv_00_Scale.csv書き出し。");

            FeatureVector fv = new FeatureVectorImpl();
            fv.SetBairitu_NikomaKankeiPp(0.002f);//仮の初期値。

            File.WriteAllText(filepathW, Format_FeatureVector_Scale.Format_Text(fv));
        }
    }
}
