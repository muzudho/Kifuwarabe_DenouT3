using Grayscale.P163_KifuCsa____.L___250_Struct;
using System.Text;


namespace Grayscale.P743_FvLearn____.L250____Learn
{
    /// <summary>
    /// 本譜指し手リストの項目。
    /// </summary>
    public class HonpuSasiteListItemImpl
    {
        /// <summary>
        /// CSA棋譜 の指し手
        /// </summary>
        private CsaKifuSasite CsaSasite { get; set; }

        /// <summary>
        /// SFEN符号
        /// </summary>
        public string Sfen { get; set; }

        public HonpuSasiteListItemImpl(CsaKifuSasite sasite, string sfen)
        {
            this.CsaSasite = sasite;
            this.Sfen = sfen;// 
        }

        /// <summary>
        /// リストボックスで表示する文字列です。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.CsaSasite.OptionTemezumi);
            sb.Append("手目 ");
            sb.Append(this.CsaSasite.DestinationMasu);
            sb.Append(" ");
            sb.Append(this.CsaSasite.Second);
            sb.Append(" ");
            sb.Append(this.CsaSasite.Sengo);
            sb.Append(" ");
            sb.Append(this.CsaSasite.SourceMasu);
            sb.Append(" ");
            sb.Append(this.CsaSasite.Syurui);
            sb.Append(" ");
            sb.Append(this.Sfen);

            return sb.ToString();
        }

    }
}
