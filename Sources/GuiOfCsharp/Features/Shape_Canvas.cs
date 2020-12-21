namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using System.Windows.Forms;

    public interface Shape_Canvas
    {

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="shogiGui"></param>
        /// <param name="logTag"></param>
        void Paint(
            object sender,
            PaintEventArgs e,
            MainGui_Csharp shogiGui,
            string windowName
        );

    }
}
