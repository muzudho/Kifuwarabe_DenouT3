using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.P341Ittesasu.I250OperationA
{
    public interface IttesasuArg
    {
        /// <summary>
        /// 一手指し、開始局面。
        /// </summary>
        KyokumenWrapper KaisiKyokumen{get;set;}

        /// <summary>
        /// 一手指し、開始局面、手番。
        /// </summary>
        Playerside KaisiTebanside { get; set; }

        /// <summary>
        /// 一手指し、終了局面。これから指されるはずの手。棋譜に記録するために「指す前／指した後」を含めた手。
        /// </summary>
        IMove KorekaranoMove { get; }// set;

        /// <summary>
        /// これから作る局面の、手目済み。
        /// </summary>
        int KorekaranoTemezumi_orMinus1{get;}
    }
}
