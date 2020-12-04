using Grayscale.P218Starlight.I500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P324_KifuTree___.I250Struct;
using System.Collections.Generic;

namespace Grayscale.P339_ConvKyokume.L500Converter
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
