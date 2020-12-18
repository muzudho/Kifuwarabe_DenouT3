namespace Grayscale.P699_Form_______
{
#if DEBUG
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
    using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P003Log.L500Struct;
using Grayscale.P027Settei.L510Xml;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P214Masu.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P234Komahaiyaku.L500Util;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P324KifuTree.L250Struct;
using Grayscale.P341Ittesasu.L250OperationA;
using Grayscale.P693ShogiGui.I080Shape;
using Grayscale.P693ShogiGui.I100Widgets;
using Grayscale.P693ShogiGui.I125Scene;
using Grayscale.P693ShogiGui.I499Repaint;
using Grayscale.P693ShogiGui.I500Gui;
using Grayscale.P693ShogiGui.I510Form;
using Grayscale.P693ShogiGui.L060TextBoxListener;
using Grayscale.P693ShogiGui.L080Shape;
using Grayscale.P693ShogiGui.L081Canvas;
using Grayscale.P693ShogiGui.L125Scene;
using Grayscale.P693ShogiGui.L249Function;
using Grayscale.P693ShogiGui.L250Timed;
using Nett;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#else
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using Grayscale.Kifuwarakaku.Entities.Logger;
    using Grayscale.P027Settei.L510Xml;
    using Grayscale.P211WordShogi.L500Word;
    using Grayscale.P212ConvPside.L500Converter;
    using Grayscale.P213Komasyurui.L500Util;
    using Grayscale.P214Masu.L500Util;
    using Grayscale.P218Starlight.I500Struct;
    using Grayscale.P224Sky.L500Struct;
    using Grayscale.P234Komahaiyaku.L500Util;
    using Grayscale.P238Seiza.L250Struct;
    using Grayscale.P238Seiza.L500Util;
    using Grayscale.P258UtilSky258.L500UtilSky;
    using Grayscale.P324KifuTree.L250Struct;
    using Grayscale.P341Ittesasu.L250OperationA;
    using Grayscale.P693ShogiGui.I080Shape;
    using Grayscale.P693ShogiGui.I100Widgets;
    using Grayscale.P693ShogiGui.I125Scene;
    using Grayscale.P693ShogiGui.I499Repaint;
    using Grayscale.P693ShogiGui.I500Gui;
    using Grayscale.P693ShogiGui.I510Form;
    using Grayscale.P693ShogiGui.L060TextBoxListener;
    using Grayscale.P693ShogiGui.L080Shape;
    using Grayscale.P693ShogiGui.L081Canvas;
    using Grayscale.P693ShogiGui.L125Scene;
    using Grayscale.P693ShogiGui.L249Function;
    using Grayscale.P693ShogiGui.L250Timed;
    using Nett;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#endif

    /// <summary>
    /// ************************************************************************************************************************
    /// メイン画面です。
    /// ************************************************************************************************************************
    /// </summary>
    [Serializable]
    public partial class Uc_Form1Main : UserControl, Uc_Form1Mainable
    {

#region プロパティー類

        public MainGui_Csharp MainGui { get { return this.mainGui; } }
        public void SetMainGui(MainGui_Csharp mainGui)
        {
            this.mainGui = mainGui;
        }
        private MainGui_Csharp mainGui;

        /// <summary>
        /// 設定XMLファイル
        /// </summary>
        public SetteiXmlFile SetteiXmlFile
        {
            get
            {
                return this.setteiXmlFile;
            }
        }
        private SetteiXmlFile setteiXmlFile;



#endregion


#region ゲームエンジンの振りをするメソッド

        private const int NSQUARE = 9 * 9;

        ///// <summary>
        ///// ************************************************************************************************************************
        ///// 入力欄のテキストを取得します。
        ///// ************************************************************************************************************************
        ///// </summary>
        ///// <returns></returns>
        //public string ReadLine2(IErrorController errH)
        //{
        //    SkyConst src_Sky = this.ShogibanGui.Link_ShogiServer.Model_Operating.Manual.GuiSkyConst;

        //    //------------------------------------------------------------
        //    // 表について
        //    //------------------------------------------------------------

        //    //
        //    // 配列の添え字は次の通り。
        //    //
        //    //    ９　８　７　６　５　４　３　２　１
        //    //  ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐
        //    //  │ 0│ 1│ 2│ 3│ 4│ 5│ 6│ 7│ 8│一
        //    //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
        //    //  │ 9│10│11│12│13│14│15│16│17│二
        //    //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
        //    //  │18│19│20│21│22│23│24│25│26│三
        //    //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
        //    //  │27│28│29│30│31│32│33│34│35│四
        //    //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
        //    //　│36│37│38│39│40│41│42│43│44│五
        //    //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
        //    //  │45│46│47│48│49│50│51│52│53│六
        //    //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
        //    //  │54│55│56│57│58│59│60│61│62│七
        //    //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
        //    //  │63│64│65│66│67│68│69│70│71│八
        //    //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
        //    //  │72│73│74│75│76│77│78│79│80│九
        //    //  └─┴─┴─┴─┴─┴─┴─┴─┴─┘

        //    //------------------------------------------------------------
        //    // 先手駒について
        //    //------------------------------------------------------------

        //    // 先手駒の位置を表にします。
        //    bool[] wallSTable = new bool[Uc_Form1Main.NSQUARE];

        //    // 先手駒の利きを表にします。
        //    bool[] kikiSTable = new bool[Uc_Form1Main.NSQUARE];

        //    Node<Starbeamable, KyokumenWrapper> siteiNode = this.ShogibanGui.Link_ShogiServer.Model_Operating.Taikyoku.Kifu.NodeAt(
        //        this.ShogibanGui.Link_ShogiServer.Model_Operating.Taikyoku.Kifu.CurNode.Value.ToKyokumenConst.Temezumi
        //        );

        //    foreach (Finger figKoma in Util_Sky_FingersQuery.InOkibaPsideNow(this.ShogibanGui.Link_ShogiServer.Model_Operating.Manual.GuiSkyConst, Okiba.ShogiBan, Playerside.P1).Items)
        //    {
        //        Starlightable light = src_Sky.StarlightIndexOf(figKoma).Now;
        //        RO_Star koma = Util_Starlightable.ToKoma(light);


        //        int suji;
        //        Util_MasuNum.TryMasuToSuji(koma.Masu, out suji);

        //        int dan;
        //        Util_MasuNum.TryMasuToDan(koma.Masu, out dan);

        //        // 壁
        //        wallSTable[(dan - 1) * 9 + (9 - suji)] = true;

        //        // 利き
        //        kikiSTable[(dan - 1) * 9 + (9 - suji)] = true;//FIXME:嘘
        //    }

        //    //------------------------------------------------------------
        //    // 後手駒について
        //    //------------------------------------------------------------

        //    // 先手駒の位置を表にします。
        //    bool[] wallGTable = new bool[Uc_Form1Main.NSQUARE];

        //    // 先手駒の利きを表にします。
        //    bool[] kikiGTable = new bool[Uc_Form1Main.NSQUARE];

        //    foreach (Finger figKoma in Util_Sky_FingersQuery.InOkibaPsideNow(this.ShogibanGui.Link_ShogiServer.Model_Operating.Manual.GuiSkyConst, Okiba.ShogiBan, Playerside.P2).Items)
        //    {
        //        RO_Star koma = Util_Starlightable.ToKoma(src_Sky.StarlightIndexOf(figKoma).Now);


        //        int suji;
        //        Util_MasuNum.TryMasuToSuji(koma.Masu, out suji);

        //        int dan;
        //        Util_MasuNum.TryMasuToDan(koma.Masu, out dan);

        //        // 壁
        //        wallGTable[(dan - 1) * 9 + (9 - suji)] = true;

        //        // 利き
        //        kikiGTable[(dan - 1) * 9 + (9 - suji)] = true;//FIXME:嘘
        //    }


        //    string jsaFugoStr_tuginoItte = "▲９九王嘘";


        //    //this.ShogiGui.Model_PnlTaikyoku.Kifu.AssertPside(this.ShogiGui.Model_PnlTaikyoku.Kifu.CurNode, "ReadLine2", errH);
        //    Playerside pside = this.ShogibanGui.Link_ShogiServer.Model_Operating.Taikyoku.Kifu.CurNode.Value.ToKyokumenConst.KaisiPside;
        //    Fingers fingers = Util_Sky_FingersQuery.InOkibaPsideNow(this.ShogibanGui.Link_ShogiServer.Model_Operating.Manual.GuiSkyConst, Okiba.ShogiBan, pside);
        //    if (0<fingers.Count)
        //    {
        //        Starbeamable tuginoMoveData;

        //        Finger finger = fingers[KwRandom.Random.Next(fingers.Count)];//ランダムに１つ。
        //        Starlight sl = src_Sky.StarlightIndexOf(finger);

        //        RO_Star koma = Util_Starlightable.ToKoma(sl.Now);

        //        //this.ShogiGui.Model_PnlTaikyoku.Kifu.AssertPside(this.ShogiGui.Model_PnlTaikyoku.Kifu.CurNode, "ReadLine2",errH);
        //        Playerside pside2 = this.ShogibanGui.Link_ShogiServer.Model_Operating.Taikyoku.Kifu.CurNode.Value.ToKyokumenConst.KaisiPside;
        //        Playerside pside_getTeban = pside2;
        //        switch (pside_getTeban)
        //        {
        //            case Playerside.P2:
        //                {
        //                    // 後手番です。
        //                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        //                    int suji;
        //                    Util_MasuNum.TryMasuToSuji(koma.Masu, out suji);

        //                    int dan;
        //                    Util_MasuNum.TryMasuToDan(koma.Masu, out dan);

        //                    // 前に１つ突き出させます。
        //                    tuginoMoveData = new RO_Starbeam(
        //                        //sl.Finger,
        //                        new RO_Star(
        //                            pside_getTeban,
        //                            Util_Masu10.OkibaSujiDanToMasu(
        //                                Conv_SyElement.ToOkiba(koma.Masu),
        //                                suji,
        //                                dan
        //                                ),
        //                            koma.Haiyaku
        //                        ),

        //                        new RO_Star(
        //                            pside_getTeban,
        //                            Util_Masu10.OkibaSujiDanToMasu(
        //                                Okiba.ShogiBan,
        //                                suji,
        //                                dan + 1
        //                                ),
        //                            koma.Haiyaku
        //                        ),

        //                        Komasyurui14.H00_Null___
        //                    );
        //                    break;
        //                }
        //            default:
        //                {
        //                    // 先手番です。
        //                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        //                    int suji;
        //                    Util_MasuNum.TryMasuToSuji(koma.Masu, out suji);

        //                    int dan;
        //                    Util_MasuNum.TryMasuToDan(koma.Masu, out dan);

        //                    // 前に１つ突き出させます。
        //                    tuginoMoveData = new RO_Starbeam(
        //                        //sl.Finger,
        //                        new RO_Star(
        //                            pside_getTeban,
        //                            Util_Masu10.OkibaSujiDanToMasu(
        //                                Conv_SyElement.ToOkiba(koma.Masu),
        //                                suji,
        //                                dan
        //                                ),
        //                            koma.Haiyaku
        //                        ),

        //                        new RO_Star(
        //                            pside_getTeban,
        //                            Util_Masu10.OkibaSujiDanToMasu(
        //                                Okiba.ShogiBan,
        //                                suji,
        //                                dan - 1
        //                                ),
        //                            koma.Haiyaku
        //                        ),

        //                        Komasyurui14.H00_Null___
        //                    );
        //                    break;
        //                }
        //        }

        //        jsaFugoStr_tuginoItte = Conv_MoveStr_Jsa.ToMoveStr_Jsa(this.ShogibanGui.Link_ShogiServer.Model_Operating.Taikyoku.Kifu.CurNode,errH);
        //        //RO_Star koma2 = Util_Starlightable.ToKoma(tuginoMoveData.LongTimeAgo);

        //        //JsaFugoImpl fugoJ = Array_JsaFugoCreator15.ItemMethods[(int)Util_Komahaiyaku184.Syurui(koma2.Haiyaku)](tuginoMoveData, new KyokumenWrapper(src_Sky), errH);//「▲２二角成」なら、馬（dst）ではなくて角（src）。
        //        //jsaFugoStr_tuginoItte = Util_Translator_JsaFugo.ToString_UseDou(fugoJ, this.ShogibanGui.Link_ShogiServer.Model_Operating.Taikyoku.Kifu.CurNode);
        //    }


        //    return jsaFugoStr_tuginoItte;
        //}

#endregion


        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクターです。
        /// ************************************************************************************************************************
        /// </summary>
        public Uc_Form1Main()
        {
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            ILogTag logTag = LogTags.CsharpGuiDefault;
            this.MainGui.Timer_Tick(logTag);
        }



        /// <summary>
        /// ************************************************************************************************************************
        /// 起動直後の流れです。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uc_Form1Main_Load(object sender, EventArgs e)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            ILogTag logTag = LogTags.CsharpGuiDefault;

            Uc_Form2Main uc_Form2Main = ((Form1_Shogi)this.ParentForm).Form2_Console.Uc_Form2Main;

            //
            // 設定XMLファイル
            //
            {
                this.setteiXmlFile = new SetteiXmlFile(toml.Get<TomlTable>("Resources").Get<string>("DataSetteiXml"));
                if (!this.SetteiXmlFile.Exists())
                {
                    // ファイルが存在しませんでした。

                    // 作ります。
                    this.SetteiXmlFile.Write();
                }

                if (!this.SetteiXmlFile.Read())
                {
                    // 読取に失敗しました。
                }

                // デバッグ
                this.SetteiXmlFile.DebugWrite();
            }


            //----------
            // 棋譜
            //----------
            //
            //      先後や駒など、対局に用いられる事柄、物を事前準備しておきます。
            //

            //----------
            // 駒の並べ方
            //----------
            //
            //      平手に並べます。
            //
            {
                this.MainGui.Link_Server.Model_Taikyoku.Kifu.Clear();// 棋譜を空っぽにします。
                this.MainGui.Link_Server.Model_Taikyoku.Kifu.SetProperty(Word_KifuTree.PropName_Startpos, "startpos");//平手の初期局面
                Playerside firstPside = Playerside.P1;//Playerside.P2
                this.MainGui.Model_Manual.SetGuiSky(
                    Util_SkyWriter.New_Hirate(firstPside)//起動直後
                    );
                //this.ShogiGui.Model_PnlTaikyoku.Kifu.SetProperty(Word_KifuTree.PropName_FirstPside, Playerside.P1);
            }



            // 全駒の再描画
            this.MainGui.RepaintRequest = new RepaintRequestImpl();
            this.MainGui.RepaintRequest.SetFlag_RecalculateRequested();

            //----------
            // フェーズ
            //----------
            this.MainGui.SetScene(SceneName.SceneB_1TumamitaiKoma);

            //----------
            // 監視
            //----------
            this.gameEngineTimer1.Start();

            //----------
            // 将棋エンジンが、コンソールの振りをします。
            //----------
            //
            //      このメインパネルに、コンソールの振りをさせます。
            //      将棋エンジンがあれば、将棋エンジンの入出力を返すように内部を改造してください。
            //
            TextboxListener.SetTextboxListener(
                uc_Form2Main.ReadText, uc_Form2Main.WriteLine_Syuturyoku);


            //----------
            // 画面の出力欄
            //----------
            //
            //      出力欄（上下段）を空っぽにします。
            //
            uc_Form2Main.WriteLine_Syuturyoku("");



            this.MainGui.Response("Launch", logTag);

            // これで、最初に見える画面の準備は終えました。
            // あとは、操作者の入力を待ちます。
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 描画するのはここです。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uc_Form1Main_Paint(object sender, PaintEventArgs e)
        {
            if (null == this.MainGui.Shape_PnlTaikyoku)
            {
                goto gt_EndMethod;
            }

            //------------------------------
            // 画面の描画です。
            //------------------------------
            this.MainGui.Shape_PnlTaikyoku.Paint(sender, e, this.MainGui, Shape_CanvasImpl.WINDOW_NAME_SHOGIBAN, LogTags.CsharpGuiPaint);

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// マウスが動いたときの挙動です。
        /// ************************************************************************************************************************
        /// 
        ///         マウスが重なったときの、表示物の反応や、将棋データの変更がこの中に書かれています。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uc_Form1Main_MouseMove(object sender, MouseEventArgs e)
        {
            ILogTag logTag = LogTags.CsharpGuiDefault;

            if (null != this.MainGui.Shape_PnlTaikyoku)
            {
                // このメインパネルに、何かして欲しいという要求は、ここに入れられます。
                this.MainGui.RepaintRequest = new RepaintRequestImpl();

                // マウスムーブ
                {
                    TimedB_MouseCapture timeB = ((TimedB_MouseCapture)this.MainGui.TimedB_MouseCapture);
                    timeB.MouseEventQueue.Enqueue(
                        new MouseEventState(this.MainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_SHOGIBAN, MouseEventStateName.MouseMove, e.Location, logTag));
                }

                //------------------------------
                // このメインパネルの反応
                //------------------------------
                this.MainGui.Response("MouseOperation", logTag);
            }
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// マウスのボタンを押下したときの挙動です。
        /// ************************************************************************************************************************
        /// 
        ///         マウスボタンが押下されたときの、表示物の反応や、将棋データの変更がこの中に書かれています。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uc_Form1Main_MouseDown(object sender, MouseEventArgs e)
        {
            ILogTag logTag = LogTags.CsharpGuiDefault;

            if (null == this.MainGui.Shape_PnlTaikyoku)
            {
                goto gt_EndMethod;
            }

            // このメインパネルに、何かして欲しいという要求は、ここに入れられます。
            this.MainGui.RepaintRequest = new RepaintRequestImpl();


            if (e.Button == MouseButtons.Left)
            {
                //------------------------------------------------------------
                // 左ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)this.MainGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(this.MainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_SHOGIBAN, MouseEventStateName.MouseLeftButtonDown, e.Location, logTag));
            }
            else if (e.Button == MouseButtons.Right)
            {
                //------------------------------------------------------------
                // 右ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)this.MainGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(this.MainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_SHOGIBAN, MouseEventStateName.MouseRightButtonDown, e.Location, logTag));


                //------------------------------
                // このメインパネルの反応
                //------------------------------
                this.MainGui.Response("MouseOperation", logTag);

            }
            else
            {
                //------------------------------
                // このメインパネルの反応
                //------------------------------
                this.MainGui.Response("MouseOperation", logTag);
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// マウスのボタンが放されたときの挙動です。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uc_Form1Main_MouseUp(object sender, MouseEventArgs e)
        {
            ILogTag logTag = LogTags.CsharpGuiDefault;

            // このメインパネルに、何かして欲しいという要求は、ここに入れられます。
            this.MainGui.RepaintRequest = new RepaintRequestImpl();

            //------------------------------
            // マウスボタンが放されたときの、表示物の反応や、将棋データの変更がこの中に書かれています。
            //------------------------------
            if (e.Button == MouseButtons.Left)
            {
                //------------------------------------------------------------
                // 左ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)this.MainGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(this.MainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_SHOGIBAN, MouseEventStateName.MouseLeftButtonUp, e.Location, logTag));
            }
            else if (e.Button == MouseButtons.Right)
            {
                //------------------------------------------------------------
                // 右ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)this.MainGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(this.MainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_SHOGIBAN, MouseEventStateName.MouseRightButtonUp, e.Location, logTag));
            }
        }


        public Form1_Mutex MutexOwner { get; set; }

        /// <summary>
        /// ************************************************************************************************************************
        /// 入力欄の表示・出力欄の表示・再描画
        /// ************************************************************************************************************************
        /// 
        /// このメインパネルに何かして欲しいことがあれば、
        /// RequestForMain に要望を入れて、この関数を呼び出してください。
        ///
        /// 同時には処理できない項目もあります。
        /// </summary>
        /// <param name="response"></param>
        public void Solute_RepaintRequest(
            Form1_Mutex mutex, MainGui_Csharp mainGui, ILogTag logTag)
        {
            Uc_Form2Main form2 = ((Form1_Shogi)this.ParentForm).Form2_Console.Uc_Form2Main;

            //------------------------------------------------------------
            // 駒の座標再計算
            //------------------------------------------------------------
            if (mainGui.RepaintRequest.Is_StarlightsRecalculateRequested())
            {
                this.MainGui.Model_Manual.GuiSkyConst.Foreach_Starlights((Finger finger, IMoveHalf light, ref bool toBreak) =>
                {
                    Util_Function_Csharp.Redraw_KomaLocation(finger, this.MainGui, logTag);
                });
            }
            mainGui.RepaintRequest.Clear_StarlightsRecalculateRequested();


            //------------------------------
            // 入力欄の表示
            //------------------------------
            if (mainGui.RepaintRequest.IsRequested_RepaintNyuryokuText)
            {
                // 指定のテキストで上書きします。
                form2.SetInputareaText(mainGui.RepaintRequest.NyuryokuText);
            }
            else if (mainGui.RepaintRequest.IsRequested_NyuryokuTextTail)
            {
                // 指定のテキストを後ろに足します。
                form2.AppendInputareaText(mainGui.RepaintRequest.NyuryokuTextTail);
                mainGui.RepaintRequest.SetNyuryokuTextTail( "");//要求の解除
            }

            //------------------------------
            // 出力欄（上・下段）の表示
            //------------------------------
            switch (mainGui.RepaintRequest.SyuturyokuRequest)
            {
                case RepaintRequestGedanTxt.Clear:
                    {
                        // 出力欄（上下段）を空っぽにします。
                        form2.WriteLine_Syuturyoku("");

                        // ログ
                        //errH.Logger.WriteLineAddMemo( "");
                        //errH.Logger.WriteLineAddMemo( "");
                    }
                    break;

                case RepaintRequestGedanTxt.Kifu:
                    {
                        // 出力欄（上下段）に、棋譜を出力します。
                        switch (this.MainGui.SyuturyokuKirikae)
                        {
                            case SyuturyokuKirikae.Japanese:
                                form2.WriteLine_Syuturyoku(Util_KirokuGakari.ToJsaFugoListString(this.MainGui.Link_Server.Model_Taikyoku.Kifu, "Ui_PnlMain.Response", logTag));
                                break;
                            case SyuturyokuKirikae.Sfen:
                                form2.WriteLine_Syuturyoku(Util_KirokuGakari.ToSfen_PositionCommand(this.MainGui.Link_Server.Model_Taikyoku.Kifu));
                                break;
                            case SyuturyokuKirikae.Html:
                                form2.WriteLine_Syuturyoku(Uc_Form1Main.CreateHtml(this.MainGui));
                                break;
                        }

#if DEBUG
                        // ログ
                        errH.Logger.WriteLineAddMemo(form2.GetOutputareaText());
#endif
                    }
                    break;

                default:
                    // スルー
                    break;
            }

            //------------------------------
            // 再描画
            //------------------------------
            if (mainGui.RepaintRequest.IsRefreshRequested())
            {
                this.Refresh();

                mainGui.RepaintRequest.ClearRefreshRequest();
            }
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 出力欄（上段）でキーボードのキーが押されたときの挙動をここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOutput1_KeyDown(object sender, KeyEventArgs e)
        {
            AspectOriented_TextBox.KeyDown_SelectAll(sender, e);
            ////------------------------------
            //// [Ctrl]+[A] で、全選択します。
            ////------------------------------
            //if (e.KeyCode == System.Windows.Forms.Keys.A & e.Control == true)
            //{
            //    ((TextBox)sender).SelectAll();
            //} 
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 入力欄でキーボードのキーが押されたときの挙動をここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInput1_KeyDown(object sender, KeyEventArgs e)
        {
            AspectOriented_TextBox.KeyDown_SelectAll(sender, e);
            ////------------------------------
            //// [Ctrl]+[A] で、全選択します。
            ////------------------------------
            //if (e.KeyCode == System.Windows.Forms.Keys.A & e.Control == true)
            //{
            //    ((TextBox)sender).SelectAll();
            //} 
        }




        /// <summary>
        /// ************************************************************************************************************************
        /// HTML出力。（これは作者のホームページ用に書かれています）
        /// ************************************************************************************************************************
        /// </summary>
        public static string CreateHtml(MainGui_Csharp mainGui)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<div style=\"position:relative; left:0px; top:0px; border:solid 1px black; width:250px; height:180px;\">");

            // 後手の持ち駒
            sb.AppendLine("    <div style=\"position:absolute; left:0px; top:2px; width:30px;\">");
            sb.AppendLine("        △後手");
            sb.AppendLine("        <div style=\"margin-top:10px; width:30px;\">");
            sb.Append("            ");

            SkyConst siteiSky = mainGui.Model_Manual.GuiSkyConst;

            siteiSky.Foreach_Starlights((Finger finger, IMoveHalf ml, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(ml.Now);


                if (Conv_SyElement.ToOkiba(koma.Masu) == Okiba.Gote_Komadai)
                {
                    sb.Append(Util_Komasyurui14.Fugo[(int)Util_Komahaiyaku184.Syurui(koma.Haiyaku)]);
                }
            });

            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");

            // 将棋盤
            sb.AppendLine("    <div style=\"position:absolute; left:30px; top:2px; width:182px;\">");
            sb.AppendLine("        <table>");
            for (int dan = 1; dan <= 9; dan++)
            {
                sb.Append("        <tr>");
                for (int suji = 9; 1 <= suji; suji--)
                {
                    bool isSpace = true;

                    siteiSky.Foreach_Starlights((Finger finger, IMoveHalf ml, ref bool toBreak) =>
                    {
                        RO_Star koma2 = Util_Starlightable.AsKoma(ml.Now);


                        int suji2;
                        Util_MasuNum.TryMasuToSuji(koma2.Masu, out suji2);

                        int dan2;
                        Util_MasuNum.TryMasuToDan(koma2.Masu, out dan2);

                        if (
                            Conv_SyElement.ToOkiba(koma2.Masu) == Okiba.ShogiBan //盤上
                            && suji2 == suji
                            && dan2 == dan
                        )
                        {
                            if (Playerside.P2 == koma2.Pside)
                            {
                                sb.Append("<td><span class=\"koma2x\">");
                                sb.Append(Util_Komasyurui14.Fugo[(int)Util_Komahaiyaku184.Syurui(koma2.Haiyaku)]);
                                sb.Append("</span></td>");
                                isSpace = false;
                            }
                            else
                            {
                                sb.Append("<td><span class=\"koma1x\">");
                                sb.Append(Util_Komasyurui14.Fugo[(int)Util_Komahaiyaku184.Syurui(koma2.Haiyaku)]);
                                sb.Append("</span></td>");
                                isSpace = false;
                            }
                        }


                    });

                    if (isSpace)
                    {
                        sb.Append("<td>　</td>");
                    }
                }

                sb.AppendLine("</tr>");
            }
            sb.AppendLine("        </table>");
            sb.AppendLine("    </div>");

            // 先手の持ち駒
            sb.AppendLine("    <div style=\"position:absolute; left:215px; top:2px; width:30px;\">");
            sb.AppendLine("        ▲先手");
            sb.AppendLine("        <div style=\"margin-top:10px; width:30px;\">");
            sb.Append("            ");

            siteiSky.Foreach_Starlights((Finger finger, IMoveHalf ml, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(ml.Now);

                if (Conv_SyElement.ToOkiba(koma.Masu) == Okiba.Sente_Komadai)
                {
                    sb.Append(Util_Komasyurui14.Fugo[(int)Util_Komahaiyaku184.Syurui(koma.Haiyaku)]);
                }
            });

            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");

            //
            sb.AppendLine("</div>");

            return sb.ToString();
        }


    }
}
