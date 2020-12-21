    using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.P521FeatureVect.I500Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
using System;
using Grayscale.Kifuwarakaku.Entities.Features;

#if DEBUG || LEARN
using System.Text;
using Grayscale.Kifuwarakaku.Entities.Features;
#endif

namespace Grayscale.P531Hyokakansu.L500Hyokakansu
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
            SkyConst srcSky,
            FeatureVector fv
            )
        {
            float score_p1 = 0.0f;
            float score_p2 = 0.0f;//2Pは、負の数なほどグッドということに注意。



            srcSky.Foreach_Starlights((Finger finger, IMoveHalf light, ref bool toBreak) =>
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
