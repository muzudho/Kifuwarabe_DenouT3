using Grayscale.P693_ShogiGui___.L___081_Canvas;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L081____Canvas;

namespace Grayscale.P693_ShogiGui___.L500____GUI
{
    /// <summary>
    /// コンソール・ウィンドウに対応。
    /// </summary>
    public class SubGuiImpl : SubGui
    {
        private MainGui_Csharp Owner { get { return this.owner; } }//ShogibanGui_CSharpImpl
        private MainGui_Csharp owner;//ShogibanGui_CSharpImpl


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// グラフィックを描くツールは全部この中です。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Shape_Canvas Shape_Canvas
        {
            get
            {
                return this.shape_Canvas;
            }
        }
        private Shape_Canvas shape_Canvas;


        /// <summary>
        /// 入力欄の文字列を、退避したもの。
        /// </summary>
        public string InputString99
        {
            get
            {
                return this.Owner.Link_Server.InputString99;
            }
        }
        public void AddInputString99(string line)
        {
            this.Owner.Link_Server.AddInputString99(line);
        }
        public void SetInputString99(string line)
        {
            this.Owner.Link_Server.SetInputString99(line);
        }
        public void ClearInputString99()
        {
            this.Owner.Link_Server.ClearInputString99();
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public SubGuiImpl(MainGui_CsharpImpl owner)
        {
            this.owner = owner;

            //----------
            // ビュー
            //----------
            //
            //      ボタンや将棋盤などを描画するツールを、事前準備しておきます。
            //
            this.shape_Canvas = new Shape_CanvasImpl( "#Canvas", 0,0,0,0);
        }

    }
}
