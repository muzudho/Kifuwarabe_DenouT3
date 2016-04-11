using Grayscale.P062_ConvText___.L500____Converter;
using System.Collections.Generic;
using System.Text;


namespace Grayscale.P780_SgSyugoTest.L500____ShogiSyugoronTest
{
    public class Sample_Func
    {

        /// <summary>
        /// 角道を想定しています。
        /// 「８八」を指定すれば、答えは「７七,６六,５五,４四,３三,２二,１一」を返すことを想定しています。
        /// 
        /// TODO:先後
        /// </summary>
        /// <param name="prm1"></param>
        /// <returns></returns>
        public static string func右上一直線升たち(string prm1)
        {
            StringBuilder sb = new StringBuilder();

            // 例「８八」
            int suji = Conv_Suji.ToInt(prm1.Trim().ToCharArray()[0].ToString());//８→8
            int dan = Conv_Suji.ToInt(prm1.Trim().ToCharArray()[1].ToString());//八→8

            bool first = true;
            suji--;
            dan--;
            while (0 < suji && 0 < dan)//本将棋盤という前提がある
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append(",");
                }

                sb.Append(Conv_Int.ToArabiaSuji(suji));
                sb.Append(Conv_Int.ToKanSuji(dan));

                suji--;
                dan--;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 角道を想定しています。
        /// 「８八」を指定すれば、答えは「７九」を返すことを想定しています。
        /// 
        /// TODO:先後
        /// </summary>
        /// <param name="prmList_Str">引数のドット区切りリスト</param>
        /// <returns></returns>
        public static string func右下一直線升たち(string prmList_Str)
        {
            StringBuilder sb = new StringBuilder();

            List<string> prmList = new List<string>();

            if (-1 != prmList_Str.IndexOf('.'))
            {
                string[] prmArray = prmList_Str.Split('.');

                prmList.AddRange(prmArray);
            }
            else
            {
                prmList.Add(prmList_Str);
            }

            int prmIx = 0;
            foreach(string prm in prmList)
            {
                switch(prmIx)
                {
                    case 0:
                        {
                            // 例「８八」
                            int suji = Conv_Suji.ToInt(prmList_Str.Trim().ToCharArray()[0].ToString());//８→8
                            int dan = Conv_Suji.ToInt(prmList_Str.Trim().ToCharArray()[1].ToString());//八→8

                            bool first = true;
                            suji--;
                            dan++;
                            while (0 < suji && dan <= 9)//本将棋盤という前提がある
                            {
                                if (first)
                                {
                                    first = false;
                                }
                                else
                                {
                                    sb.Append(",");
                                }

                                sb.Append(Conv_Int.ToArabiaSuji(suji));
                                sb.Append(Conv_Int.ToKanSuji(dan));

                                suji--;
                                dan++;
                            }
                        }
                        break;
                    default:
                        {
                        }
                        break;
                }

                prmIx++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 角道を想定しています。
        /// 「８八」を指定すれば、答えは「９九」を返すことを想定しています。
        /// 
        /// TODO:先後
        /// </summary>
        /// <param name="prm1"></param>
        /// <returns></returns>
        public static string func左下一直線升たち(string prm1)
        {
            StringBuilder sb = new StringBuilder();

            // 例「８八」
            int suji = Conv_Suji.ToInt(prm1.Trim().ToCharArray()[0].ToString());//８→8
            int dan = Conv_Suji.ToInt(prm1.Trim().ToCharArray()[1].ToString());//八→8

            bool first = true;
            suji++;
            dan++;
            while (suji <= 9 && dan <= 9)//本将棋盤という前提がある
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append(",");
                }

                sb.Append(Conv_Int.ToArabiaSuji(suji));
                sb.Append(Conv_Int.ToKanSuji(dan));

                suji++;
                dan++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 角道を想定しています。
        /// 「８八」を指定すれば、答えは「９七」を返すことを想定しています。
        /// 
        /// TODO:先後
        /// </summary>
        /// <param name="prm1"></param>
        /// <returns></returns>
        public static string func左上一直線升たち(string prm1)
        {
            StringBuilder sb = new StringBuilder();

            // 例「８八」
            int suji = Conv_Suji.ToInt(prm1.Trim().ToCharArray()[0].ToString());//８→8
            int dan = Conv_Suji.ToInt(prm1.Trim().ToCharArray()[1].ToString());//八→8

            bool first = true;
            suji++;
            dan--;
            while (suji <= 9 && 0<dan)//本将棋盤という前提がある
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append(",");
                }

                sb.Append(Conv_Int.ToArabiaSuji(suji));
                sb.Append(Conv_Int.ToKanSuji(dan));

                suji++;
                dan--;
            }

            return sb.ToString();
        }

    }
}
