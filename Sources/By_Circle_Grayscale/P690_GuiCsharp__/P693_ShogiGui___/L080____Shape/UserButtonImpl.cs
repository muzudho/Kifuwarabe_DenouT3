using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P693_ShogiGui___.L___491_Event;
using Grayscale.P693_ShogiGui___.L___492_Widgets;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using System.Drawing;

namespace Grayscale.P693_ShogiGui___.L080____Shape
{

    /// <summary>
    /// ユーザー定義ボタン
    /// </summary>
    public class UserButtonImpl : UserWidget
    {

        public static readonly UserButtonImpl NULL_OBJECT = new UserButtonImpl(new Shape_BtnBoxImpl("#NullButton"));


        public object Object { get { return this.this_object; } }
        private Shape_BtnBoxImpl this_object { get; set; }
        public void Compile()
        {
        }


        /// <summary>
        /// 表示するウィンドウ。
        /// </summary>
        public string Window { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public Color BackColor {
            get
            {
                return this.this_object.BackColor;
            }
            set
            {
                this.this_object.BackColor = value;
            }
        }

        public UserButtonImpl(Shape_BtnBoxImpl shape_BtnBox)
        {
            this.this_object = shape_BtnBox;
        }


        public DELEGATE_MouseHitEvent Delegate_MouseHitEvent{get;set;}

        public bool IsLight_OnFlowB_1TumamitaiKoma { get; set; }

        public Rectangle Bounds {
            get
            {
                return this.this_object.Bounds;
            }
        }
        public void SetBounds(Rectangle rect)
        {
            this.this_object.SetBounds( rect);
        }

        public string Text
        {
            get
            {
                return this.this_object.Text;
            }
            set
            {
            this.this_object.Text = value;
            }
        }

        public string Fugo
        {
            get
            {
                return this.this_object.Fugo;
            }
            set
            {
                this.this_object.Fugo = value;
            }
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// フォントサイズ。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public float FontSize
        {
            get
            {
                return this.this_object.FontSize;
            }
            set
            {
                this.this_object.FontSize = value;
            }
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
        public bool DeselectByMouse(int x, int y, object obj_shogiGui)
        {
            return this.this_object.DeselectByMouse(x, y, (MainGui_Csharp)obj_shogiGui);
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

        public Okiba Okiba{get;set;}
        public int Suji { get; set; }
        public int Dan { get; set; }
        public int MasuHandle { get; set; }

    }
}
