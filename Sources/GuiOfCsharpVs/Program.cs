using System;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P699_Form_______;

namespace Grayscale.P803GuiCsharpVs.L500Gui
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ILogTag logTag = LogTags.CsharpGuiDefault;
            MainGui_CsharpVsImpl mainGuiVs = new MainGui_CsharpVsImpl();

            //↓ [STAThread]指定のあるメソッドで フォームを作成してください。
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainGuiVs.OwnerForm = new Form1_Shogi(mainGuiVs);
            //↑ [STAThread]指定のあるメソッドで フォームを作成してください。

            mainGuiVs.Load_AsStart(logTag);
            mainGuiVs.LaunchForm_AsBody(logTag);

        }
    }
}
