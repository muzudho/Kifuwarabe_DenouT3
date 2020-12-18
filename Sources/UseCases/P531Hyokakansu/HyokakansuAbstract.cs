using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P321KyokumHyoka.I250Struct;
using Grayscale.P521FeatureVect.I500Struct;
using Grayscale.P531Hyokakansu.I500Hyokakansu;

namespace Grayscale.P531Hyokakansu.L500Hyokakansu
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
            IErrorController errH
            );

    }
}
