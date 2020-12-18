namespace Grayscale.P699_Form_______
{
#if DEBUG
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P693ShogiGui.I100Widgets;
using Grayscale.P693ShogiGui.I500Gui;
using Grayscale.P693ShogiGui.L080Shape;
using Grayscale.P693ShogiGui.L081Canvas;
using Grayscale.P693ShogiGui.L125Scene;
using Grayscale.P693ShogiGui.L250Timed;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
#else
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Grayscale.Kifuwarakaku.Entities.Logger;
    using Grayscale.P693ShogiGui.I100Widgets;
    using Grayscale.P693ShogiGui.I500Gui;
    using Grayscale.P693ShogiGui.L080Shape;
    using Grayscale.P693ShogiGui.L081Canvas;
    using Grayscale.P693ShogiGui.L125Scene;
    using Grayscale.P693ShogiGui.L250Timed;
#endif

    public partial class Uc_Form2Main : UserControl
    {


        public Uc_Form2Main()
        {
            InitializeComponent();
        }


        public void SetInputareaText(string value)
        {
            //System.C onsole.WriteLine("☆セット：" + value);
            this.txtInputarea.Text = value;
        }

        public void AppendInputareaText(string value, [CallerMemberName] string memberName = "")
        {
            //System.C onsole.WriteLine("☆アペンド(" + memberName + ")：" + value);
            this.txtInputarea.Text += value;
        }

        public string GetOutputareaText()
        {
            return this.txtOutputarea.Text;
        }


#region ゲームエンジンの振りをするメソッド

        /// <summary>
        /// ************************************************************************************************************************
        /// 入力欄のテキストを取得します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public string ReadText()
        {
            return this.txtInputarea.Text;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 出力欄にテキストを出力します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public void WriteLine_Syuturyoku(string text)
        {
            this.txtOutputarea.Text = text;
        }

#endregion

        private void txtInputarea_KeyDown(object sender, KeyEventArgs e)
        {
            // [Ctrl]+[A] で全選択します。
            AspectOriented_TextBox.KeyDown_SelectAll(sender, e);
        }

        private void txtOutputarea_KeyDown(object sender, KeyEventArgs e)
        {
            // [Ctrl]+[A] で全選択します。
            AspectOriented_TextBox.KeyDown_SelectAll(sender, e);
        }

        private void Uc_Form2Main_Paint(object sender, PaintEventArgs e)
        {
            MainGui_Csharp shogibanGui = ((Form2_Console)this.ParentForm).Form1_Shogi.Uc_Form1Main.MainGui;

            if (null == shogibanGui.ConsoleWindowGui.Shape_Canvas)
            {
                goto gt_EndMethod;
            }

            //------------------------------
            // 画面の描画です。
            //------------------------------
            shogibanGui.ConsoleWindowGui.Shape_Canvas.Paint(sender, e, shogibanGui, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, LogTags.CsharpGuiPaint);

        gt_EndMethod:
            ;

        }

        private void Uc_Form2Main_MouseDown(object sender, MouseEventArgs e)
        {
            ILogTag logTag = LogTags.CsharpGuiDefault;
            MainGui_Csharp shogibanGui = ((Form2_Console)this.ParentForm).Form1_Shogi.Uc_Form1Main.MainGui;

            if (null == shogibanGui.Shape_PnlTaikyoku)
            {
                goto gt_EndMethod;
            }

            // このメインパネルに、何かして欲しいという要求は、ここに入れられます。
            shogibanGui.RepaintRequest = new RepaintRequestImpl();


            if (e.Button == MouseButtons.Left)
            {
                //------------------------------------------------------------
                // 左ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)shogibanGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(shogibanGui.Scene, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, MouseEventStateName.MouseLeftButtonDown, e.Location, logTag));
            }
            else if (e.Button == MouseButtons.Right)
            {
                //------------------------------------------------------------
                // 右ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)shogibanGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(shogibanGui.Scene, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, MouseEventStateName.MouseRightButtonDown, e.Location, logTag));


                //------------------------------
                // このメインパネルの反応
                //------------------------------
                shogibanGui.Response("MouseOperation", logTag);

            }
            else
            {
                //------------------------------
                // このメインパネルの反応
                //------------------------------
                shogibanGui.Response("MouseOperation", logTag);
            }

        gt_EndMethod:
            ;

        }

        private void Uc_Form2Main_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ILogTag logTag = LogTags.CsharpGuiDefault;
            MainGui_Csharp mainGui = ((Form2_Console)this.ParentForm).Form1_Shogi.Uc_Form1Main.MainGui;

            // このメインパネルに、何かして欲しいという要求は、ここに入れられます。
            mainGui.RepaintRequest = new RepaintRequestImpl();

            //------------------------------
            // マウスボタンが放されたときの、表示物の反応や、将棋データの変更がこの中に書かれています。
            //------------------------------
            if (e.Button == MouseButtons.Left)
            {
                //------------------------------------------------------------
                // 左ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)mainGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(mainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, MouseEventStateName.MouseLeftButtonUp, e.Location, logTag));
            }
            else if (e.Button == MouseButtons.Right)
            {
                //------------------------------------------------------------
                // 右ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)mainGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(mainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, MouseEventStateName.MouseRightButtonUp, e.Location, logTag));
            }
        }

    }
}
