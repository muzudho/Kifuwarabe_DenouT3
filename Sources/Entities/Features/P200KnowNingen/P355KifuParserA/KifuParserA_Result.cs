using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.P355_KifuParserA.I500Parser
{
    public interface KifuParserA_Result
    {
        Node<IMove, KyokumenWrapper> Out_newNode_OrNull { get; set; }
    }
}
