using Grayscale.P003_Log________.L500____Struct;
using System;

namespace Grayscale.P062_ConvText___.L500____Converter
{
    public abstract class Conv_Suji
    {


        /// <summary>
        /// ************************************************************************************************************************
        /// '1'～'9' を、a～i に変換します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string ToAlphabet(char suji)
        {
            string alphabet;

            switch (suji)
            {
                case '1': alphabet = "a"; break;
                case '2': alphabet = "b"; break;
                case '3': alphabet = "c"; break;
                case '4': alphabet = "d"; break;
                case '5': alphabet = "e"; break;
                case '6': alphabet = "f"; break;
                case '7': alphabet = "g"; break;
                case '8': alphabet = "h"; break;
                case '9': alphabet = "i"; break;
                default:
                    Exception ex = new Exception("筋[" + suji + "]をアルファベットに変えることはできませんでした。");
                    Util_OwataMinister.ERROR.DonimoNaranAkirameta(ex, "筋をアルファベットに変換中☆"); throw ex;
            }

            return alphabet;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// アラビア数字（全角半角）、漢数字を、int型に変換します。変換できなかった場合、0です。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="suji"></param>
        /// <returns></returns>
        public static int ToInt(string suji)
        {
            int result;

            switch (suji)
            {
                case "1":
                case "１":
                case "一":
                    result = 1;
                    break;

                case "2":
                case "２":
                case "二":
                    result = 2;
                    break;

                case "3":
                case "３":
                case "三":
                    result = 3;
                    break;

                case "4":
                case "４":
                case "四":
                    result = 4;
                    break;

                case "5":
                case "５":
                case "五":
                    result = 5;
                    break;

                case "6":
                case "６":
                case "六":
                    result = 6;
                    break;

                case "7":
                case "７":
                case "七":
                    result = 7;
                    break;

                case "8":
                case "８":
                case "八":
                    result = 8;
                    break;

                case "9":
                case "９":
                case "九":
                    result = 9;
                    break;

                default:
                    result = 0;
                    break;

            }

            return result;
        }

    }
}
