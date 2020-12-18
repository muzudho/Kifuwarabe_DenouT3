
namespace Grayscale.P163KifuCsa.I250Struct
{
    public interface CsaKifuMove
    {
        /// <summary>
        /// 先手なら「+」、後手なら「-」。
        /// </summary>
        string Sengo { get; set; }

        /// <summary>
        /// 移動元マス。"11"や、"99"など。
        /// </summary>
        string SourceMasu { get; set; }

        /// <summary>
        /// 移動先マス。
        /// </summary>
        string DestinationMasu { get; set; }

        /// <summary>
        /// 駒の種類。
        /// </summary>
        string Syurui { get; set; }

        /// <summary>
        /// 消費秒。
        /// </summary>
        int Second { get; set; }

        /// <summary>
        /// 手目済み。初期局面は 0。
        /// </summary>
        int OptionTemezumi { get; set; }

        /// <summary>
        /// デバッグ用に、中身を確認できるよう、データの内容をテキスト形式で出力します。
        /// </summary>
        string ToStringForDebug();

    }
}
