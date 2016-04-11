using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P693_ShogiGui___.L___125_Scene;
using System.Drawing;


namespace Grayscale.P693_ShogiGui___.L125____Scene
{
    /// <summary>
    /// マウス操作の状態です。
    /// </summary>
    public class MouseEventState
    {

        public SceneName Name1 { get { return this.name1; } }
        private SceneName name1;

        /// <summary>
        /// ウィジェットが配置されているウィンドウ名。"Shogiban", "Console"。
        /// </summary>
        public string WindowName { get { return this.windowName; } }
        private string windowName;


        public MouseEventStateName Name2 { get { return this.name2; } }
        private MouseEventStateName name2;

        public Point MouseLocation { get { return this.mouseLocation; } }
        private Point mouseLocation;


        public KwErrorHandler Flg_logTag { get { return this.flg_logTag; } }
        private KwErrorHandler flg_logTag;

        public MouseEventState()
        {
            this.name1 = SceneName.Ignore;
            this.name2 = MouseEventStateName.Ignore;
            this.mouseLocation = Point.Empty;
            this.flg_logTag = null;
        }

        public MouseEventState(SceneName name1, string windowName, MouseEventStateName name2, Point mouseLocation, KwErrorHandler logTag)
        {
            this.name1 = name1;
            this.windowName = windowName;
            this.name2 = name2;
            this.mouseLocation = mouseLocation;
            this.flg_logTag = logTag;
        }

    }
}
