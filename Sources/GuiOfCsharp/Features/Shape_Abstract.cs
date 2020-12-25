namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using System.Drawing;
    using Grayscale.Kifuwarakaku.Entities.Configuration;

    /// <summary>
    /// ************************************************************************************************************************
    /// 描かれる図画です。共通の内容です。
    /// ************************************************************************************************************************
    /// </summary>
    public abstract class Shape_Abstract
    {
        /// <summary>
        /// コンストラクターです。
        /// </summary>
        public Shape_Abstract(IEngineConf engineConf, string widgetName, int x, int y, int width, int height)
        {
            EngineConf = engineConf;
            this.widgetName = widgetName;
            this.Visible = true;
            this.bounds = new Rectangle(x, y, width, height);
        }

        public IEngineConf EngineConf { get; private set; }

        public string WidgetName { get { return this.widgetName; } }
        private string widgetName;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 表示／非表示
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 位置とサイズ
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
        }
        public void SetBounds(Rectangle rect)
        {
            this.bounds = rect;
        }
        private Rectangle bounds;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 背景色
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Color BackColor
        {
            get;
            set;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// マウスカーソルに重なっているか否か。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public bool HitByMouse(int x, int y)
        {
            bool hit = false;

            if (this.Visible && this.Bounds.Contains(x, y))
            {
                hit = true;
            }

            return hit;
        }

    }
}
