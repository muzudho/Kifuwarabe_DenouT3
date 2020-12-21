using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// 指定局面から、『指差し番号』を問い合わせます。
    /// 
    /// 特殊なもの。
    /// </summary>
    public abstract class Util_Sky_FingersQueryEx
    {

        /// <summary>
        /// ************************************************************************************************************************
        /// 軌道上の駒たち
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="km"></param>
        /// <returns></returns>
        public static void Fingers_EachSrcNow(out Fingers out_fingers, SySet<SyElement> srcList, SkyConst src_Sky, Playerside pside, IMoveHalf itaru)
        {
            out_fingers = new Fingers();

            foreach (SyElement masu in srcList.Elements)
            {
                Finger finger = Util_Sky_FingerQuery.InShogibanMasuNow(src_Sky, pside, masu);
                if (Util_Finger.ForHonshogi(finger))
                {
                    // 指定の升に駒がありました。
                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    out_fingers.Add(finger);
                }
            }
        }

    }
}
