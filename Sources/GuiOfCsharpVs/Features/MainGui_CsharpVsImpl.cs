using System.IO;
using System.Text;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.GuiOfCsharp.Features;
using Nett;

namespace Grayscale.Kifuwarakaku.GuiOfCsharpVs.Features
{
    /// <summary>
    /// 将棋盤ＧＵＩ VS（C#）用
    /// </summary>
    public class MainGui_CsharpVsImpl : MainGui_CsharpImpl, MainGui_Csharp
    {


        public MainGui_CsharpVsImpl(IEngineConf engineConf) : base(engineConf)
        {
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 手番が替わったときの挙動を、ここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        public override void ChangedTurn()
        {
            this.Link_Server.EngineClient.OnChangedTurn(this.Link_Server.Model_Taikyoku.Kifu);
        }

        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        public override void Shutdown()
        {
            this.Link_Server.EngineClient.Send_Shutdown();
        }

        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        public override void Logdase()
        {
            this.Link_Server.EngineClient.Send_Logdase();
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 将棋エンジンを起動します。
        /// ************************************************************************************************************************
        /// </summary>
        public override void Start_ShogiEngine(string shogiEngineFilePath)
        {
            this.Link_Server.EngineClient.Start(shogiEngineFilePath);
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Usi();
        }

        /// <summary>
        /// コンピューターの先手
        /// (2020-11-21 sat) virtual --> override.
        /// </summary>
        public override void Do_ComputerSente()
        {
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Position(
                Util_KirokuGakari.ToSfen_PositionCommand(this.Link_Server.Model_Taikyoku.Kifu));
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Go();
        }


        /// <summary>
        /// このアプリケーションソフトの開始時の処理。
        /// </summary>
        public new void Load_AsStart()
        {
            base.Load_AsStart();

            this.Data_Settei_Csv.Read_Add(this.EngineConf.GetResourceFullPath("DataSetteiVsCsv"), Encoding.UTF8);
            this.Data_Settei_Csv.DebugOut();
            this.WidgetLoaders.Add(new WidgetsLoader_CsharpVsImpl(this.EngineConf.GetResourceFullPath("Vs03Widgets"), this));
        }

    }
}
