using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P031_Random_____.L500____Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P222_Log_Kaisetu.L250____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P339_ConvKyokume.L500____Converter;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P521_FeatureVect.L500____Struct;
using Grayscale.P542_Scoreing___.L___005_UsiLoop;
using Grayscale.P542_Scoreing___.L___240_Shogisasi;
using Grayscale.P542_Scoreing___.L___250_Args;
using Grayscale.P542_Scoreing___.L125____ScoreSibori;
using Grayscale.P542_Scoreing___.L250____Args;
using Grayscale.P554_TansaFukasa.L___500_Struct;
using Grayscale.P554_TansaFukasa.L500____Struct;
using Grayscale.P440_KifuTreeLog.L500____Struct;
using System;
using System.Collections.Generic;

#if DEBUG

#endif

namespace Grayscale.P571_KifuWarabe_.L100____Shogisasi
{
    /// <summary>
    /// 将棋指しエンジン。
    /// 
    /// 指し手を決めるエンジンです。
    /// </summary>
    public class ShogisasiImpl : Shogisasi
    {
        private ShogiEngine Owner { get { return this.owner; } }
        private ShogiEngine owner;

        /// <summary>
        /// 枝狩りエンジン。
        /// </summary>
        public ScoreSiboriEngine EdagariEngine { get; set; }

        /// <summary>
        /// 右脳。
        /// </summary>
        public FeatureVector FeatureVector { get; set; }

        public ShogisasiImpl(ShogiEngine owner)
        {
            this.owner = owner;
            this.EdagariEngine = new ScoreSiboriEngine();

            this.FeatureVector = new FeatureVectorImpl();
        }


        /// <summary>
        /// 対局開始のとき。
        /// </summary>
        public void OnTaikyokuKaisi()
        {
        }

        /// <summary>
        /// 指し手を決めます。
        /// </summary>
        /// <param name="enableLog"></param>
        /// <param name="isHonshogi"></param>
        /// <param name="kifu"></param>
        /// <param name="logTag"></param>
        /// <returns></returns>
        public KifuNode WA_Bestmove(
            bool isHonshogi,
            KifuTree kifu,
            KwErrorHandler errH
            )
        {
#if DEBUG
            KaisetuBoards logF_kiki = new KaisetuBoards();// デバッグ用だが、メソッドはこのオブジェクトを必要としてしまう。
#endif
            EvaluationArgs args = new EvaluationArgsImpl(
                kifu.GetSennititeCounter(),
                this.FeatureVector,
                this,
                Util_KifuTreeLogWriter.REPORT_ENVIRONMENT
#if DEBUG
                ,
                logF_kiki
#endif
                );

            float alphabeta_otherBranchDecidedValue;
            switch (((KifuNode)kifu.CurNode).Value.KyokumenConst.KaisiPside)
            {
                case Playerside.P1:
                    // 2プレイヤーはまだ、小さな数を見つけていないという設定。
                    alphabeta_otherBranchDecidedValue = float.MaxValue;
                    break;
                case Playerside.P2:
                    // 1プレイヤーはまだ、大きな数を見つけていないという設定。
                    alphabeta_otherBranchDecidedValue = float.MinValue;
                    break;
                default: throw new Exception("探索直前、プレイヤーサイドのエラー");
            }

            try
            {
                //
                // 指し手生成ルーチンで、棋譜ツリーを作ります。
                //
                new Tansaku_FukasaYusen_Routine().WAA_Yomu_Start(
                    kifu, isHonshogi, Mode_Tansaku.Shogi_ENgine, alphabeta_otherBranchDecidedValue, args, errH);
            }
            catch (Exception ex) { errH.DonimoNaranAkirameta(ex, "棋譜ツリーを作っていたときです。"); throw ex; }


#if DEBUG
            //
            // 評価明細ログの書出し
            //
            Util_KifuTreeLogWriter.A_Write_KifuTreeLog(
                logF_kiki,
                kifu,
                errH
                );
            //Util_LogWriter500_HyokaMeisai.Log_Html5(
            //    this,
            //    logF_kiki,
            //    kifu,
            //    playerInfo,
            //    errH
            //    );
#endif

            try
            {
                // 評価値の高いノードだけを残します。
                this.EdagariEngine.EdaSibori_HighScore(kifu, this, errH);
            }
            catch (Exception ex) { errH.DonimoNaranAkirameta(ex, "ベストムーブ後半２０：ハイスコア抽出"); throw ex; }


            // 評価値の同点があれば、同点決勝をして　1手に決めます。
            KifuNode bestKifuNode = null;
            try
            {
                bestKifuNode = this.ChoiceNode_DoutenKessyou(kifu, isHonshogi, errH);
            }
            catch (Exception ex) { errH.DonimoNaranAkirameta(ex, "ベストムーブ後半３０：同点決勝"); throw ex; }


            return bestKifuNode;
        }

        /// <summary>
        /// 同点決勝。
        /// 
        /// 評価値が同点のノード（指し手）の中で、ランダムに１つ選びます。
        /// </summary>
        /// <param name="kifu">ツリー構造になっている棋譜</param>
        /// <param name="logTag">ログ</param>
        /// <returns></returns>
        private KifuNode ChoiceNode_DoutenKessyou(
            KifuTree kifu,
            bool isHonshogi, KwErrorHandler errH)
        {
            KifuNode bestKifuNode = null;

            {
                // 次のノードをリストにします。
                //List<KifuNode> nextNodes = Util_Converter280.NextNodes_ToList(kifu.CurNode);

                // 次のノードをシャッフル済みリストにします。
                List<KifuNode> nextNodes_shuffled = Conv_NextNodes.ToList(kifu.CurNode);
                LarabeShuffle<KifuNode>.Shuffle_FisherYates(ref nextNodes_shuffled);

                // シャッフルした最初のノードを選びます。
                if (0 < nextNodes_shuffled.Count)
                {
                    bestKifuNode = nextNodes_shuffled[0];
                }
            }

            return bestKifuNode;
        }



    }
}
