
using Grayscale.P693_ShogiGui___.L___499_Repaint;



namespace Grayscale.P693_ShogiGui___.L080____Shape
{

    /// <summary>
    /// ************************************************************************************************************************
    /// このメインパネルに、何かして欲しいという要求は、ここに入れられます。
    /// ************************************************************************************************************************
    /// </summary>
    public class RepaintRequestImpl : RepaintRequest
    {

        #region プロパティ類
        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 入力欄のテキストを上書きしたいときに設定(*1)します。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        ///         *1…ヌルなら、要求フラグは偽になります。
        /// </summary>
        public string NyuryokuText
        {
            get
            {
                return this.nyuryokuText;
            }
            set
            {
                string str = value;

                if (null == str)
                {
                    this.canInputTextFlag = false;
                }
                else
                {
                    this.canInputTextFlag = true;
                }

                this.nyuryokuText = value;
            }
        }
        private string nyuryokuText;

        /// <summary>
        /// フラグ。読取専用。
        /// </summary>
        public bool IsRequested_RepaintNyuryokuText
        {
            get
            {
                return this.canInputTextFlag;
            }
        }
        private bool canInputTextFlag;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 入力欄の後ろにテキストを付け足したいときに設定(*1)します。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 
        ///         *1…ヌルなら、要求フラグは偽になります。
        /// 
        /// </summary>
        public string NyuryokuTextTail
        {
            get
            {
                return this.nyuryokuTextTail;
            }
        }

        public void SetNyuryokuTextTail(string value)
        {
            if (null == value)
            {
                this.canAppendInputTextFlag = false;
            }
            else
            {
                this.canAppendInputTextFlag = true;
            }
            this.nyuryokuTextTail = value;
        }
        private string nyuryokuTextTail;

        /// <summary>
        /// フラグ。読取専用。
        /// </summary>
        public bool IsRequested_NyuryokuTextTail
        {
            get
            {
                return canAppendInputTextFlag;
            }
        }
        private bool canAppendInputTextFlag;



        /// <summary>
        ///------------------------------------------------------------------------------------------------------------------------
        /// 出力欄を更新したいとき。
        ///------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public RepaintRequestGedanTxt SyuturyokuRequest
        {
            get;
            set;
        }

        /// <summary>
        ///------------------------------------------------------------------------------------------------------------------------
        /// メインパネルを再描画したいときは、真にしてください。
        ///------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public void SetFlag_RefreshRequest()
        {
            this.isRefreshRequested = true;
        }
        public void ClearRefreshRequest()
        {
            this.isRefreshRequested = false;
        }
        public bool IsRefreshRequested()
        {
            return this.isRefreshRequested;
        }
        private bool isRefreshRequested;


        /// <summary>
        ///------------------------------------------------------------------------------------------------------------------------
        /// 駒の座標の再計算の要求
        ///------------------------------------------------------------------------------------------------------------------------
        ///
        /// 要素は駒ハンドル。
        /// 
        /// </summary>
        public void SetFlag_RecalculateRequested()
        {
            this.starlightsRecalculateRequested = true;
        }
        public void Clear_StarlightsRecalculateRequested()
        {
            this.starlightsRecalculateRequested = false;
        }
        public bool Is_StarlightsRecalculateRequested()
        {
            return this.starlightsRecalculateRequested;
        }
        private bool starlightsRecalculateRequested;

        #endregion


        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクタです。
        /// ************************************************************************************************************************
        /// </summary>
        public RepaintRequestImpl()
        {
            this.SyuturyokuRequest = RepaintRequestGedanTxt.None;
        }

    }


}
