using System.Collections.Generic;

namespace Grayscale.Kifuwarakaku.Entities.Features
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
        public static Komahaiyaku185 GetHaiyaku_KomabukuroNarazu(PieceType ks14)
        {
            Komahaiyaku185 kh;

            switch (ks14)
            {
                case PieceType.P:
                    kh = Komahaiyaku185.n172_駒袋歩;
                    break;

                case PieceType.L:
                    kh = Komahaiyaku185.n173_駒袋香;
                    break;

                case PieceType.N:
                    kh = Komahaiyaku185.n174_駒袋桂;
                    break;

                case PieceType.S:
                    kh = Komahaiyaku185.n175_駒袋銀;
                    break;

                case PieceType.G:
                    kh = Komahaiyaku185.n176_駒袋金;
                    break;

                case PieceType.K:
                    kh = Komahaiyaku185.n177_駒袋王;
                    break;

                case PieceType.R:
                    kh = Komahaiyaku185.n178_駒袋飛;
                    break;

                case PieceType.B:
                    kh = Komahaiyaku185.n179_駒袋角;
                    break;

                case PieceType.PR:
                    kh = Komahaiyaku185.n180_駒袋竜;
                    break;

                case PieceType.PB:
                    kh = Komahaiyaku185.n181_駒袋馬;
                    break;

                case PieceType.PP:
                    kh = Komahaiyaku185.n182_駒袋と金;
                    break;

                case PieceType.PL:
                    kh = Komahaiyaku185.n183_駒袋杏;
                    break;

                case PieceType.PN:
                    kh = Komahaiyaku185.n184_駒袋圭;
                    break;

                case PieceType.PS:
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
