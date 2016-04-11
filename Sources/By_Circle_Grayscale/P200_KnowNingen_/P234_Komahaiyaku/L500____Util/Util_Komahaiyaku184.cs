using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P234_Komahaiyaku.L250____Word;
using System.Collections.Generic;

namespace Grayscale.P234_Komahaiyaku.L500____Util
{


    /// <summary>
    /// 駒配役１８４だぜ☆
    /// 
    /// 「1,歩,1,,,,,,,,,,,,,,,,,,,,,,」といった内容を、
    /// [1]「空間1」に置き換えるぜ☆
    /// 
    /// 駒の情報はそぎ落とすぜ☆　筋の情報だけが残る☆
    /// </summary>
    public abstract class Util_Komahaiyaku184
    {

        /// <summary>
        /// 配役名。
        /// </summary>
        public static List<string> Name { get { return Util_Komahaiyaku184.name; } }
        private static List<string> name;

        /// <summary>
        /// 絵修飾字。
        /// </summary>
        public static List<string> Name2 { get { return Util_Komahaiyaku184.name2; } }
        private static List<string> name2;

        /// <summary>
        /// 種類。
        /// </summary>
        public static Komasyurui14 Syurui(Komahaiyaku185 haiyaku)
        {
            return Util_Komahaiyaku184.syurui[(int)haiyaku];
        }
        public static void AddSyurui(Komasyurui14 syurui)
        {
            Util_Komahaiyaku184.syurui.Add(syurui);
        }
        private static List<Komasyurui14> syurui;


        /// <summary>
        /// 空間フィールド。（１～２４個）
        /// </summary>
        public static Dictionary<Komahaiyaku185, List<SySet<SyElement>>> KukanMasus { get { return Util_Komahaiyaku184.kukanMasus; } }
        private static Dictionary<Komahaiyaku185, List<SySet<SyElement>>> kukanMasus;

        /// <summary>
        /// 初期化が済んでいれば真。
        /// </summary>
        public static bool IsActive()
        {
            return Util_Komahaiyaku184.syurui.Count != 0;
        }

        static Util_Komahaiyaku184()
        {
            Util_Komahaiyaku184.kukanMasus = new Dictionary<Komahaiyaku185, List<SySet<SyElement>>>();
            Util_Komahaiyaku184.syurui = new List<Komasyurui14>();
            Util_Komahaiyaku184.name = new List<string>();
            Util_Komahaiyaku184.name2 = new List<string>();
        }






    }
}
