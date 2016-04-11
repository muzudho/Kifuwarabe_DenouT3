using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using System.Collections.Generic;

namespace Grayscale.P339_ConvKyokume.L500____Converter
{
    public abstract class Conv_NextNodes
    {

        /// <summary>
        /// 変換『「指し手→局面」のコレクション』→『「「指し手→局面」のリスト』
        /// </summary>
        public static List<KifuNode> ToList(
            Node<Starbeamable, KyokumenWrapper> hubNode
            )
        {
            List<KifuNode> list = new List<KifuNode>();

            // TODO:
            hubNode.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
            {
                list.Add((KifuNode)node);
            });

            return list;
        }

    }
}
