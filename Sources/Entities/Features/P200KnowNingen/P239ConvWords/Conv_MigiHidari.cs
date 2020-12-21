using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Conv_MigiHidari
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 右左。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="migiHidari"></param>
        /// <returns></returns>
        public static string ToStr(MigiHidari migiHidari)
        {
            string str;

            switch (migiHidari)
            {
                case MigiHidari.Migi:
                    str = "右";
                    break;

                case MigiHidari.Hidari:
                    str = "左";
                    break;

                case MigiHidari.Sugu:
                    str = "直";
                    break;

                default:
                    str = "";
                    break;
            }

            return str;
        }

    }
}
