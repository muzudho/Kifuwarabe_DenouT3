using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P321_KyokumHyoka.L___250_Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P531_Hyokakansu_.L___500_Hyokakansu;

namespace Grayscale.P531_Hyokakansu_.L500____Hyokakansu
{

    /// <summary>
    /// 局面の得点計算。
    /// </summary>
    public abstract class HyokakansuAbstract : Hyokakansu
    {

        /// <summary>
        /// 評価関数名
        /// </summary>
        public HyokakansuName Name
        {
            get
            {
                return this.name;
            }
        }
        private HyokakansuName name;

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="name"></param>
        public HyokakansuAbstract( HyokakansuName name)
        {
            this.name = name;
        }

        /// <summary>
        /// 評価値を返します。先手が有利ならプラス、後手が有利ならマイナス、互角は 0.0 です。
        /// </summary>
        /// <param name="keisanArgs"></param>
        /// <returns></returns>
        abstract public void Evaluate(
            out float out_score,
#if DEBUG
            out KyHyokaMeisai_Koumoku out_meisaiKoumoku_orNull,
#endif
#if LEARN
            out KyHyokaMeisai_Koumoku out_meisaiKoumoku_orNull,
#endif
            SkyConst src_Sky,
            FeatureVector featureVector,
            KwErrorHandler errH
            );

    }
}
