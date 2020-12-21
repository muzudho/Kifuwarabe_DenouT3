using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.P341Ittesasu.I250OperationA;


namespace Grayscale.P341Ittesasu.L250OperationA
{
    public class IttesasuArgImpl : IttesasuArg
    {
        /// <summary>
        /// 一手指し、開始局面。
        /// </summary>
        public　KyokumenWrapper KaisiKyokumen { get; set; }

        /// <summary>
        /// 一手指し、開始局面、手番。
        /// </summary>
        public Playerside KaisiTebanside { get; set; }

        /// <summary>
        /// これから指されるはずの、指し手。
        /// </summary>
        public IMove KorekaranoMove { get { return this.korekaranoMove; } }// set;
        private IMove korekaranoMove;

        /// <summary>
        /// これから作る局面の、手目済み。
        /// </summary>
        public int KorekaranoTemezumi_orMinus1 { get { return this.korekaranoTemezumi_orMinus1; } }
        private int korekaranoTemezumi_orMinus1;


        public IttesasuArgImpl(
            KyokumenWrapper kaisiKyokumen,
            Playerside tebanside,
            IMove korekaranoMove,
            int korekaranoTemezumi_orMinus1
            )
        {
            this.KaisiKyokumen = kaisiKyokumen;
            this.KaisiTebanside = tebanside;
            this.korekaranoMove = korekaranoMove;
            this.korekaranoTemezumi_orMinus1 = korekaranoTemezumi_orMinus1;
        }
    }
}
