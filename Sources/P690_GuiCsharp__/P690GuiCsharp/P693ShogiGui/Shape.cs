using System.Drawing;

namespace Grayscale.P693ShogiGui.I080Shape
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
