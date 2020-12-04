﻿using Grayscale.P218Starlight.I500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;


namespace Grayscale.P341_Ittesasu___.L___250_OperationA
{
    public interface IttemodosuArg
    {
        /// <summary>
        /// 一手指し局面開始ノード。
        /// </summary>
        Node<Starbeamable, KyokumenWrapper> KaisiNode{get;set;}

        /// <summary>
        /// 指し手。棋譜に記録するために「指す前／指した後」を含めた手。
        /// </summary>
        Starbeamable Sasite { get; set; }


        /// <summary>
        /// これから作る局面の、手目済み。
        /// </summary>
        int KorekaranoTemezumi_orMinus1 { get; }

    }
}
