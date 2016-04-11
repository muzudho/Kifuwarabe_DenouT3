using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P239_ConvWords__.L500____Converter;
using Grayscale.P321_KyokumHyoka.L250____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P531_Hyokakansu_.L___500_Hyokakansu;
using Grayscale.P531_Hyokakansu_.L510____HyokakansuColl;
using Grayscale.P542_Scoreing___.L___250_Args;
using System;

#if DEBUG || LEARN
using Grayscale.P321_KyokumHyoka.L___250_Struct;
#endif

namespace Grayscale.P542_Scoreing___.L500____Util
{
    /// <summary>
    /// 得点付けを行います。
    /// </summary>
    public abstract class Util_Scoreing
    {

        public static float Initial_BestScore(SkyConst src_Sky)
        {
            // このノードが、どちらの手番か。
            Playerside pside = src_Sky.KaisiPside;

            float alphabeta_bestScore;// プレイヤー1ならmax値、プレイヤー2ならmin値。
            switch (pside)
            {
                case Playerside.P1:
                    // 1プレイヤーはまだ、大きな数を見つけていないという設定。
                    alphabeta_bestScore = float.MinValue;
                    break;
                case Playerside.P2:
                    // 2プレイヤーはまだ、小さな数を見つけていないという設定。
                    alphabeta_bestScore = float.MaxValue;
                    break;
                default: throw new Exception("探索中、プレイヤーサイドのエラー");
            }

            return alphabeta_bestScore;
        }

        /// <summary>
        /// ベスト・スコアを更新します。
        /// アルファ・カットの有無も調べます。
        /// </summary>
        /// <param name="node_yomi"></param>
        /// <param name="a_parentsiblingDecidedValue"></param>
        /// <param name="a_myScore"></param>
        /// <param name="ref_a_childBest"></param>
        /// <param name="alpha_cut"></param>
        public static void Update_BestScore_And_Check_AlphaCut(
            int yomiDeep,//1start
            KifuNode node_yomi,
            float a_parentsiblingDecidedValue,
            float a_myScore,
            ref float ref_a_childBest,
            out bool alpha_cut
            )
        {
            // このノードが、どちらの手番か。
            Playerside pside = node_yomi.Value.KyokumenConst.KaisiPside;

            alpha_cut = false;
            switch (pside)
            {
                case Playerside.P1:
                    // 1プレイヤーは、大きな数を見つけたい。
                    if (ref_a_childBest < a_myScore)
                    {
                        ref_a_childBest = a_myScore;
                    }
                    //----------------------------------------
                    // アルファー・カット
                    //----------------------------------------
                    if (1<yomiDeep && a_parentsiblingDecidedValue < ref_a_childBest)
                    {
                        // 親の兄が既に見つけている数字より　大きな数字を見つけた場合
                        alpha_cut = true;//探索を打ち切り
                    }
                    break;
                case Playerside.P2:
                    // 2プレイヤーは、小さな数を見つけたい。
                    if (a_myScore < ref_a_childBest)
                    {
                        ref_a_childBest = a_myScore;
                    }
                    //----------------------------------------
                    // アルファー・カット
                    //----------------------------------------
                    if (1 < yomiDeep && ref_a_childBest < a_parentsiblingDecidedValue)
                    {
                        // 親の兄が既に見つけている数字より　小さな数字を見つけた場合
                        alpha_cut = true;//探索を打ち切り
                    }
                    break;
                default: throw new Exception("子要素探索中、プレイヤーサイドのエラー");
            }
        }

        /// <summary>
        /// 局面に、評価値を付けます。
        /// </summary>
        public static void DoScoreing_Kyokumen(
            KifuNode node_yomi_mutable,
            EvaluationArgs args,
            KwErrorHandler errH
            )
        {
            //----------------------------------------
            // 千日手判定
            //----------------------------------------
            bool isSennitite;
            {
                ulong hash = Conv_Sky.ToKyokumenHash(node_yomi_mutable.Value.KyokumenConst);
                if (args.SennititeConfirmer.IsNextSennitite(hash))
                {
                    // 千日手になる場合。
                    isSennitite = true;
                }
                else
                {
                    isSennitite = false;
                }
            }

            // 局面スコア
            node_yomi_mutable.KyHyokaSheet_Mutable.Clear();

            if (isSennitite)
            {
                // 千日手用の評価をします。
                Hyokakansu hyokakansu = Util_HyokakansuCollection.Hyokakansu_Sennichite;

                float score;
#if DEBUG || LEARN
                KyHyokaMeisai_Koumoku meisai;
#endif
                hyokakansu.Evaluate(
                    out score,
#if DEBUG || LEARN
                    out meisai,
#endif
                    node_yomi_mutable.Value.KyokumenConst,
                    args.FeatureVector,
                    errH
                );

                node_yomi_mutable.AddScore(score);
#if DEBUG || LEARN
                node_yomi_mutable.KyHyokaSheet_Mutable.Add(
                    hyokakansu.Name.ToString(),
                    meisai
                );
#endif
            }
            else
            {
                Util_HyokakansuCollection.EvaluateAll_Normal(
                    node_yomi_mutable,
                    args.FeatureVector,
                    errH
                    );
            }

        }

        public static void Update_Branch(
            float alphabeta_bestScore,
            KifuNode node_yomi_mutable
            )
        {
            // FIXME: 点数（評価明細）を上書きしているような。
            // 枝はこれでいいのか？
            node_yomi_mutable.SetScore( alphabeta_bestScore);
            node_yomi_mutable.SetBranchKyHyokaSheet(new KyHyokaSheetImpl( alphabeta_bestScore));
        }

    }
}
