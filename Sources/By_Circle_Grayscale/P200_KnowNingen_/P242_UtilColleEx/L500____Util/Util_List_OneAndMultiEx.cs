using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P242_UtilColleEx.L500____Util
{
    public abstract class Util_List_OneAndMultiEx<T1, T2>
    {

        public static int CountAllElements(List_OneAndMulti<Finger, SySet<SyElement>> collection)
        {
            int count = 0;

            foreach(Couple<Finger, SySet<SyElement>> entry in collection.Items)
            {
                foreach (SyElement masus in entry.B.Elements)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// 特殊な用途。
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string Dump(List_OneAndMulti<T1, T2> collection, SkyConst skyConst)
        {
            int count = 0;

            StringBuilder sb = new StringBuilder();
            {
                foreach (Couple<T1, T2> item in collection.Items)
                {
                    if (item.A is Finger && item.B is SySet<SyElement>)
                    {
                        foreach (SyElement syElement in ((SySet<SyElement>)item.B).Elements)
                        {
                            sb.AppendLine("(" + count + ") a=[" +
                                Util_Komasyurui14.Ichimoji[(int)Util_Starlightable.AsKoma(skyConst.StarlightIndexOf(item.A as Finger).Now).Komasyurui]
                                + "] b=[" +
                                Util_Masu10.ToSujiKanji(syElement)
                                + "]");
                            count++;
                        }
                    }
                    else
                    {
                        sb.AppendLine("(" + count + ") a=[" + item.A.ToString() + "] b=[" + item.B.ToString() + "]");
                        count++;
                    }
                }
            }

            return sb.ToString();
        }

    }
}
