namespace Grayscale.Kifuwarakaku.Engine.Features
{
#if DEBUG
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P339ConvKyokume.L500Converter;
using Grayscale.P521FeatureVect.I500Struct;
using Grayscale.P521FeatureVect.L500Struct;
using Grayscale.P542Scoreing.I005UsiLoop;
using Grayscale.P542Scoreing.L240Shogisasi;
using Grayscale.P542Scoreing.I250Args;
using Grayscale.P542Scoreing.L125ScoreSibori;
using Grayscale.P542Scoreing.L250Args;
using Grayscale.P554TansaFukasa.I500Struct;
using Grayscale.P554TansaFukasa.L500Struct;
using Grayscale.P440KifuTreeLog.L500Struct;
using System;
using System.Collections.Generic;
#else
    using System;
    using System.Collections.Generic;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.P324KifuTree.I250Struct;
    using Grayscale.P339ConvKyokume.L500Converter;
    using Grayscale.P440KifuTreeLog.L500Struct;
    using Grayscale.P521FeatureVect.I500Struct;
    using Grayscale.P521FeatureVect.L500Struct;
    using Grayscale.P542Scoreing.I005UsiLoop;
    using Grayscale.P542Scoreing.I250Args;
    using Grayscale.P542Scoreing.L125ScoreSibori;
    using Grayscale.P542Scoreing.L240Shogisasi;
    using Grayscale.P542Scoreing.L250Args;
    using Grayscale.P554TansaFukasa.I500Struct;
    using Grayscale.P554TansaFukasa.L500Struct;
#endif

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
        /// <returns></returns>
        public KifuNode WA_Bestmove(
            bool isHonshogi,
            KifuTree kifu
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

            //
            // 指し手生成ルーチンで、棋譜ツリーを作ります。
            //
            new Tansaku_FukasaYusen_Routine().WAA_Yomu_Start(
                kifu, isHonshogi, Mode_Tansaku.Shogi_ENgine, alphabeta_otherBranchDecidedValue, args);

#if DEBUG
            //
            // 評価明細ログの書出し
            //
            Util_KifuTreeLogWriter.A_Write_KifuTreeLog(
                logF_kiki,
                kifu,
                logTag
                );
            //Util_LogWriter500_HyokaMeisai.Log_Html5(
            //    this,
            //    logF_kiki,
            //    kifu,
            //    playerInfo,
            //    logTag
            //    );
#endif

            // 評価値の高いノードだけを残します。
            this.EdagariEngine.EdaSibori_HighScore(kifu, this);

            // 評価値の同点があれば、同点決勝をして　1手に決めます。
            KifuNode bestKifuNode = null;
            bestKifuNode = this.ChoiceNode_DoutenKessyou(kifu, isHonshogi);

            return bestKifuNode;
        }

        /// <summary>
        /// 同点決勝。
        /// 
        /// 評価値が同点のノード（指し手）の中で、ランダムに１つ選びます。
        /// </summary>
        /// <param name="kifu">ツリー構造になっている棋譜</param>
        /// <returns></returns>
        private KifuNode ChoiceNode_DoutenKessyou(
            KifuTree kifu,
            bool isHonshogi)
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
