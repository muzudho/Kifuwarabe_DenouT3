using Grayscale.P209_KifuJsa____.L500____Word;

namespace Grayscale.P239_ConvWords__.L500____Converter
{
    public abstract class Conv_NariNarazu
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 成り
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="nari"></param>
        /// <returns></returns>
        public static string Nari_ToStr(NariNarazu nari)
        {
            string nariStr = "";

            switch (nari)
            {
                case NariNarazu.Nari:
                    nariStr = "成";
                    break;
                case NariNarazu.Narazu:
                    nariStr = "不成";
                    break;
                default:
                    break;
            }

            return nariStr;
        }

    }
}
