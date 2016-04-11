using Grayscale.P163_KifuCsa____.L___250_Struct;
using System.Text;

namespace Grayscale.P163_KifuCsa____.L250____Struct
{
    public class CsaKifuSasiteImpl : CsaKifuSasite
    {

        /// <summary>
        /// 先手なら「+」、後手なら「-」。
        /// </summary>
        public string Sengo { get; set; }

        /// <summary>
        /// 移動元マス。
        /// </summary>
        public string SourceMasu { get; set; }

        /// <summary>
        /// 移動先マス。
        /// </summary>
        public string DestinationMasu { get; set; }

        /// <summary>
        /// 駒の種類。
        /// </summary>
        public string Syurui { get; set; }

        /// <summary>
        /// 消費秒。
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// 手目済み。初期局面は 0。
        /// </summary>
        public int OptionTemezumi { get; set; }

        public CsaKifuSasiteImpl()
        {
            this.Sengo = "";
            this.SourceMasu = "";
            this.DestinationMasu = "";
            this.Syurui = "";
            this.Second = 0;
            this.OptionTemezumi = 0;
        }

        /// <summary>
        /// デバッグ用に、中身を確認できるよう、データの内容をテキスト形式で出力します。
        /// </summary>
        public string ToStringForDebug()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.OptionTemezumi);
            sb.Append("手目済  ");

            sb.Append("先後");
            sb.Append(this.Sengo);

            sb.Append("　元");
            sb.Append(this.SourceMasu);

            sb.Append("　先");
            sb.Append(this.DestinationMasu);

            sb.Append("　種類");
            sb.Append(this.Syurui);

            sb.Append("　秒");
            sb.Append(this.Second);

            return sb.ToString();
        }

    }
}
