using System;
using System.IO;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Engine.Configuration;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Grayscale.Kifuwarakaku.Entities.Features;
using Nett;

namespace Grayscale.Kifuwarakaku.GuiOfKifuCsa
{
    public partial class Uc_Main : UserControl
    {
        private CsaKifu CsaKifu { get; set; }

        public Uc_Main()
        {
            this.CsaKifu = new CsaKifuImpl();
            InitializeComponent();

            EngineConf = new EngineConf();
        }

        IEngineConf EngineConf { get; set; }

        private void btnRead_Click(object sender, EventArgs e)
        {
            this.CsaKifu = Util_Csa.ReadFile(this.txtKifuFilepath.Text);

            string filepath_out = Path.Combine(this.txtKifuFilepath.Text, EngineConf.GetResourceBasename("N18KifuCsaLogBasename"));
            MessageBox.Show($"終わった。デバッグ出力をする☆\nファイルパス=[{filepath_out}]", "かんりょう");
            //デバッグ用にファイルを書き出します。
            CsaKifuWriterImpl.WriteForDebug(filepath_out, this.CsaKifu);
        }
    }
}
