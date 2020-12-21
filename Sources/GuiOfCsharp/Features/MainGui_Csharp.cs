namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Grayscale.P238Seiza.L250Struct;
    using Grayscale.P325PnlTaikyoku.I250Struct;
    using Grayscale.P461Server.L498Server;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

    /// <summary>
    /// 将棋盤ウィンドウ（Ｃ＃用）に対応。
    /// </summary>
    public interface MainGui_Csharp
    {
        #region プロパティー

        /// <summary>
        /// 将棋サーバー。
        /// </summary>
        Server Link_Server { get; }

        Model_Manual Model_Manual { get; }


        /// <summary>
        /// 手番が替わったときの挙動を、ここに書きます。
        /// </summary>
        void ChangedTurn();

        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        void Shutdown();


        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        void Logdase();


        /// <summary>
        /// 将棋エンジンを起動します。
        /// </summary>
        void Start_ShogiEngine(string shogiEngineFilePath);

        /// <summary>
        /// コンピューターの先手
        /// </summary>
        void Do_ComputerSente();


        RO_Star GetKoma(Finger finger);


        /// <summary>
        /// つまんでいる駒の番号。
        /// </summary>
        int FigTumandeiruKoma { get; }
        void SetFigTumandeiruKoma(int value);

        /// <summary>
        /// 成るフラグ
        /// 
        ///         マウスボタン押下時にセットされ、
        ///         マウスボタンを放したときに読み取られます。
        /// 
        /// </summary>
        bool Naru { get; }
        void SetNaruFlag(bool naru);


        /// <summary>
        /// コンソール・ウィンドウ。
        /// </summary>
        SubGui ConsoleWindowGui { get; }

        /// <summary>
        /// グラフィックを描くツールは全部この中です。
        /// </summary>
        Shape_PnlTaikyoku Shape_PnlTaikyoku { get; }

        /// <summary>
        /// ウィジェットは、１箇所にまとめておきます。
        /// </summary>
        Dictionary<string, UserWidget> Widgets { get; set; }
        void SetWidget(string name, UserWidget widget);
        UserWidget GetWidget(string name);

        #endregion


        Timed TimedA { get; set; }
        Timed TimedB_MouseCapture { get; set; }
        Timed TimedC { get; set; }
        void Timer_Tick();

        RepaintRequest RepaintRequest { get; set; }

        /// <summary>
        /// 使い方：((Ui_Form1)this.OwnerForm)
        /// </summary>
        Form OwnerForm { get; set; }


        /// <summary>
        /// ウィジェット読込みクラス。
        /// </summary>
        List<WidgetsLoader> WidgetLoaders { get; set; }




        /// <summary>
        /// ゲームの流れの状態遷移図はこれです。
        /// </summary>
        SceneName Scene { get; }
        void SetScene(SceneName scene);


        void Response(string mutexString);





        /// <summary>
        /// [出力切替]
        /// </summary>
        SyuturyokuKirikae SyuturyokuKirikae { get; }
        void SetSyuturyokuKirikae(SyuturyokuKirikae value);

    }
}
