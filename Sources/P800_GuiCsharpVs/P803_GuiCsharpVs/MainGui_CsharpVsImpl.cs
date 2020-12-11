using Grayscale.P003Log.I500Struct;
using Grayscale.P341Ittesasu.L250OperationA;
using Grayscale.P693ShogiGui.I500Gui;
using Grayscale.P693ShogiGui.L500GUI;
using Grayscale.P803GuiCsharpVs.L492Widget;
using System.Text;
using Nett;
using System.IO;

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
        public override void ChangedTurn( IErrorController errH)
        {
            this.Link_Server.EngineClient.OnChangedTurn(this.Link_Server.Model_Taikyoku.Kifu, errH);
        }

        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        public override void Shutdown( IErrorController errH)
        {
            this.Link_Server.EngineClient.Send_Shutdown(errH);
        }

        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        public override void Logdase( IErrorController errH)
        {
            this.Link_Server.EngineClient.Send_Logdase(errH);
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 将棋エンジンを起動します。
        /// ************************************************************************************************************************
        /// </summary>
        public override void Start_ShogiEngine(string shogiEngineFilePath, IErrorController errH)
        {
            this.Link_Server.EngineClient.Start(shogiEngineFilePath);
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Usi(errH);
        }

        /// <summary>
        /// コンピューターの先手
        /// (2020-11-21 sat) virtual --> override.
        /// </summary>
        public override void Do_ComputerSente(IErrorController errH)
        {
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Position(
                Util_KirokuGakari.ToSfen_PositionCommand(this.Link_Server.Model_Taikyoku.Kifu), errH);
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Go(errH);
        }


        /// <summary>
        /// このアプリケーションソフトの開始時の処理。
        /// </summary>
        public new void Load_AsStart(IErrorController errH)
        {
            base.Load_AsStart(errH);

            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            this.Data_Settei_Csv.Read_Add(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataSetteiVsCsv")), Encoding.UTF8);
            this.Data_Settei_Csv.DebugOut();
            this.WidgetLoaders.Add(new WidgetsLoader_CsharpVsImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Vs03Widgets")), this));
        }

    }
}
