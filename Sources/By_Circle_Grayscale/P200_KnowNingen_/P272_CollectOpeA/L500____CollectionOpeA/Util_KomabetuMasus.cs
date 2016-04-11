using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L___250_Masu;
using Grayscale.P246_MasusWriter.L500____Util;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using System.Collections.Generic;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P272_CollectOpeA.L500____CollectionOpeA
{
    public abstract class Util_KomabetuMasus
    {

        public static string LogString_Set(
            Maps_OneAndOne<Finger, SySet<SyElement>> komabetuMasus
            )
        {
            StringBuilder sb = new StringBuilder();

            // 全要素
            komabetuMasus.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                sb.AppendLine("駒＝[" + key + "]");
                sb.AppendLine(Util_Masus<New_Basho>.LogString_Concrete(value));
            });

            return sb.ToString();
        }


        public static string Dump(
            Maps_OneAndOne<Finger, SySet<SyElement>> komabetuMasus
            )
        {
            StringBuilder sb = new StringBuilder();

            komabetuMasus.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                foreach (New_Basho masu in value.Elements)
                {
                    sb.AppendLine("finger=[" + key.ToString() + "] masu=[" + value.ToString() + "]");
                }
            });

            return sb.ToString();
        }

        /// <summary>
        /// マージします。
        /// </summary>
        /// <param name="right"></param>
        public static void Merge(
            Maps_OneAndOne<Finger, SySet<SyElement>> komabetuMasus,
            Maps_OneAndOne<Finger, SySet<SyElement>> right
            )
        {
            right.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                Util_Sky258A.AddOverwrite(komabetuMasus, key, value);
                //if (komabetuMasus.entries.ContainsKey(entry.Key))
                //{
                //    // キーが重複していれば、value同士でマージします。

                //    komabetuMasus.entries[entry.Key].AddSupersets(entry.Value);

                //}
                //else
                //{
                //    // 新キーなら
                //    komabetuMasus.entries.Add(entry.Key, entry.Value);
                //}

            });
        }


        /// <summary>
        /// 無ければ追加、あれば上書き。
        /// </summary>
        /// <param name="hKoma"></param>
        /// <param name="masus"></param>
        public static void AddOverwrite(
            Maps_OneAndMulti<Finger, SyElement> komabetuMasu,
            Finger finger, SyElement masu)
        {
            if (komabetuMasu.Items.ContainsKey(finger))
            {
                komabetuMasu.Items[finger].Add(masu);//追加します。
            }
            else
            {
                // 無かったので、新しく追加します。
                List<SyElement> list = new List<SyElement>();
                list.Add(masu);
                komabetuMasu.Items.Add(finger, list);
            }
        }


        public static string LogString_Concrete(
            Maps_OneAndOne<Finger, SySet<SyElement>> komabetuMasus
            )
        {
            StringBuilder sb = new StringBuilder();

            komabetuMasus.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                sb.Append("[駒");
                sb.Append(key);
                sb.Append("]");

                foreach (New_Basho masu in value.Elements)
                {
                    sb.Append(Util_Masus<New_Basho>.LogString_Concrete(value));
                    //sb.Append(Masu81Array.Items[hMasu].ToString());
                }
            });


            return sb.ToString();

        }


    }
}
