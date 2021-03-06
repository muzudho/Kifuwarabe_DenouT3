﻿using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.UseCases.Features
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
