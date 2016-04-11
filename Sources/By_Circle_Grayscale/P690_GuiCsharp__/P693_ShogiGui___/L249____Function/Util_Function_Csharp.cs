using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P234_Komahaiyaku.L500____Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P296_ConvJsa____.L500____Converter;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P324_KifuTree___.L250____Struct;
using Grayscale.P461_Server_____.L250____Util;
using Grayscale.P693_ShogiGui___.L___080_Shape;
using Grayscale.P693_ShogiGui___.L___499_Repaint;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L060____TextBoxListener;
using Grayscale.P693_ShogiGui___.P703_ShogiGui___.L101____Conv;
using System.Drawing;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P693_ShogiGui___.L249____Function
{
    public abstract class Util_Function_Csharp
    {
        /// <summary>
        /// [初期配置]ボタン
        /// </summary>
        public static void Perform_SyokiHaichi(
            MainGui_Csharp mainGui,
            KwErrorHandler errH
            )
        {
            mainGui.Link_Server.Model_Taikyoku.Kifu.Clear();// 棋譜を空っぽにします。
            mainGui.Link_Server.Model_Taikyoku.Kifu.SetProperty(Word_KifuTree.PropName_Startpos, "startpos");//平手の初期局面
            Playerside firstPside = Playerside.P1;

            KifuNode newNode = new KifuNodeImpl(
                                        Util_Sky258A.ROOT_SASITE,//ルートなので
                                        new KyokumenWrapper(Util_SkyWriter.New_Hirate(firstPside))//[初期配置]ボタン押下時
                                        );

            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
            // ここで棋譜の変更をします。
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
            string jsaFugoStr;
            Util_Functions_Server.SetCurNode_Srv(
                mainGui.Link_Server.Model_Taikyoku,
                mainGui.Model_Manual,
                newNode, out jsaFugoStr, errH);
            mainGui.RepaintRequest.SetFlag_RefreshRequest();

            mainGui.RepaintRequest.SetFlag_RecalculateRequested();// 駒の再描画要求
            mainGui.RepaintRequest.SyuturyokuRequest = RepaintRequestGedanTxt.Clear;
            mainGui.RepaintRequest.SetFlag_RefreshRequest();
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// [巻戻し]ボタン
        /// ************************************************************************************************************************
        /// </summary>
        public static bool Makimodosi_Gui(
            MainGui_Csharp shogiGui,
            Finger movedKoma,
            Finger foodKoma,
            string fugoJStr,
            string backedInputText,
            KwErrorHandler errH)
        {
            //------------------------------
            // チェンジターン
            //------------------------------
            shogiGui.ChangedTurn(errH);//[巻戻し]ボタンを押したあと


            //------------------------------
            // 符号表示
            //------------------------------
            shogiGui.Shape_PnlTaikyoku.SetFugo(fugoJStr);


            Shape_BtnKoma btn_movedKoma = Conv_Koma_InGui.FingerToKomaBtn(movedKoma, shogiGui);
            Shape_BtnKoma btn_foodKoma = Conv_Koma_InGui.FingerToKomaBtn(foodKoma, shogiGui);//取られた駒
            //------------------------------------------------------------
            // 駒・再描画
            //------------------------------------------------------------
            if (
                null != btn_movedKoma//動かした駒
                ||
                null != btn_foodKoma//取ったときに下にあった駒（巻戻しのときは、これは無し）
                )
            {
                shogiGui.RepaintRequest.SetFlag_RecalculateRequested();// 駒の再描画要求
            }

            // 巻き戻したので、符号が入ります。
            {
                shogiGui.RepaintRequest.NyuryokuText = fugoJStr + " " + backedInputText;// 入力欄
                shogiGui.RepaintRequest.SyuturyokuRequest = RepaintRequestGedanTxt.Kifu;
            }
            shogiGui.RepaintRequest.SetFlag_RefreshRequest();

            return true;
        }

        public static bool Komaokuri_Gui(string restText, MainGui_Csharp shogiGui, KwErrorHandler errH)
        {
            //------------------------------
            // 符号表示
            //------------------------------
            {
                Node<Starbeamable, KyokumenWrapper> node6 = shogiGui.Link_Server.Model_Taikyoku.Kifu.CurNode;

                // [コマ送り][再生]ボタン
                string jsaFugoStr = Conv_SasiteStr_Jsa.ToSasiteStr_Jsa(node6,errH);

                shogiGui.Shape_PnlTaikyoku.SetFugo(jsaFugoStr);
            }



            shogiGui.RepaintRequest.SetFlag_RecalculateRequested();// 再描画1

            shogiGui.RepaintRequest.NyuryokuText = restText;//追加
            shogiGui.RepaintRequest.SetFlag_RefreshRequest(); // GUIに通知するだけ。


            return true;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// テキストボックスに入力された、符号の読込み
        /// ************************************************************************************************************************
        /// </summary>
        public static string ReadLine_FromTextbox()
        {
            return TextboxListener.DefaultInstance.ReadText1().Trim();
        }

        public static void Komamove1a_49Gui(
            out Komasyurui14 toSyurui,
            out Starlight dst,
            Shape_BtnKoma btnKoma_Selected,
            Shape_BtnMasu btnMasu,
            MainGui_Csharp mainGui
        )
        {
            // 駒の種類
            if (mainGui.Naru)
            {
                // 成ります

                toSyurui = Util_Komasyurui14.NariCaseHandle[(int)Util_Komahaiyaku184.Syurui(Util_Starlightable.AsKoma(mainGui.Shape_PnlTaikyoku.MouseStarlightOrNull2.Now).Haiyaku)];
                mainGui.SetNaruFlag(false);
            }
            else
            {
                // そのまま
                toSyurui = Util_Komahaiyaku184.Syurui(Util_Starlightable.AsKoma(mainGui.Shape_PnlTaikyoku.MouseStarlightOrNull2.Now).Haiyaku);
            }


            // 置く駒
            {
                dst = new RO_Starlight(
                    //btnKoma_Selected.Finger,
                    new RO_Star(
                        Util_Starlightable.AsKoma(mainGui.Model_Manual.GuiSkyConst.StarlightIndexOf(btnKoma_Selected.Finger).Now).Pside,
                        btnMasu.Zahyo,
                        toSyurui
                        )
                );
            }


            //------------------------------------------------------------
            // 「取った駒種類_巻戻し用」をクリアーします。
            //------------------------------------------------------------
            mainGui.Shape_PnlTaikyoku.MousePos_FoodKoma = null;

        }

        /// <summary>
        /// 取った駒がある場合のみ。
        /// </summary>
        /// <param name="koma_Food_after"></param>
        /// <param name="shogiGui"></param>
        public static void Komamove1a_51Gui(
            bool torareruKomaAri,
            RO_Star koma_Food_after,
            MainGui_Csharp shogiGui
        )
        {
            if (torareruKomaAri)
            {
                //------------------------------
                // 「取った駒種類_巻戻し用」を棋譜に覚えさせます。（差替え）
                //------------------------------
                shogiGui.Shape_PnlTaikyoku.MousePos_FoodKoma = koma_Food_after;//2014-10-19 21:04 追加
            }

            shogiGui.RepaintRequest.SetFlag_RecalculateRequested();
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 局面に合わせて、駒ボタンのx,y位置を変更します
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="btnKoma">駒</param>
        public static void Redraw_KomaLocation(
            Finger figKoma,
            MainGui_Csharp mainGui,
            KwErrorHandler errH
            )
        {
            RO_Star koma = Util_Starlightable.AsKoma(mainGui.Model_Manual.GuiSkyConst.StarlightIndexOf(figKoma).Now);

            Shape_BtnKoma btnKoma = Conv_Koma_InGui.FingerToKomaBtn(figKoma, mainGui);

            // マスと駒の隙間（パディング）
            int padX = 2;
            int padY = 2;

            int suji;
            int dan;
            Util_MasuNum.TryMasuToSuji(koma.Masu, out suji);
            Util_MasuNum.TryMasuToDan(koma.Masu, out dan);

            switch (Conv_SyElement.ToOkiba(koma.Masu))
            {
                case Okiba.ShogiBan:
                    btnKoma.SetBounds(new Rectangle(
                        mainGui.Shape_PnlTaikyoku.Shogiban.SujiToX(suji) + padX,
                        mainGui.Shape_PnlTaikyoku.Shogiban.DanToY(dan) + padY,
                        btnKoma.Bounds.Width,
                        btnKoma.Bounds.Height
                        ));
                    break;

                case Okiba.Sente_Komadai:
                    btnKoma.SetBounds(new Rectangle(
                        mainGui.Shape_PnlTaikyoku.KomadaiArr[0].SujiToX(suji) + padX,
                        mainGui.Shape_PnlTaikyoku.KomadaiArr[0].DanToY(dan) + padY,
                        btnKoma.Bounds.Width,
                        btnKoma.Bounds.Height
                        ));
                    break;

                case Okiba.Gote_Komadai:
                    btnKoma.SetBounds(new Rectangle(
                        mainGui.Shape_PnlTaikyoku.KomadaiArr[1].SujiToX(suji) + padX,
                        mainGui.Shape_PnlTaikyoku.KomadaiArr[1].DanToY(dan) + padY,
                        btnKoma.Bounds.Width,
                        btnKoma.Bounds.Height
                        ));
                    break;

                case Okiba.KomaBukuro:
                    btnKoma.SetBounds(new Rectangle(
                        mainGui.Shape_PnlTaikyoku.KomadaiArr[2].SujiToX(suji) + padX,
                        mainGui.Shape_PnlTaikyoku.KomadaiArr[2].DanToY(dan) + padY,
                        btnKoma.Bounds.Width,
                        btnKoma.Bounds.Height
                        ));
                    break;
            }
        }


    }
}
