using System.Collections.Generic;
using System.Drawing;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P056Syugoron.L250Struct;
using Grayscale.P211WordShogi.L260Operator;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P214Masu.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P238Seiza.L250Struct;
namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
#if DEBUG
    using Grayscale.P238Seiza.L500Util;
    using Grayscale.P239ConvWords.L500Converter;
    using Grayscale.P247KyokumenWra.L500Struct;
    using Grayscale.P258UtilSky258.L500UtilSky;
    using Grayscale.P324KifuTree.I250Struct;
    using Grayscale.P324KifuTree.L250Struct;
    using Grayscale.P339ConvKyokume.L500Converter;
    using Grayscale.P461Server.L250Util;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
    using System.Windows.Forms;
#else
    using Grayscale.P238Seiza.L500Util;
    using Grayscale.P239ConvWords.L500Converter;
    using Grayscale.P247KyokumenWra.L500Struct;
    using Grayscale.P258UtilSky258.L500UtilSky;
    using Grayscale.P324KifuTree.I250Struct;
    using Grayscale.P324KifuTree.L250Struct;
    using Grayscale.P339ConvKyokume.L500Converter;
    using Grayscale.P461Server.L250Util;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#endif


    /// <summary>
    /// マウス操作の一連の流れです。（主に１手指す動き）
    /// </summary>
    public class TimedB_MouseCapture : Timed_Abstract
    {

        private MainGui_Csharp mainGui;

        /// <summary>
        /// マウス操作の状態です。
        /// </summary>
        public Queue<MouseEventState> MouseEventQueue { get; set; }


        public static void Check_MouseoverKomaKiki(object obj_shogiGui, Finger finger)
        {
            MainGui_Csharp shogiGui = (MainGui_Csharp)obj_shogiGui;

            IMoveHalf light = shogiGui.Model_Manual.GuiSkyConst.StarlightIndexOf(finger);
            shogiGui.Shape_PnlTaikyoku.Shogiban.KikiBan = new SySet_Default<SyElement>("利き盤");// .Clear();

            // 駒の利き
            SySet<SyElement> kikiZukei = Util_Sky_SyugoQuery.KomaKidou_Potential(finger, shogiGui.Model_Manual.GuiSkyConst);
            //kikiZukei.DebugWrite("駒の利きLv1");

            // 味方の駒
            Node<IMove, KyokumenWrapper> siteiNode = shogiGui.Link_Server.Model_Taikyoku.Kifu.CurNode;

            //shogiGui.Model_PnlTaikyoku.Kifu.AssertPside(shogiGui.Model_PnlTaikyoku.Kifu.CurNode, "Check_MouseoverKomaKiki",logTag);
            SySet<SyElement> mikataZukei = Util_Sky_SyugoQuery.Masus_Now(siteiNode.Value.KyokumenConst, shogiGui.Link_Server.Model_Taikyoku.Kifu.CurNode.Value.KyokumenConst.KaisiPside);
            //mikataZukei.DebugWrite("味方の駒");

            // 駒の利き上に駒がないか。
            SySet<SyElement> ban2 = kikiZukei.Minus_Closed(mikataZukei, Util_SyElement_BinaryOperator.Dlgt_Equals_MasuNumber);
            //kikiZukei.DebugWrite("駒の利きLv2");

            shogiGui.Shape_PnlTaikyoku.Shogiban.KikiBan = ban2;

        }



        public TimedB_MouseCapture(MainGui_Csharp shogibanGui)
        {
            this.mainGui = shogibanGui;
            this.MouseEventQueue = new Queue<MouseEventState>();
        }


        public override void Step()
        {
            // 入っているマウス操作イベントのうち、マウスムーブは　１つに　集約　します。
            bool bMouseMove_SceneB_1TumamitaiKoma = false;

            // 入っているマウス操作イベントは、全部捨てていきます。
            MouseEventState[] queue = this.MouseEventQueue.ToArray();
            this.MouseEventQueue.Clear();
            foreach (MouseEventState eventState in queue)
            {
                switch (this.mainGui.Scene)
                {
                    case SceneName.SceneB_1TumamitaiKoma:
                        {
                            #region つまみたい駒


                            switch (eventState.Name2)
                            {
                                case MouseEventStateName.Arive:
                                    {
                                        #region アライブ
                                        //------------------------------
                                        // メナス
                                        //------------------------------
                                        Util_Menace.Menace(this.mainGui);
                                        #endregion
                                    }
                                    break;

                                case MouseEventStateName.MouseMove:
                                    {
                                        #region マウスムーブ
                                        if (bMouseMove_SceneB_1TumamitaiKoma)
                                        {
                                            continue;
                                        }
                                        bMouseMove_SceneB_1TumamitaiKoma = true;

                                        SkyConst src_Sky = mainGui.Model_Manual.GuiSkyConst;

                                        Point mouse = eventState.MouseLocation;

                                        //----------
                                        // 将棋盤：升目
                                        //----------
                                        foreach (UserWidget widget in mainGui.Widgets.Values)
                                        {
                                            if (
                                                eventState.WindowName == widget.Window &&
                                                "Masu" == widget.Type && Okiba.ShogiBan == widget.Okiba)
                                            {
                                                Shape_BtnMasuImpl cell = (Shape_BtnMasuImpl)widget.Object;
                                                cell.LightByMouse(mouse.X, mouse.Y);
                                                if (cell.Light)
                                                {
                                                    mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                                }
                                                break;
                                            }
                                        }

                                        //----------
                                        // 駒置き、駒袋：升目
                                        //----------
                                        foreach (UserWidget widget in mainGui.Widgets.Values)
                                        {
                                            if (
                                                eventState.WindowName == widget.Window &&
                                                "Masu" == widget.Type && widget.Okiba.HasFlag(Okiba.Sente_Komadai | Okiba.Gote_Komadai | Okiba.KomaBukuro))
                                            {
                                                Shape_BtnMasuImpl cell = (Shape_BtnMasuImpl)widget.Object;
                                                cell.LightByMouse(mouse.X, mouse.Y);
                                                if (cell.Light)
                                                {
                                                    mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                                }
                                            }
                                        }

                                        //----------
                                        // 駒
                                        //----------
                                        foreach (Shape_BtnKomaImpl btnKoma in mainGui.Shape_PnlTaikyoku.Btn40Komas)
                                        {
                                            btnKoma.LightByMouse(mouse.X, mouse.Y);
                                            if (btnKoma.Light)
                                            {
                                                mainGui.RepaintRequest.SetFlag_RefreshRequest();

                                                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(btnKoma.Koma).Now);

                                                if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu))
                                                {
                                                    // マウスオーバーした駒の利き
                                                    TimedB_MouseCapture.Check_MouseoverKomaKiki(mainGui, btnKoma.Koma);


                                                    break;
                                                }
                                            }
                                        }


                                        //----------
                                        // ウィジェット
                                        //----------
                                        foreach (UserWidget widget in mainGui.Widgets.Values)
                                        {
                                            if (
                                                eventState.WindowName == widget.Window &&
                                                widget.IsLight_OnFlowB_1TumamitaiKoma)
                                            {
                                                widget.LightByMouse(mouse.X, mouse.Y);
                                                if (widget.Light) { mainGui.RepaintRequest.SetFlag_RefreshRequest(); }
                                            }
                                        }

                                        #endregion
                                    }
                                    break;

                                case MouseEventStateName.MouseLeftButtonDown:
                                    {
                                        #region マウス左ボタンダウン
                                        SceneName nextPhaseB = SceneName.Ignore;
                                        SkyConst src_Sky = mainGui.Model_Manual.GuiSkyConst;

                                        //----------
                                        // 駒
                                        //----------
                                        foreach (Shape_BtnKomaImpl btnKoma in mainGui.Shape_PnlTaikyoku.Btn40Komas)
                                        {
                                            if (btnKoma.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                            {
                                                //>>>>>>>>>> 駒にヒットしました。

                                                if (null != mainGui.Shape_PnlTaikyoku.Btn_TumandeiruKoma(mainGui))
                                                {
                                                    //>>>>>>>>>> 既に選択されている駒があります。→★成ろうとしたときの、取られる相手の駒かも。
                                                    goto gt_Next1;
                                                }

                                                // 既に選択されている駒には無効
                                                if (mainGui.FigTumandeiruKoma == (int)btnKoma.Koma)
                                                {
                                                    goto gt_Next1;
                                                }



                                                if (btnKoma.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y)) //>>>>> 駒をつまみました。
                                                {
                                                    // 駒をつまみます。
                                                    mainGui.SetFigTumandeiruKoma((int)btnKoma.Koma);
                                                    mainGui.Shape_PnlTaikyoku.SelectFirstTouch = true;

                                                    nextPhaseB = SceneName.SceneB_2OkuKoma;

                                                    mainGui.Shape_PnlTaikyoku.SetMouseStarlightOrNull2(
                                                        src_Sky.StarlightIndexOf(btnKoma.Koma)//TODO:改造
                                                        );

                                                    mainGui.Shape_PnlTaikyoku.SetHMovedKoma(Fingers.Error_1);
                                                    mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                                }
                                            }

                                        gt_Next1:
                                            ;
                                        }


                                        //----------
                                        // 既に選択されている駒
                                        //----------
                                        Shape_BtnKoma btnKoma_Selected = mainGui.Shape_PnlTaikyoku.Btn_TumandeiruKoma(mainGui);



                                        //----------
                                        // 各種ボタン
                                        //----------
                                        {
                                            foreach (UserWidget widget in mainGui.Widgets.Values)
                                            {
                                                if (
                                                    eventState.WindowName == widget.Window &&
                                                    widget.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                                {
                                                    if (null != widget.Delegate_MouseHitEvent)
                                                    {
                                                        widget.Delegate_MouseHitEvent(
                                                            mainGui
                                                           , widget
                                                           , btnKoma_Selected
                                                           
                                                           );
                                                    }
                                                }
                                            }
                                        }


                                        mainGui.SetScene(nextPhaseB);

                                        //------------------------------
                                        // このメインパネルの反応
                                        //------------------------------
                                        mainGui.Response("MouseOperation");
                                        #endregion
                                    }
                                    break;

                                case MouseEventStateName.MouseLeftButtonUp:
                                    {
                                        #region マウス左ボタンアップ
                                        SkyConst src_Sky = mainGui.Model_Manual.GuiSkyConst;

                                        //----------
                                        // 将棋盤：升目
                                        //----------
                                        foreach (UserWidget widget in mainGui.Widgets.Values)
                                        {
                                            if (
                                                eventState.WindowName == widget.Window &&
                                                "Masu" == widget.Type && Okiba.ShogiBan == widget.Okiba)
                                            {
                                                Shape_BtnMasuImpl cell = (Shape_BtnMasuImpl)widget.Object;
                                                if (cell.DeselectByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                                {
                                                    mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                                }
                                            }
                                        }

                                        //----------
                                        // 駒置き、駒袋：升目
                                        //----------
                                        foreach (UserWidget widget in mainGui.Widgets.Values)
                                        {
                                            if (
                                                eventState.WindowName == widget.Window &&
                                                "Masu" == widget.Type && widget.Okiba.HasFlag(Okiba.Sente_Komadai | Okiba.Gote_Komadai | Okiba.KomaBukuro))
                                            {
                                                Shape_BtnMasuImpl cell = (Shape_BtnMasuImpl)widget.Object;
                                                if (cell.DeselectByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                                {
                                                    mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                                }
                                            }
                                        }

                                        //----------
                                        // 駒
                                        //----------
                                        foreach (Shape_BtnKomaImpl btnKoma in mainGui.Shape_PnlTaikyoku.Btn40Komas)
                                        {
                                            if (btnKoma.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                            {
                                                //>>>>> つまんでいる駒から、指を放しました
                                                mainGui.SetFigTumandeiruKoma(-1);


                                                RO_Star koma1 = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf((int)btnKoma.Koma).Now);


                                                if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma1.Masu))
                                                {
                                                    //----------
                                                    // 移動済表示
                                                    //----------
                                                    mainGui.Shape_PnlTaikyoku.SetHMovedKoma(btnKoma.Koma);

                                                    //------------------------------
                                                    // 棋譜に符号を追加（マウスボタンが放されたとき）TODO:まだ早い。駒が成るかもしれない。
                                                    //------------------------------
                                                    // 棋譜

                                                    IMoveHalf dstStarlight = mainGui.Shape_PnlTaikyoku.MouseStarlightOrNull2;
                                                    System.Diagnostics.Debug.Assert(null != dstStarlight, "mouseStarlightがヌル");

                                                    IMoveHalf srcStarlight = src_Sky.StarlightIndexOf(btnKoma.Koma);
                                                    System.Diagnostics.Debug.Assert(null != srcStarlight, "komaStarlightがヌル");


                                                    IMove move = new RO_Starbeam(
                                                        dstStarlight.Now,
                                                        srcStarlight.Now,
                                                        mainGui.Shape_PnlTaikyoku.MousePos_FoodKoma != null ? mainGui.Shape_PnlTaikyoku.MousePos_FoodKoma.Komasyurui : Komasyurui14.H00_Null___
                                                        );// 選択している駒の元の場所と、移動先

                                                    //
                                                    // TODO: 一手[巻戻し]のときは追加したくない
                                                    //
                                                    SkyBuffer sky_newChild = new SkyBuffer(src_Sky);
                                                    sky_newChild.SetKaisiPside(Conv_Playerside.Reverse(Playerside.P1));//FIXME:人間が先手でハードコーディング中
                                                    Node<IMove, KyokumenWrapper> newNode = new KifuNodeImpl(
                                                        move,
                                                        new KyokumenWrapper( SkyConst.NewInstance(
                                                            sky_newChild,
                                                            mainGui.Model_Manual.GuiTemezumi + 1//1手進ませる。
                                                            ))
                                                    );
                                                    //MessageBox.Show(
                                                    //    "追加前\n"+
                                                    //    "newNode=KaisiPside=" + newNode.Value.ToKyokumenConst.KaisiPside,
                                                    //    "デバッグ");

                                                    //マウスの左ボタンを放したときです。
                                                    string moveStr = ConvMoveStrSfen.ToMoveStrSfen(newNode.Key);
                                                    if (!((KifuNode)mainGui.Link_Server.Model_Taikyoku.Kifu.CurNode).HasTuginoitte(moveStr))
                                                    {
                                                        //----------------------------------------
                                                        // 次ノード追加
                                                        //----------------------------------------
                                                        mainGui.Link_Server.Model_Taikyoku.Kifu.GetSennititeCounter().CountUp_New(Conv_Sky.ToKyokumenHash(newNode.Value.KyokumenConst), "TimedB.Step(1)");
                                                        ((KifuNode)mainGui.Link_Server.Model_Taikyoku.Kifu.CurNode).PutTuginoitte_New(newNode);
                                                    }

                                                    string jsaFugoStr;
                                                    Util_Functions_Server.SetCurNode_Srv(
                                                        mainGui.Link_Server.Model_Taikyoku,
                                                        mainGui.Model_Manual,
                                                        newNode, out jsaFugoStr);
                                                    mainGui.RepaintRequest.SetFlag_RefreshRequest();


                                                    //------------------------------
                                                    // 符号表示
                                                    //------------------------------
                                                    // つまみたい駒の上でマウスの左ボタンを放したとき。
                                                    mainGui.Shape_PnlTaikyoku.SetFugo(jsaFugoStr);



                                                    //------------------------------
                                                    // チェンジターン
                                                    //------------------------------
                                                    if (!mainGui.Shape_PnlTaikyoku.Requested_NaruDialogToShow)
                                                    {
                                                        mainGui.ChangedTurn();//マウス左ボタンを放したのでチェンジターンします。
                                                    }

                                                    mainGui.RepaintRequest.SyuturyokuRequest = RepaintRequestGedanTxt.Kifu;
                                                    mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                                }
                                            }
                                        }




                                        //------------------------------------------------------------
                                        // 選択解除か否か
                                        //------------------------------------------------------------
                                        {
                                            foreach (UserWidget widget in mainGui.Widgets.Values)
                                            {
                                                if (
                                                    eventState.WindowName == widget.Window &&
                                                    widget.DeselectByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y, mainGui))
                                                {
                                                    mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                                }
                                            }
                                        }

                                        //------------------------------
                                        // このメインパネルの反応
                                        //------------------------------
                                        mainGui.Response("MouseOperation");

                                        #endregion
                                    }
                                    break;

                            }
                            #endregion
                        }
                        break;

                    case SceneName.SceneB_2OkuKoma:
                        {
                            #region 置く駒

                            switch (eventState.Name2)
                            {
                                case MouseEventStateName.MouseLeftButtonUp:
                                    {
                                        #region マウス左ボタンアップ
                                        Node<IMove, KyokumenWrapper> siteiNode = mainGui.Link_Server.Model_Taikyoku.Kifu.CurNode;
                                        SkyConst src_Sky = mainGui.Model_Manual.GuiSkyConst;


                                        //----------
                                        // 駒
                                        //----------
                                        foreach (Shape_BtnKomaImpl btnKoma in mainGui.Shape_PnlTaikyoku.Btn40Komas)
                                        {
                                            if (btnKoma.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                            {
                                                //>>>>> マウスが重なっていました

                                                if (mainGui.Shape_PnlTaikyoku.SelectFirstTouch)
                                                {
                                                    // クリックのマウスアップ
                                                    mainGui.Shape_PnlTaikyoku.SelectFirstTouch = false;
                                                }
                                                else
                                                {
                                                    RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(btnKoma.Koma).Now);


                                                    if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu))
                                                    {
                                                        //>>>>> 将棋盤の上に置いてあった駒から、指を放しました
                                                        //System.C onsole.WriteLine("つまんでいる駒を放します。(4)");
                                                        mainGui.SetFigTumandeiruKoma(-1);


                                                        //----------
                                                        // 移動済表示
                                                        //----------
                                                        mainGui.Shape_PnlTaikyoku.SetHMovedKoma(btnKoma.Koma);

                                                        //------------------------------
                                                        // 棋譜に符号を追加（マウスボタンが放されたとき）TODO:まだ早い。駒が成るかもしれない。
                                                        //------------------------------

                                                        IMove move = new RO_Starbeam(
                                                            //btnKoma.Koma,
                                                            mainGui.Shape_PnlTaikyoku.MouseStarlightOrNull2.Now,

                                                            src_Sky.StarlightIndexOf(btnKoma.Koma).Now,

                                                            mainGui.Shape_PnlTaikyoku.MousePos_FoodKoma != null ? mainGui.Shape_PnlTaikyoku.MousePos_FoodKoma.Komasyurui : Komasyurui14.H00_Null___
                                                            );// 選択している駒の元の場所と、移動先

                                                        IMove last;
                                                        {
                                                            last = siteiNode.Key;
                                                        }
                                                        //ShootingStarlightable previousMove = last; //符号の追加が行われる前に退避

                                                        Node<IMove, KyokumenWrapper> newNode =
                                                            new KifuNodeImpl(
                                                                move,
                                                                new KyokumenWrapper(SkyConst.NewInstance(
                                                                    src_Sky,
                                                                    mainGui.Model_Manual.GuiTemezumi + 1//1手進ませる。
                                                                    ))
                                                            );


                                                        //マウスの左ボタンを放したときです。
                                                        {
                                                            //----------------------------------------
                                                            // 次ノード追加
                                                            //----------------------------------------
                                                            mainGui.Link_Server.Model_Taikyoku.Kifu.GetSennititeCounter().CountUp_New(Conv_Sky.ToKyokumenHash(newNode.Value.KyokumenConst), "TimedB.Step(2)");
                                                            ((KifuNode)mainGui.Link_Server.Model_Taikyoku.Kifu.CurNode).PutTuginoitte_New(newNode);
                                                        }

                                                        string jsaFugoStr;
                                                        Util_Functions_Server.SetCurNode_Srv(
                                                            mainGui.Link_Server.Model_Taikyoku,
                                                            mainGui.Model_Manual,
                                                            newNode, out jsaFugoStr);
                                                        mainGui.RepaintRequest.SetFlag_RefreshRequest();


                                                        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
                                                        // ここでツリーの内容は変わっている。
                                                        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

                                                        //------------------------------
                                                        // 符号表示
                                                        //------------------------------
                                                        // 置いた駒からマウスの左ボタンを放したとき
                                                        mainGui.Shape_PnlTaikyoku.SetFugo(jsaFugoStr);



                                                        //------------------------------
                                                        // チェンジターン
                                                        //------------------------------
                                                        if (!mainGui.Shape_PnlTaikyoku.Requested_NaruDialogToShow)
                                                        {
                                                            //System.C onsole.WriteLine("マウス左ボタンを放したのでチェンジターンします。");
                                                            mainGui.ChangedTurn();//マウス左ボタンを放したのでチェンジターンします。
                                                        }

                                                        mainGui.RepaintRequest.SyuturyokuRequest = RepaintRequestGedanTxt.Kifu;
                                                        mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                                    }



                                                }
                                            }
                                        }

                                        //------------------------------
                                        // このメインパネルの反応
                                        //------------------------------
                                        mainGui.Response("MouseOperation");
                                        #endregion
                                    }
                                    break;

                                case MouseEventStateName.MouseLeftButtonDown:
                                    {
                                        #region マウス左ボタンダウン
                                        SceneName nextPhaseB = SceneName.Ignore;

                                        //System.C onsole.WriteLine("B2マウスダウン");

                                        //----------
                                        // つまんでいる駒
                                        //----------
                                        Shape_BtnKoma btnTumandeiruKoma = mainGui.Shape_PnlTaikyoku.Btn_TumandeiruKoma(mainGui);
                                        if (null == btnTumandeiruKoma)
                                        {
                                            //System.C onsole.WriteLine("つまんでいる駒なし");
                                            goto gt_nextBlock;
                                        }

                                        //>>>>> 選択されている駒があるとき

                                        IMoveHalf tumandeiruLight = mainGui.Model_Manual.GuiSkyConst.StarlightIndexOf((int)btnTumandeiruKoma.Finger);


                                        //----------
                                        // 指したい先
                                        //----------
                                        Shape_BtnMasuImpl btnSasitaiMasu = null;

                                        //----------
                                        // 将棋盤：升目   ＜移動先など＞
                                        //----------
                                        foreach (UserWidget widget in mainGui.Widgets.Values)
                                        {
                                            if (
                                                eventState.WindowName == widget.Window &&
                                                "Masu" == widget.Type && Okiba.ShogiBan == widget.Okiba)
                                            {
                                                if (widget.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))//>>>>> 指したいマスはここです。
                                                {
                                                    btnSasitaiMasu = (Shape_BtnMasuImpl)widget.Object;
                                                    break;
                                                }
                                            }
                                        }


                                        //----------
                                        // 駒置き、駒袋：升目
                                        //----------
                                        foreach (UserWidget widget in mainGui.Widgets.Values)
                                        {
                                            if (
                                                eventState.WindowName == widget.Window &&
                                                "Masu" == widget.Type && widget.Okiba.HasFlag(Okiba.Sente_Komadai | Okiba.Gote_Komadai | Okiba.KomaBukuro))
                                            {
                                                Shape_BtnMasuImpl btnSasitaiMasu2 = (Shape_BtnMasuImpl)widget.Object;
                                                if (btnSasitaiMasu2.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))//>>>>> 升目をクリックしたとき
                                                {
                                                    bool match = false;

                                                    mainGui.Model_Manual.GuiSkyConst.Foreach_Starlights((Finger finger, IMoveHalf ml, ref bool toBreak) =>
                                                    {
                                                        RO_Star koma = Util_Starlightable.AsKoma(ml.Now);

                                                        if (koma.Masu == btnSasitaiMasu2.Zahyo)
                                                        {
                                                            //>>>>> そこに駒が置いてあった。
#if DEBUG
                                    MessageBox.Show("駒が置いてあった","デバッグ中");
#endif
                                                            match = true;
                                                            toBreak = true;
                                                        }
                                                    });

                                                    if (!match)
                                                    {
                                                        btnSasitaiMasu = btnSasitaiMasu2;
                                                        goto gt_EndKomaoki;
                                                    }
                                                }
                                            }
                                        }
                                    gt_EndKomaoki:
                                        ;

                                        if (null == btnSasitaiMasu)
                                        {
                                            // 指したいマスなし
                                            goto gt_nextBlock;
                                        }



                                        //指したいマスが選択されたとき

                                        // TODO:合法手かどうか判定したい。

                                        if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(btnSasitaiMasu.Zahyo))//>>>>> 将棋盤：升目   ＜移動先など＞
                                        {

                                            //------------------------------
                                            // 成る／成らない
                                            //------------------------------
                                            //
                                            //      盤上の、不成の駒で、　／　相手陣に入るものか、相手陣から出てくる駒　※先手・後手区別なし
                                            //
                                            RO_Star koma = Util_Starlightable.AsKoma(tumandeiruLight.Now);


                                            if (
                                                    Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu) && Util_Sky_BoolQuery.IsNareruKoma(tumandeiruLight)
                                                    &&
                                                    (
                                                        Util_Masu10.InAitejin(btnSasitaiMasu.Zahyo, mainGui.Link_Server.Model_Taikyoku.Kifu.CurNode.Value.KyokumenConst.KaisiPside)
                                                        ||
                                                        Util_Sky_BoolQuery.InAitejin(tumandeiruLight)
                                                    )
                                                )
                                            {
                                                // 成るか／成らないか ダイアログボックスを表示します。
                                                mainGui.Shape_PnlTaikyoku.Request_NaruDialogToShow(true);
                                            }


                                            if (mainGui.Shape_PnlTaikyoku.Requested_NaruDialogToShow)
                                            {
                                                // 成る／成らないボタン表示
                                                mainGui.GetWidget("BtnNaru").Visible = true;
                                                mainGui.GetWidget("BtnNaranai").Visible = true;
                                                mainGui.Shape_PnlTaikyoku.SetNaruMasu(btnSasitaiMasu);
                                                nextPhaseB = SceneName.SceneB_3ErabuNaruNaranai;
                                            }
                                            else
                                            {
                                                mainGui.GetWidget("BtnNaru").Visible = false;
                                                mainGui.GetWidget("BtnNaranai").Visible = false;

                                                // 駒を動かします。
                                                {
                                                    // GuiからServerへ渡す情報
                                                    Komasyurui14 syurui;
                                                    IMoveHalf dst;
                                                    
                                                    Util_Function_Csharp.Komamove1a_49Gui(out syurui, out dst, btnTumandeiruKoma, btnSasitaiMasu, mainGui);

                                                    // ServerからGuiへ渡す情報
                                                    bool torareruKomaAri;
                                                    RO_Star koma_Food_after;
                                                    Util_Functions_Server.Komamove1a_50Srv(out torareruKomaAri, out koma_Food_after, dst, btnTumandeiruKoma.Koma, Util_Starlightable.AsKoma(dst.Now), mainGui.Model_Manual);

                                                    Util_Function_Csharp.Komamove1a_51Gui(torareruKomaAri, koma_Food_after, mainGui);
                                                }

                                                nextPhaseB = SceneName.SceneB_1TumamitaiKoma;
                                            }

                                            mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                        }
                                        else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                                            Conv_SyElement.ToOkiba(btnSasitaiMasu.Zahyo)))//>>>>> 駒置き：升目
                                        {
                                            //System.C onsole.WriteLine("駒台上");

                                            RO_Star koma = Util_Starlightable.AsKoma(mainGui.Model_Manual.GuiSkyConst.StarlightIndexOf(btnTumandeiruKoma.Koma).Now);

                                            mainGui.Model_Manual.SetGuiSky(
                                                SkyConst.NewInstance_OverwriteOrAdd_Light(
                                                    mainGui.Model_Manual.GuiSkyConst,
                                                    mainGui.Model_Manual.GuiTemezumi + 1,//1手進める。
                                                    btnTumandeiruKoma.Koma,
                                                    new RO_Starlight(
                                                        new RO_Star(
                                                            Conv_Okiba.ToPside(Conv_SyElement.ToOkiba(btnSasitaiMasu.Zahyo)),// 先手の駒置きに駒を置けば、先手の向きに揃えます。
                                                            btnSasitaiMasu.Zahyo,
                                                            Util_Komasyurui14.NarazuCaseHandle(koma.Komasyurui)
                                                        )
                                                    )
                                                )
                                            );

                                            nextPhaseB = SceneName.SceneB_1TumamitaiKoma;

                                            mainGui.RepaintRequest.SetFlag_RecalculateRequested();// 駒の再描画要求
                                            mainGui.RepaintRequest.SetFlag_RefreshRequest();
                                        }


                                    gt_nextBlock:

                                        //----------
                                        // 既に選択されている駒
                                        //----------
                                        Shape_BtnKoma btnKoma_Selected = mainGui.Shape_PnlTaikyoku.Btn_TumandeiruKoma(mainGui);



                                        //----------
                                        // 初期配置ボタン
                                        //----------

                                        {
                                            foreach (UserWidget widget in mainGui.Widgets.Values)
                                            {
                                                if (
                                                    eventState.WindowName == widget.Window &&
                                                    widget.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                                {
                                                    if (null != widget.Delegate_MouseHitEvent)
                                                    {
                                                        widget.Delegate_MouseHitEvent(
                                                            mainGui
                                                           , widget
                                                           , btnKoma_Selected
                                                           
                                                           );
                                                    }
                                                }
                                            }
                                        }


                                        mainGui.SetScene(nextPhaseB);

                                        //------------------------------
                                        // このメインパネルの反応
                                        //------------------------------
                                        mainGui.Response("MouseOperation");
                                        #endregion
                                    }
                                    break;

                                case MouseEventStateName.MouseRightButtonDown:
                                    {
                                        #region マウス右ボタンダウン
                                        // 各駒の、移動済フラグを解除
                                        //System.C onsole.WriteLine("つまんでいる駒を放します。(5)");
                                        mainGui.SetFigTumandeiruKoma(-1);
                                        mainGui.Shape_PnlTaikyoku.SelectFirstTouch = false;

                                        //------------------------------
                                        // 状態を戻します。
                                        //------------------------------
                                        mainGui.SetScene(SceneName.SceneB_1TumamitaiKoma);

                                        //------------------------------
                                        // このメインパネルの反応
                                        //------------------------------
                                        mainGui.Response("MouseOperation");
                                        #endregion
                                    }
                                    break;
                            }
                            #endregion
                        }
                        break;

                    case SceneName.SceneB_3ErabuNaruNaranai:
                        {
                            #region 成る成らない

                            switch (eventState.Name2)
                            {
                                case MouseEventStateName.MouseLeftButtonDown:
                                    {
                                        #region マウス左ボタンダウン
                                        SceneName nextPhaseB = SceneName.Ignore;
                                        //GuiSky この関数の途中で変更される。ローカル変数に入れているものは古くなる。

                                        //----------
                                        // 既に選択されている駒
                                        //----------
                                        Shape_BtnKoma btnKoma_Selected = mainGui.Shape_PnlTaikyoku.Btn_TumandeiruKoma(mainGui);

                                        string[] buttonNames = new string[]{
                                            "BtnNaru",// [成る]ボタン
                                            "BtnNaranai"// [成らない]ボタン
                                        };
                                        foreach (string buttonName in buttonNames)
                                        {
                                            UserWidget widget = mainGui.GetWidget(buttonName);

                                            if (
                                                eventState.WindowName == widget.Window &&
                                                widget.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                            {
                                                if (null != widget.Delegate_MouseHitEvent)
                                                {
                                                    widget.Delegate_MouseHitEvent(
                                                        mainGui
                                                       , widget
                                                       , btnKoma_Selected
                                                       
                                                       );
                                                }
                                            }
                                        }


                                        //

                                        //----------
                                        // 初期配置ボタン
                                        //----------

                                        {
                                            foreach (UserWidget widget in mainGui.Widgets.Values)
                                            {
                                                if (
                                                    eventState.WindowName == widget.Window &&
                                                    widget.HitByMouse(eventState.MouseLocation.X, eventState.MouseLocation.Y))
                                                {
                                                    if (null != widget.Delegate_MouseHitEvent)
                                                    {
                                                        widget.Delegate_MouseHitEvent(
                                                            mainGui
                                                           , widget
                                                           , btnKoma_Selected
                                                           
                                                           );
                                                    }
                                                }
                                            }
                                        }


                                        mainGui.SetScene(nextPhaseB);

                                        //------------------------------
                                        // このメインパネルの反応
                                        //------------------------------
                                        mainGui.Response("MouseOperation");
                                        #endregion
                                    }
                                    break;
                            }
                            #endregion

                        }
                        break;
                }
            }
            





        //gt_EndMethod1:
        //    ;
        }
    }


}
