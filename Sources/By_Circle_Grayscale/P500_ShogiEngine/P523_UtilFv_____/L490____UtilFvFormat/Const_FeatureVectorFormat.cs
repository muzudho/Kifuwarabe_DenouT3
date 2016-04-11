
namespace Grayscale.P523_UtilFv_____.L490____UtilFvFormat
{
    /// <summary>
    /// デバッグ・オプション。
    /// 
    /// フィーチャー・ベクター・ファイルに出力する内容を、デバッグ用のものに切り替えるフラグを持ちます。
    /// </summary>
    public abstract class Const_FeatureVectorFormat
    {
        /// <summary>
        /// 評価値ではなく、パラメーター・インデックスを出力したい場合は真。デバッグ用。
        /// </summary>
        public const bool PARAMETER_INDEX_OUTPUT = false;


        /// <summary>
        /// 評価値を読み込むのではなく、パラメーター・インデックスを読み込みたい場合は真。デバッグ用。
        /// </summary>
        public const bool PARAMETER_INDEX_INPUT = false;
    }
}
