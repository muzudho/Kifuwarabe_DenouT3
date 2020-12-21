using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P542Scoreing.L240Shogisasi;
using System.Collections.Generic;
using System;

namespace Grayscale.P542Scoreing.L125ScoreSibori
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
        public void EdaSibori_HighScore(KifuTree kifu, Shogisasi shogisasi)
        {
            //
            // ノードが２つもないようなら、スキップします。
            //
            if (kifu.CurNode.Count_ChildNodes < 2)
            {
                goto gt_EndMethod;
            }


            List<Node<IMove, KyokumenWrapper>> rankedNodes = this.RankingNode_WithJudge_ForeachNextNodes(
                kifu.CurNode);

            Dictionary<string, Node<IMove, KyokumenWrapper>> dic = new Dictionary<string, Node<IMove, KyokumenWrapper>>();
            if (kifu.CurNode.Value.KyokumenConst.KaisiPside == Playerside.P1)
            {
                // 1番高いスコアを調べます。
                float goodestScore = float.MinValue;
                foreach (Node<IMove, KyokumenWrapper> node in rankedNodes)
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

                // 1番良いスコアのノードだけ残します。
                kifu.CurNode.Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
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
                // 2Pは、マイナスの方が良い。
                float goodestScore = float.MaxValue;
                foreach (Node<IMove, KyokumenWrapper> node in rankedNodes)
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

                // 1番良いスコアのノードだけ残します。
                kifu.CurNode.Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
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

        gt_EndMethod:
            ;
        }



        /// <summary>
        /// 局面が、妄想に近いかどうかで点数付けします。
        /// </summary>
        /// <param name="nextNodes"></param>
        /// <returns></returns>
        private List<Node<IMove, KyokumenWrapper>> RankingNode_WithJudge_ForeachNextNodes(
            Node<IMove, KyokumenWrapper> hubNode
            )
        {
            List<Node<IMove, KyokumenWrapper>> list = null;

            // ランク付けしたあと、リスト構造に移し変えます。
            list = new List<Node<IMove, KyokumenWrapper>>();

            hubNode.Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
            {
                list.Add(node);
            });

            // ランク付けするために、リスト構造に変換します。

            ScoreSiboriEngine.Sort(list);

            return list;
        }


        private static void Sort(List<Node<IMove, KyokumenWrapper>> items)
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
