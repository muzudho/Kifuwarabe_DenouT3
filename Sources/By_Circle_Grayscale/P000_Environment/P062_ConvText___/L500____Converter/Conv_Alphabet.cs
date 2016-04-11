
namespace Grayscale.P062_ConvText___.L500____Converter
{
    public abstract class Conv_Alphabet
    {

        /// <summary>
        /// ************************************************************************************************************************
        /// a～i を、1～9 に変換します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static int ToInt(string alphabet)
        {
            int num;

            switch (alphabet)
            {
                case "a": num = 1; break;
                case "b": num = 2; break;
                case "c": num = 3; break;
                case "d": num = 4; break;
                case "e": num = 5; break;
                case "f": num = 6; break;
                case "g": num = 7; break;
                case "h": num = 8; break;
                case "i": num = 9; break;
                default: num = -1; break;
            }

            return num;
        }
    }
}
