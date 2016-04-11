using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P062_ConvText___.L250____Const;
using System;

namespace Grayscale.P062_ConvText___.L500____Converter
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
                    Exception ex = new Exception("筋[" + num + "]をアルファベットに変えることはできませんでした。");
                    Util_OwataMinister.ERROR.DonimoNaranAkirameta(ex, "筋をアルファベットに変換中☆"); throw ex;
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
