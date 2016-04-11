using Grayscale.P321_KyokumHyoka.L___250_Struct;
using System;
using System.Text;

namespace Grayscale.P743_FvLearn____.L250____Learn
{
    /// <summary>
    /// 合法手リストのアイテム。
    /// </summary>
    public class GohosyuListItem
    {

        /// <summary>
        /// 合法手の連番。
        /// </summary>
        public int Count { get { return this.count; } }
        private int count;

        /// <summary>
        /// SFEN符号
        /// </summary>
        public string Sfen { get { return this.sfen; } }
        private string sfen;

        /// <summary>
        /// JSAの指し手符号
        /// </summary>
        public string JsaSasiteStr { get { return this.jsaSasiteStr; } }
        private string jsaSasiteStr;

#if DEBUG || LEARN
        /// <summary>
        /// 評価明細の項目 Komawari
        /// </summary>
        public KyHyokaMeisai_Koumoku KomawariMeisai { get { return this.komawariMeisai; } }
        private KyHyokaMeisai_Koumoku komawariMeisai;

        /// <summary>
        /// 評価明細の項目 PP
        /// </summary>
        public KyHyokaMeisai_Koumoku PpMeisai { get { return this.ppMeisai; } }
        private KyHyokaMeisai_Koumoku ppMeisai;
#endif

        public GohosyuListItem(int count, string sfen, string jsaSasiteStr
#if DEBUG || LEARN
,
            KyHyokaMeisai_Koumoku komawariMeisai,
            KyHyokaMeisai_Koumoku ppMeisai
#endif
)
        {
            this.count = count;
            this.sfen = sfen;
            this.jsaSasiteStr = jsaSasiteStr;
#if DEBUG || LEARN
            this.komawariMeisai = komawariMeisai;
            this.ppMeisai = ppMeisai;
#endif
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(String.Format("{0,3}", this.Count));//合法手は 0～593手。
            sb.Append(")");

            // 日本将棋連盟式の指し手の符号表示は全角。「▲５三銀右不成」の７文字幅を最大と想定。
            sb.Append(this.JsaSasiteStr);
            switch (this.JsaSasiteStr.Length)
            {
                case 0: sb.Append("　　　　　　　"); break;
                case 1: sb.Append("　　　　　　"); break;
                case 2: sb.Append("　　　　　"); break;
                case 3: sb.Append("　　　　"); break;
                case 4: sb.Append("　　　"); break;
                case 5: sb.Append("　　"); break;
                case 6: sb.Append("　"); break;
                default: break;
            }
            sb.Append("　");
            sb.Append(String.Format("{0,-5}", this.Sfen));

            //----------------------------------------
            // 二駒関係の評価値
            //----------------------------------------
            sb.Append("　計=");
            // 小数点を4桁とし、小数点を含んだ全体の桁数を 12桁とする。
#if DEBUG || LEARN
            sb.Append(
                String.Format("{0,12:0.0000}",
                this.komawariMeisai.UtiwakeValue+
                this.PpMeisai.UtiwakeValue
                ));
#else
            sb.AppendLine("DebugかLEARNモードで実行してください。");
#endif

            sb.Append("　　");
            // 小数点を0桁とし、小数点を含んだ全体の桁数を 7桁とする。
            sb.Append("　駒割=");
#if DEBUG || LEARN
            sb.Append(String.Format("{0,7}", this.komawariMeisai.UtiwakeValue));
#else
            sb.Append("DebugかLEARNモードで実行してください。");
#endif
            // 小数点を4桁とし、小数点を含んだ全体の桁数を 12桁とする。
            sb.Append("　Pp=");
#if DEBUG || LEARN
            sb.Append(String.Format("{0,12:0.0000}", this.PpMeisai.UtiwakeValue));
#else
            sb.Append("DebugかLEARNモードで実行してください。");
#endif

#if DEBUG
            //----------------------------------------
            // 内訳（デバッグモードのみ）
            //----------------------------------------
            sb.Append("　　　　　PP内訳=");
            sb.Append(this.PpMeisai.Utiwake);
#endif

            return sb.ToString();
        }

    }
}
