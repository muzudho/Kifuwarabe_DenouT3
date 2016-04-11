using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P693_ShogiGui___.L___491_Event;
using Grayscale.P693_ShogiGui___.L___492_Widgets;
using Grayscale.P693_ShogiGui___.L080____Shape;
using System.Drawing;

namespace Grayscale.P693_ShogiGui___.L492____Widgets
{

    /// <summary>
    /// ユーザー定義マス
    /// </summary>
    public class UserWidget_MasuImpl : UserWidget
    {
        #region 定数

        public static readonly UserWidget_MasuImpl NULL_OBJECT = new UserWidget_MasuImpl(new Shape_BtnMasuImpl("#NullMasu"));

        #endregion

        #region プロパティー

        /// <summary>
        /// 升ボタン。
        /// </summary>
        public object Object { get { return this.this_object; } }
        private Shape_BtnMasuImpl this_object { get; set; }

        public Color BackColor
        {
            get
            {
                return this.this_object.BackColor;
            }
            set
            {
                this.this_object.BackColor = value;
            }
        }

        /// <summary>
        /// 表示するウィンドウ。
        /// </summary>
        public string Window { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }


        public DELEGATE_MouseHitEvent Delegate_MouseHitEvent { get; set; }

        public bool IsLight_OnFlowB_1TumamitaiKoma { get; set; }

        public Rectangle Bounds
        {
            get
            {
                return this.this_object.Bounds;
            }
        }
        public void SetBounds(Rectangle rect)
        {
            this.this_object.SetBounds(rect);
        }

        public bool Visible
        {
            get
            {
                return this.this_object.Visible;
            }
            set
            {
                this.this_object.Visible = value;
            }
        }

        public string Text { get; set; }

        public string Fugo { get; set; }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// フォントサイズ。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public float FontSize { get; set; }

        public Okiba Okiba { get; set; }

        public int Suji { get; set; }

        public int Dan { get; set; }

        public int MasuHandle { get; set; }

        #endregion

        #region コンストラクター

        public UserWidget_MasuImpl(Shape_BtnMasuImpl shape_BtnMasu)
        {
            this.this_object = shape_BtnMasu;
        }

        #endregion


        public void Compile()
        {
            //
            // 初回は、ダミーオブジェクトにプロパティが設定されています。
            // その設定を使って、再作成します。
            //
            this.this_object = new Shape_BtnMasuImpl(
                this.Name,
                Util_Masu10.OkibaSujiDanToMasu(
                            this.Okiba,
                            this.Suji,
                            this.Dan
                ),
                this.Bounds.X,
                this.Bounds.Y,
                this.Bounds.Width,
                this.Bounds.Height
                );
        }

        public void Paint(Graphics g1)
        {
            this.this_object.Paint(g1);
        }

        public bool HitByMouse(int x, int y)
        {
            return this.this_object.HitByMouse(x, y);
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
            this.this_object.LightByMouse(x, y);
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 光
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public bool Light
        {
            get
            {
                return this.this_object.Light;
            }
            set
            {
                this.this_object.Light = value;
            }
        }

        
        /// <summary>
        /// ************************************************************************************************************************
        /// 動かしたい駒の解除
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public bool DeselectByMouse(int x, int y, object obj_shogiGui )
        {
            return false;// this.this_object.DeselectByMouse(x, y, shape_PnlTaikyoku);
        }
    }
}
