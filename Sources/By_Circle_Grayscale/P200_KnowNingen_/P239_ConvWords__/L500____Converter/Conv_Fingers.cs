using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P056_Syugoron___.L250____Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P239_ConvWords__.L500____Converter
{
    public abstract class Conv_Fingers
    {

        /// <summary>
        /// フィンガー番号→駒→駒のある升の集合
        /// </summary>
        /// <param name="fingers"></param>
        /// <param name="src_Sky"></param>
        /// <returns></returns>
        public static SySet<SyElement> ToMasus(Fingers fingers, SkyConst src_Sky)
        {
            SySet<SyElement> masus = new SySet_Default<SyElement>("何かの升");

            foreach (Finger finger in fingers.Items)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);


                masus.AddElement(koma.Masu);
            }

            return masus;
        }
    }
}
