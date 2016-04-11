using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P693_ShogiGui___.L___491_Event;
using System.Drawing;

namespace Grayscale.P693_ShogiGui___.L___492_Widgets
{


    public interface UserWidget
    {
        object Object { get; }

        /// <summary>
        /// 表示するウィンドウ。
        /// </summary>
        string Window { get; set; }
        string Type { get; set; }
        string Name { get; set; }
        void Compile();

        Color BackColor { get; set; }

        DELEGATE_MouseHitEvent Delegate_MouseHitEvent { get; set; }

        bool IsLight_OnFlowB_1TumamitaiKoma { get; set; }

        Rectangle Bounds { get; }
        void SetBounds(Rectangle rect);

        string Text { get; set; }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// フォントサイズ。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        float FontSize { get; set; }

        string Fugo { get; set; }

        /// <summary>
        /// マス用
        /// </summary>
        Okiba Okiba { get; set; }

        /// <summary>
        /// マス用
        /// </summary>
        int Suji { get; set; }

        /// <summary>
        /// マス用
        /// </summary>
        int Dan { get; set; }

        /// <summary>
        /// マス用
        /// </summary>
        int MasuHandle { get; set; }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="g1"></param>
        void Paint(Graphics g1);

        /// <summary>
        /// ************************************************************************************************************************
        /// マウスカーソルに重なっているか否か。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        bool HitByMouse(int x, int y);

        
        /// <summary>
        /// ************************************************************************************************************************
        /// マウスが重なった駒は、光フラグを立てます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void LightByMouse(int x, int y);

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 光
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        bool Light { get; set; }

        
        /// <summary>
        /// ************************************************************************************************************************
        /// 動かしたい駒の解除
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        bool DeselectByMouse(int x, int y, object obj_shogiGui);


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 表示／非表示
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        bool Visible { get; set; }

    }
}
