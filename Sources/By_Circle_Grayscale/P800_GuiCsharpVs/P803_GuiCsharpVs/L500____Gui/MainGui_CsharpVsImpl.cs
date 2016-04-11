using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P341_Ittesasu___.L250____OperationA;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L500____GUI;
using Grayscale.P803_GuiCsharpVs.L492____Widget;
using System.Text;

namespace Grayscale.P803_GuiCsharpVs.L500____Gui
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
        public override void ChangedTurn( KwErrorHandler errH)
        {
            this.Link_Server.EngineClient.OnChangedTurn(this.Link_Server.Model_Taikyoku.Kifu, errH);
        }

        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        public override void Shutdown( KwErrorHandler errH)
        {
            this.Link_Server.EngineClient.Send_Shutdown(errH);
        }

        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        public override void Logdase( KwErrorHandler errH)
        {
            this.Link_Server.EngineClient.Send_Logdase(errH);
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 将棋エンジンを起動します。
        /// ************************************************************************************************************************
        /// </summary>
        public override void Start_ShogiEngine(string shogiEngineFilePath, KwErrorHandler errH)
        {
            this.Link_Server.EngineClient.Start(shogiEngineFilePath);
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Usi(errH);
        }

        /// <summary>
        /// コンピューターの先手
        /// </summary>
        public virtual void Do_ComputerSente(KwErrorHandler errH)
        {
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Position(
                Util_KirokuGakari.ToSfen_PositionCommand(this.Link_Server.Model_Taikyoku.Kifu), errH);
            this.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Go(errH);
        }


        /// <summary>
        /// このアプリケーションソフトの開始時の処理。
        /// </summary>
        public new void Load_AsStart(KwErrorHandler errH)
        {
            base.Load_AsStart(errH);

            this.Data_Settei_Csv.Read_Add("../../Data/data_settei_vs.csv", Encoding.UTF8);
            this.Data_Settei_Csv.DebugOut();
            this.WidgetLoaders.Add(new WidgetsLoader_CsharpVsImpl("../../Data/data_widgets_03_vs.csv", this));
        }

    }
}
