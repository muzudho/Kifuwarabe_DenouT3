using System;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Engine.Configuration;
using Grayscale.Kifuwarakaku.Entities;
using Grayscale.Kifuwarakaku.GuiOfSgSyugoTest.Features;

namespace Grayscale.Kifuwarakaku.GuiOfSgSyugoTest
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var engineConf = new EngineConf();
            EntitiesLayer.Implement(engineConf);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Ui_SyugoronTestForm form1 = new Ui_SyugoronTestForm();
            form1.Text = "集合論ライブラリー テスター 将棋版";
            Ui_SyugoronTestPanel panel = form1.Ui_SyugoronTestPanel;

            // サンプルで少し入れておく。
            panel.SyFuncDictionary.Add("右上一直線升たち", Sample_Func.func右上一直線升たち);
            panel.SyFuncDictionary.Add("右下一直線升たち", Sample_Func.func右下一直線升たち);
            panel.SyFuncDictionary.Add("左下一直線升たち", Sample_Func.func左下一直線升たち);
            panel.SyFuncDictionary.Add("左上一直線升たち", Sample_Func.func左上一直線升たち);




            Application.Run(form1);
        }
    }
}
