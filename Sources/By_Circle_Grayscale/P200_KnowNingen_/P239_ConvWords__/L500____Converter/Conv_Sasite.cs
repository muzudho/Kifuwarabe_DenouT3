using Grayscale.P211WordShogi.L500Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P234Komahaiyaku.L500Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;

namespace Grayscale.P239_ConvWords__.L500____Converter
{
    public abstract class Conv_Sasite
    {

        /// <summary>
        /// FIXME:使ってない？
        /// </summary>
        /// <param name="sasite"></param>
        /// <returns></returns>
        public static string Sasite_To_KsString_ForLog(Starbeamable sasite)
        {
            string sasiteInfo;

            RO_Star koma = Util_Starlightable.AsKoma(sasite.Now);

            sasiteInfo = Util_Komasyurui14.ToIchimoji(Util_Komahaiyaku184.Syurui(koma.Haiyaku));

            return sasiteInfo;
        }

        public static string Sasite_To_KsString_ForLog(Starbeamable sasite, Playerside pside_genTeban)
        {
            string result;

            if (null == sasite)
            {
                result = "合法手はありません。";
                goto gt_EndMethod;
            }

            RO_Star koma = Util_Starlightable.AsKoma(sasite.Now);

            // 指し手を「△歩」といった形で。
            result = Util_Komasyurui14.ToNimoji(Util_Komahaiyaku184.Syurui(koma.Haiyaku), pside_genTeban);

        gt_EndMethod:
            return result;
        }

    }
}
