using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P321_KyokumHyoka.L___250_Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;
using System;


#if DEBUG || LEARN
using System.Text;
using Grayscale.P321_KyokumHyoka.L250____Struct;
#endif

namespace Grayscale.P531_Hyokakansu_.L500____Hyokakansu
{


    /// <summary>
    /// 千日手。
    /// </summary>
    public class Hyokakansu_Sennitite : HyokakansuAbstract
    {

        public Hyokakansu_Sennitite()
            : base(HyokakansuName.N01_Sennitite________)
        {
        }


        /// <summary>
        /// 評価値を返します。先手が有利ならプラス、後手が有利ならマイナス、互角は 0.0 です。
        /// </summary>
        /// <param name="input_node"></param>
        /// <param name="playerInfo"></param>
        /// <returns></returns>
        public override void Evaluate(
            out float out_score,
#if DEBUG || LEARN
            out KyHyokaMeisai_Koumoku out_meisaiKoumoku_orNull,
#endif
 SkyConst src_Sky,
            FeatureVector featureVector,
            KwErrorHandler errH
            )
        {
            out_score = 0.0f;//互角

            switch (src_Sky.KaisiPside)
            {
                case Playerside.P1: out_score = float.MinValue; break;
                case Playerside.P2: out_score = float.MaxValue; break;
                default: throw new Exception("千日手判定をしようとしましたが、先後の分からない局面データがありました。"); break;
            }


        gt_EndMethod:
            ;

            //----------------------------------------
            // 明細項目
            //----------------------------------------
#if DEBUG || LEARN
            string utiwake = "";
            // 明細
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("千日手。 score=[" + out_score + "]");
                utiwake = sb.ToString();
            }
            out_meisaiKoumoku_orNull = new KyHyokaMeisai_KoumokuImpl(utiwake, out_score);
#endif
        }


    }
}
