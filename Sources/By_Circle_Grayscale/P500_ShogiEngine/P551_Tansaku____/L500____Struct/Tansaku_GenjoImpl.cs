using Grayscale.P551_Tansaku____.L___500_Tansaku;

namespace Grayscale.P551_Tansaku____.L500____Struct
{
    /// <summary>
    /// 探索中に変化するデータです。
    /// </summary>
    public class Tansaku_GenjoImpl : Tansaku_Genjo
    {


        public Tansaku_Args Args { get { return this.args; } }
        private Tansaku_Args args;

        /// <summary>
        /// 読み開始手目済み
        /// </summary>
        public int YomikaisiTemezumi { get; set; }

        ///// <summary>
        ///// 読んでいるノード。（生成後にセット）このノードに、ツリーをぶら下げていきます。
        ///// </summary>
        //public KifuNode Node_yomi { get { return this.node_yomi; } }
        //public void SetNode_yomi(KifuNode node) {
        //    this.node_yomi = node;
        //}
        //private KifuNode node_yomi;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yomikaisiTemezumi">読み開始局面の手目済み</param>
        /// <param name="yomiArgs"></param>
        /// <param name="temezumi_yomiCur">読んでいる局面の手目済み</param>
        /// <param name="pside_teban"></param>
        public Tansaku_GenjoImpl(
            //KifuNode node_yomiKaisi,
            int temezumi,
            Tansaku_Args yomiArgs)
        {
            //this.node_yomi = node_yomiKaisi;
            this.args = yomiArgs;

            // 読み開始時の手番を記憶しておきます。
            this.YomikaisiTemezumi = temezumi;// node_yomiKaisi.Value.ToKyokumenConst.Temezumi;
        }

    }
}
