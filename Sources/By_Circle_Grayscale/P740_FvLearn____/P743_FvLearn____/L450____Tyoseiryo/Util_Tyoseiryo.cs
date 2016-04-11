using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grayscale.P743_FvLearn____.L___450_Tyoseiryo;
using Grayscale.P743_FvLearn____.L___400_54List;

namespace Grayscale.P743_FvLearn____.L450____Tyoseiryo
{
    public abstract class Util_Tyoseiryo
    {
        /// <summary>
        /// 調整量の初期化
        /// </summary>
        public static void Init( TyoseiryoSettings tyoseiryoSettings_mutable, int renzoku_kaisu)
        {
            // 上昇用
            {
                int asobi = 10;

                //
                // 初期値は 1.0 だが、2回目以降の初期値は 1.0 とは限らない。
                // 100.0 かもしれない。
                //
                // 倍率を上げる調整をすると、2回目以降は素早く上がってしまう。
                // 倍率は 固定でいいのではないか☆？
                //

                //float bairitu = 1.0001f; // 超細かく。
                float bairitu = 1.001f; // 超細かく。
                //float bairitu = 1.0075f; // ほとんど上がらないぐらいで　どうか？ → 0.01 ずつ増えていく感じ。足りなさすぎ。
                //float bairitu = 1.05f;
                //float bairitu = 1.075f;
                //float bairitu = 1.1f;//元値が大きいとき、上がるの速い
                for (int pushedCount = asobi + 1; pushedCount <= renzoku_kaisu; pushedCount++)
                {
                    tyoseiryoSettings_mutable.BairituUpDic_AtStep.Add(pushedCount, bairitu);//遊び+1回かかった時点で、1割り増し。

                    //bairitu *= 1.0001f; // 超細かく。
                    bairitu *= 1.001f; // 超細かく。

                    //bairitu *= 1.0075f; // 0.0075割り増しだと、ゆっくり過ぎる。１００回ぐらい繰り返さないと、１位に上がらない。
                    //1.1 * (1.0075 ^ (25-10)) =  1.16967622254
                    
                    //bairitu *= 1.01f; // 0.01割り増しで上がっていく。ちょうど良かった。
                    //1.1 * (1.01 ^ (25-10)) = 1.27706585091


                    //bairitu *= 1.05f; // けっこう早め。３０位ぐらいの手を１位に上げようとすると、評価値が5000点ぐらいにインフレする。
                    //1.1 * (1.05 ^ (25-10)) = 2.28682099735

                    // bairitu *= 1.1f; // 1.1倍では、25回調整しているだけでも、すぐ調整量がMaxになってしまうぜ☆！
                    //1.1 * (1.1 ^ (25-10)) = 4.59497298636
                }
            }

            // 下降用
            {
                float bunbo = 100.0f;// 倍率の最小値は　0.1 より小さくしたい。
                //float bunbo = 15.0f;// 中間で。
                //float bunbo = 18.0f;// 中間で。
                //float bunbo = 20.0f;// 下がりすぎ。 倍率の最大値は　0.5 より小さくしたい。
                //float bunbo = 100.0f;// 下げ幅が強すぎる。
                //float bunbo = 1000.0f;// 思いっきり下げる。
                for (int pushedCount = 0; pushedCount <= renzoku_kaisu; pushedCount++)
                {
                    tyoseiryoSettings_mutable.BairituCooldownDic_AtStep.Add(pushedCount, 1.0f / bunbo);

                    // 倍率が 1.0f になると、減らなくなる。
                    if (bunbo < 0.6f)// 倍率の最大値が 0.6 に留まるようにします。
                    {
                        bunbo = 0.6f;
                    }

                    // 分母の数字を　どんどん小さくしていきます。
                    bunbo *= 0.96f; //(1.0 / (100.0 * 0.96 ^ 1) )=0.01041666666　／　(1.0 / (100.0 * 0.96 ^ 100) )=0.59275700639

                    //bunbo *= 0.92f;//(1.0 / (10.0 * 0.92 ^ 25) ) = 0.80408936175
                    //bunbo *= 0.83f;//(1.0 / (100.0 * 0.83 ^ 25) ) = 1.05450268672
                    //bunbo *= 0.912f;//(1.0 / (10.0 * 0.912 ^ 25) ) = 1.00029717385
                    //bunbo *= 0.912f;//(1.0 / (20.0 * 0.912 ^ 1) )=0.0548245614　最小／最大　(1.0 / (20.0 * 0.912 ^ 25) ) = 0.50014858692
                }
            }
        }


        /// <summary>
        /// 調整量を、５４要素リストの要素に均等に分配できる数にします。
        /// </summary>
        /// <param name="value">調整量</param>
        /// <param name="n40t14List">パラメーターの数を調べるのに利用。</param>
        /// <returns></returns>
        public static float Average_54x54Parameters(float value, N54List n54List)
        {
            float kizami;//刻んだ数。

            // 表を三角形に使うので、正方形から１辺分（対角ライン）を引き、２で割る。
            float nikomaKankeiPatternSu = (n54List.P54Next * (n54List.P54Next-1)) / 2; //二駒関係のＰ×Ｐのパターン数。

            kizami = value * 1.0f / nikomaKankeiPatternSu;

            return kizami;
        }

        ///// <summary>
        ///// 調整量を、５４要素リストの要素に均等に分配できる数にします。
        ///// </summary>
        ///// <param name="value">調整量</param>
        ///// <param name="n40t14List">パラメーターの数を調べるのに利用。</param>
        ///// <returns></returns>
        //public static float Average_54x54Parameters(float value, N40t14List n40t14List)
        //{
        //    float kizami;//刻んだ数。

        //    int parameterSu = n40t14List.P40Next + n40t14List.P14Next;

        //    float nikomaKankeiPatternSu = parameterSu * parameterSu; //二駒関係のＰ×Ｐのパターン数。
        //    kizami = value * 1.0f / nikomaKankeiPatternSu;

        //    return kizami;
        //}

        /// <summary>
        /// 調整量を更新します。倍率を掛けます。
        /// </summary>
        /// <param name="ref_isRequestDoEvents"></param>
        /// <param name="value">調整量</param>
        /// <param name="bairitu"></param>
        public static void Up_Bairitu(ref bool ref_isRequestDoEvents, ref float value, Uc_Main uc_Main, float bairitu)
        {
            value *= bairitu;//増減

            if (value < uc_Main.TyoseiryoSettings.Smallest)// これより細かな値にはしません。
            {
                value = uc_Main.TyoseiryoSettings.Smallest;
            }
            else if (uc_Main.TyoseiryoSettings.Largest < value)// これより荒い値にはしません。
            {
                value = uc_Main.TyoseiryoSettings.Largest;
            }

            uc_Main.TxtTyoseiryo.Text = value.ToString();
            ref_isRequestDoEvents = true;//フォームを更新してほしい。            
        }

        /// <summary>
        /// １回ボタンを押すたびに。
        /// </summary>
        /// <param name="pushedCount"></param>
        /// <param name="value">調整量</param>
        public static void Up_Bairitu_AtStep(ref bool ref_isRequestDoEvents, Uc_Main uc_Main, int pushedCount, ref float value)
        {
            if (uc_Main.TyoseiryoSettings.BairituUpDic_AtStep.ContainsKey(pushedCount))
            {
                Util_Tyoseiryo.Up_Bairitu(ref ref_isRequestDoEvents, ref value, uc_Main, uc_Main.TyoseiryoSettings.BairituUpDic_AtStep[pushedCount]);
            }
        }

        /// <summary>
        /// 調整量の自動調整。
        /// 
        /// 局面評価が終わったときに、調整量を自動でクールダウンします。
        /// </summary>
        /// <param name="pushedCount"></param>
        /// <param name="value">調整量</param>
        public static void Up_Bairitu_AtEnd(ref bool ref_isRequestDoEvents, Uc_Main uc_Main, int pushedCount, ref float value)
        {
            float bairitu = 1.0f; // 倍率の最高値は、1.0 とします。

            if (uc_Main.TyoseiryoSettings.BairituCooldownDic_AtStep.ContainsKey(pushedCount))
            {
                // 倍率の最小値は、0.1 ？
                bairitu = uc_Main.TyoseiryoSettings.BairituCooldownDic_AtStep[pushedCount];
            }

            if (1.0f != bairitu)//1.0なら変化がないのでパス。それ以外の場合、調整。
            {
                Util_Tyoseiryo.Up_Bairitu(ref ref_isRequestDoEvents, ref value, uc_Main, bairitu);
            }
        }

    }
}
