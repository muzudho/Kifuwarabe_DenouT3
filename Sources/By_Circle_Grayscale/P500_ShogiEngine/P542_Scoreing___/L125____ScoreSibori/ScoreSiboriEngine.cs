using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P542_Scoreing___.L___240_Shogisasi;
using System.Collections.Generic;
using System;

namespace Grayscale.P542_Scoreing___.L125____ScoreSibori
{

    /// <summary>
    /// 棋譜ツリーのスコアの低いノードを捨てていきます。
    /// </summary>
    public class ScoreSiboriEngine
    {

        /// <summary>
        /// 棋譜ツリーの　評価値の高いノードだけを残して、評価値の低いノードをばっさばっさ捨てて行きます。
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="atamanosumiCollection"></param>
        public void EdaSibori_HighScore(KifuTree kifu, Shogisasi shogisasi, KwErrorHandler errH)
        {
            int exception_area = 0;

            try
            {
                //
                // ノードが２つもないようなら、スキップします。
                //
                if (kifu.CurNode.Count_ChildNodes < 2)
                {
                    goto gt_EndMethod;
                }


                exception_area = 1000;
                List<Node<Starbeamable, KyokumenWrapper>> rankedNodes = this.RankingNode_WithJudge_ForeachNextNodes(
                    kifu.CurNode, errH);


                exception_area = 1500;
                Dictionary<string, Node<Starbeamable, KyokumenWrapper>> dic = new Dictionary<string, Node<Starbeamable, KyokumenWrapper>>();
                if (kifu.CurNode.Value.KyokumenConst.KaisiPside == Playerside.P1)
                {
                    exception_area = 2000;
                    // 1番高いスコアを調べます。
                    float goodestScore = float.MinValue;
                    foreach (Node<Starbeamable, KyokumenWrapper> node in rankedNodes)
                    {
                        if (node is KifuNode)
                        {
                            float score = ((KifuNode)node).Score;

                            if (goodestScore < score)
                            {
                                goodestScore = score;
                            }
                        }
                    }

                    exception_area = 2500;
                    // 1番良いスコアのノードだけ残します。
                    kifu.CurNode.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
                    {
                        float score;
                        if (node is KifuNode)
                        {
                            score = ((KifuNode)node).Score;
                        }
                        else
                        {
                            score = 0.0f;
                        }

                        if (goodestScore <= score)
                        {
                            dic.Add(key, node);
                        }
                    });
                }
                else
                {
                    exception_area = 3000;

                    // 2Pは、マイナスの方が良い。
                    float goodestScore = float.MaxValue;
                    foreach (Node<Starbeamable, KyokumenWrapper> node in rankedNodes)
                    {
                        if (node is KifuNode)
                        {
                            float score = ((KifuNode)node).Score;

                            if (score < goodestScore)//より負の値を選びます。
                            {
                                goodestScore = score;
                            }
                        }
                    }

                    exception_area = 3500;
                    // 1番良いスコアのノードだけ残します。
                    kifu.CurNode.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
                    {
                        float score;
                        if (node is KifuNode)
                        {
                            score = ((KifuNode)node).Score;
                        }
                        else
                        {
                            score = 0.0f;
                        }

                        if (score <= goodestScore)//より負数の方がよい。
                        {
                            dic.Add(key, node);
                        }
                    });
                }


                // 枝を更新します。
                kifu.CurNode.PutSet_ChildNodes(dic);
            }
            catch (Exception ex)
            {
                errH.DonimoNaranAkirameta(ex, "ベストムーブ／ハイスコア抽出中 exception_area=[" + exception_area + "]"); throw ex;
            }

        gt_EndMethod:
            ;
        }



        /// <summary>
        /// 局面が、妄想に近いかどうかで点数付けします。
        /// </summary>
        /// <param name="nextNodes"></param>
        /// <returns></returns>
        private List<Node<Starbeamable, KyokumenWrapper>> RankingNode_WithJudge_ForeachNextNodes(
            Node<Starbeamable, KyokumenWrapper> hubNode,
            KwErrorHandler errH
            )
        {
            int exception_area = 0;
            List<Node<Starbeamable, KyokumenWrapper>> list = null;

            try
            {
                // ランク付けしたあと、リスト構造に移し変えます。
                list = new List<Node<Starbeamable, KyokumenWrapper>>();

                hubNode.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
                {
                    list.Add(node);
                });

                exception_area = 1000;
                // ランク付けするために、リスト構造に変換します。

                ScoreSiboriEngine.Sort(list);
            }
            catch (Exception ex)
            {
                errH.DonimoNaranAkirameta(ex, "ベストムーブ／ハイスコア抽出中 exception_area=[" + exception_area + "]"); throw ex;
            }

            return list;
        }


        private static void Sort(List<Node<Starbeamable, KyokumenWrapper>> items)
        {
            items.Sort((a, b) =>
            {
                float bScore;
                float aScore;

                // 比較できないものは 0 にしておく必要があります。
                if (!(a is KifuNode) || !(b is KifuNode))
                {
                    return 0;
                }

                bScore = ((KifuNode)b).Score;
                aScore = ((KifuNode)a).Score;

                return (int)aScore.CompareTo(bScore);//点数が大きいほうが前に行きます。
            });

        }

    }

}
