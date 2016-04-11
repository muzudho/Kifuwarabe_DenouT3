using Grayscale.P055_Conv_Sy.L500____Converter;
using Grayscale.P056_Syugoron___.L___250_Struct;
using System.Text;

namespace Grayscale.P056_Syugoron___.L500____Util
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
