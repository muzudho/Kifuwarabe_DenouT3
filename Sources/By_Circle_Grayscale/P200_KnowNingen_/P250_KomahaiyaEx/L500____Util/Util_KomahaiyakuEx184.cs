using Grayscale.P031_Random_____.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L___250_Masu;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P234_Komahaiyaku.L250____Word;
using Grayscale.P234_Komahaiyaku.L500____Util;
using System.Collections.Generic;

namespace Grayscale.P250_KomahaiyaEx.L500____Util
{

    /// <summary>
    /// 配役１８４ユーティリティー。
    /// </summary>
    public abstract class Util_KomahaiyakuEx184
    {

        public static bool IsKomadai(Komahaiyaku185 haiyaku)
        {
            bool result = false;

            switch (haiyaku)
            {
                case Komahaiyaku185.n164_歩打:
                case Komahaiyaku185.n165_香打:
                case Komahaiyaku185.n166_桂打:
                case Komahaiyaku185.n167_銀打:
                case Komahaiyaku185.n168_金打:
                case Komahaiyaku185.n169_王打:
                case Komahaiyaku185.n170_飛打:
                case Komahaiyaku185.n171_角打:
                    result = true;
                    break;
            }

            return result;
        }

        public static bool IsKomabukuro(Komahaiyaku185 haiyaku)
        {
            bool result = false;

            switch (haiyaku)
            {
                case Komahaiyaku185.n172_駒袋歩:
                case Komahaiyaku185.n173_駒袋香:
                case Komahaiyaku185.n174_駒袋桂:
                case Komahaiyaku185.n175_駒袋銀:
                case Komahaiyaku185.n176_駒袋金:
                case Komahaiyaku185.n177_駒袋王:
                case Komahaiyaku185.n178_駒袋飛:
                case Komahaiyaku185.n179_駒袋角:
                case Komahaiyaku185.n180_駒袋竜:
                case Komahaiyaku185.n181_駒袋馬:
                case Komahaiyaku185.n182_駒袋と金:
                case Komahaiyaku185.n183_駒袋杏:
                case Komahaiyaku185.n184_駒袋圭:
                case Komahaiyaku185.n185_駒袋全:
                    result = true;
                    break;
            }

            return result;
        }

        /// <summary>
        /// 駒袋に入っている不成の駒。
        /// </summary>
        /// <param name="ks14"></param>
        /// <returns></returns>
        public static Komahaiyaku185 GetHaiyaku_KomabukuroNarazu(Komasyurui14 ks14)
        {
            Komahaiyaku185 kh;

            switch (ks14)
            {
                case Komasyurui14.H01_Fu_____:
                    kh = Komahaiyaku185.n172_駒袋歩;
                    break;

                case Komasyurui14.H02_Kyo____:
                    kh = Komahaiyaku185.n173_駒袋香;
                    break;

                case Komasyurui14.H03_Kei____:
                    kh = Komahaiyaku185.n174_駒袋桂;
                    break;

                case Komasyurui14.H04_Gin____:
                    kh = Komahaiyaku185.n175_駒袋銀;
                    break;

                case Komasyurui14.H05_Kin____:
                    kh = Komahaiyaku185.n176_駒袋金;
                    break;

                case Komasyurui14.H06_Gyoku__:
                    kh = Komahaiyaku185.n177_駒袋王;
                    break;

                case Komasyurui14.H07_Hisya__:
                    kh = Komahaiyaku185.n178_駒袋飛;
                    break;

                case Komasyurui14.H08_Kaku___:
                    kh = Komahaiyaku185.n179_駒袋角;
                    break;

                case Komasyurui14.H09_Ryu____:
                    kh = Komahaiyaku185.n180_駒袋竜;
                    break;

                case Komasyurui14.H10_Uma____:
                    kh = Komahaiyaku185.n181_駒袋馬;
                    break;

                case Komasyurui14.H11_Tokin__:
                    kh = Komahaiyaku185.n182_駒袋と金;
                    break;

                case Komasyurui14.H12_NariKyo:
                    kh = Komahaiyaku185.n183_駒袋杏;
                    break;

                case Komasyurui14.H13_NariKei:
                    kh = Komahaiyaku185.n184_駒袋圭;
                    break;

                case Komasyurui14.H14_NariGin:
                    kh = Komahaiyaku185.n185_駒袋全;
                    break;

                default:
                    // エラー
                    kh = Komahaiyaku185.n000_未設定;
                    break;
            }

            return kh;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>マス番号</returns>
        public static int Move_RandomChoice(Komahaiyaku185 haiyaku)
        {
            int result;

            if (Util_Komahaiyaku184.KukanMasus[haiyaku].Count <= 0)
            {
                result = -1;
                goto gt_EndMethod;
            }

            SySet<SyElement> michi187 = Util_Komahaiyaku184.KukanMasus[haiyaku][KwRandom.Random.Next(Util_Komahaiyaku184.KukanMasus[haiyaku].Count)];

            List<int> elements = new List<int>();
            foreach (New_Basho element in michi187.Elements)
            {
                elements.Add(element.MasuNumber);
            }

            result = elements[KwRandom.Random.Next(elements.Count)];

        gt_EndMethod:
            return result;
        }

    }
}
