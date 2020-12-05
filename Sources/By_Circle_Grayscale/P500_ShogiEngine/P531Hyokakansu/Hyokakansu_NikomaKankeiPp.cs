using Grayscale.P003Log.I500Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P256SeizaFinger.L250Struct;
using Grayscale.P321KyokumHyoka.I250Struct;
using Grayscale.P521FeatureVect.I500Struct;
using Grayscale.P531Hyokakansu.L499UtilFv;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
using System;


#if DEBUG || LEARN
using System.Text;
using Grayscale.P321KyokumHyoka.L250Struct;
#endif

namespace Grayscale.P531Hyokakansu.L500Hyokakansu
{


    /// <summary>
    /// 評価計算
    /// 
    /// 二駒関係ＰＰ。
    /// 
    /// 駒割は含めない。
    /// </summary>
    public class Hyokakansu_NikomaKankeiPp : HyokakansuAbstract
    {

        public Hyokakansu_NikomaKankeiPp()
            : base(HyokakansuName.N14_NikomaKankeiPp___)
        {
        }


        /// <summary>
        /// 評価値を返します。先手が有利ならプラス、後手が有利ならマイナス、互角は 0.0 です。
        /// </summary>
        /// <param name="input_node"></param>
        /// <returns></returns>
        public override void Evaluate(
            out float out_score,
#if DEBUG || LEARN
            out KyHyokaMeisai_Koumoku out_meisaiKoumoku_orNull,
#endif
            SkyConst src_Sky,
            FeatureVector fv,
            IKwErrorHandler errH
            )
        {
            out_score = 0.0f;            // -999～999(*bairitu) が 40×40個ほど足し合わせた数になるはず。


#if DEBUG
            float[] komabetuMeisai = new float[Finger_Honshogi.Items_KomaOnly.Length];
#endif
            //
            // 盤上にある駒だけ、項目番号を調べます。
            //

            //----------------------------------------
            // 項目番号リスト
            //----------------------------------------
            //
            // 40個の駒と、14種類の持ち駒があるだけなので、
            // 54個サイズの長さがあれば足りるんだぜ☆　固定長にしておこう☆
            //
            int nextIndex = 0;
            int[] komokuArray_unsorted = new int[54];//昇順でなくても構わないアルゴリズムにすること。

            for (int i=0; i < Finger_Honshogi.Items_KomaOnly.Length; i++)// 全駒
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(Finger_Honshogi.Items_KomaOnly[i]).Now);

                if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu))
                {
                    // 盤上
                    komokuArray_unsorted[nextIndex] = Util_FvParamIndex.ParamIndex_Banjo(koma);
                    nextIndex++;
                }
                else
                {
                    // 持ち駒は、ここでは無視します。
                }
            }
            // 持ち駒：先後×７種類
            for(int iPside=0; iPside<Array_Playerside.Items_PlayerOnly.Length; iPside++)
            {
                for (int iKomasyurui = 0; iKomasyurui < Array_Komasyurui.MotiKoma7Syurui.Length; iKomasyurui++ )
                {
                    komokuArray_unsorted[nextIndex] = Util_FvParamIndex.ParamIndex_Moti(src_Sky, Array_Playerside.Items_PlayerOnly[iPside], Array_Komasyurui.MotiKoma7Syurui[iKomasyurui]);
                    nextIndex++;
                }
            }
            //Array.Sort(komokuArray_unsorted);

            //
            //
            // 例えば、[1P１三歩、2P２一桂]という組み合わせと、[2P２一桂、1P１三歩]という組み合わせは、同じだが欄が２つある。
            // そこで、表の半分を省きたい。
            // しかし、表を三角形にするためには、要素は昇順にソートされている必要がある。
            // 合法手１つごとにソートしていては、本末転倒。
            // そこで、表は正方形に読み、内容は三角形の部分にだけ入っているということにする。
            //
            //
            // 例えば、[1P１三歩、1P１三歩]という組み合わせもある。これは、自分自身の絶対位置の評価として試しに、残しておいてみる☆
            //
            //
            for (int iA = 0; iA < nextIndex; iA++)
            {
                int p1 = komokuArray_unsorted[iA];

                for (int iB = 0; iB < nextIndex; iB++)
                {
                    int p2 = komokuArray_unsorted[iB];

                    if (p1 <= p2) // 「p2 < p1」という組み合わせは同じ意味なので省く。「p1==p2」は省かない。
                    {
                        //----------------------------------------
                        // よし、組み合わせだぜ☆！
                        //----------------------------------------
                        out_score += fv.NikomaKankeiPp_ForMemory[p1, p2];
                    }
                    else
                    {
                        //----------------------------------------
                        // 使っていない方の三角形だぜ☆！
                        //----------------------------------------

                        // スルー。
                    }
                }
            }

            //----------------------------------------
            // 明細項目
            //----------------------------------------
#if DEBUG || LEARN
            string utiwake = "";
            // 内訳
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(" ＰＰ ");
                sb.Append(out_score);
                sb.Append("点");

                utiwake = sb.ToString();
            }
            out_meisaiKoumoku_orNull = new KyHyokaMeisai_KoumokuImpl(utiwake, out_score);
#endif
        }


    }
}
