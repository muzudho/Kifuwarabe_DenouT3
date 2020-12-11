using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Codeplex.Data;//DynamicJson
using Grayscale.P003Log.I500Struct;
using Grayscale.P027Settei.L500Struct;
using Grayscale.P236KomahaiyaTr.L500Table;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P248Michi.L500Word;
using Grayscale.P250KomahaiyaEx.L500Util;
using Grayscale.P270ForcePromot.L250Struct;
using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P325PnlTaikyoku.L250Struct;
using Grayscale.P461Server.L497EngineClient;
using Grayscale.P461Server.L498Server;
using Grayscale.P693ShogiGui.I080Shape;
using Grayscale.P693ShogiGui.I125Scene;
using Grayscale.P693ShogiGui.I492Widgets;
using Grayscale.P693ShogiGui.I499Repaint;
using Grayscale.P693ShogiGui.I500Gui;
using Grayscale.P693ShogiGui.I510Form;
using Grayscale.P693ShogiGui.L080Shape;
using Grayscale.P693ShogiGui.L081Canvas;
using Grayscale.P693ShogiGui.L125Scene;
using Grayscale.P693ShogiGui.L249Function;
using Grayscale.P693ShogiGui.L250Timed;
using Grayscale.P693ShogiGui.L492Widgets;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
using Nett;

namespace Grayscale.P693ShogiGui.L500GUI
{

    /// <summary>
    /// 将棋盤ウィンドウ（Ｃ＃）に対応。
    /// 
    /// コンソール・ウィンドウを持っている。
    /// </summary>
    public class MainGui_CsharpImpl : MainGui_Csharp
    {

        #region プロパティー

        /// <summary>
        /// 将棋サーバー。
        /// </summary>
        public Server Link_Server { get { return this.server; } }
        protected Server server;

        public Model_Manual Model_Manual { get { return this.model_Manual; } }
        private Model_Manual model_Manual;

        /// <summary>
        /// コンソール・ウィンドウ。
        /// </summary>
        public SubGui ConsoleWindowGui { get { return this.consoleWindowGui; } }
        private SubGui consoleWindowGui;

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, UserWidget> Widgets { get; set; }
        public void SetWidget(string name, UserWidget widget)
        {
            this.Widgets[name] = widget;
        }
        public UserWidget GetWidget(string name)
        {
            UserWidget widget;

            if (this.Widgets.ContainsKey(name))
            {
                widget = this.Widgets[name];
            }
            else
            {
                widget = UserButtonImpl.NULL_OBJECT;
            }

            return widget;
        }

        public Timed TimedA { get; set; }
        public Timed TimedB_MouseCapture { get; set; }
        public Timed TimedC { get; set; }

        public RepaintRequest RepaintRequest { get; set; }

        /// <summary>
        /// 使い方：((Form1_Shogiable)this.OwnerForm)
        /// </summary>
        public Form OwnerForm { get; set; }

        /// <summary>
        /// ウィジェット読込みクラス。
        /// </summary>
        public List<WidgetsLoader> WidgetLoaders { get; set; }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// グラフィックを描くツールは全部この中です。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Shape_PnlTaikyoku Shape_PnlTaikyoku
        {
            get
            {
                return this.shape_PnlTaikyoku;
            }
        }
        private Shape_PnlTaikyoku shape_PnlTaikyoku;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// ゲームの流れの状態遷移図はこれです。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public SceneName Scene
        {
            get
            {
                return this.scene1;
            }
        }

        public void SetScene(SceneName scene)
        {
            if (SceneName.Ignore != scene)
            {
                this.scene1 = scene;
            }
        }
        private SceneName scene1;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒を動かす状態遷移図はこれです。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public SceneName FlowB
        {
            get
            {
                return this.flowB;
            }
        }
        public void SetFlowB(SceneName name1, IErrorController errH)
        {
            this.flowB = name1;

            //アライブ
            {
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)this.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(name1, Shape_CanvasImpl.WINDOW_NAME_SHOGIBAN, MouseEventStateName.Arive, Point.Empty, errH));
            }
        }
        private SceneName flowB;


        /// <summary>
        /// 設定データCSV
        /// </summary>
        public Data_Settei_Csv Data_Settei_Csv { get; set; }


        #endregion



        #region コンストラクター

        /// <summary>
        /// 生成後、OwnerFormをセットしてください。
        /// </summary>
        public MainGui_CsharpImpl()
        {
            this.model_Manual = new Model_ManualImpl();
            this.server = new Server_Impl(this.model_Manual.GuiSkyConst, this.model_Manual.GuiTemezumi, new Receiver_ForCsharpVsImpl());

            this.Widgets = new Dictionary<string, UserWidget>();

            this.consoleWindowGui = new SubGuiImpl(this);

            this.TimedA = new TimedA_EngineCapture(this);
            this.TimedB_MouseCapture = new TimedB_MouseCapture(this);
            this.TimedC = new TimedC_SaiseiCapture(this);

            this.Data_Settei_Csv = new Data_Settei_Csv();
            this.WidgetLoaders = new List<WidgetsLoader>();
            this.RepaintRequest = new RepaintRequestImpl();

            //----------
            // ビュー
            //----------
            //
            //      ボタンや将棋盤などを描画するツールを、事前準備しておきます。
            //
            this.shape_PnlTaikyoku = new Shape_PnlTaikyokuImpl("#TaikyokuPanel", this);

            //System.C onsole.WriteLine("つまんでいる駒を放します。(1)");
            this.SetFigTumandeiruKoma(-1);

            //----------
            // [出力切替]初期値
            //----------
            this.syuturyokuKirikae = SyuturyokuKirikae.Japanese;
        }

        #endregion


        /// <summary>
        /// ************************************************************************************************************************
        /// 将棋エンジンを起動します。
        /// ************************************************************************************************************************
        /// </summary>
        public virtual void Start_ShogiEngine(string shogiEngineFilePath, IErrorController errH)
        {
        }

        /// <summary>
        /// コンピューターの先手
        /// </summary>
        public virtual void Do_ComputerSente(IErrorController errH)
        {
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 手番が替わったときの挙動を、ここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        public virtual void ChangedTurn(IErrorController errH)
        {
        }


        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        public virtual void Shutdown(IErrorController errH)
        {
        }


        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        public virtual void Logdase(IErrorController errH)
        {
        }



        private int noopSend_counter;
        public void Timer_Tick( IErrorController errH)
        {
            if (this.server.EngineClient.ShogiEngineProcessWrapper.IsLive_ShogiEngine())
            {
                // だいたい 1tick 50ms と考えて、20倍で 1秒。
                if ( 20 * 3 < this.noopSend_counter) // 3秒に 1 回ぐらい ok を送れば？
                {
                    // noop
                    this.server.EngineClient.ShogiEngineProcessWrapper.Send_Noop_from_server(errH);
                    this.noopSend_counter = 0;
                }
                else
                {
                    this.noopSend_counter++;
                }
            }

            this.TimedA.Step(errH);
            this.TimedB_MouseCapture.Step(errH);
            this.TimedC.Step(errH);
        }




        /// <summary>
        /// 見た目の設定を読み込みます。
        /// </summary>
        public void ReadStyle_ToForm(Form1_Shogiable ui_Form1)
        {
            try
            {
                string filepath2 = Path.Combine( Path.Combine( Application.StartupPath, "../../Data/"), "data_style.txt");
#if DEBUG
                MessageBox.Show("独自スタイルシート　filepath2=" + filepath2);
#endif

                if (File.Exists(filepath2))
                {
                    string styleText = System.IO.File.ReadAllText(filepath2, Encoding.UTF8);

                    try
                    {
                        var jsonMousou_arr = DynamicJson.Parse(styleText);

                        var bodyElm = jsonMousou_arr["body"];

                        if (null != bodyElm)
                        {
                            var backColor = bodyElm["backColor"];

                            if (null != backColor)
                            {
                                var var_alpha = backColor["alpha"];

                                int red = (int)backColor["red"];

                                int green = (int)backColor["green"];

                                int blue = (int)backColor["blue"];

                                if (null != var_alpha)
                                {
                                    ui_Form1.Uc_Form1Main.BackColor = Color.FromArgb((int)var_alpha, red, green, blue);
                                }
                                else
                                {
                                    ui_Form1.Uc_Form1Main.BackColor = Color.FromArgb(red, green, blue);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("JSONのパース時にエラーか？：" + ex.GetType().Name + "：" + ex.Message);
                        throw;
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSONのパース時にエラーか？：" + ex.GetType().Name + "：" + ex.Message);
            }
        }


        /// <summary>
        /// このアプリケーションソフトの開始時の処理。
        /// </summary>
        public virtual void Load_AsStart(IErrorController errH)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            //
            // 既存のログファイルを削除したい。
            //
            {

            }

            {
#if DEBUG
                errH.Logger.WriteLine_AddMemo("(^o^)乱数のたね＝[" + KwRandom.Seed + "]");
#endif

                this.Data_Settei_Csv.Read_Add(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataSetteiCsv")), Encoding.UTF8);
                this.Data_Settei_Csv.DebugOut();

                //----------
                // 道１８７
                //----------
                string filepath_Michi = Path.Combine(Application.StartupPath, this.Data_Settei_Csv.Get("data_michi187"));
                if (Michi187Array.Load(filepath_Michi))
                {
                }

#if DEBUG
                {
                    string filepath_LogMichi = Path.Combine(Application.StartupPath, this.Data_Settei_Csv.Get("_log_道表"));
                    File.WriteAllText(filepath_LogMichi, Michi187Array.LogHtml());
                }
#endif

                //----------
                // 駒の配役１８１
                //----------
                string filepath_Haiyaku = Path.Combine(Application.StartupPath, this.Data_Settei_Csv.Get("data_haiyaku185_UTF-8"));
                Util_Array_KomahaiyakuEx184.Load(filepath_Haiyaku, Encoding.UTF8);

                {
                    string filepath_ForcePromotion = Path.Combine(Application.StartupPath, this.Data_Settei_Csv.Get("data_forcePromotion_UTF-8"));
                    List<List<string>> rows = Array_ForcePromotion.Load(filepath_ForcePromotion, Encoding.UTF8);
                    File.WriteAllText(this.Data_Settei_Csv.Get("_log_強制転成表"), Array_ForcePromotion.LogHtml());
                }

                //----------
                // 配役転換表
                //----------
                {
                    string filepath_syuruiToHaiyaku = Path.Combine(Application.StartupPath, this.Data_Settei_Csv.Get("data_syuruiToHaiyaku"));
                    List<List<string>> rows = Data_KomahaiyakuTransition.Load(filepath_syuruiToHaiyaku, Encoding.UTF8);

                    string filepath_LogHaiyakuTenkan = Path.Combine(Application.StartupPath, this.Data_Settei_Csv.Get("_log_配役転換表"));
                    File.WriteAllText(filepath_LogHaiyakuTenkan, Data_KomahaiyakuTransition.Format_LogHtml());
                }
            }


            string filepath_widgets01 = Path.Combine(Application.StartupPath, this.Data_Settei_Csv.Get("data_widgets_01_shogiban"));
            this.WidgetLoaders.Add(new WidgetsLoader_CsharpImpl(filepath_widgets01, this));
            string filepath_widgets02 = Path.Combine(Application.StartupPath, this.Data_Settei_Csv.Get("data_widgets_02_console"));
            this.WidgetLoaders.Add(new WidgetsLoader_CsharpImpl(filepath_widgets02, this));
        }

        public void LaunchForm_AsBody(IErrorController errH)
        {
            ((Form1_Shogiable)this.OwnerForm).Delegate_Form1_Load = (MainGui_Csharp shogiGui, object sender, EventArgs e) =>
            {

                //
                // ボタンのプロパティを外部ファイルから設定
                //
                foreach (WidgetsLoader widgetsLoader in this.WidgetLoaders)
                {
                    widgetsLoader.Step1_ReadFile();//shogiGui.Shape_PnlTaikyoku
                }

                foreach (WidgetsLoader widgetsLoader in this.WidgetLoaders)
                {
                    widgetsLoader.Step2_Compile_AllWidget(shogiGui);
                }

                foreach (WidgetsLoader widgetsLoader in this.WidgetLoaders)
                {
                    widgetsLoader.Step3_SetEvent(shogiGui);
                }

            };

            this.ReadStyle_ToForm((Form1_Shogiable)this.OwnerForm);

            //
            // FIXME: [初期配置]を１回やっておかないと、[コマ送り]ボタン等で不具合が出てしまう。
            //
            {
                Util_Function_Csharp.Perform_SyokiHaichi(
                    ((Form1_Shogiable)this.OwnerForm).Uc_Form1Main.MainGui,
                    errH
                );
            }


            Application.Run(this.OwnerForm);
        }


        public void Response( string mutexString, IErrorController errH)
        {
            Uc_Form1Mainable uc_Form1Main = ((Form1_Shogiable)this.OwnerForm).Uc_Form1Main;

            // enum型
            Form1_Mutex mutex2;
            switch (mutexString)
            {
                case "Timer": mutex2 = Form1_Mutex.Timer; break;
                case "MouseOperation": mutex2 = Form1_Mutex.MouseOperation; break;
                case "Saisei": mutex2 = Form1_Mutex.Saisei; break;
                case "Launch": mutex2 = Form1_Mutex.Launch; break;
                default: mutex2 = Form1_Mutex.Empty; break;
            }


            switch (uc_Form1Main.MutexOwner)
            {
                case Form1_Mutex.Launch:   // 他全部無視
                    goto gt_EndMethod;
                case Form1_Mutex.Saisei:   // マウスとタイマーは無視
                    switch (mutex2)
                    {
                        case Form1_Mutex.MouseOperation:
                        case Form1_Mutex.Timer:
                            goto gt_EndMethod;
                    }
                    break;
                case Form1_Mutex.MouseOperation:
                case Form1_Mutex.Timer:   // タイマーは無視
                    switch (mutex2)
                    {
                        case Form1_Mutex.Timer:
                            goto gt_EndMethod;
                    }
                    break;
                default: break;
            }

            uc_Form1Main.Solute_RepaintRequest(mutex2, this, errH);// 再描画

        gt_EndMethod:
            ;
        }




        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// [出力切替]
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public SyuturyokuKirikae SyuturyokuKirikae
        {
            get
            {
                return this.syuturyokuKirikae;
            }
        }
        public void SetSyuturyokuKirikae(SyuturyokuKirikae value)
        {
            this.syuturyokuKirikae = value;
        }
        private SyuturyokuKirikae syuturyokuKirikae;





        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// つまんでいる駒
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public virtual int FigTumandeiruKoma
        {
            get
            {
                return this.figTumandeiruKoma;
            }
        }
        public virtual void SetFigTumandeiruKoma(int value)
        {
            this.figTumandeiruKoma = value;
        }
        private int figTumandeiruKoma;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 成るフラグ
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        ///         マウスボタン押下時にセットされ、
        ///         マウスボタンを放したときに読み取られます。
        /// 
        /// </summary>
        public virtual bool Naru
        {
            get
            {
                return this.naruFlag;
            }
        }
        public virtual void SetNaruFlag(bool naru)
        {
            this.naruFlag = naru;
        }
        private bool naruFlag;



        public virtual RO_Star GetKoma(Finger finger)
        {
            return Util_Starlightable.AsKoma(this.Model_Manual.GuiSkyConst.StarlightIndexOf(finger).Now);
        }

    }

}
