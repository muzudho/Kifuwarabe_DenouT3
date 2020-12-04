using Grayscale.P003Log.I500Struct;
using Grayscale.P693ShogiGui.L081Canvas;
using Grayscale.P693ShogiGui.I492Widgets;
using Grayscale.P693ShogiGui.I500Gui;
using Grayscale.P693ShogiGui.L080Shape;
using System.Windows.Forms;

namespace Grayscale.P693ShogiGui.L081Canvas
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
