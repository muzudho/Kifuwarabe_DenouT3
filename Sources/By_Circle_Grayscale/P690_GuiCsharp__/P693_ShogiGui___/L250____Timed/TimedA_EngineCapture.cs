using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P461_Server_____.L250____Util;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L080____Shape;
using Grayscale.P693_ShogiGui___.L249____Function;

namespace Grayscale.P693_ShogiGui___.L250____Timed
{


    /// <summary>
    /// ▲人間vs△コンピューター対局のやりとりです。
    /// </summary>
    public class TimedA_EngineCapture : Timed_Abstract
    {


        private MainGui_Csharp mainGui;


        public TimedA_EngineCapture(MainGui_Csharp shogibanGui)
        {
            this.mainGui = shogibanGui;
        }


        public override void Step(KwErrorHandler errH)
        {
            // 将棋エンジンからの入力が、input99 に溜まるものとします。
            if (0 < this.mainGui.ConsoleWindowGui.InputString99.Length)
            {

#if DEBUG
                string message = "(^o^)timer入力 input99=[" + this.mainGui.ConsoleWindowGui.InputString99 + "]";
                errH.Logger.WriteLine_AddMemo(message);
#endif

                //
                // 棋譜入力テキストボックスに、指し手「（例）6a6b」を入力するための一連の流れです。
                //
                {
                    this.mainGui.RepaintRequest = new RepaintRequestImpl();
                    this.mainGui.RepaintRequest.SetNyuryokuTextTail(this.mainGui.ConsoleWindowGui.InputString99);// 受信文字列を、上部テキストボックスに入れるよう、依頼します。
                    this.mainGui.Response("Timer", errH);// テキストボックスに、受信文字列を入れます。
                    this.mainGui.ConsoleWindowGui.ClearInputString99();// 受信文字列の要求を空っぽにします。
                }

                //
                // コマ送り
                //
                {
                    string restText = Util_Function_Csharp.ReadLine_FromTextbox();

                    //if ("noop" == restText)
                    //{
                    //    this.mainGui.Link_Server.EngineClient.ShogiEngineProcessWrapper.Send_Ok();
                    //    restText = "";
                    //}

                    Util_Functions_Server.Komaokuri_Srv(
                        ref restText,
                        this.mainGui.Link_Server.Model_Taikyoku,
                        this.mainGui.Model_Manual,
                        errH);// 棋譜の[コマ送り]を実行します。
                    Util_Function_Csharp.Komaokuri_Gui(restText, this.mainGui, errH);//追加
                    // ↑チェンジターン済み
                    Util_Menace.Menace((MainGui_Csharp)this.mainGui, errH);// メナス
                }

                //
                // ここで、テキストボックスには「（例）6a6b」が入っています。
                //

                //
                // 駒を動かす一連の流れです。
                //
                {
                    //this.ShogiGui.ResponseData.InputTextString = "";//空っぽにすることを要求する。
                    this.mainGui.Response("Timer", errH);// GUIに反映させます。
                }

            }
        }

    }
}
