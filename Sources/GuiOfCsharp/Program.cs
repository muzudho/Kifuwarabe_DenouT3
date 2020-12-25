// 進行が停止するテストを含むデバッグモードです。
#define DEBUG_STOPPABLE

using System;
using System.IO;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Engine.Configuration;
using Grayscale.Kifuwarakaku.Entities;
using Nett;

namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
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

            MainGui_CsharpImpl mainGui = new MainGui_CsharpImpl(engineConf);//new ShogiEngineVsClientImpl(this)

            //↓ [STAThread]指定のあるメソッドで フォームを作成してください。
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainGui.OwnerForm = new Form1_Shogi(mainGui);
            //↑ [STAThread]指定のあるメソッドで フォームを作成してください。

            mainGui.Load_AsStart();
            mainGui.WidgetLoaders.Add(new WidgetsLoader_CsharpImpl(engineConf.GetResourceFullPath("Shogiban01Widgets"), mainGui));
            mainGui.WidgetLoaders.Add(new WidgetsLoader_CsharpImpl(engineConf.GetResourceFullPath("Console02Widgets"), mainGui));
            mainGui.LaunchForm_AsBody();
        }

    }
}
