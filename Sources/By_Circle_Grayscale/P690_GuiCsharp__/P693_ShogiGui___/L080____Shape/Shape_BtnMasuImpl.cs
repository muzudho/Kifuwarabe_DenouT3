using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P693_ShogiGui___.L___080_Shape;
using System.Drawing;


namespace Grayscale.P693_ShogiGui___.L080____Shape
{


    /// <summary>
    /// ************************************************************************************************************************
    /// 描かれる図画です。ボタンとして押せる升目を描きます。
    /// ************************************************************************************************************************
    /// </summary>
    public class Shape_BtnMasuImpl : Shape_Abstract, Shape_BtnMasu
    {

        #region プロパティー

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 座標
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        ///         升目の位置。
        /// 
        /// </summary>
        public SyElement Zahyo
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 光。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public bool Light
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 動かしたい駒。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public bool Select
        {
            get;
            set;
        }

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

        #endregion

        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクターです。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="suji"></param>
        /// <param name="dan"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Shape_BtnMasuImpl(string widgetName)
            : base(widgetName, 0, 0, 35, 35)
        {
            this.Zahyo = Masu_Honshogi.Query_Basho(Masu_Honshogi.nError);
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクターです。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="suji"></param>
        /// <param name="dan"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Shape_BtnMasuImpl(string widgetName, SyElement masu, int x, int y, int width, int height)
            : base(widgetName, x, y, width, height)
        {
            this.Zahyo = masu;
        }

        public bool Kiki { get; set; }
        public int KikiSu { get; set; }
        /// <summary>
        /// ************************************************************************************************************************
        /// 升ボタンの描画はここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="g1"></param>
        public void Paint(Graphics g1)//, bool kiki, int kikiSu
        {
            if (!this.Visible)
            {
                goto gt_EndMethod;
            }

            //----------
            // 背景
            //----------
            if (this.Select)
            {
                g1.FillRectangle(Brushes.Brown, this.Bounds);
            }
            else if (this.Kiki)
            {
                g1.FillRectangle(Brushes.YellowGreen, this.Bounds);
            }
            else if (0 < this.KikiSu)
            {
                int level = (this.KikiSu - 1) * 40;
                if (120 < level)
                {
                    level = 120;
                }
                //g1.FillRectangle(new SolidBrush(Color.FromArgb(255, 255 - 30 - level, 255 - 10 - level, 255 - 10 - level)), this.Bounds);
                g1.FillRectangle(new SolidBrush(Color.FromArgb(64, 255 - 30 - level, 255 - 10 - level, 255 - 10 - level)), this.Bounds);
            }
            else if (this.Light)
            {
            }

            //----------
            // 升番号
            //----------
            if(false){
                string text = Conv_SyElement.ToMasuNumber(this.Zahyo).ToString();

                float fontHeight = 13.0f;
                Font font = new Font("ＭＳ ゴシック", fontHeight, FontStyle.Regular);
                SizeF sizeF = g1.MeasureString(text, font);

                //int x = this.Bounds.X + this.Bounds.Width / 2;
                int x = this.Bounds.X + this.Bounds.Width / 2 - (int)(sizeF.Width / 2);
                //int y = this.Bounds.Y + this.Bounds.Height / 2 - (int)(fontHeight/2);
                int y = this.Bounds.Y + this.Bounds.Height / 2 - (int)(sizeF.Height / 2);



                g1.DrawString(text, font, Brushes.BurlyWood, x + 1, y + 1);//影
                g1.DrawString(text, font, Brushes.Beige, x, y);//色
            }

            //----------
            // 枠線
            //----------
            {
                if (this.Light)
                {
                    g1.DrawRectangle(Pens.Yellow, this.Bounds);
                }
            }

        gt_EndMethod:
            ;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// マウスが重なった駒は、光フラグを立てます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void LightByMouse(int x, int y)
        {
            if (this.HitByMouse(x,y)) // マウスが重なっているなら
            {
                this.Light = true;
            }
            else // マウスが重なっていないなら
            {
                this.Light = false;
            }
        }




        /// <summary>
        /// ************************************************************************************************************************
        /// 動かしたい駒の解除
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public bool DeselectByMouse(int x, int y)
        {
            bool changed = false;

            if ( this.HitByMouse(x, y)) // マウスが重なっているなら
            {
                if (this.SelectFirstTouch)
                {
                    // クリックのマウスアップ
                    this.SelectFirstTouch = false;
                }
                else
                {
                    // 選択解除のマウスアップ
                    this.Select = false;
                    changed = true;
                }
            }
            else
            {
                // 何もしない
            }

            return changed;
        }
    }


}
