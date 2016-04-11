using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P693_ShogiGui___.L___081_Canvas;
using Grayscale.P693_ShogiGui___.L___492_Widgets;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L080____Shape;
using System.Windows.Forms;

namespace Grayscale.P693_ShogiGui___.L081____Canvas
{

    /// <summary>
    /// ウィジェットを描画する土台。
    /// </summary>
    public class Shape_CanvasImpl : Shape_Abstract, Shape_Canvas
    {

        public const string WINDOW_NAME_SHOGIBAN = "Shogiban";
        public const string WINDOW_NAME_CONSOLE = "Console";


        public Shape_CanvasImpl(string widgetName, int x, int y, int width, int height)
            : base(widgetName, x, y, width, height)
        {
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 対局の描画の一式は、ここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Paint(
            object sender,
            PaintEventArgs e,
            MainGui_Csharp shogibanGui,
            string windowName,
            KwErrorHandler errH
            )
        {
            //----------------------------------------
            // 登録ウィジェットの描画
            //----------------------------------------
            foreach (UserWidget widget in shogibanGui.Widgets.Values)
            {
                if(widget.Window==windowName)
                {
                    widget.Paint(e.Graphics);
                }
            }
        }

    }
}
