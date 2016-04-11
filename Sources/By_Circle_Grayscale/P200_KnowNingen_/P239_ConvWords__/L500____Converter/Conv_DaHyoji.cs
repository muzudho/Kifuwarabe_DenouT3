using Grayscale.P209_KifuJsa____.L500____Word;

namespace Grayscale.P239_ConvWords__.L500____Converter
{
    public abstract class Conv_DaHyoji
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 打。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="da"></param>
        /// <returns></returns>
        public static string ToBool(DaHyoji da)
        {
            string daStr = "";

            if (DaHyoji.Visible == da)
            {
                daStr = "打";
            }

            return daStr;
        }

    }
}
