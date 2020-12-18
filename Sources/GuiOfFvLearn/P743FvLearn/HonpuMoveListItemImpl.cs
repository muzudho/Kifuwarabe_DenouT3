using Grayscale.P163KifuCsa.I250Struct;
using System.Text;


namespace Grayscale.P743FvLearn.L250Learn
{
    /// <summary>
    /// 本譜指し手リストの項目。
    /// </summary>
    public class HonpuMoveListItemImpl
    {
        /// <summary>
        /// CSA棋譜 の指し手
        /// </summary>
        private CsaKifuMove CsaMove { get; set; }

        /// <summary>
        /// SFEN符号
        /// </summary>
        public string Sfen { get; set; }

        public HonpuMoveListItemImpl(CsaKifuMove move, string sfen)
        {
            this.CsaMove = move;
            this.Sfen = sfen;// 
        }

        /// <summary>
        /// リストボックスで表示する文字列です。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.CsaMove.OptionTemezumi);
            sb.Append("手目 ");
            sb.Append(this.CsaMove.DestinationMasu);
            sb.Append(" ");
            sb.Append(this.CsaMove.Second);
            sb.Append(" ");
            sb.Append(this.CsaMove.Sengo);
            sb.Append(" ");
            sb.Append(this.CsaMove.SourceMasu);
            sb.Append(" ");
            sb.Append(this.CsaMove.Syurui);
            sb.Append(" ");
            sb.Append(this.Sfen);

            return sb.ToString();
        }

    }
}
