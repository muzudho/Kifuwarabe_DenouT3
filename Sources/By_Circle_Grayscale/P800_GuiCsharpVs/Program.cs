using Grayscale.P003Log.I500Struct;
using Grayscale.P003Log.L500Struct;
using Grayscale.P693ShogiGui.L500GUI;
using Grayscale.P699_Form_______;
using System;
using System.Windows.Forms;

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
            IErrorController errH = ErrorControllerReference.CsharpGuiDefault;
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
