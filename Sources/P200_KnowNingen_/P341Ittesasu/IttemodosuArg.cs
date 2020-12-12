using Grayscale.P218Starlight.I500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P247KyokumenWra.L500Struct;


namespace Grayscale.P341Ittesasu.I250OperationA
{
    public interface IttemodosuArg
    {
        /// <summary>
        /// 一手指し局面開始ノード。
        /// </summary>
        Node<IMove, KyokumenWrapper> KaisiNode{get;set;}

        /// <summary>
        /// 指し手。棋譜に記録するために「指す前／指した後」を含めた手。
        /// </summary>
        IMove Move { get; set; }


        /// <summary>
        /// これから作る局面の、手目済み。
        /// </summary>
        int KorekaranoTemezumi_orMinus1 { get; }

    }
}
