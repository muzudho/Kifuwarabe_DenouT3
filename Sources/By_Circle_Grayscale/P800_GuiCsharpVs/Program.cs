using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P693_ShogiGui___.L500____GUI;
using Grayscale.P699_Form_______;
using System;
using System.Windows.Forms;

namespace Grayscale.P803_GuiCsharpVs.L500____Gui
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            KwErrorHandler errH = Util_OwataMinister.CsharpGui_DEFAULT;
            MainGui_CsharpVsImpl mainGuiVs = new MainGui_CsharpVsImpl();

            //↓ [STAThread]指定のあるメソッドで フォームを作成してください。
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainGuiVs.OwnerForm = new Form1_Shogi(mainGuiVs);
            //↑ [STAThread]指定のあるメソッドで フォームを作成してください。

            mainGuiVs.Load_AsStart(errH);
            mainGuiVs.LaunchForm_AsBody(errH);

        }
    }
}
