
namespace Grayscale.P062_ConvText___.L500____Converter
{
    public abstract class Conv_Filepath
    {

        /// <summary>
        /// 文字列エスケープ。
        /// SFEN文字列を、ディレクトリ名、ファイル名にしようとしたところ、エラーになったことから。
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ToEscape(string src)
        {
            src = src.Replace('*', '＊');//SFENの打記号の「*」は、ファイルの文字名に使えないので。

            return src;
        }

    }
}
