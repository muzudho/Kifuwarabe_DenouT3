using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P239_ConvWords__.L500____Converter;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P276_SeizaStartp.L500____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P324_KifuTree___.L250____Struct;
using Grayscale.P341_Ittesasu___.L125____UtilB;
using Grayscale.P461_Server_____.L250____Util;
using Grayscale.P693_ShogiGui___.L___080_Shape;
using Grayscale.P693_ShogiGui___.L___125_Scene;
using Grayscale.P693_ShogiGui___.L___491_Event;
using Grayscale.P693_ShogiGui___.L___492_Widgets;
using Grayscale.P693_ShogiGui___.L___499_Repaint;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L249____Function;
using Grayscale.P693_ShogiGui___.L480____Util;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P693_ShogiGui___.L491____Event
{

    /// <summary>
    /// シングルトン
    /// </summary>
    public class Event_CsharpImpl
    {
        /// <summary>
        /// シングルトン。
        /// </summary>
        /// <returns></returns>
        public static Event_CsharpImpl GetInstance()
        {
            if (null == Event_CsharpImpl.instance)
            {
                Event_CsharpImpl ins = new Event_CsharpImpl();
                Event_CsharpImpl.instance = ins;

                //
                // [成る]ボタンのイベント。
                //
                ins.delegate_BtnNaru = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH2
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    shogibanGui2.SetNaruFlag(true);
                    ins.After_NaruNaranai_ButtonPushed(
                        shogibanGui2
                        , btnKoma_Selected
                        , errH2
                        );
                };

                //
                // [成らない]ボタンのイベント。
                //
                ins.delegate_BtnNaranai = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    shogibanGui2.SetNaruFlag(false);
                    ins.After_NaruNaranai_ButtonPushed(
                        shogibanGui2
                        , btnKoma_Selected
                        , errH
                        );
                };

                //
                // [クリアー]ボタンのイベント。
                //
                ins.delegate_BtnClear = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    Util_Lua_Csharp.ShogiGui = shogibanGui2;
                    Util_Lua_Csharp.ErrH = errH;
                    Util_Lua_Csharp.Perform("click_clearButton");
                };

                //
                // [再生]ボタンのイベント。
                //
                ins.delegate_BtnPlay = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    Util_Lua_Csharp.ShogiGui = shogibanGui2;
                    Util_Lua_Csharp.ErrH = errH;
                    Util_Lua_Csharp.Perform("click_playButton");
                };

                //
                // [コマ送り]ボタンのイベント。
                //
                ins.delegate_BtnForward = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp mainGui3 = (MainGui_Csharp)obj_shogiGui2;

                    string restText = Util_Function_Csharp.ReadLine_FromTextbox();
                    Util_Functions_Server.Komaokuri_Srv(
                        ref restText,
                        mainGui3.Link_Server.Model_Taikyoku,
                        mainGui3.Model_Manual,
                        errH);
                    Util_Function_Csharp.Komaokuri_Gui(restText, mainGui3, errH);
                    Util_Menace.Menace(mainGui3, errH);// メナス
                };

                //
                // [巻き戻し]ボタンのイベント。
                //
                ins.delegate_BtnBackward = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    Finger movedKoma;
                    Finger foodKoma;//取られた駒
                    string fugoJStr;

                    if (!Util_Functions_Server.Makimodosi_Srv(out movedKoma, out foodKoma, out fugoJStr, shogibanGui2.Link_Server.Model_Taikyoku, errH))
                    {
                        goto gt_EndBlock;
                    }

                    Util_Function_Csharp.Makimodosi_Gui(shogibanGui2, movedKoma, foodKoma, fugoJStr, Util_Function_Csharp.ReadLine_FromTextbox(), errH);
                    Util_Menace.Menace(shogibanGui2, errH);//メナス

                gt_EndBlock:
                    ;
                };

                //
                // [ログ出せ]ボタンのイベント。
                //
                ins.delegate_BtnLogdase = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;

                    shogibanGui2.Logdase(errH);
                };

                //
                // [壁置く]ボタンのイベント。
                //
                ins.delegate_BtnKabeOku = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH
                    ) =>
                {
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    UserWidget widget = shogibanGui2.GetWidget("BtnKabeOku");

                    // [壁置く]←→[駒動かす]切替
                    switch (widget.Text)
                    {
                        case "壁置く":
                            widget.Text = "駒動かす";
                            break;
                        default:
                            widget.Text = "壁置く";
                            break;
                    }

                    shogibanGui2.RepaintRequest.SetFlag_RefreshRequest();
                };

                //
                // [出力切替]ボタンのイベント。
                //
                ins.delegate_BtnSyuturyokuKirikae = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH2
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    switch (shogibanGui2.SyuturyokuKirikae)
                    {
                        case SyuturyokuKirikae.Japanese:
                            shogibanGui2.SetSyuturyokuKirikae(SyuturyokuKirikae.Sfen);
                            break;
                        case SyuturyokuKirikae.Sfen:
                            shogibanGui2.SetSyuturyokuKirikae(SyuturyokuKirikae.Html);
                            break;
                        case SyuturyokuKirikae.Html:
                            shogibanGui2.SetSyuturyokuKirikae(SyuturyokuKirikae.Japanese);
                            break;
                    }

                    shogibanGui2.RepaintRequest.SyuturyokuRequest = RepaintRequestGedanTxt.Kifu;
                };

                //
                // [各種符号]ボタンのイベント。
                //
                ins.delegate_BtnKakusyuFugo = (
                        object obj_shogiGui2
                        , object userWidget2 // UerWidget
                        , object btnKoma_Selected2
                        , KwErrorHandler errH2
                        ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    UserWidget userWidget = (UserWidget)userWidget2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;
                    UserWidget widget = shogibanGui2.GetWidget(userWidget.Name);

                    shogibanGui2.RepaintRequest.SetNyuryokuTextTail(widget.Fugo);
                };

                //
                // [全消]ボタンのイベント。
                //
                ins.delegate_BtnZenkesi = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH2
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    shogibanGui2.RepaintRequest.NyuryokuText = "";
                };

                //
                // [ここから採譜]ボタンのイベント。
                //
                ins.delegate_BtnKokokaraSaifu = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH2
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    Util_KifuTree282.SetStartpos_KokokaraSaifu(shogibanGui2.Link_Server.Model_Taikyoku.Kifu, shogibanGui2.Link_Server.Model_Taikyoku.Kifu.CurNode.Value.KyokumenConst.KaisiPside, errH2);
                    shogibanGui2.RepaintRequest.SyuturyokuRequest = RepaintRequestGedanTxt.Kifu;
                };

                //
                // [初期配置]ボタンのイベント。
                //
                ins.delegate_BtnSyokihaichi = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH2
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp shogibanGui2 = (MainGui_Csharp)obj_shogiGui2;

                    Util_Function_Csharp.Perform_SyokiHaichi(shogibanGui2, errH2);
                };

                //
                // [向き]ボタンのイベント。
                //
                ins.delegate_BtnMuki = (
                    object obj_shogiGui2
                    , object userWidget2 // UerWidget
                    , object btnKoma_Selected2
                    , KwErrorHandler errH2
                    ) =>
                {
                    Shape_BtnKoma btnKoma_Selected = (Shape_BtnKoma)btnKoma_Selected2;
                    MainGui_Csharp mainGui3 = (MainGui_Csharp)obj_shogiGui2;

                    Shape_BtnKoma movedKoma = mainGui3.Shape_PnlTaikyoku.Btn_MovedKoma();

                    RO_Star koma;
                    Finger figKoma = Fingers.Error_1;

                    if (null != movedKoma)
                    {
                        //>>>>> 移動直後の駒があるとき
                        koma = Util_Starlightable.AsKoma(mainGui3.Model_Manual.GuiSkyConst.StarlightIndexOf(movedKoma.Finger).Now);
                        figKoma = movedKoma.Finger;
                    }
                    else if (null != btnKoma_Selected)
                    {
                        //>>>>> 選択されている駒があるとき
                        koma = Util_Starlightable.AsKoma(mainGui3.Model_Manual.GuiSkyConst.StarlightIndexOf(btnKoma_Selected.Koma).Now);
                        figKoma = btnKoma_Selected.Koma;
                    }
                    else
                    {
                        koma = null;
                    }

                    if (null != koma)
                    {
                        KifuNode modifyNode = new KifuNodeImpl(
                            mainGui3.Link_Server.Model_Taikyoku.Kifu.CurNode.Key,//現在の局面を流用
                            new KyokumenWrapper(
                                SkyConst.NewInstance_OverwriteOrAdd_Light(
                                    mainGui3.Model_Manual.GuiSkyConst,
                                    -1,//そのまま
                                    figKoma,
                                    new RO_Starlight(
                                        new RO_Star(
                                            Conv_Playerside.Reverse(koma.Pside),//向きを逆さにします。
                                            koma.Masu,
                                            koma.Komasyurui
                                        )
                                    )
                                )
                            )
                        );
                        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
                        // ここで局面データを変更します。
                        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
                        string jsaFugoStr;
                        Util_Functions_Server.SetCurNode_Srv(
                            mainGui3.Link_Server.Model_Taikyoku,
                            mainGui3.Model_Manual,
                            modifyNode, out jsaFugoStr, errH2);
                        mainGui3.RepaintRequest.SetFlag_RefreshRequest();
                    }
                };

            }
            return Event_CsharpImpl.instance;
        }
        private static Event_CsharpImpl instance;

        /// <summary>
        /// [成る]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnNaru { get{return this.delegate_BtnNaru;} }
        private DELEGATE_MouseHitEvent delegate_BtnNaru;

        /// <summary>
        /// [成らない]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnNaranai { get { return this.delegate_BtnNaranai; } }
        private DELEGATE_MouseHitEvent delegate_BtnNaranai;

        /// <summary>
        /// [クリアー]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnClear { get { return this.delegate_BtnClear; } }
        private DELEGATE_MouseHitEvent delegate_BtnClear;

        /// <summary>
        /// [再生]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnPlay { get { return this.delegate_BtnPlay; } }
        private DELEGATE_MouseHitEvent delegate_BtnPlay;

        /// <summary>
        /// [コマ送り]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnForward { get { return this.delegate_BtnForward; } }
        private DELEGATE_MouseHitEvent delegate_BtnForward;

        /// <summary>
        /// [巻き戻し]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnBackward { get { return this.delegate_BtnBackward; } }
        private DELEGATE_MouseHitEvent delegate_BtnBackward;

        /// <summary>
        /// [ログ出せ]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnLogdase { get { return this.delegate_BtnLogdase; } }
        private DELEGATE_MouseHitEvent delegate_BtnLogdase;

        /// <summary>
        /// [壁置く]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnKabeOku { get { return this.delegate_BtnKabeOku; } }
        private DELEGATE_MouseHitEvent delegate_BtnKabeOku;

        /// <summary>
        /// [出力切替]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnSyuturyokuKirikae { get { return this.delegate_BtnSyuturyokuKirikae; } }
        private DELEGATE_MouseHitEvent delegate_BtnSyuturyokuKirikae;

        /// <summary>
        /// 各種符号ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnKakusyuFugo { get { return this.delegate_BtnKakusyuFugo; } }
        private DELEGATE_MouseHitEvent delegate_BtnKakusyuFugo;

        /// <summary>
        /// [全消]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnZenkesi { get { return this.delegate_BtnZenkesi; } }
        private DELEGATE_MouseHitEvent delegate_BtnZenkesi;

        /// <summary>
        /// [ここから採譜]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnKokokaraSaifu { get { return this.delegate_BtnKokokaraSaifu; } }
        private DELEGATE_MouseHitEvent delegate_BtnKokokaraSaifu;

        /// <summary>
        /// [初期配置]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnSyokihaichi { get { return this.delegate_BtnSyokihaichi; } }
        private DELEGATE_MouseHitEvent delegate_BtnSyokihaichi;

        /// <summary>
        /// [向き]ボタンのイベント。
        /// </summary>
        public DELEGATE_MouseHitEvent Delegate_BtnMuki { get { return this.delegate_BtnMuki; } }
        private DELEGATE_MouseHitEvent delegate_BtnMuki;




        /// <summary>
        /// 成る／成らない
        /// </summary>
        /// <param name="mainGui"></param>
        /// <param name="btnTumandeiruKoma"></param>
        /// <param name="errH"></param>
        private void After_NaruNaranai_ButtonPushed(
            MainGui_Csharp mainGui
            , Shape_BtnKoma btnTumandeiruKoma
            , KwErrorHandler errH
        )
        {

            // 駒を動かします。
            {
                // GuiからServerへ渡す情報
                Komasyurui14 syurui;
                Starlight dst;
                Util_Function_Csharp.Komamove1a_49Gui(out syurui, out dst, btnTumandeiruKoma, mainGui.Shape_PnlTaikyoku.NaruBtnMasu, mainGui);

                // ServerからGuiへ渡す情報
                bool torareruKomaAri;
                RO_Star koma_Food_after;
                Util_Functions_Server.Komamove1a_50Srv(out torareruKomaAri, out koma_Food_after, dst, btnTumandeiruKoma.Koma, Util_Starlightable.AsKoma(dst.Now), mainGui.Model_Manual, errH);

                Util_Function_Csharp.Komamove1a_51Gui(torareruKomaAri, koma_Food_after, mainGui);
            }

            {
                //----------
                // 移動済表示
                //----------
                mainGui.Shape_PnlTaikyoku.SetHMovedKoma(btnTumandeiruKoma.Finger);

                //------------------------------
                // 棋譜に符号を追加（マウスボタンが放されたとき）TODO:まだ早い。駒が成るかもしれない。
                //------------------------------
                // 棋譜

                Starbeamable sasite = new RO_Starbeam(
                    //btnTumandeiruKoma.Finger,
                    mainGui.Shape_PnlTaikyoku.MouseStarlightOrNull2.Now,

                    mainGui.Model_Manual.GuiSkyConst.StarlightIndexOf(btnTumandeiruKoma.Finger).Now,

                    mainGui.Shape_PnlTaikyoku.MousePos_FoodKoma != null ? mainGui.Shape_PnlTaikyoku.MousePos_FoodKoma.Komasyurui : Komasyurui14.H00_Null___
                    );// 選択している駒の元の場所と、移動先

                KifuNode newNode;
                {
                    //
                    // 成ったので、指し手データ差替え。
                    //
                    StartposImporter.Assert_HirateHonsyogi(new SkyBuffer(mainGui.Model_Manual.GuiSkyConst), "newNode作成前");
                    newNode = new KifuNodeImpl(
                        sasite,
                        new KyokumenWrapper(SkyConst.NewInstance_ReversePside(// 先後を反転させます。
                            mainGui.Model_Manual.GuiSkyConst,
                            mainGui.Model_Manual.GuiSkyConst.Temezumi + 1//１手進める
                            ))
                    );
                    StartposImporter.Assert_HirateHonsyogi(new SkyBuffer(newNode.Value.KyokumenConst), "newNode作成後");


                    //「成る／成らない」ボタンを押したときです。
                    {
                        //----------------------------------------
                        // 次ノード追加
                        //----------------------------------------
                        mainGui.Link_Server.Model_Taikyoku.Kifu.GetSennititeCounter().CountUp_New(Conv_Sky.ToKyokumenHash(newNode.Value.KyokumenConst), "After_NaruNaranai");
                        ((KifuNode)mainGui.Link_Server.Model_Taikyoku.Kifu.CurNode).PutTuginoitte_New(newNode);
                    }

                    // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
                    // ここで棋譜の変更をします。
                    // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
                    string jsaFugoStr;
                    Util_Functions_Server.SetCurNode_Srv(
                        mainGui.Link_Server.Model_Taikyoku,
                        mainGui.Model_Manual,
                        newNode, out jsaFugoStr, errH);
                    mainGui.RepaintRequest.SetFlag_RefreshRequest();

                    //------------------------------
                    // 符号表示
                    //------------------------------
                    // 成る／成らないボタンを押したとき。
                    mainGui.Shape_PnlTaikyoku.SetFugo(jsaFugoStr);
                }




                //------------------------------
                // チェンジターン
                //------------------------------
                if (!mainGui.Shape_PnlTaikyoku.Requested_NaruDialogToShow)
                {
                    //System.C onsole.WriteLine("マウス左ボタンを押したのでチェンジターンします。");
                    mainGui.ChangedTurn(errH);
                }
            }


            mainGui.RepaintRequest.SetFlag_RecalculateRequested();// 駒の再描画要求

            //System.C onsole.WriteLine("つまんでいる駒を放します。(6)");
            mainGui.SetFigTumandeiruKoma(-1);//駒を放した扱いです。

            mainGui.Shape_PnlTaikyoku.SetNaruMasu(null);

            mainGui.RepaintRequest.SyuturyokuRequest = RepaintRequestGedanTxt.Kifu;
            mainGui.RepaintRequest.SetFlag_RefreshRequest();

            Starbeamable last;
            {
                Node<Starbeamable, KyokumenWrapper> kifuElement = mainGui.Link_Server.Model_Taikyoku.Kifu.CurNode;

                last = (Starbeamable)kifuElement.Key;
            }
            mainGui.ChangedTurn(errH);//マウス左ボタンを押したのでチェンジターンします。

            mainGui.Shape_PnlTaikyoku.Request_NaruDialogToShow(false);
            mainGui.GetWidget("BtnNaru").Visible = false;
            mainGui.GetWidget("BtnNaranai").Visible = false;
            mainGui.SetScene(SceneName.SceneB_1TumamitaiKoma);
        }


    }


}
