using Grayscale.P211_WordShogi__.L500____Word;

namespace Grayscale.P212_ConvPside__.L500____Converter
{
    public abstract class Conv_Playerside
    {

        public static Okiba ToKomadai(Playerside pside)
        {
            Okiba result;
            switch(pside)
            {
                case Playerside.P1: result = Okiba.Sente_Komadai; break;
                case Playerside.P2: result = Okiba.Gote_Komadai; break;
                default: result = Okiba.Empty; break;
            }
            return result;
        }

        public static Playerside Reverse(Playerside pside)
        {
            Playerside result;
            switch(pside)
            {
                case Playerside.P1: result = Playerside.P2; break;
                case Playerside.P2: result = Playerside.P1; break;
                default: result = pside; break;
            }
            return result;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 先後。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="pside"></param>
        /// <returns></returns>
        public static string ToKanji(Playerside pside)
        {
            string psideStr;

            switch (pside)
            {
                case Playerside.P1:
                    psideStr = "先手";
                    break;
                case Playerside.P2:
                    psideStr = "後手";
                    break;
                default:
                    psideStr = "×";
                    break;
            }

            return psideStr;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 先後。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="pside"></param>
        /// <returns></returns>
        public static string ToSankaku(Playerside pside)
        {
            string psideStr;

            switch (pside)
            {
                case Playerside.P2:
                    psideStr = "△";
                    break;

                case Playerside.P1:
                default:
                    psideStr = "▲";
                    break;
            }

            return psideStr;
        }






    }
}
