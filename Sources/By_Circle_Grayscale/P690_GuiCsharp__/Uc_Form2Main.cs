using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P693_ShogiGui___.L___100_Widgets;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L080____Shape;
using Grayscale.P693_ShogiGui___.L081____Canvas;
using Grayscale.P693_ShogiGui___.L125____Scene;
using Grayscale.P693_ShogiGui___.L250____Timed;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Grayscale.P699_Form_______
{
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
            shogibanGui.ConsoleWindowGui.Shape_Canvas.Paint(sender, e, shogibanGui, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, Util_OwataMinister.CsharpGui_PAINT);

        gt_EndMethod:
            ;

        }

        private void Uc_Form2Main_MouseDown(object sender, MouseEventArgs e)
        {
            KwErrorHandler errH = Util_OwataMinister.CsharpGui_DEFAULT;
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
                    new MouseEventState(shogibanGui.Scene, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, MouseEventStateName.MouseLeftButtonDown, e.Location, errH));
            }
            else if (e.Button == MouseButtons.Right)
            {
                //------------------------------------------------------------
                // 右ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)shogibanGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(shogibanGui.Scene, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, MouseEventStateName.MouseRightButtonDown, e.Location, errH));


                //------------------------------
                // このメインパネルの反応
                //------------------------------
                shogibanGui.Response("MouseOperation", errH);

            }
            else
            {
                //------------------------------
                // このメインパネルの反応
                //------------------------------
                shogibanGui.Response("MouseOperation", errH);
            }

        gt_EndMethod:
            ;

        }

        private void Uc_Form2Main_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            KwErrorHandler errH = Util_OwataMinister.CsharpGui_DEFAULT;
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
                    new MouseEventState(mainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, MouseEventStateName.MouseLeftButtonUp, e.Location, errH));
            }
            else if (e.Button == MouseButtons.Right)
            {
                //------------------------------------------------------------
                // 右ボタン
                //------------------------------------------------------------
                TimedB_MouseCapture timeB = ((TimedB_MouseCapture)mainGui.TimedB_MouseCapture);
                timeB.MouseEventQueue.Enqueue(
                    new MouseEventState(mainGui.Scene, Shape_CanvasImpl.WINDOW_NAME_CONSOLE, MouseEventStateName.MouseRightButtonUp, e.Location, errH));
            }
        }

    }
}
