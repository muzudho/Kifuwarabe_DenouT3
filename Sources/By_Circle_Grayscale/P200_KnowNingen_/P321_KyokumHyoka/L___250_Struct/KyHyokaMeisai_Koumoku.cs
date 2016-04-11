
namespace Grayscale.P321_KyokumHyoka.L___250_Struct
{

    /// <summary>
    /// デバッグモードでのみ使用。
    /// 指定局面限定での評価明細の一項目。
    /// 
    /// 内訳は省略可能。
    /// </summary>
    public interface KyHyokaMeisai_Koumoku
    {
        /// <summary>
        /// 内訳
        /// </summary>
        string Utiwake { get; }

        /// <summary>
        /// 例えばスコアなど。
        /// </summary>
        float UtiwakeValue { get; }
    }
}
