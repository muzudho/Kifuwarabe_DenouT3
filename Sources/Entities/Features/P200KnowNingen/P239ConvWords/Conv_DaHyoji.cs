using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.Entities.Features
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
