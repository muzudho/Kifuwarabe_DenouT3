using System;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.Kifuwarakaku.GuiOfCsharp.Features;

namespace Grayscale.Kifuwarakaku.GuiOfCsharpVs.Features
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainGui_CsharpVsImpl mainGuiVs = new MainGui_CsharpVsImpl();

            //↓ [STAThread]指定のあるメソッドで フォームを作成してください。
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainGuiVs.OwnerForm = new Form1_Shogi(mainGuiVs);
            //↑ [STAThread]指定のあるメソッドで フォームを作成してください。

            mainGuiVs.Load_AsStart();
            mainGuiVs.LaunchForm_AsBody();

        }
    }
}
