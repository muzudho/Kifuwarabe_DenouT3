using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P321_KyokumHyoka.L___250_Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
using System;
using Grayscale.P212_ConvPside__.L500____Converter;

#if DEBUG || LEARN
using System.Text;
using Grayscale.P321_KyokumHyoka.L250____Struct;
#endif

namespace Grayscale.P531_Hyokakansu_.L500____Hyokakansu
{


    public class Hyokakansu_Komawari : HyokakansuAbstract
    {

        public Hyokakansu_Komawari()
            : base(HyokakansuName.N05_Komawari_________)
        {
        }

        /// <summary>
        /// 評価値を返します。先手が有利ならプラス、後手が有利ならマイナス、互角は 0.0 です。
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override void Evaluate(
            out float out_score,
#if DEBUG || LEARN
            out KyHyokaMeisai_Koumoku out_meisaiKoumoku_orNull,
#endif
            SkyConst src_Sky,
            FeatureVector fv,
            KwErrorHandler errH
            )
        {
            float score_p1 = 0.0f;
            float score_p2 = 0.0f;//2Pは、負の数なほどグッドということに注意。



            src_Sky.Foreach_Starlights((Finger finger, Starlight light, ref bool toBreak) =>
            {
                RO_Starlight ms = (RO_Starlight)light;

                RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

                // 駒の種類による点数
                float komaScore_temp = fv.Komawari[(int)koma.Komasyurui];

                // 持ち駒は、価値を高めます。（ボーナス）序盤に駒をぽんぽん打つのを防ぐため。
                if(
                    (Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag( Conv_SyElement.ToOkiba(koma.Masu))
                    )
                {
                    //komaScore_temp *= 1.05f;// 1.05倍だと、相手の桂馬の利きに、桂馬をタダ捨てした。足りてないか。
                    komaScore_temp *= 1.13f;
                    //komaScore_temp *= 1.25f;// 1.25倍だと、金、金、角を打たずに王手されて終わってしまった。ボーナスを付けすぎたか☆
                }


                if (koma.Pside == Playerside.P1)
                {
                    score_p1 += komaScore_temp;
                }
                else
                {
                    // 駒割は、他の評価値と違って、
                    // １プレイヤーも、２プレイヤーも正の数になっている。
                    // ２プレイヤーは　符号を反転させること。
                    score_p2 += -komaScore_temp;
                }
            });

            //
            // ２プレイヤーは　負の数になっている（負の数が多いほど有利）ので、
            // 足すだけでいい。
            //
            out_score = score_p1 + score_p2;

            //----------------------------------------
            // 明細項目
            //----------------------------------------
#if DEBUG || LEARN
            string utiwake = "";
            // 明細
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("駒割");
                utiwake = sb.ToString();
            }

            // 明細項目
            out_meisaiKoumoku_orNull = new KyHyokaMeisai_KoumokuImpl(utiwake, out_score);
#endif

        }

    }


}
