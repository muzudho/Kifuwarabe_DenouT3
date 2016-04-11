using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P056_Syugoron___.L250____Struct;
using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P693_ShogiGui___.L___080_Shape;
using Grayscale.P693_ShogiGui___.L___492_Widgets;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using System.Collections.Generic;
using System.Drawing;

namespace Grayscale.P693_ShogiGui___.L080____Shape
{

    /// <summary>
    /// ************************************************************************************************************************
    /// 描かれる図画です。将棋盤を描きます。
    /// ************************************************************************************************************************
    /// </summary>
    public class Shape_PnlShogibanImpl : Shape_Abstract, Shape_PnlShogiban
    {

        #region プロパティー

        /// <summary>
        /// ウィジェットにアクセスするために利用。
        /// </summary>
        private MainGui_Csharp ShogibanGui { get; set; }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 升の横幅。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public int MasuWidth
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 升の縦幅。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public int MasuHeight
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 光らせる利き升ハンドル。
        /// </summary>
        public SySet<SyElement> KikiBan
        {
            get;
            set;
        }

        /// <summary>
        /// 枡毎の、利いている駒ハンドルのリスト。
        /// </summary>
        public Dictionary<int,List<int>> HMasu_KikiKomaList
        {
            get;
            set;
        }


        /// <summary>
        /// 将棋盤の枡の数。
        /// </summary>
        public const int NSQUARE = 9 * 9;

        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクターです。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Shape_PnlShogibanImpl(string widgetName, int x, int y, MainGui_Csharp shogibanGui)
            : base(widgetName, x, y, 1, 1)
        {
            this.ShogibanGui = shogibanGui;
            this.MasuWidth = 40;
            this.MasuHeight = 40;

            this.KikiBan = new SySet_Default<SyElement>("利き盤");
            this.HMasu_KikiKomaList = new Dictionary<int, List<int>>();

            //----------
            // 枡に利いている駒への逆リンク（の入れ物を用意）
            //----------
            this.ClearHMasu_KikiKomaList();
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 枡に利いている駒への逆リンク（の入れ物を用意）
        /// ************************************************************************************************************************
        /// </summary>
        public void ClearHMasu_KikiKomaList()
        {
            this.HMasu_KikiKomaList.Clear();

            for (int masuIndex = 0; masuIndex < Shape_PnlShogibanImpl.NSQUARE; masuIndex++)
            {
                this.HMasu_KikiKomaList.Add(masuIndex, new List<int>());
            }
        }



        /// <summary>
        /// ************************************************************************************************************************
        /// 筋を指定すると、ｘ座標を返します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="suji"></param>
        /// <returns></returns>
        public int SujiToX(int suji)
        {
            return (9 - suji) * this.MasuWidth + this.Bounds.X;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 段を指定すると、ｙ座標を返します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="dan"></param>
        /// <returns></returns>
        public int DanToY(int dan)
        {
            return (dan - 1) * this.MasuHeight + this.Bounds.Y;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 将棋盤の描画はここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            if (!this.Visible)
            {
                goto gt_EndMethod;
            }

            //----------
            // 筋の数字
            //----------
            for (int i = 0; i < 9; i++)
            {
                g.DrawString(Conv_Int.ToArabiaSuji(i + 1), new Font("ＭＳ ゴシック", 25.0f), Brushes.Black, new Point((8 - i) * this.MasuWidth + this.Bounds.X - 8, -1 * this.MasuHeight + this.Bounds.Y));
            }

            //----------
            // 段の数字
            //----------
            for (int i = 0; i < 9; i++)
            {
                g.DrawString(Conv_Int.ToKanSuji(i + 1), new Font("ＭＳ ゴシック", 23.0f), Brushes.Black, new Point(9 * this.MasuWidth + this.Bounds.X, i * this.MasuHeight + this.Bounds.Y));
                g.DrawString(Conv_Int.ToAlphabet(i + 1), new Font("ＭＳ ゴシック", 11.0f), Brushes.Black, new Point(9 * this.MasuWidth + this.Bounds.X, i * this.MasuHeight + this.Bounds.Y));
            }


            //----------
            // 水平線
            //----------
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(Pens.Black,
                    0 * this.MasuWidth + this.Bounds.X,
                    i * this.MasuHeight + this.Bounds.Y,
                    9 * this.MasuHeight + this.Bounds.X,
                    i * this.MasuHeight + this.Bounds.Y
                    );
            }

            //----------
            // 垂直線
            //----------
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(Pens.Black,
                    i * this.MasuWidth + this.Bounds.X,
                    0 * this.MasuHeight + this.Bounds.Y,
                    i * this.MasuHeight + this.Bounds.X,
                    9 * this.MasuHeight + this.Bounds.Y
                    );
            }


            //----------
            // 升目
            //----------
            foreach (UserWidget widget in this.ShogibanGui.Widgets.Values)
            {
                if ("Masu" == widget.Type && Okiba.ShogiBan == widget.Okiba)
                {
                    Shape_BtnMasuImpl cell = (Shape_BtnMasuImpl)widget.Object;
                    SySet<SyElement> masus2 = new SySet_Default<SyElement>("何かの升");
                    masus2.AddElement(Masu_Honshogi.Masus_All[widget.MasuHandle]);

                    cell.Kiki = this.KikiBan.ContainsAll(masus2);
                    cell.KikiSu = this.HMasu_KikiKomaList[widget.MasuHandle].Count;
                    cell.Paint(g);
                }
            }

        gt_EndMethod:
            ;
        }


    }
}
