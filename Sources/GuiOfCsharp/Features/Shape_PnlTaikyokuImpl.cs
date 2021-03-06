﻿namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using System.Windows.Forms;
    using Grayscale.Kifuwarakaku.Entities.Configuration;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

    /// <summary>
    /// ************************************************************************************************************************
    /// 描かれる図画です。１つの対局で描かれるものは、ここにまとめて入れられています。
    /// ************************************************************************************************************************
    /// </summary>
    public class Shape_PnlTaikyokuImpl : Shape_CanvasImpl, Shape_PnlTaikyoku
    {


        #region プロパティ類


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 動かし終わった駒。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Finger MovedKoma
        {
            get
            {
                return this.movedKoma;
            }
        }

        public void SetHMovedKoma(Finger value)
        {
            this.movedKoma = value;
        }

        private Finger movedKoma;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 動かしたい駒。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public bool SelectFirstTouch
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 要求：　成る／成らないダイアログボックス
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        ///     0: なし
        ///     1: 成るか成らないかボタンを表示して決定待ち中。
        /// 
        /// </summary>
        public bool Requested_NaruDialogToShow
        {
            get
            {
                return this.requested_NaruDialogToShow;
            }
        }

        public void Request_NaruDialogToShow(bool show)
        {
            this.requested_NaruDialogToShow = show;
        }
        private bool requested_NaruDialogToShow;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 成る、で移動先
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Shape_BtnMasu NaruBtnMasu
        {
            get
            {
                return this.naruBtnMasu;
            }
        }

        public void SetNaruMasu(Shape_BtnMasu naruMasu)
        {
            this.naruBtnMasu = naruMasu;
        }
        private Shape_BtnMasu naruBtnMasu;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// マウスで駒を動かしたときに使います。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        /// 棋譜[再生]中は使いません。
        /// 
        /// </summary>
        public IMoveHalf MouseStarlightOrNull2 { get { return this.mouseStarlightOrNull2; } }
        public void SetMouseStarlightOrNull2(IMoveHalf mouseDd) { this.mouseStarlightOrNull2 = mouseDd; }
        private IMoveHalf mouseStarlightOrNull2;

        /// <summary>
        /// 「取った駒_巻戻し用」
        /// </summary>
        public RO_Star MousePos_FoodKoma { get; set; }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒ボタンの配列。局面データの駒と、同じ駒ハンドルで一対一対応。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        ///     *Doors…名前の由来：ハンドル１つに対応するから。
        /// 
        /// </summary>
        public Shape_BtnKoma[] Btn40Komas
        {
            get
            {
                return this.btn40Komas;
            }
        }
        public void SetBtn40Komas(Shape_BtnKomaImpl[] btn40Komas)
        {
            this.btn40Komas = btn40Komas;
        }
        private Shape_BtnKomaImpl[] btn40Komas;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 将棋盤
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Shape_PnlShogiban Shogiban
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒置き。[0]先手、[1]後手、[2]駒袋。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Shape_PnlKomadai[] KomadaiArr
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 差し手符号。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="fugo"></param>
        public void SetFugo(string fugo)
        {
            this.lblFugo.Text = fugo;
        }
        private Shape_LblBoxImpl lblFugo;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 先後表示。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        private Shape_LblBoxImpl lblPside;














































        //------------------------------------------------------------
        #endregion


        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクターです。
        /// ************************************************************************************************************************
        /// </summary>
        public Shape_PnlTaikyokuImpl(IEngineConf engineConf, string widgetName, MainGui_Csharp shogibanGui)
            : base(engineConf, widgetName, 0, 0, 0, 0)
        {

            // 初期化
            this.SetHMovedKoma(Fingers.Error_1);


            //----------
            // 成る成らないダイアログ
            //----------
            this.Request_NaruDialogToShow(false);

            //----------
            // 将ボタン
            //----------
            this.SetBtn40Komas(new Shape_BtnKomaImpl[]{

                new Shape_BtnKomaImpl(engineConf, "#SenteOh",Finger_Honshogi.SenteOh),//[0]
                new Shape_BtnKomaImpl(engineConf, "#GoteOh",Finger_Honshogi.GoteOh),

                new Shape_BtnKomaImpl(engineConf, "#Hi1",Finger_Honshogi.Hi1),
                new Shape_BtnKomaImpl(engineConf, "#Hi2",Finger_Honshogi.Hi2),

                new Shape_BtnKomaImpl(engineConf, "#Kaku1",Finger_Honshogi.Kaku1),
                new Shape_BtnKomaImpl(engineConf, "#Kaku2",Finger_Honshogi.Kaku2),//[5]

                new Shape_BtnKomaImpl(engineConf, "#Kin1",Finger_Honshogi.Kin1),
                new Shape_BtnKomaImpl(engineConf, "#Kin2",Finger_Honshogi.Kin2),
                new Shape_BtnKomaImpl(engineConf, "#Kin3",Finger_Honshogi.Kin3),
                new Shape_BtnKomaImpl(engineConf, "#Kin4",Finger_Honshogi.Kin4),

                new Shape_BtnKomaImpl(engineConf, "#Gin1",Finger_Honshogi.Gin1),//[10]
                new Shape_BtnKomaImpl(engineConf, "#Gin2",Finger_Honshogi.Gin2),
                new Shape_BtnKomaImpl(engineConf, "#Gin3",Finger_Honshogi.Gin3),
                new Shape_BtnKomaImpl(engineConf, "#Gin4",Finger_Honshogi.Gin4),

                new Shape_BtnKomaImpl(engineConf, "#Kei1",Finger_Honshogi.Kei1),
                new Shape_BtnKomaImpl(engineConf, "#Kei2",Finger_Honshogi.Kei2),//[15]
                new Shape_BtnKomaImpl(engineConf, "#Kei3",Finger_Honshogi.Kei3),
                new Shape_BtnKomaImpl(engineConf, "#Kei4",Finger_Honshogi.Kei4),

                new Shape_BtnKomaImpl(engineConf, "#Kyo1",Finger_Honshogi.Kyo1),
                new Shape_BtnKomaImpl(engineConf, "#Kyo2",Finger_Honshogi.Kyo2),
                new Shape_BtnKomaImpl(engineConf, "#Kyo3",Finger_Honshogi.Kyo3),//[20]
                new Shape_BtnKomaImpl(engineConf, "#Kyo4",Finger_Honshogi.Kyo4),

                new Shape_BtnKomaImpl(engineConf, "#Fu1",Finger_Honshogi.Fu1),
                new Shape_BtnKomaImpl(engineConf, "#Fu2",Finger_Honshogi.Fu2),
                new Shape_BtnKomaImpl(engineConf, "#Fu3",Finger_Honshogi.Fu3),
                new Shape_BtnKomaImpl(engineConf, "#Fu4",Finger_Honshogi.Fu4),//[25]
                new Shape_BtnKomaImpl(engineConf, "#Fu5",Finger_Honshogi.Fu5),
                new Shape_BtnKomaImpl(engineConf, "#Fu6",Finger_Honshogi.Fu6),
                new Shape_BtnKomaImpl(engineConf, "#Fu7",Finger_Honshogi.Fu7),
                new Shape_BtnKomaImpl(engineConf, "#Fu8",Finger_Honshogi.Fu8),
                new Shape_BtnKomaImpl(engineConf, "#Fu9",Finger_Honshogi.Fu9),//[30]

                new Shape_BtnKomaImpl(engineConf, "#Fu10",Finger_Honshogi.Fu10),
                new Shape_BtnKomaImpl(engineConf, "#Fu11",Finger_Honshogi.Fu11),
                new Shape_BtnKomaImpl(engineConf, "#Fu12",Finger_Honshogi.Fu12),
                new Shape_BtnKomaImpl(engineConf, "#Fu13",Finger_Honshogi.Fu13),
                new Shape_BtnKomaImpl(engineConf, "#Fu14",Finger_Honshogi.Fu14),//[35]
                new Shape_BtnKomaImpl(engineConf, "#Fu15",Finger_Honshogi.Fu15),
                new Shape_BtnKomaImpl(engineConf, "#Fu16",Finger_Honshogi.Fu16),
                new Shape_BtnKomaImpl(engineConf, "#Fu17",Finger_Honshogi.Fu17),
                new Shape_BtnKomaImpl(engineConf, "#Fu18",Finger_Honshogi.Fu18)//[39]
            });

            //----------
            // 将棋盤
            //----------
            this.Shogiban = new Shape_PnlShogibanImpl(engineConf, "#Shogiban", 200, 220, shogibanGui);

            //----------
            // 駒置き
            //----------
            this.KomadaiArr = new Shape_PnlKomadaiImpl[3];
            this.KomadaiArr[0] = new Shape_PnlKomadaiImpl(engineConf, "#Sente_Komadai", Okiba.Sente_Komadai, 610, 220, 81, shogibanGui);
            this.KomadaiArr[1] = new Shape_PnlKomadaiImpl(engineConf, "#Gote_Komadai", Okiba.Gote_Komadai, 10, 220, 121, shogibanGui);
            this.KomadaiArr[2] = new Shape_PnlKomadaiImpl(engineConf, "#KomaBukuro", Okiba.KomaBukuro, 810, 220, 161, shogibanGui);

            //----------
            // 符号表示
            //----------
            this.lblFugo = new Shape_LblBoxImpl(engineConf, "#FugoLabel", "符号", 480, 145);

            //----------
            // 先後表示
            //----------
            this.lblPside = new Shape_LblBoxImpl(engineConf, "#PsideLabel", "－－", 350, 145);
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 対局の描画の一式は、ここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Paint(
            object sender, PaintEventArgs e,
            MainGui_Csharp shogiGui,
            string windowName
            )
        {
            if (!this.Visible)
            {
                goto gt_EndMethod;
            }

            //----------
            // 将棋盤
            //----------
            this.Shogiban.Paint(e.Graphics);

            //----------
            // 駒置き、駒袋
            //----------
            for (int i = 0; i < this.KomadaiArr.Length; i++)
            {
                Shape_PnlKomadai k = this.KomadaiArr[i];
                k.Paint(e.Graphics);
            }

            //----------
            // 駒
            //----------
            foreach (Shape_BtnKomaImpl koma in this.Btn40Komas)
            {
                koma.Paint(e.Graphics, shogiGui);
            }

            //----------
            // 符号表示
            //----------
            this.lblFugo.Paint(e.Graphics);

            //----------
            // 先後表示
            //----------
            Playerside pside = shogiGui.Link_Server.Model_Taikyoku.Kifu.CurNode.Value.KyokumenConst.KaisiPside;
            this.lblPside.Text = Conv_Playerside.ToKanji(pside);
            this.lblPside.Paint(e.Graphics);


            base.Paint(sender, e, shogiGui, windowName);

        gt_EndMethod:
            ;
        }


        /// <summary>
        /// 移動直後の駒を取得。
        /// </summary>
        /// <returns>移動直後の駒。なければヌル</returns>
        public Shape_BtnKoma Btn_MovedKoma()
        {
            Shape_BtnKoma btn = null;

            if (Fingers.Error_1 != this.MovedKoma)
            {
                btn = this.Btn40Komas[(int)this.MovedKoma];
            }

            return btn;
        }

        /// <summary>
        /// つまんでいる駒。
        /// </summary>
        /// <returns>つまんでいる駒。なければヌル</returns>
        public Shape_BtnKoma Btn_TumandeiruKoma(object obj_shogiGui)
        {
            MainGui_Csharp shogiGui = (MainGui_Csharp)obj_shogiGui;
            Shape_BtnKoma found = null;

            if (-1 != shogiGui.FigTumandeiruKoma)
            {
                found = this.Btn40Komas[shogiGui.FigTumandeiruKoma];
            }

            return found;
        }

    }


}
