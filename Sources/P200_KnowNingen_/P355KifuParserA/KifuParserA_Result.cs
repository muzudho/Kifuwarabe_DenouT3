using Grayscale.P218Starlight.I500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P247KyokumenWra.L500Struct;

namespace Grayscale.P355_KifuParserA.I500Parser
{
    public interface KifuParserA_Result
    {
        Node<Starbeamable, KyokumenWrapper> Out_newNode_OrNull { get; set; }
    }
}
