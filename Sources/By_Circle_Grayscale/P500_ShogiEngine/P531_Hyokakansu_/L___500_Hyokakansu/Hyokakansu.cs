using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P321_KyokumHyoka.L___250_Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;

namespace Grayscale.P531_Hyokakansu_.L___500_Hyokakansu
{

    /// <summary>
    /// 局面評価の判断。
    /// </summary>
    public interface Hyokakansu
    {

        /// <summary>
        /// 評価関数名。
        /// </summary>
        HyokakansuName Name { get; }

        /// <summary>
        /// 評価値を返します。先手が有利ならプラス、後手が有利ならマイナス、互角は 0.0 です。
        /// </summary>
        /// <param name="keisanArgs"></param>
        /// <returns></returns>
        void Evaluate(
            out float score,
#if DEBUG
            out KyHyokaMeisai_Koumoku kyokumenScore,
#endif
#if LEARN
            out KyHyokaMeisai_Koumoku kyokumenScore,
#endif
            SkyConst src_Sky,
            FeatureVector featureVector,
            KwErrorHandler errH
            );

    }
}
