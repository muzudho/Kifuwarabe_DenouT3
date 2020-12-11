// 進行が停止するテストを含むデバッグモードです。
#define DEBUG_STOPPABLE

using Grayscale.P003Log.I500Struct;
using Grayscale.P003Log.L500Struct;
using Grayscale.P027Settei.L500Struct;
using Grayscale.P693ShogiGui.L492Widgets;
using Grayscale.P693ShogiGui.L500GUI;
using System;
using System.Windows.Forms;
using Nett;
using System.IO;

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
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            IErrorController errH = ErrorControllerReference.CsharpGuiDefault;
            MainGui_CsharpImpl mainGui = new MainGui_CsharpImpl();//new ShogiEngineVsClientImpl(this)

            //↓ [STAThread]指定のあるメソッドで フォームを作成してください。
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainGui.OwnerForm = new Form1_Shogi(mainGui);
            //↑ [STAThread]指定のあるメソッドで フォームを作成してください。

            mainGui.Load_AsStart(errH);
            mainGui.WidgetLoaders.Add(new WidgetsLoader_CsharpImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Shogiban01Widgets")), mainGui));
            mainGui.WidgetLoaders.Add(new WidgetsLoader_CsharpImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Console02Widgets")), mainGui));
            mainGui.LaunchForm_AsBody(errH);
        }

    }
}
