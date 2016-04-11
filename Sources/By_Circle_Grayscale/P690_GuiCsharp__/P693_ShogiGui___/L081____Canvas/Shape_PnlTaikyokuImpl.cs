using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P256_SeizaFinger.L250____Struct;
using Grayscale.P693_ShogiGui___.L___080_Shape;
using Grayscale.P693_ShogiGui___.L___081_Canvas;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L080____Shape;
using System.Windows.Forms;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P693_ShogiGui___.L081____Canvas
{


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
        public Starlight MouseStarlightOrNull2 { get { return this.mouseStarlightOrNull2; } }
        public void SetMouseStarlightOrNull2(Starlight mouseDd) { this.mouseStarlightOrNull2 = mouseDd; }
        private Starlight mouseStarlightOrNull2;

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
        public Shape_PnlTaikyokuImpl(string widgetName, MainGui_Csharp shogibanGui)
            : base(widgetName, 0, 0, 0, 0)
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
            this.SetBtn40Komas( new Shape_BtnKomaImpl[]{

                new Shape_BtnKomaImpl("#SenteOh",Finger_Honshogi.SenteOh),//[0]
                new Shape_BtnKomaImpl("#GoteOh",Finger_Honshogi.GoteOh),

                new Shape_BtnKomaImpl("#Hi1",Finger_Honshogi.Hi1),
                new Shape_BtnKomaImpl("#Hi2",Finger_Honshogi.Hi2),

                new Shape_BtnKomaImpl("#Kaku1",Finger_Honshogi.Kaku1),
                new Shape_BtnKomaImpl("#Kaku2",Finger_Honshogi.Kaku2),//[5]

                new Shape_BtnKomaImpl("#Kin1",Finger_Honshogi.Kin1),
                new Shape_BtnKomaImpl("#Kin2",Finger_Honshogi.Kin2),
                new Shape_BtnKomaImpl("#Kin3",Finger_Honshogi.Kin3),
                new Shape_BtnKomaImpl("#Kin4",Finger_Honshogi.Kin4),

                new Shape_BtnKomaImpl("#Gin1",Finger_Honshogi.Gin1),//[10]
                new Shape_BtnKomaImpl("#Gin2",Finger_Honshogi.Gin2),
                new Shape_BtnKomaImpl("#Gin3",Finger_Honshogi.Gin3),
                new Shape_BtnKomaImpl("#Gin4",Finger_Honshogi.Gin4),

                new Shape_BtnKomaImpl("#Kei1",Finger_Honshogi.Kei1),
                new Shape_BtnKomaImpl("#Kei2",Finger_Honshogi.Kei2),//[15]
                new Shape_BtnKomaImpl("#Kei3",Finger_Honshogi.Kei3),
                new Shape_BtnKomaImpl("#Kei4",Finger_Honshogi.Kei4),

                new Shape_BtnKomaImpl("#Kyo1",Finger_Honshogi.Kyo1),
                new Shape_BtnKomaImpl("#Kyo2",Finger_Honshogi.Kyo2),
                new Shape_BtnKomaImpl("#Kyo3",Finger_Honshogi.Kyo3),//[20]
                new Shape_BtnKomaImpl("#Kyo4",Finger_Honshogi.Kyo4),

                new Shape_BtnKomaImpl("#Fu1",Finger_Honshogi.Fu1),
                new Shape_BtnKomaImpl("#Fu2",Finger_Honshogi.Fu2),
                new Shape_BtnKomaImpl("#Fu3",Finger_Honshogi.Fu3),
                new Shape_BtnKomaImpl("#Fu4",Finger_Honshogi.Fu4),//[25]
                new Shape_BtnKomaImpl("#Fu5",Finger_Honshogi.Fu5),
                new Shape_BtnKomaImpl("#Fu6",Finger_Honshogi.Fu6),
                new Shape_BtnKomaImpl("#Fu7",Finger_Honshogi.Fu7),
                new Shape_BtnKomaImpl("#Fu8",Finger_Honshogi.Fu8),
                new Shape_BtnKomaImpl("#Fu9",Finger_Honshogi.Fu9),//[30]

                new Shape_BtnKomaImpl("#Fu10",Finger_Honshogi.Fu10),
                new Shape_BtnKomaImpl("#Fu11",Finger_Honshogi.Fu11),
                new Shape_BtnKomaImpl("#Fu12",Finger_Honshogi.Fu12),
                new Shape_BtnKomaImpl("#Fu13",Finger_Honshogi.Fu13),
                new Shape_BtnKomaImpl("#Fu14",Finger_Honshogi.Fu14),//[35]
                new Shape_BtnKomaImpl("#Fu15",Finger_Honshogi.Fu15),
                new Shape_BtnKomaImpl("#Fu16",Finger_Honshogi.Fu16),
                new Shape_BtnKomaImpl("#Fu17",Finger_Honshogi.Fu17),
                new Shape_BtnKomaImpl("#Fu18",Finger_Honshogi.Fu18)//[39]
            });

            //----------
            // 将棋盤
            //----------
            this.Shogiban = new Shape_PnlShogibanImpl("#Shogiban",200, 220, shogibanGui);

            //----------
            // 駒置き
            //----------
            this.KomadaiArr = new Shape_PnlKomadaiImpl[3];
            this.KomadaiArr[0] = new Shape_PnlKomadaiImpl("#Sente_Komadai", Okiba.Sente_Komadai, 610, 220, 81, shogibanGui);
            this.KomadaiArr[1] = new Shape_PnlKomadaiImpl("#Gote_Komadai", Okiba.Gote_Komadai, 10, 220, 121, shogibanGui);
            this.KomadaiArr[2] = new Shape_PnlKomadaiImpl("#KomaBukuro", Okiba.KomaBukuro, 810, 220, 161, shogibanGui);

            //----------
            // 符号表示
            //----------
            this.lblFugo = new Shape_LblBoxImpl("#FugoLabel","符号", 480, 145);

            //----------
            // 先後表示
            //----------
            this.lblPside = new Shape_LblBoxImpl("#PsideLabel","－－", 350, 145);
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
            string windowName,
            KwErrorHandler errH
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
            for (int i = 0; i < this.KomadaiArr.Length;i++ )
            {
                Shape_PnlKomadai k = this.KomadaiArr[i];
                k.Paint( e.Graphics);
            }

            //----------
            // 駒
            //----------
            foreach (Shape_BtnKomaImpl koma in this.Btn40Komas)
            {
                koma.Paint(e.Graphics, shogiGui, errH);
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


            base.Paint(sender, e, shogiGui, windowName, errH);

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
