using System.IO;
using System.Text;
using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.P341Ittesasu.L250OperationA;
using Grayscale.P693ShogiGui.I500Gui;
using Grayscale.P693ShogiGui.L500GUI;
using Grayscale.P803GuiCsharpVs.L492Widget;
using Nett;

namespace Grayscale.P803GuiCsharpVs.L500Gui
{
    /// <summary>
    /// 将棋盤ＧＵＩ VS（C#）用
    /// </summary>
    public class MainGui_CsharpVsImpl : MainGui_CsharpImpl, MainGui_Csharp
    {


        public MainGui_CsharpVsImpl()
        {
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 手番が替わったときの挙動を、ここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        public override void ChangedTurn( ILogTag logTag)
        {
            this.Link_Server.EngineClient.OnChangedTurn(this.Link_Server.Model_Taikyoku.Kifu, logTag);
        }

        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        public override void Shutdown( ILogTag logTag)
        {
            this.Link_Server.EngineClient.Send_Shutdown(logTag);
        }

        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        public override void Logdase( ILogTag logTag)
        {
            this.Link_Server.EngineClient.Send_Logdase(logTag);
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 将棋エンジンを起動します。
        /// ************************************************************************************************************************
        /// </summary>
        public override void Start_ShogiEngine(string shogiEngineFilePath, ILogTag logTag)
        {
            this.Link_Server.EngineClient.Start(shogiEngineFilePath);
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Usi(logTag);
        }

        /// <summary>
        /// コンピューターの先手
        /// (2020-11-21 sat) virtual --> override.
        /// </summary>
        public override void Do_ComputerSente(ILogTag logTag)
        {
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Position(
                Util_KirokuGakari.ToSfen_PositionCommand(this.Link_Server.Model_Taikyoku.Kifu), logTag);
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Go(logTag);
        }


        /// <summary>
        /// このアプリケーションソフトの開始時の処理。
        /// </summary>
        public new void Load_AsStart(ILogTag logTag)
        {
            base.Load_AsStart(logTag);

            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            this.Data_Settei_Csv.Read_Add(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataSetteiVsCsv")), Encoding.UTF8);
            this.Data_Settei_Csv.DebugOut();
            this.WidgetLoaders.Add(new WidgetsLoader_CsharpVsImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Vs03Widgets")), this));
        }

    }
}
