
using System.Windows.Forms;

namespace Grayscale.P693_ShogiGui___.L___100_Widgets
{
    public abstract class AspectOriented_TextBox
    {

        /// <summary>
        /// 全選択　[Ctrl]+[A]
        /// </summary>
        public static void KeyDown_SelectAll(object sender, KeyEventArgs e)
        {
            //------------------------------
            // [Ctrl]+[A] で、全選択します。
            //------------------------------
            if (e.KeyCode == System.Windows.Forms.Keys.A & e.Control == true)
            {
                ((TextBox)sender).SelectAll();
            }
        }

    }
}
