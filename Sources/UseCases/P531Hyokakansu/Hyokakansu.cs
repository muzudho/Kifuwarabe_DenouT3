using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P321KyokumHyoka.I250Struct;
using Grayscale.P521FeatureVect.I500Struct;

namespace Grayscale.P531Hyokakansu.I500Hyokakansu
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
            FeatureVector featureVector
            );

    }
}
