using Grayscale.P055_Conv_Sy.L500Converter;
using Grayscale.P056Syugoron.I250Struct;
using System.Text;

namespace Grayscale.P056Syugoron.L500Util
{
    public abstract class Util_SySet
    {

        public static string Dump_Elements(SySet<SyElement> sySet)
        {
            StringBuilder sb = new StringBuilder();

            foreach(SyElement syElement in sySet.Elements)
            {
                sb.Append(Conv_Sy.Query_Word( syElement.Bitfield));
                sb.Append(",");
            }

            return sb.ToString();
        }

    }
}
