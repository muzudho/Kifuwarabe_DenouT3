using Grayscale.P693_ShogiGui___.L___500_Gui;
using System.Drawing;

namespace Grayscale.P693_ShogiGui___.L080____Shape
{

    /// <summary>
    /// ************************************************************************************************************************
    /// 描かれる図画です。四角いボタンを描きます。
    /// ************************************************************************************************************************
    /// </summary>
    public class Shape_BtnBoxImpl : Shape_Abstract
    {



        #region プロパティー

        /// <summary>
        /// 符号
        /// </summary>
        public string Fugo { get; set; }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 表示テキスト
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 光
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public bool Light
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 動かしたい駒
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public bool Select
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// フォントサイズ。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public float FontSize
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
        /// <param name="label"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Shape_BtnBoxImpl(string widgetName)
            : base(widgetName, 0, 0, 70, 35)
        {
            this.Text = "";
            this.FontSize = 20.0f;
            this.Fugo = "";
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 描画
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="g1"></param>
        public void Paint(Graphics g1)
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
                g1.FillRectangle(Brushes.Green, this.Bounds);
            }
            else if (this.Light)
            {
                if (null != this.BackColor)
                {
                    g1.FillRectangle(new SolidBrush(this.BackColor), this.Bounds);
                }
            }
            else
            {
                if (null != this.BackColor)
                {
                    g1.FillRectangle(new SolidBrush(this.BackColor), this.Bounds);
                }
            }


            //----------
            // 枠線
            //----------
            {
                Pen pen;
                if (this.Light)
                {
                    pen = Pens.Yellow;
                }
                else
                {
                    pen = Pens.Black;
                }

                g1.DrawRectangle(pen, this.Bounds);
            }

            //----------
            // 文字
            //----------
            g1.DrawString(this.Text, new Font(FontFamily.GenericSerif, this.FontSize), Brushes.Black, this.Bounds.Location);

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
        public bool DeselectByMouse(int x, int y, MainGui_Csharp shogiGui)
        {
            bool changed = false;

            if ( this.HitByMouse(x, y)) // マウスが重なっているなら
            {
                if (shogiGui.Shape_PnlTaikyoku.SelectFirstTouch)
                {
                    // クリックのマウスアップ
                    shogiGui.Shape_PnlTaikyoku.SelectFirstTouch = false;
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
