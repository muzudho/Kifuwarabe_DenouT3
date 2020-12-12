using Grayscale.P211WordShogi.L500Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P234Komahaiyaku.L500Util;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;

namespace Grayscale.P239ConvWords.L500Converter
{
    public abstract class ConvMove
    {

        /// <summary>
        /// FIXME:使ってない？
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public static string ChangeMoveTo_KsString_ForLog(IMove move)
        {
            string moveInfo;

            RO_Star koma = Util_Starlightable.AsKoma(move.Now);

            moveInfo = Util_Komasyurui14.ToIchimoji(Util_Komahaiyaku184.Syurui(koma.Haiyaku));

            return moveInfo;
        }

        public static string ChangeMoveTo_KsString_ForLog(IMove move, Playerside pside_genTeban)
        {
            string result;

            if (null == move)
            {
                result = "合法手はありません。";
                goto gt_EndMethod;
            }

            RO_Star koma = Util_Starlightable.AsKoma(move.Now);

            // 指し手を「△歩」といった形で。
            result = Util_Komasyurui14.ToNimoji(Util_Komahaiyaku184.Syurui(koma.Haiyaku), pside_genTeban);

        gt_EndMethod:
            return result;
        }

    }
}
