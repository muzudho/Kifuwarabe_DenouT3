using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P163KifuCsa.I250Struct;
using Grayscale.P163KifuCsa.L250Struct;
using Grayscale.P521FeatureVect.I500Struct;
using Grayscale.P521FeatureVect.L500Struct;
using Grayscale.P523UtilFv.L480UtilFvEdit;
using Grayscale.P523UtilFv.L490UtilFvFormat;
using Grayscale.P523UtilFv.L491UtilFvIo;
using Nett;

namespace Grayscale.P720_FvWriter___
{
    public partial class Uc_Main : UserControl
    {

        public Uc_Main()
        {
            InitializeComponent();
        }

        private void btnWriter_Click(object sender, EventArgs e)
        {
        }

        private void btnMakeRandom_Click(object sender, EventArgs e)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
            string filepath = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv00KomawariSample"));

            FeatureVector fv = new FeatureVectorImpl();
            Util_FeatureVectorEdit.Make_Random(fv);

            File.WriteAllText(filepath, Format_FeatureVector_Komawari.Format_Text(fv));
            MessageBox.Show("サンプルファイルを書き出しました。\n" +
                "filepath=[" + filepath + "]");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            IErrorController errH = ErrorControllerReference.TestProgram;

            string filepathR = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv00Komawari"));
            string filepathR_KK = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv01KK"));
            string filepathW = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv2Sample"));

            FeatureVector fv = new FeatureVectorImpl();

            if (Util_FeatureVectorInput.Make_FromFile_Komawari(fv, filepathR))
            {
            }

            if (Util_FeatureVectorInput.Make_FromFile_KK(fv, filepathR_KK, errH))
            {
            }

            File.WriteAllText(filepathW, Format_FeatureVector_Komawari.Format_Text(fv));

#if DEBUG
            MessageBox.Show("FVファイルを読み込んで、書き出しました。\n" +
                "readFilepath=["+filepathR+"]"+
                "writeFilepath=[" + filepathW + "]");
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
            foreach(CsaKifuMove csaMove in moveList)
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
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
            string dataDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataDirectory"));

            FeatureVector fv = new FeatureVectorImpl();
            Util_FeatureVectorInput.Make_FromFile_Komawari(fv, Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv00Komawari")));

            Util_FeatureVectorOutput.Write_KK(fv, Path.Combine(profilePath, dataDirectory));
        }

        /// <summary>
        /// 1P玉KP書出しボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_1pKP_Write_Click(object sender, EventArgs e)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
            string dataDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataDirectory"));

            FeatureVector fv = new FeatureVectorImpl();
            Util_FeatureVectorInput.Make_FromFile_Komawari(fv, Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv00Komawari")));

            Util_FeatureVectorOutput.Write_KP(fv, Path.Combine(profilePath, dataDirectory));
        }

        /// <summary>
        /// [PP書出し]ボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteFvPp_Click(object sender, EventArgs e)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
            string dataDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataDirectory"));

            FeatureVector fv = new FeatureVectorImpl();
            Util_FeatureVectorInput.Make_FromFile_Komawari(fv, Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv00Komawari")));

            Util_FeatureVectorOutput.Write_PP_Banjo(fv, Path.Combine(profilePath, dataDirectory));
            Util_FeatureVectorOutput.Write_PP_19Mai(fv, Path.Combine(profilePath, dataDirectory));
            Util_FeatureVectorOutput.Write_PP_5Mai(fv, Path.Combine(profilePath, dataDirectory));
            Util_FeatureVectorOutput.Write_PP_3Mai(fv, Path.Combine(profilePath, dataDirectory));
        }

        /// <summary>
        /// fv_00_Scale.csv書き出し。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteFvScale_Click(object sender, EventArgs e)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            string filepathW = Path.Combine( Application.StartupPath, Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv00Scale")));
            MessageBox.Show("filepathW=[" + filepathW + "]", "fv_00_Scale.csv書き出し。");

            FeatureVector fv = new FeatureVectorImpl();
            fv.SetBairitu_NikomaKankeiPp( 0.002f);//仮の初期値。

            File.WriteAllText(filepathW, Format_FeatureVector_Scale.Format_Text(fv));
        }


    }
}
