using System.Collections.Generic;
using System.IO;
using System.Text;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Nett;

namespace Grayscale.Kifuwarakaku.Entities.Features
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
        public static Komahaiyaku185 MasuHandleTo_ForcePromotionHaiyaku(IEngineConf engineConf, Komahaiyaku185 currentHaiyaku, int masuHandle, string hint)
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
                var logFilePath = engineConf.GetResourceFullPath("N20DebugForcePromotionLog");

                StringBuilder sbLog = new StringBuilder();

                if (File.Exists(logFilePath))
                {
                    sbLog.Append(File.ReadAllText(logFilePath));
                }

                sbLog.AppendLine();
                sbLog.AppendLine(hint);
                sbLog.AppendLine($"　現在の配役=[{currentHaiyaku}]");
                sbLog.AppendLine($"　masuHandle=[{masuHandle}]");
                sbLog.AppendLine($"　強制転成後の配役=[{result}]");
                File.WriteAllText(logFilePath, sbLog.ToString());
            }


        gt_EndMethod:
            return result;
        }


    }


}
