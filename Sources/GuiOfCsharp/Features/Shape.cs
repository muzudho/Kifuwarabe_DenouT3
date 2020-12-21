namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using System.Drawing;

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
