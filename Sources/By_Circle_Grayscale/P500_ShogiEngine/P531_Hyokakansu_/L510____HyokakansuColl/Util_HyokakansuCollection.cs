using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P531_Hyokakansu_.L___500_Hyokakansu;
using Grayscale.P531_Hyokakansu_.L500____Hyokakansu;
using System.Collections.Generic;

#if DEBUG || LEARN
using Grayscale.P321_KyokumHyoka.L___250_Struct;
#endif

namespace Grayscale.P531_Hyokakansu_.L510____HyokakansuColl
{
    public abstract class Util_HyokakansuCollection
    {

        /// <summary>
        /// 「千日手」評価関数１個。
        /// </summary>
        public static Hyokakansu Hyokakansu_Sennichite { get; set; }

        /// <summary>
        /// 「駒割」「二駒関係ＰＰ」の評価関数が入ったリスト。
        /// </summary>
        public static List<Hyokakansu> Hyokakansu_Normal { get; set; }

        static Util_HyokakansuCollection()
        {
            Util_HyokakansuCollection.Hyokakansu_Sennichite = new Hyokakansu_Sennitite();

            Util_HyokakansuCollection.Hyokakansu_Normal = new List<Hyokakansu>()
            {
                new Hyokakansu_Komawari(),
                new Hyokakansu_NikomaKankeiPp(),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node_mutable">この評価シートに明細項目を追加します。</param>
        /// <param name="fv"></param>
        /// <param name="errH"></param>
        public static void EvaluateAll_Normal(
            KifuNode node_mutable,
            FeatureVector fv,
            KwErrorHandler errH
            )
        {
            // 妄想と、指定のノードを比較し、点数付けします。
            foreach (Hyokakansu hyokakansu in Util_HyokakansuCollection.Hyokakansu_Normal)
            {
                float score;
#if DEBUG || LEARN
                KyHyokaMeisai_Koumoku meisai;
#endif
                hyokakansu.Evaluate(
                    out score,
#if DEBUG || LEARN
                    out meisai,
#endif
                    node_mutable.Value.KyokumenConst,
                    fv,
                    errH
                );

                node_mutable.AddScore(score);
#if DEBUG || LEARN
                node_mutable.KyHyokaSheet_Mutable.Add(
                    hyokakansu.Name.ToString(),
                    meisai
                );
#endif
            }
        }
    }
}
