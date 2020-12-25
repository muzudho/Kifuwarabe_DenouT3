using System;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Engine.Configuration;
using Grayscale.Kifuwarakaku.Entities;

namespace Grayscale.Kifuwarakaku.GuiOfKifuCsa
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
            Application.Run(new Form1());
        }
    }
}
