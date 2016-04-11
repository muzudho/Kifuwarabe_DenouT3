using Grayscale.P234_Komahaiyaku.L250____Word;
using Grayscale.P270_ForcePromot.L250____Struct;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Grayscale.P270_ForcePromot.L500____Util
{


    public abstract class Util_ForcePromotion
    {

        /// <summary>
        /// 配役と、升から、次の強制転成配役を求めます。
        /// 
        /// 
        /// </summary>
        /// <param name="currentHaiyaku"></param>
        /// <param name="masuHandle"></param>
        /// <returns>転生しないなら　未設定　を返します。</returns>
        public static Komahaiyaku185 MasuHandleTo_ForcePromotionHaiyaku(Komahaiyaku185 currentHaiyaku, int masuHandle,string hint)
        {
            Komahaiyaku185 result;

            Dictionary<int, Komahaiyaku185> map2 = Array_ForcePromotion.HaiyakuMap[currentHaiyaku];

            if (
                null == map2
                ||
                !map2.ContainsKey(masuHandle)
                )
            {
                result = Komahaiyaku185.n000_未設定;
                goto gt_EndMethod;
            }

            result = map2[masuHandle];//null非許容型


            {
                StringBuilder sbLog = new StringBuilder();

                if (File.Exists("#強制転成デバッグ.txt"))
                {
                    sbLog.Append(File.ReadAllText("#強制転成デバッグ.txt"));
                }

                sbLog.AppendLine();
                sbLog.AppendLine(hint);
                sbLog.AppendLine("　現在の配役=[" + currentHaiyaku + "]");
                sbLog.AppendLine("　masuHandle=[" + masuHandle + "]");
                sbLog.AppendLine("　強制転成後の配役=[" + result + "]");
                File.WriteAllText("#強制転成デバッグ.txt", sbLog.ToString());
            }


        gt_EndMethod:
            return result;
        }


    }


}
