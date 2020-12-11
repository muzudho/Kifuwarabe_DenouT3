﻿using Grayscale.P163KifuCsa.I250Struct;
using Grayscale.P163KifuCsa.L250Struct;
using Grayscale.P163KifuCsa.L500Writer;
using System;
using System.Windows.Forms;

namespace Grayscale.P169_Form_______
{
    public partial class Uc_Main : UserControl
    {
        private CsaKifu CsaKifu { get; set; }

        public Uc_Main()
        {
            this.CsaKifu = new CsaKifuImpl();
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            this.CsaKifu = Util_Csa.ReadFile( this.txtKifuFilepath.Text );

            string filepath_out = Path.Combine( this.txtKifuFilepath.Text, toml.Get<TomlTable>("Resources").Get<string>("N18KifuCsaLogBasename"));
            MessageBox.Show("終わった。デバッグ出力をする☆\nファイルパス=[" + filepath_out + "]", "かんりょう");
            //デバッグ用にファイルを書き出します。
            CsaKifuWriterImpl.WriteForDebug(filepath_out, this.CsaKifu);
        }
    }
}
