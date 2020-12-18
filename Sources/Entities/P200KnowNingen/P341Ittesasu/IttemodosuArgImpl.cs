using Grayscale.P218Starlight.I500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P341Ittesasu.I250OperationA;


namespace Grayscale.P341Ittesasu.L250OperationA
{
    public class IttemodosuArgImpl : IttemodosuArg
    {
        /// <summary>
        /// 一手指し局面開始ノード。
        /// </summary>
        public Node<IMove, KyokumenWrapper> KaisiNode { get; set; }

        /// <summary>
        /// 指し手。
        /// </summary>
        public IMove Move { get; set; }

        /// <summary>
        /// これから作る局面の、手目済み。
        /// </summary>
        public int KorekaranoTemezumi_orMinus1 { get { return this.korekaranoTemezumi_orMinus1; } }
        private int korekaranoTemezumi_orMinus1;

        public IttemodosuArgImpl(
            Node<IMove, KyokumenWrapper> kaisiNode,
            IMove move,
            int korekaranoTemezumi_orMinus1
            )
        {
            this.KaisiNode = kaisiNode;
            this.Move = move;
            this.korekaranoTemezumi_orMinus1 = korekaranoTemezumi_orMinus1;
        }
    }
}
