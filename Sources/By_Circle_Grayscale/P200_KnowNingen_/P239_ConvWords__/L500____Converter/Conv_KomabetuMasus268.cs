using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P239_ConvWords__.L500____Converter
{
    /// <summary>
    /// 使ってない？
    /// </summary>
    public abstract class Conv_KomabetuMasus268
    {
        /// <summary>
        /// FIXME: 使ってない？
        /// 
        /// 変換『「駒→手」のコレクション』→『「駒、指し手」のペアのリスト』
        /// </summary>
        public static List<Couple<Finger, SyElement>> ToList(
            Maps_OneAndOne<Finger, SySet<SyElement>> km
            )
        {
            List<Couple<Finger, SyElement>> kmList = new List<Couple<Finger, SyElement>>();

            foreach (Finger koma in km.ToKeyList())
            {
                SySet<SyElement> masus = km.ElementAt(koma);

                foreach (SyElement masu in masus.Elements)
                {
                    // セットとして作っているので、重複エレメントは無いはず……☆
                    kmList.Add(new Couple<Finger, SyElement>(koma, masu));
                }
            }

            return kmList;
        }

    }
}
