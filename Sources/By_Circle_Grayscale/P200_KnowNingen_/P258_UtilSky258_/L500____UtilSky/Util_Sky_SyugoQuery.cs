using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P056_Syugoron___.L250____Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P215_KomanoKidou.L500____Struct;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P258_UtilSky258_.L500____UtilSky
{
    public abstract class Util_Sky_SyugoQuery
    {
        /// <summary>
        /// 升コレクション。
        /// </summary>
        /// <param name="src_Sky"></param>
        /// <param name="pside"></param>
        /// <returns></returns>
        public static SySet<SyElement> Masus_Now(SkyConst src_Sky, Playerside pside)
        {
            SySet_Default<SyElement> masus = new SySet_Default<SyElement>("今の升");

            src_Sky.Foreach_Starlights((Finger finger, Starlight mlLight, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(mlLight.Now);


                if (koma.Pside == pside && Conv_SyElement.ToOkiba(koma.Masu) == Okiba.ShogiBan)
                {
                    masus.AddElement(koma.Masu);
                }
            });

            return masus;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 駒の移動可能升
        /// ************************************************************************************************************************
        /// 
        /// ポテンシャルなので、貫通している。
        /// 
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public static SySet<SyElement> KomaKidou_Potential(Finger finger, SkyConst src_Sky)
        {
            SySet<SyElement> result;

            RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);

            //
            // ポテンシャルなので、貫通しているのは仕様通り。
            //
            // FIXME: 成香が横に進めることが分かっているか？
            //
            result = Array_Rule01_PotentialMove15.ItemMethods[(int)koma.Komasyurui](koma.Pside, koma.Masu);

            return result;
        }

    }
}
