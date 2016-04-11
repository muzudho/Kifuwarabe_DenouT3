
namespace Grayscale.P157_KyokumenPng.L___500_Struct
{
    public interface KyokumenPngEnvironment
    {

        /// <summary>
        /// 出力フォルダーへのパス。
        /// </summary>
        string OutFolder { get; }

        /// <summary>
        /// 画像フォルダーへのパス。
        /// </summary>
        string ImgFolder { get; }

        /// <summary>
        /// 駒画像ファイル。
        /// </summary>
        string KmFile { get; }

        /// <summary>
        /// 数字画像ファイル。
        /// </summary>
        string SjFile { get; }

        /// <summary>
        /// 駒の横幅
        /// </summary>
        int KmW { get; }

        /// <summary>
        /// 駒の縦幅
        /// </summary>
        int KmH { get; }

        /// <summary>
        /// 数字の横幅
        /// </summary>
        int SjW { get; }

        /// <summary>
        /// 数字の縦幅
        /// </summary>
        int SjH { get; }

    }
}
