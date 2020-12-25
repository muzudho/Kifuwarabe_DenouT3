using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Engine.Configuration;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;

#if DEBUG || LEARN
#endif

namespace Grayscale.Kifuwarakaku.GuiOfSpeedKeisok
{

    public partial class Uc_Main : UserControl
    {
        public Uc_Main()
        {
            this.FeatureVector = new FeatureVectorImpl();
            {
                KifuTree kifu_newHirate;
                Util_FvLoad.CreateKifuTree(out kifu_newHirate);
                this.Kifu = kifu_newHirate;
            }
            this.Src_Sky = ((KifuNode)this.Kifu.CurNode).Value.KyokumenConst;
            InitializeComponent();

            EngineConf = new EngineConf();
        }

        IEngineConf EngineConf { get; set; }

        private class KeisokuResult
        {
            public string Name { get; set; }

#if DEBUG || LEARN
            public string Utiwake { get; set; }
#endif
            public TimeSpan Time { get; set; }

            public KeisokuResult(string name,
#if DEBUG || LEARN
 string utiwake,
#endif
                TimeSpan time)
            {
                this.Name = name;
#if DEBUG || LEARN
                this.Utiwake = utiwake;
#endif
                this.Time = time;
            }
        }

        public SkyConst Src_Sky { get; set; }
        public FeatureVector FeatureVector { get; set; }

        public KifuTree Kifu { get; set; }

        private KeisokuResult Keisoku(Hyokakansu handan1)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            float score;
#if DEBUG || LEARN
            KyHyokaMeisai_Koumoku meisai;
#endif
            handan1.Evaluate(
                out score,
#if DEBUG || LEARN
                out meisai,
#endif
                this.Src_Sky,
                this.FeatureVector
                );

            watch.Stop();

            KeisokuResult result = new KeisokuResult(
                handan1.Name.ToString(),
#if DEBUG || LEARN
                meisai.Utiwake,
#endif
                watch.Elapsed);
            return result;
        }

        /// <summary>
        /// 計測ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKeisoku_Click(object sender, EventArgs e)
        {

            List<KeisokuResult> list = new List<KeisokuResult>();
            list.Add(this.Keisoku(new Hyokakansu_Komawari()));
            list.Add(this.Keisoku(new Hyokakansu_NikomaKankeiPp()));

            TimeSpan total = new TimeSpan();

            StringBuilder sb = new StringBuilder();
            foreach (KeisokuResult result in list)
            {
                sb.AppendLine("----------------------------------------");
                sb.AppendLine(result.Name.ToString());
                sb.Append("    ");
                sb.AppendLine(result.Time.ToString());
                sb.Append("    ");
#if DEBUG
                sb.AppendLine(result.Utiwake);
#else
                sb.AppendLine("Debugモードで実行してください。");
#endif

                total += result.Time;
            }

            {
                sb.AppendLine("----------------------------------------");
                sb.AppendLine("トータル時間");
                sb.Append("    ");
                sb.AppendLine(total.ToString());
            }

            this.txtResult.Text = sb.ToString();
        }

        /// <summary>
        /// 開くボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFv_Click(object sender, EventArgs e)
        {
            if ("" != this.txtFvFilepath.Text)
            {
                this.openFileDialog1.InitialDirectory = Path.GetDirectoryName(this.txtFvFilepath.Text);
                this.openFileDialog1.FileName = Path.GetFileName(this.txtFvFilepath.Text);
            }
            else
            {
                this.openFileDialog1.InitialDirectory = Application.StartupPath;
            }

            DialogResult result = this.openFileDialog1.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:

                    this.txtFvFilepath.Text = this.openFileDialog1.FileName;

                    StringBuilder sb_result = new StringBuilder();
                    // フィーチャー・ベクターの外部ファイルを開きます。
                    sb_result.Append(Util_FvLoad.OpenFv(EngineConf, this.FeatureVector, this.txtFvFilepath.Text));

                    this.txtResult.Text = sb_result.ToString();

                    break;
                default:
                    break;
            }

            //gt_EndMethod:
            //    ;

        }
    }
}
