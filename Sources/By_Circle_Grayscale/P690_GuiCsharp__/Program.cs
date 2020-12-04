// 進行が停止するテストを含むデバッグモードです。
#define DEBUG_STOPPABLE

using Grayscale.P003Log.I500Struct;
using Grayscale.P003Log.L500Struct;
using Grayscale.P027Settei.L500Struct;
using Grayscale.P693ShogiGui.L492Widgets;
using Grayscale.P693ShogiGui.L500GUI;
using System;
using System.Windows.Forms;

namespace Grayscale.P699_Form_______
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
            MainGui_CsharpImpl mainGui = new MainGui_CsharpImpl();//new ShogiEngineVsClientImpl(this)

            //↓ [STAThread]指定のあるメソッドで フォームを作成してください。
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainGui.OwnerForm = new Form1_Shogi(mainGui);
            //↑ [STAThread]指定のあるメソッドで フォームを作成してください。

            mainGui.Load_AsStart(errH);
            mainGui.WidgetLoaders.Add(new WidgetsLoader_CsharpImpl(Const_Filepath.ENGINE_TO_DATA + "data_widgets_01_shogiban.csv", mainGui));
            mainGui.WidgetLoaders.Add(new WidgetsLoader_CsharpImpl(Const_Filepath.ENGINE_TO_DATA + "data_widgets_02_console.csv", mainGui));
            mainGui.LaunchForm_AsBody(errH);
        }

    }
}
