using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P211_WordShogi__.L___250_Masu;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P214_Masu_______.L500____Util;
using System.Text;

namespace Grayscale.P246_MasusWriter.L250____Writer
{
    public abstract class Writer_Masus
    {

        /// <summary>
        /// デバッグ用文字列を作ります。
        /// </summary>
        /// <param name="masus"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public static string Log_Masus(SySet<SyElement> masus, string memo)
        {
            StringBuilder sb = new StringBuilder();

            int errorCount = 0;

            // フォルスクリア
            bool[] ban81 = new bool[81];

            // フラグ立て
            foreach (New_Basho basho in masus.Elements)
            {
                if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(Masu_Honshogi.Masus_All[basho.MasuNumber]))
                {
                    ban81[basho.MasuNumber] = true;
                }
            }



            sb.AppendLine("...(^▽^)さて、局面は☆？");

            if (null != memo && "" != memo.Trim())
            {
                sb.AppendLine(memo);
            }

            sb.AppendLine("　９　８　７　６　５　４　３　２　１");
            sb.AppendLine("┏━┯━┯━┯━┯━┯━┯━┯━┯━┓");
            for (int dan = 1; dan <= 9; dan++)
            {
                sb.Append("┃");
                for (int suji = 9; suji >= 1; suji--)// 筋は左右逆☆
                {
                    SyElement masu = Util_Masu10.OkibaSujiDanToMasu(Okiba.ShogiBan, suji, dan);
                    if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu))
                    {
                        if (ban81[Conv_SyElement.ToMasuNumber(masu)])
                        {
                            sb.Append("●");
                        }
                        else
                        {
                            sb.Append("  ");
                        }
                    }
                    else
                    {
                        errorCount++;
                        sb.Append("  ");
                    }


                    if (suji == 1)//１筋が最後だぜ☆
                    {
                        sb.Append("┃");
                        sb.AppendLine(Conv_Int.ToKanSuji(dan));
                    }
                    else
                    {
                        sb.Append("│");
                    }
                }

                if (dan == 9)
                {
                    sb.AppendLine("┗━┷━┷━┷━┷━┷━┷━┷━┷━┛");
                }
                else
                {
                    sb.AppendLine("┠─┼─┼─┼─┼─┼─┼─┼─┼─┨");
                }
            }


            // 後手駒台
            sb.Append("エラー数：");
            sb.AppendLine(errorCount.ToString());
            sb.AppendLine("...(^▽^)ﾄﾞｳﾀﾞｯﾀｶﾅ～☆");


            return sb.ToString();
        }

    }
}
