using System.Drawing;

namespace Grayscale.P693_ShogiGui___.L___080_Shape
{
    public interface Shape
    {
        
        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 位置とサイズ
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        Rectangle Bounds { get; }

        void SetBounds(Rectangle rect);

    }
}
