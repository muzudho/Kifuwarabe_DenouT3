using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.Kifuwarakaku.Entities.Features;
using System;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Conv_Int
    {


        /// <summary>
        /// ************************************************************************************************************************
        /// 1～9 を、a～i に変換します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string ToAlphabet(int num)
        {
            string alphabet;

            switch (num)
            {
                case 1: alphabet = "a"; break;
                case 2: alphabet = "b"; break;
                case 3: alphabet = "c"; break;
                case 4: alphabet = "d"; break;
                case 5: alphabet = "e"; break;
                case 6: alphabet = "f"; break;
                case 7: alphabet = "g"; break;
                case 8: alphabet = "h"; break;
                case 9: alphabet = "i"; break;
                default:
                    throw new Exception($"筋[{num}]をアルファベットに変えることはできませんでした。");
            }

            return alphabet;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 数値をアラビア数字に変換します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="num">0～9</param>
        /// <returns></returns>
        public static string ToArabiaSuji(int num)
        {
            string numStr;

            if (0 <= num && num <= 9)
            {
                numStr = TextConst.ARABIA_SUJI_ZENKAKU[num];
            }
            else
            {
                numStr = "×";
            }

            return numStr;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 数値を漢数字に変換します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToKanSuji(int num)
        {
            string numStr;

            if (1 <= num && num <= 9)
            {
                numStr = TextConst.KAN_SUJI[num - 1];
            }
            else
            {
                numStr = "×";
            }

            return numStr;
        }


    }
}
