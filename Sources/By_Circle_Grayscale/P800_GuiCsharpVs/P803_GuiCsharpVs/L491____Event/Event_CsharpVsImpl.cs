using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P693_ShogiGui___.L___080_Shape;
using Grayscale.P693_ShogiGui___.L___491_Event;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L___510_Form;
using Grayscale.P693_ShogiGui___.L500____GUI;
using Grayscale.P699_Form_______;

namespace Grayscale.P803_GuiCsharpVs.L491____Event
{
    /// <summary>
    /// シングルトン
    /// </summary>
    public class Event_CsharpVsImpl
    {
        /// <summary>
        /// シングルトン。
        /// </summary>
        /// <returns></returns>
        public static Event_CsharpVsImpl GetInstance()
        {
            if (null == Event_CsharpVsImpl.instance)
            {
                Event_CsharpVsImpl ins = new Event_CsharpVsImpl();
                Event_CsharpVsImpl.instance = ins;

                //
                // [将棋エンジン起動ボタン_学習]ボタンのイベント。
                //
                ins.delegate_BtnShogiEngineKidoL = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    // ここに処理を書く
                };

                //
                // [将棋エンジン起動ボタン_直観]ボタンのイベント。
                //
                ins.delegate_BtnShogiEngineKidoF = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogiGui = (MainGui_Csharp)obj_shogiGui2;
                    Uc_Form1Mainable ui_PnlMain = ((Form1_Shogi)shogiGui.OwnerForm).Uc_Form1Main;

                    ui_PnlMain.MainGui.Start_ShogiEngine(ui_PnlMain.SetteiXmlFile.ShogiEngineFilePath, errH);
                };

                //
                // [将棋エンジン起動ボタン_思考]ボタンのイベント。
                //
                ins.delegate_BtnShogiEngineKidoT = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    // ここに処理を書く

                    MainGui_Csharp shogiGui = (MainGui_Csharp)obj_shogiGui2;
                    Uc_Form1Mainable ui_PnlMain = ((Form1_Shogi)shogiGui.OwnerForm).Uc_Form1Main;

                    ui_PnlMain.MainGui.Do_ComputerSente( errH);
                };
            }
            return Event_CsharpVsImpl.instance;
        }
        private static Event_CsharpVsImpl instance;

        /// <summary>
        /// [将棋エンジン起動ボタン_学習]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnShogiEngineKidoL { get { return this.delegate_BtnShogiEngineKidoL; } }
        private DELEGATE_MouseHitEvent delegate_BtnShogiEngineKidoL;

        /// <summary>
        /// [将棋エンジン起動ボタン_直観]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnComputerBoot { get { return this.delegate_BtnShogiEngineKidoF; } }
        private DELEGATE_MouseHitEvent delegate_BtnShogiEngineKidoF;

        /// <summary>
        /// [将棋エンジン起動ボタン_思考]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnComputerSente { get { return this.delegate_BtnShogiEngineKidoT; } }
        private DELEGATE_MouseHitEvent delegate_BtnShogiEngineKidoT;

    }
}
